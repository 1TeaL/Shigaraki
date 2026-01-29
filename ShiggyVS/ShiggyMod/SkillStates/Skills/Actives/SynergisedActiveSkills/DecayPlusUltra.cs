using EntityStates;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Networking;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class DecayPlusUltra : Skill
    {
        private BlastAttack blastAttack;
        public float radius = StaticValues.decayPlusUltraRadius;

        //Rex + decay
        public override void OnEnter()
        {
            baseDuration = StaticValues.decayPlusUltraDuration;
            base.OnEnter();
            //play animation

            Ray aimRay = base.GetAimRay();


            blastAttack = new BlastAttack();
            blastAttack.radius = radius;
            blastAttack.procCoefficient = StaticValues.decayPlusUltraProcCoefficient;
            blastAttack.position = characterBody.footPosition;
            blastAttack.attacker = base.gameObject;
            blastAttack.crit = characterBody.RollCrit();
            blastAttack.baseDamage = damageStat * Modules.StaticValues.decayPlusUltraDamageCoefficient;
            blastAttack.falloffModel = BlastAttack.FalloffModel.None;
            blastAttack.baseForce = StaticValues.decayPlusUltraForce;
            blastAttack.bonusForce = new Vector3(0, StaticValues.decayPlusUltraForce, 0);
            blastAttack.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
            blastAttack.damageType = new DamageTypeCombo(DamageType.Generic, DamageTypeExtended.Generic, DamageSource.Secondary);
            blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;

            DamageAPI.AddModdedDamageType(blastAttack, Damage.shiggyDecay);

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            base.PlayAnimation("FullBody, Override", "FullBodySlam", "Attack.playbackRate", duration);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
        }

        public override void OnExit()
        {
            base.OnExit();

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge > fireTime && !hasFired)
            {
                hasFired = true;
                Ray aimRay = base.GetAimRay();
                EffectManager.SpawnEffect(ShiggyAsset.decayattackEffect, new EffectData
                {
                    origin = characterBody.footPosition,
                    scale = 1f,
                    rotation = Util.QuaternionSafeLookRotation(Vector3.up),

                }, true);
                EffectManager.SpawnEffect(EntityStates.BeetleGuardMonster.GroundSlam.slamEffectPrefab, new EffectData
                {
                    origin = characterBody.corePosition,
                    scale = radius,
                    rotation = Quaternion.identity,

                }, true);
                EffectManager.SpawnEffect(EntityStates.JellyfishMonster.JellyNova.novaEffectPrefab, new EffectData
                {
                    origin = characterBody.corePosition,
                    scale = radius,
                    rotation = Quaternion.identity,

                }, true);

                new SpendHealthNetworkRequest(characterBody.masterObjectId, characterBody.healthComponent.combinedHealth * StaticValues.decayPlusUltraHealthCostCoefficient).Send(NetworkDestination.Clients);
                BlastAttack.Result result = blastAttack.Fire();
                if (result.hitCount > 0)
                {
                    this.OnHitEnemyAuthority(result);
                }
                //play animation and more particles
            }

            if (base.fixedAge > duration)
            {
                this.outer.SetNextStateToMain();
                return;
            }

        }


        protected virtual void OnHitEnemyAuthority(BlastAttack.Result result)
        {
            foreach (BlastAttack.HitPoint hitpoint in result.hitPoints)
            {
                //play effect

                EffectManager.SpawnEffect(ShiggyAsset.shiggyHitImpactEffect, new EffectData
                {
                    origin = hitpoint.hurtBox.transform.position,
                    scale = 1f,
                    rotation = Quaternion.identity,

                }, true);

            }

        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}