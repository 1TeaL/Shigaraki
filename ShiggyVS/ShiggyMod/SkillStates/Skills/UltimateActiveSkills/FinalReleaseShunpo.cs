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
using EntityStates.Merc;

namespace ShiggyMod.SkillStates
{
    public class FinalReleaseShunpo : BaseSkillState
    {
        public ShiggyController Shiggycon;
        private bool startedStateGrounded;

        private Vector3 forwardDirection;
        private float baseslideDuration = StaticValues.shunpoDuration;
        private float basejumpDuration = StaticValues.shunpoDuration / 2f;
        private float slideDuration;
        private float jumpDuration;
        private ExtraSkillLocator extraskillLocator;

        public override void OnEnter()
        {
            base.OnEnter();
            this.slideDuration = this.baseslideDuration / this.attackSpeedStat;
            this.jumpDuration = this.basejumpDuration / this.attackSpeedStat;

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
                //need better animation
                this.PlayAnimation("Body", "Jump");
                Vector3 velocity = base.characterMotor.velocity;
                velocity.y = base.characterBody.jumpPower;
                base.characterMotor.velocity = velocity;
                return;
            }



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
            this.PlayAnimation("Fullbody, Override", "BufferEmpty");
            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
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
