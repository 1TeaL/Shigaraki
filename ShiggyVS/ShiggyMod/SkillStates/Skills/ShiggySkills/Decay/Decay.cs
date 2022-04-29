﻿using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using System;
using System.Collections.Generic;

namespace ShiggyMod.SkillStates
{
    public class Decay : BaseSkillState
    {

        public DecayController decaycon;
        public ShiggyController Shiggycon;
        protected string hitboxName = "SmallHitbox";
        protected string hitboxName2 = "DetectSmallHitbox";
        private Animator animator;
        private CharacterModel characterModel;
        private Transform modelTransform;
        private OverlapAttack attack;
        private OverlapAttack detector;

        private DamageType damageType;
        public static float baseduration = 0.8f;
        public float duration;
        public float fireTime;
        private float procCoefficient = 1f;
        private float pushForce = 100f;
        private float damageCoefficient = 1f;
        private float hitPauseTimer;
        private float stopwatch;
        public static float hitExtraDuration = 0.44f;
        public static float minExtraDuration = 0.2f;
        protected float hitStopDuration = 0.15f;
        private float extraDuration;
        private float smallhopvelocity = 10f;

        private bool inHitPause;
        private bool hasHopped;

        private HitStopCachedState hitStopCachedState;
        private Vector3 direction;
        private Vector3 bounceVector;
        private Vector3 bonusForce = new Vector3(10f, 10f, 0f);

        public override void OnEnter()
        {
            base.OnEnter();
            damageType = DamageType.Generic;

            duration = baseduration / attackSpeedStat;
            if(duration <= 0.2f)
            {
                duration = 0.2f;
            }
            fireTime = duration / 4;
            if (fireTime <= 0.05f)
            {
                duration = 0.05f;
            }
            base.characterBody.SetAimTimer(this.duration);

            Ray aimRay = base.GetAimRay();
            this.animator = base.GetModelAnimator();
            this.animator.SetBool("attacking", true);

            HitBoxGroup hitBoxGroup = null;
            HitBoxGroup hitBoxGroup2 = null;
            Transform modelTransform = base.GetModelTransform();
            bool flag = modelTransform.gameObject.GetComponent<AimAnimator>();
            if (flag)
            {
                modelTransform.gameObject.GetComponent<AimAnimator>().enabled = false;
            }
            this.modelTransform = base.GetModelTransform();
            if (this.modelTransform)
            {
                this.animator = this.modelTransform.GetComponent<Animator>();
                this.characterModel = this.modelTransform.GetComponent<CharacterModel>();
            }

            bool flag2 = modelTransform;
            if (flag2)
            {
                hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == hitboxName);
                hitBoxGroup2 = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == hitboxName2);
            }

            this.attack = new OverlapAttack();
            this.attack.damageType = this.damageType;
            this.attack.attacker = base.gameObject;
            this.attack.inflictor = base.gameObject;
            this.attack.teamIndex = base.GetTeam();
            this.attack.damage = base.characterBody.damage * this.damageCoefficient;
            this.attack.procCoefficient = this.procCoefficient;
            this.attack.hitEffectPrefab = EntityStates.Commando.CommandoWeapon.FireBarrage.hitEffectPrefab;
            this.attack.forceVector = this.bonusForce;
            this.attack.pushAwayForce = this.pushForce;
            this.attack.hitBoxGroup = hitBoxGroup;
            this.attack.isCrit = base.RollCrit();

            this.detector = new OverlapAttack();
            this.detector.damageType = this.damageType;
            this.detector.attacker = base.gameObject;
            this.detector.inflictor = base.gameObject;
            this.detector.teamIndex = base.GetTeam();
            this.detector.damage = 0f;
            this.detector.procCoefficient = 0f;
            this.detector.hitEffectPrefab = null;
            this.detector.forceVector = Vector3.zero;
            this.detector.pushAwayForce = 0f;
            this.detector.hitBoxGroup = hitBoxGroup2;
            this.detector.isCrit = false;

            float num = this.moveSpeedStat;
            bool isSprinting = base.characterBody.isSprinting;
            if (isSprinting)
            {
                num /= base.characterBody.sprintingSpeedMultiplier;
            }
            float num2 = (num / base.characterBody.baseMoveSpeed - 1f) * 0.67f;
            this.extraDuration = Math.Max(hitExtraDuration / (num2 + 1f), minExtraDuration);

            this.direction = base.GetAimRay().direction.normalized;
            base.characterDirection.forward = this.direction;

        }

        public override void OnExit()
        {
            Transform modelTransform = base.GetModelTransform();
            bool flag = modelTransform.gameObject.GetComponent<AimAnimator>();
            if (flag)
            {
                modelTransform.gameObject.GetComponent<AimAnimator>().enabled = false;
            }
            this.animator.SetBool("attacking", false);
            base.OnExit();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            //this.FireAttack();
            if (base.fixedAge >= this.fireTime && base.isAuthority)
            {
                this.FireAttack();
            }
            this.hitPauseTimer -= Time.fixedDeltaTime;
            bool flag = this.hitPauseTimer <= 0f && this.inHitPause;
            if (flag)
            {
                base.ConsumeHitStopCachedState(this.hitStopCachedState, base.characterMotor, this.animator);
                this.inHitPause = false;
                base.characterMotor.velocity = Vector3.zero;
                base.characterMotor.ApplyForce(this.bounceVector, true, false);
                //this.attack.Fire(null);
                this.detector.Fire(null);
            }
            bool flag2 = !this.inHitPause;
            if (flag2)
            {
                this.stopwatch += Time.fixedDeltaTime;
            }

            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }


        private void FireAttack()
        {
            bool isAuthority = base.isAuthority;
            if (isAuthority)
            {
                List<HurtBox> list = new List<HurtBox>();
                bool flag = this.attack.Fire(list);
                if (flag)
                {
                    foreach (HurtBox hurtBox in list)
                    {
                        //Decay Controller
                        if (!hurtBox.healthComponent.body.gameObject.GetComponent<DecayController>())
                        {
                            decaycon = hurtBox.healthComponent.body.gameObject.AddComponent<DecayController>();
                        }

                        
                        this.OnHitEnemyAuthority();

                        //Decay Dot
                        bool flag2 = hurtBox.healthComponent && hurtBox.healthComponent.body;
                        if (flag2)
                        {

                            InflictDotInfo info = new InflictDotInfo();
                            info.attackerObject = base.gameObject;
                            info.victimObject = hurtBox.healthComponent.body.gameObject;
                            info.duration = Modules.StaticValues.decayDamageTimer;
                            info.damageMultiplier = Modules.StaticValues.decayDamageCoeffecient + Modules.StaticValues.decayDamageStack * hurtBox.healthComponent.body.GetBuffCount(Modules.Buffs.decayDebuff);
                            info.dotIndex = Modules.Dots.decayDot;

                            DotController.InflictDot(ref info);

                        }
                    }
                }
            }
        }

        protected virtual void OnHitEnemyAuthority()
        {
            if (!isGrounded)
            {
                base.SmallHop(base.characterMotor, this.smallhopvelocity/this.attackSpeedStat);
            }
            //base.characterBody.SetAimTimer(duration/2);

            this.stopwatch = this.duration - this.extraDuration;
            bool flag = !this.hasHopped;
            if (flag)
            {
                bool flag2 = !this.inHitPause && this.hitStopDuration > 0f;
                if (flag2)
                {
                    //this.storedVelocity = base.characterMotor.velocity;
                    this.hitStopCachedState = base.CreateHitStopCachedState(base.characterMotor, this.animator, "Attack.playbackRate");
                    float num = this.moveSpeedStat;
                    bool isSprinting = base.characterBody.isSprinting;
                    if (isSprinting)
                    {
                        num /= base.characterBody.sprintingSpeedMultiplier;
                    }
                    float num2 = 1f + (num / base.characterBody.baseMoveSpeed - 1f);
                    this.hitPauseTimer = this.hitStopDuration / num2;
                    this.inHitPause = true;
                }
                bool flag3 = base.characterMotor;
                if (flag3)
                {
                    //base.characterMotor.velocity = Vector3.zero;
                    this.bounceVector = base.GetAimRay().direction * -1f;
                    this.bounceVector.y = 0.2f;
                }
                this.hasHopped = true;
            }

        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }
        
    }
}