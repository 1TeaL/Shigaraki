using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ShiggyMod.Modules.Survivors.ShiggyMasterController;

namespace ShiggyMod.Modules.Quirks
{
    //All quirk IDs
    public enum QuirkId
    {
        None,

        // ============================
        // Level 1 base actives
        // ============================
        Shiggy_DecayActive,
        Shiggy_AirCannonActive,
        Shiggy_BulletLaserActive,
        Shiggy_MultiplierActive,

        // Utility / internal
        Utility_EmptySkill,
        Utility_Choose,
        Utility_Remove,

        // ============================
        // Level 1 passives
        // ============================
        // --- Elite affixes (treat as L1 Passives) ---
        Elite_BlazingPassive,       // AffixRed
        Elite_GlacialPassive,       // AffixWhite
        Elite_MalachitePassive,     // AffixPoison
        Elite_CelestinePassive,     // AffixHaunted
        Elite_OverloadingPassive,   // AffixBlue
        Elite_LunarPassive,         // AffixLunar
        Elite_MendingPassive,       // DLC1 EliteEarth
        Elite_VoidPassive,          // DLC1 EliteVoid
        AlphaConstruct_BarrierPassive,
        Beetle_StrengthPassive,
        Pest_JumpPassive,
        Vermin_SpeedPassive,
        Gup_SpikyBodyPassive,
        HermitCrab_MortarPassive,
        Larva_AcidJumpPassive,
        LesserWisp_HastePassive,
        LunarExploder_LunarBarrierPassive,
        MiniMushrum_HealingAuraPassive,
        RoboBallMini_SolusBoostPassive,
        VoidBarnacle_VoidMortarPassive,
        VoidJailer_GravityPassive,
        ImpBoss_BleedPassive,
        StoneTitan_StoneSkinPassive,
        MagmaWorm_BlazingAuraPassive,
        OverloadingWorm_LightningAuraPassive,
        Vagrant_OrbPassive,
        Acrid_PoisonPassive,
        Commando_DoubleTapPassive,
        Captain_MicrobotsPassive,
        Loader_ScrapBarrierPassive,

        // ============================
        // Level 2 passives
        // ============================
        BeetleQueen_Scavenger_GachaPassive,
        Pest_Vermin_BlindSensesPassive,
        RoboBallMini_Commando_DoubleTimePassive,
        Grandparent_Artificer_ElementalFusionPassive,
        LesserWisp_Beetle_OmniboostPassive,
        ClayDunestrider_MiniMushrum_IngrainPassive,
        Bell_Gup_BarbedSpikesPassive,
        Acrid_Larva_AuraOfBlightPassive,
        HermitCrab_Titan_StoneFormPassive,
        Vagrant_ClayTemplar_BigBangPassive,
        Wisp_Grovetender_WisperPassive,

        // ============================
        // Level 4 passives
        // ============================
        ShiggyDecay_AuraOfBlight_DecayAwakenedPassive,
        Jailer_Grandparent_ElementalFusion_WeatherReportPassive,
        Ingrain_StoneForm_GargoyleProtectionPassive,
        BlackholeGlaive_MechStance_MachineFormPassive,
        BarrierJelly_BlindSenses_ReversalPassive,
        BigBang_Wisper_SupernovaPassive,

        // ============================
        // Level 1 actives
        // ============================
        Vulture_WindBlastActive,
        BeetleGuard_SlamActive,
        Bison_ChargeActive,
        Bell_SpikedBallActive,
        ClayApothecary_MortarActive,
        ClayTemplar_MinigunActive,
        ElderLemurian_FireBlastActive,
        GreaterWisp_SpiritBoostActive,
        Imp_BlinkActive,
        Jellyfish_HealActive,
        Lemurian_FireballActive,
        LunarGolem_SlideResetActive,
        LunarWisp_MinigunActive,
        Parent_TeleportActive,
        StoneGolem_LaserActive,
        VoidReaver_PortalActive,
        BeetleQueen_SummonActive,
        Grandparent_SunActive,
        Grovetender_ChainActive,
        ClayDunestrider_TarBoostActive,
        SolusControlUnit_KnockupActive,
        XIConstruct_BeamActive,
        VoidDevastator_MissilesActive,
        Scavenger_ThqwibActive,
        Artificer_FlamethrowerActive,
        Artificer_IceWallActive,
        Artificer_LightningOrbActive,
        Bandit_LightsOutActive,
        Engineer_TurretActive,
        Huntress_FlurryActive,
        Merc_DashActive,
        MULT_PowerStanceActive,
        MULT_PowerStanceCancelActive,
        Railgunner_CryoActive,
        REX_MortarActive,
        VoidFiend_CleanseActive,
        Deku_OFAActive,

        // ============================
        // Level 2 actives
        // ============================
        Railgunner_LunarWisp_RapidPierceActive,
        BulletLaser_StoneGolem_SweepingBeamActive,
        VoidDevastator_Huntress_BlackholeGlaiveActive,
        VoidJailer_SolusControlUnit_GravitationalDownforceActive,
        Vulture_Engineer_WindShieldActive,
        XIConstruct_ClayApothecary_GenesisActive,
        LunarExploder_LunarGolem_RefreshActive,
        Imp_MagmaWorm_ExpungeActive,
        Imp_Bandit_ShadowClawActive,
        Captain_VoidReaver_OrbitalStrikeActive,
        OverloadingWorm_Bison_ThunderclapActive,
        ElderLemurian_Lemurian_BlastBurnActive,
        AlphaConstruct_Jellyfish_BarrierJellyActive,
        BeetleGuard_MULT_MechStanceActive,
        AirCannon_Merc_WindSlashActive,
        Multiplier_Deku_LimitBreakActive,
        VoidBarnacle_VoidFiend_VoidFormActive,
        REX_Decay_DecayPlusUltraActive,
        Parent_Loader_MachPunchActive,

        // ============================
        // Level 4 actives
        // ============================
        ShadowClaw_Genesis_LightAndDarknessActive,
        Refresh_Gacha_WildCardActive,
        OrbitalStrike_BlastBurn_BlastingZoneActive,
        WindShield_WindSlash_FinalReleaseActive,
        RapidPierce_SweepingBeam_XBeamerActive,
        VoidForm_LimitBreak_OFAFOActive,
        BarbedSpikes_Expunge_DeathAuraActive,
        MachPunch_Thunderclap_ExtremeSpeedActive,
        DoubleTime_Omniboost_TheWorldActive,
    }


}
