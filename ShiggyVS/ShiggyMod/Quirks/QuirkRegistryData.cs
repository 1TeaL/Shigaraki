using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using static ShiggyMod.Modules.Buffs;
using static ShiggyMod.Modules.Survivors.Shiggy;

namespace ShiggyMod.Modules.Quirks
{
    /// <summary>
    /// Single source-of-truth quirk declarations.
    /// Everything else (skill/buff reverse maps, targeting maps, display names) is derived from this list.
    /// </summary>
    public static class QuirkRegistryData
    {
        // ---------- Data model ----------
        public sealed class Entry
        {
            public QuirkId Id;
            public QuirkLevel Level;
            public QuirkCategory Category;

            // Deferred refs (safe to evaluate after your SkillDefs/Buffs exist)
            public Func<SkillDef> Skill;
            public Func<BuffDef> Buff;

            // Body targeting
            public string[] BodyNames; // raw body.name values
            public string[] BodyPaths; // Addressables prefab paths

            // Crafting
            public List<QuirkRecipe> Recipes;

            // Optional: display name override
            public string DisplayNameOverride;
        }

        private static Entry E(
            QuirkId id,
            QuirkLevel lvl,
            QuirkCategory cat,
            Func<SkillDef> skill = null,
            Func<BuffDef> buff = null,
            string[] bodyNames = null,
            string[] bodyPaths = null,
            IEnumerable<QuirkRecipe> recipes = null,
            string displayName = null)
        {
            return new Entry
            {
                Id = id,
                Level = lvl,
                Category = cat,
                Skill = skill,
                Buff = buff,
                BodyNames = bodyNames,
                BodyPaths = bodyPaths,
                Recipes = recipes != null ? recipes.ToList() : new List<QuirkRecipe>(),
                DisplayNameOverride = displayName
            };
        }

        // ---------- The master list ----------
        // This list is what you maintain going forward.
        public static readonly IReadOnlyList<Entry> All = Build().ToList();

        private static IEnumerable<Entry> Build()
        {
            // =========================
            // Utility / internal
            // =========================
            yield return E(QuirkId.Utility_EmptySkill, QuirkLevel.Level0, QuirkCategory.Utility, skill: () => emptySkillDef);
            yield return E(QuirkId.Utility_Choose, QuirkLevel.Level0, QuirkCategory.Utility, skill: () => chooseDef);
            yield return E(QuirkId.Utility_Remove, QuirkLevel.Level0, QuirkCategory.Utility, skill: () => removeDef);

            // =========================
            // Base Shiggy actives
            // =========================
            yield return E(QuirkId.Shiggy_DecayActive, QuirkLevel.Level0, QuirkCategory.Active, skill: () => decayDef);
            yield return E(QuirkId.Shiggy_AirCannonActive, QuirkLevel.Level0, QuirkCategory.Active, skill: () => aircannonDef);
            yield return E(QuirkId.Shiggy_BulletLaserActive, QuirkLevel.Level0, QuirkCategory.Active, skill: () => bulletlaserDef);
            yield return E(QuirkId.Shiggy_MultiplierActive, QuirkLevel.Level0, QuirkCategory.Active, skill: () => multiplierDef);

            // =========================
            // Elite affix passives
            // (no body targeting; obtained by steal/elite detect)
            // =========================
            yield return E(QuirkId.Elite_BlazingPassive, QuirkLevel.Level1, QuirkCategory.Passive, buff: () => RoR2Content.Buffs.AffixRed);
            yield return E(QuirkId.Elite_GlacialPassive, QuirkLevel.Level1, QuirkCategory.Passive, buff: () => RoR2Content.Buffs.AffixWhite);
            yield return E(QuirkId.Elite_MalachitePassive, QuirkLevel.Level1, QuirkCategory.Passive, buff: () => RoR2Content.Buffs.AffixPoison);
            yield return E(QuirkId.Elite_CelestinePassive, QuirkLevel.Level1, QuirkCategory.Passive, buff: () => RoR2Content.Buffs.AffixHaunted);
            yield return E(QuirkId.Elite_OverloadingPassive, QuirkLevel.Level1, QuirkCategory.Passive, buff: () => RoR2Content.Buffs.AffixBlue);
            yield return E(QuirkId.Elite_LunarPassive, QuirkLevel.Level1, QuirkCategory.Passive, buff: () => RoR2Content.Buffs.AffixLunar);
            yield return E(QuirkId.Elite_MendingPassive, QuirkLevel.Level1, QuirkCategory.Passive, buff: () => DLC1Content.Buffs.EliteEarth);
            yield return E(QuirkId.Elite_VoidPassive, QuirkLevel.Level1, QuirkCategory.Passive, buff: () => DLC1Content.Buffs.EliteVoid);
            yield return E(QuirkId.Elite_GildedPassive, QuirkLevel.Level1, QuirkCategory.Passive, buff: () => DLC2Content.Buffs.EliteAurelionite);
            yield return E(QuirkId.Elite_TwistedPassive, QuirkLevel.Level1, QuirkCategory.Passive, buff: () => DLC2Content.Buffs.EliteBead);

            // =========================
            // P1 Level 1 passives (skill + buff + body targeting)
            // =========================
            yield return E(
                QuirkId.AlphaConstruct_BarrierPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => alphaconstructpassiveDef,
                buff: () => alphashieldonBuff,
                bodyPaths: new[]
                {
                    RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.MinorConstructBody_prefab,
                    RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.MinorConstructOnKillBody_prefab,
                    RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.MinorConstructAttachableBody_prefab
                });

            yield return E(
                QuirkId.Beetle_StrengthPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => beetlepassiveDef,
                buff: () => beetleBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Beetle.BeetleBody_prefab });

            yield return E(
                QuirkId.Pest_JumpPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => pestpassiveDef,
                buff: () => pestjumpBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_FlyingVermin.FlyingVerminBody_prefab });

            yield return E(
                QuirkId.Vermin_SpeedPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => verminpassiveDef,
                buff: () => verminsprintBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Vermin.VerminBody_prefab });

            yield return E(
                QuirkId.Gup_SpikyBodyPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => guppassiveDef,
                buff: () => gupspikeBuff,
                bodyPaths: new[]
                {
                    RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Gup.GupBody_prefab,
                    RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Gup.GipBody_prefab,
                    RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Gup.GeepBody_prefab
                });

            yield return E(
                QuirkId.HermitCrab_MortarPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => hermitcrabpassiveDef,
                buff: () => hermitcrabmortarBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_HermitCrab.HermitCrabBody_prefab });

            yield return E(
                QuirkId.Larva_AcidJumpPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => larvapassiveDef,
                buff: () => larvajumpBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_AcidLarva.AcidLarvaBody_prefab });

            yield return E(
                QuirkId.LesserWisp_HastePassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => lesserwisppassiveDef,
                buff: () => lesserwispBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Wisp.WispBody_prefab });

            yield return E(
                QuirkId.LunarExploder_LunarBarrierPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => lunarexploderpassiveDef,
                buff: () => lunarexploderBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_LunarExploder.LunarExploderBody_prefab });

            yield return E(
                QuirkId.MiniMushrum_HealingAuraPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => minimushrumpassiveDef,
                buff: () => minimushrumBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_MiniMushroom.MiniMushroomBody_prefab });

            yield return E(
                QuirkId.RoboBallMini_SolusBoostPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => roboballminibpassiveDef,
                buff: () => roboballminiBuff,
                bodyPaths: new[]
                {
                    RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_RoboBallBoss.RoboBallMiniBody_prefab,
                    RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_RoboBallBuddy.RoboBallGreenBuddyBody_prefab,
                    RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_RoboBallBuddy.RoboBallRedBuddyBody_prefab
                });

            yield return E(
                QuirkId.SolusProspector_PrimingPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => solusprospectorprimingDef, // you map this as a passive quirk; if you also have a SkillDef, add it here
                buff: () => solusPrimedBuff,
                bodyPaths: new[]
                {
                    RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_WorkerUnit.WorkerUnitBody_prefab
                });

            yield return E(
                QuirkId.VoidBarnacle_VoidMortarPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => voidbarnaclepassiveDef,
                buff: () => voidbarnaclemortarBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidBarnacle.VoidBarnacleBody_prefab });

            yield return E(
                QuirkId.VoidJailer_GravityPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => voidjailerpassiveDef,
                buff: () => voidjailerBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidJailer.VoidJailerBody_prefab });

            yield return E(
                QuirkId.ImpBoss_BleedPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => impbosspassiveDef,
                buff: () => impbossBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ImpBoss.ImpBossBody_prefab });

            yield return E(
                QuirkId.AlloyHunter_CritBoostPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => alloyhuntercritboostDef,
                buff: () => alloyhunterCritBoostBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_VultureHunter.VultureHunterBody_prefab });

            yield return E(
                QuirkId.SolusAmalgamator_EquipmentBoostPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => solusamalgamatorequipmentboostDef,
                buff: () => solusamalgamatorEquipmentBoostBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_SolusAmalgamator.SolusAmalgamatorBody_prefab });

            yield return E(
                QuirkId.StoneTitan_StoneSkinPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => stonetitanpassiveDef,
                buff: () => stonetitanBuff,
                bodyPaths: new[]
                {
                    RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Titan.TitanBody_prefab,
                    RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Titan.TitanGoldBody_prefab
                });

            yield return E(
                QuirkId.MagmaWorm_BlazingAuraPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => magmawormpassiveDef,
                buff: () => magmawormBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_MagmaWorm.MagmaWormBody_prefab });

            yield return E(
                QuirkId.OverloadingWorm_LightningAuraPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => overloadingwormpassiveDef,
                buff: () => overloadingwormBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ElectricWorm.ElectricWormBody_prefab });

            yield return E(
                QuirkId.Vagrant_OrbPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => vagrantpassiveDef,
                buff: () => vagrantBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Vagrant.VagrantBody_prefab });

            yield return E(
                QuirkId.Child_EmergencyTeleportPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => childpassiveDef,
                buff: () => childBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_Child.ChildBody_prefab });

            // Survivor passives (body targeting included in your map)
            yield return E(
                QuirkId.Captain_MicrobotsPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => captainpassiveDef,
                buff: () => captainBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Captain.CaptainBody_prefab });

            yield return E(
                QuirkId.Commando_DoubleTapPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => commandopassiveDef,
                buff: () => commandoBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Commando.CommandoBody_prefab });

            yield return E(
                QuirkId.Acrid_PoisonPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => acridpassiveDef,
                buff: () => acridBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Croco.CrocoBody_prefab });

            yield return E(
                QuirkId.Loader_ScrapBarrierPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => loaderpassiveDef,
                buff: () => loaderBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Loader.LoaderBody_prefab });

            yield return E(
                QuirkId.FalseSon_StolenInheritancePassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => falsesonpassiveDef,
                buff: () => falsesonStolenInheritanceBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_FalseSon.FalseSonBody_prefab });

            yield return E(
                QuirkId.Chef_OilBurstPassive, QuirkLevel.Level1, QuirkCategory.Passive,
                skill: () => chefpassiveDef,
                buff: () => chefOilBurstBuff,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_Chef.ChefBody_prefab });

            // =========================
            // P2 Crafted passives — Level 2
            // =========================
            yield return E(
                QuirkId.Pest_Vermin_BlindSensesPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                skill: () => blindSensesPassiveDef,
                buff: () => blindSensesBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.Pest_JumpPassive, QuirkId.Vermin_SpeedPassive) });

            yield return E(
                QuirkId.BeetleQueen_Scavenger_GachaPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                skill: () => gachaPassiveDef,
                buff: () => gachaBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.BeetleQueen_SummonActive, QuirkId.Scavenger_ThqwibActive) });

            yield return E(
                QuirkId.Pest_Vermin_BlindSensesPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                skill: () => blindSensesPassiveDef,
                buff: () => blindSensesBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.Pest_JumpPassive, QuirkId.Vermin_SpeedPassive) });

            yield return E(
                QuirkId.RoboBallMini_Commando_DoubleTimePassive, QuirkLevel.Level2, QuirkCategory.Passive,
                skill: () => doubleTimePassiveDef,
                buff: () => doubleTimeBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.RoboBallMini_SolusBoostPassive, QuirkId.Commando_DoubleTapPassive) });

            yield return E(
                QuirkId.Grandparent_Artificer_ElementalFusionPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                skill: () => elementalFusionPassiveDef,
                buff: () => elementalFusionBuffStacks,
                recipes: new[] { new QuirkRecipe(QuirkId.Grandparent_SunActive, QuirkId.Artificer_FlamethrowerActive) });

            yield return E(
                QuirkId.LesserWisp_Beetle_OmniboostPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                skill: () => omniboostPassiveDef,
                buff: () => omniboostBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.LesserWisp_HastePassive, QuirkId.Beetle_StrengthPassive) });

            yield return E(
                QuirkId.ClayDunestrider_MiniMushrum_IngrainPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                skill: () => ingrainPassiveDef,
                buff: () => ingrainBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.ClayDunestrider_TarBoostActive, QuirkId.MiniMushrum_HealingAuraPassive) });

            yield return E(
                QuirkId.Bell_Gup_BarbedSpikesPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                skill: () => barbedSpikesPassiveDef,
                buff: () => barbedSpikesBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.Bell_SpikedBallControlActive, QuirkId.Gup_SpikyBodyPassive) });

            yield return E(
                QuirkId.Acrid_Larva_AuraOfBlightPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                skill: () => auraOfBlightPassiveDef,
                buff: () => auraOfBlightBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.Acrid_PoisonPassive, QuirkId.Larva_AcidJumpPassive) });

            yield return E(
                QuirkId.HermitCrab_Titan_StoneFormPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                skill: () => stoneFormPassiveDef,
                buff: () => stoneFormBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.HermitCrab_MortarPassive, QuirkId.StoneTitan_StoneSkinPassive) });

            yield return E(
                QuirkId.Vagrant_ClayTemplar_BigBangPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                skill: () => bigBangPassiveDef,
                buff: () => bigbangBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.Vagrant_OrbPassive, QuirkId.ClayTemplar_ClayMinigunActive) });

            yield return E(
                QuirkId.Wisp_Grovetender_WisperPassive, QuirkLevel.Level2, QuirkCategory.Passive,
                skill: () => wisperPassiveDef,
                buff: () => wisperBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.LesserWisp_HastePassive, QuirkId.Grovetender_ChainActive) });


            // =========================
            // P4 Crafted passives — Level 4
            // =========================
            yield return E(
                QuirkId.ShiggyDecay_AuraOfBlight_DecayAwakenedPassive, QuirkLevel.Level4, QuirkCategory.Passive,
                skill: () => decayAwakenedPassiveDef,
                buff: () => decayAwakenedBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.Shiggy_DecayActive, QuirkId.Acrid_Larva_AuraOfBlightPassive) });

            yield return E(
                QuirkId.Jailer_Grandparent_ElementalFusion_WeatherReportPassive, QuirkLevel.Level4, QuirkCategory.Passive,
                skill: () => weatherReportPassiveDef,
                buff: () => weatherReportBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.VoidJailer_GravityPassive, QuirkId.Grandparent_Artificer_ElementalFusionPassive) });

            yield return E(
                QuirkId.Ingrain_StoneForm_GargoyleProtectionPassive, QuirkLevel.Level4, QuirkCategory.Passive,
                skill: () => gargoyleProtectionPassiveDef,
                buff: () => gargoyleProtectionBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.ClayDunestrider_MiniMushrum_IngrainPassive, QuirkId.HermitCrab_Titan_StoneFormPassive) });

            yield return E(
                QuirkId.BlackholeGlaive_MechStance_MachineFormPassive, QuirkLevel.Level4, QuirkCategory.Passive,
                skill: () => machineFormPassiveDef,
                buff: () => machineFormBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.VoidDevastator_Huntress_BlackholeGlaiveActive, QuirkId.BeetleGuard_MULT_MechStanceActive) });

            yield return E(
                QuirkId.BarrierJelly_BlindSenses_ReversalPassive, QuirkLevel.Level4, QuirkCategory.Passive,
                skill: () => reversalPassiveDef,
                buff: () => reversalBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.AlphaConstruct_Jellyfish_BarrierJellyActive, QuirkId.Pest_Vermin_BlindSensesPassive) });

            yield return E(
                QuirkId.BigBang_Wisper_SupernovaPassive, QuirkLevel.Level4, QuirkCategory.Passive,
                skill: () => supernovaPassiveDef,
                buff: () => supernovaBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.Vagrant_ClayTemplar_BigBangPassive, QuirkId.Wisp_Grovetender_WisperPassive) });

            yield return E(
                QuirkId.MiniMushrum_Jellyfish_FalseSon_Seeker_HyperRegenerationPassive, QuirkLevel.Level4, QuirkCategory.Passive,
                skill: () => hyperRegenerationPassiveDef,
                buff: () => hyperRegenerationBuff,
                recipes: new[] { new QuirkRecipe(QuirkId.MiniMushrum_HealingAuraPassive, QuirkId.Jellyfish_RegenerateActive, QuirkId.FalseSon_StolenInheritancePassive, QuirkId.Seeker_MeditateActive) });


            // =========================
            // A1 Level 1 actives (skill + body targeting)
            // =========================
            yield return E(QuirkId.Vulture_WindBlastActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => alloyvultureWindBlastDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Vulture.VultureBody_prefab });

            yield return E(QuirkId.BeetleGuard_SlamActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => beetleguardslamDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_BeetleGuard.BeetleGuardBody_prefab });

            yield return E(QuirkId.Bison_ChargeActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => bisonchargeDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bison.BisonBody_prefab });

            yield return E(QuirkId.Bell_SpikedBallControlActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => bronzongballDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bell.BellBody_prefab });

            yield return E(QuirkId.ClayApothecary_ClayAirStrikeActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => clayapothecarymortarDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_ClayGrenadier.ClayGrenadierBody_prefab });

            yield return E(QuirkId.ClayTemplar_ClayMinigunActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => claytemplarminigunDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ClayBruiser.ClayBruiserBody_prefab });

            yield return E(QuirkId.ElderLemurian_FireBlastActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => elderlemurianfireblastDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_LemurianBruiser.LemurianBruiserBody_prefab });

            yield return E(QuirkId.GreaterWisp_SpiritBoostActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => greaterWispBuffDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_GreaterWisp.GreaterWispBody_prefab });

            yield return E(QuirkId.Imp_BlinkActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => impblinkDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Imp.ImpBody_prefab });

            yield return E(QuirkId.Jellyfish_RegenerateActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => jellyfishregenerateDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Jellyfish.JellyfishBody_prefab });

            yield return E(QuirkId.Lemurian_FireballActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => lemurianfireballDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Lemurian.LemurianBody_prefab });

            yield return E(QuirkId.LunarGolem_SlideResetActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => lunargolemSlideDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_LunarGolem.LunarGolemBody_prefab });

            yield return E(QuirkId.LunarWisp_MinigunActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => lunarwispminigunDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_LunarWisp.LunarWispBody_prefab });

            yield return E(QuirkId.Parent_TeleportActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => parentteleportDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Parent.ParentBody_prefab });

            yield return E(QuirkId.ScorchWorm_LavaBombActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => scorchwormlavabombDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC2_Scorchling.ScorchlingBody_prefab});

            yield return E(QuirkId.SolusDistributor_SolusPlantMineActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => solusdistributorplantmineDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_MinePod.MinePodBody_prefab });

            yield return E(QuirkId.SolusExtractor_SolusExtractActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => solusextractorextractDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_ExtractorUnit.ExtractorUnitBody_prefab });

            yield return E(QuirkId.SolusInvalidator_SolusInvalidateActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => solusinvalidatorinvalidateDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_DefectiveUnit.DefectiveUnitBody_prefab });

            yield return E(QuirkId.SolusScorcher_SolusAccelerateActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => solusscorcheraccelerateDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_Tanker.TankerBody_prefab });

            yield return E(QuirkId.SolusTransporter_SolusTransportActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => solustransportertransportDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_IronHauler.IronHaulerBody_prefab });

            yield return E(QuirkId.StoneGolem_LaserActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => stonegolemlaserDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Golem.GolemBody_prefab });

            yield return E(QuirkId.VoidReaver_PortalActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => voidreaverportalDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Nullifier.NullifierBody_prefab });

            // Boss/other actives you mapped (body paths exist in your targeting paste)
            yield return E(QuirkId.BeetleQueen_SummonActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => beetlequeenSummonDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_BeetleQueen.BeetleQueen2Body_prefab });

            yield return E(QuirkId.Grovetender_ChainActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => grovetenderChainDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Gravekeeper.GravekeeperBody_prefab });

            yield return E(QuirkId.ClayDunestrider_TarBoostActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => claydunestriderbuffDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ClayBoss.ClayBossBody_prefab });

            yield return E(QuirkId.Grandparent_SunActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => grandparentsunDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Grandparent.GrandParentBody_prefab });

            yield return E(QuirkId.Scavenger_ThqwibActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => scavengerthqwibDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Scav.ScavBody_prefab});

            yield return E(QuirkId.SolusControlUnit_AntiGravityActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => soluscontrolunityknockupDef,
                bodyPaths: new[]
                {
                    RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_RoboBallBoss.RoboBallBossBody_prefab,
                    RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_RoboBallBoss.SuperRoboBallBossBody_prefab
                });

            yield return E(QuirkId.XIConstruct_BeamActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => xiconstructbeamDef,
                bodyPaths: new[]
                {
                    RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.MegaConstructBody_prefab,
                    RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.MajorConstructBody_prefab
                });

            yield return E(QuirkId.VoidDevastator_MissilesActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => voiddevastatorhomingDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidMegaCrab.VoidMegaCrabAllyBody_prefab });

            // Survivor/collab actives from your skill map + targeting
            yield return E(QuirkId.Bandit_LightsOutActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => banditlightsoutDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bandit2.Bandit2Body_prefab });

            yield return E(QuirkId.Drifter_SalvageActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => driftersalvageDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_Drifter.DrifterBody_prefab });

            yield return E(QuirkId.Engineer_TurretActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => engiturretDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Engi.EngiBody_prefab });

            yield return E(QuirkId.Huntress_FlurryActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => huntressattackDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Huntress.HuntressBody_prefab });

            yield return E(QuirkId.Artificer_FlamethrowerActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => artificerflamethrowerDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Mage.MageBody_prefab });

            yield return E(QuirkId.Merc_WindAssaultActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => mercdashDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Merc.MercBody_prefab });

            yield return E(QuirkId.MULT_PowerStanceActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => multbuffDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Toolbot.ToolbotBody_prefab });

            yield return E(QuirkId.Operator_S141CustomActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => operators141customDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_Drone_Tech.DroneTechBody_prefab});

            yield return E(QuirkId.Railgunner_CryoActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => railgunnercryoDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Railgunner.RailgunnerBody_prefab });

            yield return E(QuirkId.REX_MortarActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => rexmortarDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Treebot.TreebotBody_prefab });

            yield return E(QuirkId.Seeker_MeditateActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => seekermeditateDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_Seeker.SeekerBody_prefab });

            yield return E(QuirkId.VoidFiend_CleanseActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => voidfiendcleanseDef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidSurvivor.VoidSurvivorBody_prefab });

            // Collab Deku (you add by name too)
            yield return E(QuirkId.Deku_OFAActive, QuirkLevel.Level1, QuirkCategory.Active,
                skill: () => DekuOFADef,
                bodyPaths: new[] { RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Shopkeeper.ShopkeeperBody_prefab },
                bodyNames: new[] { "DekuBody" });

            // =========================
            // A2 Crafted: Level 2 actives
            // =========================
            yield return E(
                QuirkId.Railgunner_LunarWisp_RapidPierceActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => rapidPierceDef,
                recipes: new[] { new QuirkRecipe(QuirkId.Railgunner_CryoActive, QuirkId.LunarWisp_MinigunActive) });

            yield return E(
                QuirkId.BulletLaser_StoneGolem_SweepingBeamActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => sweepingBeamDef,
                recipes: new[] { new QuirkRecipe(QuirkId.Shiggy_BulletLaserActive, QuirkId.StoneGolem_LaserActive) });

            yield return E(
                QuirkId.VoidDevastator_Huntress_BlackholeGlaiveActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => blackholeGlaiveDef,
                recipes: new[] { new QuirkRecipe(QuirkId.VoidDevastator_MissilesActive, QuirkId.Huntress_FlurryActive) });

            yield return E(
                QuirkId.VoidJailer_SolusControlUnit_GravitationalDownforceActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => gravitationalDownforceDef,
                recipes: new[] { new QuirkRecipe(QuirkId.VoidJailer_GravityPassive, QuirkId.SolusControlUnit_AntiGravityActive) });

            yield return E(
                QuirkId.Vulture_Engineer_WindShieldActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => windShieldDef,
                recipes: new[] { new QuirkRecipe(QuirkId.Vulture_WindBlastActive, QuirkId.Engineer_TurretActive) });

            yield return E(
                QuirkId.XIConstruct_ClayApothecary_GenesisActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => genesisDef,
                recipes: new[] { new QuirkRecipe(QuirkId.XIConstruct_BeamActive, QuirkId.ClayApothecary_ClayAirStrikeActive) });

            yield return E(
                QuirkId.LunarExploder_LunarGolem_RefreshActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => refreshDef,
                recipes: new[] { new QuirkRecipe(QuirkId.LunarExploder_LunarBarrierPassive, QuirkId.LunarGolem_SlideResetActive) });

            yield return E(
                QuirkId.Imp_MagmaWorm_ExpungeActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => expungeDef,
                recipes: new[] { new QuirkRecipe(QuirkId.Imp_BlinkActive, QuirkId.MagmaWorm_BlazingAuraPassive) });

            yield return E(
                QuirkId.Imp_Bandit_ShadowClawActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => shadowClawDef,
                recipes: new[] { new QuirkRecipe(QuirkId.Imp_BlinkActive, QuirkId.Bandit_LightsOutActive) });

            yield return E(
                QuirkId.Captain_VoidReaver_OrbitalStrikeActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => orbitalStrikeDef,
                recipes: new[] { new QuirkRecipe(QuirkId.Captain_MicrobotsPassive, QuirkId.VoidReaver_PortalActive) });

            yield return E(
                QuirkId.OverloadingWorm_Bison_ThunderclapActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => thunderclapDef,
                recipes: new[] { new QuirkRecipe(QuirkId.OverloadingWorm_LightningAuraPassive, QuirkId.Bison_ChargeActive) });

            yield return E(
                QuirkId.ElderLemurian_Lemurian_BlastBurnActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => blastBurnDef,
                recipes: new[] { new QuirkRecipe(QuirkId.ElderLemurian_FireBlastActive, QuirkId.Lemurian_FireballActive) });

            yield return E(
                QuirkId.AlphaConstruct_Jellyfish_BarrierJellyActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => barrierJellyDef,
                recipes: new[] { new QuirkRecipe(QuirkId.AlphaConstruct_BarrierPassive, QuirkId.Jellyfish_RegenerateActive) });

            yield return E(
                QuirkId.BeetleGuard_MULT_MechStanceActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => mechStanceDef,
                recipes: new[] { new QuirkRecipe(QuirkId.BeetleGuard_SlamActive, QuirkId.MULT_PowerStanceActive) });

            yield return E(
                QuirkId.AirCannon_Merc_WindSlashActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => windSlashDef,
                recipes: new[] { new QuirkRecipe(QuirkId.Shiggy_AirCannonActive, QuirkId.Merc_WindAssaultActive) });

            yield return E(
                QuirkId.Multiplier_Deku_LimitBreakActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => limitBreakDef,
                recipes: new[] { new QuirkRecipe(QuirkId.Shiggy_MultiplierActive, QuirkId.Deku_OFAActive) });

            yield return E(
                QuirkId.VoidBarnacle_VoidFiend_VoidFormActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => voidFormDef,
                recipes: new[] { new QuirkRecipe(QuirkId.VoidBarnacle_VoidMortarPassive, QuirkId.VoidFiend_CleanseActive) });

            yield return E(
                QuirkId.REX_Decay_DecayPlusUltraActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => decayPlusUltraDef,
                recipes: new[] { new QuirkRecipe(QuirkId.REX_MortarActive, QuirkId.Shiggy_DecayActive) });

            yield return E(
                QuirkId.Parent_Loader_MachPunchActive, QuirkLevel.Level2, QuirkCategory.Active,
                skill: () => machPunchDef,
                recipes: new[] { new QuirkRecipe(QuirkId.Parent_TeleportActive, QuirkId.Loader_ScrapBarrierPassive) });

            // =========================
            // A4 Crafted: Level 4 actives
            // =========================
            yield return E(
                QuirkId.ShadowClaw_Genesis_LightAndDarknessActive, QuirkLevel.Level4, QuirkCategory.Active,
                skill: () => lightAndDarknessDef,
                recipes: new[] { new QuirkRecipe(QuirkId.Imp_Bandit_ShadowClawActive, QuirkId.XIConstruct_ClayApothecary_GenesisActive) });

            yield return E(
                QuirkId.Refresh_Gacha_WildCardActive, QuirkLevel.Level4, QuirkCategory.Active,
                skill: () => wildCardDef,
                recipes: new[] { new QuirkRecipe(QuirkId.LunarExploder_LunarGolem_RefreshActive, QuirkId.BeetleQueen_Scavenger_GachaPassive) });

            yield return E(
                QuirkId.OrbitalStrike_BlastBurn_BlastingZoneActive, QuirkLevel.Level4, QuirkCategory.Active,
                skill: () => blastingZoneDef,
                recipes: new[] { new QuirkRecipe(QuirkId.Captain_VoidReaver_OrbitalStrikeActive, QuirkId.ElderLemurian_Lemurian_BlastBurnActive) });

            yield return E(
                QuirkId.WindShield_WindSlash_FinalReleaseActive, QuirkLevel.Level4, QuirkCategory.Active,
                skill: () => finalReleaseDef,
                recipes: new[] { new QuirkRecipe(QuirkId.Vulture_Engineer_WindShieldActive, QuirkId.AirCannon_Merc_WindSlashActive) });

            yield return E(
                QuirkId.RapidPierce_SweepingBeam_XBeamerActive, QuirkLevel.Level4, QuirkCategory.Active,
                skill: () => xBeamerDef,
                recipes: new[] { new QuirkRecipe(QuirkId.Railgunner_LunarWisp_RapidPierceActive, QuirkId.BulletLaser_StoneGolem_SweepingBeamActive) });

            yield return E(
                QuirkId.VoidForm_LimitBreak_OFAFOActive, QuirkLevel.Level4, QuirkCategory.Active,
                skill: () => OFAFODef,
                recipes: new[] { new QuirkRecipe(QuirkId.VoidBarnacle_VoidFiend_VoidFormActive, QuirkId.Multiplier_Deku_LimitBreakActive) });

            yield return E(
                QuirkId.BarbedSpikes_Expunge_DeathAuraActive, QuirkLevel.Level4, QuirkCategory.Active,
                skill: () => deathAuraDef,
                recipes: new[] { new QuirkRecipe(QuirkId.Bell_Gup_BarbedSpikesPassive, QuirkId.Imp_MagmaWorm_ExpungeActive) });

            yield return E(
                QuirkId.MachPunch_Thunderclap_ExtremeSpeedActive, QuirkLevel.Level4, QuirkCategory.Active,
                skill: () => extremeSpeedDef,
                recipes: new[] { new QuirkRecipe(QuirkId.Parent_Loader_MachPunchActive, QuirkId.OverloadingWorm_Bison_ThunderclapActive) });

            yield return E(
                QuirkId.DoubleTime_Omniboost_TheWorldActive, QuirkLevel.Level4, QuirkCategory.Active,
                skill: () => theWorldDef,
                recipes: new[] { new QuirkRecipe(QuirkId.RoboBallMini_Commando_DoubleTimePassive, QuirkId.LesserWisp_Beetle_OmniboostPassive) });



            // =========================
            // A6 Crafted: Level 6 actives
            // =========================
            yield return E(
                QuirkId.Solusx6_SolusFactorUnleashedActive, QuirkLevel.Level6, QuirkCategory.Active,
                skill: () => solusfactorunleashedDef,
                recipes: new[]
                {
                    new QuirkRecipe(
                        QuirkId.SolusDistributor_SolusPlantMineActive,
                        QuirkId.SolusExtractor_SolusExtractActive,
                        QuirkId.SolusInvalidator_SolusInvalidateActive,
                        QuirkId.SolusProspector_PrimingPassive,
                        QuirkId.SolusScorcher_SolusAccelerateActive,
                        QuirkId.SolusTransporter_SolusTransportActive
                    )
                });


        }
    }
}
