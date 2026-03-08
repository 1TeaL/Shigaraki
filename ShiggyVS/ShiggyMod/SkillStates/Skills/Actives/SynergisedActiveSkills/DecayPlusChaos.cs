using EntityStates;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Networking;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class DecayPlusChaos : Skill
    {
        private BlastAttack blastAttack;
        public float radius = Config.DecayPlusChaosRange.Value;

        //Rex + decay
        public override void OnEnter()
        {
            baseDuration = StaticValues.decayPlusChaosDuration;

            base.OnEnter();
            //play animation

            if (!characterMotor.isGrounded)
            {
                this.outer.SetNextStateToMain();
                return;
            }
            else
            {

                base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
                base.PlayAnimation("FullBody, Override", "FullBodySlam", "Attack.playbackRate", duration);
                if (base.isAuthority)
                {
                    if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
                }

            }

            Ray aimRay = base.GetAimRay();


            //blastAttack = new BlastAttack();
            //blastAttack.radius = radius;
            //blastAttack.procCoefficient = StaticValues.decayPlusChaosProcCoefficient;
            //blastAttack.position = characterBody.footPosition;
            //blastAttack.attacker = base.gameObject;
            //blastAttack.crit = characterBody.RollCrit();
            //blastAttack.baseDamage = damageStat * Modules.StaticValues.decayPlusChaosDamageCoefficient;
            //blastAttack.falloffModel = BlastAttack.FalloffModel.None;
            //blastAttack.baseForce = StaticValues.decayPlusChaosForce;
            //blastAttack.bonusForce = new Vector3(0, StaticValues.decayPlusChaosForce, 0);
            //blastAttack.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
            //blastAttack.damageType = new DamageTypeCombo(DamageType.Generic, DamageTypeExtended.Generic, DamageSource.Secondary);
            //blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;

            //DamageAPI.AddModdedDamageType(blastAttack, Damage.shiggyDecay);
        }

        public override void OnExit()
        {
            base.OnExit();

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge > fireTime && !hasFired && characterMotor.isGrounded)
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

                new SpendHealthNetworkRequest(characterBody.masterObjectId, characterBody.healthComponent.combinedHealth * Config.DecayPlusChaosHealthCost.Value).Send(NetworkDestination.Clients);
                //BlastAttack.Result result = blastAttack.Fire();
                //if (result.hitCount > 0)
                //{
                //    this.OnHitEnemyAuthority(result);
                //}
                //play animation and more particles

            }

            if (base.fixedAge > duration)
            {
                this.outer.SetNextStateToMain();
                return;
            }

        }

        public void DecayPlusChaosGroundedHit()
        {
            BullseyeSearch search = new BullseyeSearch();
            search.teamMaskFilter = TeamMask.GetEnemyTeams(characterBody.teamComponent.teamIndex);
            search.filterByLoS = false;
            search.searchOrigin = characterBody.corePosition;
            search.searchDirection = Vector3.up;
            search.sortMode = BullseyeSearch.SortMode.Distance;
            search.maxDistanceFilter = radius;
            search.maxAngleFilter = 360f;

            search.RefreshCandidates();
            search.FilterOutGameObject(characterBody.gameObject);

            foreach (HurtBox hb in search.GetResults())
            {
                CharacterBody body = hb.healthComponent.body;
                if (!body) continue;

                if (!body.characterMotor)
                {
                    if(Physics.Raycast(body.corePosition, Vector3.down, 1.5f, LayerIndex.world.mask))
                    {
                        DamageInfo damageInfo = new DamageInfo
                        {
                            attacker = gameObject,
                            inflictor = gameObject,
                            damage = damageStat * Config.DecayPlusChaosDamage.Value,
                            damageType = new DamageTypeCombo(DamageType.Generic, DamageTypeExtended.Generic, DamageSource.Secondary),
                            crit = characterBody.RollCrit(),
                            position = hb.transform.position
                        };
                        DamageAPI.AddModdedDamageType(damageInfo, Damage.shiggyDecay);

                        hb.healthComponent.TakeDamage(damageInfo);
                    }
                }
                else if (body.characterMotor)
                {
                    if (body.characterMotor.isGrounded)
                    {
                        DamageInfo damageInfo = new DamageInfo
                        {
                            attacker = gameObject,
                            inflictor = gameObject,
                            damage = damageStat * Config.DecayPlusChaosDamage.Value,
                            damageType = new DamageTypeCombo(DamageType.Generic, DamageTypeExtended.Generic, DamageSource.Secondary),
                            crit = characterBody.RollCrit(),
                            position = hb.transform.position
                        };
                        DamageAPI.AddModdedDamageType(damageInfo, Damage.shiggyDecay);

                        hb.healthComponent.TakeDamage(damageInfo);
                    }
                }
                 


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