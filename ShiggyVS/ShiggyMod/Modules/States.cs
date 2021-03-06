using ShiggyMod.SkillStates;
using System.Collections.Generic;
using System;

namespace ShiggyMod.Modules
{
    public static class States
    {
        internal static List<Type> entityStates = new List<Type>();

        internal static void RegisterStates()
        {
            entityStates.Add(typeof(AlphaConstruct));

            entityStates.Add(typeof(Decay));
            entityStates.Add(typeof(BulletLaser));
            entityStates.Add(typeof(AirCannon));
            entityStates.Add(typeof(Multiplier));

            entityStates.Add(typeof(AlloyVultureFly));
            entityStates.Add(typeof(BisonCharge));
            entityStates.Add(typeof(BronzongBall));
            entityStates.Add(typeof(BeetleGuardSlam));
            entityStates.Add(typeof(ClayApothecaryMortar));
            entityStates.Add(typeof(ClayTemplarMinigun));
            entityStates.Add(typeof(ElderLemurianFireBlastCharge));
            entityStates.Add(typeof(ElderLemurianFireBlastFire));
            entityStates.Add(typeof(GreaterWispBuff));
            entityStates.Add(typeof(ImpBlink));
            entityStates.Add(typeof(JellyfishNova));
            entityStates.Add(typeof(LemurianFireball));
            entityStates.Add(typeof(LunarGolemSlide));
            entityStates.Add(typeof(LunarWispMinigun));
            entityStates.Add(typeof(ParentTeleport));
            entityStates.Add(typeof(StoneGolemLaserCharge));
            entityStates.Add(typeof(StoneGolemLaserFire));
            entityStates.Add(typeof(VoidReaverPortal));

            entityStates.Add(typeof(BeetleQueenShotgun));
            entityStates.Add(typeof(ClayDunestriderBuff));
            entityStates.Add(typeof(GrandparentSun));
            entityStates.Add(typeof(GrovetenderHook));
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
        }
    }
}