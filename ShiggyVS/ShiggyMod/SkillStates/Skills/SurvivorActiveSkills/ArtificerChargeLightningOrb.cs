using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using EntityStates.Mage.Weapon;
using RoR2.UI;
using RoR2.Audio;
using ExtraSkillSlots;

namespace ShiggyMod.SkillStates
{
    public class ArtificerChargeLightningOrb : BaseSkillState
    {
        private ExtraInputBankTest extrainputBankTest;
        private ExtraSkillLocator extraskillLocator;
        private bool skillSwapped;
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float baseDuration = 1.5f;
        public float duration;
        public ShiggyController Shiggycon;
        public HurtBox Target;
        private float damageCoefficient = Modules.StaticValues.artificericewallDamageCoefficient;

        private Animator animator;
        private ChildLocator childLocator;
        private GameObject chargeEffectInstance;
        private GameObject chargeEffectPrefab = Modules.Assets.artificerlightningorbchargeEffect;
        private string chargeSoundString;
        public LoopSoundDef loopSoundDef = Modules.Assets.artificerlightningsound;
        private LoopSoundManager.SoundLoopPtr loopPtr;
        public GameObject crosshairOverridePrefab = Modules.Assets.artificerCrosshair;
        private CrosshairUtils.OverrideRequest crosshairOverrideRequest;
        private float minChargeDuration = 0.5f;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration + 2f);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", duration, 0.1f);

            Shiggycon = gameObject.GetComponent<ShiggyController>();
            extraskillLocator = base.GetComponent<ExtraSkillLocator>();
            extrainputBankTest = outer.GetComponent<ExtraInputBankTest>();
            skillSwapped = false;
            

            this.animator = base.GetModelAnimator();
            this.childLocator = base.GetModelChildLocator();
            if (this.childLocator)
            {
                Transform transform = this.childLocator.FindChild("LHand") ?? base.characterBody.coreTransform;
                if (transform && this.chargeEffectPrefab)
                {
                    this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform.position, transform.rotation);
                    this.chargeEffectInstance.transform.parent = transform;
                    ScaleParticleSystemDuration component = this.chargeEffectInstance.GetComponent<ScaleParticleSystemDuration>();
                    ObjectScaleCurve component2 = this.chargeEffectInstance.GetComponent<ObjectScaleCurve>();
                    if (component)
                    {
                        component.newDuration = this.duration;
                    }
                    if (component2)
                    {
                        component2.timeMax = this.duration;
                    }
                }
            }
            if (this.loopSoundDef)
            {
                this.loopPtr = LoopSoundManager.PlaySoundLoopLocal(base.gameObject, this.loopSoundDef);
            }
            if (this.crosshairOverridePrefab)
            {
                this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, this.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
            }
            base.StartAimMode(this.duration + 2f, false);

        }
        public override void Update()
        {
            base.Update();
            base.characterBody.SetSpreadBloom(Util.Remap(this.CalcCharge(), 0f, 1f, 3f, 10f), true);
        }
        public override void OnExit()
        {
            base.OnExit();
            CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
            if (overrideRequest != null)
            {
                overrideRequest.Dispose();
            }
            LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
            if (!this.outer.destroying)
            {
                //this.PlayAnimation("LeftArm, Override", "BufferEmpty");
            }
            EntityState.Destroy(this.chargeEffectInstance);
            //PlayCrossfade("LeftArm, Override", "LeftArmPunch", "Attack.playbackRate", duration, 0.1f);
            
        }
        protected float CalcCharge()
        {
            return Mathf.Clamp01(base.fixedAge / this.duration);
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();


            if (base.IsKeyDownAuthority() && !skillSwapped)
            {
                skillSwapped = true;
                if (base.inputBank.skill1.down)
                {
                    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (base.inputBank.skill2.down)
                {
                    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (base.inputBank.skill3.down)
                {
                    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (base.inputBank.skill4.down)
                {
                    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (extrainputBankTest.extraSkill1.down)
                {
                    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (extrainputBankTest.extraSkill2.down)
                {
                    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (extrainputBankTest.extraSkill3.down)
                {
                    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (extrainputBankTest.extraSkill4.down)
                {
                    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                }
            }

            float charge = this.CalcCharge();
            if (base.isAuthority && ((!base.IsKeyDownAuthority() && base.fixedAge >= this.minChargeDuration)))
            {
                ArtificerThrowLightningOrb nextState = new ArtificerThrowLightningOrb();
                nextState.charge = charge;
                this.outer.SetNextState(nextState);

            }
            if (base.fixedAge >= minChargeDuration)
            {
                EffectManager.SpawnEffect(Modules.Assets.artificerlightningorbchargeEffect, new EffectData
                {
                    origin = this.childLocator.FindChild("LHand").position,
                    scale = 1f,
                    rotation = Quaternion.LookRotation(base.transform.position)

                }, false);


            }
        }



        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
