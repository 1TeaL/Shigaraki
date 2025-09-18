// ApexSurgeryController.cs
using System;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using ExtraSkillSlots;

using R2API.Networking;
using R2API.Networking.Interfaces;

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

        // HUD overlay
        private Canvas _hudCanvas;
        private CanvasGroup _hudCg;
        private Text _adaptText;
        private RectTransform _panelRT;
        private bool _overlayDirty;
        private float _overlayTick;

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

            // Only spawn HUD when running as client AND this body has authority
            if (!NetworkClient.active || !_body.hasAuthority)
            {
                // If we previously had an overlay but lost authority, clean it up
                DestroyOverlay();
                return;
            }

            // Build once
            if (_hudCanvas == null)
            {
                RebuildOverlay();
                _overlayDirty = true;
            }
        }

        // ---------- update loop (input + overlay) ----------
        private void Update()
        {
            // Overlay refresh @ ~4 Hz even on clients
            _overlayTick += Time.deltaTime;
            if (_overlayTick >= 0.25f)
            {
                _overlayTick = 0f;
                if (_overlayDirty) UpdateOverlay();
            }

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
            if (_body == null || _body.healthComponent == null || !_body.healthComponent.alive)
                return;
            if (_body == null || _skills == null) return;

            // Refresh references if needed
            if (_input == null) _input = _body.inputBank;
            if (_extraSkills == null) _extraSkills = _body.GetComponent<ExtraSkillLocator>();
            if (_extraInput == null) _extraInput = _body.GetComponent<ExtraInputBankTest>();

            // Only the local authoritative body should read inputs and send requests
            if (!_body.hasAuthority) return;

            // --------- YOUR INPUT BLOCK (unchanged) ---------
            // current down states
            bool p1 = _input != null && _input.skill1.down;
            bool p2 = _input != null && _input.skill2.down;
            bool p3 = _input != null && _input.skill3.down;
            bool p4 = _input != null && _input.skill4.down;

            bool x1 = _extraInput != null && _extraInput.extraSkill1.down;
            bool x2 = _extraInput != null && _extraInput.extraSkill2.down;
            bool x3 = _extraInput != null && _extraInput.extraSkill3.down;
            bool x4 = _extraInput != null && _extraInput.extraSkill4.down;

            // rising-edges
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

            bool outOfStock = slot.stock <= 0;
            bool cooling = slot.cooldownRemaining > 0f;
            if (!(outOfStock && cooling)) return;

            // Compute seconds to trade into stacks
            int seconds = ComputeStackSeconds(slot);
            if (seconds <= 0) return;

            // Reset by granting one stock (respect maxStock)
            if (slot.stock < slot.maxStock) slot.AddOneStock();

            // Stack math
            int apexPerSec = Mathf.Max(0, Modules.Config.ApexStacksPerSecondReset.Value);
            int adaptPerSec = Mathf.Max(0, Modules.Config.ApexAdaptPerSecondReset.Value);

            int addApex = seconds * apexPerSec;
            int addAdapt = seconds * adaptPerSec;

            if (addApex > 0) AddApexStacksServer(addApex);
            if (addAdapt > 0) AddAdaptationServer(addAdapt);
        }

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

            // Safety: Only on the local-authority client
            if (!NetworkClient.active || !_body.hasAuthority) return;

            var root = new GameObject("Apex_AdaptationHUD");
            _hudCanvas = root.AddComponent<Canvas>();
            _hudCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _hudCanvas.sortingOrder = 5001;

            var scaler = root.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);

            root.AddComponent<GraphicRaycaster>();
            _hudCg = root.AddComponent<CanvasGroup>();
            _hudCg.interactable = false;
            _hudCg.blocksRaycasts = false;
            _hudCg.alpha = 1f;

            // Transparent panel anchored bottom-center, offset left
            var panel = new GameObject("Panel").AddComponent<Image>();
            panel.color = new Color(0f, 0f, 0f, 0f);
            panel.raycastTarget = false;
            panel.transform.SetParent(_hudCanvas.transform, false);

            _panelRT = panel.rectTransform;
            _panelRT.sizeDelta = new Vector2(320f, 42f);
            _panelRT.anchorMin = _panelRT.anchorMax = new Vector2(0.5f, 0f); // bottom-center
            _panelRT.pivot = new Vector2(0.5f, 0f);
            _panelRT.anchoredPosition = new Vector2(-240f, 80f);

            // Text
            var textGO = new GameObject("AdaptText");
            textGO.transform.SetParent(panel.transform, false);
            var r = textGO.AddComponent<RectTransform>();
            r.anchorMin = Vector2.zero;
            r.anchorMax = Vector2.one;
            r.offsetMin = Vector2.zero;
            r.offsetMax = Vector2.zero;

            _adaptText = textGO.AddComponent<Text>();
            _adaptText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            _adaptText.alignment = TextAnchor.MiddleCenter;
            _adaptText.resizeTextForBestFit = false;
            _adaptText.resizeTextMinSize = 14;
            _adaptText.resizeTextMaxSize = 32;
            _adaptText.color = new Color(0.9f, 0.95f, 1f, 0.95f);
            _adaptText.horizontalOverflow = HorizontalWrapMode.Overflow;  // never wrap
            _adaptText.verticalOverflow = VerticalWrapMode.Truncate;    // single-line height


            UpdateOverlay();
        }

        private void UpdateOverlay()
        {
            _overlayDirty = false;

            // Only on authority client; if we somehow lost authority, destroy it.
            if (!NetworkClient.active || _body == null || !_body.hasAuthority)
            {
                DestroyOverlay();
                return;
            }

            if (_adaptText == null)
            {
                if (_hudCanvas == null && _body != null) RebuildOverlay();
                return;
            }

            int adapt = _adaptationStacks;
            int thr = GetAdaptationThresholds();
            int cap = ComputeOverdriveCap();
            int apex = _body ? _body.GetBuffCount(Buffs.ApexSurgeryDebuff) : 0;

            _adaptText.text = $"Adaptation: {adapt}  ({thr}x)  |  Apex Limit: {apex}/{(cap > 0 ? cap : 0)}";

            float warn = (cap > 0) ? Mathf.Clamp01(apex / Mathf.Max(1f, (float)cap)) : 0f;
            _adaptText.color = Color.Lerp(new Color(0.9f, 0.95f, 1f, 0.95f),
                                          new Color(1f, 0.8f, 0.3f, 0.95f), warn);
        }

        private void DestroyOverlay()
        {
            if (_hudCanvas != null)
            {
                try { UnityEngine.Object.Destroy(_hudCanvas.gameObject); } catch { }
            }
            _hudCanvas = null;
            _hudCg = null;
            _adaptText = null;
            _panelRT = null;
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
        }
    }
}

