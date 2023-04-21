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
    public class BlastBurn : BaseSkillState
    {
        //Elder lemurian and lemurian
        private BlastAttack blastAttack;
        public float fireInterval;
        public float stopwatch;
        public float radius;

        public override void OnEnter()
        {
            base.OnEnter();
            fireInterval = StaticValues.blastBurnBaseInterval / attackSpeedStat;
            radius = StaticValues.blastBurnStartRadius;


            Ray aimRay = base.GetAimRay();


            blastAttack = new BlastAttack();
            blastAttack.radius = radius;
            blastAttack.procCoefficient = StaticValues.blastBurnProcCoefficient;
            blastAttack.position = aimRay.origin;
            blastAttack.attacker = base.gameObject;
            blastAttack.crit = characterBody.RollCrit();
            blastAttack.baseDamage = damageStat * Modules.StaticValues.blastBurnDamageCoefficient;
            blastAttack.falloffModel = BlastAttack.FalloffModel.None;
            blastAttack.baseForce = 400f;
            blastAttack.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
            blastAttack.damageType |= DamageType.BypassArmor;
            blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;

            //play anim

        }

        public override void OnExit()
        {
            base.OnExit();

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

                    blastAttack.position = aimRay.origin;
                    blastAttack.Fire();

                    //increment radius size after each attack
                    radius += StaticValues.blastBurnIncrementRadius;
                }
            }
            else if (!base.IsKeyDownAuthority())
            {

                this.outer.SetNextStateToMain();
                return;
            }


        }
    }
}