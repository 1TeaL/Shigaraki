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
            entityStates.Add(typeof(AFOPrimary));

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
            entityStates.Add(typeof(GreaterWispBallFire));
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
        }
    }
}