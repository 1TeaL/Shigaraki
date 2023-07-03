using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using EntityStates.Treebot.Weapon;

namespace ShiggyMod.SkillStates
{
    public class RexMortar : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float baseDuration = 0.7f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;
        public HurtBox Target;
        private Animator animator;


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.rexDamageCoefficient;
        private float procCoefficient = Modules.StaticValues.rexProcCoefficient;
        private float force = 400f;
        private float speedOverride = -1f;
        private float healthCostFraction = Modules.StaticValues.rexHealthCost;
        private string muzzleName = "LHand";

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            


            damageType = DamageType.Generic;
            EffectManager.SimpleMuzzleFlash(FireMortar2.muzzleEffect, base.gameObject, muzzleName, false);
            Util.PlaySound(FireMortar2.fireSound, base.gameObject);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
            base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
            if (base.isAuthority)
            {
                this.Fire();
            }
            if (NetworkServer.active && base.healthComponent)
            {
                DamageInfo damageInfo = new DamageInfo();
                damageInfo.damage = base.healthComponent.combinedHealth * healthCostFraction;
                damageInfo.position = base.characterBody.corePosition;
                damageInfo.force = Vector3.zero;
                damageInfo.damageColorIndex = DamageColorIndex.Default;
                damageInfo.crit = false;
                damageInfo.attacker = characterBody.gameObject;
                damageInfo.inflictor = null;
                damageInfo.damageType = (DamageType.NonLethal | DamageType.BypassArmor);
                damageInfo.procCoefficient = 0f;
                damageInfo.procChainMask = default(ProcChainMask);
                base.healthComponent.TakeDamage(damageInfo);
            }

        }

        public override void OnExit()
        {
            base.OnExit();
        }
        private void Fire()
        {
            RaycastHit raycastHit;
            Vector3 point;
            if (base.inputBank.GetAimRaycast(FireMortar2.maxDistance, out raycastHit))
            {
                point = raycastHit.point;
            }
            else
            {
                point = base.inputBank.GetAimRay().GetPoint(FireMortar2.maxDistance);
            }
            FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
            {
                projectilePrefab = FireMortar2.projectilePrefab,
                position = point,
                rotation = Quaternion.identity,
                owner = base.gameObject,
                damage = damageCoefficient * this.damageStat,
                force = force,
                crit = base.RollCrit(),
                damageColorIndex = DamageColorIndex.Default,
                target = null,
                speedOverride = 0f,
                fuseOverride = -1f
            };
            ProjectileManager.instance.FireProjectile(fireProjectileInfo);
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
