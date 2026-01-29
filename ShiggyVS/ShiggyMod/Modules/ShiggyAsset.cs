using R2API;
using RoR2;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace ShiggyMod.Modules
{
    internal static class ShiggyAsset
    {
        // the assetbundle to load assets from
        internal static AssetBundle mainAssetBundle;

        //font
        internal static Font ror2Font;


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
        internal static Shader hotpoo = Addressables.LoadAssetAsync<Shader>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Shaders.HGStandard_shader).WaitForCompletion();
        //internal static Shader hotpoo = RoR2.LegacyResourcesAPI.Load<Shader>("Shaders/Deferred/HGStandard");
        internal static Material commandoMat;
        private static string[] assetNames = new string[0];

        // CHANGE THIS
        private const string assetbundleName = "shiggyassetbundle";

        //tracers
        public static GameObject VoidFiendBeamTracer;

        //buffs
        public static Sprite lunarRootIcon;
        public static Sprite strongerBurnIcon;
        public static Sprite mercExposeIcon;
        public static Sprite deathMarkDebuffIcon;
        public static Sprite singularityBuffIcon;
        public static Sprite cloakBuffIcon;
        public static Sprite voidFogDebuffIcon;
        public static Sprite medkitBuffIcon;
        public static Sprite spikyDebuffIcon;
        public static Sprite ruinDebuffIcon;
        public static Sprite warcryBuffIcon;
        public static Sprite shieldBuffIcon;
        public static Sprite tarBuffIcon;
        public static Sprite crippleBuffIcon;
        public static Sprite speedBuffIcon;
        public static Sprite boostBuffIcon;
        public static Sprite alphashieldonBuffIcon;
        public static Sprite alphashieldoffBuffIcon;
        public static Sprite decayBuffIcon;
        public static Sprite multiplierBuffIcon;
        public static Sprite jumpBuffIcon;
        public static Sprite sprintBuffIcon;
        public static Sprite spikeBuffIcon;
        public static Sprite mortarBuffIcon;
        public static Sprite healBuffIcon;
        public static Sprite attackspeedBuffIcon;
        public static Sprite noCooldownBuffIcon;
        public static Sprite bleedBuffIcon;
        public static Sprite skinBuffIcon;
        public static Sprite orbreadyBuffIcon;
        public static Sprite orbdisableBuffIcon;
        public static Sprite blazingBuffIcon;
        public static Sprite lightningBuffIcon;
        public static Sprite resonanceBuffIcon;
        public static Sprite critBuffIcon;
        public static Sprite claygooBuffIcon;
        public static Sprite predatorBuffIcon;
        public static Sprite hemorrhageBuffIcon;
        public static Sprite teleportOnLowHealthBuffIcon;
        public static Sprite teleportOnLowHealthCDBuffIcon;
        public static Sprite affixAurelioniteBuffIcon;
        public static Sprite falseSonEnergizedCoreBuffIcon;
        public static Sprite chefOilBuffIcon;
        public static Sprite soluswingWeakpointDestroyedDebuffIcon;

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
        public static GameObject voidMegaCrabExplosionEffect;
        public static GameObject huntressGlaiveChargeEffect;
        public static GameObject huntressGlaiveMuzzleEffect;
        public static GameObject bisonEffect;
        public static GameObject GupSpikeEffect;
        public static GameObject chargegreaterwispBall;
        public static GameObject larvajumpEffect;
        public static GameObject elderlemurianexplosionEffect;
        public static GameObject parentslamEffect;
        public static GameObject claydunestriderEffect;
        public static GameObject voidjailerEffect;
        public static GameObject voidjailermuzzleEffect;
        public static GameObject xiconstructbeamEffect;
        public static GameObject xiconstructSecondMuzzleEffect;
        public static GameObject xiconstructDeathEffect;
        public static GameObject xiconstructBeamLaser;
        public static GameObject lunarGolemSmokeEffect;
        public static GameObject impBossExplosionEffect;
        public static GameObject impBossGroundSlamEffect;
        public static GameObject seekerChakraEffect;

        public static RoR2.Audio.LoopSoundDef xiconstructsound;
        public static GameObject grandparentSunPrefab;
        public static GameObject grandparentSunSpawnPrefab;

        public static GameObject artificerfireEffect;
        public static GameObject artificerlightningorbchargeEffect;
        public static GameObject artificerlightningorbMuzzleEffect;
        public static GameObject artificerlightningorbprojectileEffect;
        public static GameObject artificerCrosshair;
        public static RoR2.Audio.LoopSoundDef artificerlightningsound;
        public static GameObject banditmuzzleEffect;
        public static GameObject bandittracerEffectPrefab;
        public static GameObject banditimpactEffect;
        public static GameObject banditCrosshair;
        public static GameObject engiTurret;
        public static GameObject voidfiendblinkVFX;
        public static GameObject voidfiendblinkmuzzleEffect;
        public static GameObject voidfiendblinktrailEffect;
        public static Material voidfiendblinkMaterial;
        public static GameObject railgunnercryoCrosshair;
        public static GameObject railgunnercryochargeCrosshair;
        public static GameObject railgunnercryoreloadCrosshair;
        public static GameObject railgunnercryoTracer;
        public static RoR2.Audio.LoopSoundDef railgunnercryochargingSound;
        public static RoR2.Audio.LoopSoundDef railgunnercryoofflineSound;
        public static GameObject multEffect;
        public static GameObject multCryoExplosionEffect;
        public static GameObject engiShieldEffect;
        public static GameObject scavSackEffect;
        //public static GameObject teslaEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ShockNearby/TeslaFieldBuffEffect.prefab").WaitForCompletion();
        public static GameObject overloadingEliteEffect;
        public static GameObject alphaConstructMuzzleFlashEffect;
        public static GameObject stonetitanFistEffect;
        public static GameObject titanClapEffect;
        public static GameObject voidFiendBeamMuzzleEffect;
        public static GameObject artificerFireMuzzleEffect;
        public static GameObject artificerIceMuzzleEffect;
        public static GameObject artificerLightningMuzzleEffect;
        public static GameObject railgunnerSnipeLightTracerEffect;
        public static GameObject railgunnerHitSparkEffect;
        public static GameObject commandoOmniExplosionVFXEffect;
        public static GameObject loaderMuzzleFlashEffect;
        public static GameObject loaderOmniImpactLightningEffect;
        public static GameObject lightningNovaEffectPrefab;
        public static GameObject muzzleflashMageLightningLargePrefab;
        public static GameObject voidFiendBeamChargePrefab;
        public static GameObject multRebarTracerPrefab;
        public static GameObject mushrumSporeImpactPrefab;
        //public static GameObject mercOmnimpactVFXPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("RoR2/Base/Merc/OmniImpactVFXSlashMerc.prefab");
        //public static GameObject mercOmnimpactVFXEvisPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("RoR2/Base/Merc/OmniImpactVFXSlashMercEvis.prefab");
        public static GameObject magmaWormOrbExplosionPrefab;
        public static GameObject magmaWormImpactExplosionPrefab;
        public static GameObject strongerBurnEffectPrefab;
        public static GameObject muzzleflashScavSackPrefab;
        public static GameObject tracerHuntressSnipePrefab;
        public static GameObject chefGlazeEffectMuzzlePrefab;
        public static GameObject operatorPistolTracerPrefab;
        public static GameObject operatorPistolHitEffectPrefab;
        public static GameObject operatorPistolMuzzlePrefab;
        public static GameObject solusExtractorImpactPrefab;
        public static GameObject solusExtractorMuzzlePrefab;
        public static GameObject solusInvalidatorBlastEffectPrefab;
        public static GameObject solusScorcherBlastEffectPrefab;
        public static GameObject solusTransporterBlastEffectPrefab;
        public static GameObject solusFactorMuzzleEffectPrefab1;
        public static GameObject solusFactorMuzzleEffectPrefab2;
        public static GameObject solusFactorMuzzleEffectPrefab3;
        public static GameObject solusFactorMuzzleEffectPrefab4;
        public static GameObject solusFactorMuzzleEffectPrefab5;
        public static GameObject solusFactorMuzzleEffectPrefab6;
        public static GameObject solusFactorBlastEffectPrefab;


        //game projectiles
        public static GameObject captainAirStrikeProj;
        public static GameObject mercWindProj;
        public static GameObject lemfireBall;
        public static GameObject lemfireBallGhost;
        public static GameObject greaterwispBall;
        public static GameObject stonetitanFistProj;
        public static GameObject chefGlazeProjectilePrefab;

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

            //font
            ror2Font = Addressables.LoadAssetAsync<Font>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Common_Fonts_Bombardier.BOMBARD__ttf).WaitForCompletion();

            //tracers
            VoidFiendBeamTracer = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidSurvivor.VoidSurvivorBeamTracer_prefab).WaitForCompletion();

            //buffs
            lunarRootIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_LunarSkillReplacements.bdLunarSecondaryRoot_asset).WaitForCompletion().iconSprite;
            strongerBurnIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_StrengthenBurn.bdStrongerBurn_asset).WaitForCompletion().iconSprite;
            mercExposeIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Merc.bdMercExpose_asset).WaitForCompletion().iconSprite;
            deathMarkDebuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_DeathMark.bdDeathMark_asset).WaitForCompletion().iconSprite;
            singularityBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_ElementalRingVoid.bdElementalRingVoidReady_asset).WaitForCompletion().iconSprite;
            cloakBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Common.bdCloak_asset).WaitForCompletion().iconSprite;
            voidFogDebuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Common.bdVoidFogMild_asset).WaitForCompletion().iconSprite;
            medkitBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Medkit.bdMedkitHeal_asset).WaitForCompletion().iconSprite;
            spikyDebuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Treebot.bdEntangle_asset).WaitForCompletion().iconSprite;
            ruinDebuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_LunarSkillReplacements.bdLunarDetonationCharge_asset).WaitForCompletion().iconSprite;
            warcryBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_WarCryOnMultiKill.bdWarCryBuff_asset).WaitForCompletion().iconSprite;
            shieldBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Common.bdArmorBoost_asset).WaitForCompletion().iconSprite;
            tarBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Common.bdClayGoo_asset).WaitForCompletion().iconSprite;
            crippleBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Common.bdCripple_asset).WaitForCompletion().iconSprite;
            speedBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bandit2.bdCloakSpeed_asset).WaitForCompletion().iconSprite;
            boostBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_RandomDamageZone.bdPowerBuff_asset).WaitForCompletion().iconSprite;
            alphashieldonBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_BearVoid.bdBearVoidReady_asset).WaitForCompletion().iconSprite;
            alphashieldoffBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_BearVoid.bdBearVoidCooldown_asset).WaitForCompletion().iconSprite;
            decayBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Common.bdVoidFogStrong_asset).WaitForCompletion().iconSprite;
            multiplierBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_PrimarySkillShuriken.bdPrimarySkillShurikenBuff_asset).WaitForCompletion().iconSprite;
            jumpBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MoveSpeedOnKill.bdKillMoveSpeed_asset).WaitForCompletion().iconSprite;
            sprintBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_SprintOutOfCombat.bdWhipBoost_asset).WaitForCompletion().iconSprite;
            spikeBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Grandparent.bdOverheat_asset).WaitForCompletion().iconSprite;
            mortarBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_GainArmor.bdElephantArmorBoost_asset).WaitForCompletion().iconSprite;
            healBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Croco.bdCrocoRegen_asset).WaitForCompletion().iconSprite;
            attackspeedBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_EnergizedOnEquipmentUse.bdEnergized_asset).WaitForCompletion().iconSprite;
            noCooldownBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_KillEliteFrenzy.bdNoCooldowns_asset).WaitForCompletion().iconSprite;
            bleedBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Common.bdBleeding_asset).WaitForCompletion().iconSprite;
            skinBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_OutOfCombatArmor.bdOutOfCombatArmorBuff_asset).WaitForCompletion().iconSprite;
            orbreadyBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ElementalRings.bdElementalRingsReady_asset).WaitForCompletion().iconSprite;
            orbdisableBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ElementalRings.bdElementalRingsCooldown_asset).WaitForCompletion().iconSprite;
            blazingBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Common.bdOnFire_asset).WaitForCompletion().iconSprite;
            lightningBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ShockNearby.bdTeslaField_asset).WaitForCompletion().iconSprite;
            resonanceBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_LaserTurbine.bdLaserTurbineKillCharge_asset).WaitForCompletion().iconSprite;
            critBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_CritOnUse.bdFullCrit_asset).WaitForCompletion().iconSprite;
            claygooBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Common.bdClayGoo_asset).WaitForCompletion().iconSprite;
            predatorBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_AttackSpeedOnCrit.bdAttackSpeedOnCrit_asset).WaitForCompletion().iconSprite;
            hemorrhageBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bandit2.bdSuperBleed_asset).WaitForCompletion().iconSprite;
            teleportOnLowHealthBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_Items_TeleportOnLowHealth.bdTeleportOnLowHealthActive_asset).WaitForCompletion().iconSprite;
            teleportOnLowHealthCDBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_Items_TeleportOnLowHealth.bdTeleportOnLowHealthCooldown_asset).WaitForCompletion().iconSprite;
            affixAurelioniteBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_Elites_EliteAurelionite.bdEliteAurelionite_asset).WaitForCompletion().iconSprite;
            falseSonEnergizedCoreBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_FalseSon.bdEnergizedCore_asset).WaitForCompletion().iconSprite;
            chefOilBuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_Chef.bdOiled_asset).WaitForCompletion().iconSprite;
            soluswingWeakpointDestroyedDebuffIcon = Addressables.LoadAssetAsync<BuffDef>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_Buffs.bdSolusWingWeakpointDestroyed_asset).WaitForCompletion().iconSprite;



            //game effects
            voidMegaCrabExplosionEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidMegaCrab.VoidMegacrabAntimatterExplosion_prefab).WaitForCompletion();
            huntressGlaiveChargeEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Huntress.ChargeGlaive_prefab).WaitForCompletion();
            huntressGlaiveMuzzleEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Huntress.HuntressGlaiveSwing_prefab).WaitForCompletion();
            bisonEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bison.BisonChargeStep_prefab).WaitForCompletion();
            GupSpikeEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Gup.GupExplosion_prefab).WaitForCompletion();
            chargegreaterwispBall = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_GreaterWisp.OmniExplosionVFXGreaterWisp_prefab).WaitForCompletion();
            larvajumpEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_AcidLarva.AcidLarvaLeapExplosion_prefab).WaitForCompletion();
            elderlemurianexplosionEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_LemurianBruiser.OmniExplosionVFXLemurianBruiserFireballImpact_prefab).WaitForCompletion();
            parentslamEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Parent.ParentSlamEffect_prefab).WaitForCompletion();
            claydunestriderEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ClayBruiser.ClayShockwaveEffect_prefab).WaitForCompletion();
            voidjailerEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidJailer.VoidJailerDeathBombExplosion_prefab).WaitForCompletion();
            voidjailermuzzleEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidSurvivor.VoidSurvivorBeamTracer_prefab).WaitForCompletion();
            xiconstructbeamEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.MajorConstructInitialMuzzleFlash_prefab).WaitForCompletion();
            xiconstructSecondMuzzleEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.MajorConstructSecondMuzzleFlash_prefab).WaitForCompletion();
            xiconstructDeathEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.MajorConstructDeathEffect_prefab).WaitForCompletion();
            xiconstructBeamLaser = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.LaserMajorConstruct_prefab).WaitForCompletion();
            lunarGolemSmokeEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_LunarGolem.BlastSmokeLunarGolem_prefab).WaitForCompletion();
            impBossExplosionEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ImpBoss.ImpBossBlink_prefab).WaitForCompletion();
            impBossGroundSlamEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ImpBoss.ImpBossGroundSlam_prefab).WaitForCompletion();
            xiconstructsound = Addressables.LoadAssetAsync<RoR2.Audio.LoopSoundDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.lsdMajorConstructLaser_asset).WaitForCompletion();
            seekerChakraEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_Seeker.MeditateSuccessVFX_prefab).WaitForCompletion();


            artificerfireEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Mage.MageFlamethrowerEffect_prefab).WaitForCompletion();
            artificerlightningorbchargeEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Mage.MuzzleflashMageLightningLarge_prefab).WaitForCompletion();
            artificerlightningorbMuzzleEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Mage.MuzzleflashMageLightning_prefab).WaitForCompletion();
            artificerlightningorbprojectileEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Mage.MageLightningBombProjectile_prefab).WaitForCompletion();
            artificerCrosshair = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Mage.MageCrosshair_prefab).WaitForCompletion();
            artificerlightningsound = Addressables.LoadAssetAsync<RoR2.Audio.LoopSoundDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Vagrant.lsdVagrantTrackingBombFlight_asset).WaitForCompletion();
            banditmuzzleEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bandit2.MuzzleflashBandit2_prefab).WaitForCompletion();
            bandittracerEffectPrefab = Resources.Load<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bandit2.TracerBandit2Rifle_prefab);
            banditimpactEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bandit2.HitsparkBandit2Pistol_prefab).WaitForCompletion();
            banditCrosshair = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bandit2.Bandit2CrosshairPrepRevolver_prefab).WaitForCompletion();
            engiTurret = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Engi.EngiTurretMaster_prefab).WaitForCompletion();
            voidfiendblinkVFX = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidSurvivor.VoidBlinkVfx_prefab).WaitForCompletion();
            voidfiendblinkmuzzleEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidSurvivor.VoidBlinkMuzzleflash_prefab).WaitForCompletion();
            voidfiendblinktrailEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidSurvivor.PodVoidGroundImpact_prefab).WaitForCompletion();
            voidfiendblinkMaterial = Addressables.LoadAssetAsync<Material>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidSurvivor.matVoidBlinkBodyOverlay_mat).WaitForCompletion();
            railgunnercryoCrosshair = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Railgunner.RailgunnerCryochargeCrosshair_prefab).WaitForCompletion();
            railgunnercryochargeCrosshair = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Railgunner.RailgunnerCryochargeUI_prefab).WaitForCompletion();
            railgunnercryoreloadCrosshair = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Railgunner.RailgunnerReloadUI_prefab).WaitForCompletion();
            railgunnercryoTracer = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Railgunner.TracerRailgunCryo_prefab).WaitForCompletion();
            railgunnercryochargingSound = Addressables.LoadAssetAsync<RoR2.Audio.LoopSoundDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Railgunner.lsdRailgunnerBackpackCharging_asset).WaitForCompletion();
            railgunnercryoofflineSound = Addressables.LoadAssetAsync<RoR2.Audio.LoopSoundDef>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Railgunner.lsdRailgunnerBackpackOffline_asset).WaitForCompletion();
            multEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Toolbot.OmniExplosionVFXToolbotQuick_prefab).WaitForCompletion();
            multCryoExplosionEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Toolbot.CryoCanisterExplosionPrimary_prefab).WaitForCompletion();
            engiShieldEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Engi.BubbleShieldEndEffect_prefab).WaitForCompletion();
            scavSackEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Scav.ScavSackExplosion_prefab).WaitForCompletion();
            // teslaEffect = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ShockNearby/TeslaFieldBuffEffect.prefab").WaitForCompletion();
            overloadingEliteEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_EliteLightning.LightningStakeNova_prefab).WaitForCompletion();
            alphaConstructMuzzleFlashEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.MajorConstructMuzzleflashSpawnMinorConstruct_prefab).WaitForCompletion();
            stonetitanFistEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Titan.TitanFistEffect_prefab).WaitForCompletion();
            titanClapEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Titan.ExplosionGolemClap_prefab).WaitForCompletion();
            voidFiendBeamMuzzleEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidSurvivor.VoidSurvivorBeamMuzzleflash_prefab).WaitForCompletion();
            artificerFireMuzzleEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Mage.MuzzleflashMageFire_prefab).WaitForCompletion();
            artificerIceMuzzleEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Mage.MuzzleflashMageIceLarge_prefab).WaitForCompletion();
            artificerLightningMuzzleEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Mage.MuzzleflashMageLightning_prefab).WaitForCompletion();
            railgunnerSnipeLightTracerEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Railgunner.TracerRailgunLight_prefab).WaitForCompletion();
            railgunnerHitSparkEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Railgunner.HitsparkRailgunnerPistol_prefab).WaitForCompletion();
            commandoOmniExplosionVFXEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Commando.OmniExplosionVFXFMJ_prefab).WaitForCompletion();
            loaderMuzzleFlashEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Loader.MuzzleflashLoader_prefab).WaitForCompletion();
            loaderOmniImpactLightningEffect = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Loader.OmniImpactVFXLoaderLightning_prefab).WaitForCompletion();
            lightningNovaEffectPrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_EliteLightning.LightningStakeNova_prefab).WaitForCompletion();
            muzzleflashMageLightningLargePrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Mage.MuzzleflashMageLightningLarge_prefab).WaitForCompletion();
            voidFiendBeamChargePrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidSurvivor.VoidSurvivorBeamCharge_prefab).WaitForCompletion();
            multRebarTracerPrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Toolbot.TracerToolbotRebar_prefab).WaitForCompletion();
            mushrumSporeImpactPrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_MiniMushroom.SporeGrenadeGasImpact_prefab).WaitForCompletion();
            // mercOmnimpactVFXPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("RoR2/Base/Merc/OmniImpactVFXSlashMerc.prefab");
            // mercOmnimpactVFXEvisPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("RoR2/Base/Merc/OmniImpactVFXSlashMercEvis.prefab");
            magmaWormOrbExplosionPrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_MagmaWorm.MagmaOrbExplosion_prefab).WaitForCompletion();
            magmaWormImpactExplosionPrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_MagmaWorm.MagmaWormImpactExplosion_prefab).WaitForCompletion();
            strongerBurnEffectPrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_StrengthenBurn.StrongerBurnEffect_prefab).WaitForCompletion();
            muzzleflashScavSackPrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Scav.MuzzleflashScavSack_prefab).WaitForCompletion();
            tracerHuntressSnipePrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Huntress.TracerHuntressSnipe_prefab).WaitForCompletion();
            chefGlazeEffectMuzzlePrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC2_Chef.bdOiled_asset).WaitForCompletion();
            operatorPistolTracerPrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_Drone_Tech.TracerNanoPistolCharged_prefab).WaitForCompletion();
            operatorPistolMuzzlePrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_Drone_Tech.NanoPistolMuzzleFlashVFX_prefab).WaitForCompletion();
            operatorPistolHitEffectPrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_Drone_Tech.NanoPistolRicochetImpactEffect_prefab).WaitForCompletion();
            solusExtractorMuzzlePrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_ExtractorUnit.ExtractorUnitHitEffectVFX_prefab).WaitForCompletion();
            solusExtractorMuzzlePrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_ExtractorUnit.ExtractorUnitMeleeActiveVFX_prefab).WaitForCompletion();
            solusInvalidatorBlastEffectPrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_DefectiveUnit.ArtilleryLandedExplosionVFX_prefab).WaitForCompletion();
            solusScorcherBlastEffectPrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_Tanker.GreasePuddleExplosion_VFX_prefab).WaitForCompletion();
            solusTransporterBlastEffectPrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_IronHauler.IronHaulerGravityWellPullBlastVFX_prefab).WaitForCompletion();
            solusFactorMuzzleEffectPrefab1 = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_SolusMine.SolusMinePreAttackVFX_prefab).WaitForCompletion();
            solusFactorMuzzleEffectPrefab2 = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_ExtractorUnit.ExtractorUnitHitEffectVFX_prefab).WaitForCompletion();
            solusFactorMuzzleEffectPrefab3 = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_DefectiveUnit.DenialChargeVFX_prefab).WaitForCompletion();
            solusFactorMuzzleEffectPrefab4 = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_Tanker.TankerAccelerantMuzzleFlashVFX_prefab).WaitForCompletion();
            solusFactorMuzzleEffectPrefab5 = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_IronHauler.IronHaulerMuzzleFlashVFX_prefab).WaitForCompletion();
            solusFactorMuzzleEffectPrefab6 = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_WorkerUnit.WorkerUnitHitImpact_prefab).WaitForCompletion();
            solusFactorBlastEffectPrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_IronHauler.IronHaulerCopiedAoEVFX_prefab).WaitForCompletion();


            //game projectiles
            captainAirStrikeProj = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Captain.CaptainAirstrikeProjectile1_prefab).WaitForCompletion();
            mercWindProj = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Merc.EvisProjectile_prefab).WaitForCompletion();
            lemfireBall = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Lemurian.Fireball_prefab).WaitForCompletion();
            lemfireBallGhost = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Lemurian.FireballGhost_prefab).WaitForCompletion();
            greaterwispBall = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_GreaterWisp.WispCannon_prefab).WaitForCompletion();
            stonetitanFistProj = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Titan.TitanPreFistProjectile_prefab).WaitForCompletion();
            chefGlazeProjectilePrefab = Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_Chef.ChefGlazeProjectile_prefab).WaitForCompletion();


            //sword swing
            shiggySwingEffect = ShiggyAsset.LoadEffect("SwingEffect", true);
            shiggyHitImpactEffect = ShiggyAsset.LoadEffect("ImpactEffect");
            //own effects
            blastingZoneEffect = ShiggyAsset.LoadEffect("BlastingZoneSword", true);
            finalReleasePulseEffect = ShiggyAsset.LoadEffect("finalReleasePulse", true);
            //AFO effects
            AFOLineRenderer = mainAssetBundle.LoadAsset<GameObject>("LineRendererAFO");

            //warbanner material setup
            Material warbannerMat = Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/WardOnLevel/matWarbannerSphereIndicator.mat").WaitForCompletion();
            Material[] warbannerArray = new Material[1];
            warbannerArray[0] = warbannerMat;


            //Creating spheres and adding the material to them
            sphereIndicator = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<GameObject>("SphereIndicator");
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
            decayattackEffect = LoadEffect("DecayPlusUltra");
            //decaybuffEffect = Modules.Asset.mainAssetBundle.LoadAsset<GameObject>("DecayBuff");
            decaybuffEffect = LoadEffect("DecayBuff");
            decayspreadEffect = LoadEffect("DecaySpread");
            //sounds
            hitSoundEffect = CreateNetworkSoundEventDef("ShiggyHitSFX");
            strongHitSoundEffect = CreateNetworkSoundEventDef("ShiggyStrongAttack");


            //xiconstruct beam effect
            beam = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<GameObject>("XiConstructBeam");
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
            //ShiggyEquipmentPrefab = Modules.Asset.mainAssetBundle.LoadAsset<GameObject>("ShiggyEquipmentModel");
            ////Shiggy Item prefab
            //ShiggyItemPrefab = Modules.Asset.mainAssetBundle.LoadAsset<GameObject>("ShiggyItemModel");

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

            Modules.Content.AddNetworkSoundEventDef(networkSoundEventDef);

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

        public static GameObject LoadSurvivorModel(string modelName)
        {
            GameObject model = mainAssetBundle.LoadAsset<GameObject>(modelName);
            if (model == null)
            {
                Log.Error("Trying to load a null model- check to see if the BodyName in your code matches the prefab name of the object in Unity\nFor Example, if your prefab in unity is 'mdlHenry', then your BodyName must be 'Henry'");
                return null;
            }

            return PrefabAPI.InstantiateClone(model, model.name, false);
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

            Modules.Content.AddEffectDef(newEffectDef);
        }

        public static Material CreateMaterial(string materialName, float emission, Color emissionColor, float normalStrength, bool cutout = false, float cutoff = 0.4f, bool doubleSided = true)
        {
            if (!commandoMat) commandoMat = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody").GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial;

            Material mat = UnityEngine.Object.Instantiate<Material>(commandoMat);
            Material tempMat = ShiggyAsset.mainAssetBundle.LoadAsset<Material>(materialName);

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

            // --- Cutout setup ---
            if (cutout)
            {
                mat.EnableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHABLEND_ON");

                mat.SetFloat("_Cutoff", cutoff);

                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1);

                mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest; // 2450
                mat.SetInt("_Cull", doubleSided ? 0 : 2); // 0 = Off (double sided), 2 = Backface cull
            }


            return mat;
        }

        public static Material CreateMaterial(string materialName)
        {
            return ShiggyAsset.CreateMaterial(materialName, 0f);
        }

        public static Material CreateMaterial(string materialName, float emission)
        {
            return ShiggyAsset.CreateMaterial(materialName, emission, Color.white);
        }

        public static Material CreateMaterial(string materialName, float emission, Color emissionColor)
        {
            return ShiggyAsset.CreateMaterial(materialName, emission, emissionColor, 0f);
        }
    }
}