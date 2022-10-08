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
using R2API.Networking;

namespace ShiggyMod.Modules.Survivors
{
	public class ShiggyController : MonoBehaviour
	{
		string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";

		public float strengthMultiplier;
		public float rangedMultiplier;

        public float AFOTimer;
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
        private float roboballTimer;

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
        public HurtBox Target;

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

		private ExtraInputBankTest extrainputBankTest;
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
        public bool impbosspassiveDef;
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
        public bool grandparentsunDef;
        public bool grovetenderhookDef;
		public bool claydunestriderballDef;
		public bool soluscontrolunityknockupDef;
		public bool xiconstructbeamDef;
		public bool voiddevastatorhomingDef;
		public bool scavengerthqwibDef;

        public bool hasExtra1;
        public bool hasExtra2;
        public bool hasExtra3;
        public bool hasExtra4;
        public bool hasQuirk;
        public float quirkTimer;

        public float shiggyDamage;
        public int projectileCount;
        public int decayCount;
        public int captainitemcount;
        private DamageType damageType;
        private DamageType damageType2;
        private float effecttimer1;
        private float effecttimer2;
        private float effecttimer3;

        public void Awake()
		{
			child = GetComponentInChildren<ChildLocator>();
			
			indicator = new Indicator(gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/RecyclerIndicator"));
			passiveindicator = new Indicator(gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/EngiMissileTrackingIndicator"));
			activeindicator = new Indicator(gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/HuntressTrackingIndicator"));
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


            impbosspassiveDef = false;
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
            grandparentsunDef = false;
            claydunestriderballDef = false;
			soluscontrolunityknockupDef = false;
			xiconstructbeamDef = false;
			voiddevastatorhomingDef = false;
			scavengerthqwibDef = false;

            hasQuirk = false;
            hasExtra1 = false;
            hasExtra2 = false;
            hasExtra3 = false;
            hasExtra4 = false;

        }


        public void Start()
		{

			characterMaster = characterBody.master;
			if (!characterMaster.gameObject.GetComponent<ShiggyMasterController>())
			{
				Shiggymastercon = characterMaster.gameObject.AddComponent<ShiggyMasterController>();
			}

			extraskillLocator = gameObject.GetComponent<ExtraSkillLocator>();
            extrainputBankTest = gameObject.GetComponent<ExtraInputBankTest>();

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

            impbosspassiveDef = false;
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
            grandparentsunDef = false;
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

        public void FixedUpdate()
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
                        this.indicator.active = true;
                        this.indicator.targetTransform = this.trackingTarget.transform;
                    }

                }
                else
                {
                    this.indicator.active = false;
                    this.activeindicator.active = false;
                    this.passiveindicator.active = false;

                }

            }
            //shiggy current damage
            shiggyDamage = characterBody.damage;

            //Buff effects
            //multbuff buff
            if (characterBody.HasBuff(Modules.Buffs.multBuff))
            {
                if (effecttimer1 > 1f)
                {
                    effecttimer1 = 0f;
                    EffectManager.SpawnEffect(Modules.Assets.multEffect, new EffectData
                    {
                        origin = child.FindChild("LHand").position,
                        scale = 1f,
                        rotation = Quaternion.LookRotation(characterBody.transform.position)

                    }, false);

                    EffectManager.SpawnEffect(Modules.Assets.multEffect, new EffectData
                    {
                        origin = child.FindChild("RHand").position,
                        scale = 1f,
                        rotation = Quaternion.LookRotation(characterBody.transform.position)

                    }, false);
                }
                else
                {
                    effecttimer1 += Time.fixedDeltaTime;

                }
            }
            //claydunestrider buff
            if (characterBody.HasBuff(Modules.Buffs.claydunestriderBuff))
            {
                if (effecttimer2 > 1f)
                {
                    effecttimer2 = 0f;
                    EffectManager.SpawnEffect(Modules.Assets.claydunestriderEffect, new EffectData
                    {
                        origin = characterBody.corePosition,
                        scale = 1f,
                        rotation = Quaternion.LookRotation(-characterBody.characterDirection.forward)

                    }, false);
                }
                else
                {
                    effecttimer2 += Time.fixedDeltaTime;

                }
            }
            //Greaterwisp buff
            if (characterBody.HasBuff(Modules.Buffs.greaterwispBuff))
            {
                if (effecttimer3 > 1f)
                {
                    effecttimer3 = 0f;
                    EffectManager.SpawnEffect(Modules.Assets.chargegreaterwispBall, new EffectData
                    {
                        origin = child.FindChild("LHand").position,
                        scale = 1f,
                        rotation = Quaternion.LookRotation(characterBody.transform.position)

                    }, false);

                    EffectManager.SpawnEffect(Modules.Assets.chargegreaterwispBall, new EffectData
                    {
                        origin = child.FindChild("RHand").position,
                        scale = 1f,
                        rotation = Quaternion.LookRotation(characterBody.transform.position)

                    }, false);
                }
                else
                {
                    effecttimer3 += Time.fixedDeltaTime;

                }
            }

            //captain buff items
            captainitemcount = characterBody.master.inventory.GetItemCount(RoR2Content.Items.CaptainDefenseMatrix);
            if (characterBody.HasBuff(Buffs.captainBuff))
            {
                if(captainitemcount < 1)
                {
                    characterBody.master.inventory.GiveItem(RoR2Content.Items.CaptainDefenseMatrix, 1);
                }
                
            }
            else if(!characterBody.HasBuff(Buffs.captainBuff) && captainitemcount > 0)
            {
                characterBody.master.inventory.RemoveItem(RoR2Content.Items.CaptainDefenseMatrix, 1);
            }

            //damagetypes for moves
            damageType = DamageType.Generic;
            damageType2 = DamageType.SlowOnHit;

            //check multiplier buff
            if (characterBody.HasBuff(Modules.Buffs.multiplierBuff))
            {
                decayCount = (int)Modules.StaticValues.multiplierCoefficient;
                projectileCount = 1 *(int)Modules.StaticValues.multiplierCoefficient;
            }
            else
            {
                decayCount = 1;
                projectileCount = 1;
            }

            rangedMultiplier = 1f;
            //ranged boost buff
            if (characterBody.HasBuff(Modules.Buffs.lesserwispBuff.buffIndex))
            {
                rangedMultiplier *= StaticValues.lesserwisprangedMultiplier;
            }
            if (!characterBody.HasBuff(Modules.Buffs.lesserwispBuff.buffIndex))
            {
                rangedMultiplier = 1f;
            }
            //strength boost buff
            if (characterBody.HasBuff(Modules.Buffs.loaderBuff.buffIndex) | characterBody.HasBuff(Modules.Buffs.beetleBuff.buffIndex))
            {

                if (characterBody.HasBuff(Modules.Buffs.loaderBuff.buffIndex) && characterBody.HasBuff(Modules.Buffs.beetleBuff.buffIndex))
                {
                    strengthMultiplier = StaticValues.beetlestrengthMultiplier * StaticValues.loaderDamageMultiplier;
                }
                else
                {
                    strengthMultiplier = StaticValues.beetlestrengthMultiplier;
                }
            }
            else
            {
                strengthMultiplier = 1f;
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
            if (characterBody.hasEffectiveAuthority)
            {
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

            }
            //roboballmini buff
            if (characterBody.HasBuff(Modules.Buffs.roboballminiBuff.buffIndex))
            {
                if(characterBody.inputBank.skill1.down 
                    | characterBody.inputBank.skill2.down 
                    | characterBody.inputBank.skill3.down 
                    | characterBody.inputBank.skill1.down)
                {
                    if(roboballTimer > 1f)
                    {
                        roboballTimer = 0f;
                        characterBody.AddBuff(Modules.Buffs.roboballminiattackspeedBuff.buffIndex);
                    }
                    else
                    {
                        roboballTimer += Time.fixedDeltaTime;

                    }
                }
                else if (!characterBody.inputBank.skill1.down 
                    && !characterBody.inputBank.skill2.down 
                    && !characterBody.inputBank.skill3.down 
                    && !characterBody.inputBank.skill1.down)
                {
                    characterBody.ApplyBuff(Modules.Buffs.roboballminiattackspeedBuff.buffIndex, 0);
                }
                //if (characterBody.inputBank.jump.down)
                //{
                //    characterBody.AddBuff(Buffs.flyBuff.buffIndex);
                //    base.transform.position = characterBody.transform.position;
                //    if (characterBody.hasEffectiveAuthority && characterBody.characterMotor)
                //    {
                //        if (characterBody.inputBank.moveVector != Vector3.zero)
                //        {
                //            characterBody.characterMotor.velocity = characterBody.inputBank.moveVector * (characterBody.moveSpeed * Modules.StaticValues.roboballboostMultiplier);
                //            characterBody.characterMotor.disableAirControlUntilCollision = false;
                //        }
                //    }
                //}
                //else if (!characterBody.inputBank.jump.down)
                //{
                //    characterBody.RemoveBuff(Buffs.flyBuff.buffIndex);
                //}

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
					characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortararmorBuff.buffIndex, 0);

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
					characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarattackspeedBuff.buffIndex, 0);
				}
			}

			else if (!characterBody.GetNotMoving())
			{
				if (this.mortarIndicatorInstance) EntityState.Destroy(this.mortarIndicatorInstance.gameObject);
				characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortararmorBuff.buffIndex, 0);
				if (this.voidmortarIndicatorInstance) EntityState.Destroy(this.voidmortarIndicatorInstance.gameObject);
				characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarattackspeedBuff.buffIndex, 0);

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
                    if (verminjumpbuffGiven)
                    {
                        verminjumpbuffGiven = false;
                        characterBody.characterMotor.jumpCount -= Modules.StaticValues.verminjumpStacks;
                        characterBody.maxJumpCount -= Modules.StaticValues.verminjumpStacks;
                        characterBody.baseJumpCount -= Modules.StaticValues.verminjumpStacks;
                        characterBody.jumpPower -= Modules.StaticValues.verminjumpPower;
                        characterBody.baseJumpPower -= Modules.StaticValues.verminjumpPower;
                    }

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
					blastAttack.baseDamage = characterBody.damage * Modules.StaticValues.larvaDamageCoefficient * (characterBody.jumpPower / 5) * strengthMultiplier;
					blastAttack.falloffModel = BlastAttack.FalloffModel.None;
					blastAttack.baseForce = Modules.StaticValues.larvaForce;
					blastAttack.teamIndex = characterBody.teamComponent.teamIndex;
					blastAttack.damageType = damageType;
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
					blastAttack.damageType = damageType;
					blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
					blastAttack.Fire();
					ApplyDoT();
				}
			}
			else
            {
				if (!characterBody.HasBuff(Buffs.larvajumpBuff))
                {
                    if (larvabuffGiven)
                    {
                        larvabuffGiven = false;
                        characterBody.characterMotor.jumpCount -= Modules.StaticValues.larvajumpStacks;
                        characterBody.maxJumpCount -= Modules.StaticValues.larvajumpStacks;
                        characterBody.baseJumpCount -= Modules.StaticValues.larvajumpStacks;
                        characterBody.jumpPower -= Modules.StaticValues.larvajumpPower;
                        characterBody.baseJumpPower -= Modules.StaticValues.larvajumpPower;
                        characterBody.maxJumpHeight = Trajectory.CalculateApex(characterBody.jumpPower);
                    }
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

		//public void CheckQuirks()
  //      {
  //          extraskillLocator = gameObject.GetComponent<ExtraSkillLocator>();
  //          //check passive
  //          if (extraskillLocator.extraFirst.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
		//		|| extraskillLocator.extraSecond.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
		//		|| extraskillLocator.extraThird.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
		//		|| extraskillLocator.extraFourth.skillNameToken == prefix + "ALPHACONSTRUCT_NAME")
		//	{               
		//		characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
		//	}
  //          else if(extraskillLocator.extraFirst.skillNameToken != prefix + "ALPHACONSTRUCT_NAME"
  //              && extraskillLocator.extraSecond.skillNameToken != prefix + "ALPHACONSTRUCT_NAME"
  //              && extraskillLocator.extraThird.skillNameToken != prefix + "ALPHACONSTRUCT_NAME"
  //              && extraskillLocator.extraFourth.skillNameToken != prefix + "ALPHACONSTRUCT_NAME")
  //          {
		//		characterBody.RemoveBuff(Modules.Buffs.alphashieldonBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "BEETLE_NAME"
		//		|| extraskillLocator.extraSecond.skillNameToken == prefix + "BEETLE_NAME"
		//		|| extraskillLocator.extraThird.skillNameToken == prefix + "BEETLE_NAME"
		//		|| extraskillLocator.extraFourth.skillNameToken == prefix + "BEETLE_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.beetleBuff);
		//	}
  //          else if(extraskillLocator.extraFirst.skillNameToken != prefix + "BEETLE_NAME"
  //              && extraskillLocator.extraSecond.skillNameToken != prefix + "BEETLE_NAME"
  //              && extraskillLocator.extraThird.skillNameToken != prefix + "BEETLE_NAME"
  //              && extraskillLocator.extraFourth.skillNameToken != prefix + "BEETLE_NAME")

  //          {
		//		characterBody.RemoveBuff(Modules.Buffs.beetleBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "GUP_NAME"
		//		|| extraskillLocator.extraSecond.skillNameToken == prefix + "GUP_NAME"
		//		|| extraskillLocator.extraThird.skillNameToken == prefix + "GUP_NAME"
		//		|| extraskillLocator.extraFourth.skillNameToken == prefix + "GUP_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.gupspikeBuff);
		//	}
		//	else if (extraskillLocator.extraFirst.skillNameToken != prefix + "GUP_NAME"
  //              && extraskillLocator.extraSecond.skillNameToken != prefix + "GUP_NAME"
  //              && extraskillLocator.extraThird.skillNameToken != prefix + "GUP_NAME"
  //              && extraskillLocator.extraFourth.skillNameToken != prefix + "GUP_NAME")
  //          {
		//		characterBody.RemoveBuff(Modules.Buffs.gupspikeBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "LARVA_NAME"
		//		|| extraskillLocator.extraSecond.skillNameToken == prefix + "LARVA_NAME"
		//		|| extraskillLocator.extraThird.skillNameToken == prefix + "LARVA_NAME"
		//		|| extraskillLocator.extraFourth.skillNameToken == prefix + "LARVA_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.larvajumpBuff);
		//	}
		//	else if (extraskillLocator.extraFirst.skillNameToken != prefix + "LARVA_NAME"
  //              && extraskillLocator.extraSecond.skillNameToken != prefix + "LARVA_NAME"
  //              && extraskillLocator.extraThird.skillNameToken != prefix + "LARVA_NAME"
  //              && extraskillLocator.extraFourth.skillNameToken != prefix + "LARVA_NAME")
  //          {
		//		characterBody.RemoveBuff(Modules.Buffs.larvajumpBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "LESSERWISP_NAME"
		//		|| extraskillLocator.extraSecond.skillNameToken == prefix + "LESSERWISP_NAME"
		//		|| extraskillLocator.extraThird.skillNameToken == prefix + "LESSERWISP_NAME"
		//		|| extraskillLocator.extraFourth.skillNameToken == prefix + "LESSERWISP_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.lesserwispBuff);
		//	}
		//	else if (extraskillLocator.extraFirst.skillNameToken != prefix + "LESSERWISP_NAME"
  //              && extraskillLocator.extraSecond.skillNameToken != prefix + "LESSERWISP_NAME"
  //              && extraskillLocator.extraThird.skillNameToken != prefix + "LESSERWISP_NAME"
  //              && extraskillLocator.extraFourth.skillNameToken != prefix + "LESSERWISP_NAME")
  //          {
		//		characterBody.RemoveBuff(Modules.Buffs.lesserwispBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "LUNAREXPLODER_NAME"
		//		|| extraskillLocator.extraSecond.skillNameToken == prefix + "LUNAREXPLODER_NAME"
		//		|| extraskillLocator.extraThird.skillNameToken == prefix + "LUNAREXPLODER_NAME"
		//		|| extraskillLocator.extraFourth.skillNameToken == prefix + "LUNAREXPLODER_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.lunarexploderBuff);
		//	}
		//	else if (extraskillLocator.extraFirst.skillNameToken != prefix + "LUNAREXPLODER_NAME"
  //              && extraskillLocator.extraSecond.skillNameToken != prefix + "LUNAREXPLODER_NAME"
  //              && extraskillLocator.extraThird.skillNameToken != prefix + "LUNAREXPLODER_NAME"
  //              && extraskillLocator.extraFourth.skillNameToken != prefix + "LUNAREXPLODER_NAME")
  //          {
		//		characterBody.RemoveBuff(Modules.Buffs.lunarexploderBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "HERMITCRAB_NAME"
		//		|| extraskillLocator.extraSecond.skillNameToken == prefix + "HERMITCRAB_NAME"
		//		|| extraskillLocator.extraThird.skillNameToken == prefix + "HERMITCRAB_NAME"
		//		|| extraskillLocator.extraFourth.skillNameToken == prefix + "HERMITCRAB_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.hermitcrabmortarBuff);
		//	}
		//	else if (extraskillLocator.extraFirst.skillNameToken != prefix + "HERMITCRAB_NAME"
  //              && extraskillLocator.extraSecond.skillNameToken != prefix + "HERMITCRAB_NAME"
  //              && extraskillLocator.extraThird.skillNameToken != prefix + "HERMITCRAB_NAME"
  //              && extraskillLocator.extraFourth.skillNameToken != prefix + "HERMITCRAB_NAME")
  //          {
		//		characterBody.RemoveBuff(Modules.Buffs.hermitcrabmortarBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "PEST_NAME"
		//		|| extraskillLocator.extraSecond.skillNameToken == prefix + "PEST_NAME"
		//		|| extraskillLocator.extraThird.skillNameToken == prefix + "PEST_NAME"
		//		|| extraskillLocator.extraFourth.skillNameToken == prefix + "PEST_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.pestjumpBuff);
		//	}
		//	else if (extraskillLocator.extraFirst.skillNameToken != prefix + "PEST_NAME"
  //              && extraskillLocator.extraSecond.skillNameToken != prefix + "PEST_NAME"
  //              && extraskillLocator.extraThird.skillNameToken != prefix + "PEST_NAME"
  //              && extraskillLocator.extraFourth.skillNameToken != prefix + "PEST_NAME")
  //          {
		//		characterBody.RemoveBuff(Modules.Buffs.pestjumpBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "VERMIN_NAME"
		//		|| extraskillLocator.extraSecond.skillNameToken == prefix + "VERMIN_NAME"
		//		|| extraskillLocator.extraThird.skillNameToken == prefix + "VERMIN_NAME"
		//		|| extraskillLocator.extraFourth.skillNameToken == prefix + "VERMIN_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.verminsprintBuff);
		//	}
		//	else if (extraskillLocator.extraFirst.skillNameToken != prefix + "VERMIN_NAME"
  //              && extraskillLocator.extraSecond.skillNameToken != prefix + "VERMIN_NAME"
  //              && extraskillLocator.extraThird.skillNameToken != prefix + "VERMIN_NAME"
  //              && extraskillLocator.extraFourth.skillNameToken != prefix + "VERMIN_NAME")
  //          {
		//		characterBody.RemoveBuff(Modules.Buffs.verminsprintBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "MINIMUSHRUM_NAME"
		//		|| extraskillLocator.extraSecond.skillNameToken == prefix + "MINIMUSHRUM_NAME"
		//		|| extraskillLocator.extraThird.skillNameToken == prefix + "MINIMUSHRUM_NAME"
		//		|| extraskillLocator.extraFourth.skillNameToken == prefix + "MINIMUSHRUM_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.minimushrumBuff);
		//	}
		//	else if (extraskillLocator.extraFirst.skillNameToken != prefix + "MINIMUSHRUM_NAME"
  //              && extraskillLocator.extraSecond.skillNameToken != prefix + "MINIMUSHRUM_NAME"
  //              && extraskillLocator.extraThird.skillNameToken != prefix + "MINIMUSHRUM_NAME"
  //              && extraskillLocator.extraFourth.skillNameToken != prefix + "MINIMUSHRUM_NAME")
  //          {
		//		characterBody.RemoveBuff(Modules.Buffs.minimushrumBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "ROBOBALLMINI_NAME"
		//		|| extraskillLocator.extraSecond.skillNameToken == prefix + "ROBOBALLMINI_NAME"
		//		|| extraskillLocator.extraThird.skillNameToken == prefix + "ROBOBALLMINI_NAME"
		//		|| extraskillLocator.extraFourth.skillNameToken == prefix + "ROBOBALLMINI_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.roboballminiBuff);
		//	}
		//	else if (extraskillLocator.extraFirst.skillNameToken == prefix + "ROBOBALLMINI_NAME"
  //              && extraskillLocator.extraSecond.skillNameToken == prefix + "ROBOBALLMINI_NAME"
  //              && extraskillLocator.extraThird.skillNameToken == prefix + "ROBOBALLMINI_NAME"
  //              && extraskillLocator.extraFourth.skillNameToken == prefix + "ROBOBALLMINI_NAME")
  //          {
		//		characterBody.RemoveBuff(Modules.Buffs.roboballminiBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDBARNACLE_NAME"
		//		|| extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDBARNACLE_NAME"
		//		|| extraskillLocator.extraThird.skillNameToken == prefix + "VOIDBARNACLE_NAME"
		//		|| extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDBARNACLE_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarBuff);
		//	}
		//	else if (extraskillLocator.extraFirst.skillNameToken != prefix + "VOIDBARNACLE_NAME"
  //              && extraskillLocator.extraSecond.skillNameToken != prefix + "VOIDBARNACLE_NAME"
  //              && extraskillLocator.extraThird.skillNameToken != prefix + "VOIDBARNACLE_NAME"
  //              && extraskillLocator.extraFourth.skillNameToken != prefix + "VOIDBARNACLE_NAME")
  //          {
		//		characterBody.RemoveBuff(Modules.Buffs.voidbarnaclemortarBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		|| extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		|| extraskillLocator.extraThird.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		|| extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDJAILER_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.voidjailerBuff);
		//	}
		//	else if (extraskillLocator.extraFirst.skillNameToken != prefix + "VOIDJAILER_NAME"
  //              && extraskillLocator.extraSecond.skillNameToken != prefix + "VOIDJAILER_NAME"
  //              && extraskillLocator.extraThird.skillNameToken != prefix + "VOIDJAILER_NAME"
  //              & extraskillLocator.extraFourth.skillNameToken != prefix + "VOIDJAILER_NAME")
  //          {
		//		characterBody.RemoveBuff(Modules.Buffs.voidjailerBuff);
  //          }
  //          if (extraskillLocator.extraFirst.skillNameToken == prefix + "IMPBOSS_NAME"
  //              || extraskillLocator.extraSecond.skillNameToken == prefix + "IMPBOSS_NAME"
  //              || extraskillLocator.extraThird.skillNameToken == prefix + "IMPBOSS_NAME"
  //              || extraskillLocator.extraFourth.skillNameToken == prefix + "IMPBOSS_NAME")
  //          {
  //              characterBody.AddBuff(Modules.Buffs.impbossBuff);
  //          }
  //          else if (extraskillLocator.extraFirst.skillNameToken != prefix + "IMPBOSS_NAME"
  //              && extraskillLocator.extraSecond.skillNameToken != prefix + "IMPBOSS_NAME"
  //              && extraskillLocator.extraThird.skillNameToken != prefix + "IMPBOSS_NAME"
  //              && extraskillLocator.extraFourth.skillNameToken != prefix + "IMPBOSS_NAME")
  //          {
  //              characterBody.RemoveBuff(Modules.Buffs.impbossBuff);
  //          }
  //          if (extraskillLocator.extraFirst.skillNameToken == prefix + "STONETITAN_NAME"
		//		|| extraskillLocator.extraSecond.skillNameToken == prefix + "STONETITAN_NAME"
		//		|| extraskillLocator.extraThird.skillNameToken == prefix + "STONETITAN_NAME"
		//		|| extraskillLocator.extraFourth.skillNameToken == prefix + "STONETITAN_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.stonetitanBuff);
		//	}
		//	else if (extraskillLocator.extraFirst.skillNameToken != prefix + "STONETITAN_NAME"
  //              && extraskillLocator.extraSecond.skillNameToken != prefix + "STONETITAN_NAME"
  //              && extraskillLocator.extraThird.skillNameToken != prefix + "STONETITAN_NAME"
  //              && extraskillLocator.extraFourth.skillNameToken != prefix + "STONETITAN_NAME")
  //          {
		//		characterBody.RemoveBuff(Modules.Buffs.stonetitanBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "MAGMAWORM_NAME"
		//		|| extraskillLocator.extraSecond.skillNameToken == prefix + "MAGMAWORM_NAME"
		//		|| extraskillLocator.extraThird.skillNameToken == prefix + "MAGMAWORM_NAME"
		//		|| extraskillLocator.extraFourth.skillNameToken == prefix + "MAGMAWORM_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.magmawormBuff);
		//	}
		//	else if (extraskillLocator.extraFirst.skillNameToken != prefix + "MAGMAWORM_NAME"
  //              && extraskillLocator.extraSecond.skillNameToken != prefix + "MAGMAWORM_NAME"
  //              && extraskillLocator.extraThird.skillNameToken != prefix + "MAGMAWORM_NAME"
  //              && extraskillLocator.extraFourth.skillNameToken != prefix + "MAGMAWORM_NAME")
  //          {
		//		characterBody.RemoveBuff(Modules.Buffs.magmawormBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
		//		|| extraskillLocator.extraSecond.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
		//		|| extraskillLocator.extraThird.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
		//		|| extraskillLocator.extraFourth.skillNameToken == prefix + "OVERLOADINGWORM_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.overloadingwormBuff);
		//	}
		//	else if (extraskillLocator.extraFirst.skillNameToken != prefix + "OVERLOADINGWORM_NAME"
  //              && extraskillLocator.extraSecond.skillNameToken != prefix + "OVERLOADINGWORM_NAME"
  //              && extraskillLocator.extraThird.skillNameToken != prefix + "OVERLOADINGWORM_NAME"
  //              && extraskillLocator.extraFourth.skillNameToken != prefix + "OVERLOADINGWORM_NAME")
  //          {
		//		characterBody.RemoveBuff(Modules.Buffs.overloadingwormBuff);
		//	}

		//	//check active
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "VULTURE_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "VULTURE_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "VULTURE_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "VULTURE_NAME")
		//	{
		//		if (!alloyvultureflyDef)
		//		{
		//			alloyvultureflyDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "VULTURE_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "VULTURE_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "VULTURE_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "VULTURE_NAME")
  //          {
		//		alloyvultureflyDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEGUARD_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLEGUARD_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLEGUARD_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "BEETLEGUARD_NAME")
		//	{
		//		if (!beetleguardslamDef)
		//		{
		//			beetleguardslamDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "BEETLEGUARD_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "BEETLEGUARD_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "BEETLEGUARD_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "BEETLEGUARD_NAME")
  //          {
		//		beetleguardslamDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "BRONZONG_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "BRONZONG_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "BRONZONG_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "BRONZONG_NAME")
		//	{
		//		if (!bronzongballDef)
		//		{
		//			bronzongballDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "BRONZONG_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "BRONZONG_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "BRONZONG_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "BRONZONG_NAME")
  //          {
		//		bronzongballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "APOTHECARY_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "APOTHECARY_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "APOTHECARY_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "APOTHECARY_NAME")
		//	{
		//		if (!clayapothecarymortarDef)
		//		{
		//			clayapothecarymortarDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "APOTHECARY_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "APOTHECARY_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "APOTHECARY_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "APOTHECARY_NAME")
  //          {
		//		clayapothecarymortarDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "TEMPLAR_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "TEMPLAR_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "TEMPLAR_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "TEMPLAR_NAME")
		//	{
		//		if (!claytemplarminigunDef)
		//		{
		//			claytemplarminigunDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "TEMPLAR_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "TEMPLAR_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "TEMPLAR_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "TEMPLAR_NAME")
  //          {
		//		claytemplarminigunDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "GREATERWISP_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "GREATERWISP_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "GREATERWISP_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "GREATERWISP_NAME")
		//	{
		//		if (!greaterwispballDef)
		//		{
		//			greaterwispballDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "GREATERWISP_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "GREATERWISP_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "GREATERWISP_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "GREATERWISP_NAME")
  //          {
		//		greaterwispballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "IMP_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "IMP_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "IMP_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "IMP_NAME")
		//	{
		//		if (!impblinkDef)
		//		{
		//			impblinkDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "IMP_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "IMP_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "IMP_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "IMP_NAME")
  //          {
		//		impblinkDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "JELLYFISH_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "JELLYFISH_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "JELLYFISH_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "JELLYFISH_NAME")
		//	{
		//		if (!jellyfishnovaDef)
		//		{
		//			jellyfishnovaDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "JELLYFISH_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "JELLYFISH_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "JELLYFISH_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "JELLYFISH_NAME")
  //          {
		//		jellyfishnovaDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "LEMURIAN_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "LEMURIAN_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "LEMURIAN_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "LEMURIAN_NAME")
		//	{
		//		if (!lemurianfireballDef)
		//		{
		//			lemurianfireballDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "LEMURIAN_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "LEMURIAN_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "LEMURIAN_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "LEMURIAN_NAME")
  //          {
		//		lemurianfireballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARGOLEM_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARGOLEM_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARGOLEM_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "LUNARGOLEM_NAME")
		//	{
		//		if (!lunargolemshotsDef)
		//		{
		//			lunargolemshotsDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "LUNARGOLEM_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "LUNARGOLEM_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "LUNARGOLEM_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "LUNARGOLEM_NAME")
  //          {
		//		lunargolemshotsDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARWISP_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARWISP_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARWISP_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "LUNARWISP_NAME")
		//	{
		//		if (!lunarwispminigunDef)
		//		{
		//			lunarwispminigunDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "LUNARWISP_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "LUNARWISP_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "LUNARWISP_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "LUNARWISP_NAME")
  //          {
		//		lunarwispminigunDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "PARENT_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "PARENT_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "PARENT_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "PARENT_NAME")
		//	{
		//		if (!parentteleportDef)
		//		{
		//			parentteleportDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "PARENT_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "PARENT_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "PARENT_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "PARENT_NAME")
  //          {
		//		parentteleportDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "STONEGOLEM_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "STONEGOLEM_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "STONEGOLEM_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "STONEGOLEM_NAME")
		//	{
		//		if (!stonegolemlaserDef)
		//		{
		//			stonegolemlaserDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "STONEGOLEM_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "STONEGOLEM_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "STONEGOLEM_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "STONEGOLEM_NAME")
  //          {
		//		stonegolemlaserDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "VOIDJAILER_NAME")
		//	{
		//		if (!voidjailerpassiveDef)
		//		{
		//			voidjailerpassiveDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "VOIDJAILER_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "VOIDJAILER_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "VOIDJAILER_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "VOIDJAILER_NAME")
  //          {
		//		voidjailerpassiveDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEQUEEN_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLEQUEEN_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLEQUEEN_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "BEETLEQUEEN_NAME")
		//	{
		//		if (!beetlequeenshotgunDef)
		//		{
		//			beetlequeenshotgunDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "BEETLEQUEEN_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "BEETLEQUEEN_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "BEETLEQUEEN_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "BEETLEQUEEN_NAME")
  //          {
		//		beetlequeenshotgunDef = false;
  //          }
  //          if (characterBody.skillLocator.primary.skillNameToken == prefix + "GRANDPARENT_NAME"
  //              || characterBody.skillLocator.secondary.skillNameToken == prefix + "GRANDPARENT_NAME"
  //              || characterBody.skillLocator.utility.skillNameToken == prefix + "GRANDPARENT_NAME"
  //              || characterBody.skillLocator.special.skillNameToken == prefix + "GRANDPARENT_NAME")
  //          {
  //              if (!grandparentsunDef)
  //              {
  //                  grandparentsunDef = true;
  //              }
  //          }
  //          else if (characterBody.skillLocator.primary.skillNameToken != prefix + "GRANDPARENT_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "GRANDPARENT_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "GRANDPARENT_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "GRANDPARENT_NAME")
  //          {
  //              grandparentsunDef = false;
  //          }
  //          if (characterBody.skillLocator.primary.skillNameToken == prefix + "GROVETENDER_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "GROVETENDER_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "GROVETENDER_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "GROVETENDER_NAME")
		//	{
		//		if (!grovetenderhookDef)
		//		{
		//			grovetenderhookDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "GROVETENDER_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "GROVETENDER_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "GROVETENDER_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "GROVETENDER_NAME")
  //          {
		//		grovetenderhookDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME")
		//	{
		//		if (!claydunestriderballDef)
		//		{
		//			claydunestriderballDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "CLAYDUNESTRIDER_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "CLAYDUNESTRIDER_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "CLAYDUNESTRIDER_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "CLAYDUNESTRIDER_NAME")
  //          {
		//		claydunestriderballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME")
		//	{
		//		if (!soluscontrolunityknockupDef)
		//		{
		//			soluscontrolunityknockupDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "SOLUSCONTROLUNIT_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "SOLUSCONTROLUNIT_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "SOLUSCONTROLUNIT_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "SOLUSCONTROLUNIT_NAME")
  //          {
		//		soluscontrolunityknockupDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "XICONSTRUCT_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "XICONSTRUCT_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "XICONSTRUCT_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "XICONSTRUCT_NAME")
		//	{
		//		if (!xiconstructbeamDef)
		//		{
		//			xiconstructbeamDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "XICONSTRUCT_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "XICONSTRUCT_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "XICONSTRUCT_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "XICONSTRUCT_NAME")
  //          {
		//		xiconstructbeamDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "VOIDDEVASTATOR_NAME")
		//	{
		//		if (!voiddevastatorhomingDef)
		//		{
		//			voiddevastatorhomingDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "VOIDDEVASTATOR_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "VOIDDEVASTATOR_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "VOIDDEVASTATOR_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "VOIDDEVASTATOR_NAME")
  //          {
		//		voiddevastatorhomingDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "SCAVENGER_NAME"
		//		|| characterBody.skillLocator.secondary.skillNameToken == prefix + "SCAVENGER_NAME"
		//		|| characterBody.skillLocator.utility.skillNameToken == prefix + "SCAVENGER_NAME"
		//		|| characterBody.skillLocator.special.skillNameToken == prefix + "SCAVENGER_NAME")
		//	{
		//		if (!scavengerthqwibDef)
		//		{
		//			scavengerthqwibDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "SCAVENGER_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "SCAVENGER_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "SCAVENGER_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "SCAVENGER_NAME")
  //          {
		//		scavengerthqwibDef = false;
		//	}

		//}

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
							damageType = damageType2,

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

                        for (int i = 0; i < decayCount; i++)
                        {
                            DotController.InflictDot(ref info);

                        }
                    }
				}
			}
		}

		public  void Update()
		{            
            //update mortar indicator
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

        ////drop equipment elite
        //public void dropEquipment(EquipmentDef def)
        //{
        //    PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(def.equipmentIndex), characterBody.transform.position + Vector3.up * 1.5f, Vector3.up * 20f + characterBody.transform.forward * 2f);

        //}

        ////steal quirk code
        //private void StealQuirk(HurtBox hurtBox)
        //{
        //    var name = BodyCatalog.GetBodyName(hurtBox.healthComponent.body.bodyIndex);
        //    GameObject newbodyPrefab = BodyCatalog.FindBodyPrefab(name);

        //    Shiggymastercon = characterBody.master.gameObject.GetComponent<ShiggyMasterController>();


        //    if (extrainputBankTest.extraSkill1.down && !hasExtra1 && NetworkServer.active)
        //    {
        //        AkSoundEngine.PostEvent(3192656820, characterBody.gameObject);
        //        hasExtra1 = true;
        //        if (hurtBox.healthComponent.body.isElite)
        //        {
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixBlue))
        //            {
        //                Chat.AddMessage("Stole Overloading <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Lightning.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixHaunted))
        //            {
        //                Chat.AddMessage("Stole Celestine <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Haunted.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixLunar))
        //            {
        //                Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Lunar.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixPoison))
        //            {
        //                Chat.AddMessage("Stole Malachite <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Poison.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixRed))
        //            {
        //                Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Fire.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixWhite))
        //            {
        //                Chat.AddMessage("Stole Glacial <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Ice.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteEarth))
        //            {
        //                Chat.AddMessage("Stole Mending <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(DLC1Content.Elites.Earth.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteVoid))
        //            {
        //                Chat.AddMessage("Stole Void <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(DLC1Content.Elites.Void.eliteEquipmentDef);
        //            }
        //        }

        //        if (newbodyPrefab.name == "VultureBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

        //            characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
        //        }
        //        if (newbodyPrefab.name == "BeetleBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.beetleBuff);
        //        }
        //        if (newbodyPrefab.name == "BeetleGuardBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BisonBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.bisonchargeDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "FlyingVerminBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.pestjumpBuff);
        //        }
        //        if (newbodyPrefab.name == "VerminBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.verminsprintBuff);
        //        }
        //        if (newbodyPrefab.name == "BellBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayGrenadierBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayBruiserBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 0);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.gupspikeBuff);
        //        }
        //        if (newbodyPrefab.name == "GreaterWispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spirit Ball Control Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "HermitCrabBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.hermitcrabmortarBuff);
        //        }
        //        if (newbodyPrefab.name == "ImpBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "JellyfishBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.jellyfishnovaDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "AcidLarvaBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 0);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.larvajumpBuff);
        //        }
        //        if (newbodyPrefab.name == "LemurianBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "WispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.lesserwispBuff);
        //        }
        //        if (newbodyPrefab.name == "LunarExploderBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.lunarexploderBuff);
        //        }
        //        if (newbodyPrefab.name == "LunarGolemBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LunarWispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MiniMushroomBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.minimushrumBuff);
        //        }
        //        if (newbodyPrefab.name == "ParentBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Glide Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 0);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.roboballminiBuff);
        //        }
        //        if (newbodyPrefab.name == "GolemBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidBarnacleBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarBuff);
        //        }
        //        if (newbodyPrefab.name == "VoidJailerBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 4);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.voidjailerBuff);
        //        }
        //        if (newbodyPrefab.name == "NullifierBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }


        //        if (newbodyPrefab.name == "BeetleQueen2Body")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ImpBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Bleed Quirk</style> Get!");

        //            characterBody.AddBuff(Modules.Buffs.impbossBuff);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            Shiggymastercon.writeToSkillList(Shiggy.impbosspassiveDef, 4);
        //        }
        //        if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.stonetitanBuff);
        //        }
        //        if (newbodyPrefab.name == "GrandParentBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Solar Flare Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.grandparentsunDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GravekeeperBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VagrantBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.vagrantBuff);
        //        }
        //        if (newbodyPrefab.name == "MagmaWormBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.magmawormBuff);
        //        }
        //        if (newbodyPrefab.name == "ElectricWormBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.overloadingwormBuff);
        //        }
        //        if (newbodyPrefab.name == "ClayBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Rolling Clay Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MegaConstructBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidMegaCrabBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ScavBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 0);
        //            RemovePrimary();
        //            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }

        //        if (hasQuirk = false)
        //        {
        //            //Shiggymastercon.transformed = false;
        //            Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
        //        }

        //    }

        //    if (extrainputBankTest.extraSkill2.down && !hasExtra2 && NetworkServer.active)
        //    {
        //        AkSoundEngine.PostEvent(3192656820, characterBody.gameObject);
        //        hasExtra2 = true;
        //        if (hurtBox.healthComponent.body.isElite)
        //        {
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixBlue))
        //            {
        //                Chat.AddMessage("Stole Overloading <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Lightning.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixHaunted))
        //            {
        //                Chat.AddMessage("Stole Celestine <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Haunted.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixLunar))
        //            {
        //                Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Lunar.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixPoison))
        //            {
        //                Chat.AddMessage("Stole Malachite <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Poison.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixRed))
        //            {
        //                Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Fire.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixWhite))
        //            {
        //                Chat.AddMessage("Stole Glacial <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Ice.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteEarth))
        //            {
        //                Chat.AddMessage("Stole Mending <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(DLC1Content.Elites.Earth.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteVoid))
        //            {
        //                Chat.AddMessage("Stole Void <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(DLC1Content.Elites.Void.eliteEquipmentDef);
        //            }
        //        }
        //        if (newbodyPrefab.name == "VultureBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
        //        }
        //        if (newbodyPrefab.name == "BeetleBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.beetleBuff);
        //        }
        //        if (newbodyPrefab.name == "BeetleGuardBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BisonBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.bisonchargeDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "FlyingVerminBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.pestjumpBuff);
        //        }
        //        if (newbodyPrefab.name == "VerminBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.verminsprintBuff);
        //        }
        //        if (newbodyPrefab.name == "BellBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayGrenadierBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayBruiserBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 1);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.gupspikeBuff);
        //        }
        //        if (newbodyPrefab.name == "GreaterWispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spirit Ball Control Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "HermitCrabBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.hermitcrabmortarBuff);
        //        }
        //        if (newbodyPrefab.name == "ImpBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "JellyfishBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.jellyfishnovaDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "AcidLarvaBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 1);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.larvajumpBuff);
        //        }
        //        if (newbodyPrefab.name == "LemurianBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "WispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.lesserwispBuff);
        //        }
        //        if (newbodyPrefab.name == "LunarExploderBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.lunarexploderBuff);
        //        }
        //        if (newbodyPrefab.name == "LunarGolemBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LunarWispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MiniMushroomBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.minimushrumBuff);
        //        }
        //        if (newbodyPrefab.name == "ParentBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Glide Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 1);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.roboballminiBuff);
        //        }
        //        if (newbodyPrefab.name == "GolemBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidBarnacleBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarBuff);
        //        }
        //        if (newbodyPrefab.name == "VoidJailerBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 5);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.voidjailerBuff);
        //        }
        //        if (newbodyPrefab.name == "NullifierBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }


        //        if (newbodyPrefab.name == "BeetleQueen2Body")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ImpBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Bleed Quirk</style> Get!");

        //            characterBody.AddBuff(Modules.Buffs.impbossBuff);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            Shiggymastercon.writeToSkillList(Shiggy.impbosspassiveDef, 5);
        //        }
        //        if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.stonetitanBuff);
        //        }
        //        if (newbodyPrefab.name == "GrandParentBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Solar Flare Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.grandparentsunDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GravekeeperBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VagrantBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.vagrantBuff);
        //        }
        //        if (newbodyPrefab.name == "MagmaWormBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.magmawormBuff);
        //        }
        //        if (newbodyPrefab.name == "ElectricWormBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.overloadingwormBuff);
        //        }
        //        if (newbodyPrefab.name == "ClayBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Rolling Clay Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MegaConstructBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidMegaCrabBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ScavBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 1);
        //            RemoveSecondary();
        //            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }

        //        if (hasQuirk = false)
        //        {
        //            //Shiggymastercon.transformed = false;
        //            Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
        //        }

        //    }
        //    if (extrainputBankTest.extraSkill3.down && !hasExtra3 && NetworkServer.active)
        //    {
        //        AkSoundEngine.PostEvent(3192656820, characterBody.gameObject);
        //        hasExtra3 = true;
        //        if (hurtBox.healthComponent.body.isElite)
        //        {
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixBlue))
        //            {
        //                Chat.AddMessage("Stole Overloading <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Lightning.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixHaunted))
        //            {
        //                Chat.AddMessage("Stole Celestine <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Haunted.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixLunar))
        //            {
        //                Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Lunar.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixPoison))
        //            {
        //                Chat.AddMessage("Stole Malachite <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Poison.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixRed))
        //            {
        //                Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Fire.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixWhite))
        //            {
        //                Chat.AddMessage("Stole Glacial <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Ice.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteEarth))
        //            {
        //                Chat.AddMessage("Stole Mending <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(DLC1Content.Elites.Earth.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteVoid))
        //            {
        //                Chat.AddMessage("Stole Void <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(DLC1Content.Elites.Void.eliteEquipmentDef);
        //            }
        //        }
        //        if (newbodyPrefab.name == "VultureBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
        //        }
        //        if (newbodyPrefab.name == "BeetleBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.beetleBuff);
        //        }
        //        if (newbodyPrefab.name == "BeetleGuardBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BisonBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.bisonchargeDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "FlyingVerminBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.pestjumpBuff);
        //        }
        //        if (newbodyPrefab.name == "VerminBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.verminsprintBuff);
        //        }
        //        if (newbodyPrefab.name == "BellBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayGrenadierBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayBruiserBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 2);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.gupspikeBuff);
        //        }
        //        if (newbodyPrefab.name == "GreaterWispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spirit Ball Control Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "HermitCrabBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.hermitcrabmortarBuff);
        //        }
        //        if (newbodyPrefab.name == "ImpBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "JellyfishBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.jellyfishnovaDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "AcidLarvaBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 2);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.larvajumpBuff);
        //        }
        //        if (newbodyPrefab.name == "LemurianBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "WispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.lesserwispBuff);
        //        }
        //        if (newbodyPrefab.name == "LunarExploderBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.lunarexploderBuff);
        //        }
        //        if (newbodyPrefab.name == "LunarGolemBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LunarWispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MiniMushroomBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.minimushrumBuff);
        //        }
        //        if (newbodyPrefab.name == "ParentBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Glide Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 2);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.roboballminiBuff);
        //        }
        //        if (newbodyPrefab.name == "GolemBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidBarnacleBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarBuff);
        //        }
        //        if (newbodyPrefab.name == "VoidJailerBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 6);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.voidjailerBuff);
        //        }
        //        if (newbodyPrefab.name == "NullifierBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }


        //        if (newbodyPrefab.name == "BeetleQueen2Body")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ImpBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Bleed Quirk</style> Get!");

        //            characterBody.AddBuff(Modules.Buffs.impbossBuff);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            Shiggymastercon.writeToSkillList(Shiggy.impbosspassiveDef, 6);
        //        }
        //        if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.stonetitanBuff);
        //        }
        //        if (newbodyPrefab.name == "GrandParentBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Solar Flare Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.grandparentsunDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GravekeeperBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VagrantBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.vagrantBuff);
        //        }
        //        if (newbodyPrefab.name == "MagmaWormBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.magmawormBuff);
        //        }
        //        if (newbodyPrefab.name == "ElectricWormBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.overloadingwormBuff);
        //        }
        //        if (newbodyPrefab.name == "ClayBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Rolling Clay Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MegaConstructBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidMegaCrabBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ScavBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 2);
        //            RemoveUtility();
        //            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }

        //        if (hasQuirk = false)
        //        {
        //            //Shiggymastercon.transformed = false;
        //            Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
        //        }
        //    }
        //    if (extrainputBankTest.extraSkill4.down && !hasExtra4 && NetworkServer.active)
        //    {
        //        AkSoundEngine.PostEvent(3192656820, characterBody.gameObject);
        //        hasExtra4 = true;
        //        if (hurtBox.healthComponent.body.isElite)
        //        {
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixBlue))
        //            {
        //                Chat.AddMessage("Stole Overloading <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Lightning.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixHaunted))
        //            {
        //                Chat.AddMessage("Stole Celestine <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Haunted.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixLunar))
        //            {
        //                Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Lunar.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixPoison))
        //            {
        //                Chat.AddMessage("Stole Malachite <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Poison.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixRed))
        //            {
        //                Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Fire.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixWhite))
        //            {
        //                Chat.AddMessage("Stole Glacial <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(RoR2Content.Elites.Ice.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteEarth))
        //            {
        //                Chat.AddMessage("Stole Mending <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(DLC1Content.Elites.Earth.eliteEquipmentDef);
        //            }
        //            if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteVoid))
        //            {
        //                Chat.AddMessage("Stole Void <style=cIsUtility>Quirk!</style>");
        //                dropEquipment(DLC1Content.Elites.Void.eliteEquipmentDef);
        //            }
        //        }
        //        if (newbodyPrefab.name == "VultureBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 7);
        //            RemoveExtra4();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
        //        }
        //        if (newbodyPrefab.name == "BeetleBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 7);
        //            RemoveExtra4();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.beetleBuff);
        //        }
        //        if (newbodyPrefab.name == "BeetleGuardBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BisonBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.bisonchargeDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "FlyingVerminBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 7);
        //            RemoveExtra4();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.pestjumpBuff);
        //        }
        //        if (newbodyPrefab.name == "VerminBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 7);
        //            RemoveExtra4();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.verminsprintBuff);
        //        }
        //        if (newbodyPrefab.name == "BellBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayGrenadierBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayBruiserBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 3);
        //            RemoveExtra4();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.gupspikeBuff);
        //        }
        //        if (newbodyPrefab.name == "GreaterWispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spirit Ball Control Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "HermitCrabBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 7);
        //            RemoveExtra4();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.hermitcrabmortarBuff);
        //        }
        //        if (newbodyPrefab.name == "ImpBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "JellyfishBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.jellyfishnovaDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "AcidLarvaBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 3);
        //            RemoveExtra4();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.larvajumpBuff);
        //        }
        //        if (newbodyPrefab.name == "LemurianBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "WispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 7);
        //            RemoveExtra4();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.lesserwispBuff);
        //        }
        //        if (newbodyPrefab.name == "LunarExploderBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 7);
        //            RemoveExtra4();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.lunarexploderBuff);
        //        }
        //        if (newbodyPrefab.name == "LunarGolemBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LunarWispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MiniMushroomBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 7);
        //            RemoveExtra4();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.minimushrumBuff);
        //        }
        //        if (newbodyPrefab.name == "ParentBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Glide Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 3);
        //            RemoveExtra4();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.roboballminiBuff);
        //        }
        //        if (newbodyPrefab.name == "GolemBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidBarnacleBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 7);
        //            RemoveExtra4();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarBuff);
        //        }
        //        if (newbodyPrefab.name == "VoidJailerBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 7);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.voidjailerBuff);
        //        }
        //        if (newbodyPrefab.name == "NullifierBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }


        //        if (newbodyPrefab.name == "BeetleQueen2Body")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ImpBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Bleed Quirk</style> Get!");

        //            characterBody.AddBuff(Modules.Buffs.impbossBuff);
        //            RemoveExtra4();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            Shiggymastercon.writeToSkillList(Shiggy.impbosspassiveDef, 7);
        //        }
        //        if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 7);
        //            RemoveExtra4();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.stonetitanBuff);
        //        }
        //        if (newbodyPrefab.name == "GrandParentBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Solar Flare Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.grandparentsunDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GravekeeperBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VagrantBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 7);
        //            RemoveExtra4();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.vagrantBuff);
        //        }
        //        if (newbodyPrefab.name == "MagmaWormBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 7);
        //            RemoveExtra4();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.magmawormBuff);
        //        }
        //        if (newbodyPrefab.name == "ElectricWormBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 7);
        //            RemoveExtra4();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //            characterBody.AddBuff(Modules.Buffs.overloadingwormBuff);
        //        }
        //        if (newbodyPrefab.name == "ClayBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Rolling Clay Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MegaConstructBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidMegaCrabBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ScavBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 3);
        //            RemoveSpecial();
        //            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }

        //        if (hasQuirk = false)
        //        {
        //            //Shiggymastercon.transformed = false;
        //            Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
        //        }
        //    }

        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "ALPHACONSTRUCT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 4);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "BEETLE_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 4);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "GUP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 4);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "LARVA_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 4);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "LESSERWISP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 4);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "LUNAREXPLODER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 4);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "HERMITCRAB_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 4);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "PEST_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 4);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "VERMIN_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 4);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "MINIMUSHRUM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 4);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "ROBOBALLMINI_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 4);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDBARNACLE_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 4);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDJAILER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 4);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "IMPBOSS_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.impbosspassiveDef, 4);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "STONETITAN_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 4);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "MAGMAWORM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 4);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "OVERLOADINGWORM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 4);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "VAGRANT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 4);
        //    }

        //    //check active 1
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "VULTURE_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEGUARD_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "BRONZONG_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "APOTHECARY_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "TEMPLAR_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "GREATERWISP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "IMP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "JELLYFISH_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.jellyfishnovaDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "LEMURIAN_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARGOLEM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARWISP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "PARENT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "STONEGOLEM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDREAVER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEQUEEN_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "GRANDPARENT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.grandparentsunDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "GROVETENDER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "XICONSTRUCT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 0);
        //    }
        //    if (characterBody.skillLocator.primary.skillNameToken == prefix + "SCAVENGER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 0);
        //    }

        //    //check passive 2
        //    if (extraskillLocator.extraSecond.skillNameToken == prefix + "ALPHACONSTRUCT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 5);
        //    }
        //    if (extraskillLocator.extraSecond.skillNameToken == prefix + "BEETLE_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 5);
        //    }
        //    if (extraskillLocator.extraSecond.skillNameToken == prefix + "GUP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 5);
        //    }
        //    if (extraskillLocator.extraSecond.skillNameToken == prefix + "LARVA_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 5);
        //    }
        //    if (extraskillLocator.extraSecond.skillNameToken == prefix + "LESSERWISP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 5);
        //    }
        //    if (extraskillLocator.extraSecond.skillNameToken == prefix + "LUNAREXPLODER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 5);
        //    }
        //    if (extraskillLocator.extraSecond.skillNameToken == prefix + "HERMITCRAB_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 5);
        //    }
        //    if (extraskillLocator.extraSecond.skillNameToken == prefix + "PEST_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 5);
        //    }
        //    if (extraskillLocator.extraSecond.skillNameToken == prefix + "VERMIN_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 5);
        //    }
        //    if (extraskillLocator.extraSecond.skillNameToken == prefix + "MINIMUSHRUM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 5);
        //    }
        //    if (extraskillLocator.extraSecond.skillNameToken == prefix + "ROBOBALLMINI_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 5);
        //    }
        //    if (extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDBARNACLE_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 5);
        //    }
        //    if (extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDJAILER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 5);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "IMPBOSS_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.impbosspassiveDef, 5);
        //    }
        //    if (extraskillLocator.extraSecond.skillNameToken == prefix + "STONETITAN_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 5);
        //    }
        //    if (extraskillLocator.extraSecond.skillNameToken == prefix + "MAGMAWORM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 5);
        //    }
        //    if (extraskillLocator.extraSecond.skillNameToken == prefix + "OVERLOADINGWORM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 5);
        //    }
        //    if (extraskillLocator.extraSecond.skillNameToken == prefix + "VAGRANT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 5);
        //    }

        //    //check active 2
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "VULTURE_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLEGUARD_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "BRONZONG_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "APOTHECARY_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "TEMPLAR_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "GREATERWISP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "IMP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "JELLYFISH_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.jellyfishnovaDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "LEMURIAN_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARGOLEM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARWISP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "PARENT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "STONEGOLEM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDREAVER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLEQUEEN_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "GRANDPARENT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.grandparentsunDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "GROVETENDER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "XICONSTRUCT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 1);
        //    }
        //    if (characterBody.skillLocator.secondary.skillNameToken == prefix + "SCAVENGER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 1);
        //    }

        //    //check passive 3
        //    if (extraskillLocator.extraThird.skillNameToken == prefix + "ALPHACONSTRUCT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 6);
        //    }
        //    if (extraskillLocator.extraThird.skillNameToken == prefix + "BEETLE_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 6);
        //    }
        //    if (extraskillLocator.extraThird.skillNameToken == prefix + "GUP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 6);
        //    }
        //    if (extraskillLocator.extraThird.skillNameToken == prefix + "LARVA_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 6);
        //    }
        //    if (extraskillLocator.extraThird.skillNameToken == prefix + "LESSERWISP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 6);
        //    }
        //    if (extraskillLocator.extraThird.skillNameToken == prefix + "LUNAREXPLODER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 6);
        //    }
        //    if (extraskillLocator.extraThird.skillNameToken == prefix + "HERMITCRAB_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 6);
        //    }
        //    if (extraskillLocator.extraThird.skillNameToken == prefix + "PEST_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 6);
        //    }
        //    if (extraskillLocator.extraThird.skillNameToken == prefix + "VERMIN_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 6);
        //    }
        //    if (extraskillLocator.extraThird.skillNameToken == prefix + "MINIMUSHRUM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 6);
        //    }
        //    if (extraskillLocator.extraThird.skillNameToken == prefix + "ROBOBALLMINI_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 6);
        //    }
        //    if (extraskillLocator.extraThird.skillNameToken == prefix + "VOIDBARNACLE_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 6);
        //    }
        //    if (extraskillLocator.extraThird.skillNameToken == prefix + "VOIDJAILER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 6);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "IMPBOSS_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.impbosspassiveDef, 6);
        //    }
        //    if (extraskillLocator.extraThird.skillNameToken == prefix + "STONETITAN_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 6);
        //    }
        //    if (extraskillLocator.extraThird.skillNameToken == prefix + "MAGMAWORM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 6);
        //    }
        //    if (extraskillLocator.extraThird.skillNameToken == prefix + "OVERLOADINGWORM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 6);
        //    }
        //    if (extraskillLocator.extraThird.skillNameToken == prefix + "VAGRANT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 6);
        //    }

        //    //check active 3
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "VULTURE_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLEGUARD_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "BRONZONG_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "APOTHECARY_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "TEMPLAR_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "GREATERWISP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "IMP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "JELLYFISH_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.jellyfishnovaDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "LEMURIAN_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARGOLEM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARWISP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "PARENT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "STONEGOLEM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDREAVER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLEQUEEN_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "GRANDPARENT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.grandparentsunDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "GROVETENDER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "XICONSTRUCT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDDEVASTATOR_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 2);
        //    }
        //    if (characterBody.skillLocator.utility.skillNameToken == prefix + "SCAVENGER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 2);
        //    }
        //    //check passive 4
        //    if (extraskillLocator.extraFourth.skillNameToken == prefix + "ALPHACONSTRUCT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 7);
        //    }
        //    if (extraskillLocator.extraFourth.skillNameToken == prefix + "BEETLE_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 7);
        //    }
        //    if (extraskillLocator.extraFourth.skillNameToken == prefix + "GUP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 7);
        //    }
        //    if (extraskillLocator.extraFourth.skillNameToken == prefix + "LARVA_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 7);
        //    }
        //    if (extraskillLocator.extraFourth.skillNameToken == prefix + "LESSERWISP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 7);
        //    }
        //    if (extraskillLocator.extraFourth.skillNameToken == prefix + "LUNAREXPLODER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 7);
        //    }
        //    if (extraskillLocator.extraFourth.skillNameToken == prefix + "HERMITCRAB_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 7);
        //    }
        //    if (extraskillLocator.extraFourth.skillNameToken == prefix + "PEST_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 7);
        //    }
        //    if (extraskillLocator.extraFourth.skillNameToken == prefix + "VERMIN_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 7);
        //    }
        //    if (extraskillLocator.extraFourth.skillNameToken == prefix + "MINIMUSHRUM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 7);
        //    }
        //    if (extraskillLocator.extraFourth.skillNameToken == prefix + "ROBOBALLMINI_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 7);
        //    }
        //    if (extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDBARNACLE_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 7);
        //    }
        //    if (extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDJAILER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 7);
        //    }
        //    if (extraskillLocator.extraFirst.skillNameToken == prefix + "IMPBOSS_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.impbosspassiveDef, 7);
        //    }
        //    if (extraskillLocator.extraFourth.skillNameToken == prefix + "STONETITAN_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 7);
        //    }
        //    if (extraskillLocator.extraFourth.skillNameToken == prefix + "MAGMAWORM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 7);
        //    }
        //    if (extraskillLocator.extraFourth.skillNameToken == prefix + "OVERLOADINGWORM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 7);
        //    }
        //    if (extraskillLocator.extraFourth.skillNameToken == prefix + "VAGRANT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 7);
        //    }

        //    //check active 4
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "VULTURE_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "BEETLEGUARD_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "BRONZONG_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "APOTHECARY_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "TEMPLAR_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "GREATERWISP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "IMP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "JELLYFISH_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.jellyfishnovaDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "LEMURIAN_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "LUNARGOLEM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "LUNARWISP_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "PARENT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "STONEGOLEM_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "VOIDREAVER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "BEETLEQUEEN_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "GRANDPARENT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.grandparentsunDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "GROVETENDER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "XICONSTRUCT_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "VOIDDEVASTATOR_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 3);
        //    }
        //    if (characterBody.skillLocator.special.skillNameToken == prefix + "SCAVENGER_NAME")
        //    {
        //        Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 3);
        //    }


        //}

        public void RemoveExtra1()
        {
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

        }

        public void RemoveExtra2()
        {
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

        }

        public void RemoveExtra3()
        {
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

        }

        public void RemoveExtra4()
        {
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

        }

        public void RemovePrimary()
        {
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);

        }
        public void RemoveSecondary()
        {
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);

        }

        public void RemoveUtility()
        {
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);

        }

        public void RemoveSpecial()
        {
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);

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

	//Steal Quirk


}


