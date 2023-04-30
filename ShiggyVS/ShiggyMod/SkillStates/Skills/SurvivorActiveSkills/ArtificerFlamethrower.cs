using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using EntityStates.Mage.Weapon;
using ExtraSkillSlots;

namespace ShiggyMod.SkillStates
{
    public class ArtificerFlamethrower : BaseSkillState
    {
        private ExtraInputBankTest extrainputBankTest;
        private ExtraSkillLocator extraskillLocator;
        private bool skillSwapped;
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float baseDuration = 1f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;
        public HurtBox Target;
        public GameObject flamethrowerEffectPrefab = Modules.Assets.artificerfireEffect;

        private float force = 1f;
		public float maxDistance = 16f;
        private float procCoefficientPerTick = Modules.StaticValues.artificerflamethrowerProcCoefficient;
        public float baseFlamethrowerDuration = 3f;
        public float totalDamageCoefficient = Modules.StaticValues.artificerflamethrowerDamageCoefficient;
        public float tickFrequency = 5f;
        public float ignitePercentChance;
        public float recoilForce;
        private float tickDamageCoefficient;
        private float flamethrowerStopwatch;
        private float stopwatch;
        public float baseEntryDuration = 0.5f;
        private float entryDuration;
        private float flamethrowerDuration;

        private bool hasBegunFlamethrower;
        private ChildLocator childLocator;
        private GameObject leftFlamethrowerTransform;
        private GameObject rightFlamethrowerTransform;
        private Transform leftMuzzleTransform;
        private bool isCrit;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", entryDuration, 0.1f);
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            extraskillLocator = base.GetComponent<ExtraSkillLocator>();
            extrainputBankTest = outer.GetComponent<ExtraInputBankTest>();
            skillSwapped = false;

            AkSoundEngine.PostEvent("ShiggyAirCannon", base.gameObject);
            this.stopwatch = 0f;
            this.entryDuration = baseEntryDuration / this.attackSpeedStat;
            this.flamethrowerDuration = baseFlamethrowerDuration;
            Transform modelTransform = base.GetModelTransform();
            if (base.characterBody)
            {
                base.characterBody.SetAimTimer(this.entryDuration + this.flamethrowerDuration + 1f);
            }
            if (modelTransform)
            {
                this.childLocator = modelTransform.GetComponent<ChildLocator>();
                this.leftMuzzleTransform = this.childLocator.FindChild("LHand");
            }
            int num = Mathf.CeilToInt(this.flamethrowerDuration * tickFrequency);
            this.tickDamageCoefficient = totalDamageCoefficient / (float)num;
            if (base.isAuthority && base.characterBody)
            {
                this.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
            }

        }

        public override void OnExit()
        {
            base.OnExit();
            Util.PlaySound(Flamethrower.endAttackSoundString, base.gameObject);
            PlayCrossfade("LeftArm, Override", "Empty", "Attack.playbackRate", 0.1f, 0.1f);
            if (this.leftFlamethrowerTransform)
            {
                EntityState.Destroy(this.leftFlamethrowerTransform.gameObject);
            }
            if (this.rightFlamethrowerTransform)
            {
                EntityState.Destroy(this.rightFlamethrowerTransform.gameObject);
            }
            
        }
        private void FireGauntlet(string muzzleString)
        {
            Ray aimRay = base.GetAimRay();
            if (base.isAuthority)
            {
                new BulletAttack
                {
                    owner = base.gameObject,
                    weapon = base.gameObject,
                    origin = aimRay.origin,
                    aimVector = aimRay.direction,
                    minSpread = 0f,
                    damage = this.tickDamageCoefficient * this.damageStat,
                    force = force,
                    muzzleName = muzzleString,
                    hitEffectPrefab = Flamethrower.impactEffectPrefab,
                    isCrit = this.isCrit,
                    radius = Flamethrower.radius,
                    falloffModel = BulletAttack.FalloffModel.None,
                    stopperMask = LayerIndex.world.mask,
                    procCoefficient = procCoefficientPerTick,
                    maxDistance = this.maxDistance,
                    smartCollision = true,
                    tracerEffectPrefab = Flamethrower.tracerEffectPrefab,
                    damageType = (Util.CheckRoll(Flamethrower.ignitePercentChance, base.characterBody.master) ? DamageType.IgniteOnHit : damageType)
                }.Fire();
                if (base.characterMotor)
                {
                    base.characterMotor.ApplyForce(aimRay.direction * -Flamethrower.recoilForce, false, false);
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.stopwatch += Time.fixedDeltaTime;

            if (base.IsKeyDownAuthority() && !skillSwapped)
            {
                skillSwapped = true;
                if (base.inputBank.skill1.down)
                {
                    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (base.inputBank.skill2.down)
                {
                    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (base.inputBank.skill3.down)
                {
                    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (base.inputBank.skill4.down)
                {
                    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (extrainputBankTest.extraSkill1.down)
                {
                    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (extrainputBankTest.extraSkill2.down)
                {
                    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (extrainputBankTest.extraSkill3.down)
                {
                    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (extrainputBankTest.extraSkill4.down)
                {
                    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                }
            }


            if (this.stopwatch >= this.entryDuration && !this.hasBegunFlamethrower)
            {
                this.hasBegunFlamethrower = true;
                Util.PlaySound(Flamethrower.startAttackSoundString, base.gameObject);
                if (this.childLocator)
                {
                    Transform transform = this.childLocator.FindChild("LHand");
                    if (transform)
                    {
                        this.leftFlamethrowerTransform = UnityEngine.Object.Instantiate<GameObject>(flamethrowerEffectPrefab, transform.position, transform.rotation);
                        this.leftFlamethrowerTransform.transform.parent = transform;
                    }
                    if (this.leftFlamethrowerTransform)
                    {
                        this.leftFlamethrowerTransform.GetComponent<ScaleParticleSystemDuration>().newDuration = this.flamethrowerDuration;
                    }

                }
                this.FireGauntlet("LHand");
            }
            if (this.hasBegunFlamethrower)
            {
                this.flamethrowerStopwatch += Time.deltaTime;
                float num = 1f / tickFrequency / this.attackSpeedStat;
                if (this.flamethrowerStopwatch > num)
                {
                    this.flamethrowerStopwatch -= num;
                    this.FireGauntlet("LHand");
                }
                this.UpdateFlamethrowerEffect();
                PlayAnimation("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", flamethrowerDuration);
            }
            if (this.stopwatch >= this.flamethrowerDuration + this.entryDuration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        private void UpdateFlamethrowerEffect()
        {
            Ray aimRay = base.GetAimRay();
            Vector3 direction = aimRay.direction;
            Vector3 direction2 = aimRay.direction;
            if (this.leftFlamethrowerTransform)
            {
                this.leftFlamethrowerTransform.transform.forward = direction;
            }
        }


        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
