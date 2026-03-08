using R2API.Networking;
using RoR2;
using RoR2BepInExPack;
using ShiggyMod.Modules;
using System;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class OverclockAscension : Skill
    {
        //double time + blind senses + hyper regeneration
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

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            this.animator.SetBool("attacking", true);
            base.PlayAnimation("FullBody, Override", "FullBodyUnleash", "Attack.playbackRate", duration);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            EffectData effectData2 = new EffectData
            {
                origin = characterBody.corePosition
            };
            effectData2.SetNetworkedObjectReference(base.gameObject);
            EffectManager.SpawnEffect(HealthComponent.AssetReferences.fragileDamageBonusBreakEffectPrefab, effectData2, true);

        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            EffectManager.SpawnEffect(blastEffectPrefab, new EffectData
            {
                origin = base.characterBody.corePosition,
                scale = 1f,

            }, true);
            EffectData effectData2 = new EffectData
            {
                origin = base.characterBody.corePosition
            };
            effectData2.SetNetworkedObjectReference(base.gameObject);
            EffectManager.SpawnEffect(HealthComponent.AssetReferences.fragileDamageBonusBreakEffectPrefab, effectData2, true);
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
            if (!characterBody.HasBuff(Buffs.overclockAscensionBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.overclockAscensionBuff.buffIndex, 1);
            }
            else
            if (characterBody.HasBuff(Buffs.overclockAscensionBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.overclockAscensionBuff.buffIndex, 0);
            }
        }

    }
}