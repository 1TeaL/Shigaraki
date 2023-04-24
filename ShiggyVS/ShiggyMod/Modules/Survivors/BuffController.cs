using EntityStates;
using R2API;
using RoR2;
using RoR2.Orbs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EntityStates.MiniMushroom;
using UnityEngine.Networking;
using ExtraSkillSlots;
using R2API.Networking;
using ShiggyMod.Modules.Networking;
using R2API.Networking.Interfaces;
using static UnityEngine.ParticleSystem.PlaybackState;
using HG;
using RoR2.Projectile;
using RoR2.Items;
using System;
using ShiggyMod.SkillStates;
using On.EntityStates.Huntress;
using Object = UnityEngine.Object;

namespace ShiggyMod.Modules.Survivors
{
    public class BuffController : MonoBehaviour
	{

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
        private float gachaBuffThreshold;
        private float omniboostTimer;
        private float stoneFormTimer;
        private float stoneformStillbuffTimer;
        private float auraOfBlightTimer;
        private float barbedSpikesTimer;
        private float doubleTimeTimer;
        private float doubleTimeStacksTimer;

        private Ray downRay;
		public GameObject mortarIndicatorInstance;
        public GameObject voidmortarIndicatorInstance;
        public GameObject barbedSpikesIndicatorInstance;

        public HurtBox Target;

        private CharacterBody characterBody;
		private InputBankTest inputBank;
		private readonly BullseyeSearch search = new BullseyeSearch();
		private CharacterMaster characterMaster;

		public ShiggyMasterController Shiggymastercon;
		public ShiggyController Shiggycon;
        private EnergySystem energySystem;
        private ExtraInputBankTest extrainputBankTest;
        private ExtraSkillLocator extraskillLocator;

        public bool larvabuffGiven;
		public bool verminjumpbuffGiven;
        private uint minimushrumsoundID;
        public GameObject mushroomWard;
		public GameObject magmawormWard;
		public GameObject overloadingWard;

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

        public int captainitemcount;
        private DamageType damageType;
        private DamageType damageType2;

        public void Awake()
        {
	
			//On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;

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

        }


        public void Start()
        {
            characterMaster = gameObject.GetComponent<CharacterMaster>();
            characterBody = characterMaster.GetBody();
            inputBank = gameObject.GetComponent<InputBankTest>();

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

        }

		public void OnDestroy()
        {
            if (mortarIndicatorInstance)
            {
                mortarIndicatorInstance.SetActive(false);
                EntityState.Destroy(mortarIndicatorInstance.gameObject);
            }
            if (voidmortarIndicatorInstance)
            {
                voidmortarIndicatorInstance.SetActive(false);
                EntityState.Destroy(voidmortarIndicatorInstance.gameObject);
            }
            if (barbedSpikesIndicatorInstance)
            {
                barbedSpikesIndicatorInstance.SetActive(false);
                EntityState.Destroy(barbedSpikesIndicatorInstance.gameObject);
            }
            if (mushroomWard) EntityState.Destroy (mushroomWard.gameObject);
            if (magmawormWard) EntityState.Destroy(magmawormWard);

        }

        public void FixedUpdate()
        {

			if (characterBody.hasEffectiveAuthority)
            {
                //Buff effects

                //double time slow effect
                if(characterBody.HasBuff(Buffs.doubleTimeBuff))
                {
                    if(doubleTimeTimer < 1f)
                    {
                        doubleTimeTimer += Time.fixedDeltaTime;
                    }
                    else if (doubleTimeTimer >= 1f)
                    {
                        ApplyDoubleTimeDebuff();
                        doubleTimeTimer = 0f;
                    }
                }
                //double time buff removal
                if (characterBody.HasBuff(Buffs.doubleTimeBuffStacks))
                {
                    if (doubleTimeStacksTimer < StaticValues.doubleTimeThreshold)
                    {
                        doubleTimeStacksTimer += Time.fixedDeltaTime;
                    }
                    else if (doubleTimeStacksTimer >= StaticValues.doubleTimeThreshold)
                    {
                        doubleTimeStacksTimer = 0f;
                        int doubleTimeStacksBuffcount = characterBody.GetBuffCount(Buffs.doubleTimeBuffStacks);
                        characterBody.ApplyBuff(Buffs.doubleTimeBuffStacks.buffIndex, Mathf.RoundToInt(doubleTimeStacksBuffcount/2));
                    }
                }

                if (characterBody.HasBuff(Buffs.barbedSpikesBuff))
                {
                    if (!barbedSpikesIndicatorInstance)
                    {
                        CreateBarbedSpikesIndicator();
                    }

                    if (barbedSpikesTimer < StaticValues.auraOfBlightBuffThreshold)
                    {
                        barbedSpikesTimer += Time.fixedDeltaTime;
                    }
                    else if (barbedSpikesTimer >= StaticValues.auraOfBlightBuffThreshold)
                    {
                        barbedSpikesTimer = 0f;
                        BullseyeSearch search = new BullseyeSearch
                        {

                            teamMaskFilter = TeamMask.GetEnemyTeams(characterBody.teamComponent.teamIndex),
                            filterByLoS = false,
                            searchOrigin = characterBody.corePosition,
                            searchDirection = UnityEngine.Random.onUnitSphere,
                            sortMode = BullseyeSearch.SortMode.Distance,
                            maxDistanceFilter = StaticValues.barbedSpikesRadius,
                            maxAngleFilter = 360f
                        };

                        search.RefreshCandidates();
                        search.FilterOutGameObject(characterBody.gameObject);

                        List<HurtBox> target = search.GetResults().ToList<HurtBox>();
                        foreach (HurtBox singularTarget in target)
                        {
                            if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                            {
                                LightningOrb lightningOrb = new LightningOrb();
                                lightningOrb.attacker = characterBody.gameObject;
                                lightningOrb.bouncedObjects = null;
                                lightningOrb.bouncesRemaining = 0;
                                lightningOrb.damageCoefficientPerBounce = 1f;
                                lightningOrb.damageColorIndex = DamageColorIndex.Item;
                                lightningOrb.damageValue = StaticValues.barbedSpikesDamageCoefficient * characterBody.damage;
                                lightningOrb.isCrit = characterBody.RollCrit();
                                lightningOrb.lightningType = LightningOrb.LightningType.RazorWire;
                                lightningOrb.origin = characterBody.corePosition;
                                lightningOrb.procChainMask = default(ProcChainMask);
                                lightningOrb.procChainMask.AddProc(ProcType.Thorns);
                                lightningOrb.procCoefficient = StaticValues.barbedSpikesProcCoefficient;
                                lightningOrb.range = 0f;
                                lightningOrb.teamIndex = characterBody.teamComponent.teamIndex;
                                lightningOrb.target = singularTarget;
                                OrbManager.instance.AddOrb(lightningOrb);
                                
                            }
                        }
                    }
                }
                else if (!characterBody.HasBuff(Buffs.barbedSpikesBuff))
                {
                    if (barbedSpikesIndicatorInstance)
                    {
                        barbedSpikesIndicatorInstance.SetActive(false);
                        EntityState.Destroy(barbedSpikesIndicatorInstance.gameObject);
                    }
                }

                if (characterBody.HasBuff(Buffs.auraOfBlightBuff))
                {
                    if(auraOfBlightTimer < StaticValues.auraOfBlightBuffThreshold)
                    {
                        auraOfBlightTimer += Time.fixedDeltaTime;
                    }
                    else if (auraOfBlightTimer >= StaticValues.auraOfBlightBuffThreshold)
                    {
                        auraOfBlightTimer = 0f;
                        ApplyBlight();

                    }
                }

                //gacha buff timer
                if(characterBody.HasBuff(Buffs.gachaBuff))
                {
                    int gachaBuffcount = characterBody.GetBuffCount(Buffs.gachaBuff);

                    if (gachaBuffcount < StaticValues.gachaBuffThreshold)
                    {
                        if(gachaBuffThreshold < 1f)
                        {
                            gachaBuffThreshold += Time.fixedDeltaTime;
                        }
                        else if (gachaBuffThreshold >= 1f)
                        {
                            characterBody.ApplyBuff(Buffs.gachaBuff.buffIndex, gachaBuffcount + 1);
                            gachaBuffThreshold = 0f;   
                        }
                        
                    }
                    else if (gachaBuffcount >= StaticValues.gachaBuffThreshold)
                    {
                        new ItemDropNetworked(characterBody.masterObjectId).Send(NetworkDestination.Clients);
                        characterBody.ApplyBuff(Buffs.gachaBuff.buffIndex, 1);

                        EffectManager.SpawnEffect(Modules.Assets.scavSackEffect, new EffectData
                        {
                            origin = characterBody.transform.position,
                            scale = 1f,
                            rotation = Quaternion.LookRotation(characterBody.characterDirection.forward)

                        }, false);
                    }
                }
                //gacha buff timer
                if (characterBody.HasBuff(Buffs.omniboostBuffStacks))
                {
                    if (omniboostTimer < StaticValues.omniboostBuffTimer)
                    {
                        omniboostTimer += Time.fixedDeltaTime;
                    }
                    else if (omniboostTimer >= StaticValues.omniboostBuffTimer)
                    {
                        omniboostTimer = 0f;
                        int omniboostBuffcount = characterBody.GetBuffCount(Buffs.omniboostBuffStacks);
                        characterBody.ApplyBuff(Buffs.omniboostBuffStacks.buffIndex, Mathf.RoundToInt(omniboostBuffcount/2));

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

                //roboballmini buff
                if (characterBody.HasBuff(Modules.Buffs.roboballminiBuff.buffIndex))
                {
                    if (extrainputBankTest)
                    {
                        if (characterBody.inputBank.skill1.down
                            | characterBody.inputBank.skill2.down
                            | characterBody.inputBank.skill3.down
                            | characterBody.inputBank.skill4.down
                            | extrainputBankTest.extraSkill1.down
                            | extrainputBankTest.extraSkill2.down
                            | extrainputBankTest.extraSkill3.down
                            | extrainputBankTest.extraSkill4.down)
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
                            && !characterBody.inputBank.skill1.down
                            && !extrainputBankTest.extraSkill1.down
                            && !extrainputBankTest.extraSkill2.down
                            && !extrainputBankTest.extraSkill3.down
                            && !extrainputBankTest.extraSkill4.down)
                        {
                            characterBody.ApplyBuff(Modules.Buffs.roboballminiattackspeedBuff.buffIndex, 0);
                        }

                    }
                    else if (!extrainputBankTest)
                    {
                        if (characterBody.inputBank.skill1.down
                            | characterBody.inputBank.skill2.down
                            | characterBody.inputBank.skill3.down
                            | characterBody.inputBank.skill4.down)
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

                //stoneform buff still effect
                if (characterBody.HasBuff(Buffs.stoneFormStillBuff.buffIndex))
                {
                    if(stoneformStillbuffTimer < 1f)
                    {
                        stoneformStillbuffTimer += Time.fixedDeltaTime;
                    }
                    else if (stoneformStillbuffTimer >= 1f)
                    {
                        stoneformStillbuffTimer = 0f;

                        EffectManager.SpawnEffect(Assets.titanClapEffect, new EffectData
                        {
                            origin = characterBody.transform.position,
                            scale = 1f,
                            rotation = Quaternion.identity,

                        }, true);
                    }
                }

                //Standing still/not moving buffs
                if (characterBody.GetNotMoving())
                {
                    //stoneform buff
                    if (characterBody.HasBuff(Modules.Buffs.stoneFormBuff.buffIndex))
                    {
                        if(stoneFormTimer < StaticValues.stoneFormWaitDuration)
                        {
                            stoneFormTimer += Time.fixedDeltaTime;
                        }
                        else if (stoneFormTimer >= StaticValues.stoneFormWaitDuration)
                        {
                            if (!characterBody.HasBuff(Buffs.stoneFormStillBuff.buffIndex))
                            {
                                EffectManager.SpawnEffect(Assets.stonetitanFistEffect, new EffectData
                                {
                                    origin = characterBody.transform.position,
                                    scale = 1f,
                                    rotation = Quaternion.identity,

                                }, true);
                                characterBody.ApplyBuff(Buffs.stoneFormStillBuff.buffIndex, 1);                                
                            }
                        }
                    }

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
                }//moving buffs
                else if (!characterBody.GetNotMoving())
                {
                    stoneFormTimer = 0f;
                    if (characterBody.HasBuff(Buffs.stoneFormStillBuff.buffIndex))
                    {
                        characterBody.ApplyBuff(Buffs.stoneFormStillBuff.buffIndex, 0);
                    }

                    if (mortarIndicatorInstance)
                    {
                        characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortararmorBuff.buffIndex, 0);
                        mortarIndicatorInstance.SetActive(false);
                        EntityState.Destroy(mortarIndicatorInstance.gameObject);
                    }
                    if (voidmortarIndicatorInstance)
                    {
                        characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarattackspeedBuff.buffIndex, 0);
                        voidmortarIndicatorInstance.SetActive(false);
                        EntityState.Destroy(voidmortarIndicatorInstance.gameObject);
                    }

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
                        blastAttack.baseDamage = characterBody.damage * Modules.StaticValues.larvaDamageCoefficient * (characterBody.jumpPower / 5);
                        blastAttack.falloffModel = BlastAttack.FalloffModel.None;
                        blastAttack.baseForce = Modules.StaticValues.larvaForce;
                        blastAttack.teamIndex = characterBody.teamComponent.teamIndex;
                        blastAttack.damageType = damageType;
                        blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
                        blastAttack.AddModdedDamageType(Modules.Damage.shiggyDecay);
                        blastAttack.Fire();


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
                        blastAttack.AddModdedDamageType(Modules.Damage.shiggyDecay);
                        blastAttack.Fire();
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
				if (singularTarget.healthComponent)
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

        public void ApplyDoubleTimeDebuff()
        {
            BullseyeSearch search = new BullseyeSearch
            {

                teamMaskFilter = TeamMask.GetEnemyTeams(TeamIndex.Player),
                filterByLoS = false,
                searchOrigin = characterBody.corePosition,
                searchDirection = UnityEngine.Random.onUnitSphere,
                sortMode = BullseyeSearch.SortMode.Distance,
                maxDistanceFilter = Modules.StaticValues.doubleTimeRadius,
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
                        singularTarget.healthComponent.body.ApplyBuff(Buffs.doubleTimeDebuff.buffIndex, 1, 2);
                    }


                    EffectManager.SpawnEffect(EntityStates.Commando.CommandoWeapon.FireShotgun.hitEffectPrefab, new EffectData
                    {
                        origin = singularTarget.transform.position,
                        scale = 1f,
                        rotation = Quaternion.identity

                    }, true);

                }
            }
        }

        public void ApplyBlight()
        {
            BullseyeSearch search = new BullseyeSearch
            {

                teamMaskFilter = TeamMask.GetEnemyTeams(TeamIndex.Player),
                filterByLoS = false,
                searchOrigin = characterBody.corePosition,
                searchDirection = UnityEngine.Random.onUnitSphere,
                sortMode = BullseyeSearch.SortMode.Distance,
                maxDistanceFilter = Modules.StaticValues.auraOfBlightBuffRadius,
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
                        info.duration = StaticValues.auraOfBlightBuffDotDuration;
                        info.totalDamage = StaticValues.auraOfBlightBuffDotDamage;
                        info.dotIndex = DotController.DotIndex.Blight;

                        DotController.InflictDot(ref info);
                    }


                    EffectManager.SpawnEffect(EntityStates.Croco.Disease.muzzleflashEffectPrefab, new EffectData
                    {
                        origin = singularTarget.transform.position,
                        scale = 1f,
                        rotation = Quaternion.identity

                    }, true);

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
							damage = characterBody.damage * Modules.StaticValues.voidjailerDamageCoefficient,
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
						Transform transform = characterBody.transform;
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

        public void Update()
        {
            //update mortar indicator, voidmortar and barbed spikes
            UpdateIndicator();

        }
        
     

        private void UpdateIndicator()
        {
            if (this.barbedSpikesIndicatorInstance)
            {
                this.barbedSpikesIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.barbedSpikesRadius * (characterBody.damage / characterBody.baseDamage);
                this.barbedSpikesIndicatorInstance.transform.localPosition = characterBody.corePosition;
            }
            if (this.mortarIndicatorInstance)
            {
                this.mortarIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.barbedSpikesRadius * (characterBody.armor / characterBody.baseArmor);
                this.mortarIndicatorInstance.transform.localPosition = characterBody.corePosition;

			}
			if (this.voidmortarIndicatorInstance)
            {
                this.voidmortarIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.barbedSpikesRadius * (characterBody.attackSpeed);
                this.voidmortarIndicatorInstance.transform.localPosition = characterBody.corePosition;
			}
        }
        //barbed spikes indicator
        private void CreateBarbedSpikesIndicator()
        {
            if (EntityStates.Huntress.ArrowRain.areaIndicatorPrefab)
            {
                this.barbedSpikesIndicatorInstance = Object.Instantiate<GameObject>(EntityStates.Huntress.ArrowRain.areaIndicatorPrefab);
                this.barbedSpikesIndicatorInstance.SetActive(true);
                
                this.barbedSpikesIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.barbedSpikesRadius * (characterBody.damage / characterBody.baseDamage);
                this.barbedSpikesIndicatorInstance.transform.localPosition = characterBody.corePosition;

            }
        }
        //hermit crab mortar
        private void CreateMortarIndicator()
		{
			if (EntityStates.Huntress.ArrowRain.areaIndicatorPrefab)
            {
                this.mortarIndicatorInstance = Object.Instantiate<GameObject>(EntityStates.Huntress.ArrowRain.areaIndicatorPrefab);
                this.mortarIndicatorInstance.SetActive(true);
                this.mortarIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.barbedSpikesRadius * (characterBody.armor / characterBody.baseArmor);
                this.mortarIndicatorInstance.transform.localPosition = characterBody.corePosition;

            }
		}
		//void barnacle mortar	
		private void CreateVoidMortarIndicator()
		{
			if (EntityStates.Huntress.ArrowRain.areaIndicatorPrefab)
            {
                this.voidmortarIndicatorInstance = Object.Instantiate<GameObject>(EntityStates.Huntress.ArrowRain.areaIndicatorPrefab);
                this.voidmortarIndicatorInstance.SetActive(true);

                this.voidmortarIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.barbedSpikesRadius * (characterBody.attackSpeed);
                this.voidmortarIndicatorInstance.transform.localPosition = characterBody.corePosition;

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

            EffectManager.SpawnEffect(EntityStates.HermitCrab.FireMortar.mortarMuzzleflashEffect, new EffectData
            {
                origin = characterBody.gameObject.transform.position,
                scale = 1f,
                rotation = Util.QuaternionSafeLookRotation(mortarOrb.target.transform.position - characterBody.gameObject.transform.position),

            }, true);

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


