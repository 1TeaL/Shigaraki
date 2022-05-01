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

            entityStates.Add(typeof(BisonCharge));
            entityStates.Add(typeof(BronzongBall));
            entityStates.Add(typeof(BeetleGuardSlam));
            entityStates.Add(typeof(ClayApothecaryMortar));
            entityStates.Add(typeof(ClayTemplarMinigun));
            entityStates.Add(typeof(LemurianFireball));
            entityStates.Add(typeof(VultureFly));
        }
    }
}