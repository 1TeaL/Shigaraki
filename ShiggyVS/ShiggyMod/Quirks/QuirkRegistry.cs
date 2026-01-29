using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ShiggyMod.Modules.Quirks
{
    public enum QuirkLevel
    {
        Level0,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6
    }

    public enum QuirkCategory { Active, Passive, Utility }

    public struct QuirkRecipe
    {
        public QuirkId[] Ingredients;   // order-agnostic
        public QuirkRecipe(params QuirkId[] ingredients) => Ingredients = ingredients;
    }

    public struct QuirkRecord
    {
        public QuirkId Id;
        public QuirkLevel Level;
        public QuirkCategory Category;

        public SkillDef SkillDef;   // resolved
        public BuffDef BuffDef;     // resolved (for passives, this can be the auto-equip buff)

        public List<QuirkRecipe> Recipes;

        public string[] BodyNames;
        public string[] BodyPaths;

        public string DisplayNameOverride;
        public Sprite IconOverride;

        public bool IsCrafted => Recipes != null && Recipes.Count > 0;
    }

    public static class QuirkRegistry
    {
        private static readonly Dictionary<QuirkId, QuirkRecord> _records = new Dictionary<QuirkId, QuirkRecord>();
        public static IReadOnlyDictionary<QuirkId, QuirkRecord> All => _records;

        // Reverse lookup
        private static readonly Dictionary<SkillDef, QuirkId> _skillToId = new Dictionary<SkillDef, QuirkId>();
        private static readonly Dictionary<BuffDef, QuirkId> _buffToId = new Dictionary<BuffDef, QuirkId>();

        public static bool TryGet(QuirkId id, out QuirkRecord rec) => _records.TryGetValue(id, out rec);
        public static QuirkRecord Get(QuirkId id) => _records[id];

        public static IEnumerable<QuirkRecipe> GetAllRecipesFor(QuirkId id)
        {
            if (_records.TryGetValue(id, out var r) && r.Recipes != null)
                return r.Recipes;

            return Array.Empty<QuirkRecipe>();
        }

        // Return all leaf ingredients required to build `id`
        public static IEnumerable<QuirkId> BaseIngredients(QuirkId id)
        {
            if (!_records.TryGetValue(id, out var rec))
                yield break;

            if (!rec.IsCrafted)
            {
                yield return id; // leaf
                yield break;
            }

            foreach (var recipe in rec.Recipes)
                foreach (var ing in recipe.Ingredients)
                    foreach (var leaf in BaseIngredients(ing))
                        yield return leaf;
        }

        private static bool CoversIngredient(HashSet<QuirkId> owned, QuirkId ingredient)
        {
            if (owned.Contains(ingredient))
                return true;

            var leaves = BaseIngredients(ingredient).ToArray();
            if (leaves.Length == 0) return false;

            foreach (var leaf in leaves)
                if (!owned.Contains(leaf))
                    return false;

            return true;
        }

        public static bool CanCraft(HashSet<QuirkId> owned, QuirkId target)
        {
            if (!_records.TryGetValue(target, out var rec) || !rec.IsCrafted)
                return false;

            foreach (var recipe in rec.Recipes)
            {
                bool ok = true;

                foreach (var ing in recipe.Ingredients)
                {
                    if (!CoversIngredient(owned, ing))
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok) return true;
            }

            return false;
        }

        public static IReadOnlyCollection<QuirkId> MissingLeaves(HashSet<QuirkId> owned, QuirkId target)
        {
            if (!_records.TryGetValue(target, out var rec) || !rec.IsCrafted)
                return Array.Empty<QuirkId>();

            int best = int.MaxValue;
            IReadOnlyCollection<QuirkId> bestMissing = Array.Empty<QuirkId>();

            foreach (var recipe in rec.Recipes)
            {
                var need = new HashSet<QuirkId>();

                foreach (var ing in recipe.Ingredients)
                {
                    if (owned.Contains(ing)) continue;

                    foreach (var leaf in BaseIngredients(ing))
                        need.Add(leaf);
                }

                need.RemoveWhere(owned.Contains);

                if (need.Count < best)
                {
                    best = need.Count;
                    bestMissing = need.ToArray();
                    if (best == 0) break;
                }
            }

            return bestMissing;
        }

        public static class QuirkLookup
        {
            public static bool TryFromSkill(SkillDef sd, out QuirkId id) => _skillToId.TryGetValue(sd, out id);
            public static bool TryFromBuff(BuffDef bd, out QuirkId id) => _buffToId.TryGetValue(bd, out id);

            internal static void RebuildReverseMaps()
            {
                _skillToId.Clear();
                _buffToId.Clear();

                foreach (var kv in _records)
                {
                    var id = kv.Key;
                    var rec = kv.Value;

                    if (rec.SkillDef != null) _skillToId[rec.SkillDef] = id;
                    if (rec.BuffDef != null) _buffToId[rec.BuffDef] = id;
                }
            }
        }

        // “Passive” in your clarified meaning: passive skill is equipped + automatically grants its buff.
        public static bool IsAutoBuffPassive(in QuirkRecord r)
            => r.Category == QuirkCategory.Passive && r.BuffDef != null;

        // Call this once after you have created all SkillDefs/BuffDefs.
        public static void BindQuirkRegistry()
        {
            BuildFromData();
            ResolveFromData();


            //QuirkIconBank.RegisterFromRegistryData();//too late here
        }

        private static void BuildFromData()
        {
            _records.Clear();

            foreach (var e in QuirkRegistryData.All)
            {
                _records[e.Id] = new QuirkRecord
                {
                    Id = e.Id,
                    Level = e.Level,
                    Category = e.Category,

                    Recipes = e.Recipes ?? new List<QuirkRecipe>(),

                    BodyNames = e.BodyNames,
                    BodyPaths = e.BodyPaths,

                    DisplayNameOverride = e.DisplayNameOverride,
                    IconOverride = null,

                    SkillDef = null,
                    BuffDef = null
                };
            }
        }

        private static void ResolveFromData()
        {
            _skillToId.Clear();
            _buffToId.Clear();
            QuirkTargetingMap.Clear();

            foreach (var e in QuirkRegistryData.All)
            {
                if (!_records.TryGetValue(e.Id, out var rec))
                    continue;

                // Resolve Skill/Buff
                rec.SkillDef = e.Skill != null ? e.Skill() : null;
                rec.BuffDef = e.Buff != null ? e.Buff() : null;

                _records[e.Id] = rec;

                // Reverse maps
                if (rec.SkillDef != null) _skillToId[rec.SkillDef] = e.Id;
                if (rec.BuffDef != null) _buffToId[rec.BuffDef] = e.Id;

                // Targeting map
                if (rec.BodyNames != null)
                    foreach (var n in rec.BodyNames)
                        QuirkTargetingMap.Add(n, e.Id);

                if (rec.BodyPaths != null)
                {
                    foreach (var path in rec.BodyPaths)
                    {
                        GameObject prefab = null;
                        try { prefab = Addressables.LoadAssetAsync<GameObject>(path).WaitForCompletion(); }
                        catch { prefab = null; }

                        if (prefab) QuirkTargetingMap.Add(prefab.name, e.Id);
                    }
                }
            }

            QuirkLookup.RebuildReverseMaps();
        }

        // Optional: expose icon (null allowed)
        public static Sprite GetIcon(QuirkId id)
        {
            if (!TryGet(id, out var r)) return null;
            if (r.IconOverride) return r.IconOverride;
            if (r.SkillDef && r.SkillDef.icon) return r.SkillDef.icon;
            if (r.BuffDef && r.BuffDef.iconSprite) return r.BuffDef.iconSprite;
            return null;
        }

        //elite quirk equipment
        public static bool TryGetEliteQuirkId(CharacterBody body, out QuirkId id)
        {
            id = QuirkId.None;
            if (!body) return false;

            // Vanilla elites
            if (body.HasBuff(RoR2Content.Buffs.AffixRed)) { id = QuirkId.Elite_BlazingPassive; return true; }
            if (body.HasBuff(RoR2Content.Buffs.AffixWhite)) { id = QuirkId.Elite_GlacialPassive; return true; }
            if (body.HasBuff(RoR2Content.Buffs.AffixPoison)) { id = QuirkId.Elite_MalachitePassive; return true; }
            if (body.HasBuff(RoR2Content.Buffs.AffixHaunted)) { id = QuirkId.Elite_CelestinePassive; return true; }
            if (body.HasBuff(RoR2Content.Buffs.AffixBlue)) { id = QuirkId.Elite_OverloadingPassive; return true; }
            if (body.HasBuff(RoR2Content.Buffs.AffixLunar)) { id = QuirkId.Elite_LunarPassive; return true; }

            // DLC1 elites
            if (body.HasBuff(DLC1Content.Buffs.EliteEarth)) { id = QuirkId.Elite_MendingPassive; return true; }
            if (body.HasBuff(DLC1Content.Buffs.EliteVoid)) { id = QuirkId.Elite_VoidPassive; return true; }

            // DLC2 elites (these names must match the content you have referenced elsewhere)
            if (body.HasBuff(DLC2Content.Buffs.EliteAurelionite)) { id = QuirkId.Elite_GildedPassive; return true; }
            if (body.HasBuff(DLC2Content.Buffs.EliteBead)) { id = QuirkId.Elite_TwistedPassive; return true; }

            return false;
        }

        public static EquipmentDef GetEliteEquipmentForId(QuirkId id)
        {
            switch (id)
            {
                case QuirkId.Elite_BlazingPassive: return RoR2Content.Elites.Fire.eliteEquipmentDef;
                case QuirkId.Elite_GlacialPassive: return RoR2Content.Elites.Ice.eliteEquipmentDef;
                case QuirkId.Elite_MalachitePassive: return RoR2Content.Elites.Poison.eliteEquipmentDef;
                case QuirkId.Elite_CelestinePassive: return RoR2Content.Elites.Haunted.eliteEquipmentDef;
                case QuirkId.Elite_OverloadingPassive: return RoR2Content.Elites.Lightning.eliteEquipmentDef;
                case QuirkId.Elite_LunarPassive: return RoR2Content.Elites.Lunar.eliteEquipmentDef;
                case QuirkId.Elite_MendingPassive: return DLC1Content.Elites.Earth.eliteEquipmentDef;
                case QuirkId.Elite_VoidPassive: return DLC1Content.Elites.Void.eliteEquipmentDef;
                case QuirkId.Elite_GildedPassive: return DLC2Content.Elites.Aurelionite.eliteEquipmentDef;
                case QuirkId.Elite_TwistedPassive: return DLC2Content.Elites.Bead.eliteEquipmentDef;
                default: return null;
            }
        }

        public static string GetDisplayName(QuirkId id)
        {
            // Reuse your pickup UI naming so it’s consistent
            return QuirkInventory.QuirkPickupUI.MakeNiceName(id);
        }
    }

    public static class QuirkTargetingMap
    {
        private static readonly Dictionary<string, QuirkId> _bodyToId = new Dictionary<string, QuirkId>(StringComparer.Ordinal);

        public static void Clear() => _bodyToId.Clear();

        public static void Add(string bodyName, QuirkId id)
        {
            if (!string.IsNullOrEmpty(bodyName) && id != QuirkId.None)
                _bodyToId[bodyName] = id;
        }

        public static bool TryGet(string bodyName, out QuirkId id) => _bodyToId.TryGetValue(bodyName, out id);

        public static IReadOnlyDictionary<string, QuirkId> All => _bodyToId;
    }

    public static class QuirkEquipUtils
    {
        public static HashSet<QuirkId> GetEquippedQuirks(CharacterBody body)
        {
            var set = new HashSet<QuirkId>();
            if (!body || !body.skillLocator) return set;

            void TryAdd(SkillDef sd)
            {
                if (sd != null && QuirkRegistry.QuirkLookup.TryFromSkill(sd, out var id))
                    set.Add(id);
            }

            var sl = body.skillLocator;
            TryAdd(sl.primary?.skillDef);
            TryAdd(sl.secondary?.skillDef);
            TryAdd(sl.utility?.skillDef);
            TryAdd(sl.special?.skillDef);

            var extras = body.GetComponent<ExtraSkillSlots.ExtraSkillLocator>();
            if (extras != null)
            {
                TryAdd(extras.extraFirst?.skillDef);
                TryAdd(extras.extraSecond?.skillDef);
                TryAdd(extras.extraThird?.skillDef);
                TryAdd(extras.extraFourth?.skillDef);
            }

            return set;
        }
    }
}
