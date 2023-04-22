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
using ShiggyMod.SkillStates;
using IL.RoR2.Achievements.Bandit2;
using RoR2.Items;
using UnityEngine.AddressableAssets;

namespace ShiggyMod.Modules.Survivors
{
    public class ShiggyController : MonoBehaviour
	{
		string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";


        public float AFOTimer;
        public float overloadingtimer;
		public float magmawormtimer;
		public float vagranttimer;
		public float alphaconstructshieldtimer;
		public float lunarTimer;
		public float larvaTimer;
        public float attackSpeedGain;
        public float mortarTimer;
        private float jellyfishtimer;
        private float windshieldTimer;
        private float mechStanceTimer;
        private float airwalkTimer;
        private float OFATimer;

        private Ray downRay;
        public float maxTrackingDistance = 70f;
		public float maxTrackingAngle = 20f;
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
        private Animator anim;
        private readonly BullseyeSearch search = new BullseyeSearch();
		private CharacterMaster characterMaster;

		public ShiggyMasterController Shiggymastercon;
		public ShiggyController Shiggycon;
        public EnergySystem energySystem;
        public BuffController buffcon; 

        public bool larvabuffGiven;
		public bool verminjumpbuffGiven;
        private uint minimushrumsoundID;

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

		public bool alloyvultureWindBlastDef;
		public bool beetleguardslamDef;
		public bool bisonchargeDef;
		public bool bronzongballDef;
		public bool clayapothecarymortarDef;
		public bool claytemplarminigunDef;
		public bool greaterwispballDef;
		public bool impblinkDef;
		public bool jellyfishHealDef;
		public bool lemurianfireballDef;
		public bool lunargolemshotsDef;
		public bool lunarwispminigunDef;
		public bool parentteleportDef;
		public bool stonegolemlaserDef;
		public bool voidreaverportalDef;
		public bool beetlequeenSummonDef;
        public bool grandparentsunDef;
        public bool grovetenderChainDef;
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
        public int captainitemcount;
        private DamageType damageType;
        private DamageType damageType2;
        private float multTimer;
        private float clayDunestriderTimer;
        private float greaterwispTimer;

        //AFO
        private bool informAFOToPlayers;
        private bool hasStolen;
        private float stopwatch;
        private bool hasRemoved;
        private float stopwatch2;

        public void Awake()
        {

            child = GetComponentInChildren<ChildLocator>();
            anim = GetComponentInChildren<Animator>();

            indicator = new Indicator(gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/RecyclerIndicator"));
			passiveindicator = new Indicator(gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/EngiMissileTrackingIndicator"));
			activeindicator = new Indicator(gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/HuntressTrackingIndicator"));
			//On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
			inputBank = gameObject.GetComponent<InputBankTest>();

			larvabuffGiven = false;
			verminjumpbuffGiven = false;

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


			alloyvultureWindBlastDef = false;
			beetleguardslamDef = false;
			bisonchargeDef = false;
			bronzongballDef = false;
			clayapothecarymortarDef = false;
			claytemplarminigunDef = false;
			greaterwispballDef = false;
			impblinkDef = false;
			jellyfishHealDef = false;
			lemurianfireballDef = false;
			lunargolemshotsDef = false;
			lunarwispminigunDef = false;
			parentteleportDef = false;
			stonegolemlaserDef = false;
			voidreaverportalDef = false;

			beetlequeenSummonDef = false;
			grovetenderChainDef = false;
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
            hasStolen = false;

        }


        public void Start()
        {
            characterBody = gameObject.GetComponent<CharacterBody>();
            characterMaster = characterBody.master;

            energySystem = gameObject.GetComponent<EnergySystem>();
            if(!energySystem)
            {
                energySystem = gameObject.AddComponent<EnergySystem>();
            }
            buffcon = characterMaster.gameObject.GetComponent<BuffController>();
            if (!buffcon)
            {
                buffcon = characterMaster.gameObject.AddComponent<BuffController>();
            }

            Shiggymastercon = characterMaster.gameObject.GetComponent<ShiggyMasterController>();
            if (!Shiggymastercon)
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


			alloyvultureWindBlastDef = false;
			beetleguardslamDef = false;
			bisonchargeDef = false;
			bronzongballDef = false;
			clayapothecarymortarDef = false;
			claytemplarminigunDef = false;
			greaterwispballDef = false;
			impblinkDef = false;
			jellyfishHealDef = false;
			lemurianfireballDef = false;
			lunargolemshotsDef = false;
			lunarwispminigunDef = false;
			parentteleportDef = false;
			stonegolemlaserDef = false;
			voidreaverportalDef = false;

			beetlequeenSummonDef = false;
			grovetenderChainDef = false;
            grandparentsunDef = false;
            claydunestriderbuffDef = false;
			soluscontrolunityknockupDef = false;
			xiconstructbeamDef = false;
			voiddevastatorhomingDef = false;
			scavengerthqwibDef = false;

            informAFOToPlayers = false;
            hasStolen = false;
            hasQuirk = false;
            hasExtra1 = false;
            hasExtra2 = false;
            hasExtra3 = false;
            hasExtra4 = false;
        }



        public HurtBox GetTrackingTarget()
		{
			return this.trackingTarget;
		}

        public void OnEnable()
		{
			this.indicator.active = true;
			this.passiveindicator.active = true;
			this.activeindicator.active = true;
		}

        public void OnDisable()
		{
			this.indicator.active = false;
			this.passiveindicator.active = false;
			this.activeindicator.active = false;
		}

		public void OnDestroy()
        {
            //if (mortarIndicatorInstance) EntityState.Destroy(mortarIndicatorInstance.gameObject);
            //if (this.voidmortarIndicatorInstance) EntityState.Destroy(this.voidmortarIndicatorInstance.gameObject);
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
            {
                //OFA
                if (characterBody.HasBuff(Buffs.OFABuff))
                {
                    //play ofa particles here

                    if (OFATimer < StaticValues.OFAThreshold)
                    {
                        OFATimer += Time.fixedDeltaTime;
                    }
                    else if (OFATimer >= StaticValues.OFAThreshold)
                    {
                        OFATimer = 0f;
                        //take damage every second based off current hp
                        new SpendHealthNetworkRequest(characterBody.masterObjectId, characterBody.healthComponent.combinedHealth * StaticValues.OFAHealthCostCoefficient).Send(NetworkDestination.Clients);
                    }
                }


                //moving buffs
                //air walk
                if (!characterBody.characterMotor.isGrounded)
                {
                    airwalkTimer += Time.fixedDeltaTime;
                    //after 0.5 seconds start flying
                    if (airwalkTimer > 0.5f)
                    {
                        if (energySystem.currentplusChaos > 1f)
                        {
                            if (characterBody.inputBank.jump.down)
                            {
                                characterBody.ApplyBuff(Modules.Buffs.airwalkBuff.buffIndex, 1);

                                //if falling down
                                if (characterBody.characterMotor.velocity.y < 0)
                                {
                                    energySystem.SpendplusChaos(StaticValues.airwalkEnergyFraction);
                                    if (characterBody.inputBank.skill1.down
                                        | characterBody.inputBank.skill2.down
                                        | characterBody.inputBank.skill3.down
                                        | characterBody.inputBank.skill4.down
                                        | extrainputBankTest.extraSkill1.down
                                        | extrainputBankTest.extraSkill2.down
                                        | extrainputBankTest.extraSkill3.down
                                        | extrainputBankTest.extraSkill4.down)
                                    {
                                        characterBody.characterMotor.velocity.y = 0f;
                                    }
                                    else
                                    {
                                        if (airwalkTimer < 3f)
                                        {
                                            characterBody.characterMotor.velocity.y = characterBody.moveSpeed;
                                        }
                                        else
                                        {
                                            characterBody.characterMotor.velocity.y = 0f;
                                        }
                                    }
                                }
                                //if rising up
                                else if (characterBody.characterMotor.velocity.y >= 0)
                                {
                                    energySystem.SpendplusChaos(StaticValues.airwalkEnergyFraction);
                                    if (characterBody.inputBank.skill1.down
                                        | characterBody.inputBank.skill2.down
                                        | characterBody.inputBank.skill3.down
                                        | characterBody.inputBank.skill4.down
                                        | extrainputBankTest.extraSkill1.down
                                        | extrainputBankTest.extraSkill2.down
                                        | extrainputBankTest.extraSkill3.down
                                        | extrainputBankTest.extraSkill4.down)
                                    {
                                        characterBody.characterMotor.velocity.y = 0f;
                                    }
                                    else
                                    {
                                        if (airwalkTimer < 3f)
                                        {
                                            characterBody.characterMotor.velocity.y = characterBody.moveSpeed;
                                        }
                                        else
                                        {
                                            characterBody.characterMotor.velocity.y = 0f;
                                        }
                                    }
                                }

                            }

                            //move in the direction you're moving at a normal speed
                            if (characterBody.inputBank.moveVector != Vector3.zero)
                            {
                                energySystem.SpendplusChaos(StaticValues.airwalkEnergyFraction);
                                //characterBody.characterMotor.velocity = characterBody.inputBank.moveVector * (characterBody.moveSpeed);
                                characterBody.characterMotor.rootMotion += characterBody.inputBank.moveVector * characterBody.moveSpeed * Time.fixedDeltaTime;
                                //characterBody.characterMotor.disableAirControlUntilCollision = false;
                            }


                        }

                    }
                }
                else if (characterBody.characterMotor.isGrounded)
                {
                    //remove airwalk buff when landed
                    airwalkTimer = 0f;
                    if (NetworkServer.active)
                    {
                        characterBody.ApplyBuff(Modules.Buffs.airwalkBuff.buffIndex, 0);
                    }
                }



                //shiggy current damage
                shiggyDamage = characterBody.damage;

                //Buff effects

                //mechstance buff
                if (characterBody.HasBuff(Buffs.mechStanceBuff))
                {
                    if (characterBody.inputBank.jump.down)
                    {
                        if (characterBody.characterMotor.velocity.y > 2f)
                        {
                            characterBody.characterMotor.velocity.y = 0f;
                        }
                    }

                    if (anim)
                    {
                        if (anim.GetBool("isMoving"))
                        {
                            //while walking do blast attacks, scales with movespeed
                            mechStanceTimer += Time.fixedDeltaTime;
                            if(mechStanceTimer >= StaticValues.mechStanceStepRate / characterBody.moveSpeed)
                            {

                                EffectManager.SpawnEffect(EntityStates.BeetleGuardMonster.GroundSlam.slamEffectPrefab, new EffectData
                                {
                                    origin = characterBody.footPosition,
                                    scale = StaticValues.mechStanceRadius * (characterBody.moveSpeed / characterBody.baseMoveSpeed),
                                }, true);

                                BlastAttack blastAttack = new BlastAttack();
                                blastAttack.radius = StaticValues.mechStanceRadius * (characterBody.moveSpeed / characterBody.baseMoveSpeed);
                                blastAttack.procCoefficient = StaticValues.mechStanceProcCoefficient;
                                blastAttack.position = characterBody.footPosition;
                                blastAttack.attacker = characterBody.gameObject;
                                blastAttack.crit = characterBody.RollCrit();
                                blastAttack.baseDamage = characterBody.damage * StaticValues.mechStanceDamageCoefficient * (characterBody.moveSpeed/ characterBody.baseMoveSpeed);
                                blastAttack.falloffModel = BlastAttack.FalloffModel.None;
                                blastAttack.baseForce = 400f;
                                blastAttack.teamIndex = characterBody.teamComponent.teamIndex;
                                blastAttack.damageType = damageType;
                                blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
                                blastAttack.AddModdedDamageType(Modules.Damage.shiggyDecay);

                                blastAttack.Fire();
                                mechStanceTimer = 0f;
                            }
                        }
                    }

                    
                }

                //windshield buff
                if (characterBody.HasBuff(Buffs.windShieldBuff))
                {


                    Collider[] array = Physics.OverlapSphere(characterBody.transform.position, StaticValues.windShieldRadius, LayerIndex.projectile.mask);
                    for (int i = 0; i < array.Length; i++)
                    {
                        ProjectileController component = array[i].GetComponent<ProjectileController>();
                        if (component)
                        {
                            TeamComponent component2 = component.owner.GetComponent<TeamComponent>();
                            if (component2 && component2.teamIndex != TeamComponent.GetObjectTeam(characterBody.gameObject))
                            {
                                EffectData effectData = new EffectData();
                                effectData.origin = component.transform.position;
                                effectData.scale = 1f;
                                EffectManager.SpawnEffect(EntityStates.Engi.EngiWeapon.FireSeekerGrenades.hitEffectPrefab, effectData, false);
                                UnityEngine.Object.Destroy(array[i].gameObject);
                                //Object.Destroy(component.gameObject);
                            }
                        }
                    }


                    if (windshieldTimer < 1f)
                    {
                        windshieldTimer += Time.fixedDeltaTime;
                    }
                    if (windshieldTimer >= 1f)
                    {
                        windshieldTimer = 0f;
                        new BlastAttack
                        {
                            crit = false,
                            attacker = characterBody.gameObject,
                            teamIndex = TeamComponent.GetObjectTeam(characterBody.gameObject),
                            falloffModel = BlastAttack.FalloffModel.None,
                            baseDamage = characterBody.damage * StaticValues.windShieldDamageCoefficient,
                            damageType = DamageType.Stun1s,
                            damageColorIndex = DamageColorIndex.Default,
                            baseForce = 0,
                            procChainMask = new ProcChainMask(),
                            position = characterBody.transform.position,
                            radius = StaticValues.windShieldRadius,
                            procCoefficient = 0.001f,
                            attackerFiltering = AttackerFiltering.NeverHitSelf,
                        }.Fire();

                        EffectManager.SpawnEffect(Modules.Assets.engiShieldEffect, new EffectData
                        {
                            origin = characterBody.transform.position,
                            scale = StaticValues.windShieldRadius,
                            rotation = Quaternion.LookRotation(characterBody.characterDirection.forward)

                        }, false);
                    }
                }

                //multbuff buff
                if (characterBody.HasBuff(Modules.Buffs.multBuff))
                {
                    if (multTimer > 1f)
                    {
                        multTimer = 0f;
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
                        multTimer += Time.fixedDeltaTime;

                    }
                }
                //claydunestrider buff
                if (characterBody.HasBuff(Modules.Buffs.claydunestriderBuff))
                {
                    if (clayDunestriderTimer > 1f)
                    {
                        clayDunestriderTimer = 0f;
                        EffectManager.SpawnEffect(Modules.Assets.claydunestriderEffect, new EffectData
                        {
                            origin = characterBody.corePosition,
                            scale = 1f,
                            rotation = Quaternion.LookRotation(-characterBody.characterDirection.forward)

                        }, false);
                    }
                    else
                    {
                        clayDunestriderTimer += Time.fixedDeltaTime;

                    }
                }
                //Greaterwisp buff
                if (characterBody.HasBuff(Modules.Buffs.greaterwispBuff))
                {
                    if (greaterwispTimer > 1f)
                    {
                        greaterwispTimer = 0f;
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
                        greaterwispTimer += Time.fixedDeltaTime;

                    }
                }

                ////captain buff items
                //captainitemcount = characterBody.master.inventory.GetItemCount(RoR2Content.Items.CaptainDefenseMatrix);
                //if (characterBody.HasBuff(Buffs.captainBuff))
                //{
                //    if (captainitemcount < 1)
                //    {
                //        characterBody.master.inventory.GiveItem(RoR2Content.Items.CaptainDefenseMatrix, 1);
                //    }

                //}
                //else if (!characterBody.HasBuff(Buffs.captainBuff) && captainitemcount > 0)
                //{
                //    characterBody.master.inventory.RemoveItem(RoR2Content.Items.CaptainDefenseMatrix, 1);
                //}

                //damagetypes for moves
                damageType = DamageType.Generic;
                damageType2 = DamageType.SlowOnHit;



                //overloadingworm buff
                //if (characterBody.HasBuff(Modules.Buffs.overloadingwormBuff.buffIndex))
                //{
                //    if (!NetworkServer.active)
                //    {
                //        return;
                //    }
                //    if (overloadingWard == null)
                //    {
                //        this.overloadingWard = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/NearbyDamageBonusIndicator"), characterBody.footPosition, Quaternion.identity);
                //        this.overloadingWard.transform.parent = characterBody.transform;
                //        //this.magmawormWard.GetComponent<TeamFilter>().teamIndex = characterBody.teamComponent.teamIndex;

                //        if (overloadingtimer > StaticValues.overloadingInterval / characterBody.attackSpeed)
                //        {
                //            overloadingtimer = 0f;
                //            OverloadingFire();

                //        }
                //        else
                //        {
                //            overloadingtimer += Time.fixedDeltaTime;
                //        }
                //    }
                //}
                //else if (!characterBody.HasBuff(Modules.Buffs.overloadingwormBuff.buffIndex))
                //{
                //    if (this.overloadingWard)
                //    {
                //        EntityState.Destroy(this.overloadingWard);
                //    }
                //}

                //magmaworm buff
                //if (characterBody.HasBuff(Modules.Buffs.magmawormBuff.buffIndex))
                //{
                //    if (!NetworkServer.active)
                //    {
                //        return;
                //    }
                //    if (magmawormWard == null)
                //    {
                //        this.magmawormWard = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/NearbyDamageBonusIndicator"), characterBody.footPosition, Quaternion.identity);
                //        this.magmawormWard.transform.parent = characterBody.transform;
                //        //this.magmawormWard.GetComponent<TeamFilter>().teamIndex = characterBody.teamComponent.teamIndex;

                //        if (magmawormtimer > StaticValues.magmawormInterval / characterBody.attackSpeed)
                //        {
                //            magmawormtimer = 0f;
                //            MagmawormFire();

                //        }
                //        else
                //        {
                //            magmawormtimer += Time.fixedDeltaTime;
                //        }
                //    }
                //}
                //else if (!characterBody.HasBuff(Modules.Buffs.magmawormBuff.buffIndex))
                //{
                //    if (this.magmawormWard)
                //    {
                //        EntityState.Destroy(this.magmawormWard);
                //    }
                //}

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
                //if (characterBody.HasBuff(Modules.Buffs.roboballminiBuff.buffIndex))
                //{
                //    if (characterBody.inputBank.skill1.down
                //        | characterBody.inputBank.skill2.down
                //        | characterBody.inputBank.skill3.down
                //        | characterBody.inputBank.skill4.down
                //        | extrainputBankTest.extraSkill1.down
                //        | extrainputBankTest.extraSkill2.down
                //        | extrainputBankTest.extraSkill3.down
                //        | extrainputBankTest.extraSkill4.down)
                //    {
                //        if (roboballTimer > 1f)
                //        {
                //            roboballTimer = 0f;
                //            characterBody.ApplyBuff(Modules.Buffs.roboballminiattackspeedBuff.buffIndex);
                //        }
                //        else
                //        {
                //            roboballTimer += Time.fixedDeltaTime;

                //        }
                //    }
                //    else if (!characterBody.inputBank.skill1.down
                //        && !characterBody.inputBank.skill2.down
                //        && !characterBody.inputBank.skill3.down
                //        && !characterBody.inputBank.skill1.down
                //        && !extrainputBankTest.extraSkill1.down
                //        && !extrainputBankTest.extraSkill2.down
                //        && !extrainputBankTest.extraSkill3.down
                //        && !extrainputBankTest.extraSkill4.down)
                //    {
                //        characterBody.ApplyBuff(Modules.Buffs.roboballminiattackspeedBuff.buffIndex, 0);
                //    }
                //    //if (characterBody.inputBank.jump.down)
                //    //{
                //    //    characterBody.ApplyBuff(Buffs.airwalkBuff.buffIndex);
                //    //    base.transform.position = characterBody.transform.position;
                //    //    if (characterBody.hasEffectiveAuthority && characterBody.characterMotor)
                //    //    {
                //    //        if (characterBody.inputBank.moveVector != Vector3.zero)
                //    //        {
                //    //            characterBody.characterMotor.velocity = characterBody.inputBank.moveVector * (characterBody.moveSpeed * Modules.StaticValues.roboballboostMultiplier);
                //    //            characterBody.characterMotor.disableAirControlUntilCollision = false;
                //    //        }
                //    //    }
                //    //}
                //    //else if (!characterBody.inputBank.jump.down)
                //    //{
                //    //    characterBody.RemoveBuff(Buffs.airwalkBuff.buffIndex);
                //    //}

                //}


                //jellyfish heal buff
                if (characterBody.hasEffectiveAuthority)
                {
                    if (characterBody.HasBuff(Modules.Buffs.jellyfishHealStacksBuff.buffIndex))
                    {

                        if (jellyfishtimer > 1f)
                        {
                            int buffCountToApply = characterBody.GetBuffCount(Modules.Buffs.jellyfishHealStacksBuff.buffIndex);
                            int buffCountToApply2 = Mathf.RoundToInt(characterBody.maxHealth * (1 - StaticValues.JellyfishHealTickRate));
                            int buffCountToApply3 = buffCountToApply - buffCountToApply2;
                            if(buffCountToApply3 < 2)
                            {
                                buffCountToApply3 = 2;
                            }
                            if (buffCountToApply > 1)
                            {
                                if (buffCountToApply >= 2)
                                {
                                    characterBody.ApplyBuff(Modules.Buffs.jellyfishHealStacksBuff.buffIndex, buffCountToApply3);
                                    jellyfishtimer = 0f;
                                }
                            }
                        }
                        else jellyfishtimer += Time.fixedDeltaTime;
                    }

                }


                //mini mushrum buff
                //if (characterBody.HasBuff(Modules.Buffs.minimushrumBuff.buffIndex))
                //{
                //    if (!NetworkServer.active)
                //    {
                //        return;
                //    }
                //    if (this.mushroomWard == null)
                //    {
                //        this.minimushrumsoundID = Util.PlaySound(Plant.healSoundLoop, characterBody.modelLocator.modelTransform.gameObject);
                //        this.mushroomWard = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/MiniMushroomWard"), characterBody.footPosition, Quaternion.identity);
                //        this.mushroomWard.transform.parent = characterBody.transform;
                //        this.mushroomWard.GetComponent<TeamFilter>().teamIndex = characterBody.teamComponent.teamIndex;
                //        if (this.mushroomWard)
                //        {
                //            HealingWard component = this.mushroomWard.GetComponent<HealingWard>();
                //            component.healFraction = Modules.StaticValues.minimushrumhealFraction;
                //            component.healPoints = 0f;
                //            component.Networkradius = Modules.StaticValues.minimushrumRadius;
                //            component.interval = Modules.StaticValues.minimushrumInterval;
                //            //component.healTimer = Modules.StaticValues.minimushrumhealFraction;
                //        }
                //        NetworkServer.Spawn(this.mushroomWard);
                //    }
                //}
                //else if (!characterBody.HasBuff(Modules.Buffs.minimushrumBuff.buffIndex))
                //{
                //    if (this.mushroomWard)
                //    {
                //        AkSoundEngine.StopPlayingID(this.minimushrumsoundID);
                //        Util.PlaySound(Plant.healSoundStop, base.gameObject);
                //        EntityState.Destroy(this.mushroomWard);
                //    }
                //}

                ////alpha shield buff
                //if (characterBody.hasEffectiveAuthority)
                //{
                //    if (characterBody.HasBuff(Modules.Buffs.alphashieldoffBuff.buffIndex))
                //    {

                //        if (alphaconstructshieldtimer > 1f)
                //        {
                //            int buffCountToApply2 = characterBody.GetBuffCount(Modules.Buffs.alphashieldoffBuff.buffIndex);
                //            if (buffCountToApply2 > 1)
                //            {
                //                if (buffCountToApply2 >= 2)
                //                {
                //                    characterBody.ApplyBuff(Modules.Buffs.alphashieldoffBuff.buffIndex, buffCountToApply2 - 1);
                //                    alphaconstructshieldtimer = 0f;
                //                }
                //            }
                //            else
                //            {
                //                characterBody.ApplyBuff(Modules.Buffs.alphashieldoffBuff.buffIndex, 0);
                //                characterBody.ApplyBuff(Modules.Buffs.alphashieldonBuff.buffIndex, 1);

                //            }
                //        }
                //        else alphaconstructshieldtimer += Time.fixedDeltaTime;
                //    }

                //}


                ////Standing still/not moving buffs
                //if (characterBody.GetNotMoving())
                //{

                //    //hermitcrab mortarbuff
                //    if (characterBody.HasBuff(Modules.Buffs.hermitcrabmortarBuff))
                //    {
                //        if (!this.mortarIndicatorInstance)
                //        {
                //            CreateMortarIndicator();
                //        }
                //        mortarTimer += Time.fixedDeltaTime;
                //        if (mortarTimer >= Modules.StaticValues.mortarbaseDuration / (characterBody.attackSpeed))
                //        {
                //            int hermitbuffcount = characterBody.GetBuffCount(Buffs.hermitcrabmortararmorBuff.buffIndex);
                //            characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortararmorBuff.buffIndex, hermitbuffcount + 1);
                //            mortarTimer = 0f;
                //            FireMortar();
                //        }
                //    }
                //    else
                //    {
                //        if (this.mortarIndicatorInstance) EntityState.Destroy(this.mortarIndicatorInstance.gameObject);
                //        characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortararmorBuff.buffIndex, 0);

                //    }

                //    //voidbarnacle mortarbuff
                //    if (characterBody.HasBuff(Modules.Buffs.voidbarnaclemortarBuff))
                //    {
                //        if (!this.voidmortarIndicatorInstance)
                //        {
                //            CreateVoidMortarIndicator();
                //        }
                //        voidmortarTimer += Time.fixedDeltaTime;
                //        if (voidmortarTimer >= Modules.StaticValues.voidmortarbaseDuration / (characterBody.armor / characterBody.baseArmor))
                //        {
                //            int voidbarnaclebuffcount = characterBody.GetBuffCount(Buffs.voidbarnaclemortarattackspeedBuff.buffIndex);
                //            characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarattackspeedBuff.buffIndex, voidbarnaclebuffcount + 1);
                //            attackSpeedGain = Modules.StaticValues.voidmortarattackspeedGain * characterBody.GetBuffCount(Modules.Buffs.voidbarnaclemortarattackspeedBuff);
                //            voidmortarTimer = 0f;
                //            FireMortar();
                //        }
                //    }
                //    else
                //    {
                //        if (this.voidmortarIndicatorInstance) EntityState.Destroy(this.voidmortarIndicatorInstance.gameObject);
                //        characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarattackspeedBuff.buffIndex, 0);
                //    }
                //}

                //else if (!characterBody.GetNotMoving())
                //{
                //    if (this.mortarIndicatorInstance) EntityState.Destroy(this.mortarIndicatorInstance.gameObject);
                //    characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortararmorBuff.buffIndex, 0);
                //    if (this.voidmortarIndicatorInstance) EntityState.Destroy(this.voidmortarIndicatorInstance.gameObject);
                //    characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarattackspeedBuff.buffIndex, 0);

                //    //voidjailer buff
                //    if (characterBody.HasBuff(Modules.Buffs.voidjailerBuff.buffIndex))
                //    {
                //        float num = characterBody.moveSpeed;
                //        bool isSprinting = characterBody.isSprinting;
                //        if (isSprinting)
                //        {
                //            num /= characterBody.sprintingSpeedMultiplier;
                //        }
                //        voidjailerTimer += Time.fixedDeltaTime;
                //        if (voidjailerTimer > StaticValues.voidjailerInterval / (num / characterBody.baseMoveSpeed))
                //        {
                //            voidjailerTimer = 0f;
                //            VoidJailerPull();
                //        }
                //    }
                //}

                ////verminjump buff
                //if (characterBody.HasBuff(Buffs.pestjumpBuff) && !verminjumpbuffGiven)
                //{
                //    verminjumpbuffGiven = true;
                //    characterBody.characterMotor.jumpCount += Modules.StaticValues.verminjumpStacks;
                //    characterBody.maxJumpCount += Modules.StaticValues.verminjumpStacks;
                //    characterBody.baseJumpCount += Modules.StaticValues.verminjumpStacks;
                //    characterBody.jumpPower += Modules.StaticValues.verminjumpPower;
                //    characterBody.baseJumpPower += Modules.StaticValues.verminjumpPower;
                //}
                //else
                //{
                //    if (!characterBody.HasBuff(Buffs.pestjumpBuff))
                //    {
                //        if (verminjumpbuffGiven)
                //        {
                //            verminjumpbuffGiven = false;
                //            characterBody.characterMotor.jumpCount -= Modules.StaticValues.verminjumpStacks;
                //            characterBody.maxJumpCount -= Modules.StaticValues.verminjumpStacks;
                //            characterBody.baseJumpCount -= Modules.StaticValues.verminjumpStacks;
                //            characterBody.jumpPower -= Modules.StaticValues.verminjumpPower;
                //            characterBody.baseJumpPower -= Modules.StaticValues.verminjumpPower;
                //        }

                //    }
                //}
                ////larvajump buff
                //if (characterBody.HasBuff(Buffs.larvajumpBuff))
                //{
                //    if (!larvabuffGiven)
                //    {
                //        larvabuffGiven = true;
                //        characterBody.characterMotor.jumpCount += Modules.StaticValues.larvajumpStacks;
                //        characterBody.maxJumpCount += Modules.StaticValues.larvajumpStacks;
                //        characterBody.baseJumpCount += Modules.StaticValues.larvajumpStacks;
                //        characterBody.jumpPower += Modules.StaticValues.larvajumpPower;
                //        characterBody.baseJumpPower += Modules.StaticValues.larvajumpPower;
                //        characterBody.maxJumpHeight = Trajectory.CalculateApex(characterBody.jumpPower);
                //    }

                //    if (characterBody.inputBank.jump.justPressed && characterBody && characterBody.characterMotor.jumpCount < characterBody.maxJumpCount)
                //    {
                //        Vector3 footPosition = characterBody.footPosition;
                //        EffectManager.SpawnEffect(Modules.Assets.larvajumpEffect, new EffectData
                //        {
                //            origin = footPosition,
                //            scale = Modules.StaticValues.larvaRadius
                //        }, true);

                //        BlastAttack blastAttack = new BlastAttack();
                //        blastAttack.radius = Modules.StaticValues.larvaRadius;
                //        blastAttack.procCoefficient = Modules.StaticValues.larvaProcCoefficient;
                //        blastAttack.position = characterBody.footPosition;
                //        blastAttack.attacker = base.gameObject;
                //        blastAttack.crit = Util.CheckRoll(characterBody.crit, characterBody.master);
                //        blastAttack.baseDamage = characterBody.damage * Modules.StaticValues.larvaDamageCoefficient * (characterBody.jumpPower / 5);
                //        blastAttack.falloffModel = BlastAttack.FalloffModel.None;
                //        blastAttack.baseForce = Modules.StaticValues.larvaForce;
                //        blastAttack.teamIndex = characterBody.teamComponent.teamIndex;
                //        blastAttack.damageType = damageType;
                //        blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
                //        blastAttack.AddModdedDamageType(Modules.Damage.shiggyDecay);
                //        blastAttack.Fire();


                //    }

                //    if (!characterBody.characterMotor.isGrounded)
                //    {
                //        larvaTimer += Time.fixedDeltaTime;
                //    }
                //    if (characterBody.characterMotor.isGrounded && larvaTimer > 1f)
                //    {
                //        larvaTimer = 0f;
                //        Vector3 footPosition = characterBody.footPosition;
                //        EffectManager.SpawnEffect(Modules.Assets.larvajumpEffect, new EffectData
                //        {
                //            origin = footPosition,
                //            scale = Modules.StaticValues.larvaRadius
                //        }, true);

                //        BlastAttack blastAttack = new BlastAttack();
                //        blastAttack.radius = Modules.StaticValues.larvaRadius;
                //        blastAttack.procCoefficient = Modules.StaticValues.larvaProcCoefficient;
                //        blastAttack.position = characterBody.footPosition;
                //        blastAttack.attacker = base.gameObject;
                //        blastAttack.crit = Util.CheckRoll(characterBody.crit, characterBody.master);
                //        blastAttack.baseDamage = characterBody.damage * Modules.StaticValues.larvaDamageCoefficient * (characterBody.jumpPower / 5);
                //        blastAttack.falloffModel = BlastAttack.FalloffModel.None;
                //        blastAttack.baseForce = Modules.StaticValues.larvaForce;
                //        blastAttack.teamIndex = characterBody.teamComponent.teamIndex;
                //        blastAttack.damageType = damageType;
                //        blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
                //        blastAttack.AddModdedDamageType(Modules.Damage.shiggyDecay);
                //        blastAttack.Fire();
                //    }
                //}
                //else
                //{
                //    if (!characterBody.HasBuff(Buffs.larvajumpBuff))
                //    {
                //        if (larvabuffGiven)
                //        {
                //            larvabuffGiven = false;
                //            characterBody.characterMotor.jumpCount -= Modules.StaticValues.larvajumpStacks;
                //            characterBody.maxJumpCount -= Modules.StaticValues.larvajumpStacks;
                //            characterBody.baseJumpCount -= Modules.StaticValues.larvajumpStacks;
                //            characterBody.jumpPower -= Modules.StaticValues.larvajumpPower;
                //            characterBody.baseJumpPower -= Modules.StaticValues.larvajumpPower;
                //            characterBody.maxJumpHeight = Trajectory.CalculateApex(characterBody.jumpPower);
                //        }
                //    }
                //}
                //lunar exploder buff
                //if (characterBody.HasBuff(Buffs.lunarexploderBuff))
                //{
                //    if (characterBody.healthComponent.combinedHealth < characterBody.healthComponent.fullCombinedHealth / 2)
                //    {
                //        lunarTimer += Time.fixedDeltaTime;
                //        if (characterBody.hasEffectiveAuthority && lunarTimer >= Modules.StaticValues.lunarexploderbaseDuration)
                //        {
                //            lunarTimer = 0f;
                //            for (int i = 0; i < Modules.StaticValues.lunarexploder; i++)
                //            {
                //                float num = 360f / Modules.StaticValues.lunarexploder;
                //                Vector3 forward = Util.QuaternionSafeLookRotation(characterBody.transform.forward, characterBody.transform.up) * Util.ApplySpread(Vector3.forward, 0f, 0f, 1f, 1f, num * (float)i, 0f);
                //                FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
                //                fireProjectileInfo.projectilePrefab = DeathState.projectilePrefab;
                //                fireProjectileInfo.position = characterBody.corePosition + Vector3.up * DeathState.projectileVerticalSpawnOffset;
                //                fireProjectileInfo.rotation = Util.QuaternionSafeLookRotation(forward);
                //                fireProjectileInfo.owner = characterBody.gameObject;
                //                fireProjectileInfo.damage = characterBody.damage * Modules.StaticValues.lunarexploderDamageCoefficient;
                //                fireProjectileInfo.crit = Util.CheckRoll(characterBody.crit, characterBody.master);
                //                ProjectileManager.instance.FireProjectile(fireProjectileInfo);
                //            }
                //            if (DeathState.deathExplosionEffect)
                //            {
                //                EffectManager.SpawnEffect(DeathState.deathExplosionEffect, new EffectData
                //                {
                //                    origin = characterBody.corePosition,
                //                    scale = Modules.StaticValues.lunarexploderRadius
                //                }, true);
                //            }
                //        }
                //    }
                //}

            }



        }

		//public void MagmawormFire()
		//{
		//	Ray aimRay = new Ray(this.inputBank.aimOrigin, this.inputBank.aimDirection);
		//	BullseyeSearch search = new BullseyeSearch
		//	{

		//		teamMaskFilter = TeamMask.GetEnemyTeams(characterBody.teamComponent.teamIndex),
		//		filterByLoS = false,
		//		searchOrigin = characterBody.corePosition,
		//		searchDirection = UnityEngine.Random.onUnitSphere,
		//		sortMode = BullseyeSearch.SortMode.Distance,
		//		maxDistanceFilter = StaticValues.magmawormRadius,
		//		maxAngleFilter = 360f
		//	};

		//	search.RefreshCandidates();
		//	search.FilterOutGameObject(characterBody.gameObject);



		//	List<HurtBox> target = search.GetResults().ToList<HurtBox>();
		//	foreach (HurtBox singularTarget in target)
		//	{
		//		if (singularTarget)
		//		{
		//			if (singularTarget.healthComponent && singularTarget.healthComponent.body)
		//			{
		//				InflictDotInfo info = new InflictDotInfo();
		//				info.attackerObject = characterBody.gameObject;
		//				info.victimObject = singularTarget.healthComponent.body.gameObject;
		//				info.duration = Modules.StaticValues.magmawormDuration;
		//				info.dotIndex = DotController.DotIndex.Burn;
		//				info.totalDamage = characterBody.damage * Modules.StaticValues.magmawormCoefficient;
		//				info.damageMultiplier = 1f;

		//				RoR2.StrengthenBurnUtils.CheckDotForUpgrade(characterBody.inventory, ref info);
		//				DotController.InflictDot(ref info);
		//			}
		//		}
		//	}

		//}

		//public void VoidJailerPull()
		//{
		//	BullseyeSearch search = new BullseyeSearch
		//	{

		//		teamMaskFilter = TeamMask.GetEnemyTeams(characterBody.teamComponent.teamIndex),
		//		filterByLoS = false,
		//		searchOrigin = characterBody.corePosition,
		//		searchDirection = UnityEngine.Random.onUnitSphere,
		//		sortMode = BullseyeSearch.SortMode.Distance,
		//		maxDistanceFilter = StaticValues.voidjailermaxpullDistance,
		//		maxAngleFilter = 360f
		//	};

		//	search.RefreshCandidates();
		//	search.FilterOutGameObject(characterBody.gameObject);



		//	List<HurtBox> target = search.GetResults().ToList<HurtBox>();
		//	foreach (HurtBox singularTarget in target)
		//	{
		//		if (singularTarget)
		//		{
		//			Vector3 a = singularTarget.transform.position - characterBody.corePosition;
		//			float magnitude = a.magnitude;
		//			Vector3 vector = a / magnitude;
		//			if (singularTarget.healthComponent && singularTarget.healthComponent.body)
		//			{
		//				float Weight = 1f;
		//				if (singularTarget.healthComponent.body.characterMotor)
		//				{
		//					Weight = singularTarget.healthComponent.body.characterMotor.mass;
		//				}
		//				else if (singularTarget.healthComponent.body.rigidbody)
		//				{
		//					Weight = singularTarget.healthComponent.body.rigidbody.mass;
		//				}
		//				Vector3 a2 = vector;
		//				float d = Trajectory.CalculateInitialYSpeedForHeight(Mathf.Abs(StaticValues.voidjailerminpullDistance - magnitude)) * Mathf.Sign(StaticValues.voidjailerminpullDistance - magnitude);
		//				a2 *= d;
		//				a2.y = StaticValues.voidjailerpullLiftVelocity;
		//				DamageInfo damageInfo = new DamageInfo
		//				{
		//					attacker = base.gameObject,
		//					damage = characterBody.damage * Modules.StaticValues.voidjailerDamageCoefficient,
		//					position = singularTarget.transform.position,
		//					procCoefficient = 0.5f,
		//					damageType = damageType2,

		//				};
		//				singularTarget.healthComponent.TakeDamageForce(a2 * (Weight / 2), true, true);
		//				singularTarget.healthComponent.TakeDamage(damageInfo);
		//				GlobalEventManager.instance.OnHitEnemy(damageInfo, singularTarget.healthComponent.gameObject);


  //                      EffectManager.SpawnEffect(Modules.Assets.voidjailerEffect, new EffectData
  //                      {
  //                          origin = singularTarget.transform.position,
  //                          scale = 1f,

  //                      }, true);

		//				Vector3 position = singularTarget.transform.position;
		//				Vector3 start = characterBody.corePosition;
		//				Transform transform = child.FindChild("LHand").transform;
		//				if (transform)
		//				{
		//					start = transform.position;
		//				}
		//				EffectData effectData = new EffectData
		//				{
		//					origin = position,
		//					start = start
		//				};
		//				EffectManager.SpawnEffect(Modules.Assets.voidjailermuzzleEffect, effectData, true);
						
		//			}
		//		}
		//	}
		//}

        public void Update()
        {
            //update mortar indicator
            //if (this.mortarIndicatorInstance) this.UpdateIndicator();

            if (!informAFOToPlayers)
            {
                informAFOToPlayers = true;
                Chat.AddMessage($"Press the [{Config.AFOHotkey.Value}] key to use <style=cIsUtility>All For One and steal quirks</style>."
                + $" Press the [{Config.RemoveHotkey.Value}] key to <style=cIsUtility>remove quirks</style>.");
            }


            //steal quirk

            if (trackingTarget)
            {
                if (Config.AFOHotkey.Value.IsDown() && characterBody.hasEffectiveAuthority)
                {
                    stopwatch += Time.deltaTime;
                    if (!this.hasStolen && stopwatch > Config.holdButtonAFO.Value)
                    {
                        hasStolen = true;
                        Debug.Log("Target");
                        Debug.Log(BodyCatalog.FindBodyPrefab(BodyCatalog.GetBodyName(trackingTarget.healthComponent.body.bodyIndex)));
                        AkSoundEngine.PostEvent(1719197672, this.gameObject);
                        StealQuirk(trackingTarget);

                    }

                    Debug.Log(hasStolen + "hasstolen");

                }
                else if (Config.AFOHotkey.Value.IsUp() && characterBody.hasEffectiveAuthority)
                {
                    hasStolen = false;
                    hasQuirk = false;
                    stopwatch = 0f;
                }
            }
            
            //remove quirk
            if (Config.RemoveHotkey.Value.IsDown() && characterBody.hasEffectiveAuthority)
            {

                stopwatch2 += Time.deltaTime;
                if (!this.hasRemoved && stopwatch2 > Config.holdButtonAFO.Value)
                {
                    hasRemoved = true;

                    //choose what quirk to remove
                    Chat.AddMessage("<style=cIsUtility>Choose which Quirk to Remove</style>");
                    //unset skills but not to unwrite the skilllist
                    //RemovePrimary();
                    //RemoveSecondary();
                    //RemoveUtility();
                    //RemoveSpecial();
                    //RemoveExtra1();
                    //RemoveExtra2();
                    //RemoveExtra3();
                    //RemoveExtra4();

                    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggymastercon.skillListToOverrideOnRespawn[0], GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggymastercon.skillListToOverrideOnRespawn[1], GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggymastercon.skillListToOverrideOnRespawn[2], GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggymastercon.skillListToOverrideOnRespawn[3], GenericSkill.SkillOverridePriority.Contextual);
                    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggymastercon.skillListToOverrideOnRespawn[4], GenericSkill.SkillOverridePriority.Contextual);
                    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggymastercon.skillListToOverrideOnRespawn[5], GenericSkill.SkillOverridePriority.Contextual);
                    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggymastercon.skillListToOverrideOnRespawn[6], GenericSkill.SkillOverridePriority.Contextual);
                    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggymastercon.skillListToOverrideOnRespawn[7], GenericSkill.SkillOverridePriority.Contextual);


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
        
     

  //      private void UpdateIndicator()
		//{
		//	if (this.mortarIndicatorInstance)
		//	{
		//		float maxDistance = 250f;

		//		this.downRay = new Ray
		//		{
		//			direction = Vector3.down,
		//			origin = base.transform.position
		//		};

		//		RaycastHit raycastHit;
		//		if (Physics.Raycast(this.downRay, out raycastHit, maxDistance, LayerIndex.world.mask))
		//		{
		//			this.mortarIndicatorInstance.transform.position = raycastHit.point;
		//			this.mortarIndicatorInstance.transform.up = raycastHit.normal;
		//			mortarIndicatorInstance.localScale = Vector3.one * Modules.StaticValues.mortarRadius * (characterBody.armor/characterBody.baseArmor);

		//		}
		//	}
		//	if (this.voidmortarIndicatorInstance)
		//	{
		//		float maxDistance = 250f;

		//		this.downRay = new Ray
		//		{
		//			direction = Vector3.down,
		//			origin = base.transform.position
		//		};

		//		RaycastHit raycastHit;
		//		if (Physics.Raycast(this.downRay, out raycastHit, maxDistance, LayerIndex.world.mask))
		//		{
		//			this.voidmortarIndicatorInstance.transform.position = raycastHit.point;
		//			this.voidmortarIndicatorInstance.transform.up = raycastHit.normal;
		//			voidmortarIndicatorInstance.localScale = Vector3.one * Modules.StaticValues.voidmortarRadius * characterBody.attackSpeed;

		//		}
		//	}
		//}
		//hermit crab mortar
		//private void CreateMortarIndicator()
		//{
		//	if (EntityStates.Huntress.ArrowRain.areaIndicatorPrefab)
		//	{
		//		this.downRay = new Ray
		//		{
		//			direction = Vector3.down,
		//			origin = base.transform.position
		//		};

		//		mortarIndicatorInstance = UnityEngine.Object.Instantiate<GameObject>(EntityStates.Huntress.ArrowRain.areaIndicatorPrefab).transform;
		//		mortarIndicatorInstance.localScale = Vector3.one * Modules.StaticValues.mortarRadius * characterBody.armor;

		//	}
		//}
		////void barnacle mortar	
		//private void CreateVoidMortarIndicator()
		//{
		//	if (EntityStates.Huntress.ArrowRain.areaIndicatorPrefab)
		//	{
		//		this.downRay = new Ray
		//		{
		//			direction = Vector3.down,
		//			origin = base.transform.position
		//		};

		//		voidmortarIndicatorInstance = UnityEngine.Object.Instantiate<GameObject>(EntityStates.Huntress.ArrowRain.areaIndicatorPrefab).transform;
		//		voidmortarIndicatorInstance.localScale = Vector3.one * Modules.StaticValues.voidmortarRadius * characterBody.attackSpeed;

		//	}
		//}

		////code for both mortars
		//private void FireMortar()
		//{
		//	MortarOrb mortarOrb = new MortarOrb
		//	{
		//		attacker = characterBody.gameObject,
		//		damageColorIndex = DamageColorIndex.WeakPoint,
		//		damageValue = characterBody.damage * Modules.StaticValues.mortarDamageCoefficient * characterBody.attackSpeed * (characterBody.armor/characterBody.baseArmor),
		//		origin = characterBody.corePosition,
		//		procChainMask = default(ProcChainMask),
		//		procCoefficient = 1f,
		//		isCrit = Util.CheckRoll(characterBody.crit, characterBody.master),
		//		teamIndex = characterBody.GetComponent<TeamComponent>()?.teamIndex ?? TeamIndex.Neutral,
		//	};
		//	if (mortarOrb.target = mortarOrb.PickNextTarget(mortarOrb.origin, Modules.StaticValues.mortarRadius * characterBody.attackSpeed * (characterBody.armor/characterBody.baseArmor)))
		//	{
		//		OrbManager.instance.AddOrb(mortarOrb);
		//	}

		//}

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
		//private void OverloadingFire()
		//{
		//	Ray aimRay = new Ray(this.inputBank.aimOrigin, this.inputBank.aimDirection);
		//	BullseyeSearch search = new BullseyeSearch
		//	{

		//		teamMaskFilter = TeamMask.GetEnemyTeams(characterBody.teamComponent.teamIndex),
		//		filterByLoS = false,
		//		searchOrigin = characterBody.corePosition,
		//		searchDirection = UnityEngine.Random.onUnitSphere,
		//		sortMode = BullseyeSearch.SortMode.Distance,
		//		maxDistanceFilter = StaticValues.overloadingRadius,
		//		maxAngleFilter = 360f
		//	};

		//	search.RefreshCandidates();
		//	search.FilterOutGameObject(characterBody.gameObject);

		//	HurtBox target = this.search.GetResults().FirstOrDefault<HurtBox>();
		//	ProcChainMask procChainMask1 = default(ProcChainMask);
		//	procChainMask1.AddProc(ProcType.LightningStrikeOnHit);

		//	OrbManager.instance.AddOrb(new SimpleLightningStrikeOrb
		//	{
		//		attacker = characterBody.gameObject,
		//		damageColorIndex = DamageColorIndex.Item,
		//		damageValue = characterBody.damage * Modules.StaticValues.overloadingCoefficient,
		//		origin = characterBody.corePosition,
		//		procChainMask = procChainMask1,
		//		procCoefficient = 1f,
		//		isCrit = Util.CheckRoll(characterBody.crit, characterBody.master),
		//		teamIndex = characterBody.GetComponent<TeamComponent>()?.teamIndex ?? TeamIndex.Neutral,
		//		target = target,

		//	});

		//}

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

            Debug.Log(name + "name");
            Debug.Log(newbodyPrefab + "newbodyprefab");
            //AkSoundEngine.PostEvent(3192656820, characterBody.gameObject);


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

            if (StaticValues.baseQuirkSkillDef.ContainsKey(newbodyPrefab.name))
            {
                RoR2.Skills.SkillDef skillDef = StaticValues.baseQuirkSkillDef[newbodyPrefab.name];


                Shiggymastercon.writeToAFOSkillList(skillDef, 0);
                Chat.AddMessage(StaticValues.baseQuirkSkillString[skillDef]);

                //var skilldef = Dictionary(alphaconstruct)
                

                
                //if(checked for special(skilldef, Dictionary upgradecheck, secondaryupgrade check1, secondaryupgrade check 2)

                //{
                //    Shiggymastercon.writeToAFOSkillList(supersynergy skill, 0);
                //    Chat.AddMessage("super synergy skill get");
                //}
                //else if(isquirkhave(dictionary(skilldef))
                //{
                //    Shiggymastercon.writeToAFOSkillList(Dictionary of skilldef to upgraded skill, 0);
                //    Chat.AddMessage(Dictionary upgraded skill to gainupgradedquirkstring);

                //}
                //else
                //{
                //    Shiggymastercon.writeToAFOSkillList(skilldef, 0);
                //    Chat.AddMessage(Dictionary skilldef to gainquirkstring);
                //}
            }
            //if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.alphacontructpassiveDef, 0);

            //    characterBody.ApplyBuff(Modules.Buffs.alphashieldonBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "BeetleBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.beetlepassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.beetleBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "BeetleGuardBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Beetle Armor Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.beetleguardslamDef, 0);
            //}
            //if (newbodyPrefab.name == "BisonBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.bisonchargeDef, 0);
            //}
            //if (newbodyPrefab.name == "FlyingVerminBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.pestpassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.pestjumpBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "VerminBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.verminpassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.verminsprintBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "BellBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.bronzongballDef, 0);
            //}
            //if (newbodyPrefab.name == "ClayGrenadierBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.clayapothecarymortarDef, 0);
            //}
            //if (newbodyPrefab.name == "ClayBruiserBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.claytemplarminigunDef, 0);
            //}
            //if (newbodyPrefab.name == "LemurianBruiserBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Fire Blast Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.elderlemurianfireblastDef, 0);
            //}
            //if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.guppassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.gupspikeBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "GreaterWispBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Spirit Boost Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.greaterwispballDef, 0);
            //}
            //if (newbodyPrefab.name == "HermitCrabBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.hermitcrabpassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortarBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "ImpBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.impblinkDef, 0);
            //}
            //if (newbodyPrefab.name == "JellyfishBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.jellyfishHealDef, 0);
            //}
            //if (newbodyPrefab.name == "AcidLarvaBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.larvapassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.larvajumpBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "LemurianBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.lemurianfireballDef, 0);
            //}
            //if (newbodyPrefab.name == "WispBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.lesserwisppassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.lesserwispBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "LunarExploderBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.lunarexploderpassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.lunarexploderBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "LunarGolemBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.lunargolemSlideDef, 0);
            //}
            //if (newbodyPrefab.name == "LunarWispBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.lunarwispminigunDef, 0);
            //}
            //if (newbodyPrefab.name == "MiniMushroomBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.minimushrumpassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.minimushrumBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "ParentBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.parentteleportDef, 0);
            //}
            //if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Solus Boost Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.roboballminibpassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.roboballminiBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "GolemBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.stonegolemlaserDef, 0);
            //}
            //if (newbodyPrefab.name == "VoidBarnacleBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.voidbarnaclepassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "VoidJailerBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.voidjailerpassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.voidjailerBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "NullifierBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.voidreaverportalDef, 0);
            //}
            //if (newbodyPrefab.name == "BeetleQueen2Body")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.beetlequeenSummonDef, 0);
            //}
            //if (newbodyPrefab.name == "ImpBossBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Bleed Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.impbosspassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.impbossBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.stonetitanpassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.stonetitanBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "GrandParentBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Solar Flare Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.grandparentsunDef, 0);
            //}
            //if (newbodyPrefab.name == "GravekeeperBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.grovetenderChainDef, 0);
            //}
            //if (newbodyPrefab.name == "VagrantBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.vagrantpassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.vagrantBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "MagmaWormBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.magmawormpassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.magmawormBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "ElectricWormBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.overloadingwormpassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.overloadingwormBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "ClayBossBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Tar Boost Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.claydunestriderbuffDef, 0);
            //}
            //if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.soluscontrolunityknockupDef, 0);
            //}
            //if (newbodyPrefab.name == "MegaConstructBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.xiconstructbeamDef, 0);
            //}
            //if (newbodyPrefab.name == "VoidMegaCrabBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.voiddevastatorhomingDef, 0);
            //}
            //if (newbodyPrefab.name == "ScavBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.scavengerthqwibDef, 0);
            //}

            //if (newbodyPrefab.name == "Bandit2Body")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Lights Out Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.banditlightsoutDef, 0);
            //}

            //if (newbodyPrefab.name == "CaptainBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Defensive Microbots Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.captainpassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.captainBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "CommandoBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Double Tap Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.commandopassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.commandoBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "CrocoBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Poison Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.acridpassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.acridBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "EngiBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Turret Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.engiturretDef, 0);
            //}
            ////if (newbodyPrefab.name == "HereticBody")
            ////{

            ////    
            ////    Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

            ////    Shiggymastercon.writeToAFOSkillList(Shiggy.scavengerthqwibDef, 0);
            ////    RemovePrimary();
            ////    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
            ////}
            //if (newbodyPrefab.name == "HuntressBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Flurry Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.huntressattackDef, 0);
            //}
            //if (newbodyPrefab.name == "LoaderBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Scrap Barrier Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.loaderpassiveDef, 0);
            //    characterBody.ApplyBuff(Modules.Buffs.loaderBuff.buffIndex);
            //}
            //if (newbodyPrefab.name == "MageBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Elementality Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.artificerflamethrowerDef, 0);
            //}
            //if (newbodyPrefab.name == "MercBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Eviscerate Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.mercdashDef, 0);
            //}
            //if (newbodyPrefab.name == "ToolbotBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Power Stance Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.multbuffDef, 0);
            //}
            //if (newbodyPrefab.name == "TreebotBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Seed Barrage Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.rexmortarDef, 0);
            //}
            //if (newbodyPrefab.name == "RailgunnerBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Cryocharged Railgun Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.railgunnercryoDef, 0);
            //}
            //if (newbodyPrefab.name == "VoidSurvivorBody")
            //{

                
            //    Chat.AddMessage("<style=cIsUtility>Cleanse Quirk</style> Get!");

            //    Shiggymastercon.writeToAFOSkillList(Shiggy.voidfiendcleanseDef, 0);
            //}
            if (Shiggymastercon.storedAFOSkill[0] != null)
            {
                //override skills to choosdef
                //RemovePrimary();
                //RemoveSecondary();
                //RemoveUtility();
                //RemoveSpecial();
                //RemoveExtra1();
                //RemoveExtra2();
                //RemoveExtra3();
                //RemoveExtra4();


                characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggymastercon.skillListToOverrideOnRespawn[0], GenericSkill.SkillOverridePriority.Contextual);
                characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggymastercon.skillListToOverrideOnRespawn[1], GenericSkill.SkillOverridePriority.Contextual);
                characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggymastercon.skillListToOverrideOnRespawn[2], GenericSkill.SkillOverridePriority.Contextual);
                characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggymastercon.skillListToOverrideOnRespawn[3], GenericSkill.SkillOverridePriority.Contextual);
                extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggymastercon.skillListToOverrideOnRespawn[4], GenericSkill.SkillOverridePriority.Contextual);
                extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggymastercon.skillListToOverrideOnRespawn[5], GenericSkill.SkillOverridePriority.Contextual);
                extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggymastercon.skillListToOverrideOnRespawn[6], GenericSkill.SkillOverridePriority.Contextual);
                extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggymastercon.skillListToOverrideOnRespawn[7], GenericSkill.SkillOverridePriority.Contextual);

                characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
                characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
                characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
                characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);

                extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
                extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
                extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
                extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            else if (Shiggymastercon.storedAFOSkill[0] == null)
            {
                Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
            }
            
        }


        //public void RemoveExtra1()
        //{
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.acridpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.commandopassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.captainpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.loaderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

        //}

        //public void RemoveExtra2()
        //{
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.acridpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.commandopassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.captainpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.loaderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

        //}

        //public void RemoveExtra3()
        //{
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.acridpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.commandopassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.captainpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.loaderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

        //}

        //public void RemoveExtra4()
        //{
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.acridpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.commandopassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.captainpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.loaderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

        //}

        //public void RemovePrimary()
        //{
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.alloyvultureWindBlastDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.jellyfishHealDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.lunargolemSlideDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.beetlequeenSummonDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.grovetenderChainDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.claydunestriderbuffDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.elderlemurianfireblastDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.banditlightsoutDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.engiturretDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.huntressattackDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.mercdashDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.railgunnercryoDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.rexmortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.voidfiendcleanseDef, GenericSkill.SkillOverridePriority.Contextual);

        //}
        //public void RemoveSecondary()
        //{
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.alloyvultureWindBlastDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.jellyfishHealDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lunargolemSlideDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.beetlequeenSummonDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.grovetenderChainDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.claydunestriderbuffDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.elderlemurianfireblastDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.banditlightsoutDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.engiturretDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.huntressattackDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.mercdashDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.railgunnercryoDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.rexmortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voidfiendcleanseDef, GenericSkill.SkillOverridePriority.Contextual);

        //}

        //public void RemoveUtility()
        //{
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.alloyvultureWindBlastDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.jellyfishHealDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.lunargolemSlideDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.beetlequeenSummonDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.grovetenderChainDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.claydunestriderbuffDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.elderlemurianfireblastDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.banditlightsoutDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.engiturretDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.huntressattackDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.mercdashDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.railgunnercryoDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.rexmortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.voidfiendcleanseDef, GenericSkill.SkillOverridePriority.Contextual);

        //}

        //public void RemoveSpecial()
        //{
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.alloyvultureWindBlastDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.jellyfishHealDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.lunargolemSlideDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.beetlequeenSummonDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.grovetenderChainDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.claydunestriderbuffDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.elderlemurianfireblastDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.banditlightsoutDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.engiturretDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.huntressattackDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.mercdashDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.railgunnercryoDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.rexmortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.voidfiendcleanseDef, GenericSkill.SkillOverridePriority.Contextual);

        //}



    }




    //mortar orb
 //   public class MortarOrb : Orb
	//{
	//	public override void Begin()
	//	{
	//		base.duration = 0.5f;
	//		EffectData effectData = new EffectData
	//		{
	//			origin = this.origin,
	//			genericFloat = base.duration
	//		};
	//		effectData.SetHurtBoxReference(this.target);
 //           GameObject effectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/SquidOrbEffect");
 //           EffectManager.SpawnEffect(effectPrefab, effectData, true);
	//	}
	//	public HurtBox PickNextTarget(Vector3 position, float range)
	//	{
	//		BullseyeSearch bullseyeSearch = new BullseyeSearch();
	//		bullseyeSearch.searchOrigin = position;
	//		bullseyeSearch.searchDirection = Vector3.zero;
	//		bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
	//		bullseyeSearch.teamMaskFilter.RemoveTeam(this.teamIndex);
	//		bullseyeSearch.filterByLoS = false;
	//		bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
	//		bullseyeSearch.maxDistanceFilter = range;
	//		bullseyeSearch.RefreshCandidates();
	//		List<HurtBox> list = bullseyeSearch.GetResults().ToList<HurtBox>();
	//		if (list.Count <= 0)
	//		{
	//			return null;
	//		}
	//		return list[UnityEngine.Random.Range(0, list.Count)];
	//	}
	//	public override void OnArrival()
	//	{
	//		if (this.target)
	//		{
	//			HealthComponent healthComponent = this.target.healthComponent;
	//			if (healthComponent)
	//			{
	//				DamageInfo damageInfo = new DamageInfo
	//				{
	//					damage = this.damageValue,
	//					attacker = this.attacker,
	//					inflictor = null,
	//					force = Vector3.zero,
	//					crit = this.isCrit,
	//					procChainMask = this.procChainMask,
	//					procCoefficient = this.procCoefficient,
	//					position = this.target.transform.position,
	//					damageColorIndex = this.damageColorIndex
	//				};
	//				healthComponent.TakeDamage(damageInfo);
	//				GlobalEventManager.instance.OnHitEnemy(damageInfo, healthComponent.gameObject);
	//				GlobalEventManager.instance.OnHitAll(damageInfo, healthComponent.gameObject);
	//			}
	//		}
	//	}

	//	public float damageValue;
	//	public GameObject attacker;
	//	public TeamIndex teamIndex;
	//	public bool isCrit;
	//	public ProcChainMask procChainMask;
	//	public float procCoefficient = 1f;
	//	public DamageColorIndex damageColorIndex;
	//}


}


