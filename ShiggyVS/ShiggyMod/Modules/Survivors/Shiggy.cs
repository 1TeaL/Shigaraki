using BepInEx.Configuration;
using EntityStates;
using EntityStates.Railgunner.Scope;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShiggyMod.Modules.Survivors
{


    internal class Shiggy : SurvivorBase
    {
        internal override string bodyName { get; set; } = "Shiggy";

        //monster passives
        internal static SkillDef alphacontructpassiveDef;
        internal static SkillDef beetlepassiveDef;
        internal static SkillDef pestpassiveDef;
        internal static SkillDef verminpassiveDef;
        internal static SkillDef guppassiveDef;
        internal static SkillDef hermitcrabpassiveDef;
        internal static SkillDef larvapassiveDef;
        internal static SkillDef lesserwisppassiveDef;
        internal static SkillDef lunarexploderpassiveDef;
        internal static SkillDef minimushrumpassiveDef;
        internal static SkillDef roboballminibpassiveDef;
        internal static SkillDef voidbarnaclepassiveDef;
        internal static SkillDef voidjailerpassiveDef;


        //boss monster passives
        internal static SkillDef impbosspassiveDef;
        internal static SkillDef stonetitanpassiveDef;
        internal static SkillDef magmawormpassiveDef;
        internal static SkillDef overloadingwormpassiveDef;
        internal static SkillDef vagrantpassiveDef;


        //survivor passives
        internal static SkillDef acridpassiveDef;
        internal static SkillDef commandopassiveDef;
        internal static SkillDef captainpassiveDef;
        internal static SkillDef loaderpassiveDef;


        //monster actives
        internal static SkillDef alloyvultureWindBlastDef;
        internal static SkillDef beetleguardslamDef;
        internal static SkillDef bisonchargeDef;
        internal static SkillDef bronzongballDef;
        internal static SkillDef clayapothecarymortarDef;
        internal static SkillDef claytemplarminigunDef;
        internal static SkillDef elderlemurianfireblastDef;
        internal static SkillDef greaterwispballDef;
        internal static SkillDef impblinkDef;
        internal static SkillDef jellyfishHealDef;
        internal static SkillDef lemurianfireballDef;
        internal static SkillDef lunargolemSlideDef;
        internal static SkillDef lunarwispminigunDef;
        internal static SkillDef parentteleportDef;
        internal static SkillDef stonegolemlaserDef;
        internal static SkillDef voidreaverportalDef;


        //boss monster actives
        internal static SkillDef beetlequeenSummonDef;
        internal static SkillDef grandparentsunDef;
        internal static SkillDef grovetenderChainDef;
        internal static SkillDef claydunestriderbuffDef;
        internal static SkillDef soluscontrolunityknockupDef;
        internal static SkillDef xiconstructbeamDef;
        internal static SkillDef voiddevastatorhomingDef;
        internal static SkillDef scavengerthqwibDef;


        //survivor actives
        internal static SkillDef artificerflamethrowerDef;
        internal static SkillDef artificericewallDef;
        internal static SkillDef artificerlightningorbDef;
        internal static SkillDef banditlightsoutDef;
        internal static SkillDef engiturretDef;
        internal static SkillDef huntressattackDef;
        internal static SkillDef mercdashDef;
        internal static SkillDef multbuffDef;
        internal static SkillDef multbuffcancelDef;
        internal static SkillDef railgunnercryoDef;
        internal static SkillDef rexmortarDef;
        internal static SkillDef voidfiendcleanseDef;

        internal static SkillDef DekuOFADef;

        //shiggy skills
        internal static SkillDef decayDef;
        internal static SkillDef aircannonDef;
        internal static SkillDef bulletlaserDef;
        internal static SkillDef multiplierDef;
        internal static SkillDef chooseDef;
        internal static SkillDef removeDef;
        internal static SkillDef emptySkillDef;


        //synergy actives
        internal static SkillDef sweepingBeamDef;
        internal static SkillDef blackholeGlaiveDef;
        internal static SkillDef gravitationalDownforceDef;
        internal static SkillDef windShieldDef;
        internal static SkillDef genesisDef;
        internal static SkillDef refreshDef;
        internal static SkillDef expungeDef;
        internal static SkillDef shadowClawDef;
        internal static SkillDef orbitalStrikeDef;
        internal static SkillDef thunderclapDef;
        internal static SkillDef blastBurnDef;
        internal static SkillDef barrierJellyDef;
        internal static SkillDef mechStanceDef;
        internal static SkillDef windSlashDef;
        internal static SkillDef limitBreakDef;

        //synergy passive
        internal static SkillDef bigBangPassiveDef;
        internal static SkillDef wisperPassiveDef;
        internal static SkillDef omniboostPassiveDef;
        internal static SkillDef gachaPassiveDef;
        internal static SkillDef stoneFormPassiveDef;
        internal static SkillDef auraOfBlightPassiveDef;
        internal static SkillDef barbedSpikesPassiveDef;
        internal static SkillDef ingrainPassiveDef;


        internal override GameObject bodyPrefab { get; set; }
        internal override GameObject displayPrefab { get; set; }

        internal override float sortPosition { get; set; } = 100f;

        internal override ConfigEntry<bool> characterEnabled { get; set; }

        internal override BodyInfo bodyInfo { get; set; } = new BodyInfo
        {
            armor = 10f,
            armorGrowth = 0.5f,
            bodyName = "ShiggyBody",
            bodyNameToken = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME",
            bodyColor = Color.magenta,
            characterPortrait = Modules.Assets.LoadCharacterIcon("Shiggy"),
            crosshair = Modules.Assets.LoadCrosshair("Standard"),
            damage = 5f,
            healthGrowth = 41f,
            healthRegen = 1f,
            jumpCount = 2,
            maxHealth = 141f,
            moveSpeed = 7f,
            subtitleNameToken = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_SUBTITLE",
            //podPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/SurvivorPod")
        };

        internal static Material ShiggyMat = Modules.Assets.CreateMaterial("ShiggyMat");
        internal override int mainRendererIndex { get; set; } = 1;

        internal override CustomRendererInfo[] customRendererInfos { get; set; } = new CustomRendererInfo[] {

                new CustomRendererInfo
                {
                    childName = "Hand",
                    material = ShiggyMat,
                    ignoreOverlays = true
                },
                new CustomRendererInfo
                {
                    childName = "Model",
                    material = ShiggyMat
                },
        };



        internal override Type characterMainState { get; set; } = typeof(EntityStates.GenericCharacterMain);

        //item display stuffs
        internal override ItemDisplayRuleSet itemDisplayRuleSet { get; set; }
        internal override List<ItemDisplayRuleSet.KeyAssetRuleGroup> itemDisplayRules { get; set; }

        internal override UnlockableDef characterUnlockableDef { get; set; }
        private static UnlockableDef masterySkinUnlockableDef;

        internal override void InitializeCharacter()
        {
            base.InitializeCharacter();
            bodyPrefab.AddComponent<ShiggyController>();
        }

        internal override void InitializeUnlockables()
        {
            masterySkinUnlockableDef = Modules.Unlockables.AddUnlockable<Achievements.MasteryAchievement>(true);
        }

        internal override void InitializeDoppelganger()
        {
            base.InitializeDoppelganger();
        }



        internal override void InitializeHitboxes()
        {
            ChildLocator childLocator = bodyPrefab.GetComponentInChildren<ChildLocator>();
            GameObject model = childLocator.gameObject;

            Transform hitboxTransform = childLocator.FindChild("SmallHitbox");
            Modules.Prefabs.SetupHitbox(model, hitboxTransform, "SmallHitbox");

            Transform hitboxTransform2 = childLocator.FindChild("DetectSmallHitbox");
            Modules.Prefabs.SetupHitbox(model, hitboxTransform2, "DetectSmallHitbox");

            Transform hitboxTransform3 = childLocator.FindChild("FrontHitbox");
            Modules.Prefabs.SetupHitbox(model, hitboxTransform3, "FrontHitbox");

            Transform hitboxTransform4 = childLocator.FindChild("AroundHitbox");
            Modules.Prefabs.SetupHitbox(model, hitboxTransform4, "AroundHitbox");
        }



        internal override void InitializeSkills()
        {
            Skills.CreateSkillFamilies(bodyPrefab);
            Modules.Skills.CreateFirstExtraSkillFamily(bodyPrefab);
            Modules.Skills.CreateSecondExtraSkillFamily(bodyPrefab);
            Modules.Skills.CreateThirdExtraSkillFamily(bodyPrefab);
            Modules.Skills.CreateFourthExtraSkillFamily(bodyPrefab);

            string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";

            #region Passive
            SkillLocator skillloc = bodyPrefab.GetComponent<SkillLocator>();
            skillloc.passiveSkill.enabled = true;
            skillloc.passiveSkill.skillNameToken = prefix + "PASSIVE_NAME";
            skillloc.passiveSkill.skillDescriptionToken = prefix + "PASSIVE_DESCRIPTION";
            skillloc.passiveSkill.icon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("allforone");
            skillloc.passiveSkill.keywordToken = prefix + "KEYWORD_PASSIVE";
            #endregion

            #region Primary

            decayDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {

                skillName = prefix + "DECAY_NAME",
                skillNameToken = prefix + "DECAY_NAME",
                skillDescriptionToken = prefix + "DECAY_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("decay"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Decay)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE", prefix + "KEYWORD_DECAY" }
            });


            #endregion

            

            #region Secondary

            bulletlaserDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BULLETLASER_NAME",
                skillNameToken = prefix + "BULLETLASER_NAME",
                skillDescriptionToken = prefix + "BULLETLASER_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BulletLaser)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 3f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            #endregion

            #region Utility
            aircannonDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "AIRCANNON_NAME",
                skillNameToken = prefix + "AIRCANNON_NAME",
                skillDescriptionToken = prefix + "AIRCANNON_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("aircannon"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.AirCannon)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 5f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE", prefix + "KEYWORD_DECAY", "KEYWORD_STUNNING" }

            });
            //SkillDef ARTIFICER = Skills.CreateSkillDef(new SkillDefInfo
            //{
            //    skillName = prefix + "MULTBUFF_NAME",
            //    skillNameToken = prefix + "MULTBUFF_NAME",
            //    skillDescriptionToken = prefix + "MULTBUFF_DESCRIPTION",
            //    skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("MUL-T"),
            //    activationState = new SerializableEntityStateType(typeof(SkillStates.MultBuff)),
            //    activationStateMachineName = "Weapon",
            //    baseMaxStock = 1,
            //    baseRechargeInterval = 4f,
            //    beginSkillCooldownOnSkillEnd = true,
            //    canceledFromSprinting = false,
            //    forceSprintDuringState = false,
            //    fullRestockOnAssign = false,
            //    interruptPriority = InterruptPriority.Skill,
            //    resetCooldownTimerOnUse = false,
            //    isCombatSkill = true,
            //    mustKeyPress = true,
            //    cancelSprintingOnActivation = false,
            //    rechargeStock = 1,
            //    requiredStock = 1,
            //    stockToConsume = 1,
            //    keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_SLAYER" }

            //});

            #endregion

            #region Special
            multiplierDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "MULTIPLIER_NAME",
                skillNameToken = prefix + "MULTIPLIER_NAME",
                skillDescriptionToken = prefix + "MULTIPLIER_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("multiplier"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Multiplier)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 8f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });



            #endregion

            #region Extra Skills

            emptySkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "AFO_NAME",
                skillNameToken = prefix + "AFO_NAME",
                skillDescriptionToken = prefix + "AFO_DESCRIPTION",
                skillIcon = Modules.Assets.skinBuffIcon,
                activationState = new SerializableEntityStateType(typeof(SkillStates.EmptySkill)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });

            chooseDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "CHOOSESKILL_NAME",
                skillNameToken = prefix + "CHOOSESKILL_NAME",
                skillDescriptionToken = prefix + "CHOOSESKILL_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("allforone"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ChooseSkill)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });
            removeDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "REMOVESKILL_NAME",
                skillNameToken = prefix + "REMOVESKILL_NAME",
                skillDescriptionToken = prefix + "REMOVESKILL_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("allforone"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.RemoveSkill)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1
            });
            #endregion

            #region Passive Skills
            //passives
            Shiggy.alphacontructpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "ALPHACONSTRUCT_NAME",
                skillNameToken = prefix + "ALPHACONSTRUCT_NAME",
                skillDescriptionToken = prefix + "ALPHACONSTRUCT_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Alpha_Construct"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.AlphaConstruct)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.beetlepassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BEETLE_NAME",
                skillNameToken = prefix + "BEETLE_NAME",
                skillDescriptionToken = prefix + "BEETLE_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Beetle"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Beetle)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.guppassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "GUP_NAME",
                skillNameToken = prefix + "GUP_NAME",
                skillDescriptionToken = prefix + "GUP_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Gup"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Gup)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.larvapassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "LARVA_NAME",
                skillNameToken = prefix + "LARVA_NAME",
                skillDescriptionToken = prefix + "LARVA_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Larva"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Larva)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.lesserwisppassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "LESSERWISP_NAME",
                skillNameToken = prefix + "LESSERWISP_NAME",
                skillDescriptionToken = prefix + "LESSERWISP_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Lesser_Wisp"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.LesserWisp)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.lunarexploderpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "LUNAREXPLODER_NAME",
                skillNameToken = prefix + "LUNAREXPLODER_NAME",
                skillDescriptionToken = prefix + "LUNAREXPLODER_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Lunar_Exploder"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.LunarExploder)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.hermitcrabpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "HERMITCRAB_NAME",
                skillNameToken = prefix + "HERMITCRAB_NAME",
                skillDescriptionToken = prefix + "HERMITCRAB_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Hermit_Crab"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.HermitCrab)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.pestpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "PEST_NAME",
                skillNameToken = prefix + "PEST_NAME",
                skillDescriptionToken = prefix + "PEST_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Blind_Pest"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BlindPest)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.verminpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "VERMIN_NAME",
                skillNameToken = prefix + "VERMIN_NAME",
                skillDescriptionToken = prefix + "VERMIN_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Blind_Vermin"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BlindVermin)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.minimushrumpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "MINIMUSHRUM_NAME",
                skillNameToken = prefix + "MINIMUSHRUM_NAME",
                skillDescriptionToken = prefix + "MINIMUSHRUM_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Mini_Mushrum"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.MiniMushrum)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.roboballminibpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "ROBOBALLMINI_NAME",
                skillNameToken = prefix + "ROBOBALLMINI_NAME",
                skillDescriptionToken = prefix + "ROBOBALLMINI_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Solus_Probe"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.RoboBallMini)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.voidbarnaclepassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "VOIDBARNACLE_NAME",
                skillNameToken = prefix + "VOIDBARNACLE_NAME",
                skillDescriptionToken = prefix + "VOIDBARNACLE_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Void_Barnacle"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.VoidBarnacle)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.voidjailerpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "VOIDJAILER_NAME",
                skillNameToken = prefix + "VOIDJAILER_NAME",
                skillDescriptionToken = prefix + "VOIDJAILER_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Void_Jailer"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.VoidJailer)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });

            Shiggy.impbosspassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "IMPBOSS_NAME",
                skillNameToken = prefix + "IMPBOSS_NAME",
                skillDescriptionToken = prefix + "IMPBOSS_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Imp_Overlord"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ImpBoss)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.stonetitanpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "STONETITAN_NAME",
                skillNameToken = prefix + "STONETITAN_NAME",
                skillDescriptionToken = prefix + "STONETITAN_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Stone_Titan"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.StoneTitan)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.magmawormpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "MAGMAWORM_NAME",
                skillNameToken = prefix + "MAGMAWORM_NAME",
                skillDescriptionToken = prefix + "MAGMAWORM_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Magma_Worm"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.MagmaWorm)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.overloadingwormpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "OVERLOADINGWORM_NAME",
                skillNameToken = prefix + "OVERLOADINGWORM_NAME",
                skillDescriptionToken = prefix + "OVERLOADINGWORM_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Overloading_Worm"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.OverloadingWorm)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.vagrantpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "VAGRANT_NAME",
                skillNameToken = prefix + "VAGRANT_NAME",
                skillDescriptionToken = prefix + "VAGRANT_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Wandering_Vagrant"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Vagrant)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });

            Shiggy.acridpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "ACRID_NAME",
                skillNameToken = prefix + "ACRID_NAME",
                skillDescriptionToken = prefix + "ACRID_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Acrid"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Acrid)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.commandopassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "COMMANDO_NAME",
                skillNameToken = prefix + "COMMANDO_NAME",
                skillDescriptionToken = prefix + "COMMANDO_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Commando"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Commando)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.captainpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "CAPTAIN_NAME",
                skillNameToken = prefix + "CAPTAIN_NAME",
                skillDescriptionToken = prefix + "CAPTAIN_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Captain"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Captain)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.loaderpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "LOADER_NAME",
                skillNameToken = prefix + "LOADER_NAME",
                skillDescriptionToken = prefix + "LOADER_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Loader"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Loader)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            #endregion

            #region Active Skills
            Shiggy.alloyvultureWindBlastDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "VULTURE_NAME",
                skillNameToken = prefix + "VULTURE_NAME",
                skillDescriptionToken = prefix + "VULTURE_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Alloy_Vulture"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.AlloyVultureWindBlast)),
                activationStateMachineName = "Body",
                baseMaxStock = 1,
                baseRechargeInterval = 6f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.beetleguardslamDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BEETLEGUARD_NAME",
                skillNameToken = prefix + "BEETLEGUARD_NAME",
                skillDescriptionToken = prefix + "BEETLEGUARD_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Beetle_Guard"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BeetleGuardSlam)),
                activationStateMachineName = "Body",
                baseMaxStock = 1,
                baseRechargeInterval = 4f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = true,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE", prefix + "KEYWORD_DECAY", "KEYWORD_STUNNING" }

            });
            Shiggy.bisonchargeDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BISON_NAME",
                skillNameToken = prefix + "BISON_NAME",
                skillDescriptionToken = prefix + "BISON_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Bighorn_Bison"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BisonCharge)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 4f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = true,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE", prefix + "KEYWORD_DECAY", "KEYWORD_STUNNING" }

            });
            Shiggy.bronzongballDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BRONZONG_NAME",
                skillNameToken = prefix + "BRONZONG_NAME",
                skillDescriptionToken = prefix + "BRONZONG_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Brass_Contraption"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BronzongBall)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 6f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.clayapothecarymortarDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "APOTHECARY_NAME",
                skillNameToken = prefix + "APOTHECARY_NAME",
                skillDescriptionToken = prefix + "APOTHECARY_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Clay_Apothecary"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ClayApothecaryMortar)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 7f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_TAR" }

            });
            Shiggy.claytemplarminigunDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "TEMPLAR_NAME",
                skillNameToken = prefix + "TEMPLAR_NAME",
                skillDescriptionToken = prefix + "TEMPLAR_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Clay_Templar"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ClayTemplarMinigun)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_TAR" }

            });
            Shiggy.elderlemurianfireblastDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "ELDERLEMURIAN_NAME",
                skillNameToken = prefix + "ELDERLEMURIAN_NAME",
                skillDescriptionToken = prefix + "ELDERLEMURIAN_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Elder_Lemurian"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ElderLemurianFireBlastCharge)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 5f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.greaterwispballDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "GREATERWISP_NAME",
                skillNameToken = prefix + "GREATERWISP_NAME",
                skillDescriptionToken = prefix + "GREATERWISP_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Greater_Wisp"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.GreaterWispBuff)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 8f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.impblinkDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "IMP_NAME",
                skillNameToken = prefix + "IMP_NAME",
                skillDescriptionToken = prefix + "IMP_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Imp"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ImpBlink)),
                activationStateMachineName = "Slide",
                baseMaxStock = 3,
                baseRechargeInterval = 4f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.jellyfishHealDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "JELLYFISH_NAME",
                skillNameToken = prefix + "JELLYFISH_NAME",
                skillDescriptionToken = prefix + "JELLYFISH_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Jellyfish"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.JellyfishHeal)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 6f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_STUNNING" }

            });
            Shiggy.lemurianfireballDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "LEMURIAN_NAME",
                skillNameToken = prefix + "LEMURIAN_NAME",
                skillDescriptionToken = prefix + "LEMURIAN_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Lemurian"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.LemurianFireball)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_BURNING" }

            });
            Shiggy.lunargolemSlideDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "LUNARGOLEM_NAME",
                skillNameToken = prefix + "LUNARGOLEM_NAME",
                skillDescriptionToken = prefix + "LUNARGOLEM_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Lunar_Golem"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.LunarGolemSlide)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 8f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.lunarwispminigunDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "LUNARWISP_NAME",
                skillNameToken = prefix + "LUNARWISP_NAME",
                skillDescriptionToken = prefix + "LUNARWISP_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Lunar_Wisp"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.LunarWispMinigun)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_CRIPPLE" }

            });
            Shiggy.parentteleportDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "PARENT_NAME",
                skillNameToken = prefix + "PARENT_NAME",
                skillDescriptionToken = prefix + "PARENT_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Parent"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ParentTeleport)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 2,
                baseRechargeInterval = 5f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE", prefix + "KEYWORD_DECAY", "KEYWORD_STUNNING" }

            });
            Shiggy.stonegolemlaserDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "STONEGOLEM_NAME",
                skillNameToken = prefix + "STONEGOLEM_NAME",
                skillDescriptionToken = prefix + "STONEGOLEM_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Stone_Golem"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.StoneGolemLaserCharge)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 5f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_STUNNING" }

            });
            Shiggy.voidreaverportalDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "VOIDREAVER_NAME",
                skillNameToken = prefix + "VOIDREAVER_NAME",
                skillDescriptionToken = prefix + "VOIDREAVER_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Void_Reaver"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.VoidReaverPortal)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });


            Shiggy.beetlequeenSummonDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BEETLEQUEEN_NAME",
                skillNameToken = prefix + "BEETLEQUEEN_NAME",
                skillDescriptionToken = prefix + "BEETLEQUEEN_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Beetle_Queen"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BeetleQueenSummon)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 4f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.grandparentsunDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "GRANDPARENT_NAME",
                skillNameToken = prefix + "GRANDPARENT_NAME",
                skillDescriptionToken = prefix + "GRANDPARENT_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Grandparent"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.GrandparentSun)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 12f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "" }

            });
            Shiggy.grovetenderChainDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "GROVETENDER_NAME",
                skillNameToken = prefix + "GROVETENDER_NAME",
                skillDescriptionToken = prefix + "GROVETENDER_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Grovetender"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.GrovetenderChain)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 4f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.claydunestriderbuffDef    = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "CLAYDUNESTRIDER_NAME",
                skillNameToken = prefix + "CLAYDUNESTRIDER_NAME",
                skillDescriptionToken = prefix + "CLAYDUNESTRIDER_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Clay_Dunestrider"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ClayDunestriderBuff)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 3f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.soluscontrolunityknockupDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "SOLUSCONTROLUNIT_NAME",
                skillNameToken = prefix + "SOLUSCONTROLUNIT_NAME",
                skillDescriptionToken = prefix + "SOLUSCONTROLUNIT_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Solus_Control_Unit"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.SolusControlUnitKnockup)),
                activationStateMachineName = "Body",
                baseMaxStock = 1,
                baseRechargeInterval = 10f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.xiconstructbeamDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "XICONSTRUCT_NAME",
                skillNameToken = prefix + "XICONSTRUCT_NAME",
                skillDescriptionToken = prefix + "XICONSTRUCT_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Xi_Construct"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.XiConstructBeam)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.voiddevastatorhomingDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "VOIDDEVASTATOR_NAME",
                skillNameToken = prefix + "VOIDDEVASTATOR_NAME",
                skillDescriptionToken = prefix + "VOIDDEVASTATOR_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Void_Devastator"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.VoidDevastatorHoming)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 6f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.scavengerthqwibDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "SCAVENGER_NAME",
                skillNameToken = prefix + "SCAVENGER_NAME",
                skillDescriptionToken = prefix + "SCAVENGER_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Scavenger"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ScavengerThqwibs)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 7f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });

            Shiggy.artificerflamethrowerDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "ARTIFICERFLAMETHROWER_NAME",
                skillNameToken = prefix + "ARTIFICERFLAMETHROWER_NAME",
                skillDescriptionToken = prefix + "ARTIFICERFLAMETHROWER_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("ArtificerFire"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ArtificerFlamethrower)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 6f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.artificericewallDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "ARTIFICERICEWALL_NAME",
                skillNameToken = prefix + "ARTIFICERICEWALL_NAME",
                skillDescriptionToken = prefix + "ARTIFICERICEWALL_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("ArtificerIce"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ArtificerIceWall)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 4f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.artificerlightningorbDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "ARTIFICERLIGHTNINGORB_NAME",
                skillNameToken = prefix + "ARTIFICERLIGHTNINGORB_NAME",
                skillDescriptionToken = prefix + "ARTIFICERLIGHTNINGORB_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("ArtificerLightning"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ArtificerChargeLightningOrb)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 4f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            Shiggy.banditlightsoutDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BANDIT_NAME",
                skillNameToken = prefix + "BANDIT_NAME",
                skillDescriptionToken = prefix + "BANDIT_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Bandit"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BanditPrepLightsOut)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 4f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_SLAYER" }

            });
            Shiggy.engiturretDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "ENGI_NAME",
                skillNameToken = prefix + "ENGI_NAME",
                skillDescriptionToken = prefix + "ENGI_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Engi"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.EngiTurret)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 45f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE"}

            });
            Shiggy.huntressattackDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "HUNTRESS_NAME",
                skillNameToken = prefix + "HUNTRESS_NAME",
                skillDescriptionToken = prefix + "HUNTRESS_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Huntress"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.HuntressAttack)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE"}

            });
            Shiggy.mercdashDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "MERC_NAME",
                skillNameToken = prefix + "MERC_NAME",
                skillDescriptionToken = prefix + "MERC_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Mercenary"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.MercDash)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 6f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_SLAYER" }

            });
            Shiggy.multbuffDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "MULTBUFF_NAME",
                skillNameToken = prefix + "MULTBUFF_NAME",
                skillDescriptionToken = prefix + "MULTBUFF_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("MUL-T"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.MultBuff)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_SLAYER" }

            });
            Shiggy.multbuffcancelDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "MULTBUFFCANCEL_NAME",
                skillNameToken = prefix + "MULTBUFFCANCEL_NAME",
                skillDescriptionToken = prefix + "MULTBUFFCANCEL_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("MUL-TCANCEL"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.MultBuffCancel)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_SLAYER" }

            });
            Shiggy.railgunnercryoDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "RAILGUNNNER_NAME",
                skillNameToken = prefix + "RAILGUNNNER_NAME",
                skillDescriptionToken = prefix + "RAILGUNNNER_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Railgunner"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.RailgunnerCryoCharge)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 10f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_FREEZING" }

            });
            Shiggy.rexmortarDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "REX_NAME",
                skillNameToken = prefix + "REX_NAME",
                skillDescriptionToken = prefix + "REX_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("REX"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.RexMortar)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 0.5f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE"}

            });
            Shiggy.voidfiendcleanseDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "VOIDFIEND_NAME",
                skillNameToken = prefix + "VOIDFIEND_NAME",
                skillDescriptionToken = prefix + "VOIDFIEND_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Void_Fiend"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.VoidFiendCleanse)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 5f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE"}

            });
            #endregion

            #region Synergy Active Skills

            sweepingBeamDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "SWEEPINGBEAM_NAME",
                skillNameToken = prefix + "SWEEPINGBEAM_NAME",
                skillDescriptionToken = prefix + "SWEEPINGBEAM_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.SweepingBeam)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            blackholeGlaiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BLACKHOLEGLAIVE_NAME",
                skillNameToken = prefix + "BLACKHOLEGLAIVE_NAME",
                skillDescriptionToken = prefix + "BLACKHOLEGLAIVE_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BlackHoleGlaive)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            gravitationalDownforceDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "GRAVITATIONALDOWNFORCE_NAME",
                skillNameToken = prefix + "GRAVITATIONALDOWNFORCE_NAME",
                skillDescriptionToken = prefix + "GRAVITATIONALDOWNFORCE_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.GravitationalDownforce)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            windShieldDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "WINDSHIELD_NAME",
                skillNameToken = prefix + "WINDSHIELD_NAME",
                skillDescriptionToken = prefix + "WINDSHIELD_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.WindShield)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            genesisDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "GENESIS_NAME",
                skillNameToken = prefix + "GENESIS_NAME",
                skillDescriptionToken = prefix + "GENESIS_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Genesis)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            refreshDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "REFRESH_NAME",
                skillNameToken = prefix + "REFRESH_NAME",
                skillDescriptionToken = prefix + "REFRESH_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Refresh)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            expungeDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "EXPUNGE_NAME",
                skillNameToken = prefix + "EXPUNGE_NAME",
                skillDescriptionToken = prefix + "EXPUNGE_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Expunge)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            shadowClawDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "SHADOWCLAW_NAME",
                skillNameToken = prefix + "SHADOWCLAW_NAME",
                skillDescriptionToken = prefix + "SHADOWCLAW_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ShadowClaw)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            orbitalStrikeDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "ORBITALSTRIKE_NAME",
                skillNameToken = prefix + "ORBITALSTRIKE_NAME",
                skillDescriptionToken = prefix + "ORBITALSTRIKE_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.OrbitalStrike)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            thunderclapDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "THUNDERCLAP_NAME",
                skillNameToken = prefix + "THUNDERCLAP_NAME",
                skillDescriptionToken = prefix + "THUNDERCLAP_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Thunderclap)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            blastBurnDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BLASTBURN_NAME",
                skillNameToken = prefix + "BLASTBURN_NAME",
                skillDescriptionToken = prefix + "BLASTBURN_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BlastBurn)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            barrierJellyDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BARRIERJELLY_NAME",
                skillNameToken = prefix + "BARRIERJELLY_NAME",
                skillDescriptionToken = prefix + "BARRIERJELLY_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BarrierJelly)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            mechStanceDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "MECHSTANCE_NAME",
                skillNameToken = prefix + "MECHSTANCE_NAME",
                skillDescriptionToken = prefix + "MECHSTANCE_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.MechStance)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            windSlashDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "WINDSLASH_NAME",
                skillNameToken = prefix + "WINDSLASH_NAME",
                skillDescriptionToken = prefix + "WINDSLASH_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.WindSlash)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            DekuOFADef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "DEKUOFA_NAME",
                skillNameToken = prefix + "DEKUOFA_NAME",
                skillDescriptionToken = prefix + "DEKUOFA_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.DekuOFA)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            DekuOFADef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "DEKUOFA_NAME",
                skillNameToken = prefix + "DEKUOFA_NAME",
                skillDescriptionToken = prefix + "DEKUOFA_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.DekuOFA)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            limitBreakDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "LIMITBREAK_NAME",
                skillNameToken = prefix + "LIMITBREAK_NAME",
                skillDescriptionToken = prefix + "LIMITBREAK_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.LimitBreak)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            #endregion


            #region Synergy Passive Skills

            bigBangPassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BIGBANG_NAME",
                skillNameToken = prefix + "BIGBANG_NAME",
                skillDescriptionToken = prefix + "BIGBANG_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BigBangPassive)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] {}

            });
            wisperPassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "WISPER_NAME",
                skillNameToken = prefix + "WISPER_NAME",
                skillDescriptionToken = prefix + "WISPER_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.WisperPassive)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] {}

            });
            omniboostPassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "OMNIBOOST_NAME",
                skillNameToken = prefix + "OMNIBOOST_NAME",
                skillDescriptionToken = prefix + "OMNIBOOST_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.OmniboostPassive)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] {}

            });
            gachaPassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "GACHA_NAME",
                skillNameToken = prefix + "GACHA_NAME",
                skillDescriptionToken = prefix + "GACHA_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.GachaPassive)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { }

            });
            stoneFormPassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "STONEFORM_NAME",
                skillNameToken = prefix + "STONEFORM_NAME",
                skillDescriptionToken = prefix + "STONEFORM_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.StoneFormPassive)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { }

            });
            auraOfBlightPassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "AURAOFBLIGHT_NAME",
                skillNameToken = prefix + "AURAOFBLIGHT_NAME",
                skillDescriptionToken = prefix + "AURAOFBLIGHT_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.AuraOfBlightPassive)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { }

            });
            barbedSpikesPassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BARBEDSPIKES_NAME",
                skillNameToken = prefix + "BARBEDSPIKES_NAME",
                skillDescriptionToken = prefix + "BARBEDSPIKES_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BarbedSpikesPassive)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { }

            });
            ingrainPassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "INGRAIN_NAME",
                skillNameToken = prefix + "INGRAIN_NAME",
                skillDescriptionToken = prefix + "INGRAIN_DESCRIPTION",
                skillIcon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.IngrainPassive)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 1f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { }

            });
            #endregion

            #region Chosen Skills

            Modules.Skills.AddPrimarySkill(bodyPrefab, decayDef);
            Skills.AddSecondarySkills(this.bodyPrefab, new SkillDef[]
            {
                bulletlaserDef,

            });
            Skills.AddUtilitySkills(this.bodyPrefab, new SkillDef[]
            {
                aircannonDef,
                ingrainPassiveDef
            });
            Skills.AddSpecialSkills(this.bodyPrefab, new SkillDef[]
            {
                multiplierDef,
            });
            Modules.Skills.AddFirstExtraSkill(bodyPrefab, emptySkillDef);
            Modules.Skills.AddSecondExtraSkill(bodyPrefab, emptySkillDef);
            Modules.Skills.AddThirdExtraSkill(bodyPrefab, emptySkillDef);
            Modules.Skills.AddFourthExtraSkill(bodyPrefab, emptySkillDef);
            #endregion
        }


        internal override void InitializeSkins()
        {
            GameObject model = bodyPrefab.GetComponentInChildren<ModelLocator>().modelTransform.gameObject;
            CharacterModel characterModel = model.GetComponent<CharacterModel>();

            ModelSkinController skinController = model.AddComponent<ModelSkinController>();
            ChildLocator childLocator = model.GetComponent<ChildLocator>();

            SkinnedMeshRenderer mainRenderer = characterModel.mainSkinnedMeshRenderer;

            CharacterModel.RendererInfo[] defaultRenderers = characterModel.baseRendererInfos;

            List<SkinDef> skins = new List<SkinDef>();

            #region DefaultSkin
            Material defaultMat = Modules.Assets.CreateMaterial("ShiggyMat", 0f, Color.white, 0f);
            CharacterModel.RendererInfo[] defaultRendererInfo = SkinRendererInfos(defaultRenderers, new Material[] {
                defaultMat,
                defaultMat,
            });
            SkinDef defaultSkin = Modules.Skins.CreateSkinDef(ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_DEFAULT_SKIN_NAME",
                Assets.mainAssetBundle.LoadAsset<Sprite>("ShiggyBaseSkin"),
                defaultRendererInfo,
                mainRenderer,
                model);

            defaultSkin.meshReplacements = new SkinDef.MeshReplacement[]
            {
                new SkinDef.MeshReplacement
                {
                    mesh = Modules.Assets.mainAssetBundle.LoadAsset<Mesh>("MeshHand"),
                    renderer = defaultRendererInfo[0].renderer
                },
                new SkinDef.MeshReplacement
                {
                    mesh = Modules.Assets.mainAssetBundle.LoadAsset<Mesh>("MeshShiggy"),
                    renderer = defaultRendererInfo[instance.mainRendererIndex].renderer
                }
            };

            skins.Add(defaultSkin);
            #endregion

            //handless skin
            #region handlessSkin
            Material emptyMat = Modules.Assets.mainAssetBundle.LoadAsset<Material>("EmptyMat");
            CharacterModel.RendererInfo[] handlessrendererInfos = SkinRendererInfos(defaultRenderers, new Material[] {
                emptyMat,
                defaultMat,
            });
            SkinDef handlessSkin = Modules.Skins.CreateSkinDef(ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_HANDLESS_SKIN_NAME",
                Assets.mainAssetBundle.LoadAsset<Sprite>("ShiggyBaseSkin"),
                handlessrendererInfos,
                mainRenderer,
                model);

            handlessSkin.meshReplacements = new SkinDef.MeshReplacement[]
            {
                new SkinDef.MeshReplacement
                {
                    mesh = Modules.Assets.mainAssetBundle.LoadAsset<Mesh>("MeshHand"),
                    renderer = handlessrendererInfos[0].renderer,
                },
                new SkinDef.MeshReplacement
                {
                    mesh = Modules.Assets.mainAssetBundle.LoadAsset<Mesh>("MeshShiggy"),
                    renderer = handlessrendererInfos[1].renderer
                }
            };

            skins.Add(handlessSkin);
            #endregion

            //#region masteryskin
            //Material masteryMat = Modules.Assets.CreateMaterial("ShinyShiggyMat", 0f, Color.white, 1.0f);
            //CharacterModel.RendererInfo[] masteryRendererInfos = SkinRendererInfos(defaultRenderers, new Material[] {
            //    masteryMat,
            //    masteryMat,
            //    masteryMat,
            //    masteryMat,
            //});
            //SkinDef masterySkin = Modules.Skins.CreateSkinDef(ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_MASTERY_SKIN_NAME",
            //    Assets.mainAssetBundle.LoadAsset<Sprite>("ShiggyShinySkin"),
            //    masteryRendererInfos,
            //    mainRenderer,
            //    model,
            //    masterySkinUnlockableDef);

            //masterySkin.meshReplacements = new SkinDef.MeshReplacement[] {
            //    new SkinDef.MeshReplacement
            //    {
            //        mesh = Modules.Assets.mainAssetBundle.LoadAsset<Mesh>("MeshShiggy"),
            //        renderer = defaultRenderers[instance.mainRendererIndex].renderer
            //    },
            //};
            //skins.Add(masterySkin);
            //#endregion

            skinController.skins = skins.ToArray();
        }

        internal override void SetItemDisplays()
        {
            //itemDisplayRules = new List<ItemDisplayRuleSet.KeyAssetRuleGroup>();

            //            //add item displays here
            //            //  HIGHLY recommend using KingEnderBrine's ItemDisplayPlacementHelper mod for this
            //            #region Item Displays
            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Jetpack,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBugWings"),
            //                            childName = "Chest",
            //                            localPos = new Vector3(0.0009F, 0.2767F, -0.1F),
            //                            localAngles = new Vector3(21.4993F, 358.6616F, 358.3334F),
            //                            localScale = new Vector3(0.1F, 0.1F, 0.1F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.GoldGat,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGoldGat"),
            //childName = "Chest",
            //localPos = new Vector3(0.1009F, 0.4728F, -0.1278F),
            //localAngles = new Vector3(22.6043F, 114.6042F, 299.1935F),
            //localScale = new Vector3(0.15F, 0.15F, 0.15F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.BFG,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBFG"),
            //childName = "Chest",
            //localPos = new Vector3(0.0782F, 0.4078F, 0F),
            //localAngles = new Vector3(0F, 0F, 313.6211F),
            //localScale = new Vector3(0.3F, 0.3F, 0.3F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.CritGlasses,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGlasses"),
            //childName = "Head",
            //localPos = new Vector3(0F, 0.1687F, 0.1558F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.3215F, 0.3034F, 0.3034F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Syringe,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySyringeCluster"),
            //childName = "Chest",
            //localPos = new Vector3(-0.0534F, 0.0352F, 0F),
            //localAngles = new Vector3(0F, 0F, 83.2547F),
            //localScale = new Vector3(0.1F, 0.1F, 0.1F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Behemoth,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBehemoth"),
            //childName = "Gun",
            //localPos = new Vector3(0F, 0.2247F, -0.1174F),
            //localAngles = new Vector3(6.223F, 180F, 0F),
            //localScale = new Vector3(0.1F, 0.1F, 0.1F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Missile,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayMissileLauncher"),
            //childName = "Chest",
            //localPos = new Vector3(-0.3075F, 0.5204F, -0.049F),
            //localAngles = new Vector3(0F, 0F, 51.9225F),
            //localScale = new Vector3(0.1F, 0.1F, 0.1F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Dagger,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDagger"),
            //childName = "Chest",
            //localPos = new Vector3(-0.0553F, 0.2856F, 0.0945F),
            //localAngles = new Vector3(334.8839F, 31.5284F, 34.6784F),
            //localScale = new Vector3(1.2428F, 1.2428F, 1.2299F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Hoof,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayHoof"),
            //childName = "CalfL",
            //localPos = new Vector3(-0.0071F, 0.3039F, -0.051F),
            //localAngles = new Vector3(76.5928F, 0F, 0F),
            //localScale = new Vector3(0.0846F, 0.0846F, 0.0758F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.ChainLightning,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayUkulele"),
            //childName = "Chest",
            //localPos = new Vector3(-0.0011F, 0.1031F, -0.0901F),
            //localAngles = new Vector3(0F, 180F, 89.3997F),
            //localScale = new Vector3(0.4749F, 0.4749F, 0.4749F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.GhostOnKill,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayMask"),
            //childName = "Head",
            //localPos = new Vector3(0F, 0.1748F, 0.0937F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.6313F, 0.6313F, 0.6313F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Mushroom,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayMushroom"),
            //childName = "UpperArmR",
            //localPos = new Vector3(-0.0139F, 0.1068F, 0F),
            //localAngles = new Vector3(0F, 0F, 89.4525F),
            //localScale = new Vector3(0.0501F, 0.0501F, 0.0501F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.AttackSpeedOnCrit,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayWolfPelt"),
            //childName = "Head",
            //localPos = new Vector3(0F, 0.2783F, -0.002F),
            //localAngles = new Vector3(358.4554F, 0F, 0F),
            //localScale = new Vector3(0.5666F, 0.5666F, 0.5666F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.BleedOnHit,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayTriTip"),
            //childName = "Chest",
            //localPos = new Vector3(-0.1247F, 0.416F, 0.1225F),
            //localAngles = new Vector3(11.5211F, 128.5354F, 165.922F),
            //localScale = new Vector3(0.2615F, 0.2615F, 0.2615F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.WardOnLevel,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayWarbanner"),
            //childName = "Pelvis",
            //localPos = new Vector3(0.0168F, 0.0817F, -0.0955F),
            //localAngles = new Vector3(0F, 0F, 90F),
            //localScale = new Vector3(0.3162F, 0.3162F, 0.3162F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.HealOnCrit,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayScythe"),
            //childName = "Chest",
            //localPos = new Vector3(0.0278F, 0.2322F, -0.0877F),
            //localAngles = new Vector3(323.9545F, 90F, 270F),
            //localScale = new Vector3(0.1884F, 0.1884F, 0.1884F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.HealWhileSafe,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySnail"),
            //childName = "UpperArmL",
            //localPos = new Vector3(0F, 0.3267F, 0.046F),
            //localAngles = new Vector3(90F, 0F, 0F),
            //localScale = new Vector3(0.0289F, 0.0289F, 0.0289F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Clover,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayClover"),
            //childName = "Gun",
            //localPos = new Vector3(0.0004F, 0.1094F, -0.1329F),
            //localAngles = new Vector3(85.6192F, 0.0001F, 179.4897F),
            //localScale = new Vector3(0.2749F, 0.2749F, 0.2749F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.BarrierOnOverHeal,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAegis"),
            //childName = "LowerArmL",
            //localPos = new Vector3(0F, 0.1396F, 0F),
            //localAngles = new Vector3(86.4709F, 180F, 180F),
            //localScale = new Vector3(0.2849F, 0.2849F, 0.2849F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.GoldOnHit,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBoneCrown"),
            //childName = "Head",
            //localPos = new Vector3(0F, 0.1791F, 0F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(1.1754F, 1.1754F, 1.1754F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.WarCryOnMultiKill,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayPauldron"),
            //childName = "UpperArmL",
            //localPos = new Vector3(0.0435F, 0.0905F, 0.0118F),
            //localAngles = new Vector3(82.0839F, 160.9228F, 100.7722F),
            //localScale = new Vector3(0.7094F, 0.7094F, 0.7094F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.SprintArmor,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBuckler"),
            //childName = "LowerArmR",
            //localPos = new Vector3(-0.005F, 0.285F, 0.0074F),
            //localAngles = new Vector3(358.4802F, 192.347F, 88.4811F),
            //localScale = new Vector3(0.3351F, 0.3351F, 0.3351F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.IceRing,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayIceRing"),
            //childName = "Gun",
            //localPos = new Vector3(0.0334F, 0.2587F, -0.1223F),
            //localAngles = new Vector3(274.3965F, 90F, 270F),
            //localScale = new Vector3(0.3627F, 0.3627F, 0.3627F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.FireRing,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayFireRing"),
            //childName = "Gun",
            //localPos = new Vector3(0.0352F, 0.282F, -0.1223F),
            //localAngles = new Vector3(274.3965F, 90F, 270F),
            //localScale = new Vector3(0.3627F, 0.3627F, 0.3627F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.UtilitySkillMagazine,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAfterburnerShoulderRing"),
            //                            childName = "UpperArmL",
            //                            localPos = new Vector3(0, 0, -0.002f),
            //                            localAngles = new Vector3(-90, 0, 0),
            //                            localScale = new Vector3(0.01f, 0.01f, 0.01f),
            //                            limbMask = LimbFlags.None
            //                        },
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAfterburnerShoulderRing"),
            //                            childName = "UpperArmR",
            //                            localPos = new Vector3(0, 0, -0.002f),
            //                            localAngles = new Vector3(-90, 0, 0),
            //                            localScale = new Vector3(0.01f, 0.01f, 0.01f),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.JumpBoost,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayWaxBird"),
            //childName = "Head",
            //localPos = new Vector3(0F, 0.0529F, -0.1242F),
            //localAngles = new Vector3(24.419F, 0F, 0F),
            //localScale = new Vector3(0.5253F, 0.5253F, 0.5253F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.ArmorReductionOnHit,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayWarhammer"),
            //childName = "Chest",
            //localPos = new Vector3(0.0513F, 0.0652F, -0.0792F),
            //localAngles = new Vector3(64.189F, 90F, 90F),
            //localScale = new Vector3(0.1722F, 0.1722F, 0.1722F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.NearbyDamageBonus,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDiamond"),
            //childName = "Sword",
            //localPos = new Vector3(-0.002F, 0.1828F, 0F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.1236F, 0.1236F, 0.1236F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.ArmorPlate,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayRepulsionArmorPlate"),
            //childName = "ThighL",
            //localPos = new Vector3(0.0218F, 0.4276F, 0F),
            //localAngles = new Vector3(90F, 180F, 0F),
            //localScale = new Vector3(0.1971F, 0.1971F, 0.1971F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.CommandMissile,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayMissileRack"),
            //childName = "Chest",
            //localPos = new Vector3(0F, 0.2985F, -0.0663F),
            //localAngles = new Vector3(90F, 180F, 0F),
            //localScale = new Vector3(0.3362F, 0.3362F, 0.3362F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Feather,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayFeather"),
            //childName = "LowerArmL",
            //localPos = new Vector3(0.001F, 0.2755F, 0.0454F),
            //localAngles = new Vector3(270F, 91.2661F, 0F),
            //localScale = new Vector3(0.0285F, 0.0285F, 0.0285F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Crowbar,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayCrowbar"),
            //childName = "Chest",
            //localPos = new Vector3(0F, 0.1219F, -0.0764F),
            //localAngles = new Vector3(90F, 90F, 0F),
            //localScale = new Vector3(0.1936F, 0.1936F, 0.1936F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.FallBoots,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGravBoots"),
            //childName = "CalfL",
            //localPos = new Vector3(-0.0038F, 0.3729F, -0.0046F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.1485F, 0.1485F, 0.1485F),
            //                            limbMask = LimbFlags.None
            //                        },
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGravBoots"),
            //childName = "CalfR",
            //localPos = new Vector3(-0.0038F, 0.3729F, -0.0046F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.1485F, 0.1485F, 0.1485F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.ExecuteLowHealthElite,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGuillotine"),
            //childName = "ThighR",
            //localPos = new Vector3(-0.0561F, 0.1607F, 0F),
            //localAngles = new Vector3(90F, 90F, 0F),
            //localScale = new Vector3(0.1843F, 0.1843F, 0.1843F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.EquipmentMagazine,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBattery"),
            //childName = "Chest",
            //localPos = new Vector3(0F, 0.0791F, 0.0726F),
            //localAngles = new Vector3(0F, 270F, 292.8411F),
            //localScale = new Vector3(0.0773F, 0.0773F, 0.0773F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.NovaOnHeal,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDevilHorns"),
            //childName = "Head",
            //localPos = new Vector3(0.0949F, 0.0945F, 0.0654F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.5349F, 0.5349F, 0.5349F),
            //                            limbMask = LimbFlags.None
            //                        },
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDevilHorns"),
            //childName = "Head",
            //localPos = new Vector3(-0.0949F, 0.0945F, 0.0105F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(-0.5349F, 0.5349F, 0.5349F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Infusion,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayInfusion"),
            //childName = "Pelvis",
            //localPos = new Vector3(-0.0703F, 0.0238F, -0.0366F),
            //localAngles = new Vector3(0F, 45F, 0F),
            //localScale = new Vector3(0.5253F, 0.5253F, 0.5253F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Medkit,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayMedkit"),
            //childName = "Chest",
            //localPos = new Vector3(0.0039F, -0.0125F, -0.0546F),
            //localAngles = new Vector3(290F, 180F, 0F),
            //localScale = new Vector3(0.4907F, 0.4907F, 0.4907F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Bandolier,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBandolier"),
            //childName = "Chest",
            //localPos = new Vector3(0.0035F, 0F, 0F),
            //localAngles = new Vector3(270F, 0F, 0F),
            //localScale = new Vector3(0.1684F, 0.242F, 0.242F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.BounceNearby,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayHook"),
            //childName = "Chest",
            //localPos = new Vector3(-0.0922F, 0.4106F, -0.0015F),
            //localAngles = new Vector3(290.3197F, 89F, 0F),
            //localScale = new Vector3(0.214F, 0.214F, 0.214F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.IgniteOnKill,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGasoline"),
            //childName = "ThighL",
            //localPos = new Vector3(0.0494F, 0.0954F, 0.0015F),
            //localAngles = new Vector3(90F, 0F, 0F),
            //localScale = new Vector3(0.3165F, 0.3165F, 0.3165F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.StunChanceOnHit,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayStunGrenade"),
            //childName = "ThighR",
            //localPos = new Vector3(0.001F, 0.3609F, 0.0523F),
            //localAngles = new Vector3(90F, 0F, 0F),
            //localScale = new Vector3(0.5672F, 0.5672F, 0.5672F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Firework,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayFirework"),
            //childName = "Muzzle",
            //localPos = new Vector3(0.0086F, 0.0069F, 0.0565F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.1194F, 0.1194F, 0.1194F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.LunarDagger,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayLunarDagger"),
            //childName = "Chest",
            //localPos = new Vector3(-0.0015F, 0.2234F, -0.0655F),
            //localAngles = new Vector3(277.637F, 358.2474F, 1.4903F),
            //localScale = new Vector3(0.3385F, 0.3385F, 0.3385F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Knurl,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayKnurl"),
            //childName = "LowerArmL",
            //localPos = new Vector3(-0.0186F, 0.0405F, -0.0049F),
            //localAngles = new Vector3(78.8707F, 36.6722F, 105.8275F),
            //localScale = new Vector3(0.0848F, 0.0848F, 0.0848F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.BeetleGland,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBeetleGland"),
            //childName = "Chest",
            //localPos = new Vector3(0.0852F, 0.0577F, 0F),
            //localAngles = new Vector3(359.9584F, 0.1329F, 39.8304F),
            //localScale = new Vector3(0.0553F, 0.0553F, 0.0553F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.SprintBonus,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySoda"),
            //childName = "Pelvis",
            //localPos = new Vector3(-0.075F, 0.095F, 0F),
            //localAngles = new Vector3(270F, 251.0168F, 0F),
            //localScale = new Vector3(0.1655F, 0.1655F, 0.1655F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.SecondarySkillMagazine,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDoubleMag"),
            //childName = "Gun",
            //localPos = new Vector3(-0.0018F, 0.0002F, 0.097F),
            //localAngles = new Vector3(84.2709F, 200.5981F, 25.0139F),
            //localScale = new Vector3(0.0441F, 0.0441F, 0.0441F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.StickyBomb,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayStickyBomb"),
            //childName = "Pelvis",
            //localPos = new Vector3(0.0594F, 0.0811F, 0.0487F),
            //localAngles = new Vector3(8.4958F, 176.5473F, 162.7601F),
            //localScale = new Vector3(0.0736F, 0.0736F, 0.0736F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.TreasureCache,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayKey"),
            //childName = "Pelvis",
            //localPos = new Vector3(0.0589F, 0.1056F, -0.0174F),
            //localAngles = new Vector3(0.2454F, 195.0205F, 89.0854F),
            //localScale = new Vector3(0.4092F, 0.4092F, 0.4092F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.BossDamageBonus,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAPRound"),
            //childName = "Pelvis",
            //localPos = new Vector3(-0.0371F, 0.0675F, -0.052F),
            //localAngles = new Vector3(90F, 41.5689F, 0F),
            //localScale = new Vector3(0.2279F, 0.2279F, 0.2279F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.SlowOnHit,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBauble"),
            //childName = "Pelvis",
            //localPos = new Vector3(-0.0074F, 0.076F, -0.0864F),
            //localAngles = new Vector3(0F, 23.7651F, 0F),
            //localScale = new Vector3(0.0687F, 0.0687F, 0.0687F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.ExtraLife,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayHippo"),
            //childName = "Chest",
            //localPos = new Vector3(0F, 0.3001F, 0.0555F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.2645F, 0.2645F, 0.2645F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.KillEliteFrenzy,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBrainstalk"),
            //childName = "Head",
            //localPos = new Vector3(0F, 0.1882F, 0F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.2638F, 0.2638F, 0.2638F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.RepeatHeal,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayCorpseFlower"),
            //childName = "UpperArmR",
            //localPos = new Vector3(-0.0393F, 0.1484F, 0F),
            //localAngles = new Vector3(270F, 90F, 0F),
            //localScale = new Vector3(0.1511F, 0.1511F, 0.1511F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.AutoCastEquipment,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayFossil"),
            //childName = "Chest",
            //localPos = new Vector3(-0.0722F, 0.0921F, 0F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.4208F, 0.4208F, 0.4208F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.IncreaseHealing,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAntler"),
            //childName = "Head",
            //localPos = new Vector3(0.1003F, 0.269F, 0F),
            //localAngles = new Vector3(0F, 90F, 0F),
            //localScale = new Vector3(0.3395F, 0.3395F, 0.3395F),
            //                            limbMask = LimbFlags.None
            //                        },
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAntler"),
            //childName = "Head",
            //localPos = new Vector3(-0.1003F, 0.269F, 0F),
            //localAngles = new Vector3(0F, 90F, 0F),
            //localScale = new Vector3(0.3395F, 0.3395F, -0.3395F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.TitanGoldDuringTP,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGoldHeart"),
            //childName = "Chest",
            //localPos = new Vector3(-0.0571F, 0.3027F, 0.0755F),
            //localAngles = new Vector3(335.0033F, 343.2951F, 0F),
            //localScale = new Vector3(0.1191F, 0.1191F, 0.1191F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.SprintWisp,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBrokenMask"),
            //childName = "UpperArmR",
            //localPos = new Vector3(-0.0283F, 0.0452F, -0.0271F),
            //localAngles = new Vector3(0F, 270F, 0F),
            //localScale = new Vector3(0.1385F, 0.1385F, 0.1385F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.BarrierOnKill,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBrooch"),
            //childName = "Gun",
            //localPos = new Vector3(-0.0097F, -0.0058F, -0.0847F),
            //localAngles = new Vector3(0F, 0F, 84.3456F),
            //localScale = new Vector3(0.1841F, 0.1841F, 0.1841F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.TPHealingNova,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGlowFlower"),
            //childName = "UpperArmL",
            //localPos = new Vector3(0.0399F, 0.1684F, 0.0121F),
            //localAngles = new Vector3(0F, 73.1449F, 0F),
            //localScale = new Vector3(0.2731F, 0.2731F, 0.0273F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.LunarUtilityReplacement,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBirdFoot"),
            //childName = "Head",
            //localPos = new Vector3(0F, 0.2387F, -0.199F),
            //localAngles = new Vector3(0F, 270F, 0F),
            //localScale = new Vector3(0.2833F, 0.2833F, 0.2833F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Thorns,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayRazorwireLeft"),
            //childName = "UpperArmL",
            //localPos = new Vector3(0F, 0F, 0F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.4814F, 0.4814F, 0.4814F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.LunarPrimaryReplacement,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBirdEye"),
            //childName = "Head",
            //localPos = new Vector3(0F, 0.1738F, 0.1375F),
            //localAngles = new Vector3(270F, 0F, 0F),
            //localScale = new Vector3(0.2866F, 0.2866F, 0.2866F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.NovaOnLowHealth,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayJellyGuts"),
            //childName = "Head",
            //localPos = new Vector3(-0.0484F, -0.0116F, 0.0283F),
            //localAngles = new Vector3(316.2306F, 45.1087F, 303.6165F),
            //localScale = new Vector3(0.1035F, 0.1035F, 0.1035F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.LunarTrinket,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBeads"),
            //childName = "LowerArmL",
            //localPos = new Vector3(0F, 0.3249F, 0.0381F),
            //localAngles = new Vector3(0F, 0F, 90F),
            //localScale = new Vector3(1F, 1F, 1F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Plant,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayInterstellarDeskPlant"),
            //childName = "UpperArmR",
            //localPos = new Vector3(-0.0663F, 0.2266F, 0F),
            //localAngles = new Vector3(4.9717F, 270F, 54.4915F),
            //localScale = new Vector3(0.0429F, 0.0429F, 0.0429F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Bear,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBear"),
            //childName = "Chest",
            //localPos = new Vector3(0F, 0.3014F, 0.0662F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.2034F, 0.2034F, 0.2034F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.DeathMark,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDeathMark"),
            //childName = "LowerArmR",
            //localPos = new Vector3(0F, 0.4099F, 0.0252F),
            //localAngles = new Vector3(277.5254F, 0F, 0F),
            //localScale = new Vector3(-0.0375F, -0.0341F, -0.0464F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.ExplodeOnDeath,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayWilloWisp"),
            //childName = "Pelvis",
            //localPos = new Vector3(0.0595F, 0.0696F, -0.0543F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.0283F, 0.0283F, 0.0283F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Seed,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySeed"),
            //childName = "Head",
            //localPos = new Vector3(-0.1702F, 0.1366F, -0.026F),
            //localAngles = new Vector3(344.0657F, 196.8238F, 275.5892F),
            //localScale = new Vector3(0.0275F, 0.0275F, 0.0275F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.SprintOutOfCombat,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayWhip"),
            //childName = "Pelvis",
            //localPos = new Vector3(0.1001F, -0.0132F, 0F),
            //localAngles = new Vector3(0F, 0F, 20.1526F),
            //localScale = new Vector3(0.2845F, 0.2845F, 0.2845F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.CooldownOnCrit,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySkull"),
            //childName = "Chest",
            //localPos = new Vector3(0F, 0.3997F, 0F),
            //localAngles = new Vector3(270F, 0F, 0F),
            //localScale = new Vector3(0.2789F, 0.2789F, 0.2789F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Phasing,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayStealthkit"),
            //childName = "CalfL",
            //localPos = new Vector3(-0.0063F, 0.2032F, -0.0507F),
            //localAngles = new Vector3(90F, 0F, 0F),
            //localScale = new Vector3(0.1454F, 0.2399F, 0.16F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.PersonalShield,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayShieldGenerator"),
            //childName = "Chest",
            //localPos = new Vector3(0F, 0.2649F, 0.0689F),
            //localAngles = new Vector3(304.1204F, 90F, 270F),
            //localScale = new Vector3(0.1057F, 0.1057F, 0.1057F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.ShockNearby,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayTeslaCoil"),
            //childName = "Chest",
            //localPos = new Vector3(0.0008F, 0.3747F, -0.0423F),
            //localAngles = new Vector3(297.6866F, 1.3864F, 358.5596F),
            //localScale = new Vector3(0.3229F, 0.3229F, 0.3229F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.ShieldOnly,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayShieldBug"),
            //childName = "Head",
            //localPos = new Vector3(0.0868F, 0.3114F, 0F),
            //localAngles = new Vector3(348.1819F, 268.0985F, 0.3896F),
            //localScale = new Vector3(0.3521F, 0.3521F, 0.3521F),
            //                            limbMask = LimbFlags.None
            //                        },
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayShieldBug"),
            //childName = "Head",
            //localPos = new Vector3(-0.0868F, 0.3114F, 0F),
            //localAngles = new Vector3(11.8181F, 268.0985F, 359.6104F),
            //localScale = new Vector3(0.3521F, 0.3521F, -0.3521F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.AlienHead,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAlienHead"),
            //childName = "Chest",
            //localPos = new Vector3(0.0417F, 0.2791F, -0.0493F),
            //localAngles = new Vector3(284.1172F, 243.7966F, 260.89F),
            //localScale = new Vector3(0.6701F, 0.6701F, 0.6701F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.HeadHunter,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySkullCrown"),
            //childName = "Head",
            //localPos = new Vector3(0F, 0.2556F, 0F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.4851F, 0.1617F, 0.1617F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.EnergizedOnEquipmentUse,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayWarHorn"),
            //childName = "Pelvis",
            //localPos = new Vector3(-0.1509F, 0.0659F, 0F),
            //localAngles = new Vector3(0F, 0F, 69.9659F),
            //localScale = new Vector3(0.2732F, 0.2732F, 0.2732F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.FlatHealth,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySteakCurved"),
            //childName = "Head",
            //localPos = new Vector3(0F, 0.3429F, -0.0671F),
            //localAngles = new Vector3(294.98F, 180F, 180F),
            //localScale = new Vector3(0.1245F, 0.1155F, 0.1155F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Tooth,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayToothMeshLarge"),
            //childName = "Head",
            //localPos = new Vector3(0F, 0.0687F, 0.0998F),
            //localAngles = new Vector3(344.9017F, 0F, 0F),
            //localScale = new Vector3(7.5452F, 7.5452F, 7.5452F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Pearl,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayPearl"),
            //childName = "LowerArmR",
            //localPos = new Vector3(0F, 0F, 0F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.1F, 0.1F, 0.1F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.ShinyPearl,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayShinyPearl"),
            //childName = "LowerArmL",
            //localPos = new Vector3(0F, 0F, 0F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.1F, 0.1F, 0.1F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.BonusGoldPackOnKill,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayTome"),
            //childName = "ThighR",
            //localPos = new Vector3(0.0155F, 0.2145F, 0.0615F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.0475F, 0.0475F, 0.0475F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Squid,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySquidTurret"),
            //childName = "Head",
            //localPos = new Vector3(-0.0164F, 0.1641F, -0.0005F),
            //localAngles = new Vector3(0F, 90F, 0F),
            //localScale = new Vector3(0.2235F, 0.3016F, 0.3528F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Icicle,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayFrostRelic"),
            //childName = "Base",
            //localPos = new Vector3(-0.658F, -1.0806F, 0.015F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(1F, 1F, 1F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Talisman,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayTalisman"),
            //childName = "Base",
            //localPos = new Vector3(0.8357F, -0.7042F, -0.2979F),
            //localAngles = new Vector3(270F, 0F, 0F),
            //localScale = new Vector3(1F, 1F, 1F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.LaserTurbine,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayLaserTurbine"),
            //childName = "Chest",
            //localPos = new Vector3(0F, 0.0622F, -0.0822F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.2159F, 0.2159F, 0.2159F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.FocusConvergence,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayFocusedConvergence"),
            //childName = "Base",
            //localPos = new Vector3(-0.0554F, -1.6605F, -0.3314F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.1F, 0.1F, 0.1F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.Incubator,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayAncestralIncubator"),
            //childName = "Chest",
            //localPos = new Vector3(0F, 0.3453F, 0F),
            //localAngles = new Vector3(353.0521F, 317.2421F, 69.6292F),
            //localScale = new Vector3(0.0528F, 0.0528F, 0.0528F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.FireballsOnHit,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayFireballsOnHit"),
            //childName = "LowerArmL",
            //localPos = new Vector3(0F, 0.3365F, -0.0878F),
            //localAngles = new Vector3(270F, 0F, 0F),
            //localScale = new Vector3(0.0761F, 0.0761F, 0.0761F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.SiphonOnLowHealth,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySiphonOnLowHealth"),
            //childName = "Pelvis",
            //localPos = new Vector3(0.0542F, 0.0206F, -0.0019F),
            //localAngles = new Vector3(0F, 303.4368F, 0F),
            //localScale = new Vector3(0.0385F, 0.0385F, 0.0385F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.BleedOnHitAndExplode,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBleedOnHitAndExplode"),
            //childName = "ThighR",
            //localPos = new Vector3(0F, 0.0575F, -0.0178F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.0486F, 0.0486F, 0.0486F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.MonstersOnShrineUse,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayMonstersOnShrineUse"),
            //childName = "ThighR",
            //localPos = new Vector3(0.0022F, 0.084F, 0.066F),
            //localAngles = new Vector3(352.4521F, 260.6884F, 341.5106F),
            //localScale = new Vector3(0.0246F, 0.0246F, 0.0246F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Items.RandomDamageZone,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayRandomDamageZone"),
            //childName = "LowerArmL",
            //localPos = new Vector3(0.0709F, 0.4398F, 0.0587F),
            //localAngles = new Vector3(349.218F, 235.9453F, 0F),
            //localScale = new Vector3(0.0465F, 0.0465F, 0.0465F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Fruit,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayFruit"),
            //childName = "Chest",
            //localPos = new Vector3(-0.0513F, 0.2348F, -0.1839F),
            //localAngles = new Vector3(354.7403F, 305.3714F, 336.9526F),
            //localScale = new Vector3(0.2118F, 0.2118F, 0.2118F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.AffixRed,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteHorn"),
            //childName = "Head",
            //localPos = new Vector3(0.1201F, 0.2516F, 0F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.1036F, 0.1036F, 0.1036F),
            //                            limbMask = LimbFlags.None
            //                        },
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteHorn"),
            //childName = "Head",
            //localPos = new Vector3(-0.1201F, 0.2516F, 0F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(-0.1036F, 0.1036F, 0.1036F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.AffixBlue,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteRhinoHorn"),
            //childName = "Head",
            //localPos = new Vector3(0F, 0.2648F, 0.1528F),
            //localAngles = new Vector3(315F, 0F, 0F),
            //localScale = new Vector3(0.2F, 0.2F, 0.2F),
            //                            limbMask = LimbFlags.None
            //                        },
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteRhinoHorn"),
            //childName = "Head",
            //localPos = new Vector3(0F, 0.3022F, 0.1055F),
            //localAngles = new Vector3(300F, 0F, 0F),
            //localScale = new Vector3(0.1F, 0.1F, 0.1F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.AffixWhite,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteIceCrown"),
            //childName = "Head",
            //localPos = new Vector3(0F, 0.2836F, 0F),
            //localAngles = new Vector3(270F, 0F, 0F),
            //localScale = new Vector3(0.0265F, 0.0265F, 0.0265F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.AffixPoison,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteUrchinCrown"),
            //childName = "Head",
            //localPos = new Vector3(0F, 0.2679F, 0F),
            //localAngles = new Vector3(270F, 0F, 0F),
            //localScale = new Vector3(0.0496F, 0.0496F, 0.0496F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.AffixHaunted,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEliteStealthCrown"),
            //childName = "Head",
            //localPos = new Vector3(0F, 0.2143F, 0F),
            //localAngles = new Vector3(270F, 0F, 0F),
            //localScale = new Vector3(0.065F, 0.065F, 0.065F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.CritOnUse,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayNeuralImplant"),
            //childName = "Head",
            //localPos = new Vector3(0F, 0.1861F, 0.2328F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.2326F, 0.2326F, 0.2326F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.DroneBackup,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayRadio"),
            //childName = "Pelvis",
            //localPos = new Vector3(0.0604F, 0.1269F, 0F),
            //localAngles = new Vector3(0F, 90F, 0F),
            //localScale = new Vector3(0.2641F, 0.2641F, 0.2641F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Lightning,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayLightningArmRight"),
            //childName = "UpperArmR",
            //localPos = new Vector3(0F, 0F, 0F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.3413F, 0.3413F, 0.3413F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.BurnNearby,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayPotion"),
            //childName = "Pelvis",
            //localPos = new Vector3(0.078F, 0.065F, 0F),
            //localAngles = new Vector3(359.1402F, 0.1068F, 331.8908F),
            //localScale = new Vector3(0.0307F, 0.0307F, 0.0307F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.CrippleWard,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEffigy"),
            //childName = "Pelvis",
            //localPos = new Vector3(0.0768F, -0.0002F, 0F),
            //localAngles = new Vector3(0F, 270F, 0F),
            //localScale = new Vector3(0.2812F, 0.2812F, 0.2812F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.QuestVolatileBattery,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayBatteryArray"),
            //childName = "Chest",
            //localPos = new Vector3(0F, 0.2584F, -0.0987F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.2188F, 0.2188F, 0.2188F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.GainArmor,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayElephantFigure"),
            //childName = "CalfR",
            //localPos = new Vector3(0F, 0.3011F, 0.0764F),
            //localAngles = new Vector3(77.5634F, 0F, 0F),
            //localScale = new Vector3(0.6279F, 0.6279F, 0.6279F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Recycle,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayRecycler"),
            //childName = "Chest",
            //localPos = new Vector3(0F, 0.1976F, -0.0993F),
            //localAngles = new Vector3(0F, 90F, 0F),
            //localScale = new Vector3(0.0802F, 0.0802F, 0.0802F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.FireBallDash,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayEgg"),
            //childName = "Pelvis",
            //localPos = new Vector3(0.0727F, 0.0252F, 0F),
            //localAngles = new Vector3(270F, 0F, 0F),
            //localScale = new Vector3(0.1891F, 0.1891F, 0.1891F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Cleanse,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayWaterPack"),
            //childName = "Chest",
            //localPos = new Vector3(0F, 0.1996F, -0.0489F),
            //localAngles = new Vector3(0F, 180F, 0F),
            //localScale = new Vector3(0.0821F, 0.0821F, 0.0821F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Tonic,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayTonic"),
            //childName = "Pelvis",
            //localPos = new Vector3(0.066F, 0.058F, 0F),
            //localAngles = new Vector3(0F, 90F, 0F),
            //localScale = new Vector3(0.1252F, 0.1252F, 0.1252F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Gateway,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayVase"),
            //childName = "Pelvis",
            //localPos = new Vector3(0.0807F, 0.0877F, 0F),
            //localAngles = new Vector3(0F, 90F, 0F),
            //localScale = new Vector3(0.0982F, 0.0982F, 0.0982F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Meteor,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayMeteor"),
            //childName = "Base",
            //localPos = new Vector3(0F, -1.7606F, -0.9431F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(1F, 1F, 1F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Saw,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplaySawmerang"),
            //childName = "Base",
            //localPos = new Vector3(0F, -1.7606F, -0.9431F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.1F, 0.1F, 0.1F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Blackhole,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayGravCube"),
            //childName = "Base",
            //localPos = new Vector3(0F, -1.7606F, -0.9431F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.5F, 0.5F, 0.5F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.Scanner,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayScanner"),
            //childName = "Pelvis",
            //localPos = new Vector3(0.0857F, 0.0472F, 0.0195F),
            //localAngles = new Vector3(270F, 154.175F, 0F),
            //localScale = new Vector3(0.0861F, 0.0861F, 0.0861F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.DeathProjectile,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayDeathProjectile"),
            //childName = "Pelvis",
            //localPos = new Vector3(0F, 0.028F, -0.0977F),
            //localAngles = new Vector3(0F, 180F, 0F),
            //localScale = new Vector3(0.0596F, 0.0596F, 0.0596F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.LifestealOnHit,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayLifestealOnHit"),
            //childName = "Head",
            //localPos = new Vector3(-0.2175F, 0.4404F, -0.141F),
            //localAngles = new Vector3(44.0939F, 33.5151F, 43.5058F),
            //localScale = new Vector3(0.1246F, 0.1246F, 0.1246F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });

            //            itemDisplayRules.Add(new ItemDisplayRuleSet.KeyAssetRuleGroup
            //            {
            //                keyAsset = RoR2Content.Equipment.TeamWarCry,
            //                displayRuleGroup = new DisplayRuleGroup
            //                {
            //                    rules = new ItemDisplayRule[]
            //                    {
            //                        new ItemDisplayRule
            //                        {
            //                            ruleType = ItemDisplayRuleType.ParentedPrefab,
            //                            followerPrefab = ItemDisplays.LoadDisplay("DisplayTeamWarCry"),
            //childName = "Pelvis",
            //localPos = new Vector3(0F, 0F, 0.1866F),
            //localAngles = new Vector3(0F, 0F, 0F),
            //localScale = new Vector3(0.1233F, 0.1233F, 0.1233F),
            //                            limbMask = LimbFlags.None
            //                        }
            //                    }
            //                }
            //            });
            //            #endregion

            //    itemDisplayRuleSet.keyAssetRuleGroups = itemDisplayRules.ToArray();
            //    itemDisplayRuleSet.GenerateRuntimeValues();
        }

        private static CharacterModel.RendererInfo[] SkinRendererInfos(CharacterModel.RendererInfo[] defaultRenderers, Material[] materials)
        {
            CharacterModel.RendererInfo[] newRendererInfos = new CharacterModel.RendererInfo[defaultRenderers.Length];
            defaultRenderers.CopyTo(newRendererInfos, 0);

            newRendererInfos[0].defaultMaterial = materials[0];

            return newRendererInfos;
        }
    }
}

