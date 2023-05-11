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
    public class LimitBreak : Skill
    {
        //Deku one for all + multiplier
        public override void OnEnter()
        {
            base.OnEnter();
            duration = 1f;
            Ray aimRay = base.GetAimRay();

            //play animation, also need to play particles in controller
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            base.PlayAnimation("FullBody, Override", "FullBodyUnleash", "Attack.playbackRate", duration);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            AkSoundEngine.PostEvent("ShiggyLimitBreak", base.gameObject);
            EffectManager.SpawnEffect(Assets.multCryoExplosionEffect, new EffectData
            {
                origin = base.transform.position,
                scale = 1f,
                rotation = Quaternion.LookRotation(aimRay.direction),

            }, true);

            if (!characterBody.HasBuff(Buffs.limitBreakBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.limitBreakBuff.buffIndex, 1);
            }
            else
            if (characterBody.HasBuff(Buffs.limitBreakBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.limitBreakBuff.buffIndex, 0);
            }
        }

    }
}