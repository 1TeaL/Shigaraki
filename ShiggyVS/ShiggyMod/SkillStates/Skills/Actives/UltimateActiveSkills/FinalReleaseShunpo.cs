using EntityStates;
using EntityStates.Commando;
using EntityStates.Merc;
using RoR2;
using ShiggyMod.Modules;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class FinalReleaseShunpo : Skill
    {
        private bool startedStateGrounded;

        private Vector3 forwardDirection;
        private float baseslideDuration = StaticValues.shunpoDuration;
        private float basejumpDuration = StaticValues.shunpoDuration;
        private float slideDuration;
        private float jumpDuration;
        private CharacterModel characterModel;
        private HurtBoxGroup hurtboxGroup;

        public override void OnEnter()
        {
            base.OnEnter();
            this.slideDuration = this.baseslideDuration / this.attackSpeedStat;
            this.jumpDuration = this.basejumpDuration / this.attackSpeedStat;

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            this.animator.SetBool("attacking", true);
            base.PlayCrossfade("FullBody, Override", "FullBodyDashThunder", "Attack.playbackRate", slideDuration, 0.05f);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            //AkSoundEngine.PostEvent("ShiggySlide", base.gameObject);

            if (base.inputBank && base.characterDirection)
            {
                base.characterDirection.forward = ((base.inputBank.moveVector == Vector3.zero) ? base.characterDirection.forward : base.inputBank.moveVector).normalized;
            }
            if (base.characterMotor)
            {
                this.startedStateGrounded = base.characterMotor.isGrounded;
            }
            if (!this.startedStateGrounded)
            {
                Vector3 velocity = base.characterMotor.velocity;
                velocity.y = base.characterBody.jumpPower;
                base.characterMotor.velocity = velocity;
                return;
            }

            if (this.characterModel)
            {
                this.characterModel.invisibilityCount++;
            }
            if (this.hurtboxGroup)
            {
                HurtBoxGroup hurtBoxGroup = this.hurtboxGroup;
                int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter + 1;
                hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
            }
            this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));

            Util.PlaySound(Assaulter2.endSoundString, base.gameObject);

        }
        private void CreateBlinkEffect(Vector3 origin)
        {
            EffectData effectData = new EffectData();
            effectData.rotation = Util.QuaternionSafeLookRotation(this.forwardDirection);
            effectData.origin = origin;
            EffectManager.SpawnEffect(EvisDash.blinkPrefab, effectData, false);
        }

        public override void OnExit()
        {
            base.OnExit();
            base.GetModelAnimator().SetBool("attacking", false);
            this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
            if (this.characterModel)
            {
                this.characterModel.invisibilityCount--;
            }
            if (this.hurtboxGroup)
            {
                HurtBoxGroup hurtBoxGroup = this.hurtboxGroup;
                int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter - 1;
                hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
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
                    base.characterMotor.rootMotion += num2 * this.moveSpeedStat * attackSpeedStat * this.forwardDirection * Time.fixedDeltaTime * StaticValues.shunpoSpeedCoefficient;
                }
                if (base.fixedAge >= num)
                {
                    this.outer.SetNextStateToMain();
                    return;
                }
            }

        }


        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }

    }
}
