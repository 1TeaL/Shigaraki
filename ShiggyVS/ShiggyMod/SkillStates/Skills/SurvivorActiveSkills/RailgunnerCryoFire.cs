using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using RoR2.Audio;
using RoR2.UI;

namespace ShiggyMod.SkillStates
{
    public class RailgunnerCryoFire : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float baseDuration = 2f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;
        public HurtBox Target;
        private Animator animator;


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.railgunnerDamageCoefficient;
        private float procCoefficient = Modules.StaticValues.railgunnerProcCoefficient;
        private float force = 1f;
        private float speedOverride = -1f;
        private float recoilAmplitude = 4f;
        private GameObject effectPrefab = Modules.Assets.banditmuzzleEffect;
        private string muzzleName = "RHand";
        private float bulletCount = 1;
        public LoopSoundDef loopSoundDef = Modules.Assets.railgunnercryoofflineSound;
        private LoopSoundManager.SoundLoopPtr loopPtr;
        public GameObject reloadcrosshairOverridePrefab = Modules.Assets.railgunnercryoreloadCrosshair;
        private CrosshairUtils.OverrideRequest crosshairOverrideRequest;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            
            base.AddRecoil(-3f * recoilAmplitude, -4f * recoilAmplitude, -0.5f * recoilAmplitude, 0.5f * recoilAmplitude);

            PlayCrossfade("LeftArm, Override", "LHandFingerGun", "Attack.playbackRate", duration, 0.1f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
            damageType = DamageType.Freeze2s;
            
            if (effectPrefab)
            {
                EffectManager.SimpleMuzzleFlash(effectPrefab, base.gameObject, muzzleName, false);
            }
            if (this.loopSoundDef)
            {
                this.loopPtr = LoopSoundManager.PlaySoundLoopLocal(base.gameObject, this.loopSoundDef);
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
                    stopperMask = LayerIndex.noCollision.mask,
                    falloffModel = BulletAttack.FalloffModel.None,
                    tracerEffectPrefab = Modules.Assets.railgunnercryoTracer,
                    muzzleName = muzzleName,
                    //hitEffectPrefab = Modules.Assets.banditimpactEffect,
                    isCrit = base.RollCrit(),
                    HitEffectNormal = true,
                    radius = 2f,
                    maxDistance = 2000f,
                    procCoefficient = procCoefficient,
                    damage = damageCoefficient * this.damageStat,
                    damageType = damageType,
                    smartCollision = true
                }.Fire();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
            CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
            if (overrideRequest != null)
            {
                overrideRequest.Dispose();
            }
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
            return InterruptPriority.Frozen;
        }

    }
}
