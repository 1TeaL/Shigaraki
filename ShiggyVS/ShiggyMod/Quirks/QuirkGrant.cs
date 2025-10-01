// QuirkGrant.cs
using R2API.Networking;
using RoR2;
using ShiggyMod.Modules.Survivors;
using UnityEngine;
using UnityEngine.Networking;
using static ShiggyMod.Modules.Quirks.QuirkRegistry;

namespace ShiggyMod.Modules.Quirks
{
    public static class QuirkGrant
    {
        public static float MaxGrantDistance = 30f;

        public static bool ApplyPassiveByIdServer(CharacterBody target, QuirkId id)
        {
            if (!NetworkServer.active || !target) return false;
            if (!QuirkRegistry.TryGet(id, out var rec)) return false;

            // Prefer Buff (your registry maps passives to BuffDefs)
            if (rec.Buff != null)
            {
                if (!target.HasBuff(rec.Buff))
                {
                    if (!target.GetComponent<BuffController>())
                        target.gameObject.AddComponent<BuffController>();
                    target.ApplyBuff(rec.Buff.buffIndex, 1);
                }
                return true;
            }

            // Optional: handle passives modeled as SkillDefs
            // (no-op by default)
            return rec.Skill != null;
        }

        public static bool ValidateGiverAndTarget(CharacterMaster giver, CharacterBody target)
        {
            if (!giver || !target) return false;
            var giverBody = giver.GetBody();
            if (!giverBody) return false;

            var gTeam = giverBody.teamComponent ? giverBody.teamComponent.teamIndex : TeamIndex.None;
            var tTeam = target.teamComponent ? target.teamComponent.teamIndex : TeamIndex.None;
            if (gTeam != tTeam) return false;

            // Range
            if ((target.corePosition - giverBody.corePosition).sqrMagnitude >
                MaxGrantDistance * MaxGrantDistance) return false;

            return true;
        }
    }
}
