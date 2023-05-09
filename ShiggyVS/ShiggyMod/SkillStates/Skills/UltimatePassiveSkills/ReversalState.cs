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

namespace ShiggyMod.SkillStates
{
    public class ReversalState : BaseSkillState
    {

        public CharacterBody enemycharBody;
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

            Ray aimRay = base.GetAimRay();
            Vector3 tpPosition = (enemycharBody.corePosition - characterBody.corePosition).normalized * 2f;
            tpPosition.y = 0;
            characterBody.characterMotor.Motor.SetPosition(tpPosition);


            base.characterDirection.forward = (enemycharBody.corePosition - characterBody.corePosition);
            
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
                    base.characterMotor.rootMotion += num2 * this.moveSpeedStat * attackSpeedStat * this.forwardDirection * Time.fixedDeltaTime * StaticValues.reversalSpeedCoefficient;
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

            Ray aimRay = base.GetAimRay();

            blastAttack = new BlastAttack();
            blastAttack.radius = StaticValues.reversalRadius;
            blastAttack.procCoefficient = StaticValues.reversalProcCoefficient;
            blastAttack.position = enemycharBody.corePosition;
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
                origin = enemycharBody.corePosition,
                scale = StaticValues.reversalRadius,

            }, true);


            int reversalBuffCount = characterBody.GetBuffCount(Buffs.reversalBuffStacks);
            characterBody.ApplyBuff(Buffs.reversalBuffStacks.buffIndex, reversalBuffCount - 1);
        }

    }
}