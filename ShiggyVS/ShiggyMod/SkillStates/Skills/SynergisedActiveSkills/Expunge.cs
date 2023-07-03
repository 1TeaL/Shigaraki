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

namespace ShiggyMod.SkillStates
{
    public class Expunge : BaseSkillState
    {
        private BlastAttack blastAttack;
        public float fireTime;
        public bool hasFired = false;
        public float radius;
        public float baseDuration = 1f;
        public float duration;

        //Imp boss + Magmaworm
        public override void OnEnter()
        {
            base.OnEnter();
            duration = baseDuration / attackSpeedStat;
            fireTime = duration / 2f;
            radius = StaticValues.expungeRadius * attackSpeedStat;
            if(radius < StaticValues.expungeRadius)
            {
                radius = StaticValues.expungeRadius;
            }

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
            base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }

            Ray aimRay = base.GetAimRay();


            blastAttack = new BlastAttack();
            blastAttack.radius = radius;
            blastAttack.procCoefficient = StaticValues.expungeProcCoefficient;
            blastAttack.position = aimRay.origin + aimRay.direction * radius;
            blastAttack.attacker = base.gameObject;
            blastAttack.crit = characterBody.RollCrit();
            blastAttack.baseDamage = damageStat* Modules.StaticValues.expungeDamageCoefficient;
            blastAttack.falloffModel = BlastAttack.FalloffModel.None;
            blastAttack.baseForce = 400f;
            blastAttack.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
            blastAttack.damageType |= DamageType.BypassArmor;
            blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;


        }

        protected virtual void OnHitEnemyAuthority()
        {
            AkSoundEngine.PostEvent("ShiggyStrongAttack", base.gameObject);

        }

        public override void OnExit()
        {
            base.OnExit();

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if(base.fixedAge > fireTime && !hasFired)
            {
                hasFired = true;
                Ray aimRay = base.GetAimRay();
                EffectManager.SpawnEffect(Assets.impBossExplosionEffect, new EffectData
                {
                    origin = aimRay.origin + aimRay.direction * radius,
                    scale = radius,
                    rotation = Quaternion.identity,

                }, true);

                blastAttack.position = aimRay.origin + aimRay.direction * radius;
                if (blastAttack.Fire().hitCount > 0)
                {
                    this.OnHitEnemyAuthority();
                }
            }

            if(base.fixedAge > duration)
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