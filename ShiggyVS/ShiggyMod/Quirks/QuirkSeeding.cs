using RoR2;
using UnityEngine.Networking;

namespace ShiggyMod.Modules.Quirks
{
    public static class QuirkSeeding
    {
        private static bool _hooked;

        public static void Init()
        {
            if (_hooked) return;
            _hooked = true;

            On.RoR2.Run.Start += Run_Start;
        }

        private static void Run_Start(On.RoR2.Run.orig_Start orig, Run self)
        {
            orig(self);

            if (!NetworkServer.active) return;

            // Seed everyone currently in the lobby/run (players only)
            foreach (var pcmc in PlayerCharacterMasterController.instances)
            {
                var master = pcmc ? pcmc.master : null;
                if (!master) continue;

                var inv = QuirkInventory.Ensure(master);
                if (!inv) continue;

                inv.Server_SeedStartingQuirksFromConfig();
            }
        }
    }
}
