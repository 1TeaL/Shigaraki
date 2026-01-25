// QuirkPassiveSync.cs
using R2API.Networking;
using RoR2;
using UnityEngine.Networking;

namespace ShiggyMod.Modules.Quirks
{
    public static class QuirkPassiveSync
    {
        /// <summary>
        /// Server authoritative: derives passive buffs from currently equipped quirks in skill slots.
        /// Does NOT call RecalculateStats(); caller should do that if needed.
        /// </summary>
        [Server]
        public static void SyncFromEquippedSkillsServer(CharacterBody body)
        {
            if (!NetworkServer.active || body == null) return;

            var equipped = QuirkEquipUtils.GetEquippedQuirks(body);

            foreach (var rec in QuirkRegistry.All.Values)
            {
                if (rec.Category != QuirkCategory.Passive) continue;
                if (rec.BuffDef == null) continue;

                int desired = equipped.Contains(rec.Id) ? 1 : 0;

                // Only write if it changed
                int current = body.GetBuffCount(rec.BuffDef);
                if (current != desired)
                    body.ApplyBuff(rec.BuffDef.buffIndex, desired);
            }
        }
    }
}
