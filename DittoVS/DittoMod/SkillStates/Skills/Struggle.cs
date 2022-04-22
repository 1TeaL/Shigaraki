using EntityStates;
using RoR2;
using UnityEngine;

namespace DittoMod.SkillStates
{
    public class Struggle : BaseSkillState
    {

        private float baseduration = 1f;
        private float duration;
        private float fireTime;
        private float struggleAge;
        private bool hasFired;

        public float force = 100f;
        public float initialspeedCoefficient = 6f;
        public static float procCoefficient = 1f;
        private Vector3 theSpot;
        private BlastAttack blastAttack;
        public float blastRadius = 3f;
        public float rollSpeed;

        public override void OnEnter()
        {
            base.OnEnter();
            hasFired = false;
            duration = baseduration / attackSpeedStat;
            fireTime = duration / 2;

            RecalculateRollSpeed();
            Ray aimray = base.GetAimRay();
            if (base.characterMotor && base.characterDirection)
            {
                base.characterMotor.velocity.y = this.rollSpeed;
            }

            theSpot = base.characterBody.corePosition;

            blastAttack = new BlastAttack();
            blastAttack.radius = blastRadius;
            blastAttack.procCoefficient = procCoefficient;
            blastAttack.position = theSpot;
            blastAttack.attacker = base.gameObject;
            blastAttack.crit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master);
            blastAttack.baseDamage = base.characterBody.damage * Modules.StaticValues.struggleDamageCoefficient;
            blastAttack.falloffModel = BlastAttack.FalloffModel.None;
            blastAttack.baseForce = force;
            blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
            blastAttack.damageType = DamageType.Generic;
            blastAttack.attackerFiltering = AttackerFiltering.Default;
            
            AkSoundEngine.PostEvent(500315785, this.gameObject);

            PlayAnimation("Body", "BonusJump", "Attack.playbackRate", fireTime);
            base.gameObject.layer = LayerIndex.fakeActor.intVal;
        }

        public override void OnExit()
        {
            base.gameObject.layer = LayerIndex.defaultLayer.intVal;
            base.characterMotor.velocity.y *= 0f;
            base.OnExit();
        }

        private void RecalculateRollSpeed()
        {
            float num = this.moveSpeedStat;
            bool isSprinting = base.characterBody.isSprinting;
            if (isSprinting)
            {
                num /= base.characterBody.sprintingSpeedMultiplier;
            }
            this.rollSpeed = num * Mathf.Lerp(initialspeedCoefficient, -initialspeedCoefficient/2, base.fixedAge / fireTime);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            RecalculateRollSpeed();
            Ray aimray = base.GetAimRay();

            theSpot = base.characterBody.corePosition;
            if (struggleAge > duration / 10)
            {
                blastAttack.position = theSpot;
                if (base.isAuthority)
                {
                    blastAttack.Fire();
                }
                struggleAge = 0;
            }
            else
            {
                struggleAge += Time.fixedDeltaTime;
            }
            if (base.characterMotor && base.characterDirection)
            {
                base.characterMotor.velocity.y = this.rollSpeed;
            }

            if (base.fixedAge >= this.fireTime && !this.hasFired)
            {                              
                hasFired = true;
                AkSoundEngine.PostEvent(500315785, this.gameObject);
            }


            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}