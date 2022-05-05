using EntityStates;
using R2API;
using RoR2;
using RoR2.Orbs;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EntityStates.LunarExploderMonster;
using RoR2.Projectile;
using EntityStates.MiniMushroom;
using UnityEngine.Networking;

namespace ShiggyMod.Modules.Survivors
{
	public class ShiggyController : MonoBehaviour
	{
		public float overloadingtimer;
		public float magmawormtimer;
		public float vagranttimer;
		public float alphaconstructshieldtimer;
		public float lunarTimer;
		public float larvaTimer;
        public float attackSpeedGain;
        public float mortarTimer;
		private float voidmortarTimer;
		private float voidjailerTimer;

		public Transform mortarIndicatorInstance;
		private Ray downRay;
        public Transform voidmortarIndicatorInstance;
        public float maxTrackingDistance = 60f;
		public float maxTrackingAngle = 60f;
		public float trackerUpdateFrequency = 10f;
		private HurtBox trackingTarget;
		private CharacterBody characterBody;
		private InputBankTest inputBank;
		private float trackerUpdateStopwatch;
        private ChildLocator child;
        private Indicator indicator;
		private readonly BullseyeSearch search = new BullseyeSearch();
		private CharacterMaster characterMaster;
		private CharacterBody origCharacterBody;
		private string origName;
		public ShiggyMasterController Shiggymastercon;
		public ShiggyController Shiggycon;

		public bool larvabuffGiven;
		public bool verminjumpbuffGiven;
        private uint minimushrumsoundID;
        public GameObject mushroomWard;
		public GameObject magmawormWard;
		public GameObject overloadingWard;

		private void Awake()
		{
			child = GetComponentInChildren<ChildLocator>();

			indicator = new Indicator(gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/HuntressTrackingIndicator"));
            //On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
			characterBody = gameObject.GetComponent<CharacterBody>();
			inputBank = gameObject.GetComponent<InputBankTest>();

			larvabuffGiven = false;
			verminjumpbuffGiven = false;
		}


        private void Start()
		{

			characterMaster = characterBody.master;
			if (!characterMaster.gameObject.GetComponent<ShiggyMasterController>())
			{
				Shiggymastercon = characterMaster.gameObject.AddComponent<ShiggyMasterController>();
			}


		}

		public HurtBox GetTrackingTarget()
		{
			return this.trackingTarget;
		}

		private void OnEnable()
		{
			this.indicator.active = true;
		}

		private void OnDisable()
		{
			this.indicator.active = false;
		}

		private void OnDestroy()
        {			
			if (mortarIndicatorInstance) EntityState.Destroy(mortarIndicatorInstance.gameObject);
		}

		private void FixedUpdate()
		{
			
			this.trackerUpdateStopwatch += Time.fixedDeltaTime;
			if (this.trackerUpdateStopwatch >= 1f / this.trackerUpdateFrequency)
			{
				this.trackerUpdateStopwatch -= 1f / this.trackerUpdateFrequency;
				HurtBox hurtBox = this.trackingTarget;
				Ray aimRay = new Ray(this.inputBank.aimOrigin, this.inputBank.aimDirection);
				this.SearchForTarget(aimRay);
				this.indicator.targetTransform = (this.trackingTarget ? this.trackingTarget.transform : null);
			}
			//overloadingworm buff
			if (characterBody.HasBuff(Modules.Buffs.overloadingwormBuff.buffIndex))
			{
				if (!NetworkServer.active)
				{
					return;
				}
				if (overloadingWard == null)
				{
					this.overloadingWard = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/NearbyDamageBonusIndicator"), characterBody.footPosition, Quaternion.identity);
					this.overloadingWard.transform.parent = characterBody.transform;
					//this.magmawormWard.GetComponent<TeamFilter>().teamIndex = characterBody.teamComponent.teamIndex;

					if (overloadingtimer > StaticValues.overloadingInterval / characterBody.attackSpeed)
					{
						overloadingtimer = 0f;
						OverloadingFire();

					}
					else
					{
						overloadingtimer += Time.fixedDeltaTime;
					}
				}
			}
			else if (!characterBody.HasBuff(Modules.Buffs.overloadingwormBuff.buffIndex))
			{
				if (this.overloadingWard)
				{
					EntityState.Destroy(this.overloadingWard);
				}
			}

			//magmaworm buff
			if (characterBody.HasBuff(Modules.Buffs.magmawormBuff.buffIndex))
			{
				if (!NetworkServer.active)
				{
					return;
				}
				if (magmawormWard == null)
				{
					this.magmawormWard = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/NearbyDamageBonusIndicator"), characterBody.footPosition, Quaternion.identity);
					this.magmawormWard.transform.parent = characterBody.transform;
					//this.magmawormWard.GetComponent<TeamFilter>().teamIndex = characterBody.teamComponent.teamIndex;

					if (magmawormtimer > StaticValues.magmawormInterval/characterBody.attackSpeed)
					{
						magmawormtimer = 0f;
						MagmawormFire();

					}
					else
					{
						magmawormtimer += Time.fixedDeltaTime;
					}
				}
			}
			else if (!characterBody.HasBuff(Modules.Buffs.magmawormBuff.buffIndex))
			{
				if (this.magmawormWard)
				{
					EntityState.Destroy(this.magmawormWard);
				}
			}

			//vagrant disablebuff
			if (characterBody.HasBuff(Modules.Buffs.vagrantdisableBuff.buffIndex))
			{
				if (vagranttimer > 1f)
				{
					int buffCountToApply = characterBody.GetBuffCount(Modules.Buffs.vagrantdisableBuff.buffIndex);
					if (buffCountToApply > 1)
					{
						if (buffCountToApply >= 2)
						{
							characterBody.RemoveBuff(Modules.Buffs.vagrantdisableBuff.buffIndex);
							vagranttimer = 0f;
						}
					}
					else
					{
						characterBody.RemoveBuff(Modules.Buffs.vagrantdisableBuff.buffIndex);
						characterBody.AddBuff(Modules.Buffs.vagrantBuff);

					}
				}
				else vagranttimer += Time.fixedDeltaTime;
			}
            


		    //roboballmini buff
			if (characterBody.HasBuff(Modules.Buffs.roboballminiBuff.buffIndex))
            {
                if (!characterBody.characterMotor.isGrounded)
				{
					if (characterBody.inputBank.jump.down)
					{
						characterBody.AddBuff(Buffs.flyBuff.buffIndex);
						base.transform.position = characterBody.transform.position;
						if (characterBody.hasEffectiveAuthority && characterBody.characterMotor)
						{
							if (characterBody.inputBank.moveVector != Vector3.zero)
							{
								characterBody.characterMotor.velocity = characterBody.inputBank.moveVector * (characterBody.moveSpeed * Modules.StaticValues.roboballboostMultiplier);
								characterBody.characterMotor.disableAirControlUntilCollision = false;
							}
						}
					}
                    else if(!characterBody.inputBank.jump.down)
					{
						characterBody.RemoveBuff(Buffs.flyBuff.buffIndex);
						this.characterBody.characterMotor.disableAirControlUntilCollision = true;
					}

				}
				else
				if (characterBody.characterMotor.isGrounded && !characterBody.inputBank.jump.down)
				{
					characterBody.RemoveBuff(Buffs.flyBuff.buffIndex);
					this.characterBody.characterMotor.disableAirControlUntilCollision = true;
				}

			}


			//mini mushrum buff
			if (characterBody.HasBuff(Modules.Buffs.minimushrumBuff.buffIndex))
			{
				if (!NetworkServer.active)
				{
					return;
				}
				if (this.mushroomWard == null)
				{
					this.minimushrumsoundID = Util.PlaySound(Plant.healSoundLoop, characterBody.modelLocator.modelTransform.gameObject);
					this.mushroomWard = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/MiniMushroomWard"), characterBody.footPosition, Quaternion.identity);
					this.mushroomWard.transform.parent = characterBody.transform;
					this.mushroomWard.GetComponent<TeamFilter>().teamIndex = characterBody.teamComponent.teamIndex;
					if (this.mushroomWard)
					{
						HealingWard component = this.mushroomWard.GetComponent<HealingWard>();
						component.healFraction = Modules.StaticValues.minimushrumhealFraction;
						component.healPoints = 0f;
						component.Networkradius = Modules.StaticValues.minimushrumRadius;
                        component.interval = Modules.StaticValues.minimushrumInterval;
                        //component.healTimer = Modules.StaticValues.minimushrumhealFraction;
					}
					NetworkServer.Spawn(this.mushroomWard);
				}
			}
			else if (!characterBody.HasBuff(Modules.Buffs.minimushrumBuff.buffIndex))
            {
				if (this.mushroomWard)
				{
					AkSoundEngine.StopPlayingID(this.minimushrumsoundID);
					Util.PlaySound(Plant.healSoundStop, base.gameObject);
					EntityState.Destroy(this.mushroomWard);
				}
			}

			//alpha shield buff
			if (characterBody.hasEffectiveAuthority)
			{
				if (characterBody.HasBuff(Modules.Buffs.alphashieldoffBuff.buffIndex))
				{

					if (alphaconstructshieldtimer > 1f)
					{
						int buffCountToApply = characterBody.GetBuffCount(Modules.Buffs.alphashieldoffBuff.buffIndex);
						if (buffCountToApply > 1)
						{
							if (buffCountToApply >= 2)
							{
								characterBody.RemoveBuff(Modules.Buffs.alphashieldoffBuff.buffIndex);
								alphaconstructshieldtimer = 0f;
							}
						}
						else
						{
							characterBody.RemoveBuff(Modules.Buffs.alphashieldoffBuff.buffIndex);
							characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);

						}
					}
					else alphaconstructshieldtimer += Time.fixedDeltaTime;
				}

			}
			

			//Standing still/not moving buffs
			if (characterBody.GetNotMoving())
			{

				//hermitcrab mortarbuff
				if (characterBody.HasBuff(Modules.Buffs.hermitcrabmortarBuff))
				{
					if (!this.mortarIndicatorInstance)
					{
						CreateMortarIndicator();
					}
					mortarTimer += Time.fixedDeltaTime;
					if (mortarTimer >= Modules.StaticValues.mortarbaseDuration/ (characterBody.attackSpeed))
					{
						characterBody.AddBuff(Modules.Buffs.hermitcrabmortararmorBuff);
						mortarTimer = 0f;
						FireMortar();
					}
				}
                else
				{
					if (this.mortarIndicatorInstance) EntityState.Destroy(this.mortarIndicatorInstance.gameObject);
					characterBody.SetBuffCount(Modules.Buffs.hermitcrabmortararmorBuff.buffIndex, 0);

				}

				//voidbarnacle mortarbuff
				if (characterBody.HasBuff(Modules.Buffs.voidbarnaclemortarBuff))
				{
					if (!this.voidmortarIndicatorInstance)
					{
						CreateVoidMortarIndicator();
					}
					voidmortarTimer += Time.fixedDeltaTime;
					if (voidmortarTimer >= Modules.StaticValues.voidmortarbaseDuration/ (characterBody.armor/characterBody.baseArmor))
					{
						characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarattackspeedBuff);
						attackSpeedGain = Modules.StaticValues.voidmortarattackspeedGain * characterBody.GetBuffCount(Modules.Buffs.voidbarnaclemortarattackspeedBuff);
						voidmortarTimer = 0f;
						FireMortar();
					}
				}
				else
				{
					if (this.voidmortarIndicatorInstance) EntityState.Destroy(this.voidmortarIndicatorInstance.gameObject);
					characterBody.SetBuffCount(Modules.Buffs.voidbarnaclemortarattackspeedBuff.buffIndex, 0);
				}
			}

			else if (!characterBody.GetNotMoving())
			{
				if (this.mortarIndicatorInstance) EntityState.Destroy(this.mortarIndicatorInstance.gameObject);
				characterBody.SetBuffCount(Modules.Buffs.hermitcrabmortararmorBuff.buffIndex, 0);
				if (this.voidmortarIndicatorInstance) EntityState.Destroy(this.voidmortarIndicatorInstance.gameObject);
				characterBody.SetBuffCount(Modules.Buffs.voidbarnaclemortarattackspeedBuff.buffIndex, 0);

				//voidjailer buff
				if (characterBody.HasBuff(Modules.Buffs.voidjailerBuff.buffIndex))
				{
					voidjailerTimer += Time.fixedDeltaTime;
					if (voidjailerTimer > StaticValues.voidjailerInterval / (characterBody.moveSpeed / 7))
					{
						voidjailerTimer = 0f;
						VoidJailerPull();
					}
				}
			}

			//verminjump buff
			if (characterBody.HasBuff(Buffs.pestjumpBuff) && !verminjumpbuffGiven)
			{
				verminjumpbuffGiven = true;
				characterBody.characterMotor.jumpCount += Modules.StaticValues.verminjumpStacks;
				characterBody.maxJumpCount += Modules.StaticValues.verminjumpStacks;
				characterBody.baseJumpCount += Modules.StaticValues.verminjumpStacks;
				characterBody.jumpPower += Modules.StaticValues.verminjumpPower;
                characterBody.baseJumpPower += Modules.StaticValues.verminjumpPower;
            }
			else
			{
				if (!characterBody.HasBuff(Buffs.pestjumpBuff))
				{
					verminjumpbuffGiven = false;
				}
			}
			//larvajump buff
			if (characterBody.HasBuff(Buffs.larvajumpBuff))
			{
                if (!larvabuffGiven)
                {
					larvabuffGiven = true;
					characterBody.characterMotor.jumpCount += Modules.StaticValues.larvajumpStacks;
					characterBody.maxJumpCount += Modules.StaticValues.larvajumpStacks;
					characterBody.baseJumpCount += Modules.StaticValues.larvajumpStacks;
					characterBody.jumpPower += Modules.StaticValues.larvajumpPower;
					characterBody.baseJumpPower += Modules.StaticValues.larvajumpPower;
					characterBody.maxJumpHeight = Trajectory.CalculateApex(characterBody.jumpPower);
                }

				if (characterBody.inputBank.jump.justPressed && characterBody && characterBody.characterMotor.jumpCount < characterBody.maxJumpCount)
				{					
					Vector3 footPosition = characterBody.footPosition;
					EffectManager.SpawnEffect(Modules.Assets.larvajumpEffect, new EffectData
					{
						origin = footPosition,
						scale = Modules.StaticValues.larvaRadius
					}, true);

					BlastAttack blastAttack = new BlastAttack();
					blastAttack.radius = Modules.StaticValues.larvaRadius;
					blastAttack.procCoefficient = Modules.StaticValues.larvaProcCoefficient;
					blastAttack.position = characterBody.footPosition;
					blastAttack.attacker = base.gameObject;
					blastAttack.crit = Util.CheckRoll(characterBody.crit, characterBody.master);
					blastAttack.baseDamage = characterBody.damage * Modules.StaticValues.larvaDamageCoefficient * (characterBody.jumpPower / 5);
					blastAttack.falloffModel = BlastAttack.FalloffModel.None;
					blastAttack.baseForce = Modules.StaticValues.larvaForce;
					blastAttack.teamIndex = characterBody.teamComponent.teamIndex;
					blastAttack.damageType = DamageType.Generic;
					blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;

					blastAttack.Fire();
					ApplyDoT();
				}

                if (!characterBody.characterMotor.isGrounded)
                {
					larvaTimer += Time.fixedDeltaTime;
                }
				if(characterBody.characterMotor.isGrounded && larvaTimer > 1f)
                {
					larvaTimer = 0f;
					Vector3 footPosition = characterBody.footPosition;
					EffectManager.SpawnEffect(Modules.Assets.larvajumpEffect, new EffectData
					{
						origin = footPosition,
						scale = Modules.StaticValues.larvaRadius
					}, true);

					BlastAttack blastAttack = new BlastAttack();
					blastAttack.radius = Modules.StaticValues.larvaRadius;
					blastAttack.procCoefficient = Modules.StaticValues.larvaProcCoefficient;
					blastAttack.position = characterBody.footPosition;
					blastAttack.attacker = base.gameObject;
					blastAttack.crit = Util.CheckRoll(characterBody.crit, characterBody.master);
					blastAttack.baseDamage = characterBody.damage * Modules.StaticValues.larvaDamageCoefficient * (characterBody.jumpPower / 5);
					blastAttack.falloffModel = BlastAttack.FalloffModel.None;
					blastAttack.baseForce = Modules.StaticValues.larvaForce;
					blastAttack.teamIndex = characterBody.teamComponent.teamIndex;
					blastAttack.damageType = DamageType.Generic;
					blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
					blastAttack.Fire();
					ApplyDoT();
				}
			}
			else
            {
				if (!characterBody.HasBuff(Buffs.larvajumpBuff))
                {
					larvabuffGiven = false;
				}
			}
			//lunar exploder buff
            if (characterBody.HasBuff(Buffs.lunarexploderBuff))
            {
				if(characterBody.healthComponent.combinedHealth < characterBody.healthComponent.fullCombinedHealth / 2)
                {
					lunarTimer += Time.fixedDeltaTime;
					if (characterBody.hasEffectiveAuthority && lunarTimer >= Modules.StaticValues.lunarexploderbaseDuration)
					{
						lunarTimer = 0f;
						for (int i = 0; i < Modules.StaticValues.lunarexploderprojectileCount; i++)
						{
							float num = 360f / Modules.StaticValues.lunarexploderprojectileCount;
							Vector3 forward = Util.QuaternionSafeLookRotation(characterBody.transform.forward, characterBody.transform.up) * Util.ApplySpread(Vector3.forward, 0f, 0f, 1f, 1f, num * (float)i, 0f);
							FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
							fireProjectileInfo.projectilePrefab = DeathState.projectilePrefab;
							fireProjectileInfo.position = characterBody.corePosition + Vector3.up * DeathState.projectileVerticalSpawnOffset;
							fireProjectileInfo.rotation = Util.QuaternionSafeLookRotation(forward);
							fireProjectileInfo.owner = characterBody.gameObject;
							fireProjectileInfo.damage = characterBody.damage * Modules.StaticValues.lunarexploderDamageCoefficient;
							fireProjectileInfo.crit = Util.CheckRoll(characterBody.crit, characterBody.master);
							ProjectileManager.instance.FireProjectile(fireProjectileInfo);
						}
						if (DeathState.deathExplosionEffect)
						{
							EffectManager.SpawnEffect(DeathState.deathExplosionEffect, new EffectData
							{
								origin = characterBody.corePosition,
								scale = Modules.StaticValues.lunarexploderRadius
							}, true);
						}
					}
				}
            }


		}

		public void MagmawormFire()
		{
			Ray aimRay = new Ray(this.inputBank.aimOrigin, this.inputBank.aimDirection);
			BullseyeSearch search = new BullseyeSearch
			{

				teamMaskFilter = TeamMask.GetEnemyTeams(characterBody.teamComponent.teamIndex),
				filterByLoS = false,
				searchOrigin = characterBody.corePosition,
				searchDirection = UnityEngine.Random.onUnitSphere,
				sortMode = BullseyeSearch.SortMode.Distance,
				maxDistanceFilter = StaticValues.magmawormRadius,
				maxAngleFilter = 360f
			};

			search.RefreshCandidates();
			search.FilterOutGameObject(characterBody.gameObject);



			List<HurtBox> target = search.GetResults().ToList<HurtBox>();
			foreach (HurtBox singularTarget in target)
			{
				if (singularTarget)
				{
					if (singularTarget.healthComponent && singularTarget.healthComponent.body)
					{
						InflictDotInfo info = new InflictDotInfo();
						info.attackerObject = characterBody.gameObject;
						info.victimObject = singularTarget.healthComponent.body.gameObject;
						info.duration = Modules.StaticValues.magmawormDuration;
						info.dotIndex = DotController.DotIndex.Burn;
						info.totalDamage = characterBody.damage * Modules.StaticValues.magmawormCoefficient;
						info.damageMultiplier = 1f;

						RoR2.StrengthenBurnUtils.CheckDotForUpgrade(characterBody.inventory, ref info);
						DotController.InflictDot(ref info);
					}
				}
			}

		}

		public void VoidJailerPull()
		{
			BullseyeSearch search = new BullseyeSearch
			{

				teamMaskFilter = TeamMask.GetEnemyTeams(characterBody.teamComponent.teamIndex),
				filterByLoS = false,
				searchOrigin = characterBody.corePosition,
				searchDirection = UnityEngine.Random.onUnitSphere,
				sortMode = BullseyeSearch.SortMode.Distance,
				maxDistanceFilter = StaticValues.voidjailermaxpullDistance,
				maxAngleFilter = 360f
			};

			search.RefreshCandidates();
			search.FilterOutGameObject(characterBody.gameObject);



			List<HurtBox> target = search.GetResults().ToList<HurtBox>();
			foreach (HurtBox singularTarget in target)
			{
				if (singularTarget)
				{
					Vector3 a = singularTarget.transform.position - characterBody.corePosition;
					float magnitude = a.magnitude;
					Vector3 vector = a / magnitude;
					if (singularTarget.healthComponent && singularTarget.healthComponent.body)
					{
						float Weight = 1f;
						if (singularTarget.healthComponent.body.characterMotor)
						{
							Weight = singularTarget.healthComponent.body.characterMotor.mass;
						}
						else if (singularTarget.healthComponent.body.rigidbody)
						{
							Weight = singularTarget.healthComponent.body.rigidbody.mass;
						}
						Vector3 a2 = vector;
						float d = Trajectory.CalculateInitialYSpeedForHeight(Mathf.Abs(StaticValues.voidjailerminpullDistance - magnitude)) * Mathf.Sign(StaticValues.voidjailerminpullDistance - magnitude);
						a2 *= d;
						a2.y = StaticValues.voidjailerpullLiftVelocity;
						DamageInfo damageInfo = new DamageInfo
						{
							attacker = base.gameObject,
							damage = characterBody.damage * Modules.StaticValues.voidjailerDamageCoeffecient,
							position = singularTarget.transform.position,
							procCoefficient = 0.5f,
							damageType = DamageType.SlowOnHit,

						};
						singularTarget.healthComponent.TakeDamageForce(a2 * (Weight / 2), true, true);
						singularTarget.healthComponent.TakeDamage(damageInfo);
						GlobalEventManager.instance.OnHitEnemy(damageInfo, singularTarget.healthComponent.gameObject);


                        EffectManager.SpawnEffect(Modules.Assets.voidjailerEffect, new EffectData
                        {
                            origin = singularTarget.transform.position,
                            scale = 1f,

                        }, true);

						Vector3 position = singularTarget.transform.position;
						Vector3 start = characterBody.corePosition;
						Transform transform = child.FindChild("LHand").transform;
						if (transform)
						{
							start = transform.position;
						}
						EffectData effectData = new EffectData
						{
							origin = position,
							start = start
						};
						EffectManager.SpawnEffect(Modules.Assets.voidjailermuzzleEffect, effectData, true);
						
					}
				}
			}
		}
		public void ApplyDoT()
		{
			Ray aimRay = new Ray(this.inputBank.aimOrigin, this.inputBank.aimDirection);
			BullseyeSearch search = new BullseyeSearch
			{

				teamMaskFilter = TeamMask.GetEnemyTeams(characterBody.teamComponent.teamIndex),
				filterByLoS = false,
				searchOrigin = characterBody.corePosition,
				searchDirection = UnityEngine.Random.onUnitSphere,
				sortMode = BullseyeSearch.SortMode.Distance,
				maxDistanceFilter = Modules.StaticValues.larvaRadius,
				maxAngleFilter = 360f
			};

			search.RefreshCandidates();
			search.FilterOutGameObject(characterBody.gameObject);



			List<HurtBox> target = search.GetResults().ToList<HurtBox>();
			foreach (HurtBox singularTarget in target)
			{
				if (singularTarget)
				{
					if (singularTarget.healthComponent && singularTarget.healthComponent.body)
					{
						InflictDotInfo info = new InflictDotInfo();
						info.attackerObject = characterBody.gameObject;
						info.victimObject = singularTarget.healthComponent.body.gameObject;
						info.duration = Modules.StaticValues.decayDamageTimer;
						info.dotIndex = Modules.Dots.decayDot;

						DotController.InflictDot(ref info);
					}
				}
			}
		}

		public  void Update()
		{
			if (this.mortarIndicatorInstance) this.UpdateIndicator();
		}

		private void UpdateIndicator()
		{
			if (this.mortarIndicatorInstance)
			{
				float maxDistance = 250f;

				this.downRay = new Ray
				{
					direction = Vector3.down,
					origin = base.transform.position
				};

				RaycastHit raycastHit;
				if (Physics.Raycast(this.downRay, out raycastHit, maxDistance, LayerIndex.world.mask))
				{
					this.mortarIndicatorInstance.transform.position = raycastHit.point;
					this.mortarIndicatorInstance.transform.up = raycastHit.normal;
					mortarIndicatorInstance.localScale = Vector3.one * Modules.StaticValues.mortarRadius * (characterBody.armor/characterBody.baseArmor);

				}
			}
			if (this.voidmortarIndicatorInstance)
			{
				float maxDistance = 250f;

				this.downRay = new Ray
				{
					direction = Vector3.down,
					origin = base.transform.position
				};

				RaycastHit raycastHit;
				if (Physics.Raycast(this.downRay, out raycastHit, maxDistance, LayerIndex.world.mask))
				{
					this.voidmortarIndicatorInstance.transform.position = raycastHit.point;
					this.voidmortarIndicatorInstance.transform.up = raycastHit.normal;
					voidmortarIndicatorInstance.localScale = Vector3.one * Modules.StaticValues.voidmortarRadius * characterBody.attackSpeed;

				}
			}
		}
		//hermit crab mortar
		private void CreateMortarIndicator()
		{
			if (EntityStates.Huntress.ArrowRain.areaIndicatorPrefab)
			{
				this.downRay = new Ray
				{
					direction = Vector3.down,
					origin = base.transform.position
				};

				mortarIndicatorInstance = UnityEngine.Object.Instantiate<GameObject>(EntityStates.Huntress.ArrowRain.areaIndicatorPrefab).transform;
				mortarIndicatorInstance.localScale = Vector3.one * Modules.StaticValues.mortarRadius * characterBody.armor;

			}
		}
		//void barnacle mortar	
		private void CreateVoidMortarIndicator()
		{
			if (EntityStates.Huntress.ArrowRain.areaIndicatorPrefab)
			{
				this.downRay = new Ray
				{
					direction = Vector3.down,
					origin = base.transform.position
				};

				voidmortarIndicatorInstance = UnityEngine.Object.Instantiate<GameObject>(EntityStates.Huntress.ArrowRain.areaIndicatorPrefab).transform;
				voidmortarIndicatorInstance.localScale = Vector3.one * Modules.StaticValues.voidmortarRadius * characterBody.attackSpeed;

			}
		}

		//code for both mortars
		private void FireMortar()
		{
			MortarOrb mortarOrb = new MortarOrb
			{
				attacker = characterBody.gameObject,
				damageColorIndex = DamageColorIndex.WeakPoint,
				damageValue = characterBody.damage * Modules.StaticValues.mortarDamageCoefficient * characterBody.attackSpeed * (characterBody.armor/characterBody.baseArmor),
				origin = characterBody.corePosition,
				procChainMask = default(ProcChainMask),
				procCoefficient = 1f,
				isCrit = Util.CheckRoll(characterBody.crit, characterBody.master),
				teamIndex = characterBody.GetComponent<TeamComponent>()?.teamIndex ?? TeamIndex.Neutral,
			};
			if (mortarOrb.target = mortarOrb.PickNextTarget(mortarOrb.origin, Modules.StaticValues.mortarRadius * characterBody.attackSpeed * (characterBody.armor/characterBody.baseArmor)))
			{
				OrbManager.instance.AddOrb(mortarOrb);
			}

		}

		private void SearchForTarget(Ray aimRay)
		{
			this.search.teamMaskFilter = TeamMask.all;
			this.search.filterByLoS = true;
			this.search.searchOrigin = aimRay.origin;
			this.search.searchDirection = aimRay.direction;
			this.search.sortMode = BullseyeSearch.SortMode.Distance;
			this.search.maxDistanceFilter = this.maxTrackingDistance;
			this.search.maxAngleFilter = this.maxTrackingAngle;
			this.search.RefreshCandidates();
			this.search.FilterOutGameObject(base.gameObject);
			this.trackingTarget = this.search.GetResults().FirstOrDefault<HurtBox>();
		}

		//overloading orb
		private void OverloadingFire()
		{
			Ray aimRay = new Ray(this.inputBank.aimOrigin, this.inputBank.aimDirection);
			BullseyeSearch search = new BullseyeSearch
			{

				teamMaskFilter = TeamMask.GetEnemyTeams(characterBody.teamComponent.teamIndex),
				filterByLoS = false,
				searchOrigin = characterBody.corePosition,
				searchDirection = UnityEngine.Random.onUnitSphere,
				sortMode = BullseyeSearch.SortMode.Distance,
				maxDistanceFilter = StaticValues.overloadingRadius,
				maxAngleFilter = 360f
			};

			search.RefreshCandidates();
			search.FilterOutGameObject(characterBody.gameObject);

			HurtBox target = this.search.GetResults().FirstOrDefault<HurtBox>();
			ProcChainMask procChainMask1 = default(ProcChainMask);
			procChainMask1.AddProc(ProcType.LightningStrikeOnHit);

			OrbManager.instance.AddOrb(new SimpleLightningStrikeOrb
			{
				attacker = characterBody.gameObject,
				damageColorIndex = DamageColorIndex.Item,
				damageValue = characterBody.damage * Modules.StaticValues.overloadingCoefficient,
				origin = characterBody.corePosition,
				procChainMask = procChainMask1,
				procCoefficient = 1f,
				isCrit = Util.CheckRoll(characterBody.crit, characterBody.master),
				teamIndex = characterBody.GetComponent<TeamComponent>()?.teamIndex ?? TeamIndex.Neutral,
				target = target,

			});

		}

	}




	//mortar orb
	public class MortarOrb : Orb
	{
		public override void Begin()
		{
			base.duration = 0.5f;
			EffectData effectData = new EffectData
			{
				origin = this.origin,
				genericFloat = base.duration
			};
			effectData.SetHurtBoxReference(this.target);
            GameObject effectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/SquidOrbEffect");
            EffectManager.SpawnEffect(effectPrefab, effectData, true);
		}
		public HurtBox PickNextTarget(Vector3 position, float range)
		{
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.searchOrigin = position;
			bullseyeSearch.searchDirection = Vector3.zero;
			bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
			bullseyeSearch.teamMaskFilter.RemoveTeam(this.teamIndex);
			bullseyeSearch.filterByLoS = false;
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
			bullseyeSearch.maxDistanceFilter = range;
			bullseyeSearch.RefreshCandidates();
			List<HurtBox> list = bullseyeSearch.GetResults().ToList<HurtBox>();
			if (list.Count <= 0)
			{
				return null;
			}
			return list[UnityEngine.Random.Range(0, list.Count)];
		}
		public override void OnArrival()
		{
			if (this.target)
			{
				HealthComponent healthComponent = this.target.healthComponent;
				if (healthComponent)
				{
					DamageInfo damageInfo = new DamageInfo
					{
						damage = this.damageValue,
						attacker = this.attacker,
						inflictor = null,
						force = Vector3.zero,
						crit = this.isCrit,
						procChainMask = this.procChainMask,
						procCoefficient = this.procCoefficient,
						position = this.target.transform.position,
						damageColorIndex = this.damageColorIndex
					};
					healthComponent.TakeDamage(damageInfo);
					GlobalEventManager.instance.OnHitEnemy(damageInfo, healthComponent.gameObject);
					GlobalEventManager.instance.OnHitAll(damageInfo, healthComponent.gameObject);
				}
			}
		}

		public float damageValue;
		public GameObject attacker;
		public TeamIndex teamIndex;
		public bool isCrit;
		public ProcChainMask procChainMask;
		public float procCoefficient = 1f;
		public DamageColorIndex damageColorIndex;
	}
}


