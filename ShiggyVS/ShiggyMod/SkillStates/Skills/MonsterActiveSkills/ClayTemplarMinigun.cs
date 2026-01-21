using EntityStates;
using EntityStates.ClayBruiser.Weapon;
using ExtraSkillSlots;
using RoR2;
using ShiggyMod.Modules.Survivors;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class ClayTemplarMinigun : Skill
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.claytemplarminigunDamageCoefficient;
        private float procCoefficient = 0.1f;
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
            damageType = new DamageTypeCombo(DamageType.ClayGoo, DamageTypeExtended.Generic, DamageSource.Secondary);
            keepFiring = true;
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            this.muzzleString = "RHand";

            this.animator = base.GetModelAnimator();
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);

            PlayCrossfade("RightArm, Override", "RArmOutStart", "Attack.playbackRate", baseFireInterval, 0.1f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }

            this.baseFireRate = 1f / baseFireInterval;
            baseBulletCount = 3;
            this.critEndTime = Run.FixedTimeStamp.negativeInfinity;
            this.lastCritCheck = Run.FixedTimeStamp.negativeInfinity;
            Util.PlaySound(MinigunFire.startSound, base.gameObject);


            Shiggycon = gameObject.GetComponent<ShiggyController>();
            
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

            this.animator.SetBool("attacking", false);
            PlayCrossfade("RightArm, Override", "Empty", "Attack.playbackRate", 0.1f, 0.1f);
            PlayCrossfade("LeftArm, Override", "Empty", "Attack.playbackRate", 0.1f, 0.1f);
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
                damage = damageCoefficient * characterBody.damage,
                damageColorIndex = DamageColorIndex.Default,
                damageType = damageType,
                falloffModel = BulletAttack.FalloffModel.DefaultBullet,
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
            if (base.inputBank.skill1.down && characterBody.skillLocator.primary.skillDef == Shiggy.claytemplarminigunDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill2.down && characterBody.skillLocator.secondary.skillDef == Shiggy.claytemplarminigunDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill3.down && characterBody.skillLocator.utility.skillDef == Shiggy.claytemplarminigunDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill4.down && characterBody.skillLocator.special.skillDef == Shiggy.claytemplarminigunDef)
            {

                keepFiring = true;
            }
            else if (extrainputBankTest.extraSkill1.down && extraskillLocator.extraFirst.skillDef == Shiggy.claytemplarminigunDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill2.down && extraskillLocator.extraSecond.skillDef == Shiggy.claytemplarminigunDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill3.down && extraskillLocator.extraThird.skillDef == Shiggy.claytemplarminigunDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill4.down && extraskillLocator.extraFourth.skillDef == Shiggy.claytemplarminigunDef)
            {

                keepFiring = true;
            }
            else
            {
                keepFiring = false;
            }

            if (keepFiring)
            {
                this.fireTimer -= Time.fixedDeltaTime;
                if (this.fireTimer <= 0f)
                {
                    float num = baseFireInterval / this.attackSpeedStat;
                    this.fireTimer += num;
                    this.OnFireShared();
                }
            }             
            else if(!keepFiring)
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
