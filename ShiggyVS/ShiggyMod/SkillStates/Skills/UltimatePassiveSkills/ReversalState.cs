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
using static UnityEngine.ParticleSystem.PlaybackState;
using EntityStates.Merc;
using EntityStates.Commando;
using EntityStates.Huntress;

namespace ShiggyMod.SkillStates
{
    public class ReversalState : BaseSkillState
    {

        public Vector3 enemyPos;
        private BlastAttack blastAttack;
        private Vector3 forwardDirection;
        private float baseslideDuration = StaticValues.reversalDuration;
        private float basejumpDuration = StaticValues.reversalDuration / 2f;
        private float slideDuration;
        private float jumpDuration;
        private bool startedStateGrounded;
        private CharacterModel characterModel;
        private HurtBoxGroup hurtboxGroup;

        public override void OnEnter()
        {
            base.OnEnter();
            slideDuration = baseslideDuration / attackSpeedStat;
            basejumpDuration= basejumpDuration / attackSpeedStat;


            Ray aimRay = base.GetAimRay();

            forwardDirection = (characterBody.corePosition - enemyPos).normalized;
            characterDirection.forward = forwardDirection; 

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

            if (this.hurtboxGroup)
            {
                HurtBoxGroup hurtBoxGroup = this.hurtboxGroup;
                int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter + 1;
                hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
            }
            this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
            Util.PlaySound(BaseBeginArrowBarrage.blinkSoundString, base.gameObject);
        }

        private void CreateBlinkEffect(Vector3 origin)
        {
            EffectData effectData = new EffectData();
            effectData.rotation = Util.QuaternionSafeLookRotation(this.forwardDirection);
            effectData.origin = origin;
            EffectManager.SpawnEffect(EvisDash.blinkPrefab, effectData, false);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.isAuthority)
            {
                float num = this.startedStateGrounded ? slideDuration : jumpDuration;
                //if (base.inputBank && base.characterDirection)
                //{
                //    base.characterDirection.moveVector = base.inputBank.moveVector;
                //    this.forwardDirection = base.characterDirection.forward;
                //}
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
                    base.characterMotor.rootMotion += num2 * this.forwardDirection * Time.fixedDeltaTime * StaticValues.reversalSpeedCoefficient;
                }
                if (base.fixedAge >= num)
                {
                    this.outer.SetNextStateToMain();
                    return;
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
            
            if (this.hurtboxGroup)
            {
                HurtBoxGroup hurtBoxGroup = this.hurtboxGroup;
                int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter - 1;
                hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
            }

            Ray aimRay = base.GetAimRay();

            blastAttack = new BlastAttack();
            blastAttack.radius = StaticValues.reversalRadius;
            blastAttack.procCoefficient = StaticValues.reversalProcCoefficient;
            blastAttack.position = enemyPos;
            blastAttack.attacker = base.gameObject;
            blastAttack.crit = Util.CheckRoll(characterBody.crit, characterBody.master);
            blastAttack.baseDamage = characterBody.damage * Modules.StaticValues.reversalDamageCoefficient;
            blastAttack.falloffModel = BlastAttack.FalloffModel.None;
            blastAttack.baseForce = 500f;
            blastAttack.bonusForce = Vector3.up * blastAttack.baseForce;
            blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
            blastAttack.damageType = DamageType.Freeze2s;
            blastAttack.attackerFiltering = AttackerFiltering.Default;
            blastAttack.Fire(); 

            EffectManager.SpawnEffect(EntityStates.JellyfishMonster.JellyNova.novaEffectPrefab, new EffectData
            {
                origin = enemyPos,
                scale = StaticValues.reversalRadius,

            }, true);


            int reversalBuffCount = characterBody.GetBuffCount(Buffs.reversalBuffStacks);
            characterBody.ApplyBuff(Buffs.reversalBuffStacks.buffIndex, reversalBuffCount - 1);
        }

    }
}