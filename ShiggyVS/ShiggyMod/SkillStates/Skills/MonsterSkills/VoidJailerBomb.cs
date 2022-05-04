//using EntityStates;
//using RoR2;
//using UnityEngine;
//using ShiggyMod.Modules.Survivors;
//using UnityEngine.Networking;
//using EntityStates.VoidJailer;
//using RoR2.Projectile;
//using EntityStates.VoidJailer.Weapon;

//namespace ShiggyMod.SkillStates
//{
//    public class VoidJailerBomb : BaseSkillState
//    {
//        public float baseDuration = 1f;
//        public float duration;
//        public ShiggyController Shiggycon;
//        private DamageType damageType;


//        private float radius = 15f;
//        private float damageCoefficient = Modules.StaticValues.decayDamageCoeffecient;
//        private float procCoefficient = 1f;
//        private float force = 1f;
//        private float speedOverride = -1f;
//        private Transform muzzleTransform;
//        private string muzzleName = "LHand";

//        public override void OnEnter()
//        {
//            base.OnEnter();
//            this.duration = this.baseDuration / this.attackSpeedStat;
//            Ray aimRay = base.GetAimRay();
//            base.characterBody.SetAimTimer(this.duration);

//            Util.PlaySound(CaptureFire.enterSoundString, base.gameObject);

//            this.muzzleTransform = base.FindModelChild(muzzleName);
//            if (this.muzzleTransform && base.isAuthority)
//            {
//                FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
//                fireProjectileInfo.projectilePrefab = DeathState.deathBombProjectile;
//                fireProjectileInfo.position = this.muzzleTransform.position;
//                fireProjectileInfo.rotation = Quaternion.LookRotation(aimRay.direction, Vector3.up);
//                fireProjectileInfo.owner = base.gameObject;
//                fireProjectileInfo.damage = this.damageStat;
//                fireProjectileInfo.crit = base.characterBody.RollCrit();
//                ProjectileManager.instance.FireProjectile(fireProjectileInfo);
//            }


//        }

//        public override void OnExit()
//        {
//            Util.PlaySound(ExitCapture.enterSoundString, base.gameObject);
//            base.OnExit();
//        }


//        public override void FixedUpdate()
//        {
//            base.FixedUpdate();



//            if (base.fixedAge >= this.duration && base.isAuthority)
//            {
//                this.outer.SetNextStateToMain();
//                return;
//            }
//        }




//        public override InterruptPriority GetMinimumInterruptPriority()
//        {
//            return InterruptPriority.PrioritySkill;
//        }

//    }
//}
