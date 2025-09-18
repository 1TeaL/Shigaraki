using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ShiggyMod.Modules.Quirks.QuirkRegistry;

namespace ShiggyMod.Modules.Quirks
{
    public static class QuirkInventory
    {
        private static readonly HashSet<QuirkId> _owned = new HashSet<QuirkId>();
        public static IReadOnlyCollection<QuirkId> Owned => _owned;

        private static readonly List<QuirkId> _lastAutoCrafted = new List<QuirkId>();
        public static IReadOnlyList<QuirkId> LastAutoCrafted => _lastAutoCrafted;

        public static event Action OnOwnedChanged;

        public static bool Has(QuirkId id) => _owned.Contains(id);

        // Use QuirkRegistry.CanCraft(...) so leaf-owned ingredients count toward crafted results
        private static bool TryAutoCraftAllFromOwned()
        {
            bool anyChange = false;
            _lastAutoCrafted.Clear();

            bool keepGoing;
            do
            {
                keepGoing = false;

                foreach (var kv in QuirkRegistry.All)
                {
                    var rec = kv.Value;
                    if (!rec.IsCrafted) continue;
                    if (_owned.Contains(rec.Id)) continue;

                    // <-- THIS is the important bit: use the registry’s recursive craft check
                    if (QuirkRegistry.CanCraft(_owned, rec.Id))
                    {
                        _owned.Add(rec.Id);
                        _lastAutoCrafted.Add(rec.Id);
                        keepGoing = true;
                        anyChange = true;
                    }
                }
            } while (keepGoing);

            return anyChange;
        }

        public static bool Add(QuirkId id)
        {
            if (id == QuirkId.None) return false;

            bool changed = false;
            if (_owned.Add(id)) changed = true;

            if (TryAutoCraftAllFromOwned()) changed = true;

            if (changed) OnOwnedChanged?.Invoke();
            return changed;
        }

        public static void AddRange(IEnumerable<QuirkId> ids)
        {
            bool changed = false;
            foreach (var q in ids)
                if (q != QuirkId.None && _owned.Add(q))
                    changed = true;

            if (TryAutoCraftAllFromOwned()) changed = true;

            if (changed) OnOwnedChanged?.Invoke();
        }

        public static void Clear()
        {
            if (_owned.Count == 0) return;
            _owned.Clear();
            _lastAutoCrafted.Clear();
            OnOwnedChanged?.Invoke();
        }

        // Steal helpers — make sure your steal path calls one of these!
        public static bool AddFromSkill(SkillDef sd)
        {
            if (sd == null) return false;
            if (QuirkRegistry.QuirkLookup.TryFromSkill(sd, out var id)) return Add(id);
            return false;
        }

        public static bool AddFromBuff(BuffDef bd)
        {
            if (bd == null) return false;
            if (QuirkRegistry.QuirkLookup.TryFromBuff(bd, out var id)) return Add(id);
            return false;
        }

        public static class QuirkPickupUI
        {
            public static string BuildPickupText(QuirkId id)
            {
                var rec = QuirkRegistry.Get(id);
                string niceName = MakeNiceName(id);
                string type = rec.Category == QuirkCategory.Passive ? "Passive"
                           : rec.Category == QuirkCategory.Active ? "Active"
                           : "Utility";
                string style = rec.Category == QuirkCategory.Passive ? "cIsUtility" : "cIsDamage";
                return $"<style={style}>{niceName}</style> Quirk Get! ({type})";
            }

            public static string MakeNiceName(QuirkId id)
            {
                switch (id)
                {
                    case QuirkId.Elite_BlazingPassive: return "Blazing Quirk";
                    case QuirkId.Elite_GlacialPassive: return "Glacial Quirk";
                    case QuirkId.Elite_MalachitePassive: return "Malachite Quirk";
                    case QuirkId.Elite_CelestinePassive: return "Celestine Quirk";
                    case QuirkId.Elite_OverloadingPassive: return "Overloading Quirk";
                    case QuirkId.Elite_LunarPassive: return "Lunar Quirk";
                    case QuirkId.Elite_MendingPassive: return "Mending Quirk";
                    case QuirkId.Elite_VoidPassive: return "Void Quirk";
                }
                return MakeNiceNameFromRaw(id.ToString());
            }

            private static string MakeNiceNameFromRaw(string raw)
            {
                int us = raw.LastIndexOf('_');
                // was: string tail = us >= 0 ? raw[(us + 1)..] : raw;
                string tail = us >= 0 ? raw.Substring(us + 1) : raw;
                tail = tail.Replace("Active", "").Replace("Passive", "");
                return string.IsNullOrWhiteSpace(tail) ? raw : tail;
            }
        }

        private static bool _seededThisRun;
        public static void SeedStartingQuirksFromConfig()
        {
            if (_seededThisRun) return;
            _seededThisRun = true;

            Clear();

            if (Modules.Config.StartWithAllQuirks.Value)
                AddRange(QuirkRegistry.All.Keys);
            else
                AddRange(GetShiggyBaseQuirks());
        }

        private static List<QuirkId> GetShiggyBaseQuirks() => new List<QuirkId>
        {
            QuirkId.Shiggy_DecayActive,
            QuirkId.Shiggy_AirCannonActive,
            QuirkId.Shiggy_BulletLaserActive,
            QuirkId.Shiggy_MultiplierActive,
        };

        public static void ResetSeedFlagForNextRun() { _seededThisRun = false; }
    }
}
