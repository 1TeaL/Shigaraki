using EntityStates;
using EntityStates.ClayBoss;
using EntityStates.ClayBoss.ClayBossWeapon;
using R2API.Networking;
using RoR2;
using RoR2.Projectile;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class ClayDunestriderBuff : Skill
    {
        public static float baseTimeBetweenShots = 0.5f;
        public static float recoilAmplitude = 1f;

        private float damageCoefficient = Modules.StaticValues.claydunestriderDamageCoefficient;
        private float force = 1f;
        private float speedOverride = -1f;
        private Transform modelTransform;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration;
            Ray aimRay = base.GetAimRay();

            this.modelTransform = base.GetModelTransform();

            Util.PlaySound(FireTarball.attackSoundString, base.gameObject);
            if (NetworkServer.active)
            {
                //int buffcount = characterBody.GetBuffCount(Modules.Buffs.claydunestriderBuff);
                characterBody.ApplyBuff(Modules.Buffs.claydunestriderBuff.buffIndex, 1 ,Modules.StaticValues.claydunestriderbuffDuration);
                //characterBody.AddTimedBuffAuthority(Modules.Buffs.claydunestriderBuff.buffIndex, Modules.StaticValues.claydunestriderbuffDuration);
            }

            EffectManager.SpawnEffect(Modules.ShiggyAsset.claydunestriderEffect, new EffectData
            {
                origin = base.characterBody.corePosition,
                scale = 1f,
                rotation = Quaternion.LookRotation(-base.characterDirection.forward)

            }, false);
            //if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            base.PlayCrossfade("LeftArm, Override", "LHandFist", "Attack.playbackRate", duration, 0.05f);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);

        }

        public override void OnExit()
        {
            base.OnExit();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();


            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
