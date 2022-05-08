﻿using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using EntityStates.ScavMonster;

namespace ShiggyMod.SkillStates
{
    public class ScavengerThqwibs : BaseSkillState
    {
        public float baseDuration = 1f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.scavengerDamageCoeffecient;
        private float procCoefficient = 1f;
        private float force = 1f;
        private float speedOverride = -1f;
        private float projectileVelocity = 20f;
        private float minimumDistance = 5f;
        private float timeToTarget = 0.3f;
        private float minSpread = 0f;
        private float maxSpread = 5f;
        private string muzzleName = "LHand";
        private int projectileCount;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);

            Util.PlayAttackSpeedSound(ThrowSack.sound, base.gameObject, this.attackSpeedStat);
            //base.PlayAnimation("Body", "ThrowSack", "ThrowSack.playbackRate", this.duration);
            if (ThrowSack.effectPrefab)
            {
                EffectManager.SimpleMuzzleFlash(ThrowSack.effectPrefab, base.gameObject, muzzleName, false);
            }
            if (base.HasBuff(Modules.Buffs.multiplierBuff))
            {
                projectileCount = Modules.StaticValues.scavengerProjectileCount * (int)Modules.StaticValues.multiplierCoefficient;
            }
            else
            {
                projectileCount = Modules.StaticValues.scavengerProjectileCount;
            }

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("LeftArm, Override", "LeftArmPunch", "Attack.playbackRate", duration, 0.1f);
            this.Fire();

            Shiggycon = gameObject.GetComponent<ShiggyController>();
            damageCoefficient *= Shiggycon.rangedMultiplier;

        }
        private void Fire()
        {
            Ray aimRay = base.GetAimRay();
            Ray ray = aimRay;
            Ray ray2 = aimRay;
            Vector3 point = aimRay.GetPoint(ThrowSack.minimumDistance);
            bool flag = false;
            RaycastHit raycastHit;
            if (Util.CharacterRaycast(base.gameObject, ray, out raycastHit, 500f, LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.Ignore))
            {
                point = raycastHit.point;
                flag = true;
            }
            float magnitude = projectileVelocity;
            if (flag)
            {
                Vector3 vector = point - ray2.origin;
                Vector2 a = new Vector2(vector.x, vector.z);
                float magnitude2 = a.magnitude;
                Vector2 vector2 = a / magnitude2;
                if (magnitude2 < minimumDistance)
                {
                    magnitude2 = minimumDistance;
                }
                float y = Trajectory.CalculateInitialYSpeed(timeToTarget, vector.y);
                float num = magnitude2 / timeToTarget;
                Vector3 direction = new Vector3(vector2.x * num, y, vector2.y * num);
                magnitude = direction.magnitude;
                ray2.direction = direction;
            }
            for (int i = 0; i < projectileCount; i++)
            {
                Quaternion rotation = Util.QuaternionSafeLookRotation(Util.ApplySpread(ray2.direction, minSpread, maxSpread, 1f, 1f, 0f, 0f));

                ProjectileManager.instance.FireProjectile(
                    ThrowSack.projectilePrefab, //prefab
                    FindModelChild(muzzleName).position, //position
                    rotation, //rotation
                    base.gameObject, //owner
                    this.damageStat * damageCoefficient, //damage
                    force, //force
                    Util.CheckRoll(this.critStat, base.characterBody.master), //crit
                    DamageColorIndex.Item, //damage color
                    null, //target
                    magnitude); //speed }} 


            }
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
