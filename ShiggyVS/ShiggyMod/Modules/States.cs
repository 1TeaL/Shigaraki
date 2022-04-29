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
            entityStates.Add(typeof(AirCannon));

            entityStates.Add(typeof(LemurianFireball));

            entityStates.Add(typeof(VultureFly));
            entityStates.Add(typeof(VultureLand));
        }
    }
}