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
using R2API;
using ShiggyMod.Modules.Networking;
using R2API.Networking.Interfaces;
using static RoR2.BlastAttack;

namespace ShiggyMod.SkillStates
{
    public class DecayPlusUltra : BaseSkillState
    {
        private BlastAttack blastAttack;
        public float fireTime;
        public bool hasFired = false;
        public float radius = StaticValues.decayPlusUltraRadius;
        public float baseDuration = StaticValues.decayPlusUltraDuration;
        public float duration;

        //Rex + decay
        public override void OnEnter()
        {
            base.OnEnter();
            duration = baseDuration / attackSpeedStat;
            fireTime = duration * 0.7f;
            //play animation

            Ray aimRay = base.GetAimRay();


            blastAttack = new BlastAttack();
            blastAttack.radius = radius;
            blastAttack.procCoefficient = StaticValues.decayPlusUltraProcCoefficient;
            blastAttack.position = characterBody.footPosition;
            blastAttack.attacker = base.gameObject;
            blastAttack.crit = characterBody.RollCrit();
            blastAttack.baseDamage = damageStat* Modules.StaticValues.decayPlusUltraDamageCoefficient;
            blastAttack.falloffModel = BlastAttack.FalloffModel.None;
            blastAttack.baseForce = StaticValues.decayPlusUltraForce;
            blastAttack.bonusForce = new Vector3(0, StaticValues.decayPlusUltraForce, 0);
            blastAttack.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
            blastAttack.damageType = DamageType.Generic;
            blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;

            DamageAPI.AddModdedDamageType(blastAttack, Damage.shiggyDecay);

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            base.PlayAnimation("FullBody, Override", "FullBodySlam", "Attack.playbackRate", duration);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
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
                EffectManager.SpawnEffect(Assets.decayattackEffect, new EffectData
                {
                    origin = characterBody.footPosition,
                    scale = 1f,
                    rotation = Util.QuaternionSafeLookRotation(Vector3.up),

                }, true);
                EffectManager.SpawnEffect(EntityStates.BeetleGuardMonster.GroundSlam.slamEffectPrefab, new EffectData
                {
                    origin = characterBody.corePosition,
                    scale = radius,
                    rotation = Quaternion.identity,

                }, true);
                EffectManager.SpawnEffect(EntityStates.JellyfishMonster.JellyNova.novaEffectPrefab, new EffectData
                {
                    origin = characterBody.corePosition,
                    scale = radius,
                    rotation = Quaternion.identity,

                }, true);

                new SpendHealthNetworkRequest(characterBody.masterObjectId, characterBody.healthComponent.combinedHealth * StaticValues.decayPlusUltraHealthCostCoefficient).Send(NetworkDestination.Clients);
                BlastAttack.Result result = blastAttack.Fire();
                if (result.hitCount > 0)
                {
                    this.OnHitEnemyAuthority(result);
                }
                //play animation and more particles
            }

            if(base.fixedAge > duration)
            {
                this.outer.SetNextStateToMain();
                return;
            }

        }


        protected virtual void OnHitEnemyAuthority(BlastAttack.Result result)
        {
            foreach (BlastAttack.HitPoint hitpoint in result.hitPoints)
            {
                //play effect

                EffectManager.SpawnEffect(Assets.shiggyHitImpactEffect, new EffectData
                {
                    origin = hitpoint.hurtBox.transform.position,
                    scale = 1f,
                    rotation = Quaternion.identity,

                }, true);

            }

        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}