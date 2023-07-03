//using EntityStates;
//using RoR2;
//using UnityEngine;
//using ShiggyMod.Modules.Survivors;
//using UnityEngine.Networking;
//using RoR2.Projectile;

//namespace ShiggyMod.SkillStates
//{
//    public class GreaterWispCopy : BaseSkillState
//    {
//        public float baseDuration = 1f;
//        public float duration;
//        public float fireTime;
//        public ShiggyController Shiggycon;
//        private DamageType damageType;
//        public bool hasFired;

//        public static GameObject effectPrefab;

//        private float radius = 0.5f;
//        private float damageCoefficient = Modules.StaticValues.greaterwispballDamageCoefficient;
//        private float procCoefficient = 1f;
//        private float force = 1000f;
//        private float speedOverride = -1f;
//        private GameObject chargeEffectLeft;
//        private GameObject chargeEffectRight;
//        private string LHand = "LHand";
//        private string RHand = "RHand";

//        public override void OnEnter()
//        {
//            base.OnEnter();
//            this.duration = this.baseDuration / this.attackSpeedStat;
//            this.fireTime = duration / 3;
//            hasFired = false;
//            Ray aimRay = base.GetAimRay();
//            base.characterBody.SetAimTimer(this.duration);
//            if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
//            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
//            PlayCrossfade("RightArm, Override", "RightArmPunch", "Attack.playbackRate", duration, 0.1f);
//            PlayCrossfade("LeftArm, Override", "LeftArmPunch", "Attack.playbackRate", duration, 0.1f);

//            this.duration = baseDuration / this.attackSpeedStat;


//            Shiggycon = gameObject.GetComponent<ShiggyController>();
//            
//        }

//        public override void OnExit()
//        {
//            base.OnExit();
//            PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
//            PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
//        }


//        public override void FixedUpdate()
//        {
//            base.FixedUpdate();
//            if(base.fixedAge < fireTime)
//            {
//                EffectManager.SpawnEffect(Modules.Assets.chargegreaterwispBall, new EffectData
//                {
//                    origin = FindModelChild(LHand).position,
//                    scale = radius,
//                    rotation = Quaternion.LookRotation(base.transform.position)

//                }, false);

//                EffectManager.SpawnEffect(Modules.Assets.chargegreaterwispBall, new EffectData
//                {
//                    origin = FindModelChild(RHand).position,
//                    scale = radius,
//                    rotation = Quaternion.LookRotation(base.transform.position)

//                }, false);


//            }

//            if (base.fixedAge >= this.fireTime && base.isAuthority && !hasFired)
//            {
//                hasFired = true;
//                if (base.HasBuff(Modules.Buffs.multiplierBuff))
//                {
//                    FireSpiritBall();
//                    FireSpiritBall();
//                    FireSpiritBall();
//                }
//                else
//                {
//                    FireSpiritBall();
//                }

//            }

//            if (base.fixedAge >= this.duration && base.isAuthority)
//            {
//                this.outer.SetNextStateToMain();
//                return;
//            }
//        }

//        public void FireSpiritBall()
//        {
//            Ray aimRay = base.GetAimRay();
//            //base.PlayAnimation("Gesture", "FireCannons", "FireCannons.playbackRate", this.duration);
//            if (base.isAuthority && base.modelLocator && base.modelLocator.modelTransform)
//            {
//                ChildLocator component = base.modelLocator.modelTransform.GetComponent<ChildLocator>();
//                if (component)
//                {
//                    int childIndex = component.FindChildIndex(LHand);
//                    int childIndex2 = component.FindChildIndex(RHand);
//                    Transform transform = component.FindChild(childIndex);
//                    Transform transform2 = component.FindChild(childIndex2);
//                    if (transform)
//                    {

//                        ProjectileManager.instance.FireProjectile(
//                            Modules.Assets.greaterwispBall, //prefab
//                            aimRay.origin, //position
//                            Util.QuaternionSafeLookRotation(aimRay.direction), //rotation
//                            base.gameObject, //owner
//                            this.damageStat * damageCoefficient, //damage
//                            force, //force
//                            Util.CheckRoll(this.critStat, base.characterBody.master), //crit
//                            DamageColorIndex.Default, //damage color
//                            null, //target
//                            speedOverride); //speed }}                        
//                    }
//                    if (transform2)
//                    {

//                        ProjectileManager.instance.FireProjectile(
//                            Modules.Assets.greaterwispBall, //prefab
//                            aimRay.origin, //position
//                            Util.QuaternionSafeLookRotation(aimRay.direction), //rotation
//                            base.gameObject, //owner
//                            this.damageStat * damageCoefficient, //damage
//                            force, //force
//                            Util.CheckRoll(this.critStat, base.characterBody.master), //crit
//                            DamageColorIndex.Default, //damage color
//                            null, //target
//                            speedOverride); //speed }}
//                    }

//                }
//            }
//        }


//        public override InterruptPriority GetMinimumInterruptPriority()
//        {
//            return InterruptPriority.PrioritySkill;
//        }

//    }
//}
