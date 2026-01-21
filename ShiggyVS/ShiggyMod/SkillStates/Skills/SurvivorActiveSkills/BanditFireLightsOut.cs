using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;

namespace ShiggyMod.SkillStates
{
    public class BanditFireLightsOut : Skill
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.banditDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 1f;
        private float speedOverride = -1f;
        private float recoilAmplitude = 4f;
        private GameObject effectPrefab = Modules.ShiggyAsset.banditmuzzleEffect;
        private string muzzleName = "RHand";
        private float bulletCount = 1;

        public override void OnEnter()
        {
            base.OnEnter();
            baseDuration = 0.5f;
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            base.GetModelAnimator().SetBool("attacking", false);
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            
            base.AddRecoil(-3f * recoilAmplitude, -4f * recoilAmplitude, -0.5f * recoilAmplitude, 0.5f * recoilAmplitude);


            PlayCrossfade("RightArm, Override", "RHandFingerGunRelease", "Attack.playbackRate", duration, 0.1f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
            Util.PlaySound(EntityStates.Bandit2.StealthMode.exitStealthSound, base.gameObject);

            damageType = new DamageTypeCombo(DamageType.BonusToLowHealth | DamageType.ResetCooldownsOnKill, DamageTypeExtended.Generic, DamageSource.Secondary);
            if (effectPrefab)
            {
                EffectManager.SimpleMuzzleFlash(effectPrefab, base.gameObject, muzzleName, false);
            }
            if (base.isAuthority)
            {
                new BulletAttack
                {
                    bulletCount = (uint)bulletCount,
                    owner = base.gameObject,
                    weapon = base.gameObject,
                    origin = aimRay.origin,
                    aimVector = aimRay.direction,
                    minSpread = 0f,
                    maxSpread = 0f,
                    force = force,
                    falloffModel = BulletAttack.FalloffModel.None,
                    tracerEffectPrefab = Modules.ShiggyAsset.bandittracerEffectPrefab,
                    muzzleName = muzzleName,
                    hitEffectPrefab = Modules.ShiggyAsset.banditimpactEffect,
                    isCrit = base.RollCrit(),
                    HitEffectNormal = true,
                    radius = 0.5f,
                    maxDistance = 2000f,
                    procCoefficient = 1f,
                    damage = damageCoefficient * this.damageStat,
                    damageType = damageType,
                    smartCollision = true
                }.Fire();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
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
