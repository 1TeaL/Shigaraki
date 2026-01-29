using R2API.Networking;
using RoR2;
using ShiggyMod.Modules;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class TheWorld : Skill
    {
        //double time + omniboost
        private string muzzleString = "RHand";
        private GameObject blastEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/SonicBoomEffect");

        public override void OnEnter()
        {
            base.OnEnter();
            duration = baseDuration;
            //play animation and maybe particles?

            Ray aimRay = base.GetAimRay();
            EffectManager.SpawnEffect(blastEffectPrefab, new EffectData
            {
                origin = base.characterBody.corePosition,
                scale = 1f,
                rotation = Quaternion.LookRotation(aimRay.direction),

            }, true);
            characterBody.ApplyBuff(RoR2Content.Buffs.HiddenInvincibility.buffIndex, 1, 1);

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            this.animator.SetBool("attacking", true);
            int randomAnim = UnityEngine.Random.RandomRangeInt(1, 3);
            if (randomAnim == 1)
            {
                base.PlayCrossfade("FullBody, Override", "FullBodyTheWorldCrossArm", "Attack.playbackRate", duration, 0.05f);
            }
            if (randomAnim == 2)
            {
                base.PlayCrossfade("FullBody, Override", "FullBodyTheWorldPoint", "Attack.playbackRate", duration, 0.05f);
            }
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            AkSoundEngine.PostEvent("ShiggyTheWorld", base.gameObject);

        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.fixedAge > duration)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }
        public override void OnExit()
        {
            base.OnExit();
            this.animator.SetBool("attacking", false);
            Ray aimRay = base.GetAimRay();
            EffectManager.SpawnEffect(blastEffectPrefab, new EffectData
            {
                origin = base.characterBody.corePosition,
                scale = 1f,
                rotation = Quaternion.LookRotation(aimRay.direction),

            }, true);
            EffectManager.SimpleMuzzleFlash(Modules.ShiggyAsset.commandoOmniExplosionVFXEffect, base.gameObject, this.muzzleString, false);
            //EffectManager.SpawnEffect(Asset.commandoOmniExplosionVFXEffect, new EffectData
            //{
            //    origin = muzz,
            //    scale = 1f,
            //    rotation = Quaternion.identity,

            //}, true);
            if (!characterBody.HasBuff(Buffs.theWorldBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.theWorldBuff.buffIndex, 1);
            }
            else
            if (characterBody.HasBuff(Buffs.theWorldBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.theWorldBuff.buffIndex, 0);
            }
        }

    }
}