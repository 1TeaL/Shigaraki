using ExtraSkillSlots;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Skills;
using RoR2.UI;
using ShiggyMod.Modules.Networking;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace ShiggyMod.Modules.Quirks
{
    /// <summary>
    /// Master-persistent controller for Apex Surgery / Adaptation (multiplayer-safe).
    /// - Clients send ApexResetSlotRequest; ONLY the server mutates.
    /// - Adaptation is SyncVar to clients (for HUD + local RecalculateStats).
    /// - Apex stacks are networked via buff stacks.
    /// - HUD overlay renders once on the local-authority client; protected against duplicates.
    /// </summary>
    public sealed class ApexSurgeryController : NetworkBehaviour
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
                // self.attackSpeed *= mult; // (left disabled per your latest paste)
                self.moveSpeed *= mult;
                self.regen *= mult;

                // If you want armor to scale in chunky increments rather than multiplicative
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

        // Only Adaptation needs syncing; Apex is a buff stack (already networked)
        [SyncVar(hook = nameof(OnAdaptationSync))]
        private int _adaptationStacks;

        // ---------- HUD / UI (authority client only) ----------
        private bool _overlayDirty;
        private float _overlayTick;

        // Prefab instance
        private GameObject _uiObj;
        private RectTransform _uiRoot;

        // Ring fills
        private Image _innerRingFill;   // Apex load
        private Image _outerRingFill;   // Adapt progress

        // Text
        private HGTextMeshProUGUI _tierText;   // Center: T#
        private HGTextMeshProUGUI _adaptText;  // Above: within/per

        // Cache for minimal updates
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

        // Optional small grace after attach/respawn so you don't instantly fire on spawn
        private float _holdResetGrace = 0.3f;

        // ---------- UI init state (EnergySystem-style) ----------
        private bool _uiInitialized;
        private bool _uiActivated;

        // Position tweaks (offset from the barContainer anchor center)
        // +X = right, +Y = up, -Y = down
        private readonly Vector2 _uiOffset = new Vector2(330f, 20f);
        private readonly Vector3 _uiScale = Vector3.one;

        // ---------- misc ----------
        private float _apexTick; // server-side bleed off

        // ---------- lifecycle ----------
        private void Awake()
        {
            EnsureHooksInstalled();

            _master = GetComponent<CharacterMaster>();
            CharacterBody.onBodyStartGlobal += OnBodyStartGlobal;

            // Attach now if body is already spawned
            var existing = _master ? _master.GetBody() : null;
            if (existing) AttachToBody(existing);
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            // host has both server and client; UI should only appear on the local-authority client.
            if (NetworkClient.active && _body && _body.hasAuthority)
            {
                TryInitOverlay(); // EnergySystem-style lazy init
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

            // Reset per-body UI state so it can re-init after respawn
            _uiInitialized = false;
            _uiActivated = false;

            // Add a small grace to avoid immediate hold firing during attach
            _holdResetGrace = 0.3f;
        }

        // ---------- update loop (server bleed + client UI + hold-to-reset) ----------
        private void Update()
        {
            // === Apex bleed (server authoritative) ===
            if (NetworkServer.active && _body != null)
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

            // If body is gone/dead, bail (UI will be destroyed by authority check below)
            if (_body == null || _body.healthComponent == null || !_body.healthComponent.alive)
            {
                if (_uiObj) DestroyOverlay();
                return;
            }

            // Refresh references if needed (defensive)
            if (_skills == null) _skills = _body.GetComponent<SkillLocator>();
            if (_extraSkills == null) _extraSkills = _body.GetComponent<ExtraSkillLocator>();
            if (_input == null) _input = _body.inputBank;
            if (_extraInput == null) _extraInput = _body.GetComponent<ExtraInputBankTest>();

            // Only local authority client should run UI + input checks
            if (!NetworkClient.active || !_body.hasAuthority)
            {
                if (_uiObj) DestroyOverlay();
                return;
            }

            // === EnergySystem-style UI init/activate ===
            TryInitOverlay();

            // === UI refresh tick ===
            _overlayTick += Time.deltaTime;
            if (_overlayTick >= 0.1f)
            {
                _overlayTick = 0f;

                // Buff stacks are not SyncVars; detect apex changes and mark dirty
                int apexNow = _body.GetBuffCount(Buffs.ApexSurgeryDebuff);
                if (apexNow != _lastApex) _overlayDirty = true;

                if (_overlayDirty) UpdateOverlay();
            }

            // === Hold-to-reset ===
            float holdSeconds = Mathf.Max(0.05f, Modules.Config.ApexHoldSecondsToReset.Value);

            if (_holdResetGrace > 0f)
            {
                _holdResetGrace -= Time.deltaTime;
                return;
            }

            // Base slots
            if (_skills != null && _input != null)
            {
                UpdateHoldToReset(_skills.primary, _input.skill1.down, ref _holdP1, ref _firedP1, 0, holdSeconds);
                UpdateHoldToReset(_skills.secondary, _input.skill2.down, ref _holdP2, ref _firedP2, 1, holdSeconds);
                UpdateHoldToReset(_skills.utility, _input.skill3.down, ref _holdP3, ref _firedP3, 2, holdSeconds);
                UpdateHoldToReset(_skills.special, _input.skill4.down, ref _holdP4, ref _firedP4, 3, holdSeconds);
            }

            // Extra slots
            if (_extraSkills != null && _extraInput != null)
            {
                UpdateHoldToReset(_extraSkills.extraFirst, _extraInput.extraSkill1.down, ref _holdX1, ref _firedX1, 4, holdSeconds);
                UpdateHoldToReset(_extraSkills.extraSecond, _extraInput.extraSkill2.down, ref _holdX2, ref _firedX2, 5, holdSeconds);
                UpdateHoldToReset(_extraSkills.extraThird, _extraInput.extraSkill3.down, ref _holdX3, ref _firedX3, 6, holdSeconds);
                UpdateHoldToReset(_extraSkills.extraFourth, _extraInput.extraSkill4.down, ref _holdX4, ref _firedX4, 7, holdSeconds);
            }
        }

        private void UpdateHoldToReset(GenericSkill slot, bool isDown, ref float hold, ref bool firedThisHold, byte slotIndex, float holdSeconds)
        {
            // Release resets state
            if (!isDown)
            {
                hold = 0f;
                firedThisHold = false;
                return;
            }

            // Must have a valid slot and be eligible (cooling down + not blocked)
            if (slot == null || slot.isCooldownBlocked || slot.cooldownRemaining <= 0f)
            {
                hold = 0f;
                firedThisHold = false;
                return;
            }

            // Accumulate hold time
            hold += Time.deltaTime;

            // Fire once per hold
            if (!firedThisHold && hold >= holdSeconds)
            {
                firedThisHold = true;

                new ApexResetSlotRequest(netId, slotIndex)
                    .Send(NetworkDestination.Server);

                _overlayDirty = true;
            }
        }

        // ---------- stacks & thresholds ----------
        public int GetAdaptationStacks() => _adaptationStacks;

        public int GetAdaptationThresholds()
        {
            int per = Mathf.Max(1, Modules.Config.ApexAdaptThreshold.Value);
            return _adaptationStacks / per;
        }

        private void OnAdaptationSync(int _old, int _new)
        {
            _overlayDirty = true;
        }

        [Server]
        private void AddAdaptationServer(int amount)
        {
            if (amount <= 0) return;
            _adaptationStacks += amount; // SyncVar; clients update via hook
            _overlayDirty = true;
        }

        [Server]
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

        // Server-only mutation logic. Only called from message handler path.
        [Server]
        private void TryAutoResetSlot(GenericSkill slot)
        {
            if (slot == null) return;
            if (slot.isCooldownBlocked) return;

            // Only reset if they are actually "stuck": out of stock AND cooling down
            bool outOfStock = slot.stock <= 0;
            bool cooling = slot.cooldownRemaining > 0f;
            if (!(outOfStock && cooling)) return;

            int seconds = ComputeStackSeconds(slot);
            if (seconds <= 0) return;

            // Make skill usable now: grant 1 stock. Do NOT mess with rechargeStopwatch here.
            slot.stock = 1;

            int apexPerSec = Mathf.Max(0, Modules.Config.ApexStacksPerSecondReset.Value);
            int adaptPerSec = Mathf.Max(0, Modules.Config.ApexAdaptPerSecondReset.Value);

            int addApex = seconds * apexPerSec;
            int addAdapt = seconds * adaptPerSec;

            if (addApex > 0) AddApexStacksServer(addApex);
            if (addAdapt > 0) AddAdaptationServer(addAdapt);
        }

        // Called by your network message handler (server-side)
        [Server]
        public void TryAutoResetSlotByIndex(byte idx)
        {
            if (_body == null) return;
            var slot = ResolveSlotByIndex(idx);
            if (slot == null) return;

            TryAutoResetSlot(slot);
        }

        [Server]
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

            if (_body == null || !_body.hasAuthority) return;

            // Find HUD for this body
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

            // Instantiate once (inactive first, like your EnergySystem)
            if (!_uiObj)
            {
                var prefab = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<GameObject>("apexAdaptUI");
                if (!prefab) return;

                _uiObj = UnityEngine.Object.Instantiate(prefab, parent, false);
                _uiObj.name = "ApexAdaptUI_Runtime";
                _uiObj.SetActive(false);
            }

            // Move the instantiated root rect (more reliable than moving nested roots)
            RectTransform moveRect = _uiObj.GetComponent<RectTransform>();
            if (!moveRect) moveRect = _uiObj.GetComponentInChildren<RectTransform>(true);
            if (!moveRect) return;

            moveRect.anchorMin = new Vector2(0.5f, 0.5f);
            moveRect.anchorMax = new Vector2(0.5f, 0.5f);
            moveRect.pivot = new Vector2(0.5f, 0.5f);
            moveRect.anchoredPosition = _uiOffset;
            moveRect.localScale = _uiScale;

            // Resolve binding root: Canvas (Environment) -> apexAdaptUI
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

            if (!NetworkClient.active || _body == null || !_body.hasAuthority)
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

            // Text updates only when changed
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

            // Color ramp
            Color safe = new Color(0.70f, 0.20f, 0.90f, 0.95f);
            Color warn = new Color(1.00f, 0.65f, 0.25f, 0.95f);
            Color crit = new Color(1.00f, 0.20f, 0.20f, 0.98f);

            if (apexFill < 0.65f) _innerRingFill.color = Color.Lerp(safe, warn, apexFill / 0.65f);
            else _innerRingFill.color = Color.Lerp(warn, crit, Mathf.InverseLerp(0.65f, 1f, apexFill));

            // Pulse near cap
            if (apexFill > 0.85f)
            {
                float t = Mathf.InverseLerp(0.85f, 1f, apexFill);
                float pulse = 0.6f + 0.4f * Mathf.Abs(Mathf.Sin(Time.unscaledTime * (8f + 10f * t)));
                var c = _innerRingFill.color;
                c.a = 0.75f + 0.25f * pulse;
                _innerRingFill.color = c;
            }

            // Keep outer ring subdued
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
                try { UnityEngine.Object.Destroy(_uiObj); } catch { }
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
        [Server]
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

            Chat.AddMessage("<style=cDeath>Quirk Overdrive!</style> Healing blocked and backlash applied.");
            new ForceQuirkOverdriveState(_body.masterObjectId).Send(NetworkDestination.Clients);
        }
    }
}
