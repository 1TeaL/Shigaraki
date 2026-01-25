using ShiggyMod.Modules.Survivors;
using EntityStates;
using RoR2;
using UnityEngine;
using System.Collections.Generic;
using ShiggyMod.Modules;
using UnityEngine.Networking;
using RoR2.ExpansionManagement;
using ExtraSkillSlots;
using R2API.Networking;

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