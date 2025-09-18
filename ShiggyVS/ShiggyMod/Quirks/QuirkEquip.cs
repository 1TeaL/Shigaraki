// QuirkEquip.cs
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using ExtraSkillSlots;

using R2API.Networking;
using R2API.Networking.Interfaces;

using RoR2;
using RoR2.Skills;

using UnityEngine;
using UnityEngine.Networking;

using ShiggyMod.Modules.Survivors;
using ShiggyMod.Modules.Networking; // EquipLoadoutRequest
using static ShiggyMod.Modules.Quirks.QuirkRegistry;

namespace ShiggyMod.Modules.Quirks
{
    // Built by UI; sent to server.
    public struct SelectedQuirkLoadout
    {
        public QuirkId Primary;
        public QuirkId Secondary;
        public QuirkId Utility;
        public QuirkId Special;
        public QuirkId Extra1;
        public QuirkId Extra2;
        public QuirkId Extra3;
        public QuirkId Extra4;

        // Optional: dedicated passive toggles (checkboxes). If null/empty, derive from slots.
        public List<QuirkId> PassiveToggles;
    }

    public static class QuirkEquip
    {
        // Track last overrides per body so we can unset cleanly
        private struct SlotOverrides
        {
            public SkillDef Primary, Secondary, Utility, Special;
            public SkillDef Extra1, Extra2, Extra3, Extra4;
        }
        private class SlotOverridesHolder { public SlotOverrides Last; }

        private static readonly ConditionalWeakTable<CharacterBody, SlotOverridesHolder> _overrideCache
            = new ConditionalWeakTable<CharacterBody, SlotOverridesHolder>();

        // ========================= CLIENT ENTRY =========================

        /// <summary>
        /// UI entry-point (client/host). Sends the equip request to the server.
        /// </summary>
        public static void RequestApplyFromClient(CharacterBody body, in SelectedQuirkLoadout loadout)
        {
            if (!body || !body.master) return;

            // Don’t gate on body.hasAuthority; player bodies usually don’t have authority.
            // Anyone with the UI open can send to the server.
            new EquipLoadoutRequest(body.master.netId, loadout)
                .Send(NetworkDestination.Server);
        }

        // ========================= SERVER SIDE =========================

        /// <summary>
        /// Server-authoritative apply; called from EquipLoadoutRequest.OnReceived when a body exists.
        /// - Sets/unsets slot overrides
        /// - Persists overrides via ShiggyMasterController.writeToSkillList(sd, idx) for idx=0..7
        /// - Applies passives exactly once
        /// </summary>
        [Server]
        internal static void ApplyServer(CharacterBody body, ExtraSkillLocator extras, in SelectedQuirkLoadout loadout)
        {
            if (!NetworkServer.active) return;
            if (!body || body.skillLocator == null) return;

            var sl = body.skillLocator;
            if (extras == null) extras = body.GetComponent<ExtraSkillLocator>();

            var masterCtrl = body.master ? body.master.GetComponent<ShiggyMasterController>()
                                         : body.GetComponent<ShiggyMasterController>();

            var holder = _overrideCache.GetValue(body, _ => new SlotOverridesHolder());
            var last = holder.Last;

            void Persist(int idx, SkillDef sd)
            {
                if (masterCtrl != null) masterCtrl.writeToSkillList(sd, idx);
            }

            void EquipActiveSlot(GenericSkill slot, ref SkillDef lastOverride, QuirkId qid, int persistIndex)
            {
                if (!slot)
                {
                    Persist(persistIndex, null);
                    return;
                }

                // Unset previous
                if (lastOverride)
                {
                    slot.UnsetSkillOverride(slot, lastOverride, GenericSkill.SkillOverridePriority.Contextual);
                    lastOverride = null;
                }

                // Resolve & set new
                SkillDef sd = ResolveSkill(qid);
                if (sd != null)
                {
                    slot.SetSkillOverride(slot, sd, GenericSkill.SkillOverridePriority.Contextual);
                    lastOverride = sd;
                }

                // Persist 0..7
                Persist(persistIndex, sd);
            }

            // Core 4
            EquipActiveSlot(sl.primary, ref last.Primary, loadout.Primary, 0);
            EquipActiveSlot(sl.secondary, ref last.Secondary, loadout.Secondary, 1);
            EquipActiveSlot(sl.utility, ref last.Utility, loadout.Utility, 2);
            EquipActiveSlot(sl.special, ref last.Special, loadout.Special, 3);

            // Extras 4
            if (extras)
            {
                EquipActiveSlot(extras.extraFirst, ref last.Extra1, loadout.Extra1, 4);
                EquipActiveSlot(extras.extraSecond, ref last.Extra2, loadout.Extra2, 5);
                EquipActiveSlot(extras.extraThird, ref last.Extra3, loadout.Extra3, 6);
                EquipActiveSlot(extras.extraFourth, ref last.Extra4, loadout.Extra4, 7);
            }

            holder.Last = last;

            // Passives: clear managed → apply selected once (idempotent)
            ApplyPassivesOnceServer(body, loadout);

            // Recalc on server; clients will replicate
            body.RecalculateStats();
        }

        /// <summary>
        /// Clear all managed passive buffs, then apply the new selection exactly once.
        /// </summary>
        [Server]
        private static void ApplyPassivesOnceServer(CharacterBody body, in SelectedQuirkLoadout loadout)
        {
            if (!body) return;

            // Clear all registry-defined passives
            foreach (var rec in QuirkRegistry.All.Values)
            {
                if (rec.Category == QuirkCategory.Passive && rec.Buff)
                    body.ApplyBuff(rec.Buff.buffIndex, 0);
            }

            // Build desired passive set
            var target = new HashSet<QuirkId>();
            if (loadout.PassiveToggles != null && loadout.PassiveToggles.Count > 0)
            {
                foreach (var q in loadout.PassiveToggles) target.Add(q);
            }
            else
            {
                void TryAddIfPassive(QuirkId id)
                {
                    if (id == QuirkId.None) return;
                    if (QuirkRegistry.TryGet(id, out var rec) && rec.Category == QuirkCategory.Passive && rec.Buff)
                        target.Add(id);
                }

                TryAddIfPassive(loadout.Primary);
                TryAddIfPassive(loadout.Secondary);
                TryAddIfPassive(loadout.Utility);
                TryAddIfPassive(loadout.Special);
                TryAddIfPassive(loadout.Extra1);
                TryAddIfPassive(loadout.Extra2);
                TryAddIfPassive(loadout.Extra3);
                TryAddIfPassive(loadout.Extra4);
            }

            // Apply exactly once
            foreach (var q in target)
            {
                if (q == QuirkId.None) continue;
                if (QuirkRegistry.TryGet(q, out var rec) && rec.Category == QuirkCategory.Passive && rec.Buff)
                    body.ApplyBuff(rec.Buff.buffIndex, 1);
            }
        }

        private static SkillDef ResolveSkill(QuirkId id)
        {
            if (id == QuirkId.None) return null;

            if (QuirkRegistry.TryGet(id, out var r) && r.Skill != null)
                return r.Skill;

            // Fallbacks for base four
            switch (id)
            {
                case QuirkId.Shiggy_DecayActive: return Survivors.Shiggy.decayDef;
                case QuirkId.Shiggy_AirCannonActive: return Survivors.Shiggy.aircannonDef;
                case QuirkId.Shiggy_BulletLaserActive: return Survivors.Shiggy.bulletlaserDef;
                case QuirkId.Shiggy_MultiplierActive: return Survivors.Shiggy.multiplierDef;
                default: return null;
            }
        }

        /// <summary>
        /// Unset previously applied overrides (server-only).
        /// </summary>
        [Server]
        public static void Clear(CharacterBody body, ExtraSkillLocator extras)
        {
            if (!NetworkServer.active) return;
            if (!body || body.skillLocator == null) return;

            var sl = body.skillLocator;
            if (!_overrideCache.TryGetValue(body, out var holder)) return;

            void Unset(GenericSkill slot, ref SkillDef lastOverride)
            {
                if (!slot || !lastOverride) return;
                slot.UnsetSkillOverride(slot, lastOverride, GenericSkill.SkillOverridePriority.Contextual);
                lastOverride = null;
            }

            var last = holder.Last;
            Unset(sl.primary, ref last.Primary);
            Unset(sl.secondary, ref last.Secondary);
            Unset(sl.utility, ref last.Utility);
            Unset(sl.special, ref last.Special);

            if (extras)
            {
                Unset(extras.extraFirst, ref last.Extra1);
                Unset(extras.extraSecond, ref last.Extra2);
                Unset(extras.extraThird, ref last.Extra3);
                Unset(extras.extraFourth, ref last.Extra4);
            }

            holder.Last = last;

            // Optional: also clear managed passives
            foreach (var rec in QuirkRegistry.All.Values)
                if (rec.Category == QuirkCategory.Passive && rec.Buff)
                    body.ApplyBuff(rec.Buff.buffIndex, 0);

            body.RecalculateStats();
        }
    }
}
