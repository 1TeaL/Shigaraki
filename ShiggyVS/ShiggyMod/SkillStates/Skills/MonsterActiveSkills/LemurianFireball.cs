using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using RoR2.Projectile;
using EntityStates.LemurianMonster;
using EmotesAPI;

namespace ShiggyMod.SkillStates
{
    public class LemurianFireball : BaseSkillState
    {
        public float baseDuration = 0.5f;
        public float duration;
        public ShiggyController Shiggycon;

        public static GameObject effectPrefab;

        private string muzzleString;
        private Animator animator;
        private float damageCoefficient = Modules.StaticValues.lemurianfireballDamageCoeffecient;
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
            if (transform && ChargeFireball.chargeVfxPrefab)
            {
                this.chargeVfxInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeFireball.chargeVfxPrefab, FindModelChild(this.muzzleString).position, Util.QuaternionSafeLookRotation(aimRay.direction));
                this.chargeVfxInstance.transform.parent = FindModelChild(this.muzzleString).transform;
            }

            for (int i = 0; i < 1 * Shiggycon.; i++)
            {
                FireBall();
            }


            Shiggycon = gameObject.GetComponent<ShiggyController>();
            

        }
        public void FireBall()
        {
            Ray aimRay = base.GetAimRay();
            bool flag = LemurianFireball.effectPrefab;
            if (flag)
            {
                EffectManager.SimpleMuzzleFlash(FireFireball.effectPrefab, base.gameObject, muzzleString, false);
            }
            bool isAuthority = base.isAuthority;
            if (isAuthority)
            {

                ProjectileManager.instance.FireProjectile(
                    Modules.Projectiles.lemurianFireBall, //prefab
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

            if (base.fixedAge >= this.duration && base.isAuthority && base.IsKeyDownAuthority())
            {
                this.outer.SetNextState(new LemurianFireball());
            }
            else if (base.fixedAge >= this.duration && base.isAuthority)
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
