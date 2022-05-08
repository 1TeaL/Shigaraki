using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using EntityStates.BeetleQueenMonster;
using RoR2.Projectile;
using System;

namespace ShiggyMod.SkillStates
{
    public class BeetleQueenShotgun : BaseSkillState
    {
        public float baseDuration = 1f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.beetlequeenDamageCoeffecient;
        private float procCoefficient = 1f;
        private float force = 500f;
        private float speedOverride = 20f;
        private float projectileHSpeed = 50f;
        private float projectileCount;
        private float yawSpread = 3f;
        private float minSpread = 0f;
        private float maxSpread = 3f;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            if (base.HasBuff(Modules.Buffs.multiplierBuff))
            {
                projectileCount = 5 * Modules.StaticValues.multiplierCoefficient;
            }
            else
            {
                projectileCount = 5;
            }
            
            string muzzleName = "RHand";
            this.duration = baseDuration / this.attackSpeedStat;
            EffectManager.SimpleMuzzleFlash(FireSpit.effectPrefab, base.gameObject, muzzleName, false);

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("RightArm, Override", "RightArmPunch", "Attack.playbackRate", duration, 0.1f);
            //base.PlayCrossfade("Gesture", "FireSpit", "FireSpit.playbackRate", this.duration, 0.1f);
            float magnitude = projectileHSpeed;
            Ray ray = aimRay;
            ray.origin = aimRay.GetPoint(0f);
            RaycastHit raycastHit;
            if (Util.CharacterRaycast(base.gameObject, ray, out raycastHit, float.PositiveInfinity, LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.Ignore))
            {
                float num = magnitude;
                Vector3 vector = raycastHit.point - aimRay.origin;
                Vector2 vector2 = new Vector2(vector.x, vector.z);
                float magnitude2 = vector2.magnitude;
                float y = Trajectory.CalculateInitialYSpeed(magnitude2 / num, vector.y);
                Vector3 a = new Vector3(vector2.x / magnitude2 * num, y, vector2.y / magnitude2 * num);
                magnitude = a.magnitude;
                aimRay.direction = a / magnitude;
            }
            EffectManager.SimpleMuzzleFlash(FireSpit.effectPrefab, base.gameObject, muzzleName, false);
            if (base.isAuthority)
            {
                for (int i = 0; i < projectileCount; i++)
                {
                    this.FireBlob(aimRay, 0f, ((float)projectileCount / 2f - (float)i) * yawSpread, magnitude);
                }
            }

            Shiggycon = gameObject.GetComponent<ShiggyController>();
            damageCoefficient *= Shiggycon.strengthMultiplier;

        }

        private void FireBlob(Ray aimRay, float v, object p, float magnitude)
        {
            throw new NotImplementedException();
        }

        private void FireBlob(Ray aimRay, float bonusPitch, float bonusYaw, float speed)
        {
            Vector3 forward = Util.ApplySpread(aimRay.direction, minSpread, maxSpread, 0.5f, 0.5f, bonusYaw, bonusPitch);
            
            ProjectileManager.instance.FireProjectile(
                FireSpit.projectilePrefab, //prefab
                aimRay.origin, //position
                Util.QuaternionSafeLookRotation(forward), //rotation
                base.gameObject, //owner
                this.damageStat * damageCoefficient, //damage
                force, //force
                Util.CheckRoll(this.critStat, base.characterBody.master), //crit
                DamageColorIndex.Default, //damage color
                null, //target
                speed); //speed
        }
        public override void OnExit()
        {
            base.OnExit();
            PlayCrossfade("RightArm, Override", "Empty", "Attack.playbackRate", 0.1f, 0.1f);
            PlayCrossfade("LeftArm, Override", "Empty", "Attack.playbackRate", 0.1f, 0.1f);
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




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
