using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using EntityStates.LunarGolem;
using UnityEngine.Networking;
using RoR2.Projectile;
using EntityStates.Commando;

namespace ShiggyMod.SkillStates
{
    public class LunarGolemSlide : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";

        public ShiggyController Shiggycon;
        private bool startedStateGrounded;

        public static float minLeadTime = 0.5f;
        public static float maxLeadTime = 0.5f;
        private float force = 400f;
        private float speedOverride = -1f;
        private Vector3 forwardDirection;
        private GameObject slideEffectInstance;
        private float baseslideDuration = 1f;
        private float basejumpDuration = 0.2f;
        private float slideDuration;
        private float jumpDuration;

        public override void OnEnter()
        {
            base.OnEnter();
            this.slideDuration = this.baseslideDuration / this.attackSpeedStat;
            this.jumpDuration = this.basejumpDuration / this.attackSpeedStat;

            Util.PlaySound(SlideState.soundString, base.gameObject);
            if (base.inputBank && base.characterDirection)
            {
                base.characterDirection.forward = ((base.inputBank.moveVector == Vector3.zero) ? base.characterDirection.forward : base.inputBank.moveVector).normalized;
            }
            if (base.characterMotor)
            {
                this.startedStateGrounded = base.characterMotor.isGrounded;
            }
            base.characterBody.SetSpreadBloom(0f, false);
            if (!this.startedStateGrounded)
            {
                this.PlayAnimation("Body", "Jump");
                Vector3 velocity = base.characterMotor.velocity;
                velocity.y = base.characterBody.jumpPower;
                base.characterMotor.velocity = velocity;
                return;
            }
            AkSoundEngine.PostEvent(3686556480, base.gameObject);
            if (SlideState.slideEffectPrefab)
            {
                Transform parent = base.FindModelChild("Spine");
                this.slideEffectInstance = UnityEngine.Object.Instantiate<GameObject>(SlideState.slideEffectPrefab, parent);
            }
            if(base.skillLocator.primary.skillNameToken == prefix + "LUNARGOLEM_NAME")
            {
                base.skillLocator.secondary.AddOneStock();
                base.skillLocator.utility.AddOneStock();
                base.skillLocator.special.AddOneStock();

            }
            if (base.skillLocator.secondary.skillNameToken == prefix + "LUNARGOLEM_NAME")
            {
                base.skillLocator.primary.AddOneStock();
                base.skillLocator.utility.AddOneStock();
                base.skillLocator.special.AddOneStock();

            }
            if (base.skillLocator.utility.skillNameToken == prefix + "LUNARGOLEM_NAME")
            {
                base.skillLocator.primary.AddOneStock();
                base.skillLocator.secondary.AddOneStock();
                base.skillLocator.special.AddOneStock();

            }
            if (base.skillLocator.special.skillNameToken == prefix + "LUNARGOLEM_NAME")
            {
                base.skillLocator.primary.AddOneStock();
                base.skillLocator.secondary.AddOneStock();
                base.skillLocator.utility.AddOneStock();

            }

        }

        public override void OnExit()
        {
            this.PlayAnimation("Fullbody, Override", "BufferEmpty");
            base.OnExit();
            this.PlayImpactAnimation();
            if (this.slideEffectInstance)
            {
                EntityState.Destroy(this.slideEffectInstance);
            }
        }
        private void PlayImpactAnimation()
        {
            Animator modelAnimator = base.GetModelAnimator();
            int layerIndex = modelAnimator.GetLayerIndex("Impact");
            if (layerIndex >= 0)
            {
                modelAnimator.SetLayerWeight(layerIndex, 1f);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.isAuthority)
            {
                float num = this.startedStateGrounded ? slideDuration : jumpDuration;
                if (base.inputBank && base.characterDirection)
                {
                    base.characterDirection.moveVector = base.inputBank.moveVector;
                    this.forwardDirection = base.characterDirection.forward;
                }
                if (base.characterMotor)
                {
                    float num2;
                    if (this.startedStateGrounded)
                    {
                        num2 = SlideState.forwardSpeedCoefficientCurve.Evaluate(base.fixedAge / num);
                    }
                    else
                    {
                        num2 = SlideState.jumpforwardSpeedCoefficientCurve.Evaluate(base.fixedAge / num);
                    }
                    base.characterMotor.rootMotion += num2 * this.moveSpeedStat * this.forwardDirection * Time.fixedDeltaTime;
                }
                if (base.fixedAge >= num)
                {
                    this.outer.SetNextStateToMain();
                }
            }

        }


        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
