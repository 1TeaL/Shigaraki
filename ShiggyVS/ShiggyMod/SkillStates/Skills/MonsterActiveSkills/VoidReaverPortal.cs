using EntityStates;
using EntityStates.NullifierMonster;
using ExtraSkillSlots;
using RoR2;
using RoR2.Projectile;
using ShiggyMod.Modules.Survivors;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class VoidReaverPortal : Skill
    {

        private float damageCoefficient = Modules.StaticValues.voidreaverDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 0f;
        private float speedOverride = -1f;

        private HurtBox target;
        private float fireTimer;
        private float fireInterval = 0.3f;
        private string muzzleString = "RHand";
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
            base.GetModelAnimator().SetBool("attacking", true);
            PlayCrossfade("RightArm, Override", "RArmOutStart", "Attack.playbackRate", duration, 0.1f);
            //need hand to stay out the whole time
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
            base.GetModelAnimator().SetBool("attacking", false);
            PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.inputBank.skill1.down && characterBody.skillLocator.primary.skillDef == Shiggy.voidreaverportalDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill2.down && characterBody.skillLocator.secondary.skillDef == Shiggy.voidreaverportalDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill3.down && characterBody.skillLocator.utility.skillDef == Shiggy.voidreaverportalDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill4.down && characterBody.skillLocator.special.skillDef == Shiggy.voidreaverportalDef)
            {

                keepFiring = true;
            }
            else if (extrainputBankTest.extraSkill1.down && extraskillLocator.extraFirst.skillDef == Shiggy.voidreaverportalDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill2.down && extraskillLocator.extraSecond.skillDef == Shiggy.voidreaverportalDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill3.down && extraskillLocator.extraThird.skillDef == Shiggy.voidreaverportalDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill4.down && extraskillLocator.extraFourth.skillDef == Shiggy.voidreaverportalDef)
            {

                keepFiring = true;
            }
            else
            {
                keepFiring = false;
            }

            if (keepFiring)
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
            else if (!keepFiring)
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
