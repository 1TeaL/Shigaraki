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
using EntityStates.LemurianMonster;

namespace ShiggyMod.SkillStates
{
    public class BlastBurn : BaseSkillState
    {
        //Elder lemurian and lemurian
        private BlastAttack blastAttack;
        public float fireInterval;
        public float stopwatch;
        public float radius;
        private float damage;
        private string muzzleString = "RHand";

        private GameObject chargeVfxInstance;
        public override void OnEnter()
        {
            base.OnEnter();
            fireInterval = StaticValues.blastBurnBaseInterval / attackSpeedStat;
            radius = StaticValues.blastBurnStartRadius;

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
            //base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", fireInterval, 0.05f);
            base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", fireInterval, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }

            Ray aimRay = base.GetAimRay();
            damage = characterBody.damage * StaticValues.blastBurnDamageCoefficient;

            blastAttack = new BlastAttack();
            blastAttack.radius = radius;
            blastAttack.procCoefficient = StaticValues.blastBurnProcCoefficient;
            blastAttack.position = aimRay.origin;
            blastAttack.attacker = base.gameObject;
            blastAttack.crit = characterBody.RollCrit();
            blastAttack.baseDamage = damage;
            blastAttack.falloffModel = BlastAttack.FalloffModel.None;
            blastAttack.baseForce = 400f;
            blastAttack.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
            blastAttack.damageType = DamageType.IgniteOnHit;
            blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;

            //play anim
            if (transform && ChargeFireball.chargeVfxPrefab)
            {
                this.chargeVfxInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeFireball.chargeVfxPrefab, FindModelChild(this.muzzleString).position, Util.QuaternionSafeLookRotation(aimRay.direction));
                this.chargeVfxInstance.transform.parent = FindModelChild(this.muzzleString).transform;
            }

        }

        public override void OnExit()
        {
            base.OnExit();
            if (this.chargeVfxInstance)
            {
                EntityState.Destroy(this.chargeVfxInstance);
            }

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.IsKeyDownAuthority())
            {
                stopwatch += Time.fixedDeltaTime;
                if (stopwatch > fireInterval)
                {
                    stopwatch = 0f;
                    Ray aimRay = base.GetAimRay();
                    EffectManager.SpawnEffect(Assets.elderlemurianexplosionEffect, new EffectData
                    {
                        origin = aimRay.origin,
                        scale = radius,
                        rotation = Quaternion.identity,

                    }, true);

                    blastAttack.crit = characterBody.RollCrit();
                    blastAttack.baseDamage = damage;
                    blastAttack.radius = radius;
                    blastAttack.position = aimRay.origin;
                    blastAttack.Fire();
                    EffectManager.SimpleMuzzleFlash(FireFireball.effectPrefab, base.gameObject, muzzleString, false);
                    int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
                    //base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", fireInterval, 0.05f);
                    base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", fireInterval, 0.05f);

                    //increment radius size after each attack
                    radius += StaticValues.blastBurnIncrementRadius;
                    damage += characterBody.damage * StaticValues.blastBurnDamageCoefficientGain;


                }
            }
            else if (!base.IsKeyDownAuthority())
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