using DittoMod.SkillStates;
using System.Collections.Generic;
using System;

namespace DittoMod.Modules
{
    public static class States
    {
        internal static List<Type> entityStates = new List<Type>();

        internal static void RegisterStates()
        {
            entityStates.Add(typeof(Transform));
            entityStates.Add(typeof(Struggle));
            entityStates.Add(typeof(AssaultVest));
            entityStates.Add(typeof(ChoiceBand));
            entityStates.Add(typeof(ChoiceScarf));
            entityStates.Add(typeof(ChoiceSpecs));
            entityStates.Add(typeof(Leftovers));
            entityStates.Add(typeof(LifeOrb));
            entityStates.Add(typeof(LuckyEgg));
            entityStates.Add(typeof(RockyHelmet));
            entityStates.Add(typeof(ScopeLens));
            entityStates.Add(typeof(ShellBell));
        }
    }
}