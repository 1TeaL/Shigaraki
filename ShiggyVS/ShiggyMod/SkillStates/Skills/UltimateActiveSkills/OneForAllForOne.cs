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
using System;
using static UnityEngine.UI.Image;

namespace ShiggyMod.SkillStates
{
    public class OneForAllForOne : Skill
    {
        //void form + limit break
        public float baseDuration = 0.5f;
        public float duration;
        private string muzzleString = "RHand";
        private GameObject blastEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/SonicBoomEffect");

        public override void OnEnter()
        {
            base.OnEnter();
            duration = baseDuration;
            //play differnt animation and maybe particles?

            Ray aimRay = base.GetAimRay();
            EffectManager.SpawnEffect(Assets.voidMegaCrabExplosionEffect, new EffectData
            {
                origin = FindModelChild(muzzleString).position,
                scale = 1f,
                rotation = Quaternion.LookRotation(aimRay.direction),

            }, true);
            //EffectManager.SpawnEffect(Assets.commandoOmniExplosionVFXEffect, new EffectData
            //{
            //    origin = muzz,
            //    scale = 1f,
            //    rotation = Quaternion.identity,

            //}, true);
            if (!characterBody.HasBuff(Buffs.OFAFOBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.OFAFOBuff.buffIndex, 1);
            }
            else
            if (characterBody.HasBuff(Buffs.OFAFOBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.OFAFOBuff.buffIndex, 0);
            }

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            base.PlayAnimation("FullBody, Override", "FullBodyUnleash", "Attack.playbackRate", duration);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            AkSoundEngine.PostEvent("ShiggyOFA", base.gameObject);

        }
        public override void OnExit()
        {
            base.OnExit();
        }

    }
}