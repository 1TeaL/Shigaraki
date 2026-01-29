using R2API.Networking;
using ShiggyMod.Modules;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class DekuOFA : Skill
    {
        //one for all
        public override void OnEnter()
        {
            base.OnEnter();

            PlayCrossfade("FullBody, Override", "FullBodyUnleash", "Attack.playbackRate", duration, 0.1f);
            Ray aimRay = base.GetAimRay();

            //play animation, also need to play particles in controller
            //EffectManager.SpawnEffect(Assets.multCryoExplosionEffect, new EffectData
            //{
            //    origin = base.transform.position,
            //    scale = 1f,
            //    rotation = Quaternion.LookRotation(aimRay.direction),

            //}, true);

            if (!characterBody.HasBuff(Buffs.OFABuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.OFABuff.buffIndex, 1);
            }
            else
            if (characterBody.HasBuff(Buffs.OFABuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.OFABuff.buffIndex, 0);
            }
        }

    }
}