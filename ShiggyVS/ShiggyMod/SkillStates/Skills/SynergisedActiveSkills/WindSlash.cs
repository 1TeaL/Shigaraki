using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using RoR2.Projectile;
using EntityStates.LemurianMonster;
using EmotesAPI;
using R2API;
using ShiggyMod.Modules;

namespace ShiggyMod.SkillStates
{
    //merc + air cannon
    public class WindSlash : BaseSkillState
    {
        public float baseDuration = 0.6f;
        public float duration;
        public ShiggyController Shiggycon;

        public static GameObject mercProjectile = Modules.Assets.mercWindProj;

        private string muzzleString;
        private Animator animator;
        private float damageCoefficient = Modules.StaticValues.windSlashDamageCoefficient;
        private float force = 1f;
        private float speedOverride = -1f;
        private GameObject chargeVfxInstance;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            duration = baseDuration / attackSpeedStat;

            base.characterBody.SetAimTimer(this.duration);
            this.muzzleString = "LHand";
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            this.animator = base.GetModelAnimator();
            //this.animator.SetBool("attacking", true);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", duration / 2, 0.1f);
            //PlayCrossfade("LeftArm, Override", "LeftArmPunch", "Attack.playbackRate", duration/2, 0.1f);


            FireWind();

        }
        public void FireWind()
        {
            Ray aimRay = base.GetAimRay();

            EffectManager.SpawnEffect(EntityStates.Merc.Uppercut.swingEffectPrefab, new EffectData
            {
                origin = FindModelChild(muzzleString).position,
                scale = 1f,
                rotation = Quaternion.LookRotation(aimRay.direction),

            }, true);

            bool isAuthority = base.isAuthority;
            if (isAuthority)
            {

                //var mercProjectileUpdated = mercProjectile;
                //DamageAPI.ModdedDamageTypeHolderComponent damageTypeComponent = mercProjectileUpdated.AddComponent<DamageAPI.ModdedDamageTypeHolderComponent>();
                //damageTypeComponent.Add(Damage.shiggyDecay);

                ProjectileManager.instance.FireProjectile(
                    mercProjectile, //prefab
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
            this.animator.SetBool("false", true);
            //PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            if (this.chargeVfxInstance)
            {
                EntityState.Destroy(this.chargeVfxInstance);
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
            return InterruptPriority.PrioritySkill;
        }

    }
}
