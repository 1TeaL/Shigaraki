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

namespace ShiggyMod.SkillStates
{
    public class ReversalState : BaseSkillState
    {

        public CharacterBody enemycharBody;
        private BlastAttack blastAttack;

        public override void OnEnter()
        {
            base.OnEnter();

            Ray aimRay = base.GetAimRay();
            Vector3 tpPosition = (enemycharBody.corePosition - characterBody.corePosition).normalized * 2f;
            tpPosition.y = 0;
            characterBody.characterMotor.Motor.SetPosition(tpPosition);


        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if(base.fixedAge > 0.5f)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        public override void OnExit()
        {
            base.OnExit();

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