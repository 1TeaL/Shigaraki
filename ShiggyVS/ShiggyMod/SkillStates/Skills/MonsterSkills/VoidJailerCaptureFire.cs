//using EntityStates;
//using RoR2;
//using UnityEngine;
//using ShiggyMod.Modules.Survivors;
//using UnityEngine.Networking;
//using EntityStates.VoidJailer;
//using RoR2.Projectile;
//using System.Linq;
//using EntityStates.VoidJailer.Weapon;
//using System.Collections.Generic;

//namespace ShiggyMod.SkillStates
//{
//    public class VoidJailerCaptureFire : BaseSkillState
//	{
//		public float baseDuration = 2f;
//		public float debuffDuration = 4f;
//		public float duration;
//		public float fireInterval = 10f;
//		public float fireage;
//		public ShiggyController Shiggycon;
//        private DamageType damageType = DamageType.SlowOnHit;


//        private float damageCoefficient = Modules.StaticValues.voidjailerDamageCoeffecient;
//        private float procCoefficient = 0.3f;
//		public AnimationCurve pullSuitabilityCurve;

//		private float pullMinDistance = 10f;
//		private float pullMaxDistance = 40f;
//		private float pullLiftVelocity = -10f;
//        private string muzzleString = "RHand";
//        private Vector3 theSpot;
//        private float maxWeight;
//        private GameObject pullTracerPrefab = Modules.Assets.voidjailermuzzleEffect;

//        public override void OnEnter()
//        {
//            base.OnEnter();
//            this.duration = this.baseDuration / this.attackSpeedStat;
//			fireage = 0f;
//            Ray aimRay = base.GetAimRay();
//            base.characterBody.SetAimTimer(this.duration);
//			Util.PlaySound(CaptureFire.enterSoundString, base.gameObject);
			
//			theSpot = aimRay.origin;
//			BullseyeSearch search = new BullseyeSearch
//			{

//				teamMaskFilter = TeamMask.GetEnemyTeams(base.GetTeam()),
//				filterByLoS = false,
//				searchOrigin = theSpot,
//				searchDirection = UnityEngine.Random.onUnitSphere,
//				sortMode = BullseyeSearch.SortMode.Distance,
//				maxDistanceFilter = pullMaxDistance,
//				maxAngleFilter = 360f
//			};

//			search.RefreshCandidates();
//			search.FilterOutGameObject(base.gameObject);



//			List<HurtBox> target = search.GetResults().ToList<HurtBox>();
//			foreach (HurtBox singularTarget in target)
//			{
//				if (singularTarget)
//				{
//					Vector3 a = singularTarget.transform.position - aimRay.origin;
//					float magnitude = a.magnitude;
//					Vector3 vector = a / magnitude;
//					if (singularTarget.healthComponent && singularTarget.healthComponent.body)
//					{
//						float Weight = 1f;
//                        //singularTarget.healthComponent.body.AddTimedBuff(RoR2Content.Buffs.Nullified, debuffDuration);
//                        if (singularTarget.healthComponent.body.characterMotor)
//						{
//							Weight = singularTarget.healthComponent.body.characterMotor.mass;
//							//if (singularTarget.healthComponent.body.characterMotor.mass > maxWeight)
//							//{
//							//	maxWeight = singularTarget.healthComponent.body.characterMotor.mass;
//							//}
//						}
//						else if (singularTarget.healthComponent.body.rigidbody)
//						{
//							Weight = singularTarget.healthComponent.body.rigidbody.mass;
//							//if (singularTarget.healthComponent.body.rigidbody.mass > maxWeight)
//							//{
//							//	maxWeight = singularTarget.healthComponent.body.rigidbody.mass;
//							//}
//						}
//						Vector3 a2 = vector;
//						float d = Trajectory.CalculateInitialYSpeedForHeight(Mathf.Abs(this.pullMinDistance - magnitude)) * Mathf.Sign(this.pullMinDistance - magnitude);
//						a2 *= d;
//						a2.y = this.pullLiftVelocity;
//						DamageInfo damageInfo = new DamageInfo
//						{
//							attacker = base.gameObject,
//							damage = this.damageStat * this.damageCoefficient,
//							position = singularTarget.transform.position,
//							procCoefficient = this.procCoefficient,
//							damageType = damageType,

//						};
//						singularTarget.healthComponent.TakeDamageForce(a2 * (Weight / 2), true, true);
//						singularTarget.healthComponent.TakeDamage(damageInfo);
//						GlobalEventManager.instance.OnHitEnemy(damageInfo, singularTarget.healthComponent.gameObject);


//						EffectManager.SpawnEffect(Modules.Assets.voidjailerEffect, new EffectData
//						{
//							origin = singularTarget.transform.position,
//							scale = 1f,
//							rotation = Quaternion.LookRotation(aimRay.direction),

//						}, true);

//						if (this.pullTracerPrefab)
//						{
//							Vector3 position = singularTarget.transform.position;
//							Vector3 start = base.characterBody.corePosition;
//							Transform transform = base.FindModelChild(this.muzzleString);
//							if (transform)
//							{
//								start = transform.position;
//							}
//							EffectData effectData = new EffectData
//							{
//								origin = position,
//								start = start
//							};
//							EffectManager.SpawnEffect(pullTracerPrefab, effectData, true);
//						}
//					}
//				}
//			}

//		}
//		public void GetMaxWeight()
//		{
//			Ray aimRay = base.GetAimRay();
//			theSpot = aimRay.origin;
//			BullseyeSearch search = new BullseyeSearch
//			{

//				teamMaskFilter = TeamMask.GetEnemyTeams(base.GetTeam()),
//				filterByLoS = false,
//				searchOrigin = theSpot,
//				searchDirection = UnityEngine.Random.onUnitSphere,
//				sortMode = BullseyeSearch.SortMode.Distance,
//				maxDistanceFilter = pullMaxDistance,
//				maxAngleFilter = 360f
//			};

//			search.RefreshCandidates();
//			search.FilterOutGameObject(base.gameObject);



//			List<HurtBox> target = search.GetResults().ToList<HurtBox>();
//			foreach (HurtBox singularTarget in target)
//			{
//				if (singularTarget)
//				{
//                    Vector3 a = singularTarget.transform.position - aimRay.origin;
//                    float magnitude = a.magnitude;
//                    Vector3 vector = a / magnitude;
//                    if (singularTarget.healthComponent && singularTarget.healthComponent.body)
//					{
//						float Weight = 1f;
//						//singularTarget.healthComponent.body.AddTimedBuff(RoR2Content.Buffs.Nullified, debuffDuration);
//						if (singularTarget.healthComponent.body.characterMotor)
//						{
//						    Weight = singularTarget.healthComponent.body.characterMotor.mass;
//							//if (singularTarget.healthComponent.body.characterMotor.mass > maxWeight)
//							//{
//							//	maxWeight = singularTarget.healthComponent.body.characterMotor.mass;
//							//}
//						}
//						else if (singularTarget.healthComponent.body.rigidbody)
//						{
//							Weight = singularTarget.healthComponent.body.rigidbody.mass;
//							//if (singularTarget.healthComponent.body.rigidbody.mass > maxWeight)
//							//{
//							//	maxWeight = singularTarget.healthComponent.body.rigidbody.mass;
//							//}
//						}
//                        Vector3 a2 = vector;
//                        float d = Trajectory.CalculateInitialYSpeedForHeight(Mathf.Abs(this.pullMinDistance - magnitude)) * Mathf.Sign(this.pullMinDistance - magnitude);
//                        a2 *= d;
//                        a2.y = this.pullLiftVelocity;
//                        DamageInfo damageInfo = new DamageInfo
//                        {
//                            attacker = base.gameObject,
//                            damage = this.damageStat * this.damageCoefficient,
//                            position = singularTarget.transform.position,
//                            procCoefficient = this.procCoefficient,
//							damageType = damageType,
							
//                        };
//                        singularTarget.healthComponent.TakeDamageForce(a2 * (Weight/2), true, true);
//                        singularTarget.healthComponent.TakeDamage(damageInfo);
//                        GlobalEventManager.instance.OnHitEnemy(damageInfo, singularTarget.healthComponent.gameObject);


//						//EffectManager.SpawnEffect(Modules.Assets.voidjailerEffect, new EffectData
//						//{
//						//	origin = singularTarget.transform.position,
//						//	scale = 1f,
//						//	rotation = Quaternion.LookRotation(aimRay.direction),

//						//}, true);

//						if (this.pullTracerPrefab)
//						{
//							Vector3 position = singularTarget.transform.position;
//							Vector3 start = base.characterBody.corePosition;
//							Transform transform = base.FindModelChild(this.muzzleString);
//							if (transform)
//							{
//								start = transform.position;
//							}
//							EffectData effectData = new EffectData
//							{
//								origin = position,
//								start = start
//							};
//							EffectManager.SpawnEffect(pullTracerPrefab, effectData, true);
//						}
//					}
//				}
//			}
//		}

//		public override void OnExit()
//        {
//            base.OnExit();
//        }


//        public override void FixedUpdate()
//        {
//            base.FixedUpdate();
//			fireage += Time.fixedDeltaTime;
//			if (fireage > duration/fireInterval)
//            {
//				GetMaxWeight();
//				fireage = 0f;
//            } 
//			if (base.fixedAge > this.duration)
//			{
//				this.outer.SetNextStateToMain();
//				return;
//			}
//		}




//        public override InterruptPriority GetMinimumInterruptPriority()
//        {
//            return InterruptPriority.PrioritySkill;
//        }

//    }
//}
