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
        internal static float costFlatPlusChaosSpend = 5f;
        internal static float costFlatContantlyDrainingCoefficient = 0.005f;
        internal static float regenPlusChaosFraction = 0.025f;
        internal static float backupGain = 10f;
        internal static float afterburnerGain = 30f;
        internal static float lysateGain = 15f;
        internal const float airwalkEnergyFraction = 0.1f;


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

        internal const float decayattackDamageCoefficient = 3f;
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
        internal const float rexHealthCost = 0.1f;

        internal const float railgunnerDamageCoefficient = 15f;
        internal const float railgunnerProcCoefficient = 2f;

        internal const float loaderBarrierGainCoefficient = 0.01f;

        //synergy skills
        internal const float sweepingBeamDamageCoefficient = 1f;
        internal const float sweepingBeamProcCoefficient = 0.2f;
        internal const uint sweepingBeamTotalBullets = 20;

        internal const float blackholeGlaiveDamageCoefficient = 1.2f;
        internal const float blackholeGlaiveDamageCoefficientPerBounce = 1.2f;
        internal const float blackholeGlaiveBounceRange = 40f;
        internal const float blackholeGlaiveBlastRange = 15f;
        internal const float blackholeGlaiveProcCoefficient = 0.8f;
        internal const int blackholeGlaiveMaxBounceCount = 5;
        internal const float blackholeGlaiveTravelSpeed = 30f;

        internal const int bigbangBuffThreshold = 5;
        internal const float bigbangBuffHealthCoefficient = 0.2f;
        internal const float bigbangBuffRadius= 20f;

        internal const float wisperBuffDamageCoefficient = 2f;

        internal const float omniboostBuffCoefficient = 0.3f;
        internal const float omniboostBuffStackCoefficient = 0.05f;
        internal const int omniboostNumberOfHits = 3;
        internal const float omniboostBuffTimer = 6f;

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
        internal const float expungeRadius = 7f;
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
        internal const float thunderclapRadius = 7f;
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
        internal static float reversalProcCoefficient = 1f;

        internal static float OFAFOHealthCostCoefficient = 0.1f;
        internal static float OFAFOEnergyCostCoefficient = 0.1f;
        internal static float OFAFOThreshold = 0.5f;
        internal static float OFAFOLifestealCoefficient = 0.2f;
        internal static float OFAFOEnergyGainCoefficient = 0.2f;
        internal static float OFAFOETimeMultiplierCoefficient = 2f;

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

        internal static float machineFormThreshold = 0.5f;
        internal static float machineFormDamageCoefficient = 1f;
        internal static float machineFormRadius = 50f;

        internal static float gargoyleProtectionDamageReductionCoefficient = 0.3f;


        public enum IndicatorType : uint
        {
            PASSIVE = 1,
            ACTIVE = 2,
        }

        public static Dictionary<string, IndicatorType> indicatorDict;
        public static Dictionary<string, RoR2.Skills.SkillDef> baseQuirkSkillDef;
        public static Dictionary<RoR2.Skills.SkillDef, string> baseQuirkSkillString;
        public static Dictionary<RoR2.Skills.SkillDef, RoR2.BuffIndex> baseQuirkSkilltoBuff;
        public static Dictionary<RoR2.Skills.SkillDef, RoR2.Skills.SkillDef> baseQuirkSkillbaseUpgradeCheck;
        public static Dictionary<RoR2.Skills.SkillDef, RoR2.Skills.SkillDef> baseQuirkandUpgrade;

        //public static Dictionary<RoR2.Skills.SkillDef, >
        public static void LoadDictionary()
        {

            //baseQuirkandUpgrade = new Dictionary<RoR2.Skills.SkillDef, RoR2.Skills.SkillDef>
            //{
            //    { Survivors.Shiggy.alphacontructpassiveDef, Survivors.Shiggy.alphaconstructandxiconstructDef},
            //    { Survivors.Shiggy.beetlepassiveDef, Survivors.Shiggy.beetlepassiveUpgradedDef },
            //    { Survivors.Shiggy.pestpassiveDef, Survivors.Shiggy.pestpassiveUpgradedDef },
            //    { Survivors.Shiggy.verminpassiveDef, Survivors.Shiggy.verminpassiveUpgradedDef },
            //    { Survivors.Shiggy.guppassiveDef, Survivors.Shiggy.guppassiveUpgradedDef },
            //    { Survivors.Shiggy.hermitcrabpassiveDef, Survivors.Shiggy.hermitcrabpassiveUpgradedDef },
            //    { Survivors.Shiggy.larvapassiveDef, Survivors.Shiggy.larvapassiveUpgradedDef },
            //    { Survivors.Shiggy.lesserwisppassiveDef, Survivors.Shiggy.lesserwisppassiveUpgradedDef },
            //    { Survivors.Shiggy.lunarexploderpassiveDef, Survivors.Shiggy.lunarexploderpassiveUpgradedDef },
            //    { Survivors.Shiggy.minimushrumpassiveDef, Survivors.Shiggy.minimushrumpassiveUpgradedDef },
            //    { Survivors.Shiggy.roboballminibpassiveDef, Survivors.Shiggy.roboballminibpassiveUpgradedDef },
            //    { Survivors.Shiggy.voidbarnaclepassiveDef, Survivors.Shiggy.voidbarnaclepassiveUpgradedDef },
            //    { Survivors.Shiggy.voidjailerpassiveDef, Survivors.Shiggy.voidjailerpassiveUpgradedDef },
            //    { Survivors.Shiggy.impbosspassiveDef, Survivors.Shiggy.impbosspassiveUpgradedDef },
            //    { Survivors.Shiggy.stonetitanpassiveDef, Survivors.Shiggy.stonetitanpassiveUpgradedDef },
            //    { Survivors.Shiggy.vagrantpassiveDef, Survivors.Shiggy.vagrantpassiveUpgradedDef },
            //    { Survivors.Shiggy.magmawormpassiveDef, Survivors.Shiggy.magmawormpassiveUpgradedDef },
            //    { Survivors.Shiggy.overloadingwormpassiveDef, Survivors.Shiggy.overloadingwormpassiveUpgradedDef },
            //    { Survivors.Shiggy.scavengerthqwibDef, Survivors.Shiggy.scavengerthqwibUpgradedDef },
            //    { Survivors.Shiggy.captainpassiveDef, Survivors.Shiggy.captainpassiveUpgradedDef },
            //    { Survivors.Shiggy.commandopassiveDef, Survivors.Shiggy.commandopassiveUpgradedDef },
            //    { Survivors.Shiggy.acridpassiveDef, Survivors.Shiggy.acridpassiveUpgradedDef },
            //    { Survivors.Shiggy.loaderpassiveDef, Survivors.Shiggy.loaderpassiveUpgradedDef },

            //    { Survivors.Shiggy.alloyvultureWindBlastDef, Survivors.Shiggy.alloyvultureflyUpgradedDef },
            //    { Survivors.Shiggy.beetleguardslamDef, Survivors.Shiggy.beetleguardslamUpgradedDef },
            //    { Survivors.Shiggy.bisonchargeDef, Survivors.Shiggy.bisonchargeUpgradedDef },
            //    { Survivors.Shiggy.bronzongballDef, Survivors.Shiggy.bronzongballUpgradedDef },
            //    { Survivors.Shiggy.clayapothecarymortarDef, Survivors.Shiggy.clayapothecarymortarUpgradedDef },
            //    { Survivors.Shiggy.claytemplarminigunDef, Survivors.Shiggy.claytemplarminigunUpgradedDef },
            //    { Survivors.Shiggy.elderlemurianfireblastDef, Survivors.Shiggy.elderlemurianfireblastUpgradedDef },
            //    { Survivors.Shiggy.greaterwispballDef, Survivors.Shiggy.greaterwispballUpgradedDef },
            //    { Survivors.Shiggy.impblinkDef, Survivors.Shiggy.impblinkUpgradedDef },
            //    { Survivors.Shiggy.jellyfishHealDef, Survivors.Shiggy.JellyfishHealUpgradedDef },
            //    { Survivors.Shiggy.lemurianfireballDef, Survivors.Shiggy.lemurianfireballUpgradedDef },
            //    { Survivors.Shiggy.lunargolemSlideDef, Survivors.Shiggy.lunargolemslideUpgradedDef },
            //    { Survivors.Shiggy.lunarwispminigunDef, Survivors.Shiggy.lunarwispminigunUpgradedDef },
            //    { Survivors.Shiggy.parentteleportDef, Survivors.Shiggy.parentteleportUpgradedDef },
            //    { Survivors.Shiggy.stonegolemlaserDef, Survivors.Shiggy.stonegolemlaserUpgradedDef },
            //    { Survivors.Shiggy.voidreaverportalDef, Survivors.Shiggy.voidreaverportalUpgradedDef },
            //    { Survivors.Shiggy.beetlequeenSummonDef, Survivors.Shiggy.beetlequeenshotgunUpgradedDef },
            //    { Survivors.Shiggy.claydunestriderbuffDef, Survivors.Shiggy.claydunestriderbuffUpgradedDef },
            //    { Survivors.Shiggy.grandparentsunDef, Survivors.Shiggy.grandparentsunUpgradedDef },
            //    { Survivors.Shiggy.soluscontrolunityknockupDef, Survivors.Shiggy.soluscontrolunityknockupUpgradedDef },
            //    { Survivors.Shiggy.xiconstructbeamDef, Survivors.Shiggy.xiconstructbeamUpgradedDef },
            //    { Survivors.Shiggy.voiddevastatorhomingDef, Survivors.Shiggy.voiddevastatorhomingUpgradedDef },
            //    { Survivors.Shiggy.banditlightsoutDef, Survivors.Shiggy.banditlightsoutUpgradedDef },
            //    { Survivors.Shiggy.engiturretDef, Survivors.Shiggy.engiturretUpgradedDef },
            //    { Survivors.Shiggy.huntressattackDef, Survivors.Shiggy.huntressattackUpgradedDef },
            //    { Survivors.Shiggy.artificerflamethrowerDef, Survivors.Shiggy.artificerflamethrowerUpgradedDef },
            //    { Survivors.Shiggy.mercdashDef, Survivors.Shiggy.mercdashUpgradedDef },
            //    { Survivors.Shiggy.multbuffDef, Survivors.Shiggy.multbuffUpgradedDef },
            //    { Survivors.Shiggy.rexmortarDef, Survivors.Shiggy.rexmortarUpgradedDef },
            //    { Survivors.Shiggy.railgunnercryoDef, Survivors.Shiggy.railgunnercryoUpgradedDef },
            //    { Survivors.Shiggy.voidfiendcleanseDef, Survivors.Shiggy.voidfiendcleanseUpgradedDef }
            //};


            //baseQuirkSkillbaseUpgradeCheck = new Dictionary<RoR2.Skills.SkillDef, RoR2.Skills.SkillDef>
            //{
            //    { Survivors.Shiggy.alphacontructpassiveDef, Survivors.Shiggy.xiconstructbeamDef},
            //    { Survivors.Shiggy.beetlepassiveDef, Survivors.Shiggy.beetlepassiveUpgradedDef },
            //    { Survivors.Shiggy.pestpassiveDef, Survivors.Shiggy.pestpassiveUpgradedDef },
            //    { Survivors.Shiggy.verminpassiveDef, Survivors.Shiggy.verminpassiveUpgradedDef },
            //    { Survivors.Shiggy.guppassiveDef, Survivors.Shiggy.guppassiveUpgradedDef },
            //    { Survivors.Shiggy.hermitcrabpassiveDef, Survivors.Shiggy.hermitcrabpassiveUpgradedDef },
            //    { Survivors.Shiggy.larvapassiveDef, Survivors.Shiggy.larvapassiveUpgradedDef },
            //    { Survivors.Shiggy.lesserwisppassiveDef, Survivors.Shiggy.lesserwisppassiveUpgradedDef },
            //    { Survivors.Shiggy.lunarexploderpassiveDef, Survivors.Shiggy.lunarexploderpassiveUpgradedDef },
            //    { Survivors.Shiggy.minimushrumpassiveDef, Survivors.Shiggy.minimushrumpassiveUpgradedDef },
            //    { Survivors.Shiggy.roboballminibpassiveDef, Survivors.Shiggy.roboballminibpassiveUpgradedDef },
            //    { Survivors.Shiggy.voidbarnaclepassiveDef, Survivors.Shiggy.voidbarnaclepassiveUpgradedDef },
            //    { Survivors.Shiggy.voidjailerpassiveDef, Survivors.Shiggy.voidjailerpassiveUpgradedDef },
            //    { Survivors.Shiggy.impbosspassiveDef, Survivors.Shiggy.impbosspassiveUpgradedDef },
            //    { Survivors.Shiggy.stonetitanpassiveDef, Survivors.Shiggy.stonetitanpassiveUpgradedDef },
            //    { Survivors.Shiggy.vagrantpassiveDef, Survivors.Shiggy.vagrantpassiveUpgradedDef },
            //    { Survivors.Shiggy.magmawormpassiveDef, Survivors.Shiggy.magmawormpassiveUpgradedDef },
            //    { Survivors.Shiggy.overloadingwormpassiveDef, Survivors.Shiggy.overloadingwormpassiveUpgradedDef },
            //    { Survivors.Shiggy.scavengerthqwibDef, Survivors.Shiggy.scavengerthqwibUpgradedDef },
            //    { Survivors.Shiggy.captainpassiveDef, Survivors.Shiggy.captainpassiveUpgradedDef },
            //    { Survivors.Shiggy.commandopassiveDef, Survivors.Shiggy.commandopassiveUpgradedDef },
            //    { Survivors.Shiggy.acridpassiveDef, Survivors.Shiggy.acridpassiveUpgradedDef },
            //    { Survivors.Shiggy.loaderpassiveDef, Survivors.Shiggy.loaderpassiveUpgradedDef },

            //    { Survivors.Shiggy.alloyvultureWindBlastDef, Survivors.Shiggy.alloyvultureflyUpgradedDef },
            //    { Survivors.Shiggy.beetleguardslamDef, Survivors.Shiggy.beetleguardslamUpgradedDef },
            //    { Survivors.Shiggy.bisonchargeDef, Survivors.Shiggy.bisonchargeUpgradedDef },
            //    { Survivors.Shiggy.bronzongballDef, Survivors.Shiggy.bronzongballUpgradedDef },
            //    { Survivors.Shiggy.clayapothecarymortarDef, Survivors.Shiggy.clayapothecarymortarUpgradedDef },
            //    { Survivors.Shiggy.claytemplarminigunDef, Survivors.Shiggy.claytemplarminigunUpgradedDef },
            //    { Survivors.Shiggy.elderlemurianfireblastDef, Survivors.Shiggy.elderlemurianfireblastUpgradedDef },
            //    { Survivors.Shiggy.greaterwispballDef, Survivors.Shiggy.greaterwispballUpgradedDef },
            //    { Survivors.Shiggy.impblinkDef, Survivors.Shiggy.impblinkUpgradedDef },
            //    { Survivors.Shiggy.jellyfishHealDef, Survivors.Shiggy.JellyfishHealUpgradedDef },
            //    { Survivors.Shiggy.lemurianfireballDef, Survivors.Shiggy.lemurianfireballUpgradedDef },
            //    { Survivors.Shiggy.lunargolemSlideDef, Survivors.Shiggy.lunargolemslideUpgradedDef },
            //    { Survivors.Shiggy.lunarwispminigunDef, Survivors.Shiggy.lunarwispminigunUpgradedDef },
            //    { Survivors.Shiggy.parentteleportDef, Survivors.Shiggy.parentteleportUpgradedDef },
            //    { Survivors.Shiggy.stonegolemlaserDef, Survivors.Shiggy.stonegolemlaserUpgradedDef },
            //    { Survivors.Shiggy.voidreaverportalDef, Survivors.Shiggy.voidreaverportalUpgradedDef },
            //    { Survivors.Shiggy.beetlequeenSummonDef, Survivors.Shiggy.beetlequeenshotgunUpgradedDef },
            //    { Survivors.Shiggy.claydunestriderbuffDef, Survivors.Shiggy.claydunestriderbuffUpgradedDef },
            //    { Survivors.Shiggy.grandparentsunDef, Survivors.Shiggy.grandparentsunUpgradedDef },
            //    { Survivors.Shiggy.soluscontrolunityknockupDef, Survivors.Shiggy.soluscontrolunityknockupUpgradedDef },
            //    { Survivors.Shiggy.xiconstructbeamDef, Survivors.Shiggy.xiconstructbeamUpgradedDef },
            //    { Survivors.Shiggy.voiddevastatorhomingDef, Survivors.Shiggy.voiddevastatorhomingUpgradedDef },
            //    { Survivors.Shiggy.banditlightsoutDef, Survivors.Shiggy.banditlightsoutUpgradedDef },
            //    { Survivors.Shiggy.engiturretDef, Survivors.Shiggy.engiturretUpgradedDef },
            //    { Survivors.Shiggy.huntressattackDef, Survivors.Shiggy.huntressattackUpgradedDef },
            //    { Survivors.Shiggy.artificerflamethrowerDef, Survivors.Shiggy.artificerflamethrowerUpgradedDef },
            //    { Survivors.Shiggy.mercdashDef, Survivors.Shiggy.mercdashUpgradedDef },
            //    { Survivors.Shiggy.multbuffDef, Survivors.Shiggy.multbuffUpgradedDef },
            //    { Survivors.Shiggy.rexmortarDef, Survivors.Shiggy.rexmortarUpgradedDef },
            //    { Survivors.Shiggy.railgunnercryoDef, Survivors.Shiggy.railgunnercryoUpgradedDef },
            //    { Survivors.Shiggy.voidfiendcleanseDef, Survivors.Shiggy.voidfiendcleanseUpgradedDef }
            //};

            //baseQuirkSkillUpgradeCheck = new Dictionary<RoR2.Skills.SkillDef, List<RoR2.Skills.SkillDef>>();
            //var alphaconstruct = new List<RoR2.Skills.SkillDef>
            //{
            //    Survivors.Shiggy.xiconstructbeamDef,
            //    Survivors.Shiggy.xiconstructbeamUpgradedDef,
            //    Survivors.Shiggy.clayapothecarymortarUpgradedDef
            //};
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.alphacontructpassiveDef, alphaconstruct);
            //var beetle = new List<RoR2.Skills.SkillDef>
            //{
            //    Survivors.Shiggy.beetlequeenSummonDef,
            //    Survivors.Shiggy.beetlequeenshotgunUpgradedDef,
            //    Survivors.Shiggy.lesserwisppassiveUpgradedDef
            //};
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.beetlepassiveDef, beetle);
            //var pest = new List<RoR2.Skills.SkillDef>
            //{
            //    Survivors.Shiggy.aircannonDef,
            //    Survivors.Shiggy.aircannonUpgradedDef,
            //    Survivors.Shiggy.verminpassiveUpgradedDef
            //};
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.pestpassiveDef, pest);
            //var vermin = new List<RoR2.Skills.SkillDef>
            //{
            //    Survivors.Shiggy.aircannonDef,
            //    Survivors.Shiggy.aircannonUpgradedDef,
            //    Survivors.Shiggy.pestpassiveUpgradedDef
            //};
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.verminpassiveDef, vermin);
            //var gup = new List<RoR2.Skills.SkillDef>
            //{
            //    Survivors.Shiggy.bronzongballDef,
            //    Survivors.Shiggy.bronzongballUpgradedDef,
            //};
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.guppassiveDef, gup);
            //var hermit = new List<RoR2.Skills.SkillDef>
            //{
            //    Survivors.Shiggy.voiddevastatorhomingDef,
            //    Survivors.Shiggy.voiddevastatorhomingUpgradedDef,
            //};
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.hermitcrabpassiveDef, hermit);
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.larvapassiveDef, Survivors.Shiggy.lunargolemSlideDef);
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.lesserwisppassiveDef, Survivors.Shiggy.beetlequeenSummonDef);
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.lunarexploderpassiveDef, Survivors.Shiggy.minimushrumpassiveDef);
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.minimushrumpassiveDef, Survivors.Shiggy.claydunestriderbuffDef);
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.roboballminibpassiveDef, Survivors.Shiggy.alloyvultureWindBlastDef);
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.voidbarnaclepassiveDef, Survivors.Shiggy.voidjailerpassiveDef);
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.voidjailerpassiveDef, Survivors.Shiggy.voidreaverportalDef);
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.impbosspassiveDef, Survivors.Shiggy.impbosspassiveDef);
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.stonetitanpassiveDef, Survivors.Shiggy.lunarexploderpassiveDef);
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.vagrantpassiveDef, Survivors.Shiggy.greaterwispballDef);
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.magmawormpassiveDef, Survivors.Shiggy.overloadingwormpassiveDef);
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.overloadingwormpassiveDef, Survivors.Shiggy.captainpassiveDef);
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.scavengerthqwibDef, "<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.captainpassiveDef, "<style=cIsUtility>Defensive Microbots Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.commandopassiveDef, "<style=cIsUtility>Double Tap Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.acridpassiveDef, "<style=cIsUtility>Poison Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.loaderpassiveDef, "<style=cIsUtility>Scrap Barrier Quirk</style> Get!");

            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.alloyvultureWindBlastDef, "<style=cIsUtility>Flight Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.beetleguardslamDef, "<style=cIsUtility>Beetle Armor Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.bisonchargeDef, "<style=cIsUtility>Charging Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.bronzongballDef, "<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.clayapothecarymortarDef, "<style=cIsUtility>Clay AirStrike Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.claytemplarminigunDef, "<style=cIsUtility>Clay Minigun Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.elderlemurianfireblastDef, "<style=cIsUtility>Fire Blast Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.greaterwispballDef, "<style=cIsUtility>Spirit Boost Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.impblinkDef, "<style=cIsUtility>Blink Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.jellyfishHealDef, "<style=cIsUtility>Nova Explosion Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.lemurianfireballDef, "<style=cIsUtility>Fireball Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.lunargolemSlideDef, "<style=cIsUtility>Slide Reset Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.lunarwispminigunDef, "<style=cIsUtility>Lunar Minigun Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.parentteleportDef, "<style=cIsUtility>Teleport Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.stonegolemlaserDef, "<style=cIsUtility>Laser Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.voidreaverportalDef, "<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.beetlequeenSummonDef, "<style=cIsUtility>Acid Shotgun Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.claydunestriderbuffDef, "<style=cIsUtility>Tar Boost Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.grandparentsunDef, "<style=cIsUtility>Solar Flare Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.soluscontrolunityknockupDef, "<style=cIsUtility>Anti Gravity Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.xiconstructbeamDef, "<style=cIsUtility>Beam Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.voiddevastatorhomingDef, "<style=cIsUtility>Void Missiles Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.banditlightsoutDef, "<style=cIsUtility>Lights Out Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.engiturretDef, "<style=cIsUtility>Turret Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.huntressattackDef, "<style=cIsUtility>Flurry Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.artificerflamethrowerDef, "<style=cIsUtility>Elementality Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.mercdashDef, "<style=cIsUtility>Eviscerate Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.multbuffDef, "<style=cIsUtility>Power Stance Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.rexmortarDef, "<style=cIsUtility>Seed Barrage Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.railgunnercryoDef, "<style=cIsUtility>Cryocharged Railgun Quirk</style> Get!");
            //baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.voidfiendcleanseDef, "<style=cIsUtility>Cleanse Quirk</style> Get!");

            baseQuirkSkilltoBuff = new Dictionary<RoR2.Skills.SkillDef, RoR2.BuffIndex>
            {
                { Survivors.Shiggy.alphacontructpassiveDef, Buffs.alphashieldonBuff.buffIndex },
                { Survivors.Shiggy.beetlepassiveDef, Buffs.beetleBuff.buffIndex },
                { Survivors.Shiggy.pestpassiveDef, Buffs.pestjumpBuff.buffIndex },
                { Survivors.Shiggy.verminpassiveDef, Buffs.verminsprintBuff.buffIndex },
                { Survivors.Shiggy.guppassiveDef, Buffs.gupspikeBuff.buffIndex },
                { Survivors.Shiggy.hermitcrabpassiveDef, Buffs.hermitcrabmortarBuff.buffIndex },
                { Survivors.Shiggy.larvapassiveDef, Buffs.larvajumpBuff.buffIndex },
                { Survivors.Shiggy.lesserwisppassiveDef, Buffs.lesserwispBuff.buffIndex },
                { Survivors.Shiggy.lunarexploderpassiveDef, Buffs.lunarexploderBuff.buffIndex },
                { Survivors.Shiggy.minimushrumpassiveDef, Buffs.minimushrumBuff.buffIndex },
                { Survivors.Shiggy.roboballminibpassiveDef, Buffs.roboballminiBuff.buffIndex },
                { Survivors.Shiggy.voidbarnaclepassiveDef, Buffs.voidbarnaclemortarBuff.buffIndex },
                { Survivors.Shiggy.voidjailerpassiveDef, Buffs.voidjailerBuff.buffIndex },
                { Survivors.Shiggy.impbosspassiveDef, Buffs.impbossBuff.buffIndex },
                { Survivors.Shiggy.stonetitanpassiveDef, Buffs.stonetitanBuff.buffIndex },
                { Survivors.Shiggy.vagrantpassiveDef, Buffs.vagrantBuff.buffIndex },
                { Survivors.Shiggy.magmawormpassiveDef, Buffs.magmawormBuff.buffIndex },
                { Survivors.Shiggy.overloadingwormpassiveDef, Buffs.overloadingwormBuff.buffIndex },
                { Survivors.Shiggy.scavengerthqwibDef, Buffs.overloadingwormBuff.buffIndex }, //need to update scavenger to passive
                { Survivors.Shiggy.captainpassiveDef, Buffs.captainBuff.buffIndex },
                { Survivors.Shiggy.commandopassiveDef, Buffs.commandoBuff.buffIndex },
                { Survivors.Shiggy.acridpassiveDef, Buffs.acridBuff.buffIndex },
                { Survivors.Shiggy.loaderpassiveDef, Buffs.loaderBuff.buffIndex }
            };


            baseQuirkSkillString = new Dictionary<RoR2.Skills.SkillDef, string>
            {
                { Survivors.Shiggy.alphacontructpassiveDef, "<style=cIsUtility>Barrier Quirk</style> Get!" },
                { Survivors.Shiggy.beetlepassiveDef, "<style=cIsUtility>Strength Boost Quirk</style> Get!" },
                { Survivors.Shiggy.pestpassiveDef, "<style=cIsUtility>Jump Boost Quirk</style> Get!" },
                { Survivors.Shiggy.verminpassiveDef, "<style=cIsUtility>Super Speed Quirk</style> Get!" },
                { Survivors.Shiggy.guppassiveDef, "<style=cIsUtility>Spiky Body Quirk</style> Get!" },
                { Survivors.Shiggy.hermitcrabpassiveDef, "<style=cIsUtility>Mortar Quirk</style> Get!" },
                { Survivors.Shiggy.larvapassiveDef, "<style=cIsUtility>Acid Jump Quirk</style> Get!" },
                { Survivors.Shiggy.lesserwisppassiveDef, "<style=cIsUtility>Ranged Boost Quirk</style> Get!" },
                { Survivors.Shiggy.lunarexploderpassiveDef, "<style=cIsUtility>Lunar Aura Quirk</style> Get!" },
                { Survivors.Shiggy.minimushrumpassiveDef, "<style=cIsUtility>Healing Aura Quirk</style> Get!" },
                { Survivors.Shiggy.roboballminibpassiveDef, "<style=cIsUtility>Solus Boost Quirk</style> Get!" },
                { Survivors.Shiggy.voidbarnaclepassiveDef, "<style=cIsUtility>Void Mortar Quirk</style> Get!" },
                { Survivors.Shiggy.voidjailerpassiveDef, "<style=cIsUtility>Gravity Quirk</style> Get!" },
                { Survivors.Shiggy.impbosspassiveDef, "<style=cIsUtility>Bleed Quirk</style> Get!" },
                { Survivors.Shiggy.stonetitanpassiveDef, "<style=cIsUtility>Stone Skin Quirk</style> Get!" },
                { Survivors.Shiggy.vagrantpassiveDef, "<style=cIsUtility>Vagrant's Orb Quirk</style> Get!" },
                { Survivors.Shiggy.magmawormpassiveDef, "<style=cIsUtility>Blazing Aura Quirk</style> Get!" },
                { Survivors.Shiggy.overloadingwormpassiveDef, "<style=cIsUtility>Lightning Aura Quirk</style> Get!" },
                { Survivors.Shiggy.scavengerthqwibDef, "<style=cIsUtility>Throw Thqwibs Quirk</style> Get!" },
                { Survivors.Shiggy.captainpassiveDef, "<style=cIsUtility>Defensive Microbots Quirk</style> Get!" },
                { Survivors.Shiggy.commandopassiveDef, "<style=cIsUtility>Double Tap Quirk</style> Get!" },
                { Survivors.Shiggy.acridpassiveDef, "<style=cIsUtility>Poison Quirk</style> Get!" },
                { Survivors.Shiggy.loaderpassiveDef, "<style=cIsUtility>Scrap Barrier Quirk</style> Get!" },

                { Survivors.Shiggy.alloyvultureWindBlastDef, "<style=cIsUtility>Flight Quirk</style> Get!" },
                { Survivors.Shiggy.beetleguardslamDef, "<style=cIsUtility>Beetle Armor Quirk</style> Get!" },
                { Survivors.Shiggy.bisonchargeDef, "<style=cIsUtility>Charging Quirk</style> Get!" },
                { Survivors.Shiggy.bronzongballDef, "<style=cIsUtility>Spiked Ball Control Quirk</style> Get!" },
                { Survivors.Shiggy.clayapothecarymortarDef, "<style=cIsUtility>Clay AirStrike Quirk</style> Get!" },
                { Survivors.Shiggy.claytemplarminigunDef, "<style=cIsUtility>Clay Minigun Quirk</style> Get!" },
                { Survivors.Shiggy.elderlemurianfireblastDef, "<style=cIsUtility>Fire Blast Quirk</style> Get!" },
                { Survivors.Shiggy.greaterwispballDef, "<style=cIsUtility>Spirit Boost Quirk</style> Get!" },
                { Survivors.Shiggy.impblinkDef, "<style=cIsUtility>Blink Quirk</style> Get!" },
                { Survivors.Shiggy.jellyfishHealDef, "<style=cIsUtility>Nova Explosion Quirk</style> Get!" },
                { Survivors.Shiggy.lemurianfireballDef, "<style=cIsUtility>Fireball Quirk</style> Get!" },
                { Survivors.Shiggy.lunargolemSlideDef, "<style=cIsUtility>Slide Reset Quirk</style> Get!" },
                { Survivors.Shiggy.lunarwispminigunDef, "<style=cIsUtility>Lunar Minigun Quirk</style> Get!" },
                { Survivors.Shiggy.parentteleportDef, "<style=cIsUtility>Teleport Quirk</style> Get!" },
                { Survivors.Shiggy.stonegolemlaserDef, "<style=cIsUtility>Laser Quirk</style> Get!" },
                { Survivors.Shiggy.voidreaverportalDef, "<style=cIsUtility>Nullifier Artillery Quirk</style> Get!" },
                { Survivors.Shiggy.beetlequeenSummonDef, "<style=cIsUtility>Acid Shotgun Quirk</style> Get!" },
                { Survivors.Shiggy.claydunestriderbuffDef, "<style=cIsUtility>Tar Boost Quirk</style> Get!" },
                { Survivors.Shiggy.grandparentsunDef, "<style=cIsUtility>Solar Flare Quirk</style> Get!" },
                { Survivors.Shiggy.soluscontrolunityknockupDef, "<style=cIsUtility>Anti Gravity Quirk</style> Get!" },
                { Survivors.Shiggy.xiconstructbeamDef, "<style=cIsUtility>Beam Quirk</style> Get!" },
                { Survivors.Shiggy.voiddevastatorhomingDef, "<style=cIsUtility>Void Missiles Quirk</style> Get!" },
                { Survivors.Shiggy.banditlightsoutDef, "<style=cIsUtility>Lights Out Quirk</style> Get!" },
                { Survivors.Shiggy.engiturretDef, "<style=cIsUtility>Turret Quirk</style> Get!" },
                { Survivors.Shiggy.huntressattackDef, "<style=cIsUtility>Flurry Quirk</style> Get!" },
                { Survivors.Shiggy.artificerflamethrowerDef, "<style=cIsUtility>Elementality Quirk</style> Get!" },
                { Survivors.Shiggy.mercdashDef, "<style=cIsUtility>Eviscerate Quirk</style> Get!" },
                { Survivors.Shiggy.multbuffDef, "<style=cIsUtility>Power Stance Quirk</style> Get!" },
                { Survivors.Shiggy.rexmortarDef, "<style=cIsUtility>Seed Barrage Quirk</style> Get!" },
                { Survivors.Shiggy.railgunnercryoDef, "<style=cIsUtility>Cryocharged Railgun Quirk</style> Get!" },
                { Survivors.Shiggy.voidfiendcleanseDef, "<style=cIsUtility>Cleanse Quirk</style> Get!" }
            };


            baseQuirkSkillDef = new Dictionary<string, RoR2.Skills.SkillDef>
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
                { "GreaterWispBody", Survivors.Shiggy.greaterwispballDef },
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
                { "VoidSurvivorBody", Survivors.Shiggy.voidfiendcleanseDef }
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
                { "VoidSurvivorBody", IndicatorType.ACTIVE }
            };
        }
    }
}
