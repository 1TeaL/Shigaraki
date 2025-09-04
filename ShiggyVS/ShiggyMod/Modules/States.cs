using ShiggyMod.SkillStates;
using System.Collections.Generic;
using System;
using ShiggyMod.SkillStates.BaseStates;
using ShiggyMod.SkillStates;

namespace ShiggyMod.Modules
{
    public static class States
    {
        internal static List<Type> entityStates = new List<Type>();

        internal static void RegisterStates()
        {
            Modules.Content.AddEntityState(typeof(BaseMeleeAttack));


            //base skills
            Modules.Content.AddEntityState(typeof(Decay));
            Modules.Content.AddEntityState(typeof(BulletLaser));
            Modules.Content.AddEntityState(typeof(AirCannon));
            Modules.Content.AddEntityState(typeof(Multiplier));
            Modules.Content.AddEntityState(typeof(ChooseSkill));
            Modules.Content.AddEntityState(typeof(RemoveSkill));
            Modules.Content.AddEntityState(typeof(EmptySkill));
            Modules.Content.AddEntityState(typeof(GiveSkill));

            //active skills
            Modules.Content.AddEntityState(typeof(AlloyVultureWindBlast));
            Modules.Content.AddEntityState(typeof(BisonCharge));
            Modules.Content.AddEntityState(typeof(BronzongBall));
            Modules.Content.AddEntityState(typeof(BeetleGuardSlam));
            Modules.Content.AddEntityState(typeof(ClayApothecaryMortar));
            Modules.Content.AddEntityState(typeof(ClayTemplarMinigun));
            Modules.Content.AddEntityState(typeof(ElderLemurianFireBlastCharge));
            Modules.Content.AddEntityState(typeof(ElderLemurianFireBlastFire));
            Modules.Content.AddEntityState(typeof(GreaterWispBuff));
            Modules.Content.AddEntityState(typeof(ImpBlink));
            Modules.Content.AddEntityState(typeof(JellyfishHeal));
            Modules.Content.AddEntityState(typeof(LemurianFireball));
            Modules.Content.AddEntityState(typeof(LunarGolemSlide));
            Modules.Content.AddEntityState(typeof(LunarWispMinigun));
            Modules.Content.AddEntityState(typeof(ParentTeleport));
            Modules.Content.AddEntityState(typeof(StoneGolemLaserCharge));
            Modules.Content.AddEntityState(typeof(StoneGolemLaserFire));
            Modules.Content.AddEntityState(typeof(VoidReaverPortal));

            Modules.Content.AddEntityState(typeof(BeetleQueenSummon));
            Modules.Content.AddEntityState(typeof(ClayDunestriderBuff));
            Modules.Content.AddEntityState(typeof(GrandparentSun));
            Modules.Content.AddEntityState(typeof(GrovetenderChain));
            Modules.Content.AddEntityState(typeof(SolusControlUnitKnockup));
            Modules.Content.AddEntityState(typeof(XiConstructBeam));
            Modules.Content.AddEntityState(typeof(VoidDevastatorHoming));
            Modules.Content.AddEntityState(typeof(ScavengerThqwibs));

            Modules.Content.AddEntityState(typeof(ArtificerFlamethrower));
            Modules.Content.AddEntityState(typeof(ArtificerIceWall));
            Modules.Content.AddEntityState(typeof(ArtificerChargeLightningOrb));
            Modules.Content.AddEntityState(typeof(ArtificerThrowLightningOrb));
            Modules.Content.AddEntityState(typeof(BanditPrepLightsOut));
            Modules.Content.AddEntityState(typeof(BanditFireLightsOut));
            Modules.Content.AddEntityState(typeof(EngiTurret));
            Modules.Content.AddEntityState(typeof(HuntressBlink));
            Modules.Content.AddEntityState(typeof(HuntressAttack));
            Modules.Content.AddEntityState(typeof(MercDash));
            Modules.Content.AddEntityState(typeof(MercDashAttack));
            Modules.Content.AddEntityState(typeof(MultBuff));
            Modules.Content.AddEntityState(typeof(RailgunnerCryoCharge));
            Modules.Content.AddEntityState(typeof(RailgunnerCryoFire));
            Modules.Content.AddEntityState(typeof(RexMortar));
            Modules.Content.AddEntityState(typeof(VoidFiendCleanse));
            //collab skills
            Modules.Content.AddEntityState(typeof(DekuOFA));

            //passive skills
            Modules.Content.AddEntityState(typeof(AlphaConstruct));
            Modules.Content.AddEntityState(typeof(Beetle));
            Modules.Content.AddEntityState(typeof(BlindPest));
            Modules.Content.AddEntityState(typeof(BlindVermin));
            Modules.Content.AddEntityState(typeof(Gup));
            Modules.Content.AddEntityState(typeof(HermitCrab));
            Modules.Content.AddEntityState(typeof(ScavengerThqwibs));
            Modules.Content.AddEntityState(typeof(Larva));
            Modules.Content.AddEntityState(typeof(LesserWisp));
            Modules.Content.AddEntityState(typeof(LunarExploder));
            Modules.Content.AddEntityState(typeof(MiniMushrum));
            Modules.Content.AddEntityState(typeof(RoboBallMini));
            Modules.Content.AddEntityState(typeof(VoidBarnacle));
            Modules.Content.AddEntityState(typeof(VoidJailer));
            
            Modules.Content.AddEntityState(typeof(ImpBoss));
            Modules.Content.AddEntityState(typeof(MagmaWorm));
            Modules.Content.AddEntityState(typeof(OverloadingWorm));
            Modules.Content.AddEntityState(typeof(StoneTitan));
            Modules.Content.AddEntityState(typeof(Vagrant));

            Modules.Content.AddEntityState(typeof(Acrid));
            Modules.Content.AddEntityState(typeof(Captain));
            Modules.Content.AddEntityState(typeof(Commando));
            Modules.Content.AddEntityState(typeof(Loader));

            //synergy active skills
            Modules.Content.AddEntityState(typeof(SweepingBeam));
            Modules.Content.AddEntityState(typeof(BlackHoleGlaive));
            Modules.Content.AddEntityState(typeof(GravitationalDownforce));
            Modules.Content.AddEntityState(typeof(WindShield));
            Modules.Content.AddEntityState(typeof(Genesis));
            Modules.Content.AddEntityState(typeof(Refresh));
            Modules.Content.AddEntityState(typeof(Expunge));
            Modules.Content.AddEntityState(typeof(ShadowClaw));
            Modules.Content.AddEntityState(typeof(OrbitalStrike));
            Modules.Content.AddEntityState(typeof(Thunderclap));
            Modules.Content.AddEntityState(typeof(BlastBurn));
            Modules.Content.AddEntityState(typeof(BarrierJelly));
            Modules.Content.AddEntityState(typeof(MechStance));
            Modules.Content.AddEntityState(typeof(WindSlash));
            Modules.Content.AddEntityState(typeof(LimitBreak));
            Modules.Content.AddEntityState(typeof(VoidForm));
            Modules.Content.AddEntityState(typeof(MachPunch));
            Modules.Content.AddEntityState(typeof(MachPunchRelease));
            Modules.Content.AddEntityState(typeof(RapidPierce));
            Modules.Content.AddEntityState(typeof(TheWorld));
            Modules.Content.AddEntityState(typeof(TheWorldFreeze));
            Modules.Content.AddEntityState(typeof(ExtremeSpeed));
            Modules.Content.AddEntityState(typeof(DeathAura));
            Modules.Content.AddEntityState(typeof(OneForAllForOne));
            Modules.Content.AddEntityState(typeof(XBeamer));
            Modules.Content.AddEntityState(typeof(FinalRelease));
            Modules.Content.AddEntityState(typeof(FinalReleaseShunpo));
            Modules.Content.AddEntityState(typeof(FinalReleaseMugetsu));
            Modules.Content.AddEntityState(typeof(BlastingZone));
            Modules.Content.AddEntityState(typeof(BlastingZoneBurnComponent));
            Modules.Content.AddEntityState(typeof(WildCard));
            Modules.Content.AddEntityState(typeof(LightAndDarkness));

            //synergy passive skills
            Modules.Content.AddEntityState(typeof(BigBangPassive));
            Modules.Content.AddEntityState(typeof(WisperPassive));
            Modules.Content.AddEntityState(typeof(OmniboostPassive));
            Modules.Content.AddEntityState(typeof(GachaPassive));
            Modules.Content.AddEntityState(typeof(StoneFormPassive));
            Modules.Content.AddEntityState(typeof(AuraOfBlightPassive));
            Modules.Content.AddEntityState(typeof(BarbedSpikesPassive));
            Modules.Content.AddEntityState(typeof(IngrainPassive));
            Modules.Content.AddEntityState(typeof(ElementalFusionPassive));
            Modules.Content.AddEntityState(typeof(DoubleTimePassive));
            Modules.Content.AddEntityState(typeof(BlindSensesPassive));
            Modules.Content.AddEntityState(typeof(SupernovaPassive));
            Modules.Content.AddEntityState(typeof(ReversalPassive));
            Modules.Content.AddEntityState(typeof(WeatherReportPassive));
        }
    }
}