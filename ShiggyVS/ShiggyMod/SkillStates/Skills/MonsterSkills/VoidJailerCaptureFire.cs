using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using EntityStates.VoidJailer;
using RoR2.Projectile;
using System.Linq;
using EntityStates.VoidJailer.Weapon;
using System.Collections.Generic;

namespace ShiggyMod.SkillStates
{
    public class VoidJailerCaptureFire : BaseSkillState
	{
		public float baseDuration = 1f;
        public float duration;
		public float fireage;
		public ShiggyController Shiggycon;
        private DamageType damageType;


        private float damageCoefficient = Modules.StaticValues.voidjailerDamageCoeffecient;
        private float procCoefficient = 1f;
		public AnimationCurve pullSuitabilityCurve;

		private float pullMinDistance = 5f;
		private float pullMaxDistance = 50f;
		private float pullLiftVelocity = 1f;
        private string muzzleString = "RHand";
        private Vector3 theSpot;
        private float maxWeight;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
			fireage = 0f;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
			Util.PlaySound(CaptureFire.enterSoundString, base.gameObject);

			EffectManager.SpawnEffect(Modules.Assets.voidjailermuzzleEffect, new EffectData
			{
				origin = aimRay.origin,
				scale = 1f,
				rotation = Quaternion.LookRotation(aimRay.direction),

			}, false);

		}
		public void GetMaxWeight()
		{
			Ray aimRay = base.GetAimRay();
			theSpot = aimRay.origin + 5 * aimRay.direction;
			BullseyeSearch search = new BullseyeSearch
			{

				teamMaskFilter = TeamMask.GetEnemyTeams(base.GetTeam()),
				filterByLoS = false,
				searchOrigin = theSpot,
				searchDirection = UnityEngine.Random.onUnitSphere,
				sortMode = BullseyeSearch.SortMode.Distance,
				maxDistanceFilter = pullMaxDistance,
				maxAngleFilter = 360f
			};

			search.RefreshCandidates();
			search.FilterOutGameObject(base.gameObject);



			List<HurtBox> target = search.GetResults().ToList<HurtBox>();
			foreach (HurtBox singularTarget in target)
			{
				if (singularTarget)
				{
                    Vector3 a = singularTarget.transform.position - aimRay.origin;
                    float magnitude = a.magnitude;
                    Vector3 vector = a / magnitude;
                    if (singularTarget.healthComponent && singularTarget.healthComponent.body)
					{
						if (singularTarget.healthComponent.body.characterMotor)
						{
							if (singularTarget.healthComponent.body.characterMotor.mass > maxWeight)
							{
								maxWeight = singularTarget.healthComponent.body.characterMotor.mass;
							}
						}
						else if (singularTarget.healthComponent.body.rigidbody)
						{
							if (singularTarget.healthComponent.body.rigidbody.mass > maxWeight)
							{
								maxWeight = singularTarget.healthComponent.body.rigidbody.mass;
							}
						}
						Vector3 a2 = vector;
                        float d = Trajectory.CalculateInitialYSpeedForHeight(Mathf.Abs(this.pullMinDistance - magnitude)) * Mathf.Sign(this.pullMinDistance - magnitude);
                        a2 *= d;
                        a2.y = this.pullLiftVelocity;
                        DamageInfo damageInfo = new DamageInfo
                        {
                            attacker = base.gameObject,
                            damage = this.damageStat * this.damageCoefficient,
                            position = singularTarget.transform.position,
                            procCoefficient = this.procCoefficient
                        };
                        singularTarget.healthComponent.TakeDamageForce(a2 * (maxWeight), true, true);
                        singularTarget.healthComponent.TakeDamage(damageInfo);
                        GlobalEventManager.instance.OnHitEnemy(damageInfo, singularTarget.healthComponent.gameObject);


						EffectManager.SpawnEffect(Modules.Assets.voidjailerEffect, new EffectData
						{
							origin = singularTarget.transform.position,
							scale = 1f,
							rotation = Quaternion.LookRotation(aimRay.direction),

						}, true);
					}
				}
			}
		}

		public override void OnExit()
        {
            base.OnExit();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();
			fireage += Time.fixedDeltaTime;
			if (fireage > duration/4)
            {
				GetMaxWeight();
				fireage = 0f;
            } 
			if (base.fixedAge > this.duration)
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
