// QuirkEquip.cs
using ExtraSkillSlots;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Skills;
using ShiggyMod.Modules.Networking; // EquipLoadoutRequest
using ShiggyMod.Modules.Survivors;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Networking;

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

        // Kept for wire compatibility if you ever want it; unused in slot-driven passive model.
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
            new EquipLoadoutRequest(body.master.netId, loadout)
                .Send(R2API.Networking.NetworkDestination.Server);
        }

        // ========================= SERVER SIDE =========================

        /// <summary>
        /// Server-authoritative apply; called from EquipLoadoutRequest.OnReceived when a body exists.
        /// - Sets/unsets slot overrides
        /// - Persists overrides via ShiggyMasterController.writeToSkillList(sd, idx) for idx=0..7
        /// - Passives are slot-driven: SyncFromEquippedSkillsServer(body)
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

            void EquipSlot(GenericSkill slot, ref SkillDef lastOverride, QuirkId qid, int persistIndex)
            {
                if (!slot)
                {
                    Persist(persistIndex, null);
                    return;
                }

                // Unset previous override (if any)
                if (lastOverride != null)
                {
                    slot.UnsetSkillOverride(slot, lastOverride, GenericSkill.SkillOverridePriority.Contextual);
                    lastOverride = null;
                }

                // Resolve & set new override
                SkillDef sd = ResolveSkill(qid);
                if (sd != null)
                {
                    slot.SetSkillOverride(slot, sd, GenericSkill.SkillOverridePriority.Contextual);
                    lastOverride = sd;
                }

                Persist(persistIndex, sd);
            }

            // Core 4
            EquipSlot(sl.primary, ref last.Primary, loadout.Primary, 0);
            EquipSlot(sl.secondary, ref last.Secondary, loadout.Secondary, 1);
            EquipSlot(sl.utility, ref last.Utility, loadout.Utility, 2);
            EquipSlot(sl.special, ref last.Special, loadout.Special, 3);

            // Extras 4
            if (extras != null)
            {
                EquipSlot(extras.extraFirst, ref last.Extra1, loadout.Extra1, 4);
                EquipSlot(extras.extraSecond, ref last.Extra2, loadout.Extra2, 5);
                EquipSlot(extras.extraThird, ref last.Extra3, loadout.Extra3, 6);
                EquipSlot(extras.extraFourth, ref last.Extra4, loadout.Extra4, 7);
            }
            else
            {
                // Still persist indexes so respawn doesn’t “keep” old extras
                Persist(4, null); Persist(5, null); Persist(6, null); Persist(7, null);
            }

            holder.Last = last;

            // Passives are slot-driven
            QuirkPassiveSync.SyncFromEquippedSkillsServer(body);

            // Server recalcs; clients replicate
            body.RecalculateStats();
        }

        private static SkillDef ResolveSkill(QuirkId id)
        {
            if (id == QuirkId.None) return null;

            if (QuirkRegistry.TryGet(id, out var r) && r.SkillDef != null)
                return r.SkillDef;

            // Fallbacks for base four
            switch (id)
            {
                case QuirkId.Shiggy_DecayActive: return Shiggy.decayDef;
                case QuirkId.Shiggy_AirCannonActive: return Shiggy.aircannonDef;
                case QuirkId.Shiggy_BulletLaserActive: return Shiggy.bulletlaserDef;
                case QuirkId.Shiggy_MultiplierActive: return Shiggy.multiplierDef;
                default: return null;
            }
        }

        /// <summary>
        /// Unset previously applied overrides AND clear persisted respawn list (server-only).
        /// </summary>
        [Server]
        public static void Clear(CharacterBody body, ExtraSkillLocator extras)
        {
            if (!NetworkServer.active) return;
            if (!body || body.skillLocator == null) return;

            var sl = body.skillLocator;
            if (!_overrideCache.TryGetValue(body, out var holder)) return;

            var masterCtrl = body.master ? body.master.GetComponent<ShiggyMasterController>()
                                         : body.GetComponent<ShiggyMasterController>();

            void Persist(int idx, SkillDef sd)
            {
                if (masterCtrl != null) masterCtrl.writeToSkillList(sd, idx);
            }

            void Unset(GenericSkill slot, ref SkillDef lastOverride, int persistIndex)
            {
                if (slot != null && lastOverride != null)
                {
                    slot.UnsetSkillOverride(slot, lastOverride, GenericSkill.SkillOverridePriority.Contextual);
                    lastOverride = null;
                }

                // Clear persisted slot so respawn doesn't reapply it
                Persist(persistIndex, null);
            }

            var last = holder.Last;

            Unset(sl.primary, ref last.Primary, 0);
            Unset(sl.secondary, ref last.Secondary, 1);
            Unset(sl.utility, ref last.Utility, 2);
            Unset(sl.special, ref last.Special, 3);

            if (extras != null)
            {
                Unset(extras.extraFirst, ref last.Extra1, 4);
                Unset(extras.extraSecond, ref last.Extra2, 5);
                Unset(extras.extraThird, ref last.Extra3, 6);
                Unset(extras.extraFourth, ref last.Extra4, 7);
            }
            else
            {
                Persist(4, null); Persist(5, null); Persist(6, null); Persist(7, null);
            }

            holder.Last = last;

            // Clear slot-driven passive buffs (set to 0 for all auto-buff passives)
            foreach (var rec in QuirkRegistry.All.Values)
            {
                if (rec.Category != QuirkCategory.Passive) continue;
                if (rec.BuffDef == null) continue;

                body.ApplyBuff(rec.BuffDef.buffIndex, 0);
            }

            body.RecalculateStats();
        }
    }
}
