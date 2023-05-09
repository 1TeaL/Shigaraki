using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using System.Linq;
using System;
using EntityStates.NullifierMonster;
using System.Collections.Generic;
using RoR2.Projectile;

namespace ShiggyMod.SkillStates
{
    public class VoidReaverPortal : BaseSkillState
    {
        public float baseDuration = 1f;
        public float duration;
        public ShiggyController Shiggycon;

        private float damageCoefficient = Modules.StaticValues.voidreaverDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 0f;
        private float speedOverride = -1f;

        private HurtBox target;
        private float fireTimer;
        private float fireInterval = 0.3f;
        private string muzzleString = "RHand";
        public HurtBox Target;
        private Vector3 theSpot;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);

            EffectManager.SimpleMuzzleFlash(FirePortalBomb.muzzleflashEffectPrefab, base.gameObject, muzzleString, true);
            Shiggycon = base.GetComponent<ShiggyController>();

            

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("RightArm, Override", "RightArmDetonate", "Attack.playbackRate", duration, 0.1f);
            AkSoundEngine.PostEvent("ShiggyExplosion", base.gameObject);
        }

        public void PortalFire()
        {
            EffectManager.SimpleMuzzleFlash(FirePortalBomb.muzzleflashEffectPrefab, base.gameObject, muzzleString, true);

            ProjectileManager.instance.FireProjectile(
                FirePortalBomb.portalBombProjectileEffect, //prefab
                theSpot, //position
                Quaternion.identity, //rotation
                base.gameObject, //owner
                this.damageStat * damageCoefficient, //damage
                force, //force
                Util.CheckRoll(this.critStat, base.characterBody.master), //crit
                DamageColorIndex.Default, //damage color
                null, //target
                speedOverride); //speed

        }

        public override void OnExit()
        {
            base.OnExit();
            PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.IsKeyDownAuthority())
            {
                if (base.isAuthority)
                {
                    if (Shiggycon && base.isAuthority)
                    {
                        Target = Shiggycon.GetTrackingTarget();
                    }

                    if (!Target)
                    {
                        return;
                    }
                    theSpot = Target.healthComponent.body.corePosition;

                    this.fireTimer += Time.fixedDeltaTime;
                    if (this.fireTimer >= fireInterval)
                    {
                        fireTimer = 0f;

                        this.PortalFire();
                        

                    }
                }

            }
            else
            {
                if (base.fixedAge >= this.duration)
                {
                    this.outer.SetNextStateToMain();
                }
            }

        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
