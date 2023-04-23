﻿using ShiggyMod.Modules.Survivors;
using EntityStates;
using RoR2;
using UnityEngine;
using System.Collections.Generic;
using ShiggyMod.Modules;
using UnityEngine.Networking;
using RoR2.ExpansionManagement;
using ExtraSkillSlots;
using R2API.Networking;
using System;

namespace ShiggyMod.SkillStates
{
    public class SweepingBeam : Skill
    {
        //stone golem + bullet laser

        public float baseDuration = 1f;
        public float duration;
        private float fireTime;
        private BulletAttack bulletAttack;
        public ShiggyController Shiggycon;
        private DamageType damageType;

        private float range = 100f;
        private float damageCoefficient = Modules.StaticValues.sweepingBeamDamageCoefficient;
        private float procCoefficient = Modules.StaticValues.sweepingBeamProcCoefficient;
        private float force = 100f;
        private string muzzleString;
        private uint totalBullets;

        private Vector3 direction;
        private float stopwatch;
        private float maxAngle = 120f;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("LeftArm, Override", "LeftArmPunch", "Attack.playbackRate", fireTime, 0.1f);
            AkSoundEngine.PostEvent(3660048432, base.gameObject);
            this.muzzleString = "LHand";
            EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
            EffectManager.SimpleMuzzleFlash(Modules.Assets.voidfiendblinkmuzzleEffect, base.gameObject, this.muzzleString, false);


            Shiggycon = gameObject.GetComponent<ShiggyController>();


            totalBullets = Modules.StaticValues.sweepingBeamTotalBullets;
            fireTime = duration / totalBullets;

            PlayCrossfade("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", fireTime, 0.1f);


            direction = (Quaternion.Euler(0f, -maxAngle/2f, 0f) * aimRay.direction).normalized;

        }
        public override void OnExit()
        {
            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(stopwatch > fireTime)
            {
                stopwatch = 0f;
                FireBeam(direction);
            }
            else
            {
                stopwatch += Time.fixedDeltaTime;
            }

            if(base.fixedAge > duration)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }


        public void FireBeam(Vector3 direction)
        {
            Ray aimRay = base.GetAimRay();

            bulletAttack = new BulletAttack
            {
                bulletCount = 1,
                aimVector = direction,
                origin = aimRay.origin,
                damage = this.damageStat * damageCoefficient,
                damageColorIndex = DamageColorIndex.Default,
                damageType = damageType,
                falloffModel = BulletAttack.FalloffModel.None,
                maxDistance = range,
                force = force,
                hitMask = LayerIndex.CommonMasks.bullet,
                minSpread = 0f,
                maxSpread = 0f,
                isCrit = base.RollCrit(),
                owner = base.gameObject,
                muzzleName = muzzleString,
                smartCollision = false,
                procChainMask = default(ProcChainMask),
                procCoefficient = procCoefficient,
                radius = 1f,
                sniper = false,
                stopperMask = LayerIndex.noCollision.mask,
                weapon = null,
                tracerEffectPrefab = Modules.Assets.VoidFiendBeamTracer,
                spreadPitchScale = 0f,
                spreadYawScale = 0f,
                queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                hitEffectPrefab = EntityStates.Commando.CommandoWeapon.FirePistol2.hitEffectPrefab,

            };
            bulletAttack.Fire();

            this.direction = (Quaternion.Euler(0f, maxAngle/totalBullets, 0f) * this.direction).normalized;

        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}