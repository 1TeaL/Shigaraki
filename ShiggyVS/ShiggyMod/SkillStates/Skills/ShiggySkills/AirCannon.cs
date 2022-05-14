using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShiggyMod.SkillStates
{
    public class AirCannon : BaseSkillState
    {
        public float baseDuration = 1f;
        public float duration;
        public ShiggyController Shiggycon;

        public GameObject blastEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/SonicBoomEffect");
        public static event Action<int> Compacted;

        public uint Distance = 60;
        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.aircannonDamageCoeffecient;
        private float procCoefficient = 1f;
        private float force = 1f;
        private float speedOverride =1f;
        private DamageType damageType;
        private int decayCount;

        public override void OnEnter()
        {
            base.OnEnter();
            damageType = DamageType.Stun1s;

            if (base.HasBuff(Modules.Buffs.impbossBuff))
            {
                damageType |= DamageType.BleedOnHit | DamageType.Stun1s;
            }

            if (base.HasBuff(Modules.Buffs.acridBuff))
            {
                damageType |= DamageType.PoisonOnHit | DamageType.Stun1s;
            }
            if (base.HasBuff(Modules.Buffs.multiplierBuff))
            {
                decayCount = (int)Modules.StaticValues.multiplierCoefficient;
            }
            else
            {
                decayCount = 1;
            }
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            damageCoefficient *= Shiggycon.strengthMultiplier;
            this.duration = this.baseDuration / this.attackSpeedStat;

            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            AkSoundEngine.PostEvent(356992735, base.gameObject);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayAnimation("Body", "Jump", "Attack.playbackRate", duration);

            base.characterMotor.disableAirControlUntilCollision = false;

            if (base.isAuthority)
            {
                Vector3 theSpot = aimRay.origin - 8 * aimRay.direction;
                Vector3 theSpot2 = aimRay.origin - 2 * aimRay.direction;

                ApplyDoT();
                BlastAttack blastAttack = new BlastAttack();
                blastAttack.radius = radius;
                blastAttack.procCoefficient = 1f;
                blastAttack.position = theSpot;
                blastAttack.attacker = base.gameObject;
                blastAttack.crit = base.RollCrit();
                blastAttack.baseDamage = this.damageStat * damageCoefficient;
                blastAttack.falloffModel = BlastAttack.FalloffModel.None;
                blastAttack.baseForce = 600f;
                blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
                blastAttack.damageType = damageType;
                blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
                BlastAttack.Result result = blastAttack.Fire();

                EffectData effectData = new EffectData();
                {
                    effectData.scale = 15;
                    effectData.origin = theSpot2;
                    effectData.rotation = Quaternion.LookRotation(new Vector3(aimRay.direction.x, aimRay.direction.y, aimRay.direction.z));
                };

                for (int i = 0; i <= 5; i++)
                {
                    float num = 10f;
                    Quaternion rotation = Util.QuaternionSafeLookRotation(-base.characterDirection.forward.normalized);
                    float num2 = 1f;
                    rotation.x += UnityEngine.Random.Range(-num2, num2) * num;
                    rotation.y += UnityEngine.Random.Range(-num2, num2) * num;
                    EffectManager.SpawnEffect(this.blastEffectPrefab, new EffectData
                    {
                        origin = theSpot2,
                        scale = 0.1f,
                        rotation = rotation
                    }, true);

                }

                base.characterMotor.velocity = Distance * aimRay.direction * moveSpeedStat / 7;

                Compacted?.Invoke(result.hitCount);
            }


        }

        public override void OnExit()
        {
            this.PlayAnimation("Fullbody, Override", "BufferEmpty");
            base.OnExit();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();



            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        public void ApplyDoT()
        {
            Ray aimRay = base.GetAimRay();
            BullseyeSearch search = new BullseyeSearch
            {

                teamMaskFilter = TeamMask.GetEnemyTeams(base.GetTeam()),
                filterByLoS = false,
                searchOrigin = base.characterBody.footPosition,
                searchDirection = UnityEngine.Random.onUnitSphere,
                sortMode = BullseyeSearch.SortMode.Distance,
                maxDistanceFilter = radius,
                maxAngleFilter = 360f
            };

            search.RefreshCandidates();
            search.FilterOutGameObject(base.gameObject);



            List<HurtBox> target = search.GetResults().ToList<HurtBox>();
            foreach (HurtBox singularTarget in target)
            {
                if (singularTarget)
                {
                    if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                    {
                        InflictDotInfo info = new InflictDotInfo();
                        info.attackerObject = base.gameObject;
                        info.victimObject = singularTarget.healthComponent.body.gameObject;
                        info.duration = Modules.StaticValues.decayDamageTimer;
                        info.dotIndex = Modules.Dots.decayDot;

                        for (int i = 0; i < decayCount; i++)
                        {
                            DotController.InflictDot(ref info);

                        }
                    }
                }
            }
        }


        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
