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
using ShiggyMod.Modules.Networking;
using R2API.Networking.Interfaces;
using UnityEngine.UIElements;

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
		public bool claydunestriderbuffDef;
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

        //AFO
        private bool informAFOToPlayers;
        private bool hasStolen;
        private float stopwatch;
        private bool hasRemoved;
        private float stopwatch2;

        public void Awake()
		{
			child = GetComponentInChildren<ChildLocator>();
			
			indicator = new Indicator(gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/RecyclerIndicator"));
			passiveindicator = new Indicator(gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/EngiMissileTrackingIndicator"));
			activeindicator = new Indicator(gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/HuntressTrackingIndicator"));
			//On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
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
            claydunestriderbuffDef = false;
			soluscontrolunityknockupDef = false;
			xiconstructbeamDef = false;
			voiddevastatorhomingDef = false;
			scavengerthqwibDef = false;

            hasQuirk = false;
            hasExtra1 = false;
            hasExtra2 = false;
            hasExtra3 = false;
            hasExtra4 = false;

            informAFOToPlayers = false;

        }


        public void Start()
        {
            characterBody = gameObject.GetComponent<CharacterBody>();
            characterMaster = characterBody.master;
			if (!characterMaster.gameObject.GetComponent<ShiggyMasterController>())
			{
				Shiggymastercon = characterMaster.gameObject.AddComponent<ShiggyMasterController>();
			}

			extraskillLocator = characterBody.gameObject.GetComponent<ExtraSkillLocator>();
            extrainputBankTest = characterBody.gameObject.GetComponent<ExtraInputBankTest>();

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
            claydunestriderbuffDef = false;
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
			if (characterBody.hasEffectiveAuthority)
            {//shiggy current damage
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
                    if (captainitemcount < 1)
                    {
                        characterBody.master.inventory.GiveItem(RoR2Content.Items.CaptainDefenseMatrix, 1);
                    }

                }
                else if (!characterBody.HasBuff(Buffs.captainBuff) && captainitemcount > 0)
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
                    projectileCount = 1 * (int)Modules.StaticValues.multiplierCoefficient;
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

                        if (magmawormtimer > StaticValues.magmawormInterval / characterBody.attackSpeed)
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
                                    characterBody.ApplyBuff(Modules.Buffs.vagrantdisableBuff.buffIndex, buffCountToApply - 1);
                                    vagranttimer = 0f;
                                }
                            }
                            else
                            {
                                characterBody.ApplyBuff(Modules.Buffs.vagrantdisableBuff.buffIndex, 0);
                                characterBody.ApplyBuff(Modules.Buffs.vagrantBuff.buffIndex, 1);

                            }
                        }
                        else vagranttimer += Time.fixedDeltaTime;
                    }

                }
                //roboballmini buff
                if (characterBody.HasBuff(Modules.Buffs.roboballminiBuff.buffIndex))
                {
                    if (characterBody.inputBank.skill1.down
                        | characterBody.inputBank.skill2.down
                        | characterBody.inputBank.skill3.down
                        | characterBody.inputBank.skill1.down)
                    {
                        if (roboballTimer > 1f)
                        {
                            roboballTimer = 0f;
                            characterBody.ApplyBuff(Modules.Buffs.roboballminiattackspeedBuff.buffIndex);
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
                    //    characterBody.ApplyBuff(Buffs.flyBuff.buffIndex);
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
                            int buffCountToApply2 = characterBody.GetBuffCount(Modules.Buffs.alphashieldoffBuff.buffIndex);
                            if (buffCountToApply2 > 1)
                            {
                                if (buffCountToApply2 >= 2)
                                {
                                    characterBody.ApplyBuff(Modules.Buffs.alphashieldoffBuff.buffIndex, buffCountToApply2 - 1);
                                    alphaconstructshieldtimer = 0f;
                                }
                            }
                            else
                            {
                                characterBody.ApplyBuff(Modules.Buffs.alphashieldoffBuff.buffIndex, 0);
                                characterBody.ApplyBuff(Modules.Buffs.alphashieldonBuff.buffIndex, 1);

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
                        if (mortarTimer >= Modules.StaticValues.mortarbaseDuration / (characterBody.attackSpeed))
                        {
                            int hermitbuffcount = characterBody.GetBuffCount(Buffs.hermitcrabmortararmorBuff.buffIndex);
                            characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortararmorBuff.buffIndex, hermitbuffcount + 1);
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
                        if (voidmortarTimer >= Modules.StaticValues.voidmortarbaseDuration / (characterBody.armor / characterBody.baseArmor))
                        {
                            int voidbarnaclebuffcount = characterBody.GetBuffCount(Buffs.voidbarnaclemortarattackspeedBuff.buffIndex);
                            characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarattackspeedBuff.buffIndex, voidbarnaclebuffcount + 1);
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
                        if (voidjailerTimer > StaticValues.voidjailerInterval / (num / characterBody.baseMoveSpeed))
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
                    if (characterBody.characterMotor.isGrounded && larvaTimer > 1f)
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
                    if (characterBody.healthComponent.combinedHealth < characterBody.healthComponent.fullCombinedHealth / 2)
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
		//		characterBody.ApplyBuff(Modules.Buffs.alphashieldonBuff);
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
		//		characterBody.ApplyBuff(Modules.Buffs.beetleBuff);
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
		//		characterBody.ApplyBuff(Modules.Buffs.gupspikeBuff);
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
		//		characterBody.ApplyBuff(Modules.Buffs.larvajumpBuff);
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
		//		characterBody.ApplyBuff(Modules.Buffs.lesserwispBuff);
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
		//		characterBody.ApplyBuff(Modules.Buffs.lunarexploderBuff);
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
		//		characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortarBuff);
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
		//		characterBody.ApplyBuff(Modules.Buffs.pestjumpBuff);
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
		//		characterBody.ApplyBuff(Modules.Buffs.verminsprintBuff);
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
		//		characterBody.ApplyBuff(Modules.Buffs.minimushrumBuff);
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
		//		characterBody.ApplyBuff(Modules.Buffs.roboballminiBuff);
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
		//		characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarBuff);
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
		//		characterBody.ApplyBuff(Modules.Buffs.voidjailerBuff);
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
  //              characterBody.ApplyBuff(Modules.Buffs.impbossBuff);
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
		//		characterBody.ApplyBuff(Modules.Buffs.stonetitanBuff);
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
		//		characterBody.ApplyBuff(Modules.Buffs.magmawormBuff);
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
		//		characterBody.ApplyBuff(Modules.Buffs.overloadingwormBuff);
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
		//		if (!claydunestriderbuffDef)
		//		{
		//			claydunestriderbuffDef = true;
		//		}
		//	}
		//	else if (characterBody.skillLocator.primary.skillNameToken != prefix + "CLAYDUNESTRIDER_NAME"
  //              && characterBody.skillLocator.secondary.skillNameToken != prefix + "CLAYDUNESTRIDER_NAME"
  //              && characterBody.skillLocator.utility.skillNameToken != prefix + "CLAYDUNESTRIDER_NAME"
  //              && characterBody.skillLocator.special.skillNameToken != prefix + "CLAYDUNESTRIDER_NAME")
  //          {
		//		claydunestriderbuffDef = false;
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

		public void Update()
        {
            Target = GetTrackingTarget();
            //update mortar indicator
            if (this.mortarIndicatorInstance) this.UpdateIndicator();

            if (!informAFOToPlayers)
            {
                informAFOToPlayers = true;
                Chat.AddMessage($"Press the [{Config.AFOHotkey.Value}] key to use <style=cIsUtility>All For One and steal quirks</style>."
                + $" Press the [{Config.RemoveHotkey.Value}] key to <style=cIsUtility>remove quirks</style>.");
            }

            //instant
            if (Config.holdButtonAFO.Value)
			{
                //steal quirk
				if (Config.AFOHotkey.Value.IsDown() && characterBody.hasEffectiveAuthority)
                {
                    Debug.Log("hold button AFO");
                    stopwatch += Time.deltaTime;
					if (!this.hasStolen)
					{                        
						hasStolen = true;
						if (Target)
						{

							Debug.Log("Target");
							Debug.Log(BodyCatalog.FindBodyPrefab(BodyCatalog.GetBodyName(Target.healthComponent.body.bodyIndex)));
							AkSoundEngine.PostEvent(1719197672, this.gameObject);
							StealQuirk(Target);
						}
					}
					if (stopwatch >= 3f)
					{
						//choose what quirk to remove
						Chat.AddMessage("<style=cIsUtility>Choose which Quirk to Remove</style>");
						//unset skills but not to unwrite the skilllist
						RemovePrimary();
						RemoveSecondary();
						RemoveUtility();
						RemoveSpecial();
						RemoveExtra1();
						RemoveExtra2();
						RemoveExtra3();
						RemoveExtra4();

						//override skills to choosdef
						characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
						characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
						characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
						characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
						extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
						extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
						extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
						extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
					}


				}
				else if (Config.AFOHotkey.Value.IsUp())
				{
					hasStolen = false;
					stopwatch = 0f;
				}
                //remove quirk
                if (Config.RemoveHotkey.Value.IsDown() && characterBody.hasEffectiveAuthority)
                {
                    Debug.Log("hold button AFO");
                    stopwatch2 += Time.deltaTime;
                    if (!this.hasRemoved)
                    {
                        hasRemoved = true;

                        //choose what quirk to remove
                        Chat.AddMessage("<style=cIsUtility>Choose which Quirk to Remove</style>");
                        //unset skills but not to unwrite the skilllist
                        RemovePrimary();
                        RemoveSecondary();
                        RemoveUtility();
                        RemoveSpecial();
                        RemoveExtra1();
                        RemoveExtra2();
                        RemoveExtra3();
                        RemoveExtra4();

                        //override skills to choosdef
                        characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
                        characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
                        characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
                        characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
                        extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
                        extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
                        extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
                        extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
                    }


                }
                else if (Config.RemoveHotkey.Value.IsUp())
                {
                    hasRemoved = false;
                    stopwatch2 = 0f;
                }
            }
            // 1 second wait
			else if (!Config.holdButtonAFO.Value)
            {
                //steal quirk
				if (Config.AFOHotkey.Value.IsDown() && characterBody.hasEffectiveAuthority)
				{
					stopwatch += Time.deltaTime;

					if (stopwatch >= 1f && !this.hasStolen)
					{
						hasStolen = true;
						if (Target)
						{

							Debug.Log("Target");
							Debug.Log(BodyCatalog.FindBodyPrefab(BodyCatalog.GetBodyName(Target.healthComponent.body.bodyIndex)));
							AkSoundEngine.PostEvent(1719197672, this.gameObject);
							StealQuirk(Target);
						}
					}
					if (stopwatch >= 3f)
					{
						Chat.AddMessage("<style=cIsUtility>Choose which Quirk to Remove</style>");
						//unset skills but not to unwrite the skilllist
						RemovePrimary();
						RemoveSecondary();
						RemoveUtility();
						RemoveSpecial();
						RemoveExtra1();
						RemoveExtra2();
						RemoveExtra3();
						RemoveExtra4();

						//override skills to choosdef
						characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
						characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
						characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
						characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
						extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
						extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
						extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
						extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);

					}
                }
                else if (Config.AFOHotkey.Value.IsUp())
                {
                    hasStolen = false;
                    stopwatch = 0f;
                }
                //remove quirk
                if (Config.RemoveHotkey.Value.IsDown() && characterBody.hasEffectiveAuthority)
                {
                    Debug.Log("hold button AFO");
                    stopwatch2 += Time.deltaTime;
                    if (!this.hasRemoved && stopwatch2 >=1f)
                    {
                        hasRemoved = true;

                        //choose what quirk to remove
                        Chat.AddMessage("<style=cIsUtility>Choose which Quirk to Remove</style>");
                        //unset skills but not to unwrite the skilllist
                        RemovePrimary();
                        RemoveSecondary();
                        RemoveUtility();
                        RemoveSpecial();
                        RemoveExtra1();
                        RemoveExtra2();
                        RemoveExtra3();
                        RemoveExtra4();

                        //override skills to choosdef
                        characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
                        characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
                        characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
                        characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
                        extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
                        extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
                        extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
                        extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
                    }


                }
                else if (Config.RemoveHotkey.Value.IsUp())
                {
                    hasRemoved = false;
                    stopwatch2 = 0f;
                }
            }
            
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

        //drop equipment elite
        public void dropEquipment(EquipmentDef def)
        {
            if (characterBody.hasEffectiveAuthority)
            {
                new EquipmentDropNetworked(PickupCatalog.FindPickupIndex(def.equipmentIndex),
                    base.transform.position + Vector3.up * 1.5f,
                    Vector3.up * 20f + base.transform.forward * 2f).Send(NetworkDestination.Clients);
            }
        }

        //steal quirk code
        private void StealQuirk(HurtBox hurtBox)
        {
            var name = BodyCatalog.GetBodyName(hurtBox.healthComponent.body.bodyIndex);
            GameObject newbodyPrefab = BodyCatalog.FindBodyPrefab(name);

            AkSoundEngine.PostEvent(3192656820, characterBody.gameObject);

            //unset skills but not to unwrite the skilllist
            //override skills to choosdef
            RemovePrimary();
			RemoveSecondary();
			RemoveUtility();
			RemoveSpecial();
			RemoveExtra1();
			RemoveExtra2();
			RemoveExtra3();
			RemoveExtra4();

            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);

            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);

            //elite aspects
            if (hurtBox.healthComponent.body.isElite)
			{
				if (!hurtBox.healthComponent.body.HasBuff(Buffs.eliteDebuff.buffIndex))
				{
					if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixBlue))
					{
						Chat.AddMessage("Stole Overloading <style=cIsUtility>Quirk!</style>");
						dropEquipment(RoR2Content.Elites.Lightning.eliteEquipmentDef);
						hurtBox.healthComponent.body.ApplyBuff(Buffs.eliteDebuff.buffIndex, 1, 60);
					}
					if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixHaunted))
					{
						Chat.AddMessage("Stole Celestine <style=cIsUtility>Quirk!</style>");
						dropEquipment(RoR2Content.Elites.Haunted.eliteEquipmentDef);
						hurtBox.healthComponent.body.ApplyBuff(Buffs.eliteDebuff.buffIndex, 1, 60);
					}
					if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixLunar))
					{
						Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
						dropEquipment(RoR2Content.Elites.Lunar.eliteEquipmentDef);
						hurtBox.healthComponent.body.ApplyBuff(Buffs.eliteDebuff.buffIndex, 1, 60);
					}
					if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixPoison))
					{
						Chat.AddMessage("Stole Malachite <style=cIsUtility>Quirk!</style>");
						dropEquipment(RoR2Content.Elites.Poison.eliteEquipmentDef);
						hurtBox.healthComponent.body.ApplyBuff(Buffs.eliteDebuff.buffIndex, 1, 60);
					}
					if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixRed))
					{
						Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
						dropEquipment(RoR2Content.Elites.Fire.eliteEquipmentDef);
						hurtBox.healthComponent.body.ApplyBuff(Buffs.eliteDebuff.buffIndex, 1, 60);
					}
					if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixWhite))
					{
						Chat.AddMessage("Stole Glacial <style=cIsUtility>Quirk!</style>");
						dropEquipment(RoR2Content.Elites.Ice.eliteEquipmentDef);
						hurtBox.healthComponent.body.ApplyBuff(Buffs.eliteDebuff.buffIndex, 1, 60);
					}
					if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteEarth))
					{
						Chat.AddMessage("Stole Mending <style=cIsUtility>Quirk!</style>");
						dropEquipment(DLC1Content.Elites.Earth.eliteEquipmentDef);
						hurtBox.healthComponent.body.ApplyBuff(Buffs.eliteDebuff.buffIndex, 1, 60);
					}
					if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteVoid))
					{
						Chat.AddMessage("Stole Void <style=cIsUtility>Quirk!</style>");
						dropEquipment(DLC1Content.Elites.Void.eliteEquipmentDef);
						hurtBox.healthComponent.body.ApplyBuff(Buffs.eliteDebuff.buffIndex, 1, 60);
					}

				}
				else if (hurtBox.healthComponent.body.HasBuff(Buffs.eliteDebuff.buffIndex))
				{
					Chat.AddMessage("Can't steal <style=cIsUtility>Elite Quirk until debuff is gone</style>.");
				}
			}

            if (newbodyPrefab.name == "VultureBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.alloyvultureflyDef, 0);
            }
            if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.alphacontructpassiveDef, 0);

                characterBody.ApplyBuff(Modules.Buffs.alphashieldonBuff.buffIndex);
            }
            if (newbodyPrefab.name == "BeetleBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.beetlepassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.beetleBuff.buffIndex);
            }
            if (newbodyPrefab.name == "BeetleGuardBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.beetleguardslamDef, 0);
            }
            if (newbodyPrefab.name == "BisonBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.bisonchargeDef, 0);
            }
            if (newbodyPrefab.name == "FlyingVerminBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.pestpassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.pestjumpBuff.buffIndex);
            }
            if (newbodyPrefab.name == "VerminBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.verminpassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.verminsprintBuff.buffIndex);
            }
            if (newbodyPrefab.name == "BellBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.bronzongballDef, 0);
            }
            if (newbodyPrefab.name == "ClayGrenadierBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.clayapothecarymortarDef, 0);
            }
            if (newbodyPrefab.name == "ClayBruiserBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.claytemplarminigunDef, 0);
            }
            if (newbodyPrefab.name == "LemurianBruiserBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Fire Blast Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.elderlemurianfireblastDef, 0);
            }
            if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.guppassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.gupspikeBuff.buffIndex);
            }
            if (newbodyPrefab.name == "GreaterWispBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Spirit Boost Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.greaterwispballDef, 0);
            }
            if (newbodyPrefab.name == "HermitCrabBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.hermitcrabpassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortarBuff.buffIndex);
            }
            if (newbodyPrefab.name == "ImpBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.impblinkDef, 0);
            }
            if (newbodyPrefab.name == "JellyfishBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.jellyfishnovaDef, 0);
            }
            if (newbodyPrefab.name == "AcidLarvaBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.larvapassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.larvajumpBuff.buffIndex);
            }
            if (newbodyPrefab.name == "LemurianBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.lemurianfireballDef, 0);
            }
            if (newbodyPrefab.name == "WispBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.lesserwisppassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.lesserwispBuff.buffIndex);
            }
            if (newbodyPrefab.name == "LunarExploderBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.lunarexploderpassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.lunarexploderBuff.buffIndex);
            }
            if (newbodyPrefab.name == "LunarGolemBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.lunargolemslideDef, 0);
            }
            if (newbodyPrefab.name == "LunarWispBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.lunarwispminigunDef, 0);
            }
            if (newbodyPrefab.name == "MiniMushroomBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.minimushrumpassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.minimushrumBuff.buffIndex);
            }
            if (newbodyPrefab.name == "ParentBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.parentteleportDef, 0);
            }
            if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Solus Boost Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.roboballminibpassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.roboballminiBuff.buffIndex);
            }
            if (newbodyPrefab.name == "GolemBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.stonegolemlaserDef, 0);
            }
            if (newbodyPrefab.name == "VoidBarnacleBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.voidbarnaclepassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarBuff.buffIndex);
            }
            if (newbodyPrefab.name == "VoidJailerBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.voidjailerpassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.voidjailerBuff.buffIndex);
            }
            if (newbodyPrefab.name == "NullifierBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.voidreaverportalDef, 0);
            }


            if (newbodyPrefab.name == "BeetleQueen2Body")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.beetlequeenshotgunDef, 0);
            }
            if (newbodyPrefab.name == "ImpBossBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Bleed Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.impbosspassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.impbossBuff.buffIndex);
            }
            if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.stonetitanpassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.stonetitanBuff.buffIndex);
            }
            if (newbodyPrefab.name == "GrandParentBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Solar Flare Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.grandparentsunDef, 0);
            }
            if (newbodyPrefab.name == "GravekeeperBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.grovetenderhookDef, 0);
            }
            if (newbodyPrefab.name == "VagrantBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.vagrantpassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.vagrantBuff.buffIndex);
            }
            if (newbodyPrefab.name == "MagmaWormBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.magmawormpassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.magmawormBuff.buffIndex);
            }
            if (newbodyPrefab.name == "ElectricWormBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.overloadingwormpassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.overloadingwormBuff.buffIndex);
            }
            if (newbodyPrefab.name == "ClayBossBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Tar Boost Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.claydunestriderbuffDef, 0);
            }
            if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.soluscontrolunityknockupDef, 0);
            }
            if (newbodyPrefab.name == "MegaConstructBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.xiconstructbeamDef, 0);
            }
            if (newbodyPrefab.name == "VoidMegaCrabBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.voiddevastatorhomingDef, 0);
            }
            if (newbodyPrefab.name == "ScavBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.scavengerthqwibDef, 0);
            }

            if (newbodyPrefab.name == "Bandit2Body")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Lights Out Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.banditlightsoutDef, 0);
            }

            if (newbodyPrefab.name == "CaptainBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Defensive Microbots Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.captainpassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.captainBuff.buffIndex);
            }
            if (newbodyPrefab.name == "CommandoBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Double Tap Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.commandopassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.commandoBuff.buffIndex);
            }
            if (newbodyPrefab.name == "CrocoBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Poison Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.acridpassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.acridBuff.buffIndex);
            }
            if (newbodyPrefab.name == "EngiBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Turret Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.engiturretDef, 0);
            }
            //if (newbodyPrefab.name == "HereticBody")
            //{

            //    hasQuirk = true;
            //    Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.scavengerthqwibDef, 0);
            //    RemovePrimary();
            //    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
            //}
            if (newbodyPrefab.name == "HuntressBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Flurry Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.huntressattackDef, 0);
            }
            if (newbodyPrefab.name == "LoaderBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Scrap Barrier Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.loaderpassiveDef, 0);
                characterBody.ApplyBuff(Modules.Buffs.loaderBuff.buffIndex);
            }
            if (newbodyPrefab.name == "MageBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Elementality Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.artificerflamethrowerDef, 0);
            }
            if (newbodyPrefab.name == "MercBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Eviscerate Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.mercdashDef, 0);
            }
            if (newbodyPrefab.name == "ToolbotBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Power Stance Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.multbuffDef, 0);
            }
            if (newbodyPrefab.name == "TreebotBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Seed Barrage Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.rexmortarDef, 0);
            }
            if (newbodyPrefab.name == "RailgunnerBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Cryocharged Railgun Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.railgunnercryoDef, 0);
            }
            if (newbodyPrefab.name == "VoidSurvivorBody")
            {

                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Cleanse Quirk</style> Get!");

                Shiggymastercon.writeToAFOSkillList(Shiggy.voidfiendcleanseDef, 0);
            }
            if (hasQuirk = false)
            {
                //Shiggymastercon.transformed = false;
                Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
            }
            
        }


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
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.acridpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.commandopassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.captainpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.loaderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

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
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.acridpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.commandopassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.captainpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.loaderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

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
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.acridpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.commandopassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.captainpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.loaderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

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
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.acridpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.commandopassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.captainpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.loaderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

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
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.claydunestriderbuffDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.elderlemurianfireblastDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.banditlightsoutDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.engiturretDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.huntressattackDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.mercdashDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.railgunnercryoDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.rexmortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.voidfiendcleanseDef, GenericSkill.SkillOverridePriority.Contextual);

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
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.claydunestriderbuffDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.elderlemurianfireblastDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.banditlightsoutDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.engiturretDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.huntressattackDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.mercdashDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.railgunnercryoDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.rexmortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voidfiendcleanseDef, GenericSkill.SkillOverridePriority.Contextual);

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
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.claydunestriderbuffDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.elderlemurianfireblastDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.banditlightsoutDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.engiturretDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.huntressattackDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.mercdashDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.railgunnercryoDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.rexmortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.voidfiendcleanseDef, GenericSkill.SkillOverridePriority.Contextual);

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
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.claydunestriderbuffDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.elderlemurianfireblastDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.banditlightsoutDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.engiturretDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.huntressattackDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.mercdashDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.railgunnercryoDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.rexmortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.voidfiendcleanseDef, GenericSkill.SkillOverridePriority.Contextual);

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


