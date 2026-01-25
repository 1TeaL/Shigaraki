using EntityStates;
using RoR2;
using RoR2.Projectile;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class OperatorS141Custom : Skill
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";


        private float damageCoefficient = Modules.StaticValues.operatorDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 50f;
        private float recoilAmplitude = 4f;
        private GameObject effectPrefab = Modules.ShiggyAsset.operatorPistolMuzzlePrefab;
        private string muzzleString = "RHand";
        private float bulletCount = 1;
        private float range = 100f;

        public override void OnEnter()
        {
            base.OnEnter();

            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            base.AddRecoil(-3f * recoilAmplitude, -4f * recoilAmplitude, -0.5f * recoilAmplitude, 0.5f * recoilAmplitude);

            int randomAnim = UnityEngine.Random.RandomRangeInt(1, 3);
            PlayCrossfade("RightArm, Override", "RHandFingerGun" + randomAnim, "Attack.playbackRate", duration, 0.1f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }

            damageType = new DamageTypeCombo(DamageType.Generic, DamageTypeExtended.RicochetOnKill, DamageSource.Primary);

        }

        private void FireBullet(Ray aimRay)
        {
            TrajectoryAimAssist.ApplyTrajectoryAimAssist(ref aimRay, range, base.gameObject, 1f);
            
            OnFireAuthority(aimRay);
        }

        private void OnFireAuthority(Ray aimRay)
        {
            if (effectPrefab)
            {
                EffectManager.SimpleMuzzleFlash(effectPrefab, base.gameObject, muzzleString, false);
            }

            new BulletAttack
            {
                bulletCount = (uint)bulletCount,
                aimVector = aimRay.direction,
                origin = aimRay.origin,
                damage = damageCoefficient * characterBody.damage,
                damageColorIndex = DamageColorIndex.Default,
                damageType = damageType,
                falloffModel = BulletAttack.FalloffModel.None,
                maxDistance = range,
                force = force,
                hitMask = LayerIndex.CommonMasks.bullet,
                minSpread = 0,
                maxSpread = 0,
                isCrit = base.RollCrit(),
                owner = base.gameObject,
                muzzleName = this.muzzleString,
                smartCollision = false,
                procChainMask = default(ProcChainMask),
                procCoefficient = procCoefficient,
                radius = 0.4f,
                sniper = false,
                stopperMask = LayerIndex.CommonMasks.bullet,
                weapon = base.gameObject,
                tracerEffectPrefab = ShiggyAsset.operatorPistolTracerPrefab,
                spreadPitchScale = 1f,
                spreadYawScale = 1f,
                queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                hitEffectPrefab = ShiggyAsset.operatorPistolHitEffectPrefab,
                HitEffectNormal = true,
                filterCallback = new BulletAttack.FilterCallback(this.IgnoreAllies),
            }.Fire();
        }

        private bool IgnoreAllies(BulletAttack bullet, ref BulletAttack.BulletHit hit)
        {
            HurtBox component = hit.collider.GetComponent<HurtBox>();
            return (!component || !component.healthComponent || (!(component.healthComponent.gameObject == bullet.weapon))) && !(hit.entityObject == base.gameObject) && !(hit.entityObject == bullet.weapon) && (!component || !component.healthComponent || FriendlyFireManager.ShouldDirectHitProceed(component.healthComponent, base.teamComponent.teamIndex));
        }
        public override void OnExit()
        {
            base.OnExit();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.fixedAge > fireTime && !hasFired)
            {
                hasFired = true;
                Ray aimRay = base.GetAimRay();
                FireBullet(aimRay);
            }
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
