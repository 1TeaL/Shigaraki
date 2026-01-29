// QuirkEquip.cs
using ExtraSkillSlots;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Skills;
using ShiggyMod.Modules.Networking; // EquipLoadoutRequest
using ShiggyMod.Modules.Survivors;
using System.Runtime.CompilerServices;
using UnityEngine;
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

        // Remove this from runtime + wire if you're not using it.
        // public List<QuirkId> PassiveToggles;
    }

    public static class QuirkEquip
    {
        // Stable override "source" so Set/Unset always matches.
        private static readonly object OverrideSource = new object();

        // Track last overrides per body so we can unset cleanly.
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

            var pcmc = body.master.playerCharacterMasterController;
            var nu = pcmc ? pcmc.networkUser : null;
            if (!nu) return;

            new EquipLoadoutRequest(body.master.netId, loadout, nu.netId)
                .Send(NetworkDestination.Server);
        }


        // ========================= SERVER SIDE =========================

        /// <summary>
        /// Server-authoritative apply. Sets/unsets slot overrides and persists defs into master list (0..7).
        /// </summary>
        [Server]
        internal static void ApplyServer(CharacterBody body, ExtraSkillLocator extras, in SelectedQuirkLoadout loadout)
        {
            if (!NetworkServer.active) return;
            if (!body || body.skillLocator == null) return;

            var sl = body.skillLocator;
            if (extras == null) extras = body.GetComponent<ExtraSkillLocator>();

            // Master persistence holder (must exist on server master to persist across respawn)
            var masterCtrl = body.master ? body.master.GetComponent<ShiggyMasterController>() : null;

            var holder = _overrideCache.GetValue(body, _ => new SlotOverridesHolder());
            var last = holder.Last;

            void Persist(int idx, SkillDef sd)
            {
                if (masterCtrl != null) masterCtrl.writeToSkillList(sd, idx);
            }

            void EquipSlot(GenericSkill slot, ref SkillDef lastOverride, QuirkId qid, int persistIndex)
            {
                // Always clear persisted value if slot is missing
                if (!slot)
                {
                    Persist(persistIndex, null);
                    return;
                }

                // Unset previous override (if any)
                if (lastOverride != null)
                {
                    slot.UnsetSkillOverride(OverrideSource, lastOverride, GenericSkill.SkillOverridePriority.Contextual);
                    lastOverride = null;
                }

                // Resolve & set new override
                SkillDef sd = ResolveSkill(qid);
                if (sd != null)
                {
                    slot.SetSkillOverride(OverrideSource, sd, GenericSkill.SkillOverridePriority.Contextual);
                    lastOverride = sd;
                }

                // Persist chosen def so respawn can reapply
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
                Persist(4, null); Persist(5, null); Persist(6, null); Persist(7, null);
            }

            holder.Last = last;

            // Passives are slot-driven
            QuirkPassiveSync.SyncFromEquippedSkillsServer(body);

            body.RecalculateStats();
        }

        /// <summary>
        /// Server-only: reapply saved defs from masterCtrl.skillListToOverrideOnRespawn onto a newly spawned body.
        /// Call this from your CharacterBody.Start hook (server-side) for Shiggy.
        /// </summary>
        [Server]
        public static void ApplySavedLoadoutOnSpawn(CharacterBody body)
        {
            if (!NetworkServer.active) return;
            if (!body || body.skillLocator == null || !body.master) return;

            var masterCtrl = body.master.GetComponent<ShiggyMasterController>();
            if (masterCtrl == null) return;

            var saved = masterCtrl.skillListToOverrideOnRespawn;
            if (saved == null || saved.Length < 8) return;

            var sl = body.skillLocator;
            var ex = body.GetComponent<ExtraSkillLocator>();

            SkillDef SavedOr(int idx, SkillDef fallback) =>
                saved[idx] != null ? saved[idx] : fallback;

            // Apply overrides from saved list
            sl.primary.SetSkillOverride(OverrideSource, SavedOr(0, Shiggy.decayDef), GenericSkill.SkillOverridePriority.Contextual);
            sl.secondary.SetSkillOverride(OverrideSource, SavedOr(1, Shiggy.bulletlaserDef), GenericSkill.SkillOverridePriority.Contextual);
            sl.utility.SetSkillOverride(OverrideSource, SavedOr(2, Shiggy.aircannonDef), GenericSkill.SkillOverridePriority.Contextual);
            sl.special.SetSkillOverride(OverrideSource, SavedOr(3, Shiggy.multiplierDef), GenericSkill.SkillOverridePriority.Contextual);

            if (ex != null)
            {
                ex.extraFirst.SetSkillOverride(OverrideSource, SavedOr(4, Shiggy.emptySkillDef), GenericSkill.SkillOverridePriority.Contextual);
                ex.extraSecond.SetSkillOverride(OverrideSource, SavedOr(5, Shiggy.emptySkillDef), GenericSkill.SkillOverridePriority.Contextual);
                ex.extraThird.SetSkillOverride(OverrideSource, SavedOr(6, Shiggy.emptySkillDef), GenericSkill.SkillOverridePriority.Contextual);
                ex.extraFourth.SetSkillOverride(OverrideSource, SavedOr(7, Shiggy.emptySkillDef), GenericSkill.SkillOverridePriority.Contextual);
            }

            QuirkPassiveSync.SyncFromEquippedSkillsServer(body);
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

        [Server]
        public static void Clear(CharacterBody body, ExtraSkillLocator extras)
        {
            if (!NetworkServer.active) return;
            if (!body || body.skillLocator == null) return;

            var sl = body.skillLocator;
            if (!_overrideCache.TryGetValue(body, out var holder)) return;

            var masterCtrl = body.master ? body.master.GetComponent<ShiggyMasterController>() : null;

            void Persist(int idx, SkillDef sd)
            {
                if (masterCtrl != null) masterCtrl.writeToSkillList(sd, idx);
            }

            void Unset(GenericSkill slot, ref SkillDef lastOverride, int persistIndex)
            {
                if (slot != null && lastOverride != null)
                {
                    slot.UnsetSkillOverride(OverrideSource, lastOverride, GenericSkill.SkillOverridePriority.Contextual);
                    lastOverride = null;
                }

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

            // Clear slot-driven passive buffs
            foreach (var rec in QuirkRegistry.All.Values)
            {
                if (rec.Category != QuirkCategory.Passive) continue;
                if (rec.BuffDef == null) continue;
                body.ApplyBuff(rec.BuffDef.buffIndex, 0);
            }

            body.RecalculateStats();
        }
        public static void ApplyClientLocal(CharacterBody body, in SelectedQuirkLoadout loadout)
        {
            if (!body || body.skillLocator == null) return;

            var sl = body.skillLocator;
            var ex = body.GetComponent<ExtraSkillLocator>();

            var holder = _overrideCache.GetValue(body, _ => new SlotOverridesHolder());
            var last = holder.Last;

            void EquipSlot(GenericSkill slot, ref SkillDef lastOverride, QuirkId qid)
            {
                if (!slot) return;

                // Unset our previous override locally
                if (lastOverride != null)
                {
                    slot.UnsetSkillOverride(OverrideSource, lastOverride, GenericSkill.SkillOverridePriority.Contextual);
                    lastOverride = null;
                }

                // Set new
                var sd = ResolveSkill(qid);
                if (sd != null)
                {
                    slot.SetSkillOverride(OverrideSource, sd, GenericSkill.SkillOverridePriority.Contextual);
                    lastOverride = sd;
                }
            }

            EquipSlot(sl.primary, ref last.Primary, loadout.Primary);
            EquipSlot(sl.secondary, ref last.Secondary, loadout.Secondary);
            EquipSlot(sl.utility, ref last.Utility, loadout.Utility);
            EquipSlot(sl.special, ref last.Special, loadout.Special);

            if (ex != null)
            {
                EquipSlot(ex.extraFirst, ref last.Extra1, loadout.Extra1);
                EquipSlot(ex.extraSecond, ref last.Extra2, loadout.Extra2);
                EquipSlot(ex.extraThird, ref last.Extra3, loadout.Extra3);
                EquipSlot(ex.extraFourth, ref last.Extra4, loadout.Extra4);
            }

            holder.Last = last;

            // Force refresh on client
            body.MarkAllStatsDirty();
        }


    }
}
