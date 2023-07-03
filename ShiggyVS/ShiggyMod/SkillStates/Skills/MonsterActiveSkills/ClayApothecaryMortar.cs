using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using EntityStates.ClayGrenadier;
using UnityEngine.Networking;
using System.Linq;
using RoR2.Projectile;
using System.Collections.Generic;
using R2API;
using ShiggyMod.Modules;

namespace ShiggyMod.SkillStates
{
    public class ClayApothecaryMortar : BaseSkillState
    {
        public float baseDuration = 3.5f;
        public float duration;
        private float baseDurationBeforeBlast = 1.5f;
        private float durationBeforeBlast;
        public ShiggyController Shiggycon;
        private DamageType damageType = DamageType.ClayGoo;


        private float radius = 5f;
		private float searchradius = 100f;
		private float damageCoefficient = Modules.StaticValues.clayapothecarymortarDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 100f;
		private float blastUpwardForce = 1000f;
		private float speedOverride = -1f;

        private Animator modelAnimator;
        private Transform modelTransform;
        private GameObject chargeInstance;
        private bool hasFiredBlast;
        private float healthCostFraction = Modules.StaticValues.clayapothecarymortarHealthCostCoefficient;
        private BlastAttack attack;

        public override void OnEnter()
        {
            base.OnEnter();
            damageType = DamageType.ClayGoo;
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);

            this.modelAnimator = base.GetModelAnimator();
            this.modelTransform = base.GetModelTransform();
            this.duration = baseDuration / this.attackSpeedStat;
            this.durationBeforeBlast = baseDurationBeforeBlast / this.attackSpeedStat;
            Util.PlayAttackSpeedSound(FaceSlam.attackSoundString, base.gameObject, this.attackSpeedStat);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
            base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
            if (base.characterDirection)
            {
                base.characterDirection.moveVector = base.characterDirection.forward;
            }
            Transform transform = base.FindModelChild("Chest");
            if (transform && FaceSlam.chargeEffectPrefab)
            {
                this.chargeInstance = UnityEngine.Object.Instantiate<GameObject>(FaceSlam.chargeEffectPrefab, transform.position, transform.rotation);
                this.chargeInstance.transform.parent = transform;
                ScaleParticleSystemDuration component = this.chargeInstance.GetComponent<ScaleParticleSystemDuration>();
                if (component)
                {
                    component.newDuration = this.durationBeforeBlast;
                }
            }

            Shiggycon = gameObject.GetComponent<ShiggyController>();
        }



        public override void OnExit()
        {
            base.OnExit();
            if (this.chargeInstance)
            {
                EntityState.Destroy(this.chargeInstance);
            }
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

			if (base.fixedAge > this.durationBeforeBlast && !this.hasFiredBlast)
			{
				this.hasFiredBlast = true;
				if (this.chargeInstance)
				{
					EntityState.Destroy(this.chargeInstance);
				}
				Vector3 footPosition = base.characterBody.footPosition;
				EffectManager.SpawnEffect(FaceSlam.blastImpactEffect, new EffectData
				{
					origin = footPosition,
					scale = radius
				}, true);
				if (NetworkServer.active && base.healthComponent)
				{
					DamageInfo damageInfo = new DamageInfo();
					damageInfo.damage = base.healthComponent.fullCombinedHealth * healthCostFraction;
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
				if (base.isAuthority)
                {
                    
                    if (this.modelTransform)
					{
						Transform transform = base.FindModelChild("Chest");
						if (transform)
						{
							this.attack = new BlastAttack();
							this.attack.attacker = base.gameObject;
							this.attack.inflictor = base.gameObject;
							this.attack.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
							this.attack.baseDamage = this.damageStat * damageCoefficient;
							this.attack.baseForce = force;
							this.attack.position = transform.position;
							this.attack.radius = radius;
							this.attack.falloffModel = BlastAttack.FalloffModel.None;
							this.attack.bonusForce = new Vector3(0f, blastUpwardForce, 0f);
							this.attack.damageType = damageType;
							this.attack.Fire();

                            this.attack.AddModdedDamageType(Damage.shiggyDecay);

                            if(this.attack.Fire().hitCount > 0)
                            {
                                this.OnHitEnemyAuthority();
                            }
						}
					}
					Vector3 position = footPosition;
					Vector3 up = Vector3.up;
					RaycastHit raycastHit;
					if (Physics.Raycast(base.GetAimRay(), out raycastHit, 1000f, LayerIndex.world.mask))
					{
						position = raycastHit.point;
					}
					BullseyeSearch bullseyeSearch = new BullseyeSearch();
					bullseyeSearch.viewer = base.characterBody;
					bullseyeSearch.teamMaskFilter = TeamMask.GetEnemyTeams(base.GetTeam());
					bullseyeSearch.sortMode = BullseyeSearch.SortMode.DistanceAndAngle;
					bullseyeSearch.minDistanceFilter = 0f;
					bullseyeSearch.maxDistanceFilter = searchradius;
					bullseyeSearch.searchOrigin = base.transform.position;
					bullseyeSearch.searchDirection = UnityEngine.Random.onUnitSphere;
					bullseyeSearch.maxAngleFilter = 360f;
					bullseyeSearch.filterByLoS = false;
					bullseyeSearch.RefreshCandidates();
					bullseyeSearch.FilterOutGameObject(base.gameObject);


					List<HurtBox> target = bullseyeSearch.GetResults().ToList<HurtBox>();
					foreach (HurtBox singularTarget in target)
					{
						if (singularTarget)
						{
							if (singularTarget.healthComponent && singularTarget.healthComponent.body)
							{								

								ProjectileManager.instance.FireProjectile(
									FaceSlam.projectilePrefab, //prefab
									singularTarget.healthComponent.body.footPosition, //position
									Quaternion.identity, //rotation
									base.gameObject, //owner
									this.damageStat * damageCoefficient, //damage
									force, //force
									Util.CheckRoll(this.critStat, base.characterBody.master), //crit
									DamageColorIndex.Default, //damage color
									null, //target
									speedOverride); //speed

							}
						}
					}
				}
			}

			if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        protected virtual void OnHitEnemyAuthority()
        {

        }


        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
