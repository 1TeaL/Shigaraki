﻿using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using EntityStates.ClayBruiser.Weapon;

namespace ShiggyMod.SkillStates
{
    public class ClayTemplarMinigun : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";

        public float baseDuration = 1f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.claytemplarminigunDamageCoeffecient;
        private float procCoefficient = 0.05f;
        private float force = 1f;
        private float speedOverride = -1f;
        private string muzzleString;
        private float baseFireInterval = 0.1f;
        private float baseBulletCount;
        private float baseFireRate;

        private Run.FixedTimeStamp critEndTime;
        private Run.FixedTimeStamp lastCritCheck;
        private float fireTimer;
        private float bulletMaxDistance = 100f;

        public override void OnEnter()
        {
            base.OnEnter();
            damageType = DamageType.ClayGoo;

            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            this.muzzleString = "RHand";


            this.baseFireRate = 1f / baseFireInterval;
            if (base.HasBuff(Modules.Buffs.multiplierBuff))
            {
                baseBulletCount = 3f * Modules.StaticValues.multiplierCoefficient;
            }
            else
            {
                baseBulletCount = 3f;
            }
            this.critEndTime = Run.FixedTimeStamp.negativeInfinity;
            this.lastCritCheck = Run.FixedTimeStamp.negativeInfinity;
            Util.PlaySound(MinigunFire.startSound, base.gameObject);
        }
        private void UpdateCrits()
        {
            if (this.lastCritCheck.timeSince >= 1f)
            {
                this.lastCritCheck = Run.FixedTimeStamp.now;
                if (base.RollCrit())
                {
                    this.critEndTime = Run.FixedTimeStamp.now + 2f;
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Util.PlaySound(MinigunFire.endSound, base.gameObject);
            //if (this.muzzleVfxTransform)
            //{
            //    EntityState.Destroy(this.muzzleVfxTransform.gameObject);
            //    this.muzzleVfxTransform = null;
            //}
            //base.PlayCrossfade("Gesture, Additive", "BufferEmpty", 0.2f);
        }

        private void OnFireShared()
        {
            Util.PlaySound(MinigunFire.fireSound, base.gameObject);
            if (base.isAuthority)
            {
                base.characterBody.SetAimTimer(this.duration);
                this.OnFireAuthority();
            }
        }
        private void OnFireAuthority()
        {
            this.UpdateCrits();
            bool isCrit = !this.critEndTime.hasPassed;

            EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);

            Ray aimRay = base.GetAimRay();
            new BulletAttack
            {
                bulletCount = (uint)baseBulletCount,
                aimVector = aimRay.direction,
                origin = aimRay.origin,
                damage = damageCoefficient,
                damageColorIndex = DamageColorIndex.Default,
                damageType = damageType,
                falloffModel = BulletAttack.FalloffModel.None,
                maxDistance = bulletMaxDistance,
                force = force,
                hitMask = LayerIndex.CommonMasks.bullet,
                minSpread = MinigunFire.bulletMinSpread,
                maxSpread = MinigunFire.bulletMaxSpread,
                isCrit = isCrit,
                owner = base.gameObject,
                muzzleName = this.muzzleString,
                smartCollision = false,
                procChainMask = default(ProcChainMask),
                procCoefficient = procCoefficient,
                radius = 0.4f,
                sniper = false,
                stopperMask = LayerIndex.CommonMasks.bullet,
                weapon = null,
                tracerEffectPrefab = MinigunFire.bulletTracerEffectPrefab,
                spreadPitchScale = 1f,
                spreadYawScale = 1f,
                queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                hitEffectPrefab = MinigunFire.bulletHitEffectPrefab,
                HitEffectNormal = MinigunFire.bulletHitEffectNormal
            }.Fire();
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.IsKeyDownAuthority())
            {
                this.fireTimer -= Time.fixedDeltaTime;
                if (this.fireTimer <= 0f)
                {
                    float num = baseFireInterval / this.attackSpeedStat;
                    this.fireTimer += num;
                    this.OnFireShared();
                }

            }
            else
            {
                if (base.isAuthority)
                {
                    this.outer.SetNextStateToMain();
                    return;

                }

            }


        }



        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
