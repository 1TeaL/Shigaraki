using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using RoR2.Projectile;
using EntityStates.LemurianMonster;

namespace ShiggyMod.SkillStates
{
    public class LemurianFireball : BaseSkillState
    {
        public float duration = 1f;
        public ShiggyController Shiggycon;

        public static GameObject effectPrefab;

        private string muzzleString;
        private float damageCoefficient = 1f;
        private float force = 1f;
        private float speedOverride = -1f;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            this.muzzleString = "LHand";

            bool flag = LemurianFireball.effectPrefab;
            if (flag)
            {
                EffectManager.SimpleMuzzleFlash(FireFireball.effectPrefab, base.gameObject, muzzleString, false);
            }
            bool isAuthority = base.isAuthority;
            if (isAuthority)
            {
                ProjectileManager.instance.FireProjectile(FireFireball.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * damageCoefficient, force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, speedOverride);
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
