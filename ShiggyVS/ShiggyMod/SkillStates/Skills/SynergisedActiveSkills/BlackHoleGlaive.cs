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
using RoR2.Projectile;
using EntityStates.Huntress.HuntressWeapon;
using RoR2.Orbs;
using ShiggyMod.Modules.Networking;
using R2API.Networking.Interfaces;

namespace ShiggyMod.SkillStates
{
    public class BlackHoleGlaive : BaseSkillState
    {
        //huntress + void devastator crab

        public float baseDuration = 2f;
        public float duration;
        public ShiggyController Shiggycon;
        private Transform modelTransform;

        public static GameObject effectPrefab;
        private ChildLocator childLocator;
        private GameObject chargeEffect;

        private string muzzleString;
        private Animator animator;
        private HurtBox Target;
        private bool hasTriedToThrowGlaive;
        private bool hasSuccessfullyThrownGlaive;
        private float stopwatch;
        private ExtraSkillLocator extraskillLocator;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            duration = baseDuration / attackSpeedStat;
            hasTriedToThrowGlaive = false;

            base.characterBody.SetAimTimer(this.duration);
            this.muzzleString = "LHand";
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            Transform modelTransform = base.GetModelTransform();
            if (modelTransform)
            {
                this.childLocator = modelTransform.GetComponent<ChildLocator>();
                this.animator = modelTransform.GetComponent<Animator>();
            }

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
            //base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }

            Util.PlayAttackSpeedSound(ThrowGlaive.attackSoundString, base.gameObject, this.attackSpeedStat);


            if (Shiggycon.trackingTarget.teamIndex == TeamIndex.Monster || Shiggycon.trackingTarget.teamIndex == TeamIndex.Neutral || Shiggycon.trackingTarget.teamIndex == TeamIndex.Void)
            {
                Target = Shiggycon.GetTrackingTarget();
            }
            if (!Target)
            {
                return;
            }

            if (base.characterMotor && ThrowGlaive.smallHopStrength != 0f)
            {
                base.characterMotor.velocity.y = ThrowGlaive.smallHopStrength;
            }

            if (Assets.huntressGlaiveChargeEffect)
            {
                this.chargeEffect = UnityEngine.Object.Instantiate<GameObject>(Assets.huntressGlaiveChargeEffect, this.childLocator.FindChild(muzzleString).position, Util.QuaternionSafeLookRotation(aimRay.direction.normalized));
                this.chargeEffect.transform.parent = this.childLocator.FindChild(muzzleString);
            }


        }


        private void FireOrbGlaive()
        {
            if (!NetworkServer.active || this.hasTriedToThrowGlaive)
            {
                return;
            }
            this.hasTriedToThrowGlaive = true;
            LightningOrb lightningOrb = new LightningOrb();
            lightningOrb.lightningType = LightningOrb.LightningType.HuntressGlaive;
            lightningOrb.damageValue = base.characterBody.damage * StaticValues.blackholeGlaiveDamageCoefficient;
            lightningOrb.isCrit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master);
            lightningOrb.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
            lightningOrb.attacker = base.gameObject;
            lightningOrb.procCoefficient = StaticValues.blackholeGlaiveProcCoefficient;
            lightningOrb.bouncesRemaining = StaticValues.blackholeGlaiveMaxBounceCount;
            lightningOrb.speed = StaticValues.blackholeGlaiveTravelSpeed;            
            lightningOrb.bouncedObjects = new List<HealthComponent>();
            lightningOrb.range = StaticValues.blackholeGlaiveBounceRange;
            lightningOrb.damageCoefficientPerBounce = StaticValues.blackholeGlaiveDamageCoefficientPerBounce;

            HurtBox hurtBox = this.Target;
            if (hurtBox)
            {
                this.hasSuccessfullyThrownGlaive = true;
                Transform transform = this.childLocator.FindChild(this.muzzleString);
                EffectManager.SimpleMuzzleFlash(Assets.huntressGlaiveMuzzleEffect, base.gameObject, muzzleString, true);
                lightningOrb.origin = transform.position;
                lightningOrb.target = hurtBox;
                OrbManager.instance.AddOrb(lightningOrb);
            }

        }

        public override void OnExit()
        {
            base.OnExit();
            this.animator.SetBool("false", true);
            //PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);

            if (this.chargeEffect)
            {
                EntityState.Destroy(this.chargeEffect);
            }
            if (!this.hasTriedToThrowGlaive)
            {
                FireOrbGlaive();
                
            }
            if (!this.hasSuccessfullyThrownGlaive && NetworkServer.active)
            {
                //refund if unsucessful
                if (base.skillLocator.primary.skillDef == Shiggy.blackholeGlaiveDef)
                {
                    skillLocator.primary.AddOneStock();
                }
                if (base.skillLocator.secondary.skillDef == Shiggy.blackholeGlaiveDef)
                {
                    skillLocator.secondary.AddOneStock();
                }
                if (base.skillLocator.utility.skillDef == Shiggy.blackholeGlaiveDef)
                {
                    skillLocator.utility.AddOneStock();
                }
                if (base.skillLocator.special.skillDef == Shiggy.blackholeGlaiveDef)
                {
                    skillLocator.special.AddOneStock();
                }

                extraskillLocator = base.GetComponent<ExtraSkillLocator>();

                if (extraskillLocator.extraFirst.skillDef == Shiggy.blackholeGlaiveDef)
                {
                    extraskillLocator.extraFirst.AddOneStock();
                }
                if (extraskillLocator.extraSecond.skillDef == Shiggy.blackholeGlaiveDef)
                {
                    extraskillLocator.extraSecond.AddOneStock();
                }
                if (extraskillLocator.extraThird.skillDef == Shiggy.blackholeGlaiveDef)
                {
                    extraskillLocator.extraThird.AddOneStock();
                }
                if (extraskillLocator.extraFourth.skillDef == Shiggy.blackholeGlaiveDef)
                {
                    extraskillLocator.extraFourth.AddOneStock();
                }
            }
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            stopwatch += Time.fixedDeltaTime;
            if (!this.hasTriedToThrowGlaive && stopwatch > duration/3f)
            {
                if (this.chargeEffect)
                {
                    EntityState.Destroy(this.chargeEffect);
                }
                FireOrbGlaive();
                
            }
            CharacterMotor characterMotor = base.characterMotor;
            characterMotor.velocity.y = characterMotor.velocity.y + ThrowGlaive.antigravityStrength * Time.fixedDeltaTime * (1f - this.stopwatch / this.duration);
            if (this.stopwatch >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
        public override void OnSerialize(NetworkWriter writer)
        {
            writer.Write(HurtBoxReference.FromHurtBox(this.Target));
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            this.Target = reader.ReadHurtBoxReference().ResolveHurtBox();
        }
    }
}