using BepInEx.Configuration;
using EntityStates;
using EntityStates.Railgunner.Scope;
using R2API;
using RoR2;
using RoR2.Skills;
using ShiggyMod.Modules.Quirks;
using ShiggyMod.SkillStates;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShiggyMod.Modules.Survivors
{


    internal class Shiggy : SurvivorBase
    {

        public static GameObject staticBodyPrefab; 
        public override string prefabBodyName => "Shiggy";

        public const string PLUGIN_PREFIX = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public override string survivorTokenPrefix => PLUGIN_PREFIX;

        //monster passives
        internal static SkillDef alphaconstructpassiveDef;
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
        internal static SkillDef childpassiveDef;


        //boss monster passives
        internal static SkillDef impbosspassiveDef;
        internal static SkillDef stonetitanpassiveDef;
        internal static SkillDef magmawormpassiveDef;
        internal static SkillDef overloadingwormpassiveDef;
        internal static SkillDef vagrantpassiveDef;


        //survivor passives
        internal static SkillDef acridpassiveDef;
        internal static SkillDef captainpassiveDef;
        internal static SkillDef commandopassiveDef;
        internal static SkillDef chefpassiveDef;
        internal static SkillDef falsesonpassiveDef;
        internal static SkillDef loaderpassiveDef;

        //synergy passives
        internal static SkillDef bigBangPassiveDef;
        internal static SkillDef wisperPassiveDef;
        internal static SkillDef omniboostPassiveDef;
        internal static SkillDef gachaPassiveDef;
        internal static SkillDef stoneFormPassiveDef;
        internal static SkillDef auraOfBlightPassiveDef;
        internal static SkillDef barbedSpikesPassiveDef;
        internal static SkillDef ingrainPassiveDef;
        internal static SkillDef doubleTimePassiveDef;
        internal static SkillDef blindSensesPassiveDef;

        //ultimate passives
        internal static SkillDef supernovaPassiveDef;
        internal static SkillDef reversalPassiveDef;
        internal static SkillDef machineFormPassiveDef;
        internal static SkillDef gargoyleProtectionPassiveDef;
        internal static SkillDef weatherReportPassiveDef;
        internal static SkillDef decayAwakenedPassiveDef;

        //monster actives
        internal static SkillDef alloyvultureWindBlastDef;
        internal static SkillDef beetleguardslamDef;
        internal static SkillDef bisonchargeDef;
        internal static SkillDef bronzongballDef;
        internal static SkillDef clayapothecarymortarDef;
        internal static SkillDef claytemplarminigunDef;
        internal static SkillDef elderlemurianfireblastDef;
        internal static SkillDef greaterWispBuffDef;
        internal static SkillDef impblinkDef;
        internal static SkillDef jellyfishHealDef;
        internal static SkillDef lemurianfireballDef;
        internal static SkillDef lunargolemSlideDef;
        internal static SkillDef lunarwispminigunDef;
        internal static SkillDef parentteleportDef;
        internal static SkillDef stonegolemlaserDef;
        internal static SkillDef voidreaverportalDef;
        internal static SkillDef halcyoniteGreedDef;


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
        internal static SkillDef driftersalvageDef;
        internal static SkillDef engiturretDef;
        internal static SkillDef huntressattackDef;
        internal static SkillDef mercdashDef;
        internal static SkillDef multbuffDef;
        internal static SkillDef multbuffcancelDef;
        internal static SkillDef operators141customDef;
        internal static SkillDef railgunnercryoDef;
        internal static SkillDef rexmortarDef;
        internal static SkillDef seekermeditateDef;
        internal static SkillDef voidfiendcleanseDef;

        //collab actives
        internal static SkillDef DekuOFADef;

        //shiggy skills
        internal static SkillDef decayDef;
        internal static SkillDef aircannonDef;
        internal static SkillDef bulletlaserDef;
        internal static SkillDef multiplierDef;
        internal static SkillDef chooseDef;
        internal static SkillDef giveDef;
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
        internal static SkillDef voidFormDef;
        internal static SkillDef elementalFusionPassiveDef;
        internal static SkillDef decayPlusUltraDef;
        internal static SkillDef machPunchDef;
        internal static SkillDef rapidPierceDef;


        //ultimate actives
        internal static SkillDef theWorldDef;
        internal static SkillDef extremeSpeedDef;
        internal static SkillDef deathAuraDef;
        internal static SkillDef OFAFODef;
        internal static SkillDef xBeamerDef;
        internal static SkillDef finalReleaseDef;
        internal static SkillDef blastingZoneDef;
        internal static SkillDef wildCardDef;
        internal static SkillDef lightAndDarknessDef;


        public override GameObject bodyPrefab { get; set; }
        public override GameObject displayPrefab { get; set; }

        public float sortPosition = 100f;

        //if you have more than one character, easily create a config to enable/disable them like this
        public override ConfigEntry<bool> characterEnabledConfig => null; //Modules.Config.CharacterEnableConfig(bodyName);

        public override BodyInfo bodyInfo { get; set; } = new BodyInfo
        {
            autoCalculateLevelStats = false,
            armor = 10f,
            armorGrowth = 0.5f,
            bodyName = "ShiggyBody",
            bodyNameToken = PLUGIN_PREFIX + "NAME",
            bodyColor = Color.magenta,
            characterPortrait = Modules.ShiggyAsset.LoadCharacterIcon("Shiggy"),
            crosshair = Modules.ShiggyAsset.LoadCrosshair("Standard"),
            damage = 1f,
            damageGrowth = 1f,
            healthGrowth = 41f,
            healthRegen = 1f,
            jumpCount = 2,
            maxHealth = 141f,
            moveSpeed = 7f,
            subtitleNameToken = PLUGIN_PREFIX + "SUBTITLE",

            aimOriginPosition = new Vector3(0f, 0f, 0f),
            //podPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/SurvivorPod")
        };

        //internal static Material ShiggyMat = Modules.ShiggyAsset.CreateMaterial("ShiggyMat");
        //internal static Material matShiggyBody = Modules.ShiggyAsset.CreateMaterial("matShiggyBody");
        //internal static Material matShiggyEyes = Modules.ShiggyAsset.CreateMaterial("matShiggyEyes");
        //internal static Material matShiggyFace = Modules.ShiggyAsset.CreateMaterial("matShiggyFace");
        //internal static Material matShiggyHair = Modules.ShiggyAsset.CreateMaterial("matShiggyHair");
        //internal override int mainRendererIndex { get; set; } = 1;
        protected override void InitializeEntityStateMachine()
        {
            // Let base wire the Body machine to ShiggyCharacterMain (and spawn if you set it)
            base.InitializeEntityStateMachine();

            // Ensure a single NetworkStateMachine on the root
            var nsm = bodyPrefab.GetComponent<NetworkStateMachine>();
            if (!nsm) nsm = bodyPrefab.AddComponent<NetworkStateMachine>();
            if (nsm.stateMachines == null) nsm.stateMachines = System.Array.Empty<EntityStateMachine>();

            // Find or add the dedicated "Flight" machine
            var flightESM = EntityStateMachine.FindByCustomName(bodyPrefab, "Flight");
            if (!flightESM)
            {
                flightESM = bodyPrefab.AddComponent<EntityStateMachine>();
                flightESM.customName = "Flight";
            }

            // Idle is a no-op state for this side-machine
            flightESM.initialStateType = new SerializableEntityStateType(typeof(EntityStates.Idle));
            flightESM.mainStateType = new SerializableEntityStateType(typeof(EntityStates.Idle));

            // Append to NetworkStateMachine without clobbering existing entries (Body/Weapon/etc.)
            var list = new System.Collections.Generic.List<EntityStateMachine>(nsm.stateMachines);
            if (!list.Contains(flightESM))
            {
                list.Add(flightESM);
                nsm.stateMachines = list.ToArray();
            }

            // Register flight states (CharacterMain already added by base via characterMainState)
            //Modules.Content.AddEntityState(typeof(EntityStates.Idle));
            Modules.Content.AddEntityState(typeof(AirWalk));
        }
        public override CustomRendererInfo[] customRendererInfos { get; set; } = new CustomRendererInfo[] {

                new CustomRendererInfo
                {
                    childName = "shigarakiBodyMesh",
                    material = Materials.CreateHopooMaterial("matShiggyBody", 1f),
                },
                new CustomRendererInfo
                {
                    childName = "shigarakiPantsMesh",
                    material = Materials.CreateHopooMaterial("matShiggyBody", 1f),
                },
                new CustomRendererInfo
                {
                    childName = "shigarakiEyesMesh",
                    material = Materials.CreateHopooMaterial("matShiggyEyes", 1f),
                },
                new CustomRendererInfo
                {
                    childName = "shigarakiFaceMesh",
                    material = Materials.CreateHopooMaterial("matShiggyFace", 1f),
                },
                new CustomRendererInfo
                {
                    childName = "shigarakiHairMesh",
                    material = Materials.CreateHopooMaterial("matShiggyHair", 1f),
                },
                new CustomRendererInfo
                {
                    childName = "Hand",
                    material = Materials.CreateHopooMaterial("ShiggyMat", 1f),
                    ignoreOverlays = true
                },
        };



        public override Type characterMainState => typeof(ShiggyCharacterMain);

        //item display stuffs
        //internal override ItemDisplayRuleSet itemDisplayRuleSet { get; set; }
        //internal override List<ItemDisplayRuleSet.KeyAssetRuleGroup> itemDisplayRules { get; set; }

        public override UnlockableDef characterUnlockableDef => null;
        private static UnlockableDef masterySkinUnlockableDef;


        public override void InitializeCharacter()
        {
            base.InitializeCharacter();
            bodyPrefab.AddComponent<ShiggyController>();
            bodyPrefab.AddComponent<BuffController>();

            staticBodyPrefab = bodyPrefab;
        }

        public override void InitializeUnlockables()
        {
            //masterySkinUnlockableDef = Modules.Unlockables.AddUnlockable<Achievements.MasteryAchievement>(true);
        }

        public override void InitializeDoppelganger(string clone)
        {
            GameObject newMasterGameObject = PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/" + clone + "MonsterMaster"), bodyInfo.bodyName + "MonsterMaster", true);
            CharacterMaster master = newMasterGameObject.GetComponent<CharacterMaster>();
            master.bodyPrefab = bodyPrefab;

            //can setup AI skill drivers if needed, uncomment below

            //AISkillDriver[] drivers = master.GetComponents<AISkillDriver>();
            //foreach (AISkillDriver driver in drivers)
            //{
            //    UnityEngine.Object.DestroyImmediate(driver);
            //}

            ////Fire as much as possible in range.
            //AISkillDriver armamentBarrage = master.gameObject.AddComponent<AISkillDriver>();
            //armamentBarrage.customName = "Lee: Hyperreal - Armament Barrage";
            //armamentBarrage.skillSlot = SkillSlot.Primary;
            //armamentBarrage.requireSkillReady = true;
            //armamentBarrage.requireEquipmentReady = false;
            //armamentBarrage.minDistance = 0;
            //armamentBarrage.maxDistance = 10f;
            //armamentBarrage.selectionRequiresAimTarget = true;
            //armamentBarrage.selectionRequiresOnGround = false;
            //armamentBarrage.selectionRequiresAimTarget = true;
            //armamentBarrage.moveTargetType = AISkillDriver.TargetType.CurrentEnemy;
            //armamentBarrage.activationRequiresTargetLoS = true;
            //armamentBarrage.activationRequiresAimTargetLoS = true;
            //armamentBarrage.activationRequiresAimConfirmation = true;
            //armamentBarrage.movementType = AISkillDriver.MovementType.ChaseMoveTarget;
            //armamentBarrage.buttonPressType = AISkillDriver.ButtonPressType.TapContinuous;
            //armamentBarrage.aimType = AISkillDriver.AimType.AtCurrentEnemy;
            //armamentBarrage.resetCurrentEnemyOnNextDriverSelection = true;

            Modules.Content.AddMasterPrefab(newMasterGameObject);
        }



        public override void InitializeHitboxes()
        {
            ChildLocator childLocator = bodyPrefab.GetComponentInChildren<ChildLocator>();
            GameObject model = childLocator.gameObject;

            //Transform hitboxTransform = childLocator.FindChild("SmallHitbox");
            //Modules.Prefabs.SetupHitbox(model, hitboxTransform, "SmallHitbox");
            Modules.Prefabs.SetupHitbox(prefabCharacterModel.gameObject, childLocator.FindChild("SmallHitbox"), "SmallHitbox");
            Modules.Prefabs.SetupHitbox(prefabCharacterModel.gameObject, childLocator.FindChild("DetectSmallHitbox"), "DetectSmallHitbox");
            Modules.Prefabs.SetupHitbox(prefabCharacterModel.gameObject, childLocator.FindChild("FrontHitbox"), "FrontHitbox");
            Modules.Prefabs.SetupHitbox(prefabCharacterModel.gameObject, childLocator.FindChild("AroundHitbox"), "AroundHitbox");
            Modules.Prefabs.SetupHitbox(prefabCharacterModel.gameObject, childLocator.FindChild("DecayHitbox"), "DecayHitbox");

        }



        public override void InitializeSkills()
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
            skillloc.passiveSkill.icon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("allforone");
            skillloc.passiveSkill.keywordToken = prefix + "KEYWORD_PASSIVE";
            #endregion

            #region Primary

            decayDef = Modules.Skills.CreateSkillDef(new SkillDefInfo
            {

                skillName = prefix + "DECAY_NAME",
                skillNameToken = prefix + "DECAY_NAME",
                skillDescriptionToken = prefix + "DECAY_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("decay"),
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("bulletlaser"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BulletLaser)),
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
            #endregion

            #region Utility
            aircannonDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "AIRCANNON_NAME",
                skillNameToken = prefix + "AIRCANNON_NAME",
                skillDescriptionToken = prefix + "AIRCANNON_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("aircannon"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.AirCannon)),
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
                keywordTokens = new string[] { "KEYWORD_AGILE", prefix + "KEYWORD_DECAY", "KEYWORD_STUNNING" }

            });

            #endregion

            #region Special
            multiplierDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "MULTIPLIER_NAME",
                skillNameToken = prefix + "MULTIPLIER_NAME",
                skillDescriptionToken = prefix + "MULTIPLIER_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("multiplier"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Multiplier)),
                activationStateMachineName = "Slide",
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

            #region Extra Skills

            emptySkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "AFO_NAME",
                skillNameToken = prefix + "AFO_NAME",
                skillDescriptionToken = prefix + "AFO_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.skinBuffIcon,
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("allforone"),
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
            giveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "GIVESKILL_NAME",
                skillNameToken = prefix + "GIVESKILL_NAME",
                skillDescriptionToken = prefix + "GIVESKILL_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("allforone"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.GiveSkill)),
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("allforone"),
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
            // passives
            Shiggy.alphaconstructpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "ALPHACONSTRUCT_NAME",
                skillNameToken = prefix + "ALPHACONSTRUCT_NAME",
                skillDescriptionToken = prefix + "ALPHACONSTRUCT_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.AlphaConstruct_BarrierPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.Beetle_StrengthPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.Gup_SpikyBodyPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.Larva_AcidJumpPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.LesserWisp_HastePassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.LunarExploder_LunarBarrierPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.HermitCrab_MortarPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.Pest_JumpPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.Vermin_SpeedPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.MiniMushrum_HealingAuraPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.RoboBallMini_SolusBoostPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.VoidBarnacle_VoidMortarPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.VoidJailer_GravityPassive),
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
            Shiggy.childpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "CHILD_NAME",
                skillNameToken = prefix + "CHILD_NAME",
                skillDescriptionToken = prefix + "CHILD_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.Child_EmergencyTeleportPassive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Child)),
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
                skillIcon = QuirkIconBank.Get(QuirkId.ImpBoss_BleedPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.StoneTitan_StoneSkinPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.MagmaWorm_BlazingAuraPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.OverloadingWorm_LightningAuraPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.Vagrant_OrbPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.Acrid_PoisonPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.Commando_DoubleTapPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.Captain_MicrobotsPassive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.Loader_ScrapBarrierPassive),
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
            Shiggy.falsesonpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "FALSESON_NAME",
                skillNameToken = prefix + "FALSESON_NAME",
                skillDescriptionToken = prefix + "FALSESON_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.FalseSon_StolenInheritancePassive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.FalseSon)),
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
            Shiggy.chefpassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "CHEF_NAME",
                skillNameToken = prefix + "CHEF_NAME",
                skillDescriptionToken = prefix + "CHEF_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.Chef_OilBurstPassive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Chef)),
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
                skillIcon = QuirkIconBank.Get(QuirkId.Vulture_WindBlastActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.AlloyVultureWindBlast)),
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
            Shiggy.beetleguardslamDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BEETLEGUARD_NAME",
                skillNameToken = prefix + "BEETLEGUARD_NAME",
                skillDescriptionToken = prefix + "BEETLEGUARD_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.BeetleGuard_SlamActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BeetleGuardSlam)),
                activationStateMachineName = "Body",
                baseMaxStock = 1,
                baseRechargeInterval = 8f,
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
                skillIcon = QuirkIconBank.Get(QuirkId.Bison_ChargeActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BisonCharge)),
                activationStateMachineName = "Body",
                baseMaxStock = 1,
                baseRechargeInterval = 8f,
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
                skillIcon = QuirkIconBank.Get(QuirkId.Bell_SpikedBallActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BronzongBall)),
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
            Shiggy.clayapothecarymortarDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "APOTHECARY_NAME",
                skillNameToken = prefix + "APOTHECARY_NAME",
                skillDescriptionToken = prefix + "APOTHECARY_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.ClayApothecary_MortarActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ClayApothecaryMortar)),
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
                skillIcon = QuirkIconBank.Get(QuirkId.ClayTemplar_MinigunActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ClayTemplarMinigun)),
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
                keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_TAR" }
            });
            Shiggy.elderlemurianfireblastDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "ELDERLEMURIAN_NAME",
                skillNameToken = prefix + "ELDERLEMURIAN_NAME",
                skillDescriptionToken = prefix + "ELDERLEMURIAN_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.ElderLemurian_FireBlastActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ElderLemurianFireBlastCharge)),
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
            Shiggy.greaterWispBuffDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "GREATERWISP_NAME",
                skillNameToken = prefix + "GREATERWISP_NAME",
                skillDescriptionToken = prefix + "GREATERWISP_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.GreaterWisp_SpiritBoostActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.GreaterWispBuff)),
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
                skillIcon = QuirkIconBank.Get(QuirkId.Imp_BlinkActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ImpBlink)),
                activationStateMachineName = "Slide",
                baseMaxStock = 3,
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
            Shiggy.jellyfishHealDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "JELLYFISH_NAME",
                skillNameToken = prefix + "JELLYFISH_NAME",
                skillDescriptionToken = prefix + "JELLYFISH_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.Jellyfish_HealActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.JellyfishHeal)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 12f,
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
                skillIcon = QuirkIconBank.Get(QuirkId.Lemurian_FireballActive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.LunarGolem_SlideResetActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.LunarGolemSlide)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 20f, //16-> 20 balance change due to apex
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
                skillIcon = QuirkIconBank.Get(QuirkId.LunarWisp_MinigunActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.LunarWispMinigun)),
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
                keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_CRIPPLE" }
            });
            Shiggy.parentteleportDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "PARENT_NAME",
                skillNameToken = prefix + "PARENT_NAME",
                skillDescriptionToken = prefix + "PARENT_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.Parent_TeleportActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ParentTeleport)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 2,
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
                keywordTokens = new string[] { "KEYWORD_AGILE", prefix + "KEYWORD_DECAY", "KEYWORD_STUNNING" }
            });
            Shiggy.stonegolemlaserDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "STONEGOLEM_NAME",
                skillNameToken = prefix + "STONEGOLEM_NAME",
                skillDescriptionToken = prefix + "STONEGOLEM_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.StoneGolem_LaserActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.StoneGolemLaserCharge)),
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
                keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_STUNNING" }
            });
            Shiggy.voidreaverportalDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "VOIDREAVER_NAME",
                skillNameToken = prefix + "VOIDREAVER_NAME",
                skillDescriptionToken = prefix + "VOIDREAVER_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.VoidReaver_PortalActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.VoidReaverPortal)),
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

            Shiggy.beetlequeenSummonDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BEETLEQUEEN_NAME",
                skillNameToken = prefix + "BEETLEQUEEN_NAME",
                skillDescriptionToken = prefix + "BEETLEQUEEN_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.BeetleQueen_SummonActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BeetleQueenSummon)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 30f,
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
                skillIcon = QuirkIconBank.Get(QuirkId.Grandparent_SunActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.GrandparentSun)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 5f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = true,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = true,
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
                skillIcon = QuirkIconBank.Get(QuirkId.Grovetender_ChainActive),
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
            Shiggy.claydunestriderbuffDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "CLAYDUNESTRIDER_NAME",
                skillNameToken = prefix + "CLAYDUNESTRIDER_NAME",
                skillDescriptionToken = prefix + "CLAYDUNESTRIDER_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.ClayDunestrider_TarBoostActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ClayDunestriderBuff)),
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
            Shiggy.soluscontrolunityknockupDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "SOLUSCONTROLUNIT_NAME",
                skillNameToken = prefix + "SOLUSCONTROLUNIT_NAME",
                skillDescriptionToken = prefix + "SOLUSCONTROLUNIT_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.SolusControlUnit_KnockupActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.SolusControlUnitKnockup)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 14f,
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
                skillIcon = QuirkIconBank.Get(QuirkId.XIConstruct_BeamActive),
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
                skillIcon = QuirkIconBank.Get(QuirkId.VoidDevastator_MissilesActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.VoidDevastatorHoming)),
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
                skillIcon = QuirkIconBank.Get(QuirkId.Scavenger_ThqwibActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ScavengerThqwibs)),
                activationStateMachineName = "Weapon",
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

            Shiggy.artificerflamethrowerDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "ARTIFICERFLAMETHROWER_NAME",
                skillNameToken = prefix + "ARTIFICERFLAMETHROWER_NAME",
                skillDescriptionToken = prefix + "ARTIFICERFLAMETHROWER_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.Artificer_FlamethrowerActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ArtificerFlamethrower)),
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
            Shiggy.artificericewallDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "ARTIFICERICEWALL_NAME",
                skillNameToken = prefix + "ARTIFICERICEWALL_NAME",
                skillDescriptionToken = prefix + "ARTIFICERICEWALL_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.Artificer_IceWallActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ArtificerIceWall)),
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
            Shiggy.artificerlightningorbDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "ARTIFICERLIGHTNINGORB_NAME",
                skillNameToken = prefix + "ARTIFICERLIGHTNINGORB_NAME",
                skillDescriptionToken = prefix + "ARTIFICERLIGHTNINGORB_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.Artificer_LightningOrbActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ArtificerChargeLightningOrb)),
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
            Shiggy.banditlightsoutDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BANDIT_NAME",
                skillNameToken = prefix + "BANDIT_NAME",
                skillDescriptionToken = prefix + "BANDIT_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.Bandit_LightsOutActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BanditPrepLightsOut)),
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
                keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_SLAYER" }
            });
            Shiggy.banditlightsoutDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "DRIFTERSALVAGE_NAME",
                skillNameToken = prefix + "DRIFTERSALVAGE_NAME",
                skillDescriptionToken = prefix + "DRIFTERSALVAGE_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.Drifter_SalvageActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.DrifterSalvage)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 30f,
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
            Shiggy.engiturretDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "ENGI_NAME",
                skillNameToken = prefix + "ENGI_NAME",
                skillDescriptionToken = prefix + "ENGI_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.Engineer_TurretActive),
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
                keywordTokens = new string[] { "KEYWORD_AGILE" }
            });
            Shiggy.huntressattackDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "HUNTRESS_NAME",
                skillNameToken = prefix + "HUNTRESS_NAME",
                skillDescriptionToken = prefix + "HUNTRESS_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.Huntress_FlurryActive),
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
                keywordTokens = new string[] { "KEYWORD_AGILE" }
            });
            Shiggy.mercdashDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "MERC_NAME",
                skillNameToken = prefix + "MERC_NAME",
                skillDescriptionToken = prefix + "MERC_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.Merc_DashActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.MercDash)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 12f,
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
                skillIcon = QuirkIconBank.Get(QuirkId.MULT_PowerStanceActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.MultBuff)),
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
                keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_SLAYER" }
            });
            Shiggy.multbuffcancelDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "MULTBUFFCANCEL_NAME",
                skillNameToken = prefix + "MULTBUFFCANCEL_NAME",
                skillDescriptionToken = prefix + "MULTBUFFCANCEL_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.MULT_PowerStanceCancelActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.MultBuffCancel)),
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
                keywordTokens = new string[] { "KEYWORD_AGILE", "KEYWORD_SLAYER" }
            });
            Shiggy.operators141customDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "OPERATORS141CUSTOM_NAME",
                skillNameToken = prefix + "OPERATORS141CUSTOM_NAME",
                skillDescriptionToken = prefix + "OPERATORS141CUSTOM_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.Operator_S141CustomActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.OperatorS141Custom)),
                activationStateMachineName = "Weapon",
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
                keywordTokens = new string[] { "KEYWORD_AGILE" }
            });
            Shiggy.railgunnercryoDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "RAILGUNNNER_NAME",
                skillNameToken = prefix + "RAILGUNNNER_NAME",
                skillDescriptionToken = prefix + "RAILGUNNNER_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.Railgunner_CryoActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.RailgunnerCryoCharge)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 15f,
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
                skillIcon = QuirkIconBank.Get(QuirkId.REX_MortarActive),
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
                keywordTokens = new string[] { "KEYWORD_AGILE" }
            });
            Shiggy.seekermeditateDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "SEEKER_NAME",
                skillNameToken = prefix + "SEEKER_NAME",
                skillDescriptionToken = prefix + "SEEKER_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.REX_MortarActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.SeekerMeditate)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 16f,
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
            Shiggy.voidfiendcleanseDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "VOIDFIEND_NAME",
                skillNameToken = prefix + "VOIDFIEND_NAME",
                skillDescriptionToken = prefix + "VOIDFIEND_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.VoidFiend_CleanseActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.VoidFiendCleanse)),
                activationStateMachineName = "Slide",
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
                keywordTokens = new string[] { "KEYWORD_AGILE" }
            });
            Shiggy.halcyoniteGreedDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "HALCYONITE_NAME",
                skillNameToken = prefix + "HALCYONITE_NAME",
                skillDescriptionToken = prefix + "HALCYONITE_DESCRIPTION",
                skillIcon = QuirkIconBank.Get(QuirkId.Halcyonite_GreedActive),
                activationState = new SerializableEntityStateType(typeof(SkillStates.VoidFiendCleanse)),
                activationStateMachineName = "Slide",
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
                keywordTokens = new string[] { "KEYWORD_AGILE" }
            });
            #endregion

            #region Synergy Active Skills

            sweepingBeamDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "SWEEPINGBEAM_NAME",
                skillNameToken = prefix + "SWEEPINGBEAM_NAME",
                skillDescriptionToken = prefix + "SWEEPINGBEAM_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("SweepingBeam"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.SweepingBeam)),
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("BlackHoleGlaive"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BlackHoleGlaive)),
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
            gravitationalDownforceDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "GRAVITATIONALDOWNFORCE_NAME",
                skillNameToken = prefix + "GRAVITATIONALDOWNFORCE_NAME",
                skillDescriptionToken = prefix + "GRAVITATIONALDOWNFORCE_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("GravitationalDownforce"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.SeekerMeditate)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 15f,
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("WindShield"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.WindShield)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 14f,
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("Genesis"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Genesis)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 15f,
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("Refresh"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Refresh)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 60f,
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("Expunge"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Expunge)),
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("ShadowClaw"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ShadowClaw)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 14f,
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("OrbitalStrike"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.OrbitalStrike)),
                activationStateMachineName = "Slide",
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
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            thunderclapDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "THUNDERCLAP_NAME",
                skillNameToken = prefix + "THUNDERCLAP_NAME",
                skillDescriptionToken = prefix + "THUNDERCLAP_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("Thunderclap"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.Thunderclap)),
                activationStateMachineName = "Weapon",
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
            blastBurnDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BLASTBURN_NAME",
                skillNameToken = prefix + "BLASTBURN_NAME",
                skillDescriptionToken = prefix + "BLASTBURN_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("BlastBurn"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BlastBurn)),
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
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            barrierJellyDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BARRIERJELLY_NAME",
                skillNameToken = prefix + "BARRIERJELLY_NAME",
                skillDescriptionToken = prefix + "BARRIERJELLY_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("BarrierJelly"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BarrierJelly)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 16f,
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
            mechStanceDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "MECHSTANCE_NAME",
                skillNameToken = prefix + "MECHSTANCE_NAME",
                skillDescriptionToken = prefix + "MECHSTANCE_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("MechStance"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.MechStance)),
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
                mustKeyPress = false,
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("WindSlash"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.WindSlash)),
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
            DekuOFADef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "DEKUOFA_NAME",
                skillNameToken = prefix + "DEKUOFA_NAME",
                skillDescriptionToken = prefix + "DEKUOFA_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("OFA"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.DekuOFA)),
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
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });            
            limitBreakDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "LIMITBREAK_NAME",
                skillNameToken = prefix + "LIMITBREAK_NAME",
                skillDescriptionToken = prefix + "LIMITBREAK_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("LimitBreak"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.LimitBreak)),
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
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            voidFormDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "VOIDFORM_NAME",
                skillNameToken = prefix + "VOIDFORM_NAME",
                skillDescriptionToken = prefix + "VOIDFORM_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("VoidForm"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.VoidForm)),
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
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            decayPlusUltraDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "DECAYPLUSULTRA_NAME",
                skillNameToken = prefix + "DECAYPLUSULTRA_NAME",
                skillDescriptionToken = prefix + "DECAYPLUSULTRA_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("DecayPlusUltra"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.DecayPlusUltra)),
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
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            machPunchDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "MACHPUNCH_NAME",
                skillNameToken = prefix + "MACHPUNCH_NAME",
                skillDescriptionToken = prefix + "MACHPUNCH_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("MachPunch"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.MachPunch)),
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
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            rapidPierceDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "RAPIDPIERCE_NAME",
                skillNameToken = prefix + "RAPIDPIERCE_NAME",
                skillDescriptionToken = prefix + "RAPIDPIERCE_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("RapidPierce"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.RapidPierce)),
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
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            theWorldDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "THEWORLD_NAME",
                skillNameToken = prefix + "THEWORLD_NAME",
                skillDescriptionToken = prefix + "THEWORLD_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("TheWorld"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.TheWorld)),
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
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            extremeSpeedDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "EXTREMESPEED_NAME",
                skillNameToken = prefix + "EXTREMESPEED_NAME",
                skillDescriptionToken = prefix + "EXTREMESPEED_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("ExtremeSpeed"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ExtremeSpeed)),
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
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            deathAuraDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "DEATHAURA_NAME",
                skillNameToken = prefix + "DEATHAURA_NAME",
                skillDescriptionToken = prefix + "DEATHAURA_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("DeathAura"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.DeathAura)),
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
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            OFAFODef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "OFAFO_NAME",
                skillNameToken = prefix + "OFAFO_NAME",
                skillDescriptionToken = prefix + "OFAFO_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("OneForAllForOne"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.OneForAllForOne)),
                activationStateMachineName = "Body",
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
            xBeamerDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "XBEAMER_NAME",
                skillNameToken = prefix + "XBEAMER_NAME",
                skillDescriptionToken = prefix + "XBEAMER_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("XBeamer"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.XBeamer)),
                activationStateMachineName = "Body",
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
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            finalReleaseDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "FINALRELEASE_NAME",
                skillNameToken = prefix + "FINALRELEASE_NAME",
                skillDescriptionToken = prefix + "FINALRELEASE_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("FinalRelease"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.FinalRelease)),
                activationStateMachineName = "Body",
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
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            blastingZoneDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BLASTINGZONE_NAME",
                skillNameToken = prefix + "BLASTINGZONE_NAME",
                skillDescriptionToken = prefix + "BLASTINGZONE_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("BlastingZone"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BlastingZone)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 16f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            wildCardDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "WILDCARD_NAME",
                skillNameToken = prefix + "WILDCARD_NAME",
                skillDescriptionToken = prefix + "WILDCARD_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("WildCard"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.WildCard)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 60f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = true,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,
                keywordTokens = new string[] { "KEYWORD_AGILE" }

            });
            lightAndDarknessDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "LIGHTANDDARKNESS_NAME",
                skillNameToken = prefix + "LIGHTANDDARKNESS_NAME",
                skillDescriptionToken = prefix + "LIGHTANDDARKNESS_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("LightAndDarkness"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.LightAndDarkness)),
                activationStateMachineName = "Slide",
                baseMaxStock = 1,
                baseRechargeInterval = 30f,
                beginSkillCooldownOnSkillEnd = true,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = false,
                interruptPriority = InterruptPriority.Skill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = true,
                mustKeyPress = true,
                cancelSprintingOnActivation = true,
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("BigBang"),
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("Wisper"),
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("Omniboost"),
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("Gacha"),
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("StoneForm"),
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("AuraOfBlight"),
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("BarbedSpikes"),
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
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("Ingrain"),
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
            elementalFusionPassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "ELEMENTALFUSION_NAME",
                skillNameToken = prefix + "ELEMENTALFUSION_NAME",
                skillDescriptionToken = prefix + "ELEMENTALFUSION_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("ElementalFusion"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ElementalFusionPassive)),
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
            doubleTimePassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "DOUBLETIME_NAME",
                skillNameToken = prefix + "DOUBLETIME_NAME",
                skillDescriptionToken = prefix + "DOUBLETIME_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("DoubleTime"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.DoubleTimePassive)),
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
            blindSensesPassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "BLINDSENSES_NAME",
                skillNameToken = prefix + "BLINDSENSES_NAME",
                skillDescriptionToken = prefix + "BLINDSENSES_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("BlindSenses"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.BlindSensesPassive)),
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
            supernovaPassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "SUPERNOVA_NAME",
                skillNameToken = prefix + "SUPERNOVA_NAME",
                skillDescriptionToken = prefix + "SUPERNOVA_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("Supernova"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.SupernovaPassive)),
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
            reversalPassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "REVERSAL_NAME",
                skillNameToken = prefix + "REVERSAL_NAME",
                skillDescriptionToken = prefix + "REVERSAL_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("Reversal"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.ReversalPassive)),
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
            machineFormPassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "MACHINEFORM_NAME",
                skillNameToken = prefix + "MACHINEFORM_NAME",
                skillDescriptionToken = prefix + "MACHINEFORM_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("MachineForm"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.MachineFormPassive)),
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
            gargoyleProtectionPassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "GARGOYLEPROTECTION_NAME",
                skillNameToken = prefix + "GARGOYLEPROTECTION_NAME",
                skillDescriptionToken = prefix + "GARGOYLEPROTECTION_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("GargoyleProtection"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.GargoyleProtectionPassive)),
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
            weatherReportPassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "WEATHERREPORT_NAME",
                skillNameToken = prefix + "WEATHERREPORT_NAME",
                skillDescriptionToken = prefix + "WEATHERREPORT_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("WeatherReport"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.WeatherReportPassive)),
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
            decayAwakenedPassiveDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = prefix + "DECAYAWAKENED_NAME",
                skillNameToken = prefix + "DECAYAWAKENED_NAME",
                skillDescriptionToken = prefix + "DECAYAWAKENED_DESCRIPTION",
                skillIcon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("DecayAwakened"),
                activationState = new SerializableEntityStateType(typeof(SkillStates.DecayAwakenedPassive)),
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


            Skills.AddPrimarySkills(bodyPrefab, new SkillDef[]
            {
                    decayDef,
            });
            Skills.AddSecondarySkills(this.bodyPrefab, new SkillDef[]
            {
                    bulletlaserDef,
            });
            Skills.AddUtilitySkills(this.bodyPrefab, new SkillDef[]
            {
                    aircannonDef,
            });
            Skills.AddSpecialSkills(this.bodyPrefab, new SkillDef[]
            {
                    multiplierDef,
            });
            Modules.Skills.AddFirstExtraSkills(bodyPrefab, new SkillDef[]
            {
                    emptySkillDef,
            });
            Modules.Skills.AddSecondExtraSkills(bodyPrefab, new SkillDef[]
            {
                    emptySkillDef,
            });
            Modules.Skills.AddThirdExtraSkills(bodyPrefab, new SkillDef[]
            {
                    emptySkillDef,
            });
            Modules.Skills.AddFourthExtraSkills(bodyPrefab, new SkillDef[]
            {
                    emptySkillDef,
            });

            SkillDef[] allSkills = new SkillDef[]
            {
                    alphaconstructpassiveDef,
                    beetlepassiveDef,
                    pestpassiveDef,
                    verminpassiveDef,
                    guppassiveDef,
                    hermitcrabpassiveDef,
                    larvapassiveDef,
                    lesserwisppassiveDef,
                    lunarexploderpassiveDef,
                    minimushrumpassiveDef,
                    roboballminibpassiveDef,
                    voidbarnaclepassiveDef,
                    voidjailerpassiveDef,
                    impbosspassiveDef,
                    stonetitanpassiveDef,
                    magmawormpassiveDef,
                    overloadingwormpassiveDef,
                    vagrantpassiveDef,
                    childpassiveDef,
                    acridpassiveDef,
                    commandopassiveDef,
                    captainpassiveDef,
                    loaderpassiveDef,
                    bigBangPassiveDef,
                    wisperPassiveDef,
                    omniboostPassiveDef,
                    gachaPassiveDef,
                    stoneFormPassiveDef,
                    auraOfBlightPassiveDef,
                    barbedSpikesPassiveDef,
                    ingrainPassiveDef,
                    doubleTimePassiveDef,
                    blindSensesPassiveDef,
                    supernovaPassiveDef,
                    reversalPassiveDef,
                    machineFormPassiveDef,
                    gargoyleProtectionPassiveDef,
                    weatherReportPassiveDef,
                    decayAwakenedPassiveDef,
                    alloyvultureWindBlastDef,
                    beetleguardslamDef,
                    bisonchargeDef,
                    bronzongballDef,
                    clayapothecarymortarDef,
                    claytemplarminigunDef,
                    elderlemurianfireblastDef,
                    greaterWispBuffDef,
                    impblinkDef,
                    jellyfishHealDef,
                    lemurianfireballDef,
                    lunargolemSlideDef,
                    lunarwispminigunDef,
                    parentteleportDef,
                    stonegolemlaserDef,
                    voidreaverportalDef,
                    beetlequeenSummonDef,
                    grandparentsunDef,
                    grovetenderChainDef,
                    claydunestriderbuffDef,
                    soluscontrolunityknockupDef,
                    xiconstructbeamDef,
                    voiddevastatorhomingDef,
                    scavengerthqwibDef,
                    artificerflamethrowerDef,
                    artificericewallDef,
                    artificerlightningorbDef,
                    banditlightsoutDef,
                    engiturretDef,
                    huntressattackDef,
                    mercdashDef,
                    multbuffDef,
                    railgunnercryoDef,
                    rexmortarDef,
                    voidfiendcleanseDef,
                    DekuOFADef,
                    sweepingBeamDef,
                    blackholeGlaiveDef,
                    gravitationalDownforceDef,
                    windShieldDef,
                    genesisDef,
                    refreshDef,
                    expungeDef,
                    shadowClawDef,
                    orbitalStrikeDef,
                    thunderclapDef,
                    blastBurnDef,
                    barrierJellyDef,
                    mechStanceDef,
                    windSlashDef,
                    limitBreakDef,
                    voidFormDef,
                    elementalFusionPassiveDef,
                    decayPlusUltraDef,
                    machPunchDef,
                    rapidPierceDef,
                    theWorldDef,
                    extremeSpeedDef,
                    deathAuraDef,
                    OFAFODef,
                    xBeamerDef,
                    finalReleaseDef,
                    blastingZoneDef,
                    wildCardDef,
                    lightAndDarknessDef,
            };

            if (Config.allowAllSkills.Value)
            {
                Skills.AddPrimarySkills(bodyPrefab, allSkills);
                Skills.AddSecondarySkills(bodyPrefab, allSkills);
                Skills.AddUtilitySkills(bodyPrefab, allSkills);
                Skills.AddSpecialSkills(bodyPrefab, allSkills);
                Skills.AddFirstExtraSkills(bodyPrefab, allSkills);
                Skills.AddSecondExtraSkills(bodyPrefab, allSkills);
                Skills.AddThirdExtraSkills(bodyPrefab, allSkills);
                Skills.AddFourthExtraSkills(bodyPrefab, allSkills);
            }
            #endregion
        }


        public override void InitializeSkins()
        {
            //GameObject model = bodyPrefab.GetComponentInChildren<ModelLocator>().modelTransform.gameObject;
            //CharacterModel characterModel = model.GetComponent<CharacterModel>();

            //ModelSkinController skinController = model.AddComponent<ModelSkinController>();

            ModelSkinController skinController = prefabCharacterModel.gameObject.GetComponent<ModelSkinController>();
            if (!skinController)
            {
                skinController = prefabCharacterModel.gameObject.AddComponent<ModelSkinController>();
            }

            //ChildLocator childLocator = model.GetComponent<ChildLocator>();

            //SkinnedMeshRenderer mainRenderer = characterModel.mainSkinnedMeshRenderer;

            //CharacterModel.RendererInfo[] defaultRenderers = characterModel.baseRendererInfos;

            ChildLocator childLocator = prefabCharacterModel.GetComponent<ChildLocator>();

            CharacterModel.RendererInfo[] defaultRendererInfos = prefabCharacterModel.baseRendererInfos;

            List<SkinDef> skins = new List<SkinDef>();

            #region DefaultSkin
            Material defaultMat = Modules.ShiggyAsset.CreateMaterial("ShiggyMat", 0f, Color.white, 0f);
            Material matShiggyBody = Modules.ShiggyAsset.CreateMaterial("matShiggyBody", 0f, Color.white, 0f);
            Material matShiggyEyes = Modules.ShiggyAsset.CreateMaterial("matShiggyEyes", 0f, Color.white, 0f);
            Material matShiggyFace = Modules.ShiggyAsset.CreateMaterial("matShiggyFace", 0f, Color.white, 0f);
            Material matShiggyHair = Modules.ShiggyAsset.CreateMaterial("matShiggyHair", 0f, Color.white, 0f);

            SkinDef defaultSkin = Modules.Skins.CreateSkinDef(PLUGIN_PREFIX + "DEFAULT_SKIN_NAME",
                ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("ShiggyBaseSkin"),
                defaultRendererInfos,
                prefabCharacterModel.gameObject);

            //these are your Mesh Replacements. The order here is based on your CustomRendererInfos from earlier
            //pass in meshes as they are named in your assetbundle
            defaultSkin.skinDefParams.meshReplacements = Modules.Skins.getMeshReplacements(defaultRendererInfos,
                "shigarakiBodyMesh",
                "shigarakiPantsMesh",
                "shigarakiEyesMesh",
                "shigarakiFaceMesh",
                "shigarakiHairMesh",
                "MeshHand"
            );
            //you can simply access the RendererInfos defaultMaterials and set them to the new materials for your skin.
            //string[] defaultMaterialStrings =
            //    {
            //        "matShiggyBody",
            //        "matShiggyBody",
            //        "matShiggyEyes",
            //        "matShiggyFace",
            //        "matShiggyHair",
            //        "ShiggyMat",
            //    };

            //for (int i = 0; i < defaultMaterialStrings.Length; i++)
            //{
            //    if (defaultMaterialStrings[i] == null)
            //    {
            //        defaultSkin.skinDefParams.rendererInfos[i].defaultMaterial = defaultRendererInfos[i].defaultMaterial;
            //    }
            //    else
            //    {
            //        defaultSkin.skinDefParams.rendererInfos[i].defaultMaterial = Materials.CreateHopooMaterial(defaultMaterialStrings[i], 2.5f).SetCull(true);
            //    }
            //}

            //simply find an object on your child locator you want to activate/deactivate and set if you want to activate/deacitvate it with this skin
            defaultSkin.skinDefParams.gameObjectActivations = new SkinDefParams.GameObjectActivation[]
            {
                new SkinDefParams.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("shigarakiBodyMesh"),
                    shouldActivate = true,
                },
                new SkinDefParams.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("shigarakiPantsMesh"),
                    shouldActivate = true,
                },
                new SkinDefParams.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("shigarakiEyesMesh"),
                    shouldActivate = true,
                },
                new SkinDefParams.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("shigarakiFaceMesh"),
                    shouldActivate = true,
                },
                new SkinDefParams.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("shigarakiHairMesh"),
                    shouldActivate = true,
                },
                new SkinDefParams.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("Hand"),
                    shouldActivate = false,
                },
            };

            skins.Add(defaultSkin);
            #endregion

            //handless skin
            #region handSkin

            SkinDef handSkin = Modules.Skins.CreateSkinDef(PLUGIN_PREFIX + "HAND_SKIN_NAME",
                ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("ShiggyBaseSkin"),
                defaultRendererInfos,
                prefabCharacterModel.gameObject);

            //these are your Mesh Replacements. The order here is based on your CustomRendererInfos from earlier
            //pass in meshes as they are named in your assetbundle
            handSkin.skinDefParams.meshReplacements = Modules.Skins.getMeshReplacements(defaultRendererInfos,
                "shigarakiBodyMesh",
                "shigarakiPantsMesh",
                "shigarakiEyesMesh",
                "shigarakiFaceMesh",
                "shigarakiHairMesh",
                "MeshHand"
            );
            //string[] handMaterialStrings =
            //{
            //    "matShiggyBody",
            //    "matShiggyBody",
            //    "matShiggyEyes",
            //    "matShiggyFace",
            //    "matShiggyHair",
            //    "ShiggyMat",
            //};

            //for (int i = 0; i < handMaterialStrings.Length; i++)
            //{
            //    if (handMaterialStrings[i] == null)
            //    {
            //        handSkin.skinDefParams.rendererInfos[i].defaultMaterial = defaultRendererInfos[i].defaultMaterial;
            //    }
            //    else
            //    {
            //        handSkin.skinDefParams.rendererInfos[i].defaultMaterial = Materials.CreateHopooMaterial(handMaterialStrings[i], 2.5f).SetCull(true);
            //    }
            //}

            handSkin.skinDefParams.gameObjectActivations = new SkinDefParams.GameObjectActivation[]
            {
                new SkinDefParams.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("shigarakiBodyMesh"),
                    shouldActivate = true,
                },
                new SkinDefParams.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("shigarakiPantsMesh"),
                    shouldActivate = true,
                },
                new SkinDefParams.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("shigarakiEyesMesh"),
                    shouldActivate = true,
                },
                new SkinDefParams.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("shigarakiFaceMesh"),
                    shouldActivate = true,
                },
                new SkinDefParams.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("shigarakiHairMesh"),
                    shouldActivate = true,
                },
                new SkinDefParams.GameObjectActivation
                {
                    gameObject = childLocator.FindChildGameObject("Hand"),
                    shouldActivate = true,
                },
            };

            skins.Add(handSkin);
            #endregion

            skinController.skins = skins.ToArray();
        }


        //public override void SetItemDisplays()
        //{

        //}

        //private static CharacterModel.RendererInfo[] SkinRendererInfos(CharacterModel.RendererInfo[] defaultRenderers, Material[] materials)
        //{
        //    CharacterModel.RendererInfo[] newRendererInfos = new CharacterModel.RendererInfo[defaultRenderers.Length];
        //    defaultRenderers.CopyTo(newRendererInfos, 0);

        //    for (int i = 0; i < defaultRenderers.Length; i++)
        //    {
        //        newRendererInfos[i].defaultMaterial = materials[i];
        //    }


        //    return newRendererInfos;
        //}
    }
}

