using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using EntityStates.VoidMegaCrab.BackWeapon;

namespace ShiggyMod.SkillStates
{
    public class VoidDevastatorHoming : BaseSkillState
    {
        public float baseDuration = 1f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.voiddevastatorDamageCoefficient;
        private float procCoefficient = Modules.StaticValues.voiddevastatorProcCoefficient;
        private float force = 1f;
        private float speedOverride = -1f;
        private float durationBetweenMissiles;
        private string MuzzleString = "RHand";
        private float missileTimer;
        private int missileWaveCount;
        private int totalMissileWaveCount = Modules.StaticValues.voiddevastatorTotalMissiles;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            //base.PlayAnimation(FireVoidMissiles.animationLayerName, FireVoidMissiles.animationStateName, FireVoidMissiles.animationPlaybackRateParam, this.duration);
            Util.PlaySound(FireVoidMissiles.enterSoundString, base.gameObject);
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            
            durationBetweenMissiles = (duration / totalMissileWaveCount)-0.05f;

            //if (FireVoidMissiles.muzzleEffectPrefab)
            //{
            //    EffectManager.SimpleMuzzleFlash(FireVoidMissiles.muzzleEffectPrefab, base.gameObject, MuzzleString, false);
            //}

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
            //base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            
        }
        private void FireMissile()
        {
            if (base.isAuthority)
            {
                Ray aimRay = base.GetAimRay();
                EffectManager.SpawnEffect(FireVoidMissiles.muzzleEffectPrefab, new EffectData
                {
                    origin = FindModelChild(MuzzleString).position,
                    scale = 1f,
                    rotation = Quaternion.LookRotation(aimRay.direction)
                }, false);


                ProjectileManager.instance.FireProjectile(
                    FireVoidMissiles.projectilePrefab, //prefab
                    aimRay.origin, //position
                    Util.QuaternionSafeLookRotation(aimRay.direction), //rotation
                    base.gameObject, //owner
                    this.damageStat * damageCoefficient, //damage
                    force, //force
                    Util.CheckRoll(this.critStat, base.characterBody.master), //crit
                    DamageColorIndex.Default, //damage color
                    null, //target
                    speedOverride); //speed }} 

                ProjectileManager.instance.FireProjectile(
                    FireVoidMissiles.projectilePrefab, //prefab
                    aimRay.origin, //position
                    Util.QuaternionSafeLookRotation(aimRay.direction), //rotation
                    base.gameObject, //owner
                    this.damageStat * damageCoefficient, //damage
                    force, //force
                    Util.CheckRoll(this.critStat, base.characterBody.master), //crit
                    DamageColorIndex.Default, //damage color
                    null, //target
                    speedOverride); //speed }} 
            }
        }
        public override void OnExit()
        {
            base.OnExit();
            PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            //PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.missileTimer -= Time.fixedDeltaTime;
            if (this.missileWaveCount < totalMissileWaveCount && this.missileTimer <= 0f)
            {
                this.missileWaveCount++;
                this.missileTimer += this.durationBetweenMissiles;
                this.FireMissile();
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
