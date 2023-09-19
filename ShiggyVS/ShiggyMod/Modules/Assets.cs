using System.Reflection;
using R2API;
using UnityEngine;
using UnityEngine.Networking;
using RoR2;
using System.IO;
using System.Collections.Generic;
using RoR2.UI;
using UnityEngine.AddressableAssets;
using System.Runtime.CompilerServices;
using System;

namespace ShiggyMod.Modules
{
    internal static class Assets
    {
        // the assetbundle to load assets from
        internal static AssetBundle mainAssetBundle;

        // particle effects
        internal static GameObject beam;
        internal static List<GameObject> networkObjDefs = new List<GameObject>();

        // networked hit sounds
        internal static NetworkSoundEventDef hitSoundEffect;
        internal static NetworkSoundEventDef strongHitSoundEffect;

        // lists of assets to add to contentpack
        internal static List<NetworkSoundEventDef> networkSoundEventDefs = new List<NetworkSoundEventDef>();
        internal static List<EffectDef> effectDefs = new List<EffectDef>();

        // cache these and use to create our own materials
        internal static Shader hotpoo = RoR2.LegacyResourcesAPI.Load<Shader>("Shaders/Deferred/HGStandard");
        internal static Material commandoMat;
        private static string[] assetNames = new string[0];

        // CHANGE THIS
        private const string assetbundleName = "shiggyassetbundle";

        //tracers
        public static GameObject VoidFiendBeamTracer = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidSurvivor/VoidSurvivorBeamTracer.prefab").WaitForCompletion();

        //buffs
        public static Sprite lunarRootIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/LunarSkillReplacements/bdLunarSecondaryRoot.asset").WaitForCompletion().iconSprite;
        public static Sprite strongerBurnIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/DLC1/StrengthenBurn/bdStrongerBurn.asset").WaitForCompletion().iconSprite;
        public static Sprite mercExposeIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Merc/bdMercExpose.asset").WaitForCompletion().iconSprite;
        public static Sprite deathMarkDebuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/DeathMark/bdDeathMark.asset").WaitForCompletion().iconSprite;
        public static Sprite singularityBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/DLC1/ElementalRingVoid/bdElementalRingVoidReady.asset").WaitForCompletion().iconSprite;
        public static Sprite cloakBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Common/bdCloak.asset").WaitForCompletion().iconSprite;
        public static Sprite voidFogDebuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Common/bdVoidFogMild.asset").WaitForCompletion().iconSprite;
        public static Sprite medkitBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Medkit/bdMedkitHeal.asset").WaitForCompletion().iconSprite;
        public static Sprite spikyDebuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Treebot/bdEntangle.asset").WaitForCompletion().iconSprite;
        public static Sprite ruinDebuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/LunarSkillReplacements/bdLunarDetonationCharge.asset").WaitForCompletion().iconSprite;
        public static Sprite warcryBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/WarCryOnMultiKill/bdWarCryBuff.asset").WaitForCompletion().iconSprite;
        public static Sprite shieldBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Common/bdArmorBoost.asset").WaitForCompletion().iconSprite;
        public static Sprite tarBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Common/bdClayGoo.asset").WaitForCompletion().iconSprite;
        public static Sprite crippleBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Common/bdCripple.asset").WaitForCompletion().iconSprite;
        public static Sprite speedBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Bandit2/bdCloakSpeed.asset").WaitForCompletion().iconSprite;
        public static Sprite boostBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/RandomDamageZone/bdPowerBuff.asset").WaitForCompletion().iconSprite;
        public static Sprite alphashieldonBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/DLC1/BearVoid/bdBearVoidReady.asset").WaitForCompletion().iconSprite;
        public static Sprite alphashieldoffBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/DLC1/BearVoid/bdBearVoidCooldown.asset").WaitForCompletion().iconSprite;
        public static Sprite decayBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Common/bdVoidFogStrong.asset").WaitForCompletion().iconSprite;
        public static Sprite multiplierBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/DLC1/PrimarySkillShuriken/bdPrimarySkillShurikenBuff.asset").WaitForCompletion().iconSprite;
        public static Sprite jumpBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/DLC1/MoveSpeedOnKill/bdKillMoveSpeed.asset").WaitForCompletion().iconSprite;
        public static Sprite sprintBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/SprintOutOfCombat/bdWhipBoost.asset").WaitForCompletion().iconSprite;
        public static Sprite spikeBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Grandparent/bdOverheat.asset").WaitForCompletion().iconSprite;
        public static Sprite mortarBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/GainArmor/bdElephantArmorBoost.asset").WaitForCompletion().iconSprite;
        public static Sprite healBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Croco/bdCrocoRegen.asset").WaitForCompletion().iconSprite;
        public static Sprite attackspeedBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/EnergizedOnEquipmentUse/bdEnergized.asset").WaitForCompletion().iconSprite;
        public static Sprite gravityBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/KillEliteFrenzy/bdNoCooldowns.asset").WaitForCompletion().iconSprite;
        public static Sprite bleedBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Common/bdBleeding.asset").WaitForCompletion().iconSprite;
        public static Sprite skinBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/DLC1/OutOfCombatArmor/bdOutOfCombatArmorBuff.asset").WaitForCompletion().iconSprite;
        public static Sprite orbreadyBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/ElementalRings/bdElementalRingsReady.asset").WaitForCompletion().iconSprite;
        public static Sprite orbdisableBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/ElementalRings/bdElementalRingsCooldown.asset").WaitForCompletion().iconSprite;
        public static Sprite blazingBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Common/bdOnFire.asset").WaitForCompletion().iconSprite;
        public static Sprite lightningBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/ShockNearby/bdTeslaField.asset").WaitForCompletion().iconSprite;
        public static Sprite resonanceBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/LaserTurbine/bdLaserTurbineKillCharge.asset").WaitForCompletion().iconSprite;
        public static Sprite critBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/CritOnUse/bdFullCrit.asset").WaitForCompletion().iconSprite;
        public static Sprite claygooBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Common/bdClayGoo.asset").WaitForCompletion().iconSprite;
        public static Sprite predatorBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/AttackSpeedOnCrit/bdAttackSpeedOnCrit.asset").WaitForCompletion().iconSprite;

        //game material
        //public static Material alphaconstructShieldBuffMat = RoR2.LegacyResourcesAPI.Load<Material>("Materials/matEnergyShield");
        public static Material alphaconstructShieldBuffMat;
        public static Material blastingZoneBurnMat;

        //own material
        public static Material multiplierShieldBuffMat;
        public static Material lightAndDarknessMat;
        public static Material lightFormBuffMat;
        public static Material deathAuraBuffMat;
        public static Material limitBreakBuffMat; 
        public static Material voidFormBuffMat;

        //own effects
        public static GameObject decayattackEffect;
        public static GameObject decaybuffEffect;
        public static GameObject decayspreadEffect;
        internal static GameObject AFOLineRenderer;
        public static GameObject blastingZoneEffect;
        public static GameObject finalReleasePulseEffect;
        //melee swing
        internal static GameObject shiggySwingEffect;
        internal static GameObject shiggyHitImpactEffect;


        //game effects
        public static GameObject voidMegaCrabExplosionEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidMegaCrab/VoidMegacrabAntimatterExplosion.prefab").WaitForCompletion();
        public static GameObject huntressGlaiveChargeEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Huntress/ChargeGlaive.prefab").WaitForCompletion();
        public static GameObject huntressGlaiveMuzzleEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Huntress/HuntressGlaiveSwing.prefab").WaitForCompletion();
        public static GameObject bisonEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Bison/BisonChargeStep.prefab").WaitForCompletion();
        public static GameObject GupSpikeEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/Gup/GupExplosion.prefab").WaitForCompletion();
        public static GameObject chargegreaterwispBall = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/GreaterWisp/OmniExplosionVFXGreaterWisp.prefab").WaitForCompletion();
        public static GameObject larvajumpEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/AcidLarva/AcidLarvaLeapExplosion.prefab").WaitForCompletion();
        public static GameObject elderlemurianexplosionEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/LemurianBruiser/OmniExplosionVFXLemurianBruiserFireballImpact.prefab").WaitForCompletion();
        public static GameObject parentslamEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Parent/ParentSlamEffect.prefab").WaitForCompletion();
        public static GameObject claydunestriderEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ClayBruiser/ClayShockwaveEffect.prefab").WaitForCompletion();
        public static GameObject voidjailerEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidJailer/VoidJailerDeathBombExplosion.prefab").WaitForCompletion();
        public static GameObject voidjailermuzzleEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidSurvivor/VoidSurvivorBeamTracer.prefab").WaitForCompletion();
        public static GameObject xiconstructbeamEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/MajorAndMinorConstruct/MajorConstructInitialMuzzleFlash.prefab").WaitForCompletion();
        public static GameObject xiconstructSecondMuzzleEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/MajorAndMinorConstruct/MajorConstructSecondMuzzleFlash.prefab").WaitForCompletion();
        public static GameObject xiconstructDeathEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/MajorAndMinorConstruct/MajorConstructDeathEffect.prefab").WaitForCompletion();
        public static GameObject xiconstructBeamLaser = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/MajorAndMinorConstruct/LaserMajorConstruct.prefab").WaitForCompletion();
        public static GameObject lunarGolemSmokeEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/LunarGolem/BlastSmokeLunarGolem.prefab").WaitForCompletion();
        public static GameObject impBossExplosionEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ImpBoss/ImpBossBlink.prefab").WaitForCompletion();
        public static GameObject impBossGroundSlamEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ImpBoss/ImpBossGroundSlam.prefab").WaitForCompletion();

        public static RoR2.Audio.LoopSoundDef xiconstructsound = Addressables.LoadAssetAsync<RoR2.Audio.LoopSoundDef>("RoR2/DLC1/MajorAndMinorConstruct/lsdMajorConstructLaser.asset").WaitForCompletion();
        public static GameObject grandparentSunPrefab;
        public static GameObject grandparentSunSpawnPrefab;

        public static GameObject artificerfireEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mage/MageFlamethrowerEffect.prefab").WaitForCompletion();
        public static GameObject artificerlightningorbchargeEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mage/MuzzleflashMageLightning.prefab").WaitForCompletion();
        public static GameObject artificerlightningorbMuzzleEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mage/MuzzleflashMageLightning.prefab").WaitForCompletion();
        public static GameObject artificerlightningorbprojectileEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mage/MageLightningBombProjectile.prefab").WaitForCompletion();
        public static GameObject artificerCrosshair = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mage/MageCrosshair.prefab").WaitForCompletion();
        public static RoR2.Audio.LoopSoundDef artificerlightningsound = Addressables.LoadAssetAsync<RoR2.Audio.LoopSoundDef>("RoR2/Base/Vagrant/lsdVagrantTrackingBombFlight.asset").WaitForCompletion();
        public static GameObject banditmuzzleEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Bandit2/MuzzleflashBandit2.prefab").WaitForCompletion();
        public static GameObject bandittracerEffectPrefab = Resources.Load<GameObject>("prefabs/effects/tracers/tracerbandit2rifle");
        public static GameObject banditimpactEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Bandit2/HitsparkBandit2Pistol.prefab").WaitForCompletion();
        public static GameObject banditCrosshair = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Bandit2/Bandit2CrosshairPrepRevolver.prefab").WaitForCompletion();
        public static GameObject engiTurret = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Engi/EngiTurretMaster.prefab").WaitForCompletion();
        public static GameObject voidfiendblinkVFX = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidSurvivor/VoidBlinkVfx.prefab").WaitForCompletion();
        public static GameObject voidfiendblinkmuzzleEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidSurvivor/VoidBlinkMuzzleflash.prefab").WaitForCompletion();
        public static GameObject voidfiendblinktrailEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidSurvivor/PodVoidGroundImpact.prefab").WaitForCompletion();
        public static Material voidfiendblinkMaterial = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/VoidSurvivor/matVoidBlinkBodyOverlay.mat").WaitForCompletion();
        public static GameObject railgunnercryoCrosshair = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/Railgunner/RailgunnerCryochargeCrosshair.prefab").WaitForCompletion();
        public static GameObject railgunnercryochargeCrosshair = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/Railgunner/RailgunnerCryochargeUI.prefab").WaitForCompletion();
        public static GameObject railgunnercryoreloadCrosshair = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/Railgunner/RailgunnerReloadUI.prefab").WaitForCompletion();
        public static GameObject railgunnercryoTracer = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/Railgunner/TracerRailgunCryo.prefab").WaitForCompletion();
        public static RoR2.Audio.LoopSoundDef railgunnercryochargingSound = Addressables.LoadAssetAsync<RoR2.Audio.LoopSoundDef>("RoR2/DLC1/Railgunner/lsdRailgunnerBackpackCharging.asset").WaitForCompletion();
        public static RoR2.Audio.LoopSoundDef railgunnercryoofflineSound = Addressables.LoadAssetAsync<RoR2.Audio.LoopSoundDef>("RoR2/DLC1/Railgunner/lsdRailgunnerBackpackOffline.asset").WaitForCompletion();
        public static GameObject multEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Toolbot/OmniExplosionVFXToolbotQuick.prefab").WaitForCompletion();
        public static GameObject multCryoExplosionEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Toolbot/CryoCanisterExplosionPrimary.prefab").WaitForCompletion();
        public static GameObject engiShieldEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Engi/BubbleShieldEndEffect.prefab").WaitForCompletion();
        public static GameObject scavSackEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Scav/ScavSackExplosion.prefab").WaitForCompletion();
        //public static GameObject teslaEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ShockNearby/TeslaFieldBuffEffect.prefab").WaitForCompletion();
        public static GameObject overloadingEliteEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/EliteLightning/LightningStakeNova.prefab").WaitForCompletion();
        public static GameObject alphaConstructMuzzleFlashEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/MajorAndMinorConstruct/MajorConstructMuzzleflashSpawnMinorConstruct.prefab").WaitForCompletion();
        public static GameObject stonetitanFistEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Titan/TitanFistEffect.prefab").WaitForCompletion();
        public static GameObject titanClapEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Titan/ExplosionGolemClap.prefab").WaitForCompletion();
        public static GameObject voidFiendBeamMuzzleEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidSurvivor/VoidSurvivorBeamMuzzleflash.prefab").WaitForCompletion();
        public static GameObject artificerFireMuzzleEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mage/MuzzleflashMageFire.prefab").WaitForCompletion();
        public static GameObject artificerIceMuzzleEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mage/MuzzleflashMageIceLarge.prefab").WaitForCompletion();
        public static GameObject artificerLightningMuzzleEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mage/MuzzleflashMageLightning.prefab").WaitForCompletion();
        public static GameObject railgunnerSnipeLightTracerEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/Railgunner/TracerRailgunLight.prefab").WaitForCompletion();
        public static GameObject railgunnerHitSparkEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/Railgunner/HitsparkRailgunnerPistol.prefab").WaitForCompletion();
        public static GameObject commandoOmniExplosionVFXEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Commando/OmniExplosionVFXFMJ.prefab").WaitForCompletion();
        public static GameObject loaderMuzzleFlashEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Loader/MuzzleflashLoader.prefab").WaitForCompletion();
        public static GameObject loaderOmniImpactLightningEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Loader/OmniImpactVFXLoaderLightning.prefab").WaitForCompletion();
        public static GameObject lightningNovaEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/LightningStakeNova");
        public static GameObject muzzleflashMageLightningLargePrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/MuzzleFlashes/MuzzleflashMageLightningLarge");
        public static GameObject voidFiendBeamChargePrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("RoR2/DLC1/VoidSurvivor/VoidSurvivorBeamCharge.prefab");
        public static GameObject multRebarTracerPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("RoR2/Base/Toolbot/TracerToolbotRebar.prefab");
        public static GameObject mushrumSporeImpactPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("RoR2/Base/MiniMushroom/SporeGrenadeGasImpact.prefab");
        //public static GameObject mercOmnimpactVFXPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("RoR2/Base/Merc/OmniImpactVFXSlashMerc.prefab");
        //public static GameObject mercOmnimpactVFXEvisPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("RoR2/Base/Merc/OmniImpactVFXSlashMercEvis.prefab");
        public static GameObject magmaWormOrbExplosionPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("RoR2/Base/MagmaWorm/MagmaOrbExplosion.prefab");
        public static GameObject magmaWormImpactExplosionPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("RoR2/Base/MagmaWorm/MagmaWormImpactExplosion.prefab");
        public static GameObject strongerBurnEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("RoR2/DLC1/StrengthenBurn/StrongerBurnEffect.prefab");
        public static GameObject muzzleflashScavSackPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("RoR2/Base/Scav/MuzzleflashScavSack.prefab");
        public static GameObject tracerHuntressSnipePrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("RoR2/Base/Huntress/TracerHuntressSnipe.prefab");


        //game projectiles
        public static GameObject captainAirStrikeProj = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Captain/CaptainAirstrikeProjectile1.prefab").WaitForCompletion();
        public static GameObject mercWindProj = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Merc/EvisProjectile.prefab").WaitForCompletion();
        public static GameObject lemfireBall = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Lemurian/Fireball.prefab").WaitForCompletion();
        public static GameObject lemfireBallGhost = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Lemurian/FireballGhost.prefab").WaitForCompletion();
        public static GameObject greaterwispBall = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/GreaterWisp/WispCannon.prefab").WaitForCompletion();
        public static GameObject stonetitanFistProj = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Titan/TitanPreFistProjectile.prefab").WaitForCompletion();

        //Area indicators
        internal static GameObject sphereIndicator;
        internal static GameObject deathAuraIndicator;
        internal static GameObject theWorldIndicator;
        internal static GameObject hermitCrabMortarIndicator;
        internal static GameObject voidBarnacleMortarIndicator;
        internal static GameObject barbedSpikesIndicator;
        internal static GameObject auraOfBlightIndicator;
        internal static GameObject doubleTimeIndicator;
        internal static GameObject xBeamerIndicator;


        //Shiggy Equipment Obj
        //internal static GameObject ShiggyEquipmentPrefab;
        ////Shiggy Item Obj
        //internal static GameObject ShiggyItemPrefab;

        internal static void Initialize()
        {

            if (assetbundleName == "myassetbundle")
            {
                Debug.LogError("AssetBundle name hasn't been changed- not loading any assets to avoid conflicts");
                return;
            }

            LoadAssetBundle();
            LoadSoundbank();
            PopulateAssets();

        }

        internal static void LoadAssetBundle()
        {
            if (mainAssetBundle == null)
            {
                using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ShiggyMod." + assetbundleName))
                {
                    mainAssetBundle = AssetBundle.LoadFromStream(assetStream);
                }
            }
            Debug.Log(mainAssetBundle + "main asset bundle");
            assetNames = mainAssetBundle.GetAllAssetNames();
        }

        internal static void LoadSoundbank()
        {
            using (Stream manifestResourceStream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream("ShiggyMod.Shiggy.bnk"))
            {
                byte[] array = new byte[manifestResourceStream2.Length];
                manifestResourceStream2.Read(array, 0, array.Length);
                SoundAPI.SoundBanks.Add(array);
            }
        }

        internal static void PopulateAssets()
        {
            if (!mainAssetBundle)
            {
                Debug.LogError("There is no AssetBundle to load assets from.");
                return;
            }

            //sword swing
            shiggySwingEffect = Assets.LoadEffect("SwingEffect", true);
            shiggyHitImpactEffect = Assets.LoadEffect("ImpactEffect");
            //own effects
            blastingZoneEffect = Assets.LoadEffect("BlastingZoneSword", true);
            finalReleasePulseEffect = Assets.LoadEffect("finalReleasePulse", true);
            //AFO effects
            AFOLineRenderer = mainAssetBundle.LoadAsset<GameObject>("LineRendererAFO");

            //warbanner material setup
            Material warbannerMat = Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/WardOnLevel/matWarbannerSphereIndicator.mat").WaitForCompletion();
            Material[] warbannerArray = new Material[1];
            warbannerArray[0] = warbannerMat;

            
            //Creating spheres and adding the material to them
            sphereIndicator = Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("SphereIndicator");
            sphereIndicator.AddComponent<NetworkIdentity>();
            PrefabAPI.RegisterNetworkPrefab(sphereIndicator);

            deathAuraIndicator = PrefabAPI.InstantiateClone(sphereIndicator, "deathAuraIndicator");

            MeshRenderer deathAuraIndicatorMeshRender = deathAuraIndicator.gameObject.GetComponent<MeshRenderer>();
            if (!deathAuraIndicatorMeshRender)
            {
                Debug.Log("Failed to find Mesh renderer!");
            }
            deathAuraIndicatorMeshRender.materials = warbannerArray;
            deathAuraIndicatorMeshRender.material.SetColor("_TintColor", new Color(153f / 255f, 21f / 255f, 63f / 255f)); //new Color(153/255f, 21/255f, 63/255f)
            networkObjDefs.Add(deathAuraIndicator);
            PrefabAPI.RegisterNetworkPrefab(deathAuraIndicator);

            theWorldIndicator = PrefabAPI.InstantiateClone(sphereIndicator, "theWorldIndicator"); 

            MeshRenderer theWorldIndicatorMeshRender = theWorldIndicator.gameObject.GetComponent<MeshRenderer>();
            if (!theWorldIndicatorMeshRender)
            {
                Debug.Log("Failed to find Mesh renderer!");
            }
            theWorldIndicatorMeshRender.materials = warbannerArray;
            theWorldIndicatorMeshRender.material.SetColor("_TintColor", Color.cyan);
            networkObjDefs.Add(theWorldIndicator);
            PrefabAPI.RegisterNetworkPrefab(theWorldIndicator);


            hermitCrabMortarIndicator = PrefabAPI.InstantiateClone(sphereIndicator, "hermitCrabMortarIndicator");

            MeshRenderer hermitCrabMortarIndicatorMeshRender = hermitCrabMortarIndicator.gameObject.GetComponent<MeshRenderer>();
            if (!hermitCrabMortarIndicatorMeshRender)
            {
                Debug.Log("Failed to find Mesh renderer!");
            }
            hermitCrabMortarIndicatorMeshRender.materials = warbannerArray;
            hermitCrabMortarIndicatorMeshRender.material.SetColor("_TintColor", Color.yellow);
            networkObjDefs.Add(hermitCrabMortarIndicator);
            PrefabAPI.RegisterNetworkPrefab(hermitCrabMortarIndicator);

            voidBarnacleMortarIndicator = PrefabAPI.InstantiateClone(sphereIndicator, "voidBarnacleMortarIndicator");

            MeshRenderer voidBarnacleMortarIndicatorMeshRender = voidBarnacleMortarIndicator.gameObject.GetComponent<MeshRenderer>();
            if (!voidBarnacleMortarIndicatorMeshRender)
            {
                Debug.Log("Failed to find Mesh renderer!");
            }
            voidBarnacleMortarIndicatorMeshRender.materials = warbannerArray;
            voidBarnacleMortarIndicatorMeshRender.material.SetColor("_TintColor", Color.magenta);
            networkObjDefs.Add(voidBarnacleMortarIndicator);
            PrefabAPI.RegisterNetworkPrefab(voidBarnacleMortarIndicator);

            barbedSpikesIndicator = PrefabAPI.InstantiateClone(sphereIndicator, "barbedSpikesIndicator");

            MeshRenderer barbedSpikesIndicatorMeshRender = barbedSpikesIndicator.gameObject.GetComponent<MeshRenderer>();
            if (!barbedSpikesIndicatorMeshRender)
            {
                Debug.Log("Failed to find Mesh renderer!");
            }
            barbedSpikesIndicatorMeshRender.materials = warbannerArray;
            barbedSpikesIndicatorMeshRender.material.SetColor("_TintColor", new Color(153f / 255f, 74f / 255f, 21f / 255f));
            networkObjDefs.Add(barbedSpikesIndicator);
            PrefabAPI.RegisterNetworkPrefab(barbedSpikesIndicator);

            auraOfBlightIndicator = PrefabAPI.InstantiateClone(sphereIndicator, "auraOfBlightIndicator");

            MeshRenderer auraOfBlightIndicatorMeshRender = auraOfBlightIndicator.gameObject.GetComponent<MeshRenderer>();
            if (!auraOfBlightIndicatorMeshRender)
            {
                Debug.Log("Failed to find Mesh renderer!");
            }
            auraOfBlightIndicatorMeshRender.materials = warbannerArray;
            auraOfBlightIndicatorMeshRender.material.SetColor("_TintColor", new Color(125f / 255f, 156f / 255f, 42f / 255f));
            networkObjDefs.Add(auraOfBlightIndicator);
            PrefabAPI.RegisterNetworkPrefab(auraOfBlightIndicator);

            doubleTimeIndicator = PrefabAPI.InstantiateClone(sphereIndicator, "doubleTimeIndicator");

            MeshRenderer doubleTimeIndicatorMeshRender = doubleTimeIndicator.gameObject.GetComponent<MeshRenderer>();
            if (!doubleTimeIndicatorMeshRender)
            {
                Debug.Log("Failed to find Mesh renderer!");
            }
            doubleTimeIndicatorMeshRender.materials = warbannerArray;
            doubleTimeIndicatorMeshRender.material.SetColor("_TintColor", new Color(247f / 255f, 222f / 255f, 27f / 255f));
            networkObjDefs.Add(doubleTimeIndicator);
            PrefabAPI.RegisterNetworkPrefab(doubleTimeIndicator);

            xBeamerIndicator = PrefabAPI.InstantiateClone(sphereIndicator, "doubleTimeIndicator");

            MeshRenderer xBeamerIndicatorMeshRender = xBeamerIndicator.gameObject.GetComponent<MeshRenderer>();
            if (!xBeamerIndicatorMeshRender)
            {
                Debug.Log("Failed to find Mesh renderer!");
            }
            xBeamerIndicatorMeshRender.materials = warbannerArray;
            xBeamerIndicatorMeshRender.material.SetColor("_TintColor", Color.yellow);
            networkObjDefs.Add(xBeamerIndicator);
            PrefabAPI.RegisterNetworkPrefab(xBeamerIndicator);



            //decay particle
            decayattackEffect = LoadEffect("DecayAttack");
            //decaybuffEffect = Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("DecayBuff");
            decaybuffEffect = LoadEffect("DecayBuff");
            decayspreadEffect = LoadEffect("DecaySpread");
            //sounds
            hitSoundEffect = CreateNetworkSoundEventDef("ShiggyHitSFX");
            strongHitSoundEffect = CreateNetworkSoundEventDef("ShiggyStrongAttack");


            //xiconstruct beam effect
            beam = Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("XiConstructBeam");
            beam.AddComponent<NetworkIdentity>();
            networkObjDefs.Add(beam);

            //materials
            //alpha shield effect
            alphaconstructShieldBuffMat = UnityEngine.GameObject.Instantiate<Material>(RoR2.LegacyResourcesAPI.Load<Material>("Materials/matEnergyShield"));
            alphaconstructShieldBuffMat.SetColor("_TintColor", new Color(0.8f, 0.5f, 0f));
            blastingZoneBurnMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/BurnNearby/matHelfireIgniteEffectFlare.mat").WaitForCompletion();


            //multiplier effect
            multiplierShieldBuffMat = mainAssetBundle.LoadAsset<Material>("MultiplierMat");
            //limit break
            limitBreakBuffMat = mainAssetBundle.LoadAsset<Material>("LimitBreakMat");
            //void form
            voidFormBuffMat = Addressables.LoadAssetAsync<Material>("RoR2/DLC1/VoidSurvivor/matVoidBlinkBodyOverlay.mat").WaitForCompletion();
            //death aura effect
            deathAuraBuffMat = mainAssetBundle.LoadAsset<Material>("DeathAuraMat");
            //light form effect
            lightFormBuffMat = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/matFlashWhite.mat").WaitForCompletion();
            //light form effect
            lightAndDarknessMat = mainAssetBundle.LoadAsset<Material>("LightAndDarknessMat");


            PopulateEffects();

            PrefabAPI.RegisterNetworkPrefab(beam);

            //Shiggy Equipment prefab
            //ShiggyEquipmentPrefab = Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("ShiggyEquipmentModel");
            ////Shiggy Item prefab
            //ShiggyItemPrefab = Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("ShiggyItemModel");

        }

        private static void PopulateEffects()
        {
            //grandparent Sun 
            grandparentSunPrefab = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Grandparent/GrandParentSun.prefab").WaitForCompletion(), "ShiggySun", true);

            //grandparentSunPrefab.transform.localScale = new Vector3(0.375f, 0.375f, 0.375f);
            //grandparentSunPrefab.transform.Find("VfxRoot/LightSpinner/LightSpinner/Point Light").GetComponent<Light>().intensity *= 0.375f;
            //grandparentSunPrefab.transform.Find("VfxRoot/LightSpinner/LightSpinner/Point Light").GetComponent<Light>().range = 200 * 0.375f;
            //grandparentSunPrefab.transform.Find("VfxRoot/Mesh/SunMesh").transform.localScale = new Vector3(10, 10, 10);
            //grandparentSunPrefab.transform.Find("VfxRoot/Mesh/AreaIndicator").transform.localScale = new Vector3(105, 105, 105);

            //grandparent Sun spawn explosion effect
            //grandparentSunSpawnPrefab = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Grandparent/GrandParentSunSpawn.prefab").WaitForCompletion(), "ShiggySunSpawn", false);
            //grandparentSunSpawnPrefab.transform.localScale = new Vector3(0.375f, 0.375f, 0.375f);
            //grandparentSunSpawnPrefab.transform.Find("Point Light").GetComponent<Light>().intensity *= 0.375f;
            //grandparentSunSpawnPrefab.transform.Find("Point Light").GetComponent<Light>().range = 200 * 0.375f;
            //grandparentSunSpawnPrefab.GetComponent<DestroyOnTimer>().duration = 1.5f;
            //AddNewEffectDef(grandparentSunPrefab);

        }

        private static GameObject CreateTracer(string originalTracerName, string newTracerName)
        {
            if (RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/" + originalTracerName) == null) return null;

            GameObject newTracer = PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/" + originalTracerName), newTracerName, true);

            if (!newTracer.GetComponent<EffectComponent>()) newTracer.AddComponent<EffectComponent>();
            if (!newTracer.GetComponent<VFXAttributes>()) newTracer.AddComponent<VFXAttributes>();
            if (!newTracer.GetComponent<NetworkIdentity>()) newTracer.AddComponent<NetworkIdentity>();

            newTracer.GetComponent<Tracer>().speed = 250f;
            newTracer.GetComponent<Tracer>().length = 50f;

            AddNewEffectDef(newTracer);

            return newTracer;
        }

        

        internal static NetworkSoundEventDef CreateNetworkSoundEventDef(string eventName)
        {
            NetworkSoundEventDef networkSoundEventDef = ScriptableObject.CreateInstance<NetworkSoundEventDef>();
            networkSoundEventDef.akId = AkSoundEngine.GetIDFromString(eventName);
            networkSoundEventDef.eventName = eventName;

            networkSoundEventDefs.Add(networkSoundEventDef);

            return networkSoundEventDef;
        }

        internal static void ConvertAllRenderersToHopooShader(GameObject objectToConvert)
        {
            if (!objectToConvert) return;

            foreach (MeshRenderer i in objectToConvert.GetComponentsInChildren<MeshRenderer>())
            {
                if (i)
                {
                    if (i.material)
                    {
                        i.material.shader = hotpoo;
                    }
                }
            }

            foreach (SkinnedMeshRenderer i in objectToConvert.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if (i)
                {
                    if (i.material)
                    {
                        i.material.shader = hotpoo;
                    }
                }
            }
        }

        internal static CharacterModel.RendererInfo[] SetupRendererInfos(GameObject obj)
        {
            MeshRenderer[] meshes = obj.GetComponentsInChildren<MeshRenderer>();
            CharacterModel.RendererInfo[] rendererInfos = new CharacterModel.RendererInfo[meshes.Length];

            for (int i = 0; i < meshes.Length; i++)
            {
                rendererInfos[i] = new CharacterModel.RendererInfo
                {
                    defaultMaterial = meshes[i].material,
                    renderer = meshes[i],
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                    ignoreOverlays = false
                };
            }

            return rendererInfos;
        }

        internal static Texture LoadCharacterIcon(string characterName)
        {
            return mainAssetBundle.LoadAsset<Texture>("tex" + characterName + "Icon");
        }

        internal static GameObject LoadCrosshair(string crosshairName)
        {
            if (RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Crosshair/" + crosshairName + "Crosshair") == null) return RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Crosshair/StandardCrosshair");
            return RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Crosshair/" + crosshairName + "Crosshair");
        }

        private static GameObject LoadEffect(string resourceName)
        {
            return LoadEffect(resourceName, "", false);
        }

        private static GameObject LoadEffect(string resourceName, string soundName)
        {
            return LoadEffect(resourceName, soundName, false);
        }

        private static GameObject LoadEffect(string resourceName, bool parentToTransform)
        {
            return LoadEffect(resourceName, "", parentToTransform);
        }

        private static GameObject LoadEffect(string resourceName, string soundName, bool parentToTransform)
        {
            bool assetExists = false;
            for (int i = 0; i < assetNames.Length; i++)
            {
                if (assetNames[i].Contains(resourceName.ToLower()))
                {
                    assetExists = true;
                    i = assetNames.Length;
                }
            }

            if (!assetExists)
            {
                Debug.LogError("Failed to load effect: " + resourceName + " because it does not exist in the AssetBundle");
                return null;
            }

            GameObject newEffect = mainAssetBundle.LoadAsset<GameObject>(resourceName);

            newEffect.AddComponent<DestroyOnTimer>().duration = 12;
            newEffect.AddComponent<NetworkIdentity>();
            newEffect.AddComponent<VFXAttributes>().vfxPriority = VFXAttributes.VFXPriority.Always;
            var effect = newEffect.AddComponent<EffectComponent>();
            effect.applyScale = false;
            effect.effectIndex = EffectIndex.Invalid;
            effect.parentToReferencedTransform = parentToTransform;
            effect.positionAtReferencedTransform = true;
            effect.soundName = soundName;

            AddNewEffectDef(newEffect, soundName);

            return newEffect;
        }

        private static void AddNewEffectDef(GameObject effectPrefab)
        {
            AddNewEffectDef(effectPrefab, "");
        }

        private static void AddNewEffectDef(GameObject effectPrefab, string soundName)
        {
            EffectDef newEffectDef = new EffectDef();
            newEffectDef.prefab = effectPrefab;
            newEffectDef.prefabEffectComponent = effectPrefab.GetComponent<EffectComponent>();
            newEffectDef.prefabName = effectPrefab.name;
            newEffectDef.prefabVfxAttributes = effectPrefab.GetComponent<VFXAttributes>();
            newEffectDef.spawnSoundEventName = soundName;

            effectDefs.Add(newEffectDef);
        }

        public static Material CreateMaterial(string materialName, float emission, Color emissionColor, float normalStrength)
        {
            if (!commandoMat) commandoMat = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody").GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial;

            Material mat = UnityEngine.Object.Instantiate<Material>(commandoMat);
            Material tempMat = Assets.mainAssetBundle.LoadAsset<Material>(materialName);

            if (!tempMat)
            {
                Debug.LogError("Failed to load material: " + materialName + " - Check to see that the name in your Unity project matches the one in this code");
                return commandoMat;
            }

            mat.name = materialName;
            mat.SetColor("_Color", tempMat.GetColor("_Color"));
            mat.SetTexture("_MainTex", tempMat.GetTexture("_MainTex"));
            mat.SetColor("_EmColor", emissionColor);
            mat.SetFloat("_EmPower", emission);
            mat.SetTexture("_EmTex", tempMat.GetTexture("_EmissionMap"));
            mat.SetFloat("_NormalStrength", normalStrength);

            return mat;
        }

        public static Material CreateMaterial(string materialName)
        {
            return Assets.CreateMaterial(materialName, 0f);
        }

        public static Material CreateMaterial(string materialName, float emission)
        {
            return Assets.CreateMaterial(materialName, emission, Color.white);
        }

        public static Material CreateMaterial(string materialName, float emission, Color emissionColor)
        {
            return Assets.CreateMaterial(materialName, emission, emissionColor, 0f);
        }
    }
}