using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using EntityStates.LunarGolem;
using UnityEngine.Networking;
using RoR2.Projectile;
using EntityStates.Commando;
using ExtraSkillSlots;
using ShiggyMod.Modules;

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
        private ExtraSkillLocator extraskillLocator;

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
                //need better animation
                base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
                base.PlayCrossfade("FullBody, Override", "FullBodyDash", "Attack.playbackRate", slideDuration, 0.05f);
                if (base.isAuthority)
                {
                    if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
                }
                Vector3 velocity = base.characterMotor.velocity;
                velocity.y = base.characterBody.jumpPower;
                base.characterMotor.velocity = velocity;
                return;
            }
            AkSoundEngine.PostEvent(2422625683, base.gameObject);
            if (SlideState.slideEffectPrefab)
            {
                Transform parent = base.FindModelChild("Spine");
                this.slideEffectInstance = UnityEngine.Object.Instantiate<GameObject>(SlideState.slideEffectPrefab, parent);
            }

            //lower cd of all skills
            skillLocator.DeductCooldownFromAllSkillsServer(StaticValues.lunarGolemSlideCooldown);
            //if (base.skillLocator.primary.skillDef != Shiggy.lunargolemSlideDef)
            //{
            //    skillLocator.primary.AddOneStock();
            //}
            //if (base.skillLocator.secondary.skillDef != Shiggy.lunargolemSlideDef)
            //{
            //    skillLocator.secondary.AddOneStock();
            //}
            //if (base.skillLocator.utility.skillDef != Shiggy.lunargolemSlideDef)
            //{
            //    skillLocator.utility.AddOneStock();
            //}
            //if (base.skillLocator.special.skillDef != Shiggy.lunargolemSlideDef)
            //{
            //    skillLocator.special.AddOneStock();
            //}

            //extraskillLocator = base.GetComponent<ExtraSkillLocator>();

            //if(extraskillLocator.extraFirst.skillDef != Shiggy.lunargolemSlideDef)
            //{
            //    extraskillLocator.extraFirst.AddOneStock();
            //}
            //if (extraskillLocator.extraSecond.skillDef != Shiggy.lunargolemSlideDef)
            //{
            //    extraskillLocator.extraSecond.AddOneStock();
            //}
            //if (extraskillLocator.extraThird.skillDef != Shiggy.lunargolemSlideDef)
            //{
            //    extraskillLocator.extraThird.AddOneStock();
            //}
            //if (extraskillLocator.extraFourth.skillDef != Shiggy.lunargolemSlideDef)
            //{
            //    extraskillLocator.extraFourth.AddOneStock();
            //}


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
