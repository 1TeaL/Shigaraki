﻿using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using EntityStates;
using System.Collections.Generic;
using System.Linq;
using ShiggyMod.Modules.Survivors;
using EntityStates.ParentMonster;

namespace ShiggyMod.SkillStates
{
    public class ParentTeleport : BaseSkillState
    {

        public bool hasTeleported;
        public bool hasFired;
        public float baseDuration = 1f;
        public float duration;
        public float fireTime;
        public ShiggyController Shiggycon;
        private DamageType damageType;

        private float radius = 10f;
        private float damageCoefficient = Modules.StaticValues.parentDamageCoeffecient;
        private float procCoefficient = 1f;
        private float force = 1000f;

        private BlastAttack blastAttack;

        public HurtBox Target;
        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / base.attackSpeedStat;
            this.fireTime = this.duration / 3f;
            hasFired = false;
            hasTeleported = false;
            damageType = DamageType.Stun1s;


            Shiggycon = gameObject.GetComponent<ShiggyController>();
            if (Shiggycon && base.isAuthority)
            {
                Target = Shiggycon.GetTrackingTarget();
            }
            if (!Target)
            {
                return;
            }



            blastAttack = new BlastAttack();
            blastAttack.radius = radius;
            blastAttack.procCoefficient = procCoefficient;
            blastAttack.attacker = base.gameObject;
            blastAttack.crit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master);
            blastAttack.baseDamage = base.damageStat * damageCoefficient;
            blastAttack.falloffModel = BlastAttack.FalloffModel.None;
            blastAttack.baseForce = force;
            blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
            blastAttack.damageType = damageType;
            blastAttack.attackerFiltering = AttackerFiltering.Default;

        }


        public override void OnExit()
        {
            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            if(!hasTeleported && Target)
            {
                hasTeleported = true;
                base.characterMotor.velocity = Vector3.zero;
                base.characterMotor.Motor.SetPositionAndRotation(Target.healthComponent.body.transform.position, Target.healthComponent.body.transform.rotation, true);
            }

            Vector3 latestposition = base.transform.position;
            if (base.fixedAge > this.fireTime && !hasFired && base.isAuthority)
            {
                hasFired = true;
                blastAttack.position = latestposition;
                EffectManager.SpawnEffect(Modules.Assets.parentslamEffect, new EffectData
                {
                    origin = latestposition,
                    scale = radius,
                }, true);

                blastAttack.Fire();
                ApplyDoT();
                
            }

            if ((base.fixedAge >= this.duration && base.isAuthority))
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        public void ApplyDoT()
        {
            Ray aimRay = base.GetAimRay();
            BullseyeSearch search = new BullseyeSearch
            {

                teamMaskFilter = TeamMask.GetEnemyTeams(base.GetTeam()),
                filterByLoS = false,
                searchOrigin = base.characterBody.corePosition,
                searchDirection = UnityEngine.Random.onUnitSphere,
                sortMode = BullseyeSearch.SortMode.Distance,
                maxDistanceFilter = radius,
                maxAngleFilter = 360f
            };

            search.RefreshCandidates();
            search.FilterOutGameObject(base.gameObject);



            List<HurtBox> target = search.GetResults().ToList<HurtBox>();
            foreach (HurtBox singularTarget in target)
            {
                if (singularTarget)
                {
                    if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                    {
                        InflictDotInfo info = new InflictDotInfo();
                        info.attackerObject = base.gameObject;
                        info.victimObject = singularTarget.healthComponent.body.gameObject;
                        info.duration = Modules.StaticValues.decayDamageTimer;
                        info.dotIndex = Modules.Dots.decayDot;

                        DotController.InflictDot(ref info);
                    }
                }
            }
        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}
