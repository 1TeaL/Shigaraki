using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using RoR2.Skills;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class AlloyVultureFly : BaseSkillState
    {
        public ShiggyController Shiggycon;

        public GameObject blastEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/SonicBoomEffect");
        public static float duration = 1f;
        public float rollSpeed;
        public float initialspeedCoefficient = 10f;
        public static float slamRadius = 5f;
        private Transform modelTransform;
        private Vector3 flyVector = Vector3.zero;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();

            this.modelTransform = base.GetModelTransform();
            this.flyVector = Vector3.up;

            for (int i = 0; i <= 5; i++)
            {
                float num = 60f;
                Quaternion rotation = Util.QuaternionSafeLookRotation(base.characterDirection.forward.normalized);
                float num2 = 0.01f;
                rotation.x += UnityEngine.Random.Range(-num2, num2) * num;
                rotation.y += UnityEngine.Random.Range(-num2, num2) * num;
                EffectManager.SpawnEffect(this.blastEffectPrefab, new EffectData
                {
                    origin = base.transform.position,
                    scale = slamRadius,
                    rotation = rotation
                }, true);

            }

            base.characterBody.bodyFlags |= CharacterBody.BodyFlags.IgnoreFallDamage;
            base.characterMotor.useGravity = false;
            base.characterMotor.Motor.RebuildCollidableLayers();

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("Body", "Jump", 0.1f);
            AkSoundEngine.PostEvent(2381833256, base.gameObject);

            bool active = NetworkServer.active;
            if (active)
            {
                base.characterBody.AddTimedBuffAuthority(Modules.Buffs.flyBuff.buffIndex, Modules.StaticValues.alloyvultureflyduration);
            }

            RecalculateRollSpeed();
            //Ray aimray = base.GetAimRay();

            if (base.characterMotor && base.characterDirection)
            {
                base.characterMotor.velocity.y = rollSpeed;
                //base.characterMotor.velocity = (characterBody.transform.position - enemyPosition).normalized * this.rollSpeed;
            }

        }

        public override void OnExit()
        {
            base.characterMotor.velocity.y = 0f;
            base.gameObject.layer = LayerIndex.defaultLayer.intVal;
            base.characterMotor.Motor.RebuildCollidableLayers();
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
            this.rollSpeed = num * Mathf.Lerp(initialspeedCoefficient, 0, base.fixedAge / duration);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            RecalculateRollSpeed();
            if (base.characterMotor && base.characterDirection)
            {
                base.characterMotor.velocity.y = rollSpeed;
            }


            if (base.fixedAge >= duration)
            {

                this.outer.SetNextStateToMain();
            }
        }
    




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
