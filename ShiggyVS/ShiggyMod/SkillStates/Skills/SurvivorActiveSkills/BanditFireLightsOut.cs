using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;

namespace ShiggyMod.SkillStates
{
    public class BanditFireLightsOut : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float baseDuration = 0.5f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;
        public HurtBox Target;
        private Animator animator;


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.banditDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 1f;
        private float speedOverride = -1f;
        private float recoilAmplitude = 4f;
        private GameObject effectPrefab = Modules.Assets.banditmuzzleEffect;
        private string muzzleName = "RHand";
        private float bulletCount = 1;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            
            base.AddRecoil(-3f * recoilAmplitude, -4f * recoilAmplitude, -0.5f * recoilAmplitude, 0.5f * recoilAmplitude);

            PlayCrossfade("RightArm, Override", "RHandFingerGun", "Attack.playbackRate", duration, 0.1f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
            Util.PlaySound(EntityStates.Bandit2.StealthMode.exitStealthSound, base.gameObject);
            damageType = DamageType.BonusToLowHealth | DamageType.ResetCooldownsOnKill;
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
                    tracerEffectPrefab = Modules.Assets.bandittracerEffectPrefab,
                    muzzleName = muzzleName,
                    hitEffectPrefab = Modules.Assets.banditimpactEffect,
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
