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
    public class BarrierJelly : Skill
    {
        //Jellyfish + alpha construct
        public override void OnEnter()
        {
            base.OnEnter();
            duration = 0.5f;

            Ray aimRay = base.GetAimRay();

            EffectManager.SpawnEffect(Assets.alphaConstructMuzzleFlashEffect, new EffectData
            {
                origin = base.transform.position,
                scale = 1f,
                rotation = Quaternion.LookRotation(aimRay.direction),

            }, true);

            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(RoR2Content.Buffs.HiddenInvincibility.buffIndex, 1, StaticValues.barrierJellyDuration);
            }
        }

    }
}