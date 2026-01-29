using ExtraSkillSlots;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.UI;
using ShiggyMod.Modules.Networking;
using ShiggyMod.Modules.Survivors;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace ShiggyMod.Modules.Quirks
{
    /// <summary>
    /// Master-persistent controller for Apex Surgery / Adaptation (MonoBehaviour port).
    /// Keeps ALL original functionality:
    /// - Hold-to-reset input (authority client) -> sends request to server
    /// - Server mutates: stock reset + apex buff stacks + adaptation stacks
    /// - Adaptation sync via INetMessage (replaces SyncVar)
    /// - Overdrive notify via INetMessage (replaces TargetRpc)
    /// - HUD overlay lazy init + updates (authority client only)
    /// - Apex bleed (server)
    /// - RecalculateStats hook applies Adapt thresholds + apex negative regen
    /// </summary>
    public sealed class ApexSurgeryController : MonoBehaviour
    {
        // ---------- static, per-process hook install ----------
        private static bool s_hooksInstalled;

        private static void EnsureHooksInstalled()
        {
            if (s_hooksInstalled) return;
            s_hooksInstalled = true;

            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;
        }

        private static void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            orig(self);
            if (self == null || self.master == null) return;

            var ctrl = self.master.GetComponent<ApexSurgeryController>();
            if (ctrl == null) return;

            // ----- Adapt bonuses per threshold -----
            int thresholds = ctrl.GetAdaptationThresholds();
            if (thresholds > 0)
            {
                float r = Mathf.Max(0f, Modules.Config.ApexAdaptReward.Value);
                float mult = 1f + thresholds * r;

                self.damage *= mult;
                // self.attackSpeed *= mult;
                self.moveSpeed *= mult;
                self.regen *= mult;

                self.armor += (mult - 1f) * 20f;
            }

            // ----- Apex negative regen -----
            if (self.HasBuff(Buffs.ApexSurgeryDebuff.buffIndex))
            {
                int apex = self.GetBuffCount(Buffs.ApexSurgeryDebuff);
                float hpDrainPerSecPerStack = Mathf.Max(0f, Modules.Config.ApexHPDrainPerStackPerSecond.Value);
                if (apex > 0 && hpDrainPerSecPerStack > 0f)
                {
                    self.regen -= hpDrainPerSecPerStack * apex;
                }
            }
        }

        // ---------- instance (master-persistent) ----------
        private CharacterMaster _master;
        private CharacterBody _body;
        private SkillLocator _skills;
        private ExtraSkillLocator _extraSkills;
        private InputBankTest _input;
        private ExtraInputBankTest _extraInput;


        // SyncVar replaced: server owns truth, client receives via ApexSyncAdaptationMessage
        private int _adaptationStacks;

        // ---------- HUD / UI (authority client only) ----------
        private bool _overlayDirty;
        private float _overlayTick;

        private GameObject _uiObj;
        private RectTransform _uiRoot;

        private Image _innerRingFill;
        private Image _outerRingFill;

        private HGTextMeshProUGUI _tierText;
        private HGTextMeshProUGUI _adaptText;

        private int _lastTier = int.MinValue;
        private int _lastWithin = int.MinValue;
        private int _lastPer = int.MinValue;
        private int _lastApex = int.MinValue;
        private int _lastCap = int.MinValue;

        // ---------- hold-to-reset (client-side) ----------
        private float _holdP1, _holdP2, _holdP3, _holdP4;
        private float _holdX1, _holdX2, _holdX3, _holdX4;

        private bool _firedP1, _firedP2, _firedP3, _firedP4;
        private bool _firedX1, _firedX2, _firedX3, _firedX4;
        private bool _wasP1, _wasP2, _wasP3, _wasP4, _wasX1, _wasX2, _wasX3, _wasX4;

        private float _holdResetGrace = 0.3f;

        // ---------- UI init state ----------
        private bool _uiInitialized;
        private bool _uiActivated;

        private readonly Vector2 _uiOffset = new Vector2(330f, 20f);
        private readonly Vector3 _uiScale = Vector3.one;

        // ---------- misc ----------
        private float _apexTick; // server-side bleed off
        public EnergySystem energySystem;

        // ---------- lifecycle ----------
        private void Awake()
        {
            EnsureHooksInstalled();

            _master = GetComponent<CharacterMaster>();
            //_netIdentity = GetComponent<NetworkIdentity>();

            CharacterBody.onBodyStartGlobal += OnBodyStartGlobal;

            var existing = _master ? _master.GetBody() : null;
            if (existing) AttachToBody(existing);
        }

        private void Start()
        {
            // Mirror old OnStartClient behavior: if we are a client with authority, init overlay lazily.
            if (NetworkClient.active && _body && _body.hasEffectiveAuthority)
            {
                TryInitOverlay();
            }
        }

        private void OnDestroy()
        {
            CharacterBody.onBodyStartGlobal -= OnBodyStartGlobal;
            DestroyOverlay();

            _body = null;
            _skills = null;
            _extraSkills = null;
            _input = null;
            _extraInput = null;
        }

        private void OnBodyStartGlobal(CharacterBody body)
        {
            if (body == null || _master == null || body.master != _master) return;
            AttachToBody(body);
        }

        private void AttachToBody(CharacterBody body)
        {
            _body = body;
            _skills = body.GetComponent<SkillLocator>();
            _extraSkills = body.GetComponent<ExtraSkillLocator>();
            _input = body.inputBank;
            _extraInput = body.GetComponent<ExtraInputBankTest>();

            _uiInitialized = false;
            _uiActivated = false;

            _holdResetGrace = 0.3f;

            energySystem = body.GetComponent<EnergySystem>();
        }

        // ---------- update loop (server bleed + client UI + hold-to-reset) ----------
        private float _dbgTick;
        private void Update()
        {
            //_dbgTick += Time.deltaTime;
            //bool dbg = _dbgTick >= 1f;
            //if (dbg) _dbgTick = 0f;

            //if (dbg)
            //    Debug.Log($"[ApexSC CLIENTCHK] clientActive={NetworkClient.active} serverActive={NetworkServer.active} body={(bool)_body} hasAuth={(_body ? _body.hasEffectiveAuthority : false)}");

            // === Apex bleed (server authoritative) ===
            if (_body != null)
            {
                _apexTick += Time.deltaTime;
                if (_apexTick >= 1f)
                {
                    _apexTick = 0f;
                    int buffcount = _body.GetBuffCount(Buffs.ApexSurgeryDebuff);
                    if (buffcount > 0)
                    {
                        _body.ApplyBuff(Buffs.ApexSurgeryDebuff.buffIndex, buffcount - 1);
                        _overlayDirty = true;
                    }
                }
            }

            // If body is gone/dead, bail
            if (_body == null || _body.healthComponent == null || !_body.healthComponent.alive)
            {
                //if (dbg) Debug.Log("[ApexSC CLIENTCHK] return: no body/health/dead");
                if (_uiObj) DestroyOverlay();
                return;
            }

            // Only local authority client should run UI + input checks
            if (!NetworkClient.active || !_body.hasEffectiveAuthority)
            {
                //if (dbg) Debug.Log("[ApexSC CLIENTCHK] return: not client active OR no effective authority");
                if (_uiObj) DestroyOverlay();
                return;
            }

            // Refresh references if needed (defensive)
            if (_skills == null) _skills = _body.GetComponent<SkillLocator>();
            if (_extraSkills == null) _extraSkills = _body.GetComponent<ExtraSkillLocator>();
            if (_input == null) _input = _body.inputBank;
            if (_extraInput == null) _extraInput = _body.GetComponent<ExtraInputBankTest>();

            // === UI init/activate ===
            TryInitOverlay();

            // === UI refresh tick ===
            _overlayTick += Time.deltaTime;
            if (_overlayTick >= 0.1f)
            {
                _overlayTick = 0f;

                int apexNow = _body.GetBuffCount(Buffs.ApexSurgeryDebuff);
                if (apexNow != _lastApex) _overlayDirty = true;

                if (_overlayDirty) UpdateOverlay();
            }

            // === Hold-to-reset ===
            float holdSeconds = Modules.Config.ApexHoldSecondsToReset.Value;

            if (_holdResetGrace > 0f)
            {
                _holdResetGrace -= Time.deltaTime;
                return;
            }

            // Base slots
            if (_skills != null && _input != null)
            {
                UpdateHoldToReset(_skills.primary, _input.skill1.down, ref _wasP1 ,ref _holdP1, ref _firedP1, 0, holdSeconds);
                UpdateHoldToReset(_skills.secondary, _input.skill2.down, ref _wasP2, ref _holdP2, ref _firedP2, 1, holdSeconds);
                UpdateHoldToReset(_skills.utility, _input.skill3.down, ref _wasP3, ref _holdP3, ref _firedP3, 2, holdSeconds);
                UpdateHoldToReset(_skills.special, _input.skill4.down, ref _wasP4, ref _holdP4, ref _firedP4, 3, holdSeconds);
            }

            // Extra slots
            if (_extraSkills != null && _extraInput != null)
            {
                UpdateHoldToReset(_extraSkills.extraFirst, _extraInput.extraSkill1.down, ref _wasX1, ref _holdX1, ref _firedX1, 4, holdSeconds);
                UpdateHoldToReset(_extraSkills.extraSecond, _extraInput.extraSkill2.down, ref _wasX2, ref _holdX2, ref _firedX2, 5, holdSeconds);
                UpdateHoldToReset(_extraSkills.extraThird, _extraInput.extraSkill3.down, ref _wasX3, ref _holdX3, ref _firedX3, 6, holdSeconds);
                UpdateHoldToReset(_extraSkills.extraFourth, _extraInput.extraSkill4.down, ref _wasX4, ref _holdX4, ref _firedX4, 7, holdSeconds);
            }
        }

        private void UpdateHoldToReset(GenericSkill slot, bool isDown, ref bool wasDown, ref float hold, ref bool firedThisHold, byte slotIndex, float holdSeconds)
        {
            bool justPressed = isDown && !wasDown;
            bool justReleased = !isDown && wasDown;
            wasDown = isDown;

            if (justReleased)
            {
                hold = 0f;
                firedThisHold = false;
                return;
            }

            // Only start counting hold after a fresh press
            if (!isDown || (!justPressed && hold <= 0f))
                return;

            if (slot == null || slot.isCooldownBlocked || slot.cooldownRemaining <= 0f)
            {
                hold = 0f;
                firedThisHold = false;
                return;
            }

            hold += Time.deltaTime;

            if (!firedThisHold && hold >= holdSeconds)
            {
                firedThisHold = true;
                if (NetworkServer.active)
                {
                    Debug.Log($"[Apex] HOST direct reset slot={slotIndex}");
                    TryAutoResetSlotByIndex_Server(slotIndex);
                }
                else
                {
                    var masterNetId = _master ? _master.netId : default;
                    Debug.Log($"[Apex] SEND reset req slot={slotIndex} masterNetId={masterNetId}");
                    new ApexResetSlotRequest(masterNetId, slotIndex).Send(NetworkDestination.Clients);
                }
            }
        }


        // ---------- stacks & thresholds ----------
        public int GetAdaptationStacks() => _adaptationStacks;

        public int GetAdaptationThresholds()
        {
            int per = Mathf.Max(1, Modules.Config.ApexAdaptThreshold.Value);
            return _adaptationStacks / per;
        }


        // Server authoritative add + broadcast
        private void AddAdaptationServer(int amount)
        {
            if (amount <= 0) return;

            int old = _adaptationStacks;
            _adaptationStacks += amount;
            _overlayDirty = true;

            // Broadcast to all clients; only the owning client applies (see message handler)
            if (_master)
            {
                new ApexSyncAdaptationMessage(_master.netId, _adaptationStacks)
                    .Send(NetworkDestination.Clients);

            }

            // Server stats dirty too (for server-side calculations / host)
            if (_body) _body.MarkAllStatsDirty();

        }


        private void AddApexStacksServer(int amount)
        {
            if (_body == null || amount <= 0) return;

            int before = _body.GetBuffCount(Buffs.ApexSurgeryDebuff);
            int after = before + amount;

            _body.ApplyBuff(Buffs.ApexSurgeryDebuff.buffIndex, after);
            _overlayDirty = true;

            int cap = ComputeOverdriveCap();
            if (cap > 0 && after > cap)
            {
                TriggerOverdriveServer();
                _body.ApplyBuff(Buffs.ApexSurgeryDebuff.buffIndex, 0);
            }
        }

        private int ComputeOverdriveCap()
        {
            int baseCap = Mathf.Max(0, Modules.Config.ApexOverdriveBaseCap.Value);
            int perCap = Mathf.Max(0, Modules.Config.ApexOverdriveCapPerAdapt.Value);
            int threshes = GetAdaptationThresholds();
            return baseCap + perCap * threshes;
        }

        private int ComputeStackSeconds(GenericSkill slot)
        {
            float seconds = slot.cooldownRemaining;
            int rounded = Mathf.RoundToInt(seconds);
            return Mathf.Max(0, rounded);
        }

        // Server-only mutation logic
        private void TryAutoResetSlotServer(GenericSkill slot)
        {


            if (slot == null) return;
            if (slot.isCooldownBlocked) return;

            // Only reset if they are actually "stuck": out of stock AND cooling down
            bool outOfStock = slot.stock <= 0;
            bool cooling = slot.cooldownRemaining > 0f;
            if (!(outOfStock && cooling)) return;

            int seconds = ComputeStackSeconds(slot);
            if (seconds <= 0) return;

            // Make skill usable now: grant 1 stock
            slot.AddOneStock();

            int apexPerSec = Mathf.Max(0, Modules.Config.ApexStacksPerSecondReset.Value);
            int adaptPerSec = Mathf.Max(0, Modules.Config.ApexAdaptPerSecondReset.Value);

            int addApex = seconds * apexPerSec;
            int addAdapt = seconds * adaptPerSec;

            if (addApex > 0) AddApexStacksServer(addApex);
            if (addAdapt > 0) AddAdaptationServer(addAdapt);
        }

        // Called by your network message handler (server-side)
        public void TryAutoResetSlotByIndex_Server(byte idx)
        {

            if (_body == null && _master != null)
            {
                var b = _master.GetBody();
                if (b) AttachToBody(b); // make AttachToBody internal/public or add a small internal helper
            }
            if (_body == null) return;

            var slot = ResolveSlotByIndex(idx);
            if (slot == null) return;
            

            TryAutoResetSlotServer(slot);
        }

        private GenericSkill ResolveSlotByIndex(byte idx)
        {
            if (_skills == null) _skills = _body?.GetComponent<SkillLocator>();
            if (_extraSkills == null) _extraSkills = _body?.GetComponent<ExtraSkillLocator>();
            if (_skills == null) return null;

            switch (idx)
            {
                case 0: return _skills.primary;
                case 1: return _skills.secondary;
                case 2: return _skills.utility;
                case 3: return _skills.special;
                case 4: return _extraSkills ? _extraSkills.extraFirst : null;
                case 5: return _extraSkills ? _extraSkills.extraSecond : null;
                case 6: return _extraSkills ? _extraSkills.extraThird : null;
                case 7: return _extraSkills ? _extraSkills.extraFourth : null;
                default: return null;
            }
        }

        // ---------- HUD overlay (EnergySystem-style lazy init) ----------
        private void TryInitOverlay()
        {
            if (_uiInitialized)
            {
                if (!_uiActivated && _uiObj)
                {
                    _uiObj.SetActive(true);
                    _uiActivated = true;
                }
                return;
            }

            if (_body == null || !_body.hasEffectiveAuthority) return;

            HUD hud = null;
            if (HUD.instancesList != null)
            {
                for (int i = 0; i < HUD.instancesList.Count; i++)
                {
                    var h = HUD.instancesList[i];
                    if (h && h.targetBodyObject == _body.gameObject)
                    {
                        hud = h;
                        break;
                    }
                }
            }
            if (!hud || !hud.healthBar || !hud.healthBar.barContainer) return;

            Transform parent = hud.healthBar.barContainer.transform;
            if (!parent) return;

            if (!_uiObj)
            {
                var prefab = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<GameObject>("apexAdaptUI");
                if (!prefab) return;

                _uiObj = Instantiate(prefab, parent, false);
                _uiObj.name = "ApexAdaptUI_Runtime";
                _uiObj.SetActive(false);
            }

            RectTransform moveRect = _uiObj.GetComponent<RectTransform>();
            if (!moveRect) moveRect = _uiObj.GetComponentInChildren<RectTransform>(true);
            if (!moveRect) return;

            moveRect.anchorMin = new Vector2(0.5f, 0.5f);
            moveRect.anchorMax = new Vector2(0.5f, 0.5f);
            moveRect.pivot = new Vector2(0.5f, 0.5f);
            moveRect.anchoredPosition = _uiOffset;
            moveRect.localScale = _uiScale;

            Transform rootTf = _uiObj.transform;
            var canvasTf = rootTf.Find("Canvas (Environment)");
            if (canvasTf != null)
            {
                var uiTf = canvasTf.Find("apexAdaptUI");
                if (uiTf != null) rootTf = uiTf;
            }
            else
            {
                var uiTf = rootTf.Find("apexAdaptUI");
                if (uiTf != null) rootTf = uiTf;
            }

            _uiRoot = rootTf.GetComponent<RectTransform>();
            if (_uiRoot == null) _uiRoot = rootTf.gameObject.AddComponent<RectTransform>();

            _innerRingFill = rootTf.Find("InnerRing")?.GetComponent<Image>();
            _outerRingFill = rootTf.Find("OuterRingFill")?.GetComponent<Image>();

            if (_innerRingFill == null || _outerRingFill == null)
            {
                if (rootTf.childCount >= 5)
                {
                    if (_innerRingFill == null) _innerRingFill = rootTf.GetChild(1).GetComponent<Image>();
                    if (_outerRingFill == null) _outerRingFill = rootTf.GetChild(4).GetComponent<Image>();
                }
            }

            _tierText = CreateLabel(rootTf, "TierText", "T0", new Vector2(0f, 0f), 24f, new Color(0.92f, 0.92f, 0.95f, 0.95f));
            _adaptText = CreateLabel(rootTf, "AdaptText", "0/0", new Vector2(0f, 60f), 16f, new Color(0.70f, 0.95f, 0.70f, 0.85f));

            _tierText.transform.SetAsLastSibling();
            _adaptText.transform.SetAsLastSibling();

            ResetOverlayCaches();
            _overlayDirty = true;

            _uiInitialized = true;

            _uiObj.SetActive(true);
            _uiActivated = true;

            UpdateOverlay();
        }

        private void ResetOverlayCaches()
        {
            _lastTier = int.MinValue;
            _lastWithin = int.MinValue;
            _lastPer = int.MinValue;
            _lastApex = int.MinValue;
            _lastCap = int.MinValue;
        }

        private HGTextMeshProUGUI CreateLabel(Transform parent, string name, string text, Vector2 position, float textScale, Color color)
        {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent, false);
            go.AddComponent<CanvasRenderer>();

            RectTransform rectTransform = go.AddComponent<RectTransform>();
            HGTextMeshProUGUI tmp = go.AddComponent<HGTextMeshProUGUI>();

            tmp.enabled = true;
            tmp.text = text;
            tmp.fontSize = textScale;
            tmp.color = color;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.enableWordWrapping = false;

            rectTransform.localPosition = Vector2.zero;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.localScale = Vector3.one;
            rectTransform.sizeDelta = Vector2.zero;
            rectTransform.anchoredPosition = position;

            return tmp;
        }

        private void UpdateOverlay()
        {
            _overlayDirty = false;

            if (!NetworkClient.active || _body == null || !_body.hasEffectiveAuthority)
            {
                DestroyOverlay();
                return;
            }

            if (_uiObj == null) return;
            if (_innerRingFill == null || _outerRingFill == null || _tierText == null) return;

            int adapt = _adaptationStacks;
            int per = Mathf.Max(1, Modules.Config.ApexAdaptThreshold.Value);
            int tier = adapt / per;
            int within = adapt % per;

            int cap = ComputeOverdriveCap();
            int apex = _body.GetBuffCount(Buffs.ApexSurgeryDebuff);

            if (tier != _lastTier)
            {
                _tierText.SetText($"T{tier}");
                _lastTier = tier;
            }

            if (_adaptText != null && (within != _lastWithin || per != _lastPer))
            {
                _adaptText.SetText($"{within}/{per}");
                _lastWithin = within;
                _lastPer = per;
            }

            _outerRingFill.fillAmount = within / (float)per;

            float apexFill = (cap > 0) ? Mathf.Clamp01(apex / (float)cap) : 0f;
            _innerRingFill.fillAmount = apexFill;

            Color safe = new Color(0.70f, 0.20f, 0.90f, 0.95f);
            Color warn = new Color(1.00f, 0.65f, 0.25f, 0.95f);
            Color crit = new Color(1.00f, 0.20f, 0.20f, 0.98f);

            if (apexFill < 0.65f) _innerRingFill.color = Color.Lerp(safe, warn, apexFill / 0.65f);
            else _innerRingFill.color = Color.Lerp(warn, crit, Mathf.InverseLerp(0.65f, 1f, apexFill));

            if (apexFill > 0.85f)
            {
                float t = Mathf.InverseLerp(0.85f, 1f, apexFill);
                float pulse = 0.6f + 0.4f * Mathf.Abs(Mathf.Sin(Time.unscaledTime * (8f + 10f * t)));
                var c = _innerRingFill.color;
                c.a = 0.75f + 0.25f * pulse;
                _innerRingFill.color = c;
            }

            var oc = _outerRingFill.color;
            oc.a = 0.75f;
            _outerRingFill.color = oc;

            _lastApex = apex;
            _lastCap = cap;
        }

        private void DestroyOverlay()
        {
            if (_uiObj != null)
            {
                try { Destroy(_uiObj); } catch { }
            }

            _uiObj = null;
            _uiRoot = null;

            _innerRingFill = null;
            _outerRingFill = null;

            _tierText = null;
            _adaptText = null;

            _uiInitialized = false;
            _uiActivated = false;

            ResetOverlayCaches();
        }

        // ---------- server-side overdrive ----------
        private void TriggerOverdriveServer()
        {
            if (_body == null) return;

            var hc = _body.healthComponent;
            if (hc == null) return;

            float chunkPct = Mathf.Clamp01(Modules.Config.ApexOverdriveChunk.Value);
            float dmg = chunkPct * hc.fullCombinedHealth;
            if (dmg > 0f)
            {
                var di = new DamageInfo
                {
                    attacker = _body.gameObject,
                    inflictor = _body.gameObject,
                    damage = dmg,
                    damageType = DamageType.BypassArmor | DamageType.Stun1s | DamageType.NonLethal,
                    damageColorIndex = DamageColorIndex.WeakPoint,
                    procCoefficient = 0f
                };
                hc.TakeDamage(di);
            }

            int healBlock = Mathf.Max(0, Modules.Config.ApexHealBlockDuration.Value);
            if (healBlock > 0)
                _body.ApplyBuff(RoR2Content.Buffs.HealingDisabled.buffIndex, 1, healBlock);

            new ForceQuirkOverdriveState(_master.netId).Send(NetworkDestination.Clients);

            // Replace TargetRpc: broadcast message; only owning client displays it.
            if (_master)
            {
                new ApexOverdriveNotifyMessage(_master.netId).Send(NetworkDestination.Clients);
            }
        }
        public void Client_ApplyAdaptation(int newValue)
        {
            int old = _adaptationStacks;
            _adaptationStacks = newValue;
            _overlayDirty = true;
            if (_body) _body.MarkAllStatsDirty();
        }


    }
}
