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

namespace ShiggyMod.SkillStates
{
    public class BombGlaive : Skill
    {
        //huntress + void devastator crab

        public float baseDuration = 1f;
        public float duration;
        public ShiggyController Shiggycon;
        private Transform modelTransform;

        public static GameObject effectPrefab;
        private ChildLocator childLocator;
        private GameObject chargeEffect;

        private string muzzleString;
        private Animator animator;
        private HurtBox initialOrbTarget;
        private bool hasTriedToThrowGlaive;
        private bool hasSuccessfullyThrownGlaive;
        private float stopwatch;
        private ExtraSkillLocator extraskillLocator;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            duration = baseDuration / attackSpeedStat;

            base.characterBody.SetAimTimer(this.duration);
            this.muzzleString = "LHand";
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            this.animator = base.GetModelAnimator();
            //this.animator.SetBool("attacking", true);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", duration / 2, 0.1f);
            //PlayCrossfade("LeftArm, Override", "LeftArmPunch", "Attack.playbackRate", duration/2, 0.1f);

            Util.PlayAttackSpeedSound(ThrowGlaive.attackSoundString, base.gameObject, this.attackSpeedStat);
            if (Shiggycon.Target && base.isAuthority)
            {
                this.initialOrbTarget = Shiggycon.GetTrackingTarget();
            }
            if (base.characterMotor && ThrowGlaive.smallHopStrength != 0f)
            {
                base.characterMotor.velocity.y = ThrowGlaive.smallHopStrength;
            }

            if (this.modelTransform)
            {
                this.childLocator = this.modelTransform.GetComponent<ChildLocator>();
                if (this.childLocator)
                {
                    Transform transform = this.childLocator.FindChild(muzzleString);
                    if (transform && ThrowGlaive.chargePrefab)
                    {
                        this.chargeEffect = UnityEngine.Object.Instantiate<GameObject>(ThrowGlaive.chargePrefab, transform.position, transform.rotation);
                        this.chargeEffect.transform.parent = transform;
                    }
                }
            }


            On.RoR2.Orbs.LightningOrb.OnArrival += LightningOrb_OnArrival;

        }

        private void LightningOrb_OnArrival(On.RoR2.Orbs.LightningOrb.orig_OnArrival orig, LightningOrb self)
        {
            orig(self);
            if (self.attacker.gameObject.GetComponent<CharacterBody>().baseNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
            {
                new BlastAttack
                {
                    attacker = self.attacker.gameObject,
                    teamIndex = TeamComponent.GetObjectTeam(self.attacker.gameObject),
                    falloffModel = BlastAttack.FalloffModel.None,
                    baseDamage = self.damageValue,
                    damageType = DamageType.Generic,
                    damageColorIndex = DamageColorIndex.Default,
                    baseForce = -1000f,
                    position = self.target.transform.position,
                    radius = StaticValues.bombGlaiveBounceRange/2f,
                    procCoefficient = StaticValues.bombGlaiveProcCoefficient,
                    attackerFiltering = AttackerFiltering.NeverHitSelf,
                }.Fire();

                EffectManager.SpawnEffect(EntityStates.VoidMegaCrab.DeathState.voidDeathEffect, new EffectData
                {
                    origin = self.target.transform.position,
                    scale = StaticValues.bombGlaiveBounceRange / 2f
                }, true);
            }

        }

        public void FireOrbGlaive()
        {
            if (!NetworkServer.active || this.hasTriedToThrowGlaive)
            {
                return;
            }
            this.hasTriedToThrowGlaive = true;
            LightningOrb lightningOrb = new LightningOrb();
            lightningOrb.lightningType = LightningOrb.LightningType.HuntressGlaive;
            lightningOrb.damageValue = base.characterBody.damage * StaticValues.bombGlaiveDamageCoefficient;
            lightningOrb.isCrit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master);
            lightningOrb.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
            lightningOrb.attacker = base.gameObject;
            lightningOrb.procCoefficient = StaticValues.bombGlaiveProcCoefficient;
            lightningOrb.bouncesRemaining = StaticValues.bombGlaiveMaxBounceCount;
            lightningOrb.speed = StaticValues.bombGlaiveTravelSpeed;
            lightningOrb.bouncedObjects = new List<HealthComponent>();
            lightningOrb.range = StaticValues.bombGlaiveBounceRange;
            lightningOrb.damageCoefficientPerBounce = StaticValues.bombGlaiveDamageCoefficientPerBounce;

            HurtBox hurtBox = this.initialOrbTarget;
            if (hurtBox)
            {
                this.hasSuccessfullyThrownGlaive = true;
                Transform transform = this.childLocator.FindChild(muzzleString);
                EffectManager.SimpleMuzzleFlash(ThrowGlaive.muzzleFlashPrefab, base.gameObject, muzzleString, true);
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

            On.RoR2.Orbs.LightningOrb.OnArrival -= LightningOrb_OnArrival;
            if (this.chargeEffect)
            {
                EntityState.Destroy(this.chargeEffect);
            }
            if (!this.hasTriedToThrowGlaive)
            {
                for (int i = 0; i < 1 * Shiggycon.projectileCount; i++)
                {
                    FireOrbGlaive();
                }
            }
            if (!this.hasSuccessfullyThrownGlaive && NetworkServer.active)
            {
                //refund if unsucessful
                if (base.skillLocator.primary.skillDef == Shiggy.bombGlaiveDef)
                {
                    skillLocator.primary.AddOneStock();
                }
                if (base.skillLocator.secondary.skillDef == Shiggy.bombGlaiveDef)
                {
                    skillLocator.secondary.AddOneStock();
                }
                if (base.skillLocator.utility.skillDef == Shiggy.bombGlaiveDef)
                {
                    skillLocator.utility.AddOneStock();
                }
                if (base.skillLocator.special.skillDef == Shiggy.bombGlaiveDef)
                {
                    skillLocator.special.AddOneStock();
                }

                extraskillLocator = base.GetComponent<ExtraSkillLocator>();

                if (extraskillLocator.extraFirst.skillDef == Shiggy.bombGlaiveDef)
                {
                    extraskillLocator.extraFirst.AddOneStock();
                }
                if (extraskillLocator.extraSecond.skillDef == Shiggy.bombGlaiveDef)
                {
                    extraskillLocator.extraSecond.AddOneStock();
                }
                if (extraskillLocator.extraThird.skillDef == Shiggy.bombGlaiveDef)
                {
                    extraskillLocator.extraThird.AddOneStock();
                }
                if (extraskillLocator.extraFourth.skillDef == Shiggy.bombGlaiveDef)
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
                for (int i = 0; i < 1 * Shiggycon.projectileCount; i++)
                {
                    FireOrbGlaive();
                }
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
            writer.Write(HurtBoxReference.FromHurtBox(this.initialOrbTarget));
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            this.initialOrbTarget = reader.ReadHurtBoxReference().ResolveHurtBox();
        }
    }
}