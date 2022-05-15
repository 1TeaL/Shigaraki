using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using EntityStates.Mage.Weapon;
using RoR2.UI;
using RoR2.Audio;

namespace ShiggyMod.SkillStates
{
    public class ArtificerChargeLightningOrb : BaseSkillState
    {
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
            //PlayCrossfade("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", duration, 0.1f);

            Shiggycon = gameObject.GetComponent<ShiggyController>();
            damageCoefficient *= Shiggycon.rangedMultiplier;

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
