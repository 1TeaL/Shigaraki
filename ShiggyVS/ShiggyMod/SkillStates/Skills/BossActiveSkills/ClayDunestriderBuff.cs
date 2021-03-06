using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using EntityStates.ClayBoss;
using RoR2.Projectile;
using EntityStates.ClayBoss.ClayBossWeapon;

namespace ShiggyMod.SkillStates
{
    public class ClayDunestriderBuff : BaseSkillState
    {
        public float baseDuration = 0.1f;
        public float duration;
        public ShiggyController Shiggycon;
        public static float baseTimeBetweenShots = 0.5f;
        public static float recoilAmplitude = 1f;

        private float damageCoefficient = Modules.StaticValues.claydunestriderDamageCoeffecient;
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
                int buffcount = characterBody.GetBuffCount(Modules.Buffs.claydunestriderBuff);
                characterBody.SetBuffCount(Modules.Buffs.claydunestriderBuff.buffIndex, Modules.StaticValues.claydunestriderbuffDuration + buffcount);
                //characterBody.AddBuff(Modules.Buffs.claydunestriderBuff);
            }

            EffectManager.SpawnEffect(Modules.Assets.claydunestriderEffect, new EffectData
            {
                origin = base.characterBody.corePosition,
                scale = 1f,
                rotation = Quaternion.LookRotation(-base.characterDirection.forward)

            }, false);
            //AkSoundEngine.PostEvent(3660048432, base.gameObject);

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
