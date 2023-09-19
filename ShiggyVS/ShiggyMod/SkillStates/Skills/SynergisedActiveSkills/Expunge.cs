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
using ShiggyMod.Modules.Networking;
using R2API.Networking.Interfaces;

namespace ShiggyMod.SkillStates
{
    public class Expunge : BaseSkillState
    {
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
                    origin = aimRay.origin,
                    scale = radius,
                    rotation = Quaternion.identity,

                }, true);

                new ExpungeNetworkRequest(characterBody.master.netId, Vector3.up, radius, characterBody.damage * StaticValues.expungeDamageCoefficient).Send(NetworkDestination.Clients);

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