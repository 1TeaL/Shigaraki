using EntityStates;
using RoR2;
using RoR2.Audio;
using RoR2.UI;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class RailgunnerCryoFire : Skill
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.railgunnerDamageCoefficient;
        private float procCoefficient = Modules.StaticValues.railgunnerProcCoefficient;
        private float force = 1f;
        private float speedOverride = -1f;
        private float recoilAmplitude = 4f;
        private GameObject effectPrefab = Modules.ShiggyAsset.banditmuzzleEffect;
        private string muzzleName = "RHand";
        private float bulletCount = 1;
        public LoopSoundDef loopSoundDef = Modules.ShiggyAsset.railgunnercryoofflineSound;
        private LoopSoundManager.SoundLoopPtr loopPtr;
        public GameObject reloadcrosshairOverridePrefab = Modules.ShiggyAsset.railgunnercryoreloadCrosshair;
        private CrosshairUtils.OverrideRequest crosshairOverrideRequest;

        public override void OnEnter()
        {
            base.OnEnter();
            baseDuration = 1f;
            this.duration = this.baseDuration / this.attackSpeedStat;
            fireTime = duration * StaticValues.universalFiretime;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            this.animator.SetBool("attacking", false);
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            base.AddRecoil(-3f * recoilAmplitude, -4f * recoilAmplitude, -0.5f * recoilAmplitude, 0.5f * recoilAmplitude);

            PlayCrossfade("LeftArm, Override", "LArmAimRelease", "Attack.playbackRate", duration, 0.1f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
            damageType = new DamageTypeCombo(DamageType.Freeze2s, DamageTypeExtended.Generic, DamageSource.Secondary);

            if (effectPrefab)
            {
                EffectManager.SimpleMuzzleFlash(effectPrefab, base.gameObject, muzzleName, false);
            }
            if (this.loopSoundDef)
            {
                this.loopPtr = LoopSoundManager.PlaySoundLoopLocal(base.gameObject, this.loopSoundDef);
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

            if (base.fixedAge > fireTime && !hasFired)
            {
                hasFired = true;
                if (base.isAuthority)
                {
                    Ray aimRay = base.GetAimRay();
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
                        tracerEffectPrefab = Modules.ShiggyAsset.railgunnercryoTracer,
                        muzzleName = muzzleName,
                        //hitEffectPrefab = Modules.Asset.banditimpactEffect,
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
