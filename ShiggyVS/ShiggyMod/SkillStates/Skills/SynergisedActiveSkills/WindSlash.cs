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
        public float baseDuration = 0.8f;
        public float duration;
        public ShiggyController Shiggycon;

        public static GameObject mercProjectile = Modules.Assets.mercWindProj;

        private string muzzleString;
        private Animator animator;
        private float damageCoefficient = Modules.StaticValues.windSlashDamageCoefficient;
        private float force = 1f;
        private float speedOverride = -1f;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            duration = baseDuration / attackSpeedStat;

            base.characterBody.SetAimTimer(this.duration);
            this.muzzleString = "LHand";
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            Shiggycon.boolswordAuraR = true;

            this.animator = base.GetModelAnimator();
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            //int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
            //base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            base.PlayCrossfade("RightArm, Override", "RArmGetsuga", "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
            AkSoundEngine.PostEvent("ShiggyGetsuga", base.gameObject);


            FireWind();

        }
        public void FireWind()
        {
            Ray aimRay = base.GetAimRay();

            EffectManager.SpawnEffect(Modules.Assets.shiggySwingEffect, new EffectData
            {
                origin = FindModelChild("Swing2").position,
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
            Shiggycon.boolswordAuraR = false;
            this.animator.SetBool("false", true);
            //PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
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
