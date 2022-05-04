//using EntityStates;
//using RoR2;
//using UnityEngine;
//using ShiggyMod.Modules.Survivors;
//using UnityEngine.Networking;

//namespace ShiggyMod.SkillStates
//{
//    public class RobobalMiniBeam : BaseSkillState
//    {
//        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";

//        public float maxDistance = 60f;
//        public float fireFrequency = 1f;
//        public float fireTimer;
//        private Transform modelTransform;
//        private Transform laserEffectInstanceEndTransform;
//        public ShiggyController Shiggycon;
//        private DamageType damageType;
//        public string muzzleString = "RHand";
//        public GameObject laserPrefab = Modules.Assets.roboballminibeamEffect;
//        public GameObject effectPrefab = Modules.Assets.roboballminimuzzleEffect;
//        private GameObject hitEffectPrefab = Modules.Assets.roboballminiimpactEffect;
//        private GameObject laserEffectInstance;
//        private Ray laserRay;

//        private float radius = 15f;
//        private float damageCoefficient = Modules.StaticValues.decayDamageCoeffecient;
//        private float procCoefficient = 1f;
//        private float force = 1f;
//        private float speedOverride = -1f;
//        private Ray aimRay;

//        public override void OnEnter()
//        {
//            base.OnEnter();
//            aimRay = base.GetAimRay();
//            base.characterBody.SetAimTimer(2f);
//            damageType = DamageType.SlowOnHit;

//            this.fireTimer = 0f;
//            this.modelTransform = base.GetModelTransform();
//            if (this.modelTransform)
//            {
//                ChildLocator component = this.modelTransform.GetComponent<ChildLocator>();
//                if (component)
//                {
//                    Transform transform = component.FindChild(this.muzzleString);
//                    if (transform && this.laserPrefab)
//                    {
//                        this.laserEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.laserPrefab, transform.position, Util.QuaternionSafeLookRotation(aimRay.direction));
//                        this.laserEffectInstance.transform.parent = transform;
//                        this.laserEffectInstanceEndTransform = this.laserEffectInstance.GetComponent<ChildLocator>().FindChild("RHand");
//                    }
//                }
//            }
//        }
//        protected Vector3 GetBeamEndPoint()
//        {
//            Vector3 point = this.aimRay.GetPoint(this.maxDistance);
//            RaycastHit raycastHit;
//            if (Util.CharacterRaycast(base.gameObject, this.aimRay, out raycastHit, this.maxDistance, LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.UseGlobal))
//            {
//                point = raycastHit.point;
//            }
//            return point;
//        }

//        public override void OnExit()
//        {
//            base.OnExit();
//            if (this.laserEffectInstance)
//            {
//                EntityState.Destroy(this.laserEffectInstance);
//            }
//        }


//        public override void FixedUpdate()
//        {
//            base.FixedUpdate();

//            Ray aimRay = base.GetAimRay();
//            if (base.skillLocator.primary.skillNameToken == prefix + "ROBOBALLMINI_NAME")
//            {
//                if (base.inputBank.skill1.down)
//                {

//                    this.fireTimer += Time.fixedDeltaTime;
//                    float num = this.fireFrequency * base.characterBody.attackSpeed;
//                    float num2 = 1f / num;
//                    if (this.fireTimer > num2)
//                    {
//                        this.FireBullet(this.modelTransform, this.laserRay, this.muzzleString);
//                        this.fireTimer = 0f;
//                    }
//                    if (this.laserEffectInstance && this.laserEffectInstanceEndTransform)
//                    {
//                        this.laserEffectInstanceEndTransform.position = this.GetBeamEndPoint();
//                    }

//                }
//                else
//                {
//                    if (base.isAuthority)
//                    {
//                        this.outer.SetNextStateToMain();
//                        return;

//                    }

//                }
//            }
//            if (base.skillLocator.secondary.skillNameToken == prefix + "ROBOBALLMINI_NAME")
//            {
//                if (base.inputBank.skill2.down)
//                {

//                    this.fireTimer += Time.fixedDeltaTime;
//                    float num = this.fireFrequency * base.characterBody.attackSpeed;
//                    float num2 = 1f / num;
//                    if (this.fireTimer > num2)
//                    {
//                        this.FireBullet(this.modelTransform, this.laserRay, this.muzzleString);
//                        this.fireTimer = 0f;
//                    }
//                    if (this.laserEffectInstance && this.laserEffectInstanceEndTransform)
//                    {
//                        this.laserEffectInstanceEndTransform.position = this.GetBeamEndPoint();
//                    }

//                }
//                else
//                {
//                    if (base.isAuthority)
//                    {
//                        this.outer.SetNextStateToMain();
//                        return;

//                    }

//                }
//            }
//            if (base.skillLocator.utility.skillNameToken == prefix + "ROBOBALLMINI_NAME")
//            {
//                if (base.inputBank.skill3.down)
//                {

//                    this.fireTimer += Time.fixedDeltaTime;
//                    float num = this.fireFrequency * base.characterBody.attackSpeed;
//                    float num2 = 1f / num;
//                    if (this.fireTimer > num2)
//                    {
//                        this.FireBullet(this.modelTransform, this.laserRay, this.muzzleString);
//                        this.fireTimer = 0f;
//                    }
//                    if (this.laserEffectInstance && this.laserEffectInstanceEndTransform)
//                    {
//                        this.laserEffectInstanceEndTransform.position = this.GetBeamEndPoint();
//                    }

//                }
//                else
//                {
//                    if (base.isAuthority)
//                    {
//                        this.outer.SetNextStateToMain();
//                        return;

//                    }

//                }
//            }
//            if (base.skillLocator.special.skillNameToken == prefix + "ROBOBALLMINI_NAME")
//            {
//                if (base.inputBank.skill4.down)
//                {

//                    this.fireTimer += Time.fixedDeltaTime;
//                    float num = this.fireFrequency * base.characterBody.attackSpeed;
//                    float num2 = 1f / num;
//                    if (this.fireTimer > num2)
//                    {
//                        this.FireBullet(this.modelTransform, this.laserRay, this.muzzleString);
//                        this.fireTimer = 0f;
//                    }
//                    if (this.laserEffectInstance && this.laserEffectInstanceEndTransform)
//                    {
//                        this.laserEffectInstanceEndTransform.position = this.GetBeamEndPoint();
//                    }

//                }
//                else
//                {
//                    if (base.isAuthority)
//                    {
//                        this.outer.SetNextStateToMain();
//                        return;

//                    }

//                }
//            }


//        }

//        private void FireBullet(Transform modelTransform, Ray laserRay, string targetMuzzle)
//        {
//            if (this.effectPrefab)
//            {
//                EffectManager.SimpleMuzzleFlash(this.effectPrefab, base.gameObject, targetMuzzle, false);
//            }
//            if (base.isAuthority)
//            {
//                BulletAttack bulletAttack = new BulletAttack();
//                bulletAttack.owner = base.gameObject;
//                bulletAttack.weapon = base.gameObject;
//                bulletAttack.origin = laserRay.origin;
//                bulletAttack.aimVector = laserRay.direction;
//                bulletAttack.minSpread = 0f;
//                bulletAttack.maxSpread = 0f;
//                bulletAttack.bulletCount = 1U;
//                bulletAttack.damage = this.damageCoefficient * this.damageStat / this.fireFrequency;
//                bulletAttack.procCoefficient = this.procCoefficient / this.fireFrequency;
//                bulletAttack.force = this.force;
//                bulletAttack.muzzleName = targetMuzzle;
//                bulletAttack.hitEffectPrefab = this.hitEffectPrefab;
//                bulletAttack.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
//                bulletAttack.HitEffectNormal = false;
//                bulletAttack.radius = 0f;
//                bulletAttack.maxDistance = this.maxDistance;
//                bulletAttack.damageType = damageType;
//                bulletAttack.Fire();
//            }
//        }


//        public override InterruptPriority GetMinimumInterruptPriority()
//        {
//            return InterruptPriority.PrioritySkill;
//        }

//    }
//}
