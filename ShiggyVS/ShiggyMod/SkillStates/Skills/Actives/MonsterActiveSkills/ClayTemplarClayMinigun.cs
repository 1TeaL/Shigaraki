using EntityStates;
using EntityStates.ClayBruiser.Weapon;
using ExtraSkillSlots;
using RoR2;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class ClayTemplarClayMinigun : Skill
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


            this.animator.SetBool("attacking", true);
            PlayCrossfade("RightArm, Override", "RArmOutStart", "Attack.playbackRate", baseFireInterval, 0.1f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }

            this.baseFireRate = 1f / baseFireInterval;
            baseBulletCount = StaticValues.claytemplarminigunBullets;
            this.critEndTime = Run.FixedTimeStamp.negativeInfinity;
            this.lastCritCheck = Run.FixedTimeStamp.negativeInfinity;
            Util.PlaySound(MinigunFire.startSound, base.gameObject);


            Shiggycon = gameObject.GetComponent<ShiggyController>();

            SetSkillDef(Shiggy.claytemplarminigunDef);
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
            base.FixedUpdate();
            base.characterBody.SetAimTimer(1f);

            if (base.isAuthority)
            {
                if (!IsHeldDown())
                {
                    this.outer.SetNextStateToMain();                    
                    return;
                }

            }

            // Fire loop
            fireTimer -= Time.fixedDeltaTime;
            if (fireTimer <= 0f)
            {
                fireTimer += (baseFireInterval / this.attackSpeedStat);
                OnFireShared();
            }

        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
