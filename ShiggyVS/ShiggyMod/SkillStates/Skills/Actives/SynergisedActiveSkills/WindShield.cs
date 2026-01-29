using R2API.Networking;
using RoR2;
using ShiggyMod.Modules;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class WindShield : Skill
    {
        //Alloy vulture + Engineer
        public override void OnEnter()
        {
            base.OnEnter();

            Ray aimRay = base.GetAimRay();
            EffectManager.SpawnEffect(EntityStates.Vulture.Fly.jumpEffectPrefab, new EffectData
            {
                origin = base.transform.position,
                scale = 1f,
                rotation = Quaternion.LookRotation(aimRay.direction),

            }, true);

            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Buffs.windShieldBuff.buffIndex, 1, StaticValues.windShieldDuration);
            }
        }

    }
}