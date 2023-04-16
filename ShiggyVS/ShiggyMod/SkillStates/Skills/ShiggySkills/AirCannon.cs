using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using System;
using System.Collections.Generic;
using System.Linq;
using R2API.Networking;
using R2API;

namespace ShiggyMod.SkillStates
{
    public class AirCannon : BaseSkillState
    {
        public float baseDuration = 0.5f;
        public float duration;
        private Ray aimRay;
        public ShiggyController Shiggycon;

        public GameObject blastEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/SonicBoomEffect");
        public static event Action<int> Compacted;

        public float SpeedCoefficient = 15f;
        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.aircannonDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 1f;
        private float speedOverride =1f;
        private DamageType damageType;
        
        private float previousMass;
        private float rollSpeed;
        private Vector3 previousPosition;
        private Vector3 aimRayDir;

        public override void OnEnter()
        {
            base.OnEnter();
            damageType = DamageType.Stun1s;
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            
            this.duration = this.baseDuration / this.attackSpeedStat;

            aimRay = base.GetAimRay();
            this.aimRayDir = aimRay.direction;
            base.characterBody.SetAimTimer(this.duration);
            AkSoundEngine.PostEvent("ShiggyAirCannon", base.gameObject);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayAnimation("Body", "Jump", "Attack.playbackRate", duration);

            base.characterMotor.disableAirControlUntilCollision = false;

            if (base.isAuthority)
            {
                Vector3 theSpot = aimRay.origin - 2 * aimRay.direction;
                Vector3 theSpot2 = aimRay.origin - 2 * aimRay.direction;

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

                blastAttack.AddModdedDamageType(Modules.Damage.shiggyDecay);

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


                Compacted?.Invoke(result.hitCount);
            }

            base.characterMotor.useGravity = false;
            this.previousMass = base.characterMotor.mass;
            base.characterMotor.mass = 0f;
            base.characterBody.bodyFlags |= CharacterBody.BodyFlags.IgnoreFallDamage;


            this.RecalculateRollSpeed();

            if (base.characterMotor && base.characterDirection)
            {
                base.characterMotor.velocity = aimRay.direction * this.rollSpeed;
            }

            Vector3 b = base.characterMotor ? base.characterMotor.velocity : Vector3.zero;
            this.previousPosition = base.transform.position - b;

        }
        private void RecalculateRollSpeed()
        {
            this.rollSpeed = this.moveSpeedStat * SpeedCoefficient;
        }

        public override void OnExit()
        {
            this.PlayAnimation("Fullbody, Override", "BufferEmpty");
            base.OnExit();

            base.characterMotor.mass = this.previousMass;
            base.characterMotor.useGravity = true;
            base.characterMotor.velocity = Vector3.zero;
            if (base.cameraTargetParams) base.cameraTargetParams.fovOverride = -1f;
            base.characterMotor.disableAirControlUntilCollision = false;
            base.characterMotor.velocity.y = 0;
            base.characterBody.bodyFlags &= ~CharacterBody.BodyFlags.IgnoreFallDamage;
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            this.RecalculateRollSpeed();
            Vector3 normalized = (base.transform.position - this.previousPosition).normalized;
            if (base.characterDirection) base.characterDirection.forward = this.aimRayDir;
            if (base.characterMotor && base.characterDirection && normalized != Vector3.zero)
            {
                Vector3 vector = normalized * this.rollSpeed;
                float d = Mathf.Max(Vector3.Dot(vector, this.aimRay.direction), 0f);
                vector = this.aimRay.direction * d;

                base.characterMotor.velocity = vector;
            }
            this.previousPosition = base.transform.position;


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
