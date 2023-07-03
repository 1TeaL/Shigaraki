//using EntityStates;
//using RoR2;
//using UnityEngine;
//using ShiggyMod.Modules.Survivors;
//using UnityEngine.Networking;
//using EntityStates.ClayBoss;
//using RoR2.Projectile;
//using EntityStates.ClayBoss.ClayBossWeapon;

//namespace ShiggyMod.SkillStates
//{
//    public class ClayDunestriderCopy : BaseSkillState
//    {
//        public float baseDuration = 0.5f;
//        public float duration;
//        public ShiggyController Shiggycon;
//        public static float baseTimeBetweenShots = 0.5f;
//        public static float recoilAmplitude = 1f;

//        private float damageCoefficient = Modules.StaticValues.claydunestriderDamageCoefficient;
//        private float force = 1f;
//        private float speedOverride = -1f;
//        private Transform modelTransform;

//        public override void OnEnter()
//        {
//            base.OnEnter();
//            this.duration = this.baseDuration / this.attackSpeedStat;
//            Ray aimRay = base.GetAimRay();

//            this.modelTransform = base.GetModelTransform();
//            if (base.characterBody)
//            {
//                base.characterBody.SetAimTimer(duration);
//            }
//            if (base.HasBuff(Modules.Buffs.multiplierBuff))
//            {
//                FireSingleTarball("RHand");
//                FireSingleTarball("RHand");
//                FireSingleTarball("RHand");
//            }
//            else
//            {
//                FireSingleTarball("RHand");
//            }

//            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
//            PlayCrossfade("RightArm, Override", "RightArmPunch", "Attack.playbackRate", duration, 0.1f);
//            if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }

//        }
//        private void FireSingleTarball(string targetMuzzle)
//        {
//            Ray aimRay = base.GetAimRay();
//            base.characterBody.SetAimTimer(this.duration);
//            Util.PlaySound(FireTarball.attackSoundString, base.gameObject);

//            if (this.modelTransform)
//            {
//                ChildLocator component = this.modelTransform.GetComponent<ChildLocator>();
//                if (component)
//                {
//                    Transform transform = component.FindChild(targetMuzzle);
//                    if (transform)
//                    {
//                        aimRay.origin = transform.position;
//                    }
//                }
//            }
//            base.AddRecoil(-1f * FireTarball.recoilAmplitude, -2f * FireTarball.recoilAmplitude, -1f * FireTarball.recoilAmplitude, 1f * FireTarball.recoilAmplitude);

//            EffectManager.SimpleMuzzleFlash(FireTarball.effectPrefab, base.gameObject, targetMuzzle, false);

//            if (base.isAuthority)
//            {
//                Vector3 forward = Vector3.ProjectOnPlane(aimRay.direction, Vector3.up);

//                ProjectileManager.instance.FireProjectile(
//                    FireTarball.projectilePrefab, //prefab
//                    aimRay.origin, //position
//                    Util.QuaternionSafeLookRotation(aimRay.direction), //rotation
//                    base.gameObject, //owner
//                    this.damageStat * damageCoefficient, //damage
//                    force, //force
//                    Util.CheckRoll(this.critStat, base.characterBody.master), //crit
//                    DamageColorIndex.Default, //damage color
//                    null, //target
//                    speedOverride); //speed }}                
//            }
//            base.characterBody.AddSpreadBloom(FireTarball.spreadBloomValue);

//            Shiggycon = gameObject.GetComponent<ShiggyController>();
//            
//        }

//        public override void OnExit()
//        {
//            base.OnExit();
//            PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
//            //PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
//        }


//        public override void FixedUpdate()
//        {
//            base.FixedUpdate();

//            //this.fireTimer -= Time.fixedDeltaTime;
//            //if (this.fireTimer <= 0f)
//            //{
//            //    if (this.tarballCount < tarballCountMax)
//            //    {
//            //        this.fireTimer += this.timeBetweenShots;
//            //        this.FireSingleTarball("LHand");
//            //        this.tarballCount++;
//            //    }
//            //    else
//            //    {
//            //        this.fireTimer += 9999f;
//            //        //base.PlayCrossfade("Body", "ExitTarBall", "ExitTarBall.playbackRate", (FireTarball.cooldownDuration - FireTarball.baseTimeBetweenShots) / this.attackSpeedStat, 0.1f);
//            //    }
//            //}


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
