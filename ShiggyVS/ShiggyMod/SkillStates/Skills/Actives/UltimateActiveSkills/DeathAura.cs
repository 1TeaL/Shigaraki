using R2API.Networking;
using RoR2;
using ShiggyMod.Modules;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class DeathAura : Skill
    {
        //Barbed spikes + Expunge
        private string muzzleString = "RHand";
        private GameObject blastEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/SonicBoomEffect");

        public override void OnEnter()
        {
            base.OnEnter();
            //play animation and maybe particles?

            Ray aimRay = base.GetAimRay();
            EffectManager.SpawnEffect(ShiggyAsset.impBossExplosionEffect, new EffectData
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
            if (!characterBody.HasBuff(Buffs.deathAuraBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.deathAuraBuff.buffIndex, 1);
                base.PlayAnimation("FullBody, Override", "BothHandLift", "Attack.playbackRate", duration);
            }
            else
            if (characterBody.HasBuff(Buffs.deathAuraBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.deathAuraBuff.buffIndex, 0);
                base.PlayAnimation("FullBody, Override", "BothHandDown", "Attack.playbackRate", duration);
            }


        }
        public override void OnExit()
        {
            base.OnExit();
        }

    }
}