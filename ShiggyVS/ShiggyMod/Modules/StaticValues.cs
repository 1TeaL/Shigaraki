using IL.RoR2;
using IL.RoR2.Skills;
using On.RoR2.Skills;
using RoR2;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShiggyMod.Modules
{
    internal static class StaticValues
    {
        //Energy
        internal static float basePlusChaos = 100f;
        internal static float levelPlusChaos = 10f;
        internal static float regenPlusChaosRate = 8f;
        internal static float basePlusChaosGain = 1f;
        internal static float killPlusChaosGain = 0.1f;
        internal static float minimumCostFlatPlusChaosSpend = 0.005f;
        internal static float costFlatPlusChaosSpend = 5f;
        internal static float costFlatContantlyDrainingCoefficient = 0.005f;
        internal static float regenPlusChaosFraction = 0.025f;
        internal static float backupGain = 10f;
        internal static float afterburnerGain = 30f;
        internal static float lysateGain = 15f;
        internal const float airwalkEnergyFraction = 0.1f;
        internal const float airwalkThreshold = 3f;
        internal const float AFOEnergyCost = 50f;


        internal const float vulturePushRange = 70f;
        internal const float vulturePushAngle = 180f;
        internal const float vultureDamageCoefficient = 2f;

        internal const float multAttackspeed = 2f;
        internal const float multMovespeed = 0.4f;
        internal const float multArmor = 100f;

        internal const float loaderDamageMultiplier = 1.5f;

        internal const float commandoProcCoefficient = 1f;
        internal const float commandoDamageMultiplier = 0.1f;

        internal const int vagrantCooldown = 10;

        internal const int alphaconstructCooldown = 10;

        internal const float overloadingInterval = 1f;
        internal const float overloadingRadius = 6.5f;
        internal const float overloadingCoefficient = 5f;

        internal const float magmawormInterval = 0.5f;
        internal const float magmawormRadius = 6.5f;
        internal const float magmawormDuration = 5f;
        internal const float magmawormCoefficient = 1.5f;

        internal const float vagrantDamageCoefficient = 21f;
        internal const float vagrantRadius = 12f;
        internal const float vagrantdamageThreshold = 4f;

        internal const float claydunestriderHealCoefficient = 0.1f;
        internal const int claydunestriderbuffDuration = 8;
        internal const float claydunestriderAttackSpeed = 1.5f;
        //internal const float clayduneArmor = 100f;

        internal const float stonetitanarmorGain = 10f;

        internal const float voidjailerInterval = 1f;
        internal const float voidjailerDamageCoefficient = 1f;
        internal const float voidjailerminpullDistance = 10f;
        internal const float voidjailermaxpullDistance = 50f;
        internal const float voidjailerpullLiftVelocity = -10f;

        internal const float roboballattackspeedMultiplier = 0.02f;

        internal const float minimushrumInterval = 1f;
        internal const float minimushrumhealFraction = 0.05f;
        internal const float minimushrumRadius = 4f;

        internal const float lunarexploderShieldCoefficient = 0.25f;
        //internal const float lunarexploder = 1f;
        //internal const float lunarexploderbaseDuration = 2f;
        //internal const float lunarexploderRadius = 2.5f;
        //internal const float lunarexploderDamageCoefficient = 1f;

        internal const float larvaForce= 400f;
        internal const float larvaRadius = 5f;
        internal const float larvaDamageCoefficient = 1f;
        internal const float larvaProcCoefficient = 1f;
        internal const float larvajumpPower = 5f;
        internal const int larvajumpStacks = 2;

        internal const float voidmortarbaseDuration = 2f;
        internal const float voidmortarRadius = 10f;
        internal const float voidmortarattackspeedGain = 0.05f;

        internal const float mortarbaseDuration = 2f;
        internal const float mortarRadius = 10f;
        internal const float mortarDamageCoefficient = 1f;
        internal const float mortararmorGain = 1f;

        internal const float spikedamageCoefficient = 1.5f;
        internal const float spikedamageRadius = 6f;

        internal const int maxballCount = 3;

        internal const float verminsprintMultiplier = 2f;
        internal const float verminmovespeedMultiplier = 1.5f;

        internal const int verminjumpStacks = 4;
        internal const float verminjumpPower = 10f;

        internal const float multiplierCoefficient = 3f;
        internal const float multiplierEnergyCost = 10f;



        internal const float beetleFlatDamage = 5f;

        internal const float lesserwispFlatAttackSpeed = 0.5f;

        internal const float decayspreadRadius = 10f;
        internal const float decayadditionalTimer = 6f;
        internal const float decayDamageCoefficient = 1f;
        internal const float decayDamageStack = 0.5f;
        internal const float decayDamagePercentage = 0.005f;
        internal const float decayDamageTimer = 10f;
        internal const float decayInstaKillThreshold = 100f;

        internal const float decayattackDamageCoefficient = 2f;
        internal const float decayattackProcCoefficient = 1f;

        internal const float aircannonDamageCoefficient = 4f;
        internal const float aircannonProcCoefficient = 1f;

        internal const float bulletlaserDamageCoefficient = 2f;
        internal const float bulletlaserProcCoefficient = 0.3f;

        internal const float beetleguardSlamDamageCoefficient = 4f;
        internal const float beetleguardSlamBarrierCoefficient = 0.05f;
        internal const float beetleguardslamProcCoefficient = 1f;

        internal const float bisonchargeDamageCoefficient = 6f;
        internal const float bisonchargeProcCoefficient = 1f;

        internal const float bronzongballDamageCoefficient = 4f;
        internal const float bronzongballProcCoefficient = 1f;

        internal const float clayapothecarymortarDamageCoefficient = 2f;
        internal const float clayapothecarymortarHealthCostCoefficient = 0.05f;
        internal const float clayapothecarymortarProcCoefficient = 0.5f;

        internal const float claytemplarminigunDamageCoefficient = 0.3f;
        internal const float claytemplarminigunProcCoefficient = 0.05f;

        internal const float greaterwispballDamageCoefficient = 0.5f;
        internal const float greaterwispballRadius = 6f;
        internal const int greaterwispballbuffDuration = 8;
        internal const float greaterwispballProcCoefficient = 0f;

        internal const float JellyfishHealTickRate = 0.05f;
        internal const float JellyfishHealProcCoefficient = 2f;

        internal const float lemurianfireballDamageCoefficient = 2f;
        internal const float lemurianfireballProcCoefficient = 1f;

        internal const float elderlemurianfireblastProcCoefficient = 0.5f;
        internal const float elderlemurianfireblastDamageCoefficient = 2f;

        internal const float lunarwispminigunDamageCoefficient = 3f;
        internal const float lunarwispminigunProcCoefficient = 0.1f;

        internal const float lunarGolemSlideCooldown = 4f;

        internal const float parentDamageCoefficient = 6f;
        internal const float parentProcCoefficient = 1f;

        internal const float stonegolemDamageCoefficient = 4f;
        internal const float stonegolemProcCoefficient = 2f;

        internal const float voidreaverDamageCoefficient = 2f;
        internal const float voidreaverProcCoefficient = 0.2f;

        internal const float beetlequeenDamageCoefficient = 4f;
        internal const float beetlequeenProcCoefficient = 0.5f;

        internal const int grovetenderDuration = 6;
        internal const float grovetenderRadius = 40f;
        internal const float grovetenderAngle = 60f;
        internal const float grovetenderDamageCoefficient = 1f;

        internal const float claydunestriderDamageCoefficient = 8f;
        internal const float claydunestriderProcCoefficient = 1f;

        internal const float soluscontrolunitDamageCoefficient = 4f;
        internal const float soluscontrolunitProcCoefficient = 1f;

        internal const float xiconstructDamageCoefficient = 2f;
        internal const float xiconstructProcCoefficient = 0.2f;

        internal const int voiddevastatorTotalMissiles = 8;
        internal const float voiddevastatorDamageCoefficient = 1f;
        internal const float voiddevastatorProcCoefficient = 0.5f;

        internal const int scavenger = 3;
        internal const float scavengerDamageCoefficient = 4f;
        internal const float scavengerProcCoefficient = 0.5f;

        internal const float artificerflamethrowerDamageCoefficient = 15f;
        internal const float artificerflamethrowerProcCoefficient = 1f;

        internal const float artificericewallDamageCoefficient = 1f;

        internal const float artificerlightningorbMinDamageCoefficient = 4f;
        internal const float artificerlightningorbMaxDamageCoefficient = 12f;

        internal const float banditDamageCoefficient = 6f;
        internal const float banditcloakDuration = 3f;

        internal const float huntressDamageCoefficient = 1f;
        internal const float huntressProcCoefficient = 0.7f;
        internal const int huntressmaxArrowCount = 6;

        internal static float exposeDamageCoefficient = 3.5f;
        internal const float mercDamageCoefficient = 3f;
        internal const float mercProcCoefficient = 1f;
        internal const float mercpushForce = 300f;
        public static Vector3 mercbonusForce = Vector3.zero;
        internal const float mercbaseDuration = 0.4f;
        internal const float mercattackStartTime = 0.1f;
        internal const float mercattackEndTime = 0.9f;
        internal const float merchitStopDuration = 0.07f;
        internal const float mercattackRecoil = 0.75f;
        internal const float mercSpeedCoefficient = 5f;
        internal const float mercSpeedCoefficientOnExit = 0.1f;

        internal const float rexDamageCoefficient = 4f;
        internal const float rexProcCoefficient = 1f;
        internal const float rexHealthCost = 0.05f;

        internal const float railgunnerDamageCoefficient = 15f;
        internal const float railgunnerProcCoefficient = 2f;

        internal const float loaderBarrierGainCoefficient = 0.01f;

        internal const float grandparentSunEnergyCost = 10f;

        //synergy skills
        internal const float sweepingBeamDamageCoefficient = 1f;
        internal const float sweepingBeamProcCoefficient = 0.2f;
        internal const uint sweepingBeamTotalBullets = 20;
        internal const float sweepingBeamMaxAngle = 120f;

        internal const float blackholeGlaiveDamageCoefficient = 1.2f;
        internal const float blackholeGlaiveDamageCoefficientPerBounce = 1.2f;
        internal const float blackholeGlaiveBounceRange = 40f;
        internal const float blackholeGlaiveBlastRange = 15f;
        internal const float blackholeGlaiveProcCoefficient = 0.8f;
        internal const int blackholeGlaiveMaxBounceCount = 5;
        internal const float blackholeGlaiveTravelSpeed = 30f;

        internal const int bigbangBuffThreshold = 5;
        internal const float bigbangBuffCoefficient = 10f;
        internal const float bigbangBuffRadius= 20f;

        internal const float wisperBuffDamageCoefficient = 2f;

        internal const float omniboostBuffCoefficient = 0.3f;
        internal const float omniboostBuffStackCoefficient = 0.05f;
        internal const int omniboostNumberOfHits = 3;
        internal const float omniboostBuffTimer = 1f;

        internal const float gravitationalDownforceDamageCoefficient = 2f;
        internal const float gravitationalDownforceRange = 70f;
        internal const float gravitationalDownforceForce = 100f;

        internal const float gachaBuffThreshold = 30f;
        internal const int gachaTier1Amount = 4;
        internal const int gachaTier2Amount = 2;
        internal const int gachaTier3Amount = 1;

        internal const float windShieldRadius = 8f;
        internal const float windShieldDuration = 8f;
        internal const float windShieldDamageCoefficient = 0.1f;

        internal const float genesisRadius = 70f;
        internal const float genesisDamageCoefficient = 2f;
        internal const int genesisNumberOfAttacks = 5;
        internal const float genesisProcCoefficient = 0.4f;
        internal const float genesisStartHeight = 30f;

        internal const float refreshEnergyCoefficient = 0.5f;

        internal const float expungeDamageCoefficient = 6f;
        internal const float expungeProcCoefficient = 1f;
        internal const float expungeRadius = 50f;
        internal const float expungeDamageMultiplier = 1.5f;

        internal const float shadowClawDamageCoefficient = 3f;
        internal const float shadowClawProcCoefficient = 0.3f;
        internal const float shadowClawRadius = 6f;
        internal const int shadowClawHits = 3;
        internal const float shadowClawInterval = 0.3f;
        internal const float shadowClawMovespeedCharge = 10f;

        internal const float orbitalStrikeDamageCoefficient = 15f;
        internal const float orbitalStrikeMaxDistance = 150f;

        internal const float thunderclapDamageCoefficient = 2f;
        internal const float thunderclapRadius = 10f;
        internal const float thunderclapprocCoefficient = 1f;
        internal const float thunderclappushForce = 300f;
        public static Vector3 thunderclapbonusForce = Vector3.zero;
        internal const float thunderclapbaseDuration = 0.25f;
        internal const float thunderclapattackStartTime = 0.1f;
        internal const float thunderclapattackEndTime = 0.9f;
        internal const float thunderclaphitStopDuration = 0.1f;
        internal const float thunderclapattackRecoil = 0.75f;
        internal const float thunderclapSpeedCoefficient = 15f;
        internal const float thunderclapSpeedCoefficientOnExit = 0f;

        internal const float blastBurnDamageCoefficient = 2f;
        internal const float blastBurnDamageCoefficientGain = 0.2f;
        internal const float blastBurnProcCoefficient = 0.3f;
        internal const float blastBurnStartRadius = 5f;
        internal const float blastBurnIncrementRadius = 1f;
        internal const float blastBurnBaseInterval = 0.8f;

        internal const int barrierJellyDuration = 3;

        internal const float stoneFormBlockChance = 30f;
        internal const float stoneFormWaitDuration = 2f;

        internal static float mechStanceStepRate = 8f;
        internal static float mechStanceRadius = 6f;
        internal static float mechStanceDamageCoefficient = 3f;
        internal static float mechStanceProcCoefficient = 1f;

        internal static float auraOfBlightBuffThreshold = 1f;
        internal static float auraOfBlightBuffRadius = 10f;
        internal static float auraOfBlightBuffDotDamage = 4f;
        internal static float auraOfBlightBuffDotDuration = 5f;

        internal static float barbedSpikesBuffThreshold = 0.5f;
        internal static float barbedSpikesDamageCoefficient = 0.5f;
        internal static float barbedSpikesProcCoefficient = 0.5f;
        internal static float barbedSpikesRadius = 7f;

        internal static float ingrainBuffHealthRegen = 0.05f;

        internal static float windSlashDamageCoefficient = 1f;

        internal static float OFACoefficient = 1f;
        internal static float OFAThreshold = 1f;
        internal static float OFAHealthCostCoefficient = 0.1f;

        internal static float limitBreakCoefficient = 3f;
        internal static float limitBreakHealthCostCoefficient = 0.05f;

        internal static float voidFormHealthCostCoefficient = 0.025f;
        internal static float voidFormThreshold = 0.5f;

        internal static int elementalFusionThreshold = 5;

        internal static float decayPlusUltraHealthCostCoefficient = 0.1f;
        internal static float decayPlusUltraDuration = 1.5f;
        internal static float decayPlusUltraDamageCoefficient = 10f;
        internal static float decayPlusUltraProcCoefficient = 1f;
        internal static float decayPlusUltraRadius = 50f;
        internal static float decayPlusUltraForce = 1000f;

        internal static float doubleTimeCoefficient = 0.5f;
        internal static float doubleTimeSlowCoefficient = 0.5f;
        internal static float doubleTimeThreshold = 2f;
        internal static float doubleTimeRadius = 15f;

        internal static float blindSensesBlockChance = 20f;
        internal static float blindSensesDamageCoefficient = 1f;

        internal static float machPunchDamageCoefficient = 6f;
        internal static float machPunchProcCoefficient = 1f;
        internal static float machPunchForce = 600f;
        internal static int machPunchBaseMaxCharge = 3;
        internal static float machPunchBaseDistance = 10f;
        internal static float machPunchRadius = 4f;

        internal static float rapidPierceDamageCoefficient = 4f;
        internal static float rapidPierceProcCoefficient = 1f;

        internal static float theWorldCoefficient = 15f;
        internal static float theWorldEnergyCost = 0.1f;
        internal static float theWorldMaxRadius = 400f;

        internal static float supernovaDamageCoefficient = 20f;
        internal static float supernovaRadius = 50f;
        internal static float supernovaProcCoefficient = 3f;
        internal static float supernovaHealthThreshold = 0.75f;

        internal static float extremeSpeedDistance = 20f;
        internal static float extremeSpeedDamageCoefficient = 2f;
        internal static float extremeSpeedProcCoefficient = 0.2f;
        internal static int extremeSpeedNumberOfHits = 3;
        internal static float extremeSpeedIntervals = 0.2f;
        internal static float extremeSpeedForce = 20f;

        internal static float deathAuraThreshold = 1f;
        internal static float deathAuraRadius = 15f;
        internal static float deathAuraRadiusStacks = 4f;
        internal static int deathAuraDuration = 3;
        internal static float deathAuraBuffCoefficient = 0.5f;
        internal static float deathAuraBuffEnergyCost = 20f;
        internal static float deathAuraDebuffCoefficient = 0.2f;

        internal static float reversalStepRate = 150f;

        internal static float reversalRadius = 15f;
        internal static float reversalDamageCoefficient = 1f;
        internal static float reversalDuration = 0.5f;
        internal static float reversalSpeedCoefficient = 10f;
        internal static float reversalProcCoefficient = 1f;

        internal static float OFAFOHealthCostCoefficient = 0.2f;
        internal static float OFAFOEnergyCostCoefficient = 0.1f;
        internal static float OFAFOThreshold = 1f;
        internal static float OFAFOLifestealCoefficient = 0.2f;
        internal static float OFAFOEnergyGainCoefficient = 0.2f;
        internal static float OFAFOTimeMultiplierCoefficient = 2f;

        internal static float xBeamerDamageCoefficient = 8f;
        internal static float xBeamerProcCoefficient = 1f;
        internal static float xBeamerBlastRadius = 5f;
        internal static float xBeamerDuration = 3f;
        internal static float xBeamerRadius = 0.5f;
        internal static float xBeamerDistance = 200f;
        internal static int xBeamerChargeCoefficient = 2;
        internal static float xBeamerTickFrequency = 6f;
        internal static float xBeamerEnergyCost = 0.5f;
        internal static float xBeamerBaseEnergyCost = 30f;

        internal static float machineFormThreshold = 1f;
        internal static float machineFormDamageCoefficient = 1f;
        internal static float machineFormRadius = 50f;

        internal static float gargoyleProtectionDamageReductionCoefficient = 0.3f;

        internal static float finalReleaseInitialEnergyRequirement = 50f;
        internal static float finalReleaseEnergyCost = 10f;
        internal static float finalReleaseTapSpeed = 0.5f;
        internal static float finalReleaseThreshold = 1f;
        internal static float finalReleaseDamageCoefficient= 1f;
        internal static float finalReleaseProcCoefficient = 0.2f;
        internal static float finalReleaseDamagePerStackCoefficient = 1f;
        internal static float finalReleaseForceCoefficient = 200f;
        internal static float finalReleaseBaseRadius = 20f;
        internal static float finalReleaseRadiusPerStackCoefficient = 2f;
        internal static int finalReleaseCountIncrement = 25;

        internal static float finalReleaseMugetsuInterval = 0.2f;

        internal static float shunpoDuration = 0.3f;
        internal static float shunpoSpeedCoefficient = 5f;

        internal static float weatherReportThreshold = 3f;
        internal static float weatherReportRadius = 80f;
        internal static float weatherReportDamageCoefficient = 2f;

        internal static float blastingZoneProcCoefficient = 0.5f;
        internal static float blastingZoneDamageCoefficient = 3f;
        internal static float blastingZoneRangeAddition = 10f;
        internal static int blastingZoneDebuffStackApplication = 6;
        internal static int blastingZoneDebuffStackRemoval = 3;
        internal static float blastingZoneDebuffDamagePerStack = 0.01f;
        internal static float blastingZoneWindup = 0.5f;
        internal static float blastingZoneInterval = 0.1f;
        internal static int blastingZoneTotalHits = 6;
        internal static float blastingZoneDebuffInterval = 1f;
        internal static float blastingZoneRadius= 10f;

        internal static float wildcardRangeGlobal = 500f;
        internal static int wildcardDuration = 15;
        internal static float wildcardTeleportRange = 50f;
        internal static float wildcardSpeedCoefficient = 10f;
        internal static float wildcardDamageCoefficient = 10f;

        internal static float lightFormEnergyCost = 10f;
        internal static float lightFormBonusDamage = 0.2f;
        internal static float darkFormEnergyGain = 10f;
        internal static float darkFormBonusDamage = 0.5f;
        internal static float FormThreshold = 1f;
        internal static float lightAndDarknessRange = 20f;
        internal static float lightAndDarknessRangeAddition = 2f;
        internal static float lightAndDarknessBonusDamage = 0.2f;
        

        public enum IndicatorType : uint
        {
            PASSIVE = 1,
            ACTIVE = 2,
        }
        public enum skillType : uint
        {
            PASSIVE = 1,
            ACTIVE = 2,
        }

        public static Dictionary<string, IndicatorType> indicatorDict;
        public static Dictionary<string, skillType> skillDict;
        public static Dictionary<string, RoR2.Skills.SkillDef> skillNameToSkillDef;
        public static Dictionary<string, RoR2.Skills.SkillDef> bodyNameToSkillDef;
        public static Dictionary<string, string> quirkStringToInfoString;
        public static Dictionary<string, RoR2.BuffDef> passiveToBuff;
        public static Dictionary<string, RoR2.Skills.SkillDef> baseSkillPair;
        public static Dictionary<string, RoR2.Skills.SkillDef> baseSkillUpgrade;
        public static Dictionary<string, RoR2.Skills.SkillDef> synergySkillPair;
        public static Dictionary<string, RoR2.Skills.SkillDef> synergySkillUpgrade;

        //public static Dictionary<RoR2.Skills.SkillDef.skillName, >
        public static void LoadDictionary()
        {
            synergySkillPair = new Dictionary<string, RoR2.Skills.SkillDef>
            {
                { Survivors.Shiggy.shadowClawDef.skillName, Survivors.Shiggy.genesisDef},
                { Survivors.Shiggy.genesisDef.skillName, Survivors.Shiggy.shadowClawDef},

                { Survivors.Shiggy.refreshDef.skillName, Survivors.Shiggy.gachaPassiveDef},
                { Survivors.Shiggy.gachaPassiveDef.skillName, Survivors.Shiggy.refreshDef},

                { Survivors.Shiggy.orbitalStrikeDef.skillName, Survivors.Shiggy.blastBurnDef},
                { Survivors.Shiggy.blastBurnDef.skillName, Survivors.Shiggy.orbitalStrikeDef},

                { Survivors.Shiggy.decayPlusUltraDef.skillName, Survivors.Shiggy.auraOfBlightPassiveDef},
                { Survivors.Shiggy.auraOfBlightPassiveDef.skillName, Survivors.Shiggy.decayPlusUltraDef},

                { Survivors.Shiggy.gravitationalDownforceDef.skillName, Survivors.Shiggy.elementalFusionPassiveDef},
                { Survivors.Shiggy.elementalFusionPassiveDef.skillName, Survivors.Shiggy.gravitationalDownforceDef},

                { Survivors.Shiggy.windShieldDef.skillName, Survivors.Shiggy.windSlashDef},
                { Survivors.Shiggy.windSlashDef.skillName, Survivors.Shiggy.windShieldDef},

                { Survivors.Shiggy.ingrainPassiveDef.skillName, Survivors.Shiggy.stoneFormPassiveDef},
                { Survivors.Shiggy.stoneFormPassiveDef.skillName, Survivors.Shiggy.ingrainPassiveDef},

                { Survivors.Shiggy.blackholeGlaiveDef.skillName, Survivors.Shiggy.mechStanceDef},
                { Survivors.Shiggy.mechStanceDef.skillName, Survivors.Shiggy.blackholeGlaiveDef},

                { Survivors.Shiggy.rapidPierceDef.skillName, Survivors.Shiggy.sweepingBeamDef},
                { Survivors.Shiggy.sweepingBeamDef.skillName, Survivors.Shiggy.rapidPierceDef},

                { Survivors.Shiggy.voidFormDef.skillName, Survivors.Shiggy.limitBreakDef},
                { Survivors.Shiggy.limitBreakDef.skillName, Survivors.Shiggy.voidFormDef},

                { Survivors.Shiggy.barrierJellyDef.skillName, Survivors.Shiggy.blindSensesPassiveDef},
                { Survivors.Shiggy.blindSensesPassiveDef.skillName, Survivors.Shiggy.barrierJellyDef},

                { Survivors.Shiggy.barbedSpikesPassiveDef.skillName, Survivors.Shiggy.expungeDef},
                { Survivors.Shiggy.expungeDef.skillName, Survivors.Shiggy.barbedSpikesPassiveDef},

                { Survivors.Shiggy.machPunchDef.skillName, Survivors.Shiggy.thunderclapDef},
                { Survivors.Shiggy.thunderclapDef.skillName, Survivors.Shiggy.machPunchDef},

                { Survivors.Shiggy.bigBangPassiveDef.skillName, Survivors.Shiggy.wisperPassiveDef},
                { Survivors.Shiggy.wisperPassiveDef.skillName, Survivors.Shiggy.bigBangPassiveDef},

                { Survivors.Shiggy.doubleTimePassiveDef.skillName, Survivors.Shiggy.omniboostPassiveDef},
                { Survivors.Shiggy.omniboostPassiveDef.skillName, Survivors.Shiggy.doubleTimePassiveDef},
            };

            synergySkillUpgrade = new Dictionary<string, RoR2.Skills.SkillDef>
            {
                { Survivors.Shiggy.shadowClawDef.skillName, Survivors.Shiggy.lightAndDarknessDef},
                { Survivors.Shiggy.genesisDef.skillName, Survivors.Shiggy.lightAndDarknessDef},

                { Survivors.Shiggy.refreshDef.skillName, Survivors.Shiggy.wildCardDef},
                { Survivors.Shiggy.gachaPassiveDef.skillName, Survivors.Shiggy.wildCardDef},

                { Survivors.Shiggy.orbitalStrikeDef.skillName, Survivors.Shiggy.blastingZoneDef},
                { Survivors.Shiggy.blastBurnDef.skillName, Survivors.Shiggy.blastingZoneDef},

                { Survivors.Shiggy.decayPlusUltraDef.skillName, Survivors.Shiggy.decayAwakenedPassiveDef},
                { Survivors.Shiggy.auraOfBlightPassiveDef.skillName, Survivors.Shiggy.decayAwakenedPassiveDef},

                { Survivors.Shiggy.gravitationalDownforceDef.skillName, Survivors.Shiggy.weatherReportPassiveDef},
                { Survivors.Shiggy.elementalFusionPassiveDef.skillName, Survivors.Shiggy.weatherReportPassiveDef},

                { Survivors.Shiggy.windShieldDef.skillName, Survivors.Shiggy.finalReleaseDef},
                { Survivors.Shiggy.windSlashDef.skillName, Survivors.Shiggy.finalReleaseDef},

                { Survivors.Shiggy.ingrainPassiveDef.skillName, Survivors.Shiggy.gargoyleProtectionPassiveDef},
                { Survivors.Shiggy.stoneFormPassiveDef.skillName, Survivors.Shiggy.gargoyleProtectionPassiveDef},

                { Survivors.Shiggy.blackholeGlaiveDef.skillName, Survivors.Shiggy.machineFormPassiveDef},
                { Survivors.Shiggy.mechStanceDef.skillName, Survivors.Shiggy.machineFormPassiveDef},

                { Survivors.Shiggy.rapidPierceDef.skillName, Survivors.Shiggy.xBeamerDef},
                { Survivors.Shiggy.sweepingBeamDef.skillName, Survivors.Shiggy.xBeamerDef},

                { Survivors.Shiggy.voidFormDef.skillName, Survivors.Shiggy.OFAFODef},
                { Survivors.Shiggy.limitBreakDef.skillName, Survivors.Shiggy.OFAFODef},

                { Survivors.Shiggy.barrierJellyDef.skillName, Survivors.Shiggy.reversalPassiveDef},
                { Survivors.Shiggy.blindSensesPassiveDef.skillName, Survivors.Shiggy.reversalPassiveDef},

                { Survivors.Shiggy.barbedSpikesPassiveDef.skillName, Survivors.Shiggy.deathAuraDef},
                { Survivors.Shiggy.expungeDef.skillName, Survivors.Shiggy.deathAuraDef},

                { Survivors.Shiggy.machPunchDef.skillName, Survivors.Shiggy.extremeSpeedDef},
                { Survivors.Shiggy.thunderclapDef.skillName, Survivors.Shiggy.extremeSpeedDef},

                { Survivors.Shiggy.bigBangPassiveDef.skillName, Survivors.Shiggy.supernovaPassiveDef},
                { Survivors.Shiggy.wisperPassiveDef.skillName, Survivors.Shiggy.supernovaPassiveDef},

                { Survivors.Shiggy.doubleTimePassiveDef.skillName, Survivors.Shiggy.theWorldDef},
                { Survivors.Shiggy.omniboostPassiveDef.skillName, Survivors.Shiggy.theWorldDef},
            };


            skillNameToSkillDef = new Dictionary<string, RoR2.Skills.SkillDef>
            {
                //base skillname to skilldef
                { Survivors.Shiggy.railgunnercryoDef.skillName, Survivors.Shiggy.railgunnercryoDef},
                { Survivors.Shiggy.lunarwispminigunDef.skillName, Survivors.Shiggy.lunarwispminigunDef},

                { Survivors.Shiggy.parentteleportDef.skillName, Survivors.Shiggy.parentteleportDef},
                { Survivors.Shiggy.loaderpassiveDef.skillName, Survivors.Shiggy.loaderpassiveDef},

                { Survivors.Shiggy.pestpassiveDef.skillName, Survivors.Shiggy.pestpassiveDef},
                { Survivors.Shiggy.verminpassiveDef.skillName, Survivors.Shiggy.verminpassiveDef},

                { Survivors.Shiggy.roboballminibpassiveDef.skillName, Survivors.Shiggy.roboballminibpassiveDef},
                { Survivors.Shiggy.commandopassiveDef.skillName, Survivors.Shiggy.commandopassiveDef},

                { Survivors.Shiggy.rexmortarDef.skillName, Survivors.Shiggy.rexmortarDef},
                { Survivors.Shiggy.decayDef.skillName, Survivors.Shiggy.decayDef},

                { Survivors.Shiggy.grandparentsunDef.skillName, Survivors.Shiggy.grandparentsunDef},
                { Survivors.Shiggy.artificerflamethrowerDef.skillName, Survivors.Shiggy.artificerflamethrowerDef},
                { Survivors.Shiggy.artificericewallDef.skillName, Survivors.Shiggy.artificericewallDef},
                { Survivors.Shiggy.artificerlightningorbDef.skillName, Survivors.Shiggy.artificerlightningorbDef},

                { Survivors.Shiggy.voidbarnaclepassiveDef.skillName, Survivors.Shiggy.voidbarnaclepassiveDef},
                { Survivors.Shiggy.voidfiendcleanseDef.skillName, Survivors.Shiggy.voidfiendcleanseDef},

                { Survivors.Shiggy.lesserwisppassiveDef.skillName, Survivors.Shiggy.lesserwisppassiveDef},
                { Survivors.Shiggy.beetlepassiveDef.skillName, Survivors.Shiggy.beetlepassiveDef},

                { Survivors.Shiggy.multiplierDef.skillName, Survivors.Shiggy.multiplierDef},
                { Survivors.Shiggy.DekuOFADef.skillName, Survivors.Shiggy.DekuOFADef},

                { Survivors.Shiggy.aircannonDef.skillName, Survivors.Shiggy.aircannonDef},
                { Survivors.Shiggy.mercdashDef.skillName, Survivors.Shiggy.mercdashDef},

                { Survivors.Shiggy.claydunestriderbuffDef.skillName, Survivors.Shiggy.claydunestriderbuffDef},
                { Survivors.Shiggy.minimushrumpassiveDef.skillName, Survivors.Shiggy.minimushrumpassiveDef},

                { Survivors.Shiggy.bronzongballDef.skillName, Survivors.Shiggy.bronzongballDef},
                { Survivors.Shiggy.guppassiveDef.skillName, Survivors.Shiggy.guppassiveDef},

                { Survivors.Shiggy.acridpassiveDef.skillName, Survivors.Shiggy.acridpassiveDef},
                { Survivors.Shiggy.larvapassiveDef.skillName, Survivors.Shiggy.larvapassiveDef},

                { Survivors.Shiggy.beetleguardslamDef.skillName, Survivors.Shiggy.beetleguardslamDef},
                { Survivors.Shiggy.multbuffDef.skillName, Survivors.Shiggy.multbuffDef},

                { Survivors.Shiggy.hermitcrabpassiveDef.skillName, Survivors.Shiggy.hermitcrabpassiveDef},
                { Survivors.Shiggy.stonetitanpassiveDef.skillName, Survivors.Shiggy.stonetitanpassiveDef},

                { Survivors.Shiggy.alphacontructpassiveDef.skillName, Survivors.Shiggy.alphacontructpassiveDef},
                { Survivors.Shiggy.jellyfishHealDef.skillName, Survivors.Shiggy.jellyfishHealDef},

                { Survivors.Shiggy.elderlemurianfireblastDef.skillName, Survivors.Shiggy.elderlemurianfireblastDef},
                { Survivors.Shiggy.lemurianfireballDef.skillName, Survivors.Shiggy.lemurianfireballDef},

                { Survivors.Shiggy.overloadingwormpassiveDef.skillName, Survivors.Shiggy.overloadingwormpassiveDef},
                { Survivors.Shiggy.bisonchargeDef.skillName, Survivors.Shiggy.bisonchargeDef},

                { Survivors.Shiggy.captainpassiveDef.skillName, Survivors.Shiggy.captainpassiveDef},
                { Survivors.Shiggy.voidreaverportalDef.skillName, Survivors.Shiggy.voidreaverportalDef},

                { Survivors.Shiggy.impblinkDef.skillName, Survivors.Shiggy.impblinkDef},
                { Survivors.Shiggy.banditlightsoutDef.skillName, Survivors.Shiggy.banditlightsoutDef},

                { Survivors.Shiggy.lunarexploderpassiveDef.skillName, Survivors.Shiggy.lunarexploderpassiveDef},
                { Survivors.Shiggy.lunargolemSlideDef.skillName, Survivors.Shiggy.lunargolemSlideDef},

                { Survivors.Shiggy.impbosspassiveDef.skillName, Survivors.Shiggy.impbosspassiveDef},
                { Survivors.Shiggy.magmawormpassiveDef.skillName, Survivors.Shiggy.magmawormpassiveDef},

                { Survivors.Shiggy.xiconstructbeamDef.skillName, Survivors.Shiggy.xiconstructbeamDef},
                { Survivors.Shiggy.clayapothecarymortarDef.skillName, Survivors.Shiggy.clayapothecarymortarDef},

                { Survivors.Shiggy.alloyvultureWindBlastDef.skillName, Survivors.Shiggy.alloyvultureWindBlastDef},
                { Survivors.Shiggy.engiturretDef.skillName, Survivors.Shiggy.engiturretDef},

                { Survivors.Shiggy.beetlequeenSummonDef.skillName, Survivors.Shiggy.beetlequeenSummonDef},
                { Survivors.Shiggy.scavengerthqwibDef.skillName, Survivors.Shiggy.scavengerthqwibDef},

                { Survivors.Shiggy.vagrantpassiveDef.skillName, Survivors.Shiggy.vagrantpassiveDef},
                { Survivors.Shiggy.claytemplarminigunDef.skillName, Survivors.Shiggy.claytemplarminigunDef},

                { Survivors.Shiggy.greaterWispBuffDef.skillName, Survivors.Shiggy.greaterWispBuffDef},
                { Survivors.Shiggy.grovetenderChainDef.skillName, Survivors.Shiggy.grovetenderChainDef},

                { Survivors.Shiggy.voidjailerpassiveDef.skillName, Survivors.Shiggy.voidjailerpassiveDef},
                { Survivors.Shiggy.soluscontrolunityknockupDef.skillName, Survivors.Shiggy.soluscontrolunityknockupDef},

                { Survivors.Shiggy.voiddevastatorhomingDef.skillName, Survivors.Shiggy.voiddevastatorhomingDef},
                { Survivors.Shiggy.huntressattackDef.skillName, Survivors.Shiggy.huntressattackDef},
                
                //synergy skillname to skilldef
                { Survivors.Shiggy.bulletlaserDef.skillName, Survivors.Shiggy.bulletlaserDef},
                { Survivors.Shiggy.stonegolemlaserDef.skillName, Survivors.Shiggy.stonegolemlaserDef},

                { Survivors.Shiggy.shadowClawDef.skillName, Survivors.Shiggy.shadowClawDef},
                { Survivors.Shiggy.genesisDef.skillName, Survivors.Shiggy.genesisDef},

                { Survivors.Shiggy.refreshDef.skillName, Survivors.Shiggy.refreshDef},
                { Survivors.Shiggy.gachaPassiveDef.skillName, Survivors.Shiggy.gachaPassiveDef},

                { Survivors.Shiggy.orbitalStrikeDef.skillName, Survivors.Shiggy.orbitalStrikeDef},
                { Survivors.Shiggy.blastBurnDef.skillName, Survivors.Shiggy.blastBurnDef},

                { Survivors.Shiggy.decayPlusUltraDef.skillName, Survivors.Shiggy.decayPlusUltraDef},
                { Survivors.Shiggy.auraOfBlightPassiveDef.skillName, Survivors.Shiggy.auraOfBlightPassiveDef},

                { Survivors.Shiggy.gravitationalDownforceDef.skillName, Survivors.Shiggy.gravitationalDownforceDef},
                { Survivors.Shiggy.elementalFusionPassiveDef.skillName, Survivors.Shiggy.elementalFusionPassiveDef},

                { Survivors.Shiggy.windShieldDef.skillName, Survivors.Shiggy.windShieldDef},
                { Survivors.Shiggy.windSlashDef.skillName, Survivors.Shiggy.windSlashDef},

                { Survivors.Shiggy.ingrainPassiveDef.skillName, Survivors.Shiggy.ingrainPassiveDef},
                { Survivors.Shiggy.stoneFormPassiveDef.skillName, Survivors.Shiggy.stoneFormPassiveDef},

                { Survivors.Shiggy.blackholeGlaiveDef.skillName, Survivors.Shiggy.blackholeGlaiveDef},
                { Survivors.Shiggy.mechStanceDef.skillName, Survivors.Shiggy.mechStanceDef},

                { Survivors.Shiggy.rapidPierceDef.skillName, Survivors.Shiggy.rapidPierceDef},
                { Survivors.Shiggy.sweepingBeamDef.skillName, Survivors.Shiggy.sweepingBeamDef},

                { Survivors.Shiggy.voidFormDef.skillName, Survivors.Shiggy.voidFormDef},
                { Survivors.Shiggy.limitBreakDef.skillName, Survivors.Shiggy.limitBreakDef},

                { Survivors.Shiggy.barrierJellyDef.skillName, Survivors.Shiggy.barrierJellyDef},
                { Survivors.Shiggy.blindSensesPassiveDef.skillName, Survivors.Shiggy.blindSensesPassiveDef},

                { Survivors.Shiggy.barbedSpikesPassiveDef.skillName, Survivors.Shiggy.barbedSpikesPassiveDef},
                { Survivors.Shiggy.expungeDef.skillName, Survivors.Shiggy.expungeDef},

                { Survivors.Shiggy.machPunchDef.skillName, Survivors.Shiggy.machPunchDef},
                { Survivors.Shiggy.thunderclapDef.skillName, Survivors.Shiggy.thunderclapDef},

                { Survivors.Shiggy.bigBangPassiveDef.skillName, Survivors.Shiggy.bigBangPassiveDef},
                { Survivors.Shiggy.wisperPassiveDef.skillName, Survivors.Shiggy.wisperPassiveDef},

                { Survivors.Shiggy.doubleTimePassiveDef.skillName, Survivors.Shiggy.doubleTimePassiveDef},
                { Survivors.Shiggy.omniboostPassiveDef.skillName, Survivors.Shiggy.omniboostPassiveDef},

                //ultimate skillname to skilldef
                { Survivors.Shiggy.lightAndDarknessDef.skillName, Survivors.Shiggy.lightAndDarknessDef},

                { Survivors.Shiggy.wildCardDef.skillName, Survivors.Shiggy.wildCardDef},

                { Survivors.Shiggy.blastingZoneDef.skillName, Survivors.Shiggy.blastingZoneDef},

                { Survivors.Shiggy.decayAwakenedPassiveDef.skillName, Survivors.Shiggy.decayAwakenedPassiveDef},

                { Survivors.Shiggy.weatherReportPassiveDef.skillName, Survivors.Shiggy.weatherReportPassiveDef},

                { Survivors.Shiggy.finalReleaseDef.skillName, Survivors.Shiggy.finalReleaseDef},

                { Survivors.Shiggy.gargoyleProtectionPassiveDef.skillName, Survivors.Shiggy.gargoyleProtectionPassiveDef},

                { Survivors.Shiggy.machineFormPassiveDef.skillName, Survivors.Shiggy.machineFormPassiveDef},

                { Survivors.Shiggy.xBeamerDef.skillName, Survivors.Shiggy.xBeamerDef},

                { Survivors.Shiggy.OFAFODef.skillName, Survivors.Shiggy.OFAFODef},

                { Survivors.Shiggy.reversalPassiveDef.skillName, Survivors.Shiggy.reversalPassiveDef},

                { Survivors.Shiggy.deathAuraDef.skillName, Survivors.Shiggy.deathAuraDef},

                { Survivors.Shiggy.extremeSpeedDef.skillName, Survivors.Shiggy.extremeSpeedDef},

                { Survivors.Shiggy.supernovaPassiveDef.skillName, Survivors.Shiggy.supernovaPassiveDef},

                { Survivors.Shiggy.theWorldDef.skillName, Survivors.Shiggy.theWorldDef},
            };

            baseSkillUpgrade = new Dictionary<string, RoR2.Skills.SkillDef>
            {
                { Survivors.Shiggy.railgunnercryoDef.skillName, Survivors.Shiggy.sweepingBeamDef},
                { Survivors.Shiggy.lunarwispminigunDef.skillName, Survivors.Shiggy.sweepingBeamDef},

                { Survivors.Shiggy.parentteleportDef.skillName, Survivors.Shiggy.machPunchDef},
                { Survivors.Shiggy.loaderpassiveDef.skillName, Survivors.Shiggy.machPunchDef},

                { Survivors.Shiggy.pestpassiveDef.skillName, Survivors.Shiggy.blindSensesPassiveDef},
                { Survivors.Shiggy.verminpassiveDef.skillName, Survivors.Shiggy.blindSensesPassiveDef},

                { Survivors.Shiggy.roboballminibpassiveDef.skillName, Survivors.Shiggy.doubleTimePassiveDef},
                { Survivors.Shiggy.commandopassiveDef.skillName, Survivors.Shiggy.doubleTimePassiveDef},

                { Survivors.Shiggy.rexmortarDef.skillName, Survivors.Shiggy.decayPlusUltraDef},
                { Survivors.Shiggy.decayDef.skillName, Survivors.Shiggy.decayPlusUltraDef},

                { Survivors.Shiggy.grandparentsunDef.skillName, Survivors.Shiggy.elementalFusionPassiveDef},
                { Survivors.Shiggy.artificerflamethrowerDef.skillName, Survivors.Shiggy.elementalFusionPassiveDef},
                { Survivors.Shiggy.artificericewallDef.skillName, Survivors.Shiggy.elementalFusionPassiveDef},
                { Survivors.Shiggy.artificerlightningorbDef.skillName, Survivors.Shiggy.elementalFusionPassiveDef},

                { Survivors.Shiggy.voidbarnaclepassiveDef.skillName, Survivors.Shiggy.voidFormDef},
                { Survivors.Shiggy.voidfiendcleanseDef.skillName, Survivors.Shiggy.voidFormDef},

                { Survivors.Shiggy.lesserwisppassiveDef.skillName, Survivors.Shiggy.omniboostPassiveDef},
                { Survivors.Shiggy.beetlepassiveDef.skillName, Survivors.Shiggy.omniboostPassiveDef},

                { Survivors.Shiggy.multiplierDef.skillName, Survivors.Shiggy.limitBreakDef},
                { Survivors.Shiggy.DekuOFADef.skillName, Survivors.Shiggy.limitBreakDef},

                { Survivors.Shiggy.aircannonDef.skillName, Survivors.Shiggy.windSlashDef},
                { Survivors.Shiggy.mercdashDef.skillName, Survivors.Shiggy.windSlashDef},

                { Survivors.Shiggy.claydunestriderbuffDef.skillName, Survivors.Shiggy.ingrainPassiveDef},
                { Survivors.Shiggy.minimushrumpassiveDef.skillName, Survivors.Shiggy.ingrainPassiveDef},

                { Survivors.Shiggy.bronzongballDef.skillName, Survivors.Shiggy.barbedSpikesPassiveDef},
                { Survivors.Shiggy.guppassiveDef.skillName, Survivors.Shiggy.barbedSpikesPassiveDef},

                { Survivors.Shiggy.acridpassiveDef.skillName, Survivors.Shiggy.auraOfBlightPassiveDef},
                { Survivors.Shiggy.larvapassiveDef.skillName, Survivors.Shiggy.auraOfBlightPassiveDef},

                { Survivors.Shiggy.beetleguardslamDef.skillName, Survivors.Shiggy.mechStanceDef},
                { Survivors.Shiggy.multbuffDef.skillName, Survivors.Shiggy.mechStanceDef},

                { Survivors.Shiggy.hermitcrabpassiveDef.skillName, Survivors.Shiggy.stoneFormPassiveDef},
                { Survivors.Shiggy.stonetitanpassiveDef.skillName, Survivors.Shiggy.stoneFormPassiveDef},

                { Survivors.Shiggy.alphacontructpassiveDef.skillName, Survivors.Shiggy.barrierJellyDef},
                { Survivors.Shiggy.jellyfishHealDef.skillName, Survivors.Shiggy.barrierJellyDef},

                { Survivors.Shiggy.elderlemurianfireblastDef.skillName, Survivors.Shiggy.blastBurnDef},
                { Survivors.Shiggy.lemurianfireballDef.skillName, Survivors.Shiggy.blastBurnDef},

                { Survivors.Shiggy.overloadingwormpassiveDef.skillName, Survivors.Shiggy.thunderclapDef},
                { Survivors.Shiggy.bisonchargeDef.skillName, Survivors.Shiggy.thunderclapDef},

                { Survivors.Shiggy.captainpassiveDef.skillName, Survivors.Shiggy.orbitalStrikeDef},
                { Survivors.Shiggy.voidreaverportalDef.skillName, Survivors.Shiggy.orbitalStrikeDef},

                { Survivors.Shiggy.impblinkDef.skillName, Survivors.Shiggy.shadowClawDef},
                { Survivors.Shiggy.banditlightsoutDef.skillName, Survivors.Shiggy.shadowClawDef},

                { Survivors.Shiggy.lunarexploderpassiveDef.skillName, Survivors.Shiggy.refreshDef},
                { Survivors.Shiggy.lunargolemSlideDef.skillName, Survivors.Shiggy.refreshDef},

                { Survivors.Shiggy.impbosspassiveDef.skillName, Survivors.Shiggy.expungeDef},
                { Survivors.Shiggy.magmawormpassiveDef.skillName, Survivors.Shiggy.expungeDef},

                { Survivors.Shiggy.xiconstructbeamDef.skillName, Survivors.Shiggy.genesisDef},
                { Survivors.Shiggy.clayapothecarymortarDef.skillName, Survivors.Shiggy.genesisDef},

                { Survivors.Shiggy.alloyvultureWindBlastDef.skillName, Survivors.Shiggy.windShieldDef},
                { Survivors.Shiggy.engiturretDef.skillName, Survivors.Shiggy.windShieldDef},

                { Survivors.Shiggy.beetlequeenSummonDef.skillName, Survivors.Shiggy.gachaPassiveDef},
                { Survivors.Shiggy.scavengerthqwibDef.skillName, Survivors.Shiggy.gachaPassiveDef},

                { Survivors.Shiggy.vagrantpassiveDef.skillName, Survivors.Shiggy.bigBangPassiveDef},
                { Survivors.Shiggy.claytemplarminigunDef.skillName, Survivors.Shiggy.bigBangPassiveDef},

                { Survivors.Shiggy.greaterWispBuffDef.skillName, Survivors.Shiggy.wisperPassiveDef},
                { Survivors.Shiggy.grovetenderChainDef.skillName, Survivors.Shiggy.wisperPassiveDef},

                { Survivors.Shiggy.voidjailerpassiveDef.skillName, Survivors.Shiggy.gravitationalDownforceDef},
                { Survivors.Shiggy.soluscontrolunityknockupDef.skillName, Survivors.Shiggy.gravitationalDownforceDef},

                { Survivors.Shiggy.voiddevastatorhomingDef.skillName, Survivors.Shiggy.blackholeGlaiveDef},
                { Survivors.Shiggy.huntressattackDef.skillName, Survivors.Shiggy.blackholeGlaiveDef},

                { Survivors.Shiggy.bulletlaserDef.skillName, Survivors.Shiggy.sweepingBeamDef},
                { Survivors.Shiggy.stonegolemlaserDef.skillName, Survivors.Shiggy.sweepingBeamDef},
            };


            baseSkillPair = new Dictionary<string, RoR2.Skills.SkillDef>
            {
                { Survivors.Shiggy.railgunnercryoDef.skillName, Survivors.Shiggy.lunarwispminigunDef},
                { Survivors.Shiggy.lunarwispminigunDef.skillName, Survivors.Shiggy.railgunnercryoDef},

                { Survivors.Shiggy.parentteleportDef.skillName, Survivors.Shiggy.loaderpassiveDef},
                { Survivors.Shiggy.loaderpassiveDef.skillName, Survivors.Shiggy.parentteleportDef},

                { Survivors.Shiggy.verminpassiveDef.skillName, Survivors.Shiggy.pestpassiveDef},
                { Survivors.Shiggy.pestpassiveDef.skillName, Survivors.Shiggy.verminpassiveDef},

                { Survivors.Shiggy.roboballminibpassiveDef.skillName, Survivors.Shiggy.commandopassiveDef},
                { Survivors.Shiggy.commandopassiveDef.skillName, Survivors.Shiggy.roboballminibpassiveDef},

                { Survivors.Shiggy.rexmortarDef.skillName, Survivors.Shiggy.decayDef},
                { Survivors.Shiggy.decayDef.skillName, Survivors.Shiggy.rexmortarDef},

                { Survivors.Shiggy.grandparentsunDef.skillName, Survivors.Shiggy.artificerflamethrowerDef},
                { Survivors.Shiggy.artificerflamethrowerDef.skillName, Survivors.Shiggy.grandparentsunDef},
                { Survivors.Shiggy.artificericewallDef.skillName, Survivors.Shiggy.grandparentsunDef},
                { Survivors.Shiggy.artificerlightningorbDef.skillName, Survivors.Shiggy.grandparentsunDef},

                { Survivors.Shiggy.voidbarnaclepassiveDef.skillName, Survivors.Shiggy.voidfiendcleanseDef},
                { Survivors.Shiggy.voidfiendcleanseDef.skillName, Survivors.Shiggy.voidbarnaclepassiveDef},

                { Survivors.Shiggy.lesserwisppassiveDef.skillName, Survivors.Shiggy.beetlepassiveDef},
                { Survivors.Shiggy.beetlepassiveDef.skillName, Survivors.Shiggy.lesserwisppassiveDef},

                { Survivors.Shiggy.DekuOFADef.skillName, Survivors.Shiggy.multiplierDef},
                { Survivors.Shiggy.multiplierDef.skillName, Survivors.Shiggy.DekuOFADef},

                { Survivors.Shiggy.aircannonDef.skillName, Survivors.Shiggy.mercdashDef},
                { Survivors.Shiggy.mercdashDef.skillName, Survivors.Shiggy.aircannonDef},

                { Survivors.Shiggy.claydunestriderbuffDef.skillName, Survivors.Shiggy.minimushrumpassiveDef},
                { Survivors.Shiggy.minimushrumpassiveDef.skillName, Survivors.Shiggy.claydunestriderbuffDef},

                { Survivors.Shiggy.bronzongballDef.skillName, Survivors.Shiggy.guppassiveDef},
                { Survivors.Shiggy.guppassiveDef.skillName, Survivors.Shiggy.bronzongballDef},

                { Survivors.Shiggy.acridpassiveDef.skillName, Survivors.Shiggy.larvapassiveDef},
                { Survivors.Shiggy.larvapassiveDef.skillName, Survivors.Shiggy.acridpassiveDef},

                { Survivors.Shiggy.beetleguardslamDef.skillName, Survivors.Shiggy.multbuffDef},
                { Survivors.Shiggy.multbuffDef.skillName, Survivors.Shiggy.beetleguardslamDef},

                { Survivors.Shiggy.hermitcrabpassiveDef.skillName, Survivors.Shiggy.stonetitanpassiveDef},
                { Survivors.Shiggy.stonetitanpassiveDef.skillName, Survivors.Shiggy.hermitcrabpassiveDef},

                { Survivors.Shiggy.alphacontructpassiveDef.skillName, Survivors.Shiggy.jellyfishHealDef},
                { Survivors.Shiggy.jellyfishHealDef.skillName, Survivors.Shiggy.alphacontructpassiveDef},

                { Survivors.Shiggy.elderlemurianfireblastDef.skillName, Survivors.Shiggy.lemurianfireballDef},
                { Survivors.Shiggy.lemurianfireballDef.skillName, Survivors.Shiggy.elderlemurianfireblastDef},

                { Survivors.Shiggy.overloadingwormpassiveDef.skillName, Survivors.Shiggy.bisonchargeDef},
                { Survivors.Shiggy.bisonchargeDef.skillName, Survivors.Shiggy.overloadingwormpassiveDef},

                { Survivors.Shiggy.captainpassiveDef.skillName, Survivors.Shiggy.voidreaverportalDef},
                { Survivors.Shiggy.voidreaverportalDef.skillName, Survivors.Shiggy.captainpassiveDef},

                { Survivors.Shiggy.impblinkDef.skillName, Survivors.Shiggy.banditlightsoutDef},
                { Survivors.Shiggy.banditlightsoutDef.skillName, Survivors.Shiggy.impblinkDef},

                { Survivors.Shiggy.lunarexploderpassiveDef.skillName, Survivors.Shiggy.lunargolemSlideDef},
                { Survivors.Shiggy.lunargolemSlideDef.skillName, Survivors.Shiggy.lunarexploderpassiveDef},

                { Survivors.Shiggy.impbosspassiveDef.skillName, Survivors.Shiggy.magmawormpassiveDef},
                { Survivors.Shiggy.magmawormpassiveDef.skillName, Survivors.Shiggy.impbosspassiveDef},

                { Survivors.Shiggy.xiconstructbeamDef.skillName, Survivors.Shiggy.clayapothecarymortarDef},
                { Survivors.Shiggy.clayapothecarymortarDef.skillName, Survivors.Shiggy.xiconstructbeamDef},

                { Survivors.Shiggy.alloyvultureWindBlastDef.skillName, Survivors.Shiggy.engiturretDef},
                { Survivors.Shiggy.engiturretDef.skillName, Survivors.Shiggy.alloyvultureWindBlastDef},

                { Survivors.Shiggy.beetlequeenSummonDef.skillName, Survivors.Shiggy.scavengerthqwibDef},
                { Survivors.Shiggy.scavengerthqwibDef.skillName, Survivors.Shiggy.beetlequeenSummonDef},

                { Survivors.Shiggy.vagrantpassiveDef.skillName, Survivors.Shiggy.claytemplarminigunDef},
                { Survivors.Shiggy.claytemplarminigunDef.skillName, Survivors.Shiggy.vagrantpassiveDef},

                { Survivors.Shiggy.greaterWispBuffDef.skillName, Survivors.Shiggy.grovetenderChainDef},
                { Survivors.Shiggy.grovetenderChainDef.skillName, Survivors.Shiggy.greaterWispBuffDef},

                { Survivors.Shiggy.voidjailerpassiveDef.skillName, Survivors.Shiggy.soluscontrolunityknockupDef},
                { Survivors.Shiggy.soluscontrolunityknockupDef.skillName, Survivors.Shiggy.voidjailerpassiveDef},

                { Survivors.Shiggy.voiddevastatorhomingDef.skillName, Survivors.Shiggy.huntressattackDef},
                { Survivors.Shiggy.huntressattackDef.skillName, Survivors.Shiggy.voiddevastatorhomingDef},

                { Survivors.Shiggy.stonegolemlaserDef.skillName, Survivors.Shiggy.bulletlaserDef},
            };

            
            passiveToBuff = new Dictionary<string, RoR2.BuffDef>
            {
                { Survivors.Shiggy.alphacontructpassiveDef.skillName, Buffs.alphashieldonBuff },
                { Survivors.Shiggy.beetlepassiveDef.skillName, Buffs.beetleBuff},
                { Survivors.Shiggy.pestpassiveDef.skillName, Buffs.pestjumpBuff},
                { Survivors.Shiggy.verminpassiveDef.skillName, Buffs.verminsprintBuff},
                { Survivors.Shiggy.guppassiveDef.skillName, Buffs.gupspikeBuff},
                { Survivors.Shiggy.hermitcrabpassiveDef.skillName, Buffs.hermitcrabmortarBuff},
                { Survivors.Shiggy.larvapassiveDef.skillName, Buffs.larvajumpBuff},
                { Survivors.Shiggy.lesserwisppassiveDef.skillName, Buffs.lesserwispBuff},
                { Survivors.Shiggy.lunarexploderpassiveDef.skillName, Buffs.lunarexploderBuff},
                { Survivors.Shiggy.minimushrumpassiveDef.skillName, Buffs.minimushrumBuff},
                { Survivors.Shiggy.roboballminibpassiveDef.skillName, Buffs.roboballminiBuff},
                { Survivors.Shiggy.voidbarnaclepassiveDef.skillName, Buffs.voidbarnaclemortarBuff},
                { Survivors.Shiggy.voidjailerpassiveDef.skillName, Buffs.voidjailerBuff},
                { Survivors.Shiggy.impbosspassiveDef.skillName, Buffs.impbossBuff},
                { Survivors.Shiggy.stonetitanpassiveDef.skillName, Buffs.stonetitanBuff},
                { Survivors.Shiggy.vagrantpassiveDef.skillName, Buffs.vagrantBuff},
                { Survivors.Shiggy.magmawormpassiveDef.skillName, Buffs.magmawormBuff},
                { Survivors.Shiggy.overloadingwormpassiveDef.skillName, Buffs.overloadingwormBuff},
                { Survivors.Shiggy.captainpassiveDef.skillName, Buffs.captainBuff},
                { Survivors.Shiggy.commandopassiveDef.skillName, Buffs.commandoBuff},
                { Survivors.Shiggy.acridpassiveDef.skillName, Buffs.acridBuff},
                { Survivors.Shiggy.loaderpassiveDef.skillName, Buffs.loaderBuff},
                { Survivors.Shiggy.bigBangPassiveDef.skillName, Buffs.bigbangBuff},
                { Survivors.Shiggy.wisperPassiveDef.skillName, Buffs.wisperBuff},
                { Survivors.Shiggy.omniboostPassiveDef.skillName, Buffs.omniboostBuff},
                { Survivors.Shiggy.gachaPassiveDef.skillName, Buffs.gachaBuff},
                { Survivors.Shiggy.stoneFormPassiveDef.skillName, Buffs.stoneFormBuff},
                { Survivors.Shiggy.auraOfBlightPassiveDef.skillName, Buffs.auraOfBlightBuff},
                { Survivors.Shiggy.barbedSpikesPassiveDef.skillName, Buffs.barbedSpikesBuff},
                { Survivors.Shiggy.ingrainPassiveDef.skillName, Buffs.ingrainBuff},
                { Survivors.Shiggy.doubleTimePassiveDef.skillName, Buffs.doubleTimeBuff},
                { Survivors.Shiggy.blindSensesPassiveDef.skillName, Buffs.blindSensesBuff},
                { Survivors.Shiggy.supernovaPassiveDef.skillName, Buffs.supernovaBuff},
                { Survivors.Shiggy.reversalPassiveDef.skillName, Buffs.reversalBuff},
                { Survivors.Shiggy.machineFormPassiveDef.skillName, Buffs.machineFormBuff},
                { Survivors.Shiggy.gargoyleProtectionPassiveDef.skillName, Buffs.gargoyleProtectionBuff},
                { Survivors.Shiggy.weatherReportPassiveDef.skillName, Buffs.weatherReportBuff},
                { Survivors.Shiggy.decayAwakenedPassiveDef.skillName, Buffs.decayAwakenedBuff},
            };


            quirkStringToInfoString = new Dictionary<string, string>
            {
                //passive skill to info
                { Survivors.Shiggy.alphacontructpassiveDef.skillName, "<style=cIsUtility>Barrier Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.beetlepassiveDef.skillName, "<style=cIsUtility>Strength Boost Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.pestpassiveDef.skillName, "<style=cIsUtility>Jump Boost Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.verminpassiveDef.skillName, "<style=cIsUtility>Super Speed Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.guppassiveDef.skillName, "<style=cIsUtility>Spiky Body Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.hermitcrabpassiveDef.skillName, "<style=cIsUtility>Mortar Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.larvapassiveDef.skillName, "<style=cIsUtility>Acid Jump Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.lesserwisppassiveDef.skillName, "<style=cIsUtility>Haste Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.lunarexploderpassiveDef.skillName, "<style=cIsUtility>Lunar Barrier Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.minimushrumpassiveDef.skillName, "<style=cIsUtility>Healing Aura Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.roboballminibpassiveDef.skillName, "<style=cIsUtility>Solus Boost Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.voidbarnaclepassiveDef.skillName, "<style=cIsUtility>Void Mortar Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.voidjailerpassiveDef.skillName, "<style=cIsUtility>Gravity Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.impbosspassiveDef.skillName, "<style=cIsUtility>Bleed Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.stonetitanpassiveDef.skillName, "<style=cIsUtility>Stone Skin Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.vagrantpassiveDef.skillName, "<style=cIsUtility>Vagrant's Orb Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.magmawormpassiveDef.skillName, "<style=cIsUtility>Blazing Aura Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.overloadingwormpassiveDef.skillName, "<style=cIsUtility>Lightning Aura Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.captainpassiveDef.skillName, "<style=cIsUtility>Defensive Microbots Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.commandopassiveDef.skillName, "<style=cIsUtility>Double Tap Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.acridpassiveDef.skillName, "<style=cIsUtility>Poison Quirk</style> Get! (Passive)" },
                { Survivors.Shiggy.loaderpassiveDef.skillName, "<style=cIsUtility>Scrap Barrier Quirk</style> Get! (Passive)" },
                //synergy passive to info
                { Survivors.Shiggy.bigBangPassiveDef.skillName, "<style=cIsUtility>Big Bang Synergy</style> Gained! (Passive)" },
                { Survivors.Shiggy.wisperPassiveDef.skillName, "<style=cIsUtility>Wisper Synergy</style> Gained! (Passive)" },
                { Survivors.Shiggy.omniboostPassiveDef.skillName, "<style=cIsUtility>Omniboost Synergy</style> Gained! (Passive)" },
                { Survivors.Shiggy.gachaPassiveDef.skillName, "<style=cIsUtility>Gacha Synergy</style> Gained! (Passive)" },
                { Survivors.Shiggy.stoneFormPassiveDef.skillName, "<style=cIsUtility>Stone Form Synergy</style> Gained! (Passive)" },
                { Survivors.Shiggy.auraOfBlightPassiveDef.skillName, "<style=cIsUtility>Aura Of Blight Synergy</style> Gained! (Passive)" },
                { Survivors.Shiggy.barbedSpikesPassiveDef.skillName, "<style=cIsUtility>Barbed Spikes Synergy</style> Gained! (Passive)" },
                { Survivors.Shiggy.ingrainPassiveDef.skillName, "<style=cIsUtility>Ingrain Synergy</style> Gained! (Passive)" },
                { Survivors.Shiggy.elementalFusionPassiveDef.skillName, "<style=cIsUtility>Elemental Fusion Synergy</style> Gained! (Passive)" },
                { Survivors.Shiggy.doubleTimePassiveDef.skillName, "<style=cIsUtility>Double Time Synergy</style> Gained! (Passive)" },
                { Survivors.Shiggy.blindSensesPassiveDef.skillName, "<style=cIsUtility>Blind Sense Synergy</style> Gained! (Passive)" },
                //ultimate passive skill to info                
                { Survivors.Shiggy.supernovaPassiveDef.skillName, "<style=cIsUtility>Supernova Ultimate Synergy</style> Created! (Passive)" },
                { Survivors.Shiggy.reversalPassiveDef.skillName, "<style=cIsUtility>Reversal Ultimate Synergy</style> Created! (Passive)" },
                { Survivors.Shiggy.machineFormPassiveDef.skillName, "<style=cIsUtility>Machine Form Ultimate Synergy</style> Created! (Passive)" },
                { Survivors.Shiggy.gargoyleProtectionPassiveDef.skillName, "<style=cIsUtility>Gargoyle Protection Ultimate Synergy</style> Created! (Passive)" },
                { Survivors.Shiggy.weatherReportPassiveDef.skillName, "<style=cIsUtility>Weather Report Ultimate Synergy</style> Created! (Passive)" },
                { Survivors.Shiggy.decayAwakenedPassiveDef.skillName, "<style=cIsUtility>Decay Awakened Ultimate Synergy</style> Created! (Passive)" },
                
                //active skill to info
                { Survivors.Shiggy.alloyvultureWindBlastDef.skillName, "<style=cIsUtility>Wind Blast Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.beetleguardslamDef.skillName, "<style=cIsUtility>Fast Drop Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.bisonchargeDef.skillName, "<style=cIsUtility>Charging Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.bronzongballDef.skillName, "<style=cIsUtility>Spiked Ball Control Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.clayapothecarymortarDef.skillName, "<style=cIsUtility>Clay AirStrike Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.claytemplarminigunDef.skillName, "<style=cIsUtility>Clay Minigun Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.elderlemurianfireblastDef.skillName, "<style=cIsUtility>Fire Blast Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.greaterWispBuffDef.skillName, "<style=cIsUtility>Spirit Boost Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.impblinkDef.skillName, "<style=cIsUtility>Blink Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.jellyfishHealDef.skillName, "<style=cIsUtility>Regenerate Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.lemurianfireballDef.skillName, "<style=cIsUtility>Fireball Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.lunargolemSlideDef.skillName, "<style=cIsUtility>Slide Reset Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.lunarwispminigunDef.skillName, "<style=cIsUtility>Lunar Minigun Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.parentteleportDef.skillName, "<style=cIsUtility>Teleport Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.stonegolemlaserDef.skillName, "<style=cIsUtility>Laser Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.voidreaverportalDef.skillName, "<style=cIsUtility>Nullifier Artillery Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.beetlequeenSummonDef.skillName, "<style=cIsUtility>Summon Ally Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.grandparentsunDef.skillName, "<style=cIsUtility>Solar Flare Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.grovetenderChainDef.skillName, "<style=cIsUtility>Chain Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.claydunestriderbuffDef.skillName, "<style=cIsUtility>Tar Boost Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.soluscontrolunityknockupDef.skillName, "<style=cIsUtility>Anti Gravity Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.xiconstructbeamDef.skillName, "<style=cIsUtility>Beam Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.voiddevastatorhomingDef.skillName, "<style=cIsUtility>Void Missiles Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.scavengerthqwibDef.skillName, "<style=cIsUtility>Throw Thqwibs Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.artificerflamethrowerDef.skillName, "<style=cIsUtility>Elementality Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.banditlightsoutDef.skillName, "<style=cIsUtility>Lights Out Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.engiturretDef.skillName, "<style=cIsUtility>Turret Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.huntressattackDef.skillName, "<style=cIsUtility>Flurry Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.mercdashDef.skillName, "<style=cIsUtility>Wind Assault Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.multbuffDef.skillName, "<style=cIsUtility>Power Stance Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.rexmortarDef.skillName, "<style=cIsUtility>Seed Barrage Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.railgunnercryoDef.skillName, "<style=cIsUtility>Cryocharged Railgun Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.voidfiendcleanseDef.skillName, "<style=cIsUtility>Cleanse Quirk</style> Get! (Active)" },
                { Survivors.Shiggy.DekuOFADef.skillName, "<style=cIsUtility>One For All Quirk</style> Get!!! (Active)" },
                //synergy active skill to info
                { Survivors.Shiggy.sweepingBeamDef.skillName, "<style=cIsUtility>Sweeping Beam Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.blackholeGlaiveDef.skillName, "<style=cIsUtility>Blackhole Glaive Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.gravitationalDownforceDef.skillName, "<style=cIsUtility>Gravitational Downforce Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.windShieldDef.skillName, "<style=cIsUtility>Wind Shield Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.genesisDef.skillName, "<style=cIsUtility>Genesis Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.refreshDef.skillName, "<style=cIsUtility>Refresh Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.expungeDef.skillName, "<style=cIsUtility>Expunge Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.shadowClawDef.skillName, "<style=cIsUtility>Shadow Claw Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.orbitalStrikeDef.skillName, "<style=cIsUtility>Orbital Strike Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.thunderclapDef.skillName, "<style=cIsUtility>Thunderclap Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.blastBurnDef.skillName, "<style=cIsUtility>Blast Burn Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.barrierJellyDef.skillName, "<style=cIsUtility>Barrier Jelly Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.mechStanceDef.skillName, "<style=cIsUtility>Mech Stance Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.windSlashDef.skillName, "<style=cIsUtility>Wind Slash Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.limitBreakDef.skillName, "<style=cIsUtility>Limit Break Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.voidFormDef.skillName, "<style=cIsUtility>Void Form Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.decayPlusUltraDef.skillName, "<style=cIsUtility>Decay Plus Ultra Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.machPunchDef.skillName, "<style=cIsUtility>Mach Punch Synergy</style> Gained! (Active)" },
                { Survivors.Shiggy.rapidPierceDef.skillName, "<style=cIsUtility>Rapid Pierce Synergy</style> Gained! (Active)" },
                //ultimate active skill to info
                { Survivors.Shiggy.theWorldDef.skillName, "<style=cIsUtility>The World Ultimate Synergy</style> Created! (Active)" },
                { Survivors.Shiggy.extremeSpeedDef.skillName, "<style=cIsUtility>Extreme Speed Ultimate Synergy</style> Created! (Active)" },
                { Survivors.Shiggy.deathAuraDef.skillName, "<style=cIsUtility>Death Aura Ultimate Synergy</style> Created! (Active)" },
                { Survivors.Shiggy.OFAFODef.skillName, "<style=cIsUtility>One For All For One Synergy</style> Created!!! (Active)" },
                { Survivors.Shiggy.xBeamerDef.skillName, "<style=cIsUtility>x Beamer Ultimate Synergy</style> Created! (Active)" },
                { Survivors.Shiggy.finalReleaseDef.skillName, "<style=cIsUtility>Final Release Ultimate Synergy</style> Created! (Active)" },
                { Survivors.Shiggy.wildCardDef.skillName, "<style=cIsUtility>Wild Card Ultimate Synergy</style> Created! (Active)" },
                { Survivors.Shiggy.lightAndDarknessDef.skillName, "<style=cIsUtility>Light And Darkness Ultimate Synergy</style> Created! (Active)" },
            };


            bodyNameToSkillDef = new Dictionary<string, RoR2.Skills.SkillDef>
            {
                { "MinorConstructBody", Survivors.Shiggy.alphacontructpassiveDef },
                { "MinorConstructOnKillBody", Survivors.Shiggy.alphacontructpassiveDef },
                { "BeetleBody", Survivors.Shiggy.beetlepassiveDef },
                { "FlyingVerminBody", Survivors.Shiggy.pestpassiveDef },
                { "VerminBody", Survivors.Shiggy.verminpassiveDef },
                { "GupBody", Survivors.Shiggy.guppassiveDef },
                { "GipBody", Survivors.Shiggy.guppassiveDef },
                { "GeepBody", Survivors.Shiggy.guppassiveDef },
                { "HermitCrabBody", Survivors.Shiggy.hermitcrabpassiveDef },
                { "AcidLarvaBody", Survivors.Shiggy.larvapassiveDef },
                { "WispBody", Survivors.Shiggy.lesserwisppassiveDef },
                { "LunarExploderBody", Survivors.Shiggy.lunarexploderpassiveDef },
                { "MiniMushroomBody", Survivors.Shiggy.minimushrumpassiveDef },
                { "RoboBallMiniBody", Survivors.Shiggy.roboballminibpassiveDef },
                { "RoboBallGreenBuddyBody", Survivors.Shiggy.roboballminibpassiveDef },
                { "RoboBallRedBuddyBody", Survivors.Shiggy.roboballminibpassiveDef },
                { "VoidBarnacleBody", Survivors.Shiggy.voidbarnaclepassiveDef },
                { "VoidJailerBody", Survivors.Shiggy.voidjailerpassiveDef },
                { "ImpBossBody", Survivors.Shiggy.impbosspassiveDef },
                { "TitanBody", Survivors.Shiggy.stonetitanpassiveDef },
                { "TitanGoldBody", Survivors.Shiggy.stonetitanpassiveDef },
                { "VagrantBody", Survivors.Shiggy.vagrantpassiveDef },
                { "MagmaWormBody", Survivors.Shiggy.magmawormpassiveDef },
                { "ElectricWormBody", Survivors.Shiggy.overloadingwormpassiveDef },
                { "ScavBody", Survivors.Shiggy.scavengerthqwibDef },
                { "CaptainBody", Survivors.Shiggy.captainpassiveDef },
                { "CommandoBody", Survivors.Shiggy.commandopassiveDef },
                { "CrocoBody", Survivors.Shiggy.acridpassiveDef },
                { "LoaderBody", Survivors.Shiggy.loaderpassiveDef },

                { "VultureBody", Survivors.Shiggy.alloyvultureWindBlastDef },
                { "BeetleGuardBody", Survivors.Shiggy.beetleguardslamDef },
                { "BisonBody", Survivors.Shiggy.bisonchargeDef },
                { "BellBody", Survivors.Shiggy.bronzongballDef },
                { "ClayGrenadierBody", Survivors.Shiggy.clayapothecarymortarDef },
                { "ClayBruiserBody", Survivors.Shiggy.claytemplarminigunDef },
                { "LemurianBruiserBody", Survivors.Shiggy.elderlemurianfireblastDef },
                { "GreaterWispBody", Survivors.Shiggy.greaterWispBuffDef },
                { "ImpBody", Survivors.Shiggy.impblinkDef },
                { "JellyfishBody", Survivors.Shiggy.jellyfishHealDef },
                { "LemurianBody", Survivors.Shiggy.lemurianfireballDef },
                { "LunarGolemBody", Survivors.Shiggy.lunargolemSlideDef },
                { "LunarWispBody", Survivors.Shiggy.lunarwispminigunDef },
                { "ParentBody", Survivors.Shiggy.parentteleportDef },
                { "GolemBody", Survivors.Shiggy.stonegolemlaserDef },
                { "NullifierBody", Survivors.Shiggy.voidreaverportalDef },
                { "BeetleQueen2Body", Survivors.Shiggy.beetlequeenSummonDef },
                { "GravekeeperBody", Survivors.Shiggy.grovetenderChainDef },
                { "ClayBossBody", Survivors.Shiggy.claydunestriderbuffDef },
                { "GrandParentBody", Survivors.Shiggy.grandparentsunDef },
                { "RoboBallBossBody", Survivors.Shiggy.soluscontrolunityknockupDef },
                { "SuperRoboBallBossBody", Survivors.Shiggy.soluscontrolunityknockupDef },
                { "MegaConstructBody", Survivors.Shiggy.xiconstructbeamDef },
                { "VoidMegaCrabBody", Survivors.Shiggy.voiddevastatorhomingDef },
                { "Bandit2Body", Survivors.Shiggy.banditlightsoutDef },
                { "EngiBody", Survivors.Shiggy.engiturretDef },
                { "HuntressBody", Survivors.Shiggy.huntressattackDef },
                { "MageBody", Survivors.Shiggy.artificerflamethrowerDef },
                { "MercBody", Survivors.Shiggy.mercdashDef },
                { "ToolbotBody", Survivors.Shiggy.multbuffDef },
                { "TreebotBody", Survivors.Shiggy.rexmortarDef },
                { "RailgunnerBody", Survivors.Shiggy.railgunnercryoDef }, 
                { "VoidSurvivorBody", Survivors.Shiggy.voidfiendcleanseDef },


                { "DekuBody", Survivors.Shiggy.DekuOFADef },
                { "ShopkeeperBody", Survivors.Shiggy.DekuOFADef },
            };


            skillDict = new Dictionary<string, skillType>
            {
                { Survivors.Shiggy.alphacontructpassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.beetlepassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.pestpassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.verminpassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.guppassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.hermitcrabpassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.larvapassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.lesserwisppassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.lunarexploderpassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.minimushrumpassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.roboballminibpassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.voidbarnaclepassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.voidjailerpassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.impbosspassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.stonetitanpassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.magmawormpassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.overloadingwormpassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.vagrantpassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.acridpassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.commandopassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.captainpassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.loaderpassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.bigBangPassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.wisperPassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.omniboostPassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.gachaPassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.stoneFormPassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.auraOfBlightPassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.barbedSpikesPassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.ingrainPassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.doubleTimePassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.blindSensesPassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.supernovaPassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.reversalPassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.machineFormPassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.gargoyleProtectionPassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.weatherReportPassiveDef.skillName, skillType.PASSIVE },
                { Survivors.Shiggy.decayAwakenedPassiveDef.skillName, skillType.PASSIVE },

                { Survivors.Shiggy.alloyvultureWindBlastDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.beetleguardslamDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.bisonchargeDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.bronzongballDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.clayapothecarymortarDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.claytemplarminigunDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.elderlemurianfireblastDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.greaterWispBuffDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.impblinkDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.jellyfishHealDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.lemurianfireballDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.lunargolemSlideDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.lunarwispminigunDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.parentteleportDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.stonegolemlaserDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.voidreaverportalDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.beetlequeenSummonDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.grandparentsunDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.grovetenderChainDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.claydunestriderbuffDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.soluscontrolunityknockupDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.xiconstructbeamDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.voiddevastatorhomingDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.scavengerthqwibDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.artificerflamethrowerDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.artificericewallDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.artificerlightningorbDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.banditlightsoutDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.engiturretDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.huntressattackDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.mercdashDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.multbuffDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.multbuffcancelDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.railgunnercryoDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.rexmortarDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.voidfiendcleanseDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.DekuOFADef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.decayDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.aircannonDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.bulletlaserDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.multiplierDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.chooseDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.removeDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.emptySkillDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.sweepingBeamDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.blackholeGlaiveDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.gravitationalDownforceDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.windShieldDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.genesisDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.refreshDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.expungeDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.shadowClawDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.orbitalStrikeDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.thunderclapDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.blastBurnDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.barrierJellyDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.mechStanceDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.windSlashDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.limitBreakDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.voidFormDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.elementalFusionPassiveDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.decayPlusUltraDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.machPunchDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.rapidPierceDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.theWorldDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.extremeSpeedDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.deathAuraDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.OFAFODef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.xBeamerDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.finalReleaseDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.blastingZoneDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.wildCardDef.skillName, skillType.ACTIVE },
                { Survivors.Shiggy.lightAndDarknessDef.skillName, skillType.ACTIVE },
            };

            indicatorDict = new Dictionary<string, IndicatorType>
            {
                { "MinorConstructBody", IndicatorType.PASSIVE },
                { "MinorConstructOnKillBody", IndicatorType.PASSIVE },
                { "BeetleBody", IndicatorType.PASSIVE },
                { "FlyingVerminBody", IndicatorType.PASSIVE },
                { "VerminBody", IndicatorType.PASSIVE },
                { "GupBody", IndicatorType.PASSIVE },
                { "GipBody", IndicatorType.PASSIVE },
                { "GeepBody", IndicatorType.PASSIVE },
                { "HermitCrabBody", IndicatorType.PASSIVE },
                { "AcidLarvaBody", IndicatorType.PASSIVE },
                { "WispBody", IndicatorType.PASSIVE },
                { "LunarExploderBody", IndicatorType.PASSIVE },
                { "MiniMushroomBody", IndicatorType.PASSIVE },
                { "RoboBallMiniBody", IndicatorType.PASSIVE },
                { "RoboBallGreenBuddyBody", IndicatorType.PASSIVE },
                { "RoboBallRedBuddyBody", IndicatorType.PASSIVE },
                { "VoidBarnacleBody", IndicatorType.PASSIVE },
                { "VoidJailerBody", IndicatorType.PASSIVE },
                { "ImpBossBody", IndicatorType.PASSIVE },
                { "TitanBody", IndicatorType.PASSIVE },
                { "TitanGoldBody", IndicatorType.PASSIVE },
                { "VagrantBody", IndicatorType.PASSIVE },
                { "MagmaWormBody", IndicatorType.PASSIVE },
                { "ElectricWormBody", IndicatorType.PASSIVE },
                { "ScavBody", IndicatorType.PASSIVE },
                { "CaptainBody", IndicatorType.PASSIVE },
                { "CommandoBody", IndicatorType.PASSIVE },
                { "CrocoBody", IndicatorType.PASSIVE },
                { "LoaderBody", IndicatorType.PASSIVE },

                { "VultureBody", IndicatorType.ACTIVE },
                { "BeetleGuardBody", IndicatorType.ACTIVE },
                { "BisonBody", IndicatorType.ACTIVE },
                { "BellBody", IndicatorType.ACTIVE },
                { "ClayGrenadierBody", IndicatorType.ACTIVE },
                { "ClayBruiserBody", IndicatorType.ACTIVE },
                { "LemurianBruiserBody", IndicatorType.ACTIVE },
                { "GreaterWispBody", IndicatorType.ACTIVE },
                { "ImpBody", IndicatorType.ACTIVE },
                { "JellyfishBody", IndicatorType.ACTIVE },
                { "LemurianBody", IndicatorType.ACTIVE },
                { "LunarGolemBody", IndicatorType.ACTIVE },
                { "LunarWispBody", IndicatorType.ACTIVE },
                { "ParentBody", IndicatorType.ACTIVE },
                { "GolemBody", IndicatorType.ACTIVE },
                { "NullifierBody", IndicatorType.ACTIVE },
                { "BeetleQueen2Body", IndicatorType.ACTIVE },
                { "GravekeeperBody", IndicatorType.ACTIVE },
                { "ClayBossBody", IndicatorType.ACTIVE },
                { "GrandParentBody", IndicatorType.ACTIVE },
                { "RoboBallBossBody", IndicatorType.ACTIVE },
                { "SuperRoboBallBossBody", IndicatorType.ACTIVE },
                { "MegaConstructBody", IndicatorType.ACTIVE },
                { "VoidMegaCrabBody", IndicatorType.ACTIVE },
                { "Bandit2Body", IndicatorType.ACTIVE },
                { "EngiBody", IndicatorType.ACTIVE },
                { "HuntressBody", IndicatorType.ACTIVE },
                { "MageBody", IndicatorType.ACTIVE },
                { "MercBody", IndicatorType.ACTIVE },
                { "ToolbotBody", IndicatorType.ACTIVE },
                { "TreebotBody", IndicatorType.ACTIVE },
                { "RailgunnerBody", IndicatorType.ACTIVE },
                { "VoidSurvivorBody", IndicatorType.ACTIVE },

                { "DekuBody", IndicatorType.ACTIVE },   
                { "ShopkeeperBody", IndicatorType.ACTIVE },
            };
        }
    }
}
