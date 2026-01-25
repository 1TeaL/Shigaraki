using RoR2;
using RoR2.Skills;
using ShiggyMod.Modules.Survivors;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static ShiggyMod.Modules.Buffs;
using static ShiggyMod.Modules.Quirks.QuirkRegistry;
using static ShiggyMod.Modules.Survivors.Shiggy;
using static ShiggyMod.Modules.Survivors.ShiggyMasterController;

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

        public SkillDef Skill;  // null for pure passives
        public BuffDef Buff;   // null for pure actives

        public List<QuirkRecipe> Recipes;  // empty for Level1
        public bool IsCrafted => Recipes != null && Recipes.Count > 0;
    }

    public static class QuirkRegistry
    {
        private static readonly Dictionary<QuirkId, QuirkRecord> _records = new Dictionary<QuirkId, QuirkRecord>();
        public static IReadOnlyDictionary<QuirkId, QuirkRecord> All => _records;

        public static bool TryGet(QuirkId id, out QuirkRecord rec) => _records.TryGetValue(id, out rec);
        public static QuirkRecord Get(QuirkId id) => _records[id];
        public static IEnumerable<QuirkRecipe> GetAllRecipesFor(QuirkId id)
        {
            QuirkRecord r;
            if (_records.TryGetValue(id, out r) && r.Recipes != null)
                return r.Recipes;
            return Array.Empty<QuirkRecipe>();
        }

        // Return all leaf (Level-1) ingredients required to build `id`
        public static IEnumerable<QuirkId> BaseIngredients(QuirkId id)
        {
            if (!_records.TryGetValue(id, out var rec)) yield break;

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

        // Owned “covers” ingredient if we own the ingredient OR all its leaves
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

        // Craft check using base leaves (ingredient OR its leaves)
        public static bool CanCraft(HashSet<QuirkId> owned, QuirkId target)
        {
            if (!_records.TryGetValue(target, out var rec) || !rec.IsCrafted) return false;

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

        // Optional helper to show what leaves are missing for the best recipe
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

        /// Bind your concrete SkillDefs/BuffDefs after your content packs are built.
        private static readonly Dictionary<SkillDef, QuirkId> _skillToId = new Dictionary<SkillDef, QuirkId>();
        private static readonly Dictionary<BuffDef, QuirkId> _buffToId = new Dictionary<BuffDef, QuirkId>();


        public static bool TryResolve(SkillDef sd, out QuirkId id) => _skillToId.TryGetValue(sd, out id);
        public static bool TryResolve(BuffDef bf, out QuirkId id) => _buffToId.TryGetValue(bf, out id);


        public static class QuirkLookup
        {
            // Reverse mapping built during BindQuirkRegistry
            private static readonly Dictionary<SkillDef, QuirkId> _skillToId = new Dictionary<SkillDef, QuirkId>();
            private static readonly Dictionary<BuffDef, QuirkId> _buffToId = new Dictionary<BuffDef, QuirkId>();

            public static void RebuildReverseMaps(IReadOnlyDictionary<QuirkId, QuirkRecord> all)
            {
                _skillToId.Clear();
                _buffToId.Clear();
                foreach (var kv in all)
                {
                    var id = kv.Key;
                    var rec = kv.Value;
                    if (rec.Skill != null && !_skillToId.ContainsKey(rec.Skill))
                        _skillToId.Add(rec.Skill, id);
                    if (rec.Buff != null && !_buffToId.ContainsKey(rec.Buff))
                        _buffToId.Add(rec.Buff, id);
                }
            }

            public static bool TryFromSkill(SkillDef sd, out QuirkId id) { return _skillToId.TryGetValue(sd, out id); }
            public static bool TryFromBuff(BuffDef bd, out QuirkId id) { return _buffToId.TryGetValue(bd, out id); }
        }



        // Update your existing Bind() to populate reverse maps:
        public static void Bind(Dictionary<QuirkId, SkillDef> skills, Dictionary<QuirkId, BuffDef> buffs = null)
        {
            foreach (var kv in skills)
                if (_records.TryGetValue(kv.Key, out var r))
                {
                    r.Skill = kv.Value; _records[kv.Key] = r;
                    if (kv.Value) _skillToId[kv.Value] = kv.Key;
                }

            if (buffs != null)
                foreach (var kv in buffs)
                    if (_records.TryGetValue(kv.Key, out var r))
                    {
                        r.Buff = kv.Value; _records[kv.Key] = r;
                        if (kv.Value) _buffToId[kv.Value] = kv.Key;
                    }
        }

        // Call this once after you have created all SkillDefs/BuffDefs (e.g., after content packs are built)
        public static void BindQuirkRegistry()
        {
            QuirkRegistry.Build(); // builds the recipe graph

            QuirkRegistry.Bind(
                BuildSkillMap(),   // SkillDefs per QuirkId
                BuildBuffMap()     // (optional) BuffDefs per QuirkId
            );
            // Build reverse lookup tables for stealing
            QuirkLookup.RebuildReverseMaps(QuirkRegistry.All);

            //Build body name to quirk ID
            QuirkTargetingMap.BuildBodyToQuirkId();

            //add starting quirks
            QuirkInventory.SeedStartingQuirksFromConfig();

            //create sprites for quirks
            QuirkIconBank.CreateSpriteIcons();
        }
        public static void LateRebindIfMissing()
        {
            // If any record still lacks a SkillDef, try rebinding (cheap idempotent pass)
            if (All.Values.Any(r => r.Skill == null))
            {
                Bind(BuildSkillMap(), BuildBuffMap());
                QuirkLookup.RebuildReverseMaps(All);
                // (No need to rebuild recipes/targets; Bind() only fills Skill/Buff refs)
                Debug.Log("[Shiggy] LateRebindIfMissing: filled missing SkillDefs.");
            }
        }

        public static void Build()
        {
            _records.Clear();

            void AddLeaf(QuirkId id, QuirkLevel lvl, QuirkCategory cat)
            {
                _records[id] = new QuirkRecord
                {
                    Id = id,
                    Level = lvl,
                    Category = cat,
                    Recipes = new List<QuirkRecipe>()
                };
            }

            void AddRecipe(QuirkId result, QuirkLevel lvl, QuirkCategory cat, params QuirkId[] ingredients)
            {
                if (!_records.TryGetValue(result, out var rec))
                {
                    rec = new QuirkRecord
                    {
                        Id = result,
                        Level = lvl,
                        Category = cat,
                        Recipes = new List<QuirkRecipe>()
                    };
                }
                rec.Level = lvl;       // keep newest level if called multiple times
                rec.Category = cat; 
                if (rec.Recipes == null)
                    rec.Recipes = new List<QuirkRecipe>();
                rec.Recipes.Add(new QuirkRecipe(ingredients));
                _records[result] = rec;
            }

            // ============================
            // LEVEL 1 — Utilities
            // ============================
            AddLeaf(QuirkId.Utility_EmptySkill, QuirkLevel.Level0, QuirkCategory.Utility);
            AddLeaf(QuirkId.Utility_Choose, QuirkLevel.Level0, QuirkCategory.Utility);
            AddLeaf(QuirkId.Utility_Remove, QuirkLevel.Level0, QuirkCategory.Utility);

            // ============================
            // Elite affixes — treat as Level 1 Passives
            // ============================
            AddLeaf(QuirkId.Elite_BlazingPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Elite_GlacialPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Elite_MalachitePassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Elite_CelestinePassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Elite_OverloadingPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Elite_LunarPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Elite_MendingPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Elite_VoidPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Elite_GildedPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Elite_TwistedPassive, QuirkLevel.Level1, QuirkCategory.Passive);

            // ============================
            // LEVEL 1 — Actives (standalone)
            // ============================
            AddLeaf(QuirkId.Shiggy_DecayActive, QuirkLevel.Level0, QuirkCategory.Active);
            AddLeaf(QuirkId.Shiggy_AirCannonActive, QuirkLevel.Level0, QuirkCategory.Active);
            AddLeaf(QuirkId.Shiggy_BulletLaserActive, QuirkLevel.Level0, QuirkCategory.Active);
            AddLeaf(QuirkId.Shiggy_MultiplierActive, QuirkLevel.Level0, QuirkCategory.Active);

            AddLeaf(QuirkId.Vulture_WindBlastActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.BeetleGuard_SlamActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Bison_ChargeActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Bell_SpikedBallControlActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.ClayApothecary_ClayAirStrikeActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.ClayTemplar_ClayMinigunActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.ElderLemurian_FireBlastActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.GreaterWisp_SpiritBoostActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Imp_BlinkActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Jellyfish_RegenerateActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Lemurian_FireballActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.LunarGolem_SlideResetActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.LunarWisp_MinigunActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Parent_TeleportActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.SolusDistributor_SolusPlantMineActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.SolusExtractor_SolusExtractActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.SolusInvalidator_SolusInvalidateActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.SolusScorcher_SolusAccelerateActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.SolusTransporter_SolusTransportActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.StoneGolem_LaserActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.VoidReaver_PortalActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.BeetleQueen_SummonActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Grandparent_SunActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Grovetender_ChainActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.ClayDunestrider_TarBoostActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.SolusControlUnit_KnockupActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.XIConstruct_BeamActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.VoidDevastator_MissilesActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Scavenger_ThqwibActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Halcyonite_GreedActive, QuirkLevel.Level1, QuirkCategory.Active);

            AddLeaf(QuirkId.Artificer_FlamethrowerActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Artificer_IceWallActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Artificer_LightningOrbActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Bandit_LightsOutActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Engineer_TurretActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Huntress_FlurryActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Merc_DashActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.MULT_PowerStanceActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.MULT_PowerStanceCancelActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Operator_S141CustomActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Railgunner_CryoActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.REX_MortarActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Seeker_MeditateActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.VoidFiend_CleanseActive, QuirkLevel.Level1, QuirkCategory.Active);
            AddLeaf(QuirkId.Deku_OFAActive, QuirkLevel.Level1, QuirkCategory.Active);



            // ============================
            // Level 2 — Actives (pairs of L1)
            // ============================
            AddRecipe(QuirkId.Railgunner_LunarWisp_RapidPierceActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.Railgunner_CryoActive, QuirkId.LunarWisp_MinigunActive);
            AddRecipe(QuirkId.BulletLaser_StoneGolem_SweepingBeamActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.Shiggy_BulletLaserActive, QuirkId.StoneGolem_LaserActive);

            AddRecipe(QuirkId.VoidDevastator_Huntress_BlackholeGlaiveActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.VoidDevastator_MissilesActive, QuirkId.Huntress_FlurryActive);

            AddRecipe(QuirkId.VoidJailer_SolusControlUnit_GravitationalDownforceActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.VoidJailer_GravityPassive, QuirkId.SolusControlUnit_KnockupActive);

            AddRecipe(QuirkId.Vulture_Engineer_WindShieldActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.Vulture_WindBlastActive, QuirkId.Engineer_TurretActive);

            AddRecipe(QuirkId.XIConstruct_ClayApothecary_GenesisActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.XIConstruct_BeamActive, QuirkId.ClayApothecary_ClayAirStrikeActive);

            AddRecipe(QuirkId.LunarExploder_LunarGolem_RefreshActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.LunarExploder_LunarBarrierPassive, QuirkId.LunarGolem_SlideResetActive);

            AddRecipe(QuirkId.Imp_MagmaWorm_ExpungeActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.ImpBoss_BleedPassive, QuirkId.MagmaWorm_BlazingAuraPassive);

            AddRecipe(QuirkId.Imp_Bandit_ShadowClawActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.Imp_BlinkActive, QuirkId.Bandit_LightsOutActive);

            AddRecipe(QuirkId.Captain_VoidReaver_OrbitalStrikeActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.Captain_MicrobotsPassive, QuirkId.VoidReaver_PortalActive);

            AddRecipe(QuirkId.OverloadingWorm_Bison_ThunderclapActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.OverloadingWorm_LightningAuraPassive, QuirkId.Bison_ChargeActive);

            AddRecipe(QuirkId.ElderLemurian_Lemurian_BlastBurnActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.ElderLemurian_FireBlastActive, QuirkId.Lemurian_FireballActive);

            AddRecipe(QuirkId.AlphaConstruct_Jellyfish_BarrierJellyActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.AlphaConstruct_BarrierPassive, QuirkId.Jellyfish_RegenerateActive);

            AddRecipe(QuirkId.BeetleGuard_MULT_MechStanceActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.BeetleGuard_SlamActive, QuirkId.MULT_PowerStanceActive);

            AddRecipe(QuirkId.AirCannon_Merc_WindSlashActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.Shiggy_AirCannonActive, QuirkId.Merc_DashActive);

            AddRecipe(QuirkId.Multiplier_Deku_LimitBreakActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.Shiggy_MultiplierActive, QuirkId.Deku_OFAActive);

            AddRecipe(QuirkId.VoidBarnacle_VoidFiend_VoidFormActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.VoidBarnacle_VoidMortarPassive, QuirkId.VoidFiend_CleanseActive);

            AddRecipe(QuirkId.REX_Decay_DecayPlusUltraActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.REX_MortarActive, QuirkId.Shiggy_DecayActive);

            AddRecipe(QuirkId.Parent_Loader_MachPunchActive, QuirkLevel.Level2, QuirkCategory.Active,
                QuirkId.Parent_TeleportActive, QuirkId.Loader_ScrapBarrierPassive);

            // ============================
            // Level 4 — Actives (pairs of L2)
            // ============================
            AddRecipe(QuirkId.ShadowClaw_Genesis_LightAndDarknessActive, QuirkLevel.Level4, QuirkCategory.Active,
                QuirkId.Imp_Bandit_ShadowClawActive, QuirkId.XIConstruct_ClayApothecary_GenesisActive);

            AddRecipe(QuirkId.Refresh_Gacha_WildCardActive, QuirkLevel.Level4, QuirkCategory.Active,
                QuirkId.LunarExploder_LunarGolem_RefreshActive, QuirkId.BeetleQueen_Scavenger_GachaPassive);

            AddRecipe(QuirkId.OrbitalStrike_BlastBurn_BlastingZoneActive, QuirkLevel.Level4, QuirkCategory.Active,
                QuirkId.Captain_VoidReaver_OrbitalStrikeActive, QuirkId.ElderLemurian_Lemurian_BlastBurnActive);

            AddRecipe(QuirkId.WindShield_WindSlash_FinalReleaseActive, QuirkLevel.Level4, QuirkCategory.Active,
                QuirkId.Vulture_Engineer_WindShieldActive, QuirkId.AirCannon_Merc_WindSlashActive);

            AddRecipe(QuirkId.RapidPierce_SweepingBeam_XBeamerActive, QuirkLevel.Level4, QuirkCategory.Active,
                QuirkId.Railgunner_LunarWisp_RapidPierceActive, QuirkId.Railgunner_LunarWisp_RapidPierceActive);
            AddRecipe(QuirkId.RapidPierce_SweepingBeam_XBeamerActive, QuirkLevel.Level4, QuirkCategory.Active,
                QuirkId.Railgunner_LunarWisp_RapidPierceActive, QuirkId.BulletLaser_StoneGolem_SweepingBeamActive);

            AddRecipe(QuirkId.VoidForm_LimitBreak_OFAFOActive, QuirkLevel.Level4, QuirkCategory.Active,
                QuirkId.VoidBarnacle_VoidFiend_VoidFormActive, QuirkId.Multiplier_Deku_LimitBreakActive);

            AddRecipe(QuirkId.BarbedSpikes_Expunge_DeathAuraActive, QuirkLevel.Level4, QuirkCategory.Active,
                QuirkId.Bell_Gup_BarbedSpikesPassive, QuirkId.Imp_MagmaWorm_ExpungeActive);

            AddRecipe(QuirkId.MachPunch_Thunderclap_ExtremeSpeedActive, QuirkLevel.Level4, QuirkCategory.Active,
                QuirkId.Parent_Loader_MachPunchActive, QuirkId.OverloadingWorm_Bison_ThunderclapActive);

            AddRecipe(QuirkId.DoubleTime_Omniboost_TheWorldActive, QuirkLevel.Level4, QuirkCategory.Active,
                QuirkId.RoboBallMini_Commando_DoubleTimePassive, QuirkId.LesserWisp_Beetle_OmniboostPassive);

            // ============================
            // Level 6 — Actives 
            // ============================
            AddRecipe(QuirkId.Solusx6_SolusFactorUnleashedActive, QuirkLevel.Level6, QuirkCategory.Active,
                QuirkId.SolusDistributor_SolusPlantMineActive, QuirkId.SolusExtractor_SolusExtractActive, QuirkId.SolusInvalidator_SolusInvalidateActive, QuirkId.SolusProspector_PrimingPassive, QuirkId.SolusScorcher_SolusAccelerateActive, QuirkId.SolusTransporter_SolusTransportActive);

            // ============================
            // LEVEL 1 — Passives (standalone)
            // ============================
            AddLeaf(QuirkId.AlphaConstruct_BarrierPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Beetle_StrengthPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Pest_JumpPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Vermin_SpeedPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Gup_SpikyBodyPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.HermitCrab_MortarPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Larva_AcidJumpPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.LesserWisp_HastePassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.LunarExploder_LunarBarrierPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.MiniMushrum_HealingAuraPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.RoboBallMini_SolusBoostPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.VoidBarnacle_VoidMortarPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.VoidJailer_GravityPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.ImpBoss_BleedPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.SolusProspector_PrimingPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.StoneTitan_StoneSkinPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.MagmaWorm_BlazingAuraPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.OverloadingWorm_LightningAuraPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Vagrant_OrbPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Child_EmergencyTeleportPassive, QuirkLevel.Level1, QuirkCategory.Passive);

            AddLeaf(QuirkId.Acrid_PoisonPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Commando_DoubleTapPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Captain_MicrobotsPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Loader_ScrapBarrierPassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.FalseSon_StolenInheritancePassive, QuirkLevel.Level1, QuirkCategory.Passive);
            AddLeaf(QuirkId.Chef_OilBurstPassive, QuirkLevel.Level1, QuirkCategory.Passive);

            // ============================
            // Level 2 — Passives (pairs of L1)
            // ============================
            AddRecipe(QuirkId.Pest_Vermin_BlindSensesPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                QuirkId.Pest_JumpPassive, QuirkId.Vermin_SpeedPassive);

            AddRecipe(QuirkId.RoboBallMini_Commando_DoubleTimePassive, QuirkLevel.Level2, QuirkCategory.Passive,
                QuirkId.RoboBallMini_SolusBoostPassive, QuirkId.Commando_DoubleTapPassive);

            AddRecipe(QuirkId.Grandparent_Artificer_ElementalFusionPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                QuirkId.Grandparent_SunActive, QuirkId.Artificer_FlamethrowerActive);
            AddRecipe(QuirkId.Grandparent_Artificer_ElementalFusionPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                QuirkId.Grandparent_SunActive, QuirkId.Artificer_IceWallActive);
            AddRecipe(QuirkId.Grandparent_Artificer_ElementalFusionPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                QuirkId.Grandparent_SunActive, QuirkId.Artificer_LightningOrbActive);

            AddRecipe(QuirkId.LesserWisp_Beetle_OmniboostPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                QuirkId.LesserWisp_HastePassive, QuirkId.Beetle_StrengthPassive);

            AddRecipe(QuirkId.ClayDunestrider_MiniMushrum_IngrainPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                QuirkId.ClayDunestrider_TarBoostActive, QuirkId.MiniMushrum_HealingAuraPassive);

            AddRecipe(QuirkId.Bell_Gup_BarbedSpikesPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                QuirkId.Bell_SpikedBallControlActive, QuirkId.Gup_SpikyBodyPassive);

            AddRecipe(QuirkId.Acrid_Larva_AuraOfBlightPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                QuirkId.Acrid_PoisonPassive, QuirkId.Larva_AcidJumpPassive);

            AddRecipe(QuirkId.HermitCrab_Titan_StoneFormPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                QuirkId.HermitCrab_MortarPassive, QuirkId.StoneTitan_StoneSkinPassive);

            AddRecipe(QuirkId.Vagrant_ClayTemplar_BigBangPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                QuirkId.Vagrant_OrbPassive, QuirkId.ClayTemplar_ClayMinigunActive);

            AddRecipe(QuirkId.Wisp_Grovetender_WisperPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                QuirkId.GreaterWisp_SpiritBoostActive, QuirkId.Grovetender_ChainActive);

            // ============================
            // Level 4 — Passives (pairs of L2)
            // ============================
            AddRecipe(QuirkId.ShiggyDecay_AuraOfBlight_DecayAwakenedPassive, QuirkLevel.Level4, QuirkCategory.Passive,
                QuirkId.REX_Decay_DecayPlusUltraActive, QuirkId.Acrid_Larva_AuraOfBlightPassive);

            AddRecipe(QuirkId.Jailer_Grandparent_ElementalFusion_WeatherReportPassive, QuirkLevel.Level4, QuirkCategory.Passive,
                QuirkId.VoidJailer_SolusControlUnit_GravitationalDownforceActive, QuirkId.Grandparent_Artificer_ElementalFusionPassive);

            AddRecipe(QuirkId.Ingrain_StoneForm_GargoyleProtectionPassive, QuirkLevel.Level4, QuirkCategory.Passive,
                QuirkId.ClayDunestrider_MiniMushrum_IngrainPassive, QuirkId.HermitCrab_Titan_StoneFormPassive);

            AddRecipe(QuirkId.BlackholeGlaive_MechStance_MachineFormPassive, QuirkLevel.Level4, QuirkCategory.Passive,
                QuirkId.VoidDevastator_Huntress_BlackholeGlaiveActive, QuirkId.BeetleGuard_MULT_MechStanceActive);

            AddRecipe(QuirkId.BarrierJelly_BlindSenses_ReversalPassive, QuirkLevel.Level4, QuirkCategory.Passive,
                QuirkId.AlphaConstruct_Jellyfish_BarrierJellyActive, QuirkId.Pest_Vermin_BlindSensesPassive);

            AddRecipe(QuirkId.BigBang_Wisper_SupernovaPassive, QuirkLevel.Level4, QuirkCategory.Passive,
                QuirkId.Vagrant_ClayTemplar_BigBangPassive, QuirkId.Wisp_Grovetender_WisperPassive);
            AddRecipe(QuirkId.MiniMushrum_Jellyfish_FalseSon_Seeker_HyperRegenerationPassive, QuirkLevel.Level4, QuirkCategory.Passive,
                QuirkId.MiniMushrum_HealingAuraPassive, QuirkId.Jellyfish_RegenerateActive, QuirkId.FalseSon_StolenInheritancePassive, QuirkId.Seeker_MeditateActive);

        }


        // -------------------- Skill map --------------------
        private static Dictionary<QuirkId, SkillDef> BuildSkillMap()
        {
            var m = new Dictionary<QuirkId, SkillDef>
            {
                // Utilities
                [QuirkId.Utility_EmptySkill] = emptySkillDef,
                [QuirkId.Utility_Choose] = chooseDef,
                [QuirkId.Utility_Remove] = removeDef,


                // =========================
                // Level 1 — Base Actives
                // =========================
                [QuirkId.Shiggy_DecayActive] = decayDef,
                [QuirkId.Shiggy_AirCannonActive] = aircannonDef,
                [QuirkId.Shiggy_BulletLaserActive] = bulletlaserDef,
                [QuirkId.Shiggy_MultiplierActive] = multiplierDef,

                [QuirkId.Vulture_WindBlastActive] = alloyvultureWindBlastDef,
                [QuirkId.BeetleGuard_SlamActive] = beetleguardslamDef,
                [QuirkId.Bison_ChargeActive] = bisonchargeDef,
                [QuirkId.Bell_SpikedBallControlActive] = bronzongballDef,
                [QuirkId.ClayApothecary_ClayAirStrikeActive] = clayapothecarymortarDef,
                [QuirkId.ClayTemplar_ClayMinigunActive] = claytemplarminigunDef,
                [QuirkId.ElderLemurian_FireBlastActive] = elderlemurianfireblastDef,
                [QuirkId.GreaterWisp_SpiritBoostActive] = greaterWispBuffDef,
                [QuirkId.Imp_BlinkActive] = impblinkDef,
                [QuirkId.Jellyfish_RegenerateActive] = JellyfishRegenerateDef,
                [QuirkId.Lemurian_FireballActive] = lemurianfireballDef,
                [QuirkId.LunarGolem_SlideResetActive] = lunargolemSlideDef,
                [QuirkId.LunarWisp_MinigunActive] = lunarwispminigunDef,
                [QuirkId.Parent_TeleportActive] = parentteleportDef,
                [QuirkId.SolusDistributor_SolusPlantMineActive] = solusdistributorplantmineDef,
                [QuirkId.SolusExtractor_SolusExtractActive] = solusextractorextractDef,
                [QuirkId.SolusInvalidator_SolusInvalidateActive] = solusinvalidatorinvalidateDef,
                [QuirkId.SolusScorcher_SolusAccelerateActive] = solusscorcheraccelerateDef,
                [QuirkId.SolusTransporter_SolusTransportActive] = solustransportertransportDef,
                [QuirkId.StoneGolem_LaserActive] = stonegolemlaserDef,
                [QuirkId.VoidReaver_PortalActive] = voidreaverportalDef,

                [QuirkId.BeetleQueen_SummonActive] = beetlequeenSummonDef,
                [QuirkId.Grandparent_SunActive] = grandparentsunDef,
                [QuirkId.Grovetender_ChainActive] = grovetenderChainDef,
                [QuirkId.ClayDunestrider_TarBoostActive] = claydunestriderbuffDef,
                [QuirkId.SolusControlUnit_KnockupActive] = soluscontrolunityknockupDef,
                [QuirkId.XIConstruct_BeamActive] = xiconstructbeamDef,
                [QuirkId.VoidDevastator_MissilesActive] = voiddevastatorhomingDef,
                [QuirkId.Scavenger_ThqwibActive] = scavengerthqwibDef,

                [QuirkId.Artificer_FlamethrowerActive] = artificerflamethrowerDef,
                [QuirkId.Artificer_IceWallActive] = artificericewallDef,
                [QuirkId.Artificer_LightningOrbActive] = artificerlightningorbDef,
                [QuirkId.Bandit_LightsOutActive] = banditlightsoutDef,
                [QuirkId.Engineer_TurretActive] = engiturretDef,
                [QuirkId.Huntress_FlurryActive] = huntressattackDef,
                [QuirkId.Merc_DashActive] = mercdashDef,
                [QuirkId.MULT_PowerStanceActive] = multbuffDef,
                [QuirkId.MULT_PowerStanceCancelActive] = multbuffcancelDef,
                [QuirkId.Operator_S141CustomActive] = operators141customDef,
                [QuirkId.Railgunner_CryoActive] = railgunnercryoDef,
                [QuirkId.REX_MortarActive] = rexmortarDef,
                [QuirkId.Seeker_MeditateActive] = seekermeditateDef,
                [QuirkId.VoidFiend_CleanseActive] = voidfiendcleanseDef,
                [QuirkId.Deku_OFAActive] = DekuOFADef,


                // =========================
                // Level 2 — Actives
                // =========================
                [QuirkId.BulletLaser_StoneGolem_SweepingBeamActive] = sweepingBeamDef,
                [QuirkId.VoidDevastator_Huntress_BlackholeGlaiveActive] = blackholeGlaiveDef,
                [QuirkId.VoidJailer_SolusControlUnit_GravitationalDownforceActive] = gravitationalDownforceDef,
                [QuirkId.Vulture_Engineer_WindShieldActive] = windShieldDef,
                [QuirkId.XIConstruct_ClayApothecary_GenesisActive] = genesisDef,
                [QuirkId.LunarExploder_LunarGolem_RefreshActive] = refreshDef,
                [QuirkId.Imp_MagmaWorm_ExpungeActive] = expungeDef,
                [QuirkId.Imp_Bandit_ShadowClawActive] = shadowClawDef,
                [QuirkId.Captain_VoidReaver_OrbitalStrikeActive] = orbitalStrikeDef,
                [QuirkId.OverloadingWorm_Bison_ThunderclapActive] = thunderclapDef,
                [QuirkId.ElderLemurian_Lemurian_BlastBurnActive] = blastBurnDef,
                [QuirkId.AlphaConstruct_Jellyfish_BarrierJellyActive] = barrierJellyDef,
                [QuirkId.BeetleGuard_MULT_MechStanceActive] = mechStanceDef,
                [QuirkId.AirCannon_Merc_WindSlashActive] = windSlashDef,
                [QuirkId.Multiplier_Deku_LimitBreakActive] = limitBreakDef,
                [QuirkId.VoidBarnacle_VoidFiend_VoidFormActive] = voidFormDef,
                [QuirkId.REX_Decay_DecayPlusUltraActive] = decayPlusUltraDef,
                [QuirkId.Parent_Loader_MachPunchActive] = machPunchDef,
                [QuirkId.Railgunner_LunarWisp_RapidPierceActive] = rapidPierceDef,


                // =========================
                // Level 4 — Actives
                // =========================
                [QuirkId.ShadowClaw_Genesis_LightAndDarknessActive] = lightAndDarknessDef,
                [QuirkId.Refresh_Gacha_WildCardActive] = wildCardDef,
                [QuirkId.OrbitalStrike_BlastBurn_BlastingZoneActive] = blastingZoneDef,
                [QuirkId.WindShield_WindSlash_FinalReleaseActive] = finalReleaseDef,
                [QuirkId.RapidPierce_SweepingBeam_XBeamerActive] = xBeamerDef,
                [QuirkId.VoidForm_LimitBreak_OFAFOActive] = OFAFODef,
                [QuirkId.BarbedSpikes_Expunge_DeathAuraActive] = deathAuraDef,
                [QuirkId.MachPunch_Thunderclap_ExtremeSpeedActive] = extremeSpeedDef,
                [QuirkId.DoubleTime_Omniboost_TheWorldActive] = theWorldDef,

                // =========================
                // Level 6 — Actives
                // =========================
                [QuirkId.Solusx6_SolusFactorUnleashedActive] = solusfactorunleashedDef,


                // =========================
                // Level 1 — Base Passives (as SkillDefs)
                // =========================
                [QuirkId.AlphaConstruct_BarrierPassive] = alphaconstructpassiveDef,   // (typo in field name kept)
                [QuirkId.Beetle_StrengthPassive] = beetlepassiveDef,
                [QuirkId.Pest_JumpPassive] = pestpassiveDef,
                [QuirkId.Child_EmergencyTeleportPassive] = childpassiveDef,
                [QuirkId.Vermin_SpeedPassive] = verminpassiveDef,
                [QuirkId.Gup_SpikyBodyPassive] = guppassiveDef,
                [QuirkId.HermitCrab_MortarPassive] = hermitcrabpassiveDef,
                [QuirkId.Larva_AcidJumpPassive] = larvapassiveDef,
                [QuirkId.LesserWisp_HastePassive] = lesserwisppassiveDef,
                [QuirkId.LunarExploder_LunarBarrierPassive] = lunarexploderpassiveDef,
                [QuirkId.MiniMushrum_HealingAuraPassive] = minimushrumpassiveDef,
                [QuirkId.RoboBallMini_SolusBoostPassive] = roboballminibpassiveDef,
                [QuirkId.VoidBarnacle_VoidMortarPassive] = voidbarnaclepassiveDef,
                [QuirkId.VoidJailer_GravityPassive] = voidjailerpassiveDef,

                [QuirkId.AlloyHunter_CritBoostPassive] = alloyhuntercritboostDef,
                [QuirkId.ImpBoss_BleedPassive] = impbosspassiveDef,
                [QuirkId.SolusAmalgamator_EquipmentBoostPassive] = solusamalgamatorequipmentboostDef,
                [QuirkId.StoneTitan_StoneSkinPassive] = stonetitanpassiveDef,
                [QuirkId.MagmaWorm_BlazingAuraPassive] = magmawormpassiveDef,
                [QuirkId.OverloadingWorm_LightningAuraPassive] = overloadingwormpassiveDef,
                [QuirkId.Vagrant_OrbPassive] = vagrantpassiveDef,

                [QuirkId.Acrid_PoisonPassive] = acridpassiveDef,
                [QuirkId.Commando_DoubleTapPassive] = commandopassiveDef,
                [QuirkId.Captain_MicrobotsPassive] = captainpassiveDef,
                [QuirkId.Drifter_SalvageActive] = driftersalvageDef,
                [QuirkId.Loader_ScrapBarrierPassive] = loaderpassiveDef,
                [QuirkId.FalseSon_StolenInheritancePassive] = falsesonpassiveDef,
                [QuirkId.Chef_OilBurstPassive] = chefpassiveDef,

                // =========================
                // Level 2 — Passives
                // =========================
                [QuirkId.Pest_Vermin_BlindSensesPassive] = blindSensesPassiveDef,
                [QuirkId.RoboBallMini_Commando_DoubleTimePassive] = doubleTimePassiveDef,
                [QuirkId.Grandparent_Artificer_ElementalFusionPassive] = elementalFusionPassiveDef,
                [QuirkId.LesserWisp_Beetle_OmniboostPassive] = omniboostPassiveDef,
                [QuirkId.ClayDunestrider_MiniMushrum_IngrainPassive] = ingrainPassiveDef,
                [QuirkId.Bell_Gup_BarbedSpikesPassive] = barbedSpikesPassiveDef,
                [QuirkId.Acrid_Larva_AuraOfBlightPassive] = auraOfBlightPassiveDef,
                [QuirkId.HermitCrab_Titan_StoneFormPassive] = stoneFormPassiveDef,
                [QuirkId.Vagrant_ClayTemplar_BigBangPassive] = bigBangPassiveDef,
                [QuirkId.Wisp_Grovetender_WisperPassive] = wisperPassiveDef,


                // =========================
                // Level 4 — Passives
                // =========================
                [QuirkId.ShiggyDecay_AuraOfBlight_DecayAwakenedPassive] = decayAwakenedPassiveDef,
                [QuirkId.Jailer_Grandparent_ElementalFusion_WeatherReportPassive] = weatherReportPassiveDef,
                [QuirkId.Ingrain_StoneForm_GargoyleProtectionPassive] = gargoyleProtectionPassiveDef,
                [QuirkId.BlackholeGlaive_MechStance_MachineFormPassive] = machineFormPassiveDef,
                [QuirkId.BarrierJelly_BlindSenses_ReversalPassive] = reversalPassiveDef,
                [QuirkId.BigBang_Wisper_SupernovaPassive] = supernovaPassiveDef,
                [QuirkId.MiniMushrum_Jellyfish_FalseSon_Seeker_HyperRegenerationPassive] = hyperRegenerationPassiveDef,

            };

            return m;
        }

        // -------------------- Buff map (optional but useful) --------------------
        private static Dictionary<QuirkId, BuffDef> BuildBuffMap()
        {
            var b = new Dictionary<QuirkId, BuffDef>
            {
                // Elite affix buffs (from base/DLC content)
                [QuirkId.Elite_BlazingPassive] = RoR2Content.Buffs.AffixRed,
                [QuirkId.Elite_GlacialPassive] = RoR2Content.Buffs.AffixWhite,
                [QuirkId.Elite_MalachitePassive] = RoR2Content.Buffs.AffixPoison,
                [QuirkId.Elite_CelestinePassive] = RoR2Content.Buffs.AffixHaunted,
                [QuirkId.Elite_OverloadingPassive] = RoR2Content.Buffs.AffixBlue,
                [QuirkId.Elite_LunarPassive] = RoR2Content.Buffs.AffixLunar,
                [QuirkId.Elite_MendingPassive] = DLC1Content.Buffs.EliteEarth,
                [QuirkId.Elite_VoidPassive] = DLC1Content.Buffs.EliteVoid,
                // Level 1 passives (primary effect buffs)
                [QuirkId.AlphaConstruct_BarrierPassive] = alphashieldonBuff, // NOTE: there is also alphashieldoffBuff; on/off are internal states.
                [QuirkId.Beetle_StrengthPassive] = beetleBuff,
                [QuirkId.Pest_JumpPassive] = pestjumpBuff,
                [QuirkId.Vermin_SpeedPassive] = verminsprintBuff,
                [QuirkId.Gup_SpikyBodyPassive] = gupspikeBuff,
                [QuirkId.HermitCrab_MortarPassive] = hermitcrabmortarBuff,
                [QuirkId.Larva_AcidJumpPassive] = larvajumpBuff,
                [QuirkId.LesserWisp_HastePassive] = lesserwispBuff,
                [QuirkId.LunarExploder_LunarBarrierPassive] = lunarexploderBuff,
                [QuirkId.MiniMushrum_HealingAuraPassive] = minimushrumBuff,
                [QuirkId.RoboBallMini_SolusBoostPassive] = roboballminiBuff, // roboballminiattackspeedBuff is a sub-effect
                [QuirkId.SolusProspector_PrimingPassive] = solusPrimedBuff, // applies the primed debuff
                [QuirkId.VoidBarnacle_VoidMortarPassive] = voidbarnaclemortarBuff, // *_attackspeedBuff is a sub-effect
                [QuirkId.VoidJailer_GravityPassive] = voidjailerBuff,
                [QuirkId.Child_EmergencyTeleportPassive] = childBuff, // childCDDebuff is a debuff to turn it off

                [QuirkId.AlloyHunter_CritBoostPassive] = alloyhunterCritBoostBuff,
                [QuirkId.ImpBoss_BleedPassive] = impbossBuff,
                [QuirkId.SolusAmalgamator_EquipmentBoostPassive] = solusamalgamatorEquipmentBoostBuff,
                [QuirkId.StoneTitan_StoneSkinPassive] = stonetitanBuff,
                [QuirkId.MagmaWorm_BlazingAuraPassive] = magmawormBuff,
                [QuirkId.OverloadingWorm_LightningAuraPassive] = overloadingwormBuff,
                [QuirkId.Vagrant_OrbPassive] = vagrantBuff, // vagrantDebuff/vagrantdisableBuff are internal state/debuff toggles

                // Survivor passives
                [QuirkId.Acrid_PoisonPassive] = acridBuff,
                [QuirkId.Commando_DoubleTapPassive] = commandoBuff,
                [QuirkId.Captain_MicrobotsPassive] = captainBuff,
                [QuirkId.Loader_ScrapBarrierPassive] = loaderBuff,
                [QuirkId.FalseSon_StolenInheritancePassive] = falsesonStolenInheritanceBuff,
                [QuirkId.Chef_OilBurstPassive] = chefOilBurstBuff,

                // Level 2 passives
                [QuirkId.Pest_Vermin_BlindSensesPassive] = blindSensesBuff,
                [QuirkId.RoboBallMini_Commando_DoubleTimePassive] = doubleTimeBuff, // stacks: doubleTimeBuffStacks
                [QuirkId.Grandparent_Artificer_ElementalFusionPassive] = elementalFusionBuffStacks, // cycles: fire/freeze/shock buffs too
                [QuirkId.LesserWisp_Beetle_OmniboostPassive] = omniboostBuff, // stacks: omniboostBuffStacks
                [QuirkId.ClayDunestrider_MiniMushrum_IngrainPassive] = ingrainBuff,
                [QuirkId.Bell_Gup_BarbedSpikesPassive] = barbedSpikesBuff,
                [QuirkId.Acrid_Larva_AuraOfBlightPassive] = auraOfBlightBuff,
                [QuirkId.HermitCrab_Titan_StoneFormPassive] = stoneFormBuff, // plus stoneFormStillBuff
                [QuirkId.Vagrant_ClayTemplar_BigBangPassive] = bigbangBuff,
                [QuirkId.Wisp_Grovetender_WisperPassive] = wisperBuff,

                // Level 2 actives with persistent effects
                //[QuirkId.Vulture_Engineer_WindShieldActive] = windShieldBuff,
                //[QuirkId.BeetleGuard_MULT_MechStanceActive] = mechStanceBuff,
                //[QuirkId.Multiplier_Deku_LimitBreakActive] = limitBreakBuff,
                //[QuirkId.VoidBarnacle_VoidFiend_VoidFormActive] = voidFormBuff,

                // Level 4 passives
                [QuirkId.ShiggyDecay_AuraOfBlight_DecayAwakenedPassive] = decayAwakenedBuff,
                [QuirkId.Jailer_Grandparent_ElementalFusion_WeatherReportPassive] = weatherReportBuff,
                [QuirkId.Ingrain_StoneForm_GargoyleProtectionPassive] = gargoyleProtectionBuff,
                [QuirkId.BlackholeGlaive_MechStance_MachineFormPassive] = machineFormBuff,
                [QuirkId.BarrierJelly_BlindSenses_ReversalPassive] = reversalBuff, // + reversalBuffStacks
                [QuirkId.BigBang_Wisper_SupernovaPassive] = supernovaBuff,
                [QuirkId.MiniMushrum_Jellyfish_FalseSon_Seeker_HyperRegenerationPassive] = hyperRegenerationBuff,

                // Level 4 actives with toggled/lasting effects
                //[QuirkId.DoubleTime_Omniboost_TheWorldActive] = theWorldBuff,
                //[QuirkId.BarbedSpikes_Expunge_DeathAuraActive] = deathAuraBuff,
                //[QuirkId.VoidForm_LimitBreak_OFAFOActive] = OFAFOBuff,
                //[QuirkId.WindShield_WindSlash_FinalReleaseActive] = finalReleaseBuff,
                //[QuirkId.ShadowClaw_Genesis_LightAndDarknessActive] = lightAndDarknessFormBuff, // NOTE: this form swaps between light/dark/combined.


            };

            // Optional: add UI/“wild card” effect toggles if you want the registry to know them:
            // b[QuirkId.Refresh_Gacha_WildCardActive] = wildcardSpeedBuff; // etc. (multiple outcomes exist)

            return b;
        }
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

        // Optional: expose Skill/Buff icons (null allowed)
        public static Sprite GetIcon(QuirkId id)
        {
            QuirkRecord r;
            if (!TryGet(id, out r)) return null;
            if (r.Skill && r.Skill.icon) return r.Skill.icon;
            if (r.Buff && r.Buff.iconSprite) return r.Buff.iconSprite;
            return null;
        }

    }

    public static class QuirkTargetingMap
    {
        private static readonly Dictionary<string, QuirkId> _bodyToId = new Dictionary<string, QuirkId>(StringComparer.Ordinal);

        public static void Clear() { _bodyToId.Clear(); }

        public static void Add(string bodyName, QuirkId id)
        {
            if (!string.IsNullOrEmpty(bodyName) && id != QuirkId.None)
                _bodyToId[bodyName] = id;
        }

        public static bool TryGet(string bodyName, out QuirkId id)
        {
            return _bodyToId.TryGetValue(bodyName, out id);
        }

        public static IReadOnlyDictionary<string, QuirkId> All { get { return _bodyToId; } }



        public static void BuildBodyToQuirkId()
        {
            QuirkTargetingMap.Clear();

            // Passives (L1)
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.MinorConstructBody_prefab).WaitForCompletion().name, QuirkId.AlphaConstruct_BarrierPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.MinorConstructOnKillBody_prefab).WaitForCompletion().name, QuirkId.AlphaConstruct_BarrierPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.MinorConstructAttachableBody_prefab).WaitForCompletion().name, QuirkId.AlphaConstruct_BarrierPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Beetle.BeetleBody_prefab).WaitForCompletion().name, QuirkId.Beetle_StrengthPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_FlyingVermin.FlyingVerminBody_prefab).WaitForCompletion().name, QuirkId.Pest_JumpPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Vermin.VerminBody_prefab).WaitForCompletion().name, QuirkId.Vermin_SpeedPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Gup.GupBody_prefab).WaitForCompletion().name, QuirkId.Gup_SpikyBodyPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Gup.GipBody_prefab).WaitForCompletion().name, QuirkId.Gup_SpikyBodyPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Gup.GeepBody_prefab).WaitForCompletion().name, QuirkId.Gup_SpikyBodyPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_HermitCrab.HermitCrabBody_prefab).WaitForCompletion().name, QuirkId.HermitCrab_MortarPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_AcidLarva.AcidLarvaBody_prefab).WaitForCompletion().name, QuirkId.Larva_AcidJumpPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Wisp.WispBody_prefab).WaitForCompletion().name, QuirkId.LesserWisp_HastePassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_LunarExploder.LunarExploderBody_prefab).WaitForCompletion().name, QuirkId.LunarExploder_LunarBarrierPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_MiniMushroom.MiniMushroomBody_prefab).WaitForCompletion().name, QuirkId.MiniMushrum_HealingAuraPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_RoboBallBoss.RoboBallMiniBody_prefab).WaitForCompletion().name, QuirkId.RoboBallMini_SolusBoostPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_RoboBallBuddy.RoboBallGreenBuddyBody_prefab).WaitForCompletion().name, QuirkId.RoboBallMini_SolusBoostPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_RoboBallBuddy.RoboBallRedBuddyBody_prefab).WaitForCompletion().name, QuirkId.RoboBallMini_SolusBoostPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_WorkerUnit.WorkerUnitBody_prefab).WaitForCompletion().name, QuirkId.SolusProspector_PrimingPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidBarnacle.VoidBarnacleBody_prefab).WaitForCompletion().name, QuirkId.VoidBarnacle_VoidMortarPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidJailer.VoidJailerBody_prefab).WaitForCompletion().name, QuirkId.VoidJailer_GravityPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_VultureHunter.VultureHunterBody_prefab).WaitForCompletion().name, QuirkId.AlloyHunter_CritBoostPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ImpBoss.ImpBossBody_prefab).WaitForCompletion().name, QuirkId.ImpBoss_BleedPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_SolusAmalgamator.SolusAmalgamatorBody_prefab).WaitForCompletion().name, QuirkId.SolusAmalgamator_EquipmentBoostPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Titan.TitanBody_prefab).WaitForCompletion().name, QuirkId.StoneTitan_StoneSkinPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Titan.TitanGoldBody_prefab).WaitForCompletion().name, QuirkId.StoneTitan_StoneSkinPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Vagrant.VagrantBody_prefab).WaitForCompletion().name, QuirkId.Vagrant_OrbPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_MagmaWorm.MagmaWormBody_prefab).WaitForCompletion().name, QuirkId.MagmaWorm_BlazingAuraPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ElectricWorm.ElectricWormBody_prefab).WaitForCompletion().name, QuirkId.OverloadingWorm_LightningAuraPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_Child.ChildBody_prefab).WaitForCompletion().name, QuirkId.Child_EmergencyTeleportPassive);

            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Captain.CaptainBody_prefab).WaitForCompletion().name, QuirkId.Captain_MicrobotsPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Commando.CommandoBody_prefab).WaitForCompletion().name, QuirkId.Commando_DoubleTapPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Croco.CrocoBody_prefab).WaitForCompletion().name, QuirkId.Acrid_PoisonPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Loader.LoaderBody_prefab).WaitForCompletion().name, QuirkId.Loader_ScrapBarrierPassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_FalseSon.FalseSonBody_prefab).WaitForCompletion().name, QuirkId.FalseSon_StolenInheritancePassive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_Chef.ChefBody_prefab).WaitForCompletion().name, QuirkId.Chef_OilBurstPassive);

            // Actives (L1)
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Vulture.VultureBody_prefab).WaitForCompletion().name, QuirkId.Vulture_WindBlastActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_BeetleGuard.BeetleGuardBody_prefab).WaitForCompletion().name, QuirkId.BeetleGuard_SlamActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bison.BisonBody_prefab).WaitForCompletion().name, QuirkId.Bison_ChargeActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bell.BellBody_prefab).WaitForCompletion().name, QuirkId.Bell_SpikedBallControlActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_ClayGrenadier.ClayGrenadierBody_prefab).WaitForCompletion().name, QuirkId.ClayApothecary_ClayAirStrikeActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ClayBruiser.ClayBruiserBody_prefab).WaitForCompletion().name, QuirkId.ClayTemplar_ClayMinigunActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_LemurianBruiser.LemurianBruiserBody_prefab).WaitForCompletion().name, QuirkId.ElderLemurian_FireBlastActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_GreaterWisp.GreaterWispBody_prefab).WaitForCompletion().name, QuirkId.GreaterWisp_SpiritBoostActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Imp.ImpBody_prefab).WaitForCompletion().name, QuirkId.Imp_BlinkActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Jellyfish.JellyfishBody_prefab).WaitForCompletion().name, QuirkId.Jellyfish_RegenerateActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Lemurian.LemurianBody_prefab).WaitForCompletion().name, QuirkId.Lemurian_FireballActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_LunarGolem.LunarGolemBody_prefab).WaitForCompletion().name, QuirkId.LunarGolem_SlideResetActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_LunarWisp.LunarWispBody_prefab).WaitForCompletion().name, QuirkId.LunarWisp_MinigunActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Parent.ParentBody_prefab).WaitForCompletion().name, QuirkId.Parent_TeleportActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_MinePod.MinePodBody_prefab).WaitForCompletion().name, QuirkId.SolusDistributor_SolusPlantMineActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_ExtractorUnit.ExtractorUnitBody_prefab).WaitForCompletion().name, QuirkId.SolusExtractor_SolusExtractActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_Tanker.TankerBody_prefab).WaitForCompletion().name, QuirkId.SolusScorcher_SolusAccelerateActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Golem.GolemBody_prefab).WaitForCompletion().name, QuirkId.StoneGolem_LaserActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Nullifier.NullifierBody_prefab).WaitForCompletion().name, QuirkId.VoidReaver_PortalActive);

            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_BeetleQueen.BeetleQueen2Body_prefab).WaitForCompletion().name, QuirkId.BeetleQueen_SummonActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Gravekeeper.GravekeeperBody_prefab).WaitForCompletion().name, QuirkId.Grovetender_ChainActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ClayBoss.ClayBossBody_prefab).WaitForCompletion().name, QuirkId.ClayDunestrider_TarBoostActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Grandparent.GrandParentBody_prefab).WaitForCompletion().name, QuirkId.Grandparent_SunActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_RoboBallBoss.RoboBallBossBody_prefab).WaitForCompletion().name, QuirkId.SolusControlUnit_KnockupActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_RoboBallBoss.SuperRoboBallBossBody_prefab).WaitForCompletion().name, QuirkId.SolusControlUnit_KnockupActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.MegaConstructBody_prefab).WaitForCompletion().name, QuirkId.XIConstruct_BeamActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.MajorConstructBody_prefab).WaitForCompletion().name, QuirkId.XIConstruct_BeamActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidMegaCrab.VoidMegaCrabAllyBody_prefab).WaitForCompletion().name, QuirkId.VoidDevastator_MissilesActive);

            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bandit2.Bandit2Body_prefab).WaitForCompletion().name, QuirkId.Bandit_LightsOutActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_Drifter.DrifterBody_prefab).WaitForCompletion().name, QuirkId.Drifter_SalvageActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Engi.EngiBody_prefab).WaitForCompletion().name, QuirkId.Engineer_TurretActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Huntress.HuntressBody_prefab).WaitForCompletion().name, QuirkId.Huntress_FlurryActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Mage.MageBody_prefab).WaitForCompletion().name, QuirkId.Artificer_FlamethrowerActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Merc.MercBody_prefab).WaitForCompletion().name, QuirkId.Merc_DashActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Toolbot.ToolbotBody_prefab).WaitForCompletion().name, QuirkId.MULT_PowerStanceActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Treebot.TreebotBody_prefab).WaitForCompletion().name, QuirkId.REX_MortarActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Railgunner.RailgunnerBody_prefab).WaitForCompletion().name, QuirkId.Railgunner_CryoActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_Seeker.SeekerBody_prefab).WaitForCompletion().name, QuirkId.Seeker_MeditateActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidSurvivor.VoidSurvivorBody_prefab).WaitForCompletion().name, QuirkId.VoidFiend_CleanseActive);
            QuirkTargetingMap.Add(Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Shopkeeper.ShopkeeperBody_prefab).WaitForCompletion().name, QuirkId.Deku_OFAActive);

            // Collab
            QuirkTargetingMap.Add("DekuBody", QuirkId.Deku_OFAActive);
        }

       

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
