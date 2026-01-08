// ApexSurgeryController.cs
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
    /// - All clients (including the host's client) send ApexResetSlotRequest; ONLY the server mutates.
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

            // Recalc hook for regen penalties & adaptation bonuses
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
                float r = Mathf.Max(0f, Modules.Config.ApexAdaptReward.Value); // e.g. 0.1
                float mult = 1f + thresholds * r;

                self.damage *= mult;
                self.attackSpeed *= mult;
                self.moveSpeed *= mult;
                self.regen *= mult;
                self.armor *= mult;
            }

            // ----- Apex negative regen (buffs are networked) -----
            if (self.HasBuff(Buffs.ApexSurgeryDebuff.buffIndex))
            {
                int apex = self.GetBuffCount(Buffs.ApexSurgeryDebuff);
                float hpDrainPerSecPerStack = Mathf.Max(0f, Modules.Config.ApexHPDrainPerStackPerSecond.Value); // HP/sec per stack
                if (apex > 0 && hpDrainPerSecPerStack > 0f)
                {
                    float negRegen = hpDrainPerSecPerStack * apex;
                    self.regen -= negRegen;
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
        private Image _innerRingFill;   // Apex load (Radial180 bottom)
        private Image _outerRingFill;   // Adapt progress (Radial360)

        // Text
        private HGTextMeshProUGUI _tierText;   // Center: T#
        private HGTextMeshProUGUI _adaptText;  // Above: within/per (or stacks)

        // Cache for minimal updates
        private int _lastTier = int.MinValue;
        private int _lastWithin = int.MinValue;
        private int _lastPer = int.MinValue;
        private int _lastApex = int.MinValue;
        private int _lastCap = int.MinValue;


        // input edge armers (client-side)
        private bool _p1Armed, _p2Armed, _p3Armed, _p4Armed;
        private bool _x1Armed, _x2Armed, _x3Armed, _x4Armed;

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
            // On host, both server & client run; HUD must only spawn on the local-authority client.
            TryEnsureOverlay();
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

            // Prevent duplicate HUDs; only local-authority client creates one.
            TryEnsureOverlay();

        }

        // Only build overlay for the local player (authority) on the client.
        private void TryEnsureOverlay()
        {
            if (_body == null) return;

            // Only spawn UI when running as client AND this body has authority
            if (!NetworkClient.active || !_body.hasAuthority)
            {
                // If we previously had an overlay but lost authority, clean it up
                DestroyOverlay();
                return;
            }

            // Build once
            if (_uiObj == null)
            {
                RebuildOverlay();
                _overlayDirty = true;
            }
        }


        // ---------- update loop (input + overlay) ----------
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
                    }
                }
            }

            // If body is gone/dead, bail (and overlay will be destroyed by UpdateOverlay/TryEnsureOverlay)
            if (_body == null || _body.healthComponent == null || !_body.healthComponent.alive)
                return;

            if (_skills == null) return;

            // Ensure overlay exists when we have authority
            // (Optional but recommended to handle late body attach or respawns)
            TryEnsureOverlay();

            // --- UI refresh (authority client only) ---
            _overlayTick += Time.deltaTime;
            if (_overlayTick >= 0.1f) // 0.1f is fine; 0.25f also fine
            {
                _overlayTick = 0f;

                if (NetworkClient.active && _body.hasAuthority)
                {
                    // Buff stacks are not SyncVars; detect apex changes and mark dirty
                    int apexNow = _body.GetBuffCount(Buffs.ApexSurgeryDebuff);
                    if (apexNow != _lastApex) _overlayDirty = true;

                    if (_overlayDirty) UpdateOverlay();
                }
            }

            // Refresh references if needed
            if (_input == null) _input = _body.inputBank;
            if (_extraSkills == null) _extraSkills = _body.GetComponent<ExtraSkillLocator>();
            if (_extraInput == null) _extraInput = _body.GetComponent<ExtraInputBankTest>();

            // Only the local authoritative body should read inputs and send requests
            if (!_body.hasAuthority) return;

            // --------- YOUR INPUT BLOCK (unchanged) ---------
            HandleSkillInput(_input.skill1.justReleased, _input.skill1.justPressed, ref _p1Armed, _skills.primary);
            HandleSkillInput(_input.skill2.justReleased, _input.skill2.justPressed, ref _p2Armed, _skills.secondary);
            HandleSkillInput(_input.skill3.justReleased, _input.skill3.justPressed, ref _p3Armed, _skills.utility);
            HandleSkillInput(_input.skill4.justReleased, _input.skill4.justPressed, ref _p4Armed, _skills.special);

            HandleSkillInput(_extraInput.extraSkill1.justReleased, _extraInput.extraSkill1.justPressed, ref _x1Armed, _extraSkills.extraFirst);
            HandleSkillInput(_extraInput.extraSkill2.justReleased, _extraInput.extraSkill2.justPressed, ref _x2Armed, _extraSkills.extraSecond);
            HandleSkillInput(_extraInput.extraSkill3.justReleased, _extraInput.extraSkill3.justPressed, ref _x3Armed, _extraSkills.extraThird);
            HandleSkillInput(_extraInput.extraSkill4.justReleased, _extraInput.extraSkill4.justPressed, ref _x4Armed, _extraSkills.extraFourth);
            // --------- END INPUT BLOCK ---------
        }


        // NOTE: signature & usage unchanged — we ALWAYS send a message (host included).
        private void HandleSkillInput(bool justReleased, bool justPressed, ref bool armed, GenericSkill slot)
        {
            if (justReleased) armed = true;

            if (justPressed && armed)
            {
                armed = false;
                if (slot == null) return;

                if (hasAuthority)
                {
                    byte idx = ResolveSlotIndex(slot);
                    if (idx != 255)
                    {
                        // Host client will loopback to server; dedicated clients route to server.
                        new ShiggyMod.Modules.Networking.ApexResetSlotRequest(netId, idx)
                            .Send(NetworkDestination.Server);
                    }
                }
            }
        }

        // ---------- stacks & thresholds ----------
        public int GetAdaptationStacks() => _adaptationStacks;

        public int GetAdaptationThresholds()
        {
            int per = Mathf.Max(1, Modules.Config.ApexAdaptThreshold.Value);
            return per == 0 ? 0 : _adaptationStacks / per;
        }

        // SyncVar hook (runs on clients when server updates Adaptation)
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
                // clamp: remove excess stacks
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
            bool remainingOnly = Modules.Config.ApexScaleByRemainingOnly.Value;
            float seconds = remainingOnly ? slot.cooldownRemaining : slot.finalRechargeInterval;
            int rounded = Mathf.RoundToInt(seconds);
            return Mathf.Max(0, rounded);
        }

        // Server-only mutation logic. Only called from message handler path.
        [Server]
        private void TryAutoResetSlot(GenericSkill slot)
        {
            if (slot == null) return;

            // If blocked, do not reset (prevents bypassing special lockouts)
            if (slot.isCooldownBlocked) return;

            // Only reset if actually cooling down (your intended gate)
            if (slot.cooldownRemaining <= 0f) return;

            // Compute seconds traded into stacks
            int seconds = ComputeStackSeconds(slot);
            if (seconds <= 0) return;

            // --- Make skill usable NOW ---
            // Complete the current recharge cycle
            slot.rechargeStopwatch = slot.finalRechargeInterval;

            // Ensure at least one stock is available
            // (You can choose: add one stock, or clamp to 1 if you want deterministic)
            if (slot.stock < 1)
            {
                slot.stock = 1; // direct set is allowed by property
            }
            else if (slot.stock < slot.maxStock)
            {
                slot.AddOneStock();
            }

            // --- Apply Apex/Adapt costs ---
            int apexPerSec = Mathf.Max(0, Modules.Config.ApexStacksPerSecondReset.Value);
            int adaptPerSec = Mathf.Max(0, Modules.Config.ApexAdaptPerSecondReset.Value);

            int addApex = seconds * apexPerSec;
            int addAdapt = seconds * adaptPerSec;

            if (addApex > 0) AddApexStacksServer(addApex);
            if (addAdapt > 0) AddAdaptationServer(addAdapt);
        }
        //private void TryAutoResetSlot(GenericSkill slot)
        //{
        //    if (slot == null) return;

        //    bool outOfStock = slot.stock <= 0;
        //    bool cooling = slot.cooldownRemaining > 0f;
        //    if (!(outOfStock && cooling)) return;

        //    // Compute seconds to trade into stacks
        //    int seconds = ComputeStackSeconds(slot);
        //    if (seconds <= 0) return;

        //    // Reset by granting one stock (respect maxStock)
        //    if (slot.stock < slot.maxStock) slot.AddOneStock();

        //    // Stack math
        //    int apexPerSec = Mathf.Max(0, Modules.Config.ApexStacksPerSecondReset.Value);
        //    int adaptPerSec = Mathf.Max(0, Modules.Config.ApexAdaptPerSecondReset.Value);

        //    int addApex = seconds * apexPerSec;
        //    int addAdapt = seconds * adaptPerSec;

        //    if (addApex > 0) AddApexStacksServer(addApex);
        //    if (addAdapt > 0) AddAdaptationServer(addAdapt);
        //}

        // ---------- client->server entry from message ----------
        [Server]
        public void TryAutoResetSlotByIndex(byte idx)
        {
            if (_body == null) return;

            var slot = ResolveSlotByIndex(idx);
            if (slot == null) return;

            TryAutoResetSlot(slot);
        }

        // ---------- slot index mapping ----------
        private byte ResolveSlotIndex(GenericSkill slot)
        {
            if (_skills == null) _skills = _body?.GetComponent<SkillLocator>();
            if (_extraSkills == null) _extraSkills = _body?.GetComponent<ExtraSkillLocator>();
            if (_skills == null) return 255;

            if (slot == _skills.primary)   return 0;
            if (slot == _skills.secondary) return 1;
            if (slot == _skills.utility)   return 2;
            if (slot == _skills.special)   return 3;

            if (_extraSkills != null)
            {
                if (slot == _extraSkills.extraFirst)  return 4;
                if (slot == _extraSkills.extraSecond) return 5;
                if (slot == _extraSkills.extraThird)  return 6;
                if (slot == _extraSkills.extraFourth) return 7;
            }
            return 255; // invalid
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
                case 4: return _extraSkills ? _extraSkills.extraFirst  : null;
                case 5: return _extraSkills ? _extraSkills.extraSecond : null;
                case 6: return _extraSkills ? _extraSkills.extraThird  : null;
                case 7: return _extraSkills ? _extraSkills.extraFourth : null;
                default: return null;
            }
        }

        // ---------- HUD overlay (authority client only) ----------
        private void RebuildOverlay()
        {
            DestroyOverlay();

            bool show = true;
            try { show = Modules.Config.ApexShowAdaptationOverlay == null || Modules.Config.ApexShowAdaptationOverlay.Value; }
            catch { show = true; }

            if (!show || _body == null) return;
            if (!NetworkClient.active || !_body.hasAuthority) return;

            // Instantiate prefab (asset bundle)
            _uiObj = UnityEngine.Object.Instantiate(
                Modules.ShiggyAsset.mainAssetBundle.LoadAsset<GameObject>("apexAdaptUI")
            );
            _uiObj.name = "ApexAdaptUI_Runtime";
            _uiObj.SetActive(true);

            // OPTIONAL (recommended later): parent to RoR2 HUD container
            // Only enable when you confirm which container exists in your RoR2 version.
            /*
            var hud = HUD.instancesList != null
                ? HUD.instancesList.Find(h => h && h.targetBodyObject == _body.gameObject)
                : null;

            if (hud != null && hud.mainContainer != null)
                _uiObj.transform.SetParent(hud.mainContainer.transform, false);
            */

            // Resolve the inner root: Canvas (Environment) -> apexAdaptUI
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

            // Bind fills (prefer name-based, fallback to your screenshot order)
            _innerRingFill = rootTf.Find("InnerRing")?.GetComponent<Image>();
            _outerRingFill = rootTf.Find("OuterRingFill")?.GetComponent<Image>();

            if (_innerRingFill == null || _outerRingFill == null)
            {
                // Fallback to index mapping:
                // 0 InnerRingBackground
                // 1 InnerRing
                // 2 OuterRingBackground
                // 3 OuterRing
                // 4 OuterRingFill
                if (rootTf.childCount >= 5)
                {
                    if (_innerRingFill == null) _innerRingFill = rootTf.GetChild(1).GetComponent<Image>();
                    if (_outerRingFill == null) _outerRingFill = rootTf.GetChild(4).GetComponent<Image>();
                }
            }

            // Create texts (code-created like your EnergySystem)
            _tierText = CreateLabel(rootTf, "TierText", "T0", new Vector2(0f, 0f), 24f, new Color(0.92f, 0.92f, 0.95f, 0.95f));
            _adaptText = CreateLabel(rootTf, "AdaptText", "0/0", new Vector2(0f, 60f), 16f, new Color(0.70f, 0.95f, 0.70f, 0.85f));

            // Ensure text draws above rings
            _tierText.transform.SetAsLastSibling();
            _adaptText.transform.SetAsLastSibling();

            ResetOverlayCaches();
            _overlayDirty = true;
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
            go.transform.parent = parent;
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

            // Outer ring fill (adapt progress)
            _outerRingFill.fillAmount = within / (float)per;

            // Inner ring fill (apex danger)
            float apexFill = (cap > 0) ? Mathf.Clamp01(apex / (float)cap) : 0f;
            _innerRingFill.fillAmount = apexFill;

            // Inner ring color ramp
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

            // Keep outer ring slightly subdued (prevents green dominance)
            var oc = _outerRingFill.color;
            oc.a = 0.75f;
            _outerRingFill.color = oc;

            // Update cache values used for dirty detection
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

            ResetOverlayCaches();
        }



        // ---------- server-side overdrive ----------
        [Server]
        private void TriggerOverdriveServer()
        {
            if (_body == null) return;

            var hc = _body.healthComponent;
            if (hc == null) return;

            float chunkPct = Mathf.Clamp01(Modules.Config.ApexOverdriveChunk.Value); // e.g. 0.40
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

