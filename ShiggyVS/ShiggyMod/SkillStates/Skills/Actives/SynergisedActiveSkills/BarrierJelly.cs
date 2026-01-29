using R2API.Networking;
using RoR2;
using ShiggyMod.Modules;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class BarrierJelly : Skill
    {
        //Jellyfish + alpha construct
        public override void OnEnter()
        {
            base.OnEnter();
            duration = 0.5f;

            int randomAnim = UnityEngine.Random.RandomRangeInt(1, 3);
            base.PlayCrossfade("LeftArm, Override", "LHandSnap" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            Ray aimRay = base.GetAimRay();

            EffectManager.SpawnEffect(ShiggyAsset.alphaConstructMuzzleFlashEffect, new EffectData
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