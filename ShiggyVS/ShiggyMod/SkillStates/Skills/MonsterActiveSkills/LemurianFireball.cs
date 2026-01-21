using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using RoR2.Projectile;
using EntityStates.LemurianMonster;
using EmotesAPI;
using Rewired.ComponentControls.Data;

namespace ShiggyMod.SkillStates
{
    public class LemurianFireball : Skill
    {

        public static GameObject effectPrefab;

        private string muzzleString;
        private float damageCoefficient = Modules.StaticValues.lemurianfireballDamageCoefficient;
        private float force = 1f;
        private float speedOverride = -1f;
        private GameObject chargeVfxInstance;
        public bool isContinued;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            baseDuration = 0.5f;
            duration = baseDuration / attackSpeedStat;

                

            base.characterBody.SetAimTimer(this.duration);
            this.muzzleString = "LHand";
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            this.animator = base.GetModelAnimator();
            base.GetModelAnimator().SetBool("attacking", true);
            //this.animator.SetBool("attacking", true);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("LeftArm, Override", "LArmOutStart", "Attack.playbackRate", duration / 2, 0.1f);
            //PlayCrossfade("LeftArm, Override", "LeftArmPunch", "Attack.playbackRate", duration/2, 0.1f);
            if (transform && ChargeFireball.chargeVfxPrefab)
            {
                this.chargeVfxInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeFireball.chargeVfxPrefab, FindModelChild(this.muzzleString).position, Util.QuaternionSafeLookRotation(aimRay.direction));
                this.chargeVfxInstance.transform.parent = FindModelChild(this.muzzleString).transform;
            }

            FireBall();
            

            

        }
        public void FireBall()
        {
            Ray aimRay = base.GetAimRay();
            
            EffectManager.SimpleMuzzleFlash(FireFireball.effectPrefab, base.gameObject, muzzleString, false);
            
            bool isAuthority = base.isAuthority;
            if (isAuthority)
            {

                ProjectileManager.instance.FireProjectile(
                    Modules.Projectiles.lemurianFireBall, //prefab
                    aimRay.origin, //position
                    Util.QuaternionSafeLookRotation(aimRay.direction), //rotation
                    base.gameObject, //owner
                    this.damageStat * damageCoefficient, //damage
                    force, //force
                    Util.CheckRoll(this.critStat, base.characterBody.master), //crit
                    DamageColorIndex.Default, //damage color
                    null, //target
                    speedOverride); //speed }


                //ProjectileManager.instance.FireProjectile(
                //    FireFireball.projectilePrefab, //prefab
                //    FindModelChild(this.muzzleString).position, //position
                //    Util.QuaternionSafeLookRotation(aimRay.direction), //rotation
                //    base.gameObject, //owner
                //    0f, //damage
                //    0f, //force
                //    Util.CheckRoll(this.critStat, base.characterBody.master), //crit
                //    DamageColorIndex.Default, //damage color
                //    null, //target
                //    speedOverride); //speed }
            }

        }

        public override void OnExit()
        {
            base.OnExit();
            base.GetModelAnimator().SetBool("attacking", isContinued);
            //PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            if (this.chargeVfxInstance)
            {
                EntityState.Destroy(this.chargeVfxInstance);
            }
        }


        public override void FixedUpdate()
        {

            if (base.inputBank.skill1.down && characterBody.skillLocator.primary.skillDef == Shiggy.lemurianfireballDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill2.down && characterBody.skillLocator.secondary.skillDef == Shiggy.lemurianfireballDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill3.down && characterBody.skillLocator.utility.skillDef == Shiggy.lemurianfireballDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill4.down && characterBody.skillLocator.special.skillDef == Shiggy.lemurianfireballDef)
            {

                keepFiring = true;
            }
            else if (extrainputBankTest.extraSkill1.down && extraskillLocator.extraFirst.skillDef == Shiggy.lemurianfireballDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill2.down && extraskillLocator.extraSecond.skillDef == Shiggy.lemurianfireballDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill3.down && extraskillLocator.extraThird.skillDef == Shiggy.lemurianfireballDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill4.down && extraskillLocator.extraFourth.skillDef == Shiggy.lemurianfireballDef)
            {

                keepFiring = true;
            }
            else
            {
                keepFiring = false;
            }


            if (base.fixedAge >= this.duration && base.isAuthority && keepFiring)
            {
                if (keepFiring)
                {
                    LemurianFireball lemFireball = new LemurianFireball();
                    lemFireball.isContinued = true;
                    this.outer.SetNextState(lemFireball);

                }
                else if (!keepFiring)
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
