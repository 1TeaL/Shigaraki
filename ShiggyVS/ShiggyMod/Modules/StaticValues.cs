using IL.RoR2;
using IL.RoR2.Skills;
using On.RoR2.Skills;
using RoR2;
using System;
using System.Collections.Generic;

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
        internal static float modePlusChaosSpend = 5f;
        internal static float regenPlusChaosFraction = 0.05f;
        internal static float backupGain = 10f;
        internal static float afterburnerGain = 30f;
        internal static float lysateGain = 15f;



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
        internal const int claydunestriderbuffDuration = 4;
        internal const float claydunestriderAttackSpeed = 1.5f;
        //internal const float clayduneArmor = 100f;

        internal const float stonetitanarmorGain = 10f;

        internal const float voidjailerInterval = 1f;
        internal const float voidjailerDamageCoeffecient = 1f;
        internal const float voidjailerminpullDistance = 10f;
        internal const float voidjailermaxpullDistance = 50f;
        internal const float voidjailerpullLiftVelocity = -10f;

        internal const float roboballattackspeedMultiplier = 0.02f;

        internal const float minimushrumInterval = 1f;
        internal const float minimushrumhealFraction = 0.05f;
        internal const float minimushrumRadius = 4f;

        internal const float lunarexploderprojectileCount = 1f;
        internal const float lunarexploderbaseDuration = 2f;
        internal const float lunarexploderRadius = 2.5f;
        internal const float lunarexploderDamageCoefficient = 1f;

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
        
        internal const float alloyvultureflyduration = 10f;

        internal const float beetledamageMultiplier = 1.5f;
        internal const float beetlestrengthMultiplier = 1.5f;

        internal const float lesserwispdamageMultiplier = 1.5f;
        internal const float lesserwisprangedMultiplier = 1.5f;

        internal const float decayspreadRadius = 10f;
        internal const float decayadditionalTimer = 6f;
        internal const float decayDamageCoeffecient = 1f;
        internal const float decayDamageStack = 0.5f;
        internal const float decayDamagePercentage = 0.005f;
        internal const float decayDamageTimer = 10f;
        internal const float decayInstaKillThreshold = 50f;

        internal const float decayattackDamageCoeffecient = 3f;
        internal const float decayattackProcCoefficient = 1f;

        internal const float aircannonDamageCoeffecient = 4f;
        internal const float aircannonProcCoefficient = 1f;

        internal const float bulletlaserDamageCoeffecient = 3f;
        internal const float bulletlaserProcCoefficient = 0.4f;

        internal const float beetleguardslamDamageCoeffecient = 4f;
        internal const float beetleguardslamProcCoefficient = 1f;

        internal const float bisonchargeDamageCoeffecient = 6f;
        internal const float bisonchargeProcCoefficient = 1f;

        internal const float bronzongballDamageCoeffecient = 4f;
        internal const float bronzongballProcCoefficient = 1f;

        internal const float clayapothecarymortarDamageCoeffecient = 2f;
        internal const float clayapothecarymortarProcCoefficient = 0.5f;

        internal const float claytemplarminigunDamageCoeffecient = 0.3f;
        internal const float claytemplarminigunProcCoefficient = 0.05f;

        internal const float greaterwispballDamageCoeffecient = 0.5f;
        internal const int greaterwispballbuffDuration = 4;
        internal const float greaterwispballProcCoefficient = 0f;

        internal const float jellyfishnovaDamageCoeffecient = 20f;
        internal const float jellyfishnovaProcCoefficient = 2f;

        internal const float lemurianfireballDamageCoeffecient = 2f;
        internal const float lemurianfireballProcCoefficient = 1f;

        internal const float elderlemurianfireblastProcCoefficient = 0.5f;
        internal const float elderlemurianfireblastDamageCoefficient = 2f;

        internal const float lunarwispminigunDamageCoeffecient = 3f;
        internal const float lunarwispminigunProcCoefficient = 0.1f;

        internal const float parentDamageCoeffecient = 6f;
        internal const float parentProcCoefficient = 1f;

        internal const float stonegolemDamageCoeffecient = 4f;
        internal const float stonegolemProcCoefficient = 2f;

        internal const float voidreaverDamageCoeffecient = 2f;
        internal const float voidreaverProcCoefficient = 0.2f;

        internal const float beetlequeenDamageCoeffecient = 4f;
        internal const float beetlequeenProcCoefficient = 0.5f;

        internal const float grovetenderDamageCoeffecient = 3f;
        internal const float grovetenderProcCoefficient = 0.5f;

        internal const float claydunestriderDamageCoeffecient = 8f;
        internal const float claydunestriderProcCoefficient = 1f;

        internal const float soluscontrolunitDamageCoeffecient = 4f;
        internal const float soluscontrolunitProcCoefficient = 1f;

        internal const float xiconstructDamageCoeffecient = 2f;
        internal const float xiconstructProcCoefficient = 0.2f;

        internal const int voiddevastatorTotalMissiles = 8;
        internal const float voiddevastatorDamageCoeffecient = 1f;
        internal const float voiddevastatorProcCoefficient = 0.5f;

        internal const int scavengerProjectileCount = 3;
        internal const float scavengerDamageCoeffecient = 4f;
        internal const float scavengerProcCoefficient = 0.5f;

        internal const float artificerflamethrowerDamageCoefficient = 15f;
        internal const float artificerflamethrowerProcCoefficient = 1f;

        internal const float artificericewallDamageCoefficient = 1f;

        internal const float artificerlightningorbMinDamageCoefficient = 4f;
        internal const float artificerlightningorbMaxDamageCoefficient = 12f;

        internal const float banditDamageCoefficient = 3f;
        internal const float banditcloakDuration = 3f;

        internal const float huntressDamageCoefficient = 1f;
        internal const float huntressProcCoefficient = 0.7f;
        internal const int huntressmaxArrowCount = 6;

        internal const float mercDamageCoefficient = 1f;
        internal const float mercProcCoefficient = 1f;

        internal const float rexDamageCoefficient = 4f;
        internal const float rexProcCoefficient = 1f;
        internal const float rexHealthCost = 0.1f;

        internal const float railgunnerDamageCoefficient = 15f;
        internal const float railgunnerProcCoefficient = 2f;
        public enum IndicatorType : uint
        {
            PASSIVE = 1,
            ACTIVE = 2,
        }

        public static Dictionary<string, IndicatorType> indicatorDict;
        public static Dictionary<string, RoR2.Skills.SkillDef> baseQuirkSkillDef;
        public static Dictionary<RoR2.Skills.SkillDef, string> baseQuirkSkillString;
        public static Dictionary<RoR2.Skills.SkillDef, RoR2.BuffIndex> baseQuirkSkilltoBuff;
        public static Dictionary<RoR2.Skills.SkillDef, List<RoR2.Skills.SkillDef>> baseQuirkSkillUpgradeCheck;
        public static void LoadDictionary()
        {
            baseQuirkSkillUpgradeCheck = new Dictionary<RoR2.Skills.SkillDef, List<RoR2.Skills.SkillDef>>();
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.alphacontructpassiveDef, Survivors.Shiggy.xiconstructbeamDef.skillNameToken);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.beetlepassiveDef, Survivors.Shiggy.beetlequeenshotgunDef);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.pestpassiveDef, Survivors.Shiggy.aircannonDef);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.verminpassiveDef, Survivors.Shiggy.aircannonDef);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.guppassiveDef, Survivors.Shiggy.bronzongballDef);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.hermitcrabpassiveDef, Survivors.Shiggy.voiddevastatorhomingDef);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.larvapassiveDef, Survivors.Shiggy.lunargolemslideDef);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.lesserwisppassiveDef, Survivors.Shiggy.beetlequeenshotgunDef);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.lunarexploderpassiveDef, Survivors.Shiggy.minimushrumpassiveDef);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.minimushrumpassiveDef, Survivors.Shiggy.claydunestriderbuffDef);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.roboballminibpassiveDef, Survivors.Shiggy.alloyvultureflyDef);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.voidbarnaclepassiveDef, Survivors.Shiggy.voidjailerpassiveDef);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.voidjailerpassiveDef, Survivors.Shiggy.voidreaverportalDef);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.impbosspassiveDef, Survivors.Shiggy.impbosspassiveDef);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.stonetitanpassiveDef, Survivors.Shiggy.lunarexploderpassiveDef);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.vagrantpassiveDef, Survivors.Shiggy.greaterwispballDef);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.magmawormpassiveDef, Survivors.Shiggy.overloadingwormpassiveDef);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.overloadingwormpassiveDef, Survivors.Shiggy.captainpassiveDef);
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.scavengerthqwibDef, "<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.captainpassiveDef, "<style=cIsUtility>Defensive Microbots Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.commandopassiveDef, "<style=cIsUtility>Double Tap Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.acridpassiveDef, "<style=cIsUtility>Poison Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.loaderpassiveDef, "<style=cIsUtility>Scrap Barrier Quirk</style> Get!");

            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.alloyvultureflyDef, "<style=cIsUtility>Flight Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.beetleguardslamDef, "<style=cIsUtility>Beetle Armor Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.bisonchargeDef, "<style=cIsUtility>Charging Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.bronzongballDef, "<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.clayapothecarymortarDef, "<style=cIsUtility>Clay AirStrike Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.claytemplarminigunDef, "<style=cIsUtility>Clay Minigun Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.elderlemurianfireblastDef, "<style=cIsUtility>Fire Blast Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.greaterwispballDef, "<style=cIsUtility>Spirit Boost Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.impblinkDef, "<style=cIsUtility>Blink Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.jellyfishnovaDef, "<style=cIsUtility>Nova Explosion Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.lemurianfireballDef, "<style=cIsUtility>Fireball Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.lunargolemslideDef, "<style=cIsUtility>Slide Reset Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.lunarwispminigunDef, "<style=cIsUtility>Lunar Minigun Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.parentteleportDef, "<style=cIsUtility>Teleport Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.stonegolemlaserDef, "<style=cIsUtility>Laser Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.voidreaverportalDef, "<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.beetlequeenshotgunDef, "<style=cIsUtility>Acid Shotgun Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.claydunestriderbuffDef, "<style=cIsUtility>Tar Boost Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.grandparentsunDef, "<style=cIsUtility>Solar Flare Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.soluscontrolunityknockupDef, "<style=cIsUtility>Anti Gravity Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.xiconstructbeamDef, "<style=cIsUtility>Beam Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.voiddevastatorhomingDef, "<style=cIsUtility>Void Missiles Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.banditlightsoutDef, "<style=cIsUtility>Lights Out Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.engiturretDef, "<style=cIsUtility>Turret Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.huntressattackDef, "<style=cIsUtility>Flurry Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.artificerflamethrowerDef, "<style=cIsUtility>Elementality Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.mercdashDef, "<style=cIsUtility>Eviscerate Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.multbuffDef, "<style=cIsUtility>Power Stance Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.rexmortarDef, "<style=cIsUtility>Seed Barrage Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.railgunnercryoDef, "<style=cIsUtility>Cryocharged Railgun Quirk</style> Get!");
            baseQuirkSkillUpgradeCheck.Add(Survivors.Shiggy.voidfiendcleanseDef, "<style=cIsUtility>Cleanse Quirk</style> Get!");

            baseQuirkSkilltoBuff = new Dictionary<RoR2.Skills.SkillDef, RoR2.BuffIndex>();
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.alphacontructpassiveDef, Buffs.alphashieldonBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.beetlepassiveDef, Buffs.beetleBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.pestpassiveDef, Buffs.pestjumpBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.verminpassiveDef, Buffs.verminsprintBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.guppassiveDef, Buffs.gupspikeBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.hermitcrabpassiveDef, Buffs.hermitcrabmortarBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.larvapassiveDef, Buffs.larvajumpBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.lesserwisppassiveDef, Buffs.lesserwispBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.lunarexploderpassiveDef, Buffs.lunarexploderBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.minimushrumpassiveDef, Buffs.minimushrumBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.roboballminibpassiveDef, Buffs.roboballminiBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.voidbarnaclepassiveDef, Buffs.voidbarnaclemortarBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.voidjailerpassiveDef, Buffs.voidjailerBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.impbosspassiveDef, Buffs.impbossBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.stonetitanpassiveDef, Buffs.stonetitanBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.vagrantpassiveDef, Buffs.vagrantBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.magmawormpassiveDef, Buffs.magmawormBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.overloadingwormpassiveDef, Buffs.overloadingwormBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.scavengerthqwibDef, Buffs.overloadingwormBuff.buffIndex); //need to update scavenger to passive
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.captainpassiveDef, Buffs.captainBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.commandopassiveDef, Buffs.commandoBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.acridpassiveDef, Buffs.acridBuff.buffIndex);
            baseQuirkSkilltoBuff.Add(Survivors.Shiggy.loaderpassiveDef, Buffs.loaderBuff.buffIndex);


            baseQuirkSkillString = new Dictionary<RoR2.Skills.SkillDef, string>();
            baseQuirkSkillString.Add(Survivors.Shiggy.alphacontructpassiveDef, "<style=cIsUtility>Barrier Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.beetlepassiveDef, "<style=cIsUtility>Strength Boost Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.pestpassiveDef, "<style=cIsUtility>Jump Boost Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.verminpassiveDef, "<style=cIsUtility>Super Speed Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.guppassiveDef, "<style=cIsUtility>Spiky Body Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.hermitcrabpassiveDef, "<style=cIsUtility>Mortar Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.larvapassiveDef, "<style=cIsUtility>Acid Jump Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.lesserwisppassiveDef, "<style=cIsUtility>Ranged Boost Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.lunarexploderpassiveDef, "<style=cIsUtility>Lunar Aura Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.minimushrumpassiveDef, "<style=cIsUtility>Healing Aura Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.roboballminibpassiveDef, "<style=cIsUtility>Solus Boost Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.voidbarnaclepassiveDef, "<style=cIsUtility>Void Mortar Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.voidjailerpassiveDef, "<style=cIsUtility>Gravity Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.impbosspassiveDef, "<style=cIsUtility>Bleed Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.stonetitanpassiveDef, "<style=cIsUtility>Stone Skin Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.vagrantpassiveDef, "<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.magmawormpassiveDef, "<style=cIsUtility>Blazing Aura Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.overloadingwormpassiveDef, "<style=cIsUtility>Lightning Aura Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.scavengerthqwibDef, "<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.captainpassiveDef, "<style=cIsUtility>Defensive Microbots Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.commandopassiveDef, "<style=cIsUtility>Double Tap Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.acridpassiveDef, "<style=cIsUtility>Poison Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.loaderpassiveDef, "<style=cIsUtility>Scrap Barrier Quirk</style> Get!");

            baseQuirkSkillString.Add(Survivors.Shiggy.alloyvultureflyDef, "<style=cIsUtility>Flight Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.beetleguardslamDef, "<style=cIsUtility>Beetle Armor Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.bisonchargeDef, "<style=cIsUtility>Charging Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.bronzongballDef, "<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.clayapothecarymortarDef, "<style=cIsUtility>Clay AirStrike Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.claytemplarminigunDef, "<style=cIsUtility>Clay Minigun Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.elderlemurianfireblastDef, "<style=cIsUtility>Fire Blast Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.greaterwispballDef, "<style=cIsUtility>Spirit Boost Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.impblinkDef , "<style=cIsUtility>Blink Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.jellyfishnovaDef, "<style=cIsUtility>Nova Explosion Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.lemurianfireballDef, "<style=cIsUtility>Fireball Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.lunargolemslideDef, "<style=cIsUtility>Slide Reset Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.lunarwispminigunDef, "<style=cIsUtility>Lunar Minigun Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.parentteleportDef, "<style=cIsUtility>Teleport Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.stonegolemlaserDef, "<style=cIsUtility>Laser Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.voidreaverportalDef, "<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.beetlequeenshotgunDef, "<style=cIsUtility>Acid Shotgun Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.claydunestriderbuffDef, "<style=cIsUtility>Tar Boost Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.grandparentsunDef, "<style=cIsUtility>Solar Flare Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.soluscontrolunityknockupDef, "<style=cIsUtility>Anti Gravity Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.xiconstructbeamDef, "<style=cIsUtility>Beam Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.voiddevastatorhomingDef, "<style=cIsUtility>Void Missiles Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.banditlightsoutDef, "<style=cIsUtility>Lights Out Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.engiturretDef, "<style=cIsUtility>Turret Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.huntressattackDef, "<style=cIsUtility>Flurry Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.artificerflamethrowerDef, "<style=cIsUtility>Elementality Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.mercdashDef, "<style=cIsUtility>Eviscerate Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.multbuffDef, "<style=cIsUtility>Power Stance Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.rexmortarDef, "<style=cIsUtility>Seed Barrage Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.railgunnercryoDef, "<style=cIsUtility>Cryocharged Railgun Quirk</style> Get!");
            baseQuirkSkillString.Add(Survivors.Shiggy.voidfiendcleanseDef, "<style=cIsUtility>Cleanse Quirk</style> Get!");


            baseQuirkSkillDef = new Dictionary<string, RoR2.Skills.SkillDef>();
            baseQuirkSkillDef.Add("MinorConstructBody", Survivors.Shiggy.alphacontructpassiveDef);
            baseQuirkSkillDef.Add("MinorConstructOnKillBody", Survivors.Shiggy.alphacontructpassiveDef);
            baseQuirkSkillDef.Add("BeetleBody", Survivors.Shiggy.beetlepassiveDef);
            baseQuirkSkillDef.Add("FlyingVerminBody", Survivors.Shiggy.pestpassiveDef);
            baseQuirkSkillDef.Add("VerminBody", Survivors.Shiggy.verminpassiveDef);
            baseQuirkSkillDef.Add("GupBody", Survivors.Shiggy.guppassiveDef);
            baseQuirkSkillDef.Add("GipBody", Survivors.Shiggy.guppassiveDef);
            baseQuirkSkillDef.Add("GeepBody", Survivors.Shiggy.guppassiveDef);
            baseQuirkSkillDef.Add("HermitCrabBody", Survivors.Shiggy.hermitcrabpassiveDef);
            baseQuirkSkillDef.Add("AcidLarvaBody", Survivors.Shiggy.larvapassiveDef);
            baseQuirkSkillDef.Add("WispBody", Survivors.Shiggy.lesserwisppassiveDef);
            baseQuirkSkillDef.Add("LunarExploderBody", Survivors.Shiggy.lunarexploderpassiveDef);
            baseQuirkSkillDef.Add("MiniMushroomBody", Survivors.Shiggy.minimushrumpassiveDef);
            baseQuirkSkillDef.Add("RoboBallMiniBody", Survivors.Shiggy.roboballminibpassiveDef);
            baseQuirkSkillDef.Add("RoboBallGreenBuddyBody", Survivors.Shiggy.roboballminibpassiveDef);
            baseQuirkSkillDef.Add("RoboBallRedBuddyBody", Survivors.Shiggy.roboballminibpassiveDef);
            baseQuirkSkillDef.Add("VoidBarnacleBody", Survivors.Shiggy.voidbarnaclepassiveDef);
            baseQuirkSkillDef.Add("VoidJailerBody", Survivors.Shiggy.voidjailerpassiveDef);
            baseQuirkSkillDef.Add("ImpBossBody", Survivors.Shiggy.impbosspassiveDef);
            baseQuirkSkillDef.Add("TitanBody", Survivors.Shiggy.stonetitanpassiveDef);
            baseQuirkSkillDef.Add("TitanGoldBody", Survivors.Shiggy.stonetitanpassiveDef);
            baseQuirkSkillDef.Add("VagrantBody", Survivors.Shiggy.vagrantpassiveDef);
            baseQuirkSkillDef.Add("MagmaWormBody", Survivors.Shiggy.magmawormpassiveDef);
            baseQuirkSkillDef.Add("ElectricWormBody", Survivors.Shiggy.overloadingwormpassiveDef);
            baseQuirkSkillDef.Add("ScavBody", Survivors.Shiggy.scavengerthqwibDef);
            baseQuirkSkillDef.Add("CaptainBody", Survivors.Shiggy.captainpassiveDef);
            baseQuirkSkillDef.Add("CommandoBody", Survivors.Shiggy.commandopassiveDef);
            baseQuirkSkillDef.Add("CrocoBody", Survivors.Shiggy.acridpassiveDef);
            baseQuirkSkillDef.Add("LoaderBody", Survivors.Shiggy.loaderpassiveDef);

            baseQuirkSkillDef.Add("VultureBody", Survivors.Shiggy.alloyvultureflyDef);
            baseQuirkSkillDef.Add("BeetleGuardBody", Survivors.Shiggy.beetleguardslamDef);
            baseQuirkSkillDef.Add("BisonBody", Survivors.Shiggy.bisonchargeDef);
            baseQuirkSkillDef.Add("BellBody", Survivors.Shiggy.bronzongballDef);
            baseQuirkSkillDef.Add("ClayGrenadierBody", Survivors.Shiggy.clayapothecarymortarDef);
            baseQuirkSkillDef.Add("ClayBruiserBody", Survivors.Shiggy.claytemplarminigunDef);
            baseQuirkSkillDef.Add("LemurianBruiserBody", Survivors.Shiggy.elderlemurianfireblastDef);
            baseQuirkSkillDef.Add("GreaterWispBody", Survivors.Shiggy.greaterwispballDef);
            baseQuirkSkillDef.Add("ImpBody", Survivors.Shiggy.impblinkDef);
            baseQuirkSkillDef.Add("JellyfishBody", Survivors.Shiggy.jellyfishnovaDef);
            baseQuirkSkillDef.Add("LemurianBody", Survivors.Shiggy.lemurianfireballDef);
            baseQuirkSkillDef.Add("LunarGolemBody", Survivors.Shiggy.lunargolemslideDef);
            baseQuirkSkillDef.Add("LunarWispBody", Survivors.Shiggy.lunarwispminigunDef);
            baseQuirkSkillDef.Add("ParentBody", Survivors.Shiggy.parentteleportDef);
            baseQuirkSkillDef.Add("GolemBody", Survivors.Shiggy.stonegolemlaserDef);
            baseQuirkSkillDef.Add("NullifierBody", Survivors.Shiggy.voidreaverportalDef);
            baseQuirkSkillDef.Add("BeetleQueen2Body", Survivors.Shiggy.beetlequeenshotgunDef);
            baseQuirkSkillDef.Add("GravekeeperBody", Survivors.Shiggy.grovetenderhookDef);
            baseQuirkSkillDef.Add("ClayBossBody", Survivors.Shiggy.claydunestriderbuffDef);
            baseQuirkSkillDef.Add("GrandParentBody", Survivors.Shiggy.grandparentsunDef);
            baseQuirkSkillDef.Add("RoboBallBossBody", Survivors.Shiggy.soluscontrolunityknockupDef);
            baseQuirkSkillDef.Add("SuperRoboBallBossBody", Survivors.Shiggy.soluscontrolunityknockupDef);
            baseQuirkSkillDef.Add("MegaConstructBody", Survivors.Shiggy.xiconstructbeamDef);
            baseQuirkSkillDef.Add("VoidMegaCrabBody", Survivors.Shiggy.voiddevastatorhomingDef);
            baseQuirkSkillDef.Add("Bandit2Body", Survivors.Shiggy.banditlightsoutDef);
            baseQuirkSkillDef.Add("EngiBody", Survivors.Shiggy.engiturretDef);
            baseQuirkSkillDef.Add("HuntressBody", Survivors.Shiggy.huntressattackDef);
            baseQuirkSkillDef.Add("MageBody", Survivors.Shiggy.artificerflamethrowerDef);
            baseQuirkSkillDef.Add("MercBody", Survivors.Shiggy.mercdashDef);
            baseQuirkSkillDef.Add("ToolbotBody", Survivors.Shiggy.multbuffDef);
            baseQuirkSkillDef.Add("TreebotBody", Survivors.Shiggy.rexmortarDef);
            baseQuirkSkillDef.Add("RailgunnerBody", Survivors.Shiggy.railgunnercryoDef);
            baseQuirkSkillDef.Add("VoidSurvivorBody", Survivors.Shiggy.voidfiendcleanseDef);



            indicatorDict = new Dictionary<string, IndicatorType>();
            indicatorDict.Add("MinorConstructBody", IndicatorType.PASSIVE);
            indicatorDict.Add("MinorConstructOnKillBody", IndicatorType.PASSIVE);
            indicatorDict.Add("BeetleBody", IndicatorType.PASSIVE);
            indicatorDict.Add("FlyingVerminBody", IndicatorType.PASSIVE);
            indicatorDict.Add("VerminBody", IndicatorType.PASSIVE);
            indicatorDict.Add("GupBody", IndicatorType.PASSIVE);
            indicatorDict.Add("GipBody", IndicatorType.PASSIVE);
            indicatorDict.Add("GeepBody", IndicatorType.PASSIVE);
            indicatorDict.Add("HermitCrabBody", IndicatorType.PASSIVE);
            indicatorDict.Add("AcidLarvaBody", IndicatorType.PASSIVE);
            indicatorDict.Add("WispBody", IndicatorType.PASSIVE);
            indicatorDict.Add("LunarExploderBody", IndicatorType.PASSIVE);
            indicatorDict.Add("MiniMushroomBody", IndicatorType.PASSIVE);
            indicatorDict.Add("RoboBallMiniBody", IndicatorType.PASSIVE);
            indicatorDict.Add("RoboBallGreenBuddyBody", IndicatorType.PASSIVE);
            indicatorDict.Add("RoboBallRedBuddyBody", IndicatorType.PASSIVE);
            indicatorDict.Add("VoidBarnacleBody", IndicatorType.PASSIVE);
            indicatorDict.Add("VoidJailerBody", IndicatorType.PASSIVE);
            indicatorDict.Add("ImpBossBody", IndicatorType.PASSIVE);
            indicatorDict.Add("TitanBody", IndicatorType.PASSIVE);
            indicatorDict.Add("TitanGoldBody", IndicatorType.PASSIVE);
            indicatorDict.Add("VagrantBody", IndicatorType.PASSIVE);
            indicatorDict.Add("MagmaWormBody", IndicatorType.PASSIVE);
            indicatorDict.Add("ElectricWormBody", IndicatorType.PASSIVE);
            indicatorDict.Add("ScavBody", IndicatorType.PASSIVE);
            indicatorDict.Add("CaptainBody", IndicatorType.PASSIVE);
            indicatorDict.Add("CommandoBody", IndicatorType.PASSIVE);
            indicatorDict.Add("CrocoBody", IndicatorType.PASSIVE);
            indicatorDict.Add("LoaderBody", IndicatorType.PASSIVE);

            indicatorDict.Add("VultureBody", IndicatorType.ACTIVE);
            indicatorDict.Add("BeetleGuardBody", IndicatorType.ACTIVE);
            indicatorDict.Add("BisonBody", IndicatorType.ACTIVE);
            indicatorDict.Add("BellBody", IndicatorType.ACTIVE);
            indicatorDict.Add("ClayGrenadierBody", IndicatorType.ACTIVE);
            indicatorDict.Add("ClayBruiserBody", IndicatorType.ACTIVE);
            indicatorDict.Add("LemurianBruiserBody", IndicatorType.ACTIVE);
            indicatorDict.Add("GreaterWispBody", IndicatorType.ACTIVE);
            indicatorDict.Add("ImpBody", IndicatorType.ACTIVE);
            indicatorDict.Add("JellyfishBody", IndicatorType.ACTIVE);
            indicatorDict.Add("LemurianBody", IndicatorType.ACTIVE);
            indicatorDict.Add("LunarGolemBody", IndicatorType.ACTIVE);
            indicatorDict.Add("LunarWispBody", IndicatorType.ACTIVE);
            indicatorDict.Add("ParentBody", IndicatorType.ACTIVE);
            indicatorDict.Add("GolemBody", IndicatorType.ACTIVE);
            indicatorDict.Add("NullifierBody", IndicatorType.ACTIVE);
            indicatorDict.Add("BeetleQueen2Body", IndicatorType.ACTIVE);
            indicatorDict.Add("GravekeeperBody", IndicatorType.ACTIVE);
            indicatorDict.Add("ClayBossBody", IndicatorType.ACTIVE);
            indicatorDict.Add("GrandParentBody", IndicatorType.ACTIVE);
            indicatorDict.Add("RoboBallBossBody", IndicatorType.ACTIVE);
            indicatorDict.Add("SuperRoboBallBossBody", IndicatorType.ACTIVE);
            indicatorDict.Add("MegaConstructBody", IndicatorType.ACTIVE);
            indicatorDict.Add("VoidMegaCrabBody", IndicatorType.ACTIVE);
            indicatorDict.Add("Bandit2Body", IndicatorType.ACTIVE);
            indicatorDict.Add("EngiBody", IndicatorType.ACTIVE);
            indicatorDict.Add("HuntressBody", IndicatorType.ACTIVE);
            indicatorDict.Add("MageBody", IndicatorType.ACTIVE);
            indicatorDict.Add("MercBody", IndicatorType.ACTIVE);
            indicatorDict.Add("ToolbotBody", IndicatorType.ACTIVE);
            indicatorDict.Add("TreebotBody", IndicatorType.ACTIVE);
            indicatorDict.Add("RailgunnerBody", IndicatorType.ACTIVE);
            indicatorDict.Add("VoidSurvivorBody", IndicatorType.ACTIVE);
        }
    }
}
