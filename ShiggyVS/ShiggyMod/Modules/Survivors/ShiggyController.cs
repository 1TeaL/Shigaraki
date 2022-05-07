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
using ExtraSkillSlots;

namespace ShiggyMod.Modules.Survivors
{
	public class ShiggyController : MonoBehaviour
	{
		string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";

		public float strengthMultiplier;
		public float rangedMultiplier;

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

		private Ray downRay;
		public Transform mortarIndicatorInstance;
        public Transform voidmortarIndicatorInstance;

        public float maxTrackingDistance = 60f;
		public float maxTrackingAngle = 60f;
		public float trackerUpdateFrequency = 10f;
		private Indicator indicator;
		private Indicator passiveindicator;
		private Indicator activeindicator;
		private HurtBox trackingTarget;

		private CharacterBody characterBody;
		private InputBankTest inputBank;
		private float trackerUpdateStopwatch;
        private ChildLocator child;
		private readonly BullseyeSearch search = new BullseyeSearch();
		private CharacterMaster characterMaster;

		public ShiggyMasterController Shiggymastercon;
		public ShiggyController Shiggycon;

		public bool larvabuffGiven;
		public bool verminjumpbuffGiven;
        private uint minimushrumsoundID;
        public GameObject mushroomWard;
		public GameObject magmawormWard;
		public GameObject overloadingWard;

		private ExtraSkillLocator extraskillLocator;
		public bool alphacontructpassiveDef;
		public bool beetlepassiveDef;
		public bool pestpassiveDef;
		public bool verminpassiveDef;
		public bool guppassiveDef;
		public bool hermitcrabpassiveDef;
		public bool larvapassiveDef;
		public bool lesserwisppassiveDef;
		public bool lunarexploderpassiveDef;
		public bool minimushrumpassiveDef;
		public bool roboballminibpassiveDef;
		public bool voidbarnaclepassiveDef;
		public bool voidjailerpassiveDef;
		public bool stonetitanpassiveDef;
		public bool magmawormpassiveDef;
		public bool overloadingwormpassiveDef;

		public bool alloyvultureflyDef;
		public bool beetleguardslamDef;
		public bool bisonchargeDef;
		public bool bronzongballDef;
		public bool clayapothecarymortarDef;
		public bool claytemplarminigunDef;
		public bool greaterwispballDef;
		public bool impblinkDef;
		public bool jellyfishnovaDef;
		public bool lemurianfireballDef;
		public bool lunargolemshotsDef;
		public bool lunarwispminigunDef;
		public bool parentteleportDef;
		public bool stonegolemlaserDef;
		public bool voidreaverportalDef;
		public bool beetlequeenshotgunDef;
		public bool grovetenderhookDef;
		public bool claydunestriderballDef;
		public bool soluscontrolunityknockupDef;
		public bool xiconstructbeamDef;
		public bool voiddevastatorhomingDef;
		public bool scavengerthqwibDef;


		private void Awake()
		{
			child = GetComponentInChildren<ChildLocator>();

			indicator = new Indicator(gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/HuntressTrackingIndicator"));
			passiveindicator = new Indicator(gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/EngiMissileTrackingIndicator"));
			activeindicator = new Indicator(gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/RecyclerIndicator"));
			//On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
			characterBody = gameObject.GetComponent<CharacterBody>();
			inputBank = gameObject.GetComponent<InputBankTest>();

			larvabuffGiven = false;
			verminjumpbuffGiven = false;
			strengthMultiplier = 1f;
			rangedMultiplier = 1f;

			alphacontructpassiveDef = false;
			beetlepassiveDef = false;
			pestpassiveDef = false;
			verminpassiveDef = false;
			guppassiveDef = false;
			hermitcrabpassiveDef = false;
			larvapassiveDef = false;
			lesserwisppassiveDef = false;
			lunarexploderpassiveDef = false;
			minimushrumpassiveDef = false;
			roboballminibpassiveDef = false;
			voidbarnaclepassiveDef = false;
			voidjailerpassiveDef = false;

			stonetitanpassiveDef = false;
			magmawormpassiveDef = false;
			overloadingwormpassiveDef = false;


			alloyvultureflyDef = false;
			beetleguardslamDef = false;
			bisonchargeDef = false;
			bronzongballDef = false;
			clayapothecarymortarDef = false;
			claytemplarminigunDef = false;
			greaterwispballDef = false;
			impblinkDef = false;
			jellyfishnovaDef = false;
			lemurianfireballDef = false;
			lunargolemshotsDef = false;
			lunarwispminigunDef = false;
			parentteleportDef = false;
			stonegolemlaserDef = false;
			voidreaverportalDef = false;

			beetlequeenshotgunDef = false;
			grovetenderhookDef = false;
			claydunestriderballDef = false;
			soluscontrolunityknockupDef = false;
			xiconstructbeamDef = false;
			voiddevastatorhomingDef = false;
			scavengerthqwibDef = false;
		}


        private void Start()
		{

			characterMaster = characterBody.master;
			if (!characterMaster.gameObject.GetComponent<ShiggyMasterController>())
			{
				Shiggymastercon = characterMaster.gameObject.AddComponent<ShiggyMasterController>();
			}

			extraskillLocator = base.GetComponent<ExtraSkillLocator>();			

			alphacontructpassiveDef = false;
			beetlepassiveDef = false;
			pestpassiveDef = false;
			verminpassiveDef = false;
			guppassiveDef = false;
			hermitcrabpassiveDef = false;
			larvapassiveDef = false;
			lesserwisppassiveDef = false;
			lunarexploderpassiveDef = false;
			minimushrumpassiveDef = false;
			roboballminibpassiveDef = false;
			voidbarnaclepassiveDef = false;
			voidjailerpassiveDef = false;

			stonetitanpassiveDef = false;
			magmawormpassiveDef = false;
			overloadingwormpassiveDef = false;


			alloyvultureflyDef = false;
			beetleguardslamDef = false;
			bisonchargeDef = false;
			bronzongballDef = false;
			clayapothecarymortarDef = false;
			claytemplarminigunDef = false;
			greaterwispballDef = false;
			impblinkDef = false;
			jellyfishnovaDef = false;
			lemurianfireballDef = false;
			lunargolemshotsDef = false;
			lunarwispminigunDef = false;
			parentteleportDef = false;
			stonegolemlaserDef = false;
			voidreaverportalDef = false;

			beetlequeenshotgunDef = false;
			grovetenderhookDef = false;
			claydunestriderballDef = false;
			soluscontrolunityknockupDef = false;
			xiconstructbeamDef = false;
			voiddevastatorhomingDef = false;
			scavengerthqwibDef = false;

		}

		public HurtBox GetTrackingTarget()
		{
			return this.trackingTarget;
		}

		private void OnEnable()
		{
			this.indicator.active = true;
			this.passiveindicator.active = true;
			this.activeindicator.active = true;
		}

		private void OnDisable()
		{
			this.indicator.active = false;
			this.passiveindicator.active = false;
			this.activeindicator.active = false;
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
                Ray aimRay = new Ray(this.inputBank.aimOrigin, this.inputBank.aimDirection);
                this.SearchForTarget(aimRay);
                HurtBox hurtBox = this.trackingTarget;

                if (hurtBox)
                {
                    var name = BodyCatalog.GetBodyName(hurtBox.healthComponent.body.bodyIndex);
                    if (StaticValues.indicatorDict.ContainsKey(name))
                    {
                        if (Modules.StaticValues.indicatorDict[name] == StaticValues.IndicatorType.PASSIVE)
                        {
                            this.passiveindicator.active = true;
							this.activeindicator.active = false;
							this.passiveindicator.targetTransform = this.trackingTarget.transform;

                        }
						else if (Modules.StaticValues.indicatorDict[name] == StaticValues.IndicatorType.ACTIVE)
						{
							this.passiveindicator.active = false;
							this.activeindicator.active = true;
                            this.activeindicator.targetTransform = this.trackingTarget.transform;

                        }

                    }
                    else
                    {
                        this.activeindicator.active = false;
                        this.passiveindicator.active = false;
                        this.indicator.targetTransform = (this.trackingTarget ? this.trackingTarget.transform : null);
                    }

                }
                else
                {
                    this.indicator.active = false;
                    this.activeindicator.active = false;
                    this.passiveindicator.active = false;

                }

            }

            //check quirks
            CheckQuirks();

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
					float num = characterBody.moveSpeed;
					bool isSprinting = characterBody.isSprinting;
					if (isSprinting)
					{
						num /= characterBody.sprintingSpeedMultiplier;
					}
					voidjailerTimer += Time.fixedDeltaTime;
					if (voidjailerTimer > StaticValues.voidjailerInterval / (num/characterBody.baseMoveSpeed) )
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

		public void CheckQuirks()
		{
			//check passive
			if(extraskillLocator.extraFirst.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
				| extraskillLocator.extraSecond.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
				| extraskillLocator.extraThird.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
				| extraskillLocator.extraFourth.skillNameToken == prefix + "ALPHACONSTRUCT_NAME")
			{
                if (!alphacontructpassiveDef)
                {
					characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
					alphacontructpassiveDef = true;
					Shiggymastercon.alphacontructpassiveDef = true;
				}
			}
            else 
            {
				alphacontructpassiveDef = false;
				Shiggymastercon.alphacontructpassiveDef = false;
			}
			if (extraskillLocator.extraFirst.skillNameToken == prefix + "BEETLE_NAME"
				| extraskillLocator.extraSecond.skillNameToken == prefix + "BEETLE_NAME"
				| extraskillLocator.extraThird.skillNameToken == prefix + "BEETLE_NAME"
				| extraskillLocator.extraFourth.skillNameToken == prefix + "BEETLE_NAME")
			{
				strengthMultiplier = StaticValues.beetlestrengthMultiplier;
                if (!beetlepassiveDef)
				{
					characterBody.AddBuff(Modules.Buffs.beetleBuff);
					beetlepassiveDef = true;
					Shiggymastercon.beetlepassiveDef = true;

				}
			}
			else
			{
				strengthMultiplier = 1f;
				beetlepassiveDef = false;
				Shiggymastercon.beetlepassiveDef = true;
			}
			if (extraskillLocator.extraFirst.skillNameToken == prefix + "GUP_NAME"
				| extraskillLocator.extraSecond.skillNameToken == prefix + "GUP_NAME"
				| extraskillLocator.extraThird.skillNameToken == prefix + "GUP_NAME"
				| extraskillLocator.extraFourth.skillNameToken == prefix + "GUP_NAME")
			{
				if (!guppassiveDef)
				{
					characterBody.AddBuff(Modules.Buffs.spikeBuff);
					guppassiveDef = true;
					Shiggymastercon.guppassiveDef = true;

				}
			}
			else
			{
				guppassiveDef = false;
				Shiggymastercon.guppassiveDef = true;
			}
			if (extraskillLocator.extraFirst.skillNameToken == prefix + "LARVA_NAME"
				| extraskillLocator.extraSecond.skillNameToken == prefix + "LARVA_NAME"
				| extraskillLocator.extraThird.skillNameToken == prefix + "LARVA_NAME"
				| extraskillLocator.extraFourth.skillNameToken == prefix + "LARVA_NAME")
			{
                if (!larvapassiveDef)
                {
					characterBody.AddBuff(Modules.Buffs.larvajumpBuff);
					larvapassiveDef = true;
					Shiggymastercon.larvapassiveDef = true;
				}
			}
			else
			{
				larvapassiveDef = false;
				Shiggymastercon.alphacontructpassiveDef = false;
			}
			if (extraskillLocator.extraFirst.skillNameToken == prefix + "LESSERWISP_NAME"
				| extraskillLocator.extraSecond.skillNameToken == prefix + "LESSERWISP_NAME"
				| extraskillLocator.extraThird.skillNameToken == prefix + "LESSERWISP_NAME"
				| extraskillLocator.extraFourth.skillNameToken == prefix + "LESSERWISP_NAME")
			{
				rangedMultiplier = StaticValues.lesserwisprangedMultiplier;
				if (!lesserwisppassiveDef)
				{
					characterBody.AddBuff(Modules.Buffs.lesserwispBuff);
					lesserwisppassiveDef = true;
					Shiggymastercon.lesserwisppassiveDef = true;
				}
			}
			else
			{
				rangedMultiplier = 1f;
				lesserwisppassiveDef = false;
				Shiggymastercon.lesserwisppassiveDef = false;
			}
			if (extraskillLocator.extraFirst.skillNameToken == prefix + "LUNAREXPLODER_NAME"
				| extraskillLocator.extraSecond.skillNameToken == prefix + "LUNAREXPLODER_NAME"
				| extraskillLocator.extraThird.skillNameToken == prefix + "LUNAREXPLODER_NAME"
				| extraskillLocator.extraFourth.skillNameToken == prefix + "LUNAREXPLODER_NAME")
			{
				if (!lunarexploderpassiveDef)
				{
					characterBody.AddBuff(Modules.Buffs.lunarexploderBuff);
					lunarexploderpassiveDef = true;
					Shiggymastercon.lunarexploderpassiveDef = true;
				}
			}
			else
			{
				lunarexploderpassiveDef = false;
				Shiggymastercon.lunarexploderpassiveDef = false;
			}
			if (extraskillLocator.extraFirst.skillNameToken == prefix + "HERMITCRAB_NAME"
				| extraskillLocator.extraSecond.skillNameToken == prefix + "HERMITCRAB_NAME"
				| extraskillLocator.extraThird.skillNameToken == prefix + "HERMITCRAB_NAME"
				| extraskillLocator.extraFourth.skillNameToken == prefix + "HERMITCRAB_NAME")
			{
				if (!hermitcrabpassiveDef)
				{
					characterBody.AddBuff(Modules.Buffs.hermitcrabmortarBuff);
					hermitcrabpassiveDef = true;
					Shiggymastercon.hermitcrabpassiveDef = true;
				}
			}
			else
			{
				hermitcrabpassiveDef = false;
				Shiggymastercon.hermitcrabpassiveDef = false;
			}
			if (extraskillLocator.extraFirst.skillNameToken == prefix + "PEST_NAME"
				| extraskillLocator.extraSecond.skillNameToken == prefix + "PEST_NAME"
				| extraskillLocator.extraThird.skillNameToken == prefix + "PEST_NAME"
				| extraskillLocator.extraFourth.skillNameToken == prefix + "PEST_NAME")
			{
                if (!pestpassiveDef)
				{
					characterBody.AddBuff(Modules.Buffs.pestjumpBuff);
					pestpassiveDef = true;
					Shiggymastercon.pestpassiveDef = true;
				}
			}
			else
			{
				pestpassiveDef = false;
				Shiggymastercon.pestpassiveDef = false;
			}
			if (extraskillLocator.extraFirst.skillNameToken == prefix + "VERMIN_NAME"
				| extraskillLocator.extraSecond.skillNameToken == prefix + "VERMIN_NAME"
				| extraskillLocator.extraThird.skillNameToken == prefix + "VERMIN_NAME"
				| extraskillLocator.extraFourth.skillNameToken == prefix + "VERMIN_NAME")
			{
                if (!verminpassiveDef)
				{
					characterBody.AddBuff(Modules.Buffs.verminsprintBuff);
					verminpassiveDef = true;
					Shiggymastercon.verminpassiveDef = true;
				}
			}
			else
			{
				verminpassiveDef = false;
				Shiggymastercon.verminpassiveDef = false;
			}
			if (extraskillLocator.extraFirst.skillNameToken == prefix + "MINIMUSHRUM_NAME"
				| extraskillLocator.extraSecond.skillNameToken == prefix + "MINIMUSHRUM_NAME"
				| extraskillLocator.extraThird.skillNameToken == prefix + "MINIMUSHRUM_NAME"
				| extraskillLocator.extraFourth.skillNameToken == prefix + "MINIMUSHRUM_NAME")
			{
				if (!minimushrumpassiveDef)
				{
					characterBody.AddBuff(Modules.Buffs.minimushrumBuff);
					minimushrumpassiveDef = true;
					Shiggymastercon.minimushrumpassiveDef = true;
				}
			}
			else
			{
				minimushrumpassiveDef = false;
				Shiggymastercon.minimushrumpassiveDef = false;
			}
			if (extraskillLocator.extraFirst.skillNameToken == prefix + "ROBOBALLMINI_NAME"
				| extraskillLocator.extraSecond.skillNameToken == prefix + "ROBOBALLMINI_NAME"
				| extraskillLocator.extraThird.skillNameToken == prefix + "ROBOBALLMINI_NAME"
				| extraskillLocator.extraFourth.skillNameToken == prefix + "ROBOBALLMINI_NAME")
			{
				if (!roboballminibpassiveDef)
				{
					characterBody.AddBuff(Modules.Buffs.roboballminiBuff);
					roboballminibpassiveDef = true;
					Shiggymastercon.roboballminibpassiveDef = true;
				}
			}
			else
			{
				alphacontructpassiveDef = false;
				Shiggymastercon.roboballminibpassiveDef = false;
			}
			if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDBARNACLE_NAME"
				| extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDBARNACLE_NAME"
				| extraskillLocator.extraThird.skillNameToken == prefix + "VOIDBARNACLE_NAME"
				| extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDBARNACLE_NAME")
			{
				if (!voidbarnaclepassiveDef)
				{
					characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarBuff);
					voidbarnaclepassiveDef = true;
					Shiggymastercon.voidbarnaclepassiveDef = true;
				}
			}
			else
			{
				voidbarnaclepassiveDef = false;
				Shiggymastercon.voidbarnaclepassiveDef = false;
			}
			if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDJAILER_NAME"
				| extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDJAILER_NAME"
				| extraskillLocator.extraThird.skillNameToken == prefix + "VOIDJAILER_NAME"
				| extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDJAILER_NAME")
			{
				if (!voidjailerpassiveDef)
                {
					characterBody.AddBuff(Modules.Buffs.voidjailerBuff);
					voidjailerpassiveDef = true;
					Shiggymastercon.voidjailerpassiveDef = true;
				}

			}
			else
			{
				voidjailerpassiveDef = false;
				Shiggymastercon.voidjailerpassiveDef = false;
			}
			if (extraskillLocator.extraFirst.skillNameToken == prefix + "STONETITAN_NAME"
				| extraskillLocator.extraSecond.skillNameToken == prefix + "STONETITAN_NAME"
				| extraskillLocator.extraThird.skillNameToken == prefix + "STONETITAN_NAME"
				| extraskillLocator.extraFourth.skillNameToken == prefix + "STONETITAN_NAME")
			{
				if (!stonetitanpassiveDef)
                {
					characterBody.AddBuff(Modules.Buffs.stonetitanBuff);
					stonetitanpassiveDef = true;
					Shiggymastercon.stonetitanpassiveDef = true;
				}

			}
			else
			{
				stonetitanpassiveDef = false;
				Shiggymastercon.stonetitanpassiveDef = false;
			}
			if (extraskillLocator.extraFirst.skillNameToken == prefix + "MAGMAWORM_NAME"
				| extraskillLocator.extraSecond.skillNameToken == prefix + "MAGMAWORM_NAME"
				| extraskillLocator.extraThird.skillNameToken == prefix + "MAGMAWORM_NAME"
				| extraskillLocator.extraFourth.skillNameToken == prefix + "MAGMAWORM_NAME")
			{
                if (!magmawormpassiveDef)
                {
					characterBody.AddBuff(Modules.Buffs.magmawormBuff);
					magmawormpassiveDef = true;
					Shiggymastercon.magmawormpassiveDef = true;
				}

			}
			else
			{
				magmawormpassiveDef = false;
				Shiggymastercon.magmawormpassiveDef = false;
			}
			if (extraskillLocator.extraFirst.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
				| extraskillLocator.extraSecond.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
				| extraskillLocator.extraThird.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
				| extraskillLocator.extraFourth.skillNameToken == prefix + "OVERLOADINGWORM_NAME")
			{
                if (!overloadingwormpassiveDef)
                {
					characterBody.AddBuff(Modules.Buffs.overloadingwormBuff);
					overloadingwormpassiveDef = true;
					Shiggymastercon.overloadingwormpassiveDef = true;
				}

			}
			else
			{
				overloadingwormpassiveDef = false;
				Shiggymastercon.overloadingwormpassiveDef = false;
			}

			//check active
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "VULTURE_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "VULTURE_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "VULTURE_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "VULTURE_NAME")
			{
				if (!alloyvultureflyDef)
				{
					alloyvultureflyDef = true;
					Shiggymastercon.alloyvultureflyDef = true;
				}
			}
			else
			{
				alloyvultureflyDef = false;
				Shiggymastercon.alloyvultureflyDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEGUARD_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLEGUARD_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLEGUARD_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "BEETLEGUARD_NAME")
			{
				if (!beetleguardslamDef)
				{
					beetleguardslamDef = true;
					Shiggymastercon.beetleguardslamDef = true;
				}
			}
			else
			{
				beetleguardslamDef = false;
				Shiggymastercon.beetleguardslamDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "BRONZONG_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "BRONZONG_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "BRONZONG_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "BRONZONG_NAME")
			{
				if (!bronzongballDef)
				{
					bronzongballDef = true;
					Shiggymastercon.bronzongballDef = true;
				}
			}
			else
			{
				bronzongballDef = false;
				Shiggymastercon.bronzongballDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "APOTHECARY_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "APOTHECARY_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "APOTHECARY_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "APOTHECARY_NAME")
			{
				if (!clayapothecarymortarDef)
				{
					clayapothecarymortarDef = true;
					Shiggymastercon.clayapothecarymortarDef = true;
				}
			}
			else
			{
				clayapothecarymortarDef = false;
				Shiggymastercon.clayapothecarymortarDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "TEMPLAR_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "TEMPLAR_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "TEMPLAR_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "TEMPLAR_NAME")
			{
				if (!claytemplarminigunDef)
				{
					claytemplarminigunDef = true;
					Shiggymastercon.claytemplarminigunDef = true;
				}
			}
			else
			{
				claytemplarminigunDef = false;
				Shiggymastercon.claytemplarminigunDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "GREATERWISP_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "GREATERWISP_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "GREATERWISP_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "GREATERWISP_NAME")
			{
				if (!greaterwispballDef)
				{
					greaterwispballDef = true;
					Shiggymastercon.greaterwispballDef = true;
				}
			}
			else
			{
				greaterwispballDef = false;
				Shiggymastercon.greaterwispballDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "IMP_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "IMP_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "IMP_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "IMP_NAME")
			{
				if (!impblinkDef)
				{
					impblinkDef = true;
				}
			}
			else
			{
				impblinkDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "JELLYFISH_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "JELLYFISH_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "JELLYFISH_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "JELLYFISH_NAME")
			{
				if (!jellyfishnovaDef)
				{
					jellyfishnovaDef = true;
				}
			}
			else
			{
				jellyfishnovaDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "LEMURIAN_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "LEMURIAN_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "LEMURIAN_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "LEMURIAN_NAME")
			{
				if (!lemurianfireballDef)
				{
					lemurianfireballDef = true;
				}
			}
			else
			{
				lemurianfireballDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARGOLEM_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARGOLEM_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARGOLEM_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "LUNARGOLEM_NAME")
			{
				if (!lunargolemshotsDef)
				{
					lunargolemshotsDef = true;
				}
			}
			else
			{
				lunargolemshotsDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARWISP_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARWISP_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARWISP_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "LUNARWISP_NAME")
			{
				if (!lunarwispminigunDef)
				{
					lunarwispminigunDef = true;
				}
			}
			else
			{
				lunarwispminigunDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "PARENT_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "PARENT_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "PARENT_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "PARENT_NAME")
			{
				if (!parentteleportDef)
				{
					parentteleportDef = true;
				}
			}
			else
			{
				parentteleportDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "STONEGOLEM_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "STONEGOLEM_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "STONEGOLEM_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "STONEGOLEM_NAME")
			{
				if (!stonegolemlaserDef)
				{
					stonegolemlaserDef = true;
				}
			}
			else
			{
				stonegolemlaserDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDJAILER_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDJAILER_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDJAILER_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "VOIDJAILER_NAME")
			{
				if (!voidjailerpassiveDef)
				{
					voidjailerpassiveDef = true;
				}
			}
			else
			{
				voidjailerpassiveDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEQUEEN_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLEQUEEN_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLEQUEEN_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "BEETLEQUEEN_NAME")
			{
				if (!beetlequeenshotgunDef)
				{
					beetlequeenshotgunDef = true;
				}
			}
			else
			{
				beetlequeenshotgunDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "GROVETENDER_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "GROVETENDER_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "GROVETENDER_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "GROVETENDER_NAME")
			{
				if (!grovetenderhookDef)
				{
					grovetenderhookDef = true;
				}
			}
			else
			{
				grovetenderhookDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME")
			{
				if (!claydunestriderballDef)
				{
					claydunestriderballDef = true;
				}
			}
			else
			{
				claydunestriderballDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME")
			{
				if (!soluscontrolunityknockupDef)
				{
					soluscontrolunityknockupDef = true;
				}
			}
			else
			{
				soluscontrolunityknockupDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "XICONSTRUCT_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "XICONSTRUCT_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "XICONSTRUCT_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "XICONSTRUCT_NAME")
			{
				if (!xiconstructbeamDef)
				{
					xiconstructbeamDef = true;
				}
			}
			else
			{
				xiconstructbeamDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "VOIDDEVASTATOR_NAME")
			{
				if (!voiddevastatorhomingDef)
				{
					voiddevastatorhomingDef = true;
				}
			}
			else
			{
				voiddevastatorhomingDef = false;
			}
			if (characterBody.skillLocator.primary.skillNameToken == prefix + "SCAVENGER_NAME"
				| characterBody.skillLocator.secondary.skillNameToken == prefix + "SCAVENGER_NAME"
				| characterBody.skillLocator.utility.skillNameToken == prefix + "SCAVENGER_NAME"
				| characterBody.skillLocator.special.skillNameToken == prefix + "SCAVENGER_NAME")
			{
				if (!scavengerthqwibDef)
				{
					scavengerthqwibDef = true;
				}
			}
			else
			{
				scavengerthqwibDef = false;
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


