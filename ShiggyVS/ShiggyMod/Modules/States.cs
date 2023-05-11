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
            entityStates.Add(typeof(BaseMeleeAttack));


            //base skills
            entityStates.Add(typeof(Decay));
            entityStates.Add(typeof(BulletLaser));
            entityStates.Add(typeof(AirCannon));
            entityStates.Add(typeof(Multiplier));
            entityStates.Add(typeof(ChooseSkill));
            entityStates.Add(typeof(RemoveSkill));
            entityStates.Add(typeof(EmptySkill));
            entityStates.Add(typeof(GiveSkill));

            //active skills
            entityStates.Add(typeof(AlloyVultureWindBlast));
            entityStates.Add(typeof(BisonCharge));
            entityStates.Add(typeof(BronzongBall));
            entityStates.Add(typeof(BeetleGuardSlam));
            entityStates.Add(typeof(ClayApothecaryMortar));
            entityStates.Add(typeof(ClayTemplarMinigun));
            entityStates.Add(typeof(ElderLemurianFireBlastCharge));
            entityStates.Add(typeof(ElderLemurianFireBlastFire));
            entityStates.Add(typeof(GreaterWispBuff));
            entityStates.Add(typeof(ImpBlink));
            entityStates.Add(typeof(JellyfishHeal));
            entityStates.Add(typeof(LemurianFireball));
            entityStates.Add(typeof(LunarGolemSlide));
            entityStates.Add(typeof(LunarWispMinigun));
            entityStates.Add(typeof(ParentTeleport));
            entityStates.Add(typeof(StoneGolemLaserCharge));
            entityStates.Add(typeof(StoneGolemLaserFire));
            entityStates.Add(typeof(VoidReaverPortal));

            entityStates.Add(typeof(BeetleQueenSummon));
            entityStates.Add(typeof(ClayDunestriderBuff));
            entityStates.Add(typeof(GrandparentSun));
            entityStates.Add(typeof(GrovetenderChain));
            entityStates.Add(typeof(SolusControlUnitKnockup));
            entityStates.Add(typeof(XiConstructBeam));
            entityStates.Add(typeof(VoidDevastatorHoming));
            entityStates.Add(typeof(ScavengerThqwibs));

            entityStates.Add(typeof(ArtificerFlamethrower));
            entityStates.Add(typeof(ArtificerIceWall));
            entityStates.Add(typeof(ArtificerChargeLightningOrb));
            entityStates.Add(typeof(ArtificerThrowLightningOrb));
            entityStates.Add(typeof(BanditPrepLightsOut));
            entityStates.Add(typeof(BanditFireLightsOut));
            entityStates.Add(typeof(EngiTurret));
            entityStates.Add(typeof(HuntressBlink));
            entityStates.Add(typeof(HuntressAttack));
            entityStates.Add(typeof(MercDash));
            entityStates.Add(typeof(MercDashAttack));
            entityStates.Add(typeof(MultBuff));
            entityStates.Add(typeof(RailgunnerCryoCharge));
            entityStates.Add(typeof(RailgunnerCryoFire));
            entityStates.Add(typeof(RexMortar));
            entityStates.Add(typeof(VoidFiendCleanse));
            //collab skills
            entityStates.Add(typeof(DekuOFA));

            //passive skills
            entityStates.Add(typeof(AlphaConstruct));
            entityStates.Add(typeof(Beetle));
            entityStates.Add(typeof(BlindPest));
            entityStates.Add(typeof(BlindVermin));
            entityStates.Add(typeof(Gup));
            entityStates.Add(typeof(HermitCrab));
            entityStates.Add(typeof(ScavengerThqwibs));
            entityStates.Add(typeof(Larva));
            entityStates.Add(typeof(LesserWisp));
            entityStates.Add(typeof(LunarExploder));
            entityStates.Add(typeof(MiniMushrum));
            entityStates.Add(typeof(RoboBallMini));
            entityStates.Add(typeof(VoidBarnacle));
            entityStates.Add(typeof(VoidJailer));
            
            entityStates.Add(typeof(ImpBoss));
            entityStates.Add(typeof(MagmaWorm));
            entityStates.Add(typeof(OverloadingWorm));
            entityStates.Add(typeof(StoneTitan));
            entityStates.Add(typeof(Vagrant));

            entityStates.Add(typeof(Acrid));
            entityStates.Add(typeof(Captain));
            entityStates.Add(typeof(Commando));
            entityStates.Add(typeof(Loader));

            //synergy active skills
            entityStates.Add(typeof(SweepingBeam));
            entityStates.Add(typeof(BlackHoleGlaive));
            entityStates.Add(typeof(GravitationalDownforce));
            entityStates.Add(typeof(WindShield));
            entityStates.Add(typeof(Genesis));
            entityStates.Add(typeof(Refresh));
            entityStates.Add(typeof(Expunge));
            entityStates.Add(typeof(ShadowClaw));
            entityStates.Add(typeof(OrbitalStrike));
            entityStates.Add(typeof(Thunderclap));
            entityStates.Add(typeof(BlastBurn));
            entityStates.Add(typeof(BarrierJelly));
            entityStates.Add(typeof(MechStance));
            entityStates.Add(typeof(WindSlash));
            entityStates.Add(typeof(LimitBreak));
            entityStates.Add(typeof(VoidForm));
            entityStates.Add(typeof(MachPunch));
            entityStates.Add(typeof(MachPunchRelease));
            entityStates.Add(typeof(RapidPierce));
            entityStates.Add(typeof(TheWorld));
            entityStates.Add(typeof(TheWorldFreeze));
            entityStates.Add(typeof(ExtremeSpeed));
            entityStates.Add(typeof(DeathAura));
            entityStates.Add(typeof(OneForAllForOne));
            entityStates.Add(typeof(XBeamer));
            entityStates.Add(typeof(FinalRelease));
            entityStates.Add(typeof(FinalReleaseShunpo));
            entityStates.Add(typeof(FinalReleaseMugetsu));
            entityStates.Add(typeof(BlastingZone));
            entityStates.Add(typeof(BlastingZoneBurnComponent));
            entityStates.Add(typeof(WildCard));
            entityStates.Add(typeof(LightAndDarkness));

            //synergy passive skills
            entityStates.Add(typeof(BigBangPassive));
            entityStates.Add(typeof(WisperPassive));
            entityStates.Add(typeof(OmniboostPassive));
            entityStates.Add(typeof(GachaPassive));
            entityStates.Add(typeof(StoneFormPassive));
            entityStates.Add(typeof(AuraOfBlightPassive));
            entityStates.Add(typeof(BarbedSpikesPassive));
            entityStates.Add(typeof(IngrainPassive));
            entityStates.Add(typeof(ElementalFusionPassive));
            entityStates.Add(typeof(DoubleTimePassive));
            entityStates.Add(typeof(BlindSensesPassive));
            entityStates.Add(typeof(SupernovaPassive));
            entityStates.Add(typeof(ReversalPassive));
            entityStates.Add(typeof(WeatherReportPassive));
        }
    }
}