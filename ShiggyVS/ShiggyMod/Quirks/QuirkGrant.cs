// QuirkGrant.cs
// Server-authoritative helper for granting a passive (buff-backed) quirk to a target.
//
// Assumptions based on your current registry shape:
// - QuirkRecord has: Category, SkillDef, BuffDef
// - Passive quirks that should be "grantable" are modeled as BuffDef-backed passives.
//
// IMPORTANT DESIGN NOTE:
// - This grants a BUFF to the target body; it does NOT modify their equipped loadout.
// - Do NOT touch QuirkInventory here (yours is a static/global inventory, not per-player).

using R2API.Networking;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.Modules.Quirks
{
    public static class QuirkGrant
    {
        public static float MaxGrantDistance = 30f;

        /// <summary>
        /// Grants a passive quirk by ID to the target BODY on the server.
        /// Returns true if a meaningful grant occurred (or was already present).
        /// </summary>
        public static bool ApplyPassiveByIdServer(CharacterBody target, QuirkId id)
        {
            if (!NetworkServer.active) return false;
            if (!target) return false;
            if (id == QuirkId.None) return false;

            if (!QuirkRegistry.TryGet(id, out var rec)) return false;

            // Only allow granting passives
            if (rec.Category != QuirkCategory.Passive) return false;

            // Prefer BuffDef-backed passives (this is your main model)
            if (rec.BuffDef != null)
            {
                // Idempotent: ensure buff count is exactly 1
                // (Use SetBuffCount to avoid stacking or repeated adds.)
                target.ApplyBuff(rec.BuffDef.buffIndex, 1);
                return true;
            }

            // Optional: if you ever model a passive as a SkillDef-only thing,
            // you need separate logic (equipment override, inventory, etc.).
            // For now, treat as not grantable.
            return false;
        }

        /// <summary>
        /// Validates giver (master) and target (body) are:
        /// - both valid/alive
        /// - same team
        /// - within MaxGrantDistance
        /// </summary>
        public static bool ValidateGiverAndTarget(CharacterMaster giver, CharacterBody target)
        {
            if (!giver || !target) return false;

            var giverBody = giver.GetBody();
            if (!giverBody) return false;

            var gTeam = giverBody.teamComponent ? giverBody.teamComponent.teamIndex : TeamIndex.None;
            var tTeam = target.teamComponent ? target.teamComponent.teamIndex : TeamIndex.None;
            if (gTeam == TeamIndex.None || tTeam == TeamIndex.None) return false;
            if (gTeam != tTeam) return false;

            float maxSqr = MaxGrantDistance * MaxGrantDistance;
            if ((target.corePosition - giverBody.corePosition).sqrMagnitude > maxSqr)
                return false;

            return true;
        }
    }
}
