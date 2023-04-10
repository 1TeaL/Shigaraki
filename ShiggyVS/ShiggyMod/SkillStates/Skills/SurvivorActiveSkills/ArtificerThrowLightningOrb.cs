using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using EntityStates.Mage.Weapon;
using RoR2.UI;
using RoR2.Audio;
using System;

namespace ShiggyMod.SkillStates
{
    public class ArtificerThrowLightningOrb : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float baseDuration = 1f;
        public float duration;
        public float force = 2000f;
        private float selfForce = 100f;
        private float speedOverride = -1f;
        public ShiggyController Shiggycon;
        public HurtBox Target;

        public LoopSoundDef loopSoundDef = Modules.Assets.artificerlightningsound;
        private LoopSoundManager.SoundLoopPtr loopPtr;
        public GameObject crosshairOverridePrefab;
        private CrosshairUtils.OverrideRequest crosshairOverrideRequest;
        public float charge;
        private GameObject muzzleflashEffectPrefab = Modules.Assets.artificerlightningorbMuzzleEffect;
        public GameObject projectilePrefab = Modules.Assets.artificerlightningorbprojectileEffect;
        private float minDamageCoefficient = Modules.StaticValues.artificerlightningorbMinDamageCoefficient;
        private float maxDamageCoefficient = Modules.StaticValues.artificerlightningorbMaxDamageCoefficient;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration + 2f);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("LeftArm, Override", "LeftArmPunch", "Attack.playbackRate", duration, 0.1f);

            AkSoundEngine.PostEvent("ShiggyAirCannon", base.gameObject);
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            
            this.duration = this.baseDuration / this.attackSpeedStat;
            if (this.muzzleflashEffectPrefab)
            {
                EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, "LHand", false);
            }

            if (base.HasBuff(Modules.Buffs.multiplierBuff))
            {
                this.Fire();
                this.Fire();
                this.Fire();
            }
            else
            {
                this.Fire();
            }

        }
        private void Fire()
        {
            if (base.isAuthority)
            {
                Ray aimRay = base.GetAimRay();
                if (this.projectilePrefab != null)
                {
                    float num = Util.Remap(this.charge, 0f, 1f, this.minDamageCoefficient, this.maxDamageCoefficient);
                    float num2 = this.charge * this.force;
                    ProjectileManager.instance.FireProjectile(
                        projectilePrefab, //prefab
                        aimRay.origin, //position
                        Util.QuaternionSafeLookRotation(aimRay.direction), //rotation
                        base.gameObject, //owner
                        this.damageStat * num, //damage
                        num2, //force
                        Util.CheckRoll(this.critStat, base.characterBody.master), //crit
                        DamageColorIndex.Default, //damage color
                        null, //target
                        speedOverride * -(num * 5f)); //speed }
                    //FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
                    //{
                    //    projectilePrefab = this.projectilePrefab,
                    //    position = aimRay.origin,
                    //    rotation = Util.QuaternionSafeLookRotation(aimRay.direction),
                    //    owner = base.gameObject,
                    //    damage = this.damageStat * num,
                    //    force = num2,
                    //    crit = base.RollCrit(),
                    //    speedOverride = speedOverride *- num,                        
                    //};
                    //this.ModifyProjectile(ref fireProjectileInfo);
                    //ProjectileManager.instance.FireProjectile(fireProjectileInfo);
                }
                if (base.characterMotor)
                {
                    base.characterMotor.ApplyForce(aimRay.direction * (-selfForce * this.charge), false, false);
                }
            }
        }


        public override void OnExit()
        {
            base.OnExit();
            PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", duration, 0.1f);

        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.isAuthority && base.fixedAge >= this.duration)
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
