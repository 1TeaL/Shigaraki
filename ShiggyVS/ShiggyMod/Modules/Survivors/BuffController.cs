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
using EntityStates.VoidMegaCrab.BackWeapon;

namespace ShiggyMod.Modules.Survivors
{
    public class BuffController : MonoBehaviour
    {
        private float OFAFOTimeMultiplier;

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
        private float reversalTimer;
        private float machineFormTimer;
        private float weatherReportTimer;

        private Ray downRay;
        public GameObject doubleTimeIndicatorInstance;
        public GameObject auraOfBlightIndicatorInstance;
		public GameObject mortarIndicatorInstance;
        public GameObject voidmortarIndicatorInstance;
        public GameObject barbedSpikesIndicatorInstance;
        private GameObject weatherReportIndicatorInstance;

        public HurtBox Target;

        private CharacterBody characterBody;
		private InputBankTest inputBank;
		private readonly BullseyeSearch search = new BullseyeSearch();
		private CharacterMaster characterMaster;

        public NetworkInstanceId networkInstanceID;
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


        public int captainitemcount;


        public void Awake()
        {
	
			//On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;


        }


        public void Start()
        {
            characterBody = gameObject.GetComponent<CharacterBody>();
            characterMaster = characterBody.master;

            inputBank = gameObject.GetComponent<InputBankTest>();

            extraskillLocator = gameObject.GetComponent<ExtraSkillLocator>();
            extrainputBankTest = gameObject.GetComponent<ExtraInputBankTest>();


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
            if (auraOfBlightIndicatorInstance)
            {
                auraOfBlightIndicatorInstance.SetActive(false);
                EntityState.Destroy(auraOfBlightIndicatorInstance.gameObject);
            }
            if (doubleTimeIndicatorInstance)
            {
                doubleTimeIndicatorInstance.SetActive(false);
                EntityState.Destroy(doubleTimeIndicatorInstance.gameObject);
            }
            if (weatherReportIndicatorInstance)
            {
                weatherReportIndicatorInstance.SetActive(false);
                EntityState.Destroy(weatherReportIndicatorInstance.gameObject);
            }
            if (mushroomWard) EntityState.Destroy (mushroomWard.gameObject);
            if (magmawormWard) EntityState.Destroy(magmawormWard);

        }

        public void OFAFO()
        {

            //ofafo time multiplier- put it here only for shiggy, not for others
            if (characterBody.HasBuff(Buffs.OFAFOBuff))
            {
                OFAFOTimeMultiplier = StaticValues.OFAFOTimeMultiplierCoefficient;
            }
            else
            if (!characterBody.HasBuff(Buffs.OFAFOBuff))
            {
                OFAFOTimeMultiplier = 1f;
            }
        }

        public void WeatherReport()
        {
            //weather report buff
            if (characterBody.HasBuff(Buffs.weatherReportBuff))
            {
                if (!weatherReportIndicatorInstance)
                {
                    CreateWeatherReportIndicator();
                }
                if (weatherReportTimer <= StaticValues.weatherReportThreshold)
                {
                    weatherReportTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                }
                else if (weatherReportTimer > StaticValues.weatherReportThreshold)
                {
                    weatherReportTimer = 0f;

                    //randomly hit enemies with different effects
                    BullseyeSearch search = new BullseyeSearch
                    {

                        teamMaskFilter = TeamMask.GetEnemyTeams(characterBody.teamComponent.teamIndex),
                        filterByLoS = false,
                        searchOrigin = characterBody.corePosition,
                        searchDirection = UnityEngine.Random.onUnitSphere,
                        sortMode = BullseyeSearch.SortMode.Distance,
                        maxDistanceFilter = StaticValues.weatherReportRadius,
                        maxAngleFilter = 360f
                    };

                    search.RefreshCandidates();
                    search.FilterOutGameObject(characterBody.gameObject);

                    List<HurtBox> target = search.GetResults().ToList<HurtBox>();
                    foreach (HurtBox singularTarget in target)
                    {
                        if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                        {
                            int random = UnityEngine.Random.RandomRangeInt(0, 4);

                            switch (random)
                            {
                                case 0:
                                    //overloading worm lightning strike

                                    ProcChainMask procChainMask1 = default(ProcChainMask);
                                    procChainMask1.AddProc(ProcType.LightningStrikeOnHit);

                                    OrbManager.instance.AddOrb(new SimpleLightningStrikeOrb
                                    {
                                        attacker = characterBody.gameObject,
                                        damageColorIndex = DamageColorIndex.Default,
                                        damageValue = characterBody.damage * Modules.StaticValues.weatherReportDamageCoefficient,
                                        damageType = DamageType.Shock5s,
                                        origin = characterBody.corePosition,
                                        procChainMask = procChainMask1,
                                        procCoefficient = 1f,
                                        isCrit = Util.CheckRoll(characterBody.crit, characterBody.master),
                                        teamIndex = characterBody.GetComponent<TeamComponent>().teamIndex,
                                        target = singularTarget,

                                    });
                                    break;
                                case 1:
                                    //kjaro band fire tornado

                                    ProcChainMask procChainMask5 = default(ProcChainMask);
                                    procChainMask5.AddProc(ProcType.Rings);
                                    GameObject gameObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/FireTornado");
                                    float resetInterval = gameObject.GetComponent<ProjectileOverlapAttack>().resetInterval;
                                    float lifetime = gameObject.GetComponent<ProjectileSimple>().lifetime;
                                    float damageCoefficient9 = StaticValues.weatherReportDamageCoefficient;
                                    float damage3 = Util.OnHitProcDamage(characterBody.damage, characterBody.damage, damageCoefficient9) / lifetime * resetInterval;
                                    float speedOverride = 0f;
                                    Quaternion rotation2 = Quaternion.identity;
                                    Vector3 vector = singularTarget.transform.position - characterBody.inputBank.aimOrigin;
                                    vector.y = 0f;
                                    if (vector != Vector3.zero)
                                    {
                                        speedOverride = -1f;
                                        rotation2 = Util.QuaternionSafeLookRotation(vector, Vector3.up);
                                    }
                                    ProjectileManager.instance.FireProjectile(new FireProjectileInfo
                                    {
                                        damage = damage3,
                                        crit = characterBody.RollCrit(),
                                        damageColorIndex = DamageColorIndex.Default,
                                        position = singularTarget.transform.position,
                                        procChainMask = procChainMask5,
                                        force = 0f,
                                        owner = characterBody.gameObject,
                                        projectilePrefab = gameObject,
                                        rotation = rotation2,
                                        speedOverride = speedOverride,
                                        target = null
                                    });

                                    break;
                                case 2:
                                    //runald band ice explosion
                                    ProcChainMask procChainMask4 = default(ProcChainMask);
                                    procChainMask4.AddProc(ProcType.Rings);
                                    DamageInfo damageInfo2 = new DamageInfo
                                    {
                                        damage = characterBody.damage * StaticValues.weatherReportDamageCoefficient,
                                        damageColorIndex = DamageColorIndex.Default,
                                        damageType = DamageType.Freeze2s,
                                        attacker = characterBody.gameObject,
                                        crit = characterBody.RollCrit(),
                                        force = Vector3.zero,
                                        inflictor = null,
                                        position = singularTarget.transform.position,
                                        procChainMask = procChainMask4,
                                        procCoefficient = 1f
                                    };
                                    EffectManager.SimpleImpactEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/IceRingExplosion"), singularTarget.transform.position, Vector3.up, true);
                                    characterBody.ApplyBuff(RoR2Content.Buffs.Slow80.buffIndex, 1, 5);
                                    characterBody.healthComponent.TakeDamage(damageInfo2);

                                    break;
                                case 3:
                                    //stone titan fist projectile? otherwise just do a knock up
                                    EffectManager.SpawnEffect(Assets.stonetitanFistEffect, new EffectData
                                    {
                                        origin = singularTarget.transform.position,
                                        scale = 1f,
                                        rotation = Quaternion.identity,

                                    }, true);


                                    FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
                                    fireProjectileInfo.projectilePrefab = Assets.stonetitanFistProj;
                                    fireProjectileInfo.position = singularTarget.transform.position;
                                    fireProjectileInfo.rotation = Quaternion.identity;
                                    fireProjectileInfo.owner = characterBody.gameObject;
                                    fireProjectileInfo.damage = characterBody.damage * StaticValues.weatherReportDamageCoefficient;
                                    fireProjectileInfo.force = 2000f;
                                    fireProjectileInfo.crit = characterBody.RollCrit();
                                    fireProjectileInfo.fuseOverride = 0.5f;
                                    ProjectileManager.instance.FireProjectile(fireProjectileInfo);

                                    break;
                                case 4:
                                    //gravitational force down
                                    float Weight = 1f;
                                    if (singularTarget.healthComponent.body.characterMotor)
                                    {
                                        Weight = singularTarget.healthComponent.body.characterMotor.mass;
                                    }
                                    else if (singularTarget.healthComponent.body.rigidbody)
                                    {
                                        Weight = singularTarget.healthComponent.body.rigidbody.mass;
                                    }
                                    DamageInfo damageInfo = new DamageInfo
                                    {
                                        attacker = characterBody.gameObject,
                                        inflictor = characterBody.gameObject,
                                        damage = characterBody.damage * StaticValues.weatherReportDamageCoefficient,
                                        position = singularTarget.transform.position,
                                        procCoefficient = 1f,
                                        damageType = DamageType.Generic,
                                        crit = characterBody.RollCrit(),

                                    };

                                    singularTarget.healthComponent.TakeDamageForce(Vector3.down * 100f * (Weight), true, true);
                                    singularTarget.healthComponent.TakeDamage(damageInfo);
                                    GlobalEventManager.instance.OnHitEnemy(damageInfo, singularTarget.healthComponent.gameObject);


                                    EffectManager.SpawnEffect(Assets.voidjailerEffect, new EffectData
                                    {
                                        origin = singularTarget.transform.position,
                                        scale = 1f,
                                        rotation = Quaternion.LookRotation(Vector3.down),

                                    }, true);
                                    break;


                            }
                        }


                    }
                }
            }
            else if (!characterBody.HasBuff(Buffs.weatherReportBuff))
            {
                if (weatherReportIndicatorInstance)
                {
                    weatherReportIndicatorInstance.SetActive(false);
                    EntityState.Destroy(weatherReportIndicatorInstance.gameObject);
                }

            }
        }

        public void MachineForm()
        {

            //machine form buff
            if (characterBody.HasBuff(Buffs.machineFormBuff))
            {
                if (machineFormTimer <= StaticValues.machineFormThreshold)
                {
                    machineFormTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                }
                else if (machineFormTimer > StaticValues.machineFormThreshold)
                {
                    machineFormTimer = 0f;
                    //shoot missiles here
                    Ray aimRay = characterBody.inputBank.GetAimRay();
                    EffectManager.SpawnEffect(FireVoidMissiles.muzzleEffectPrefab, new EffectData
                    {
                        origin = characterBody.corePosition,
                        scale = 1f,
                        rotation = Quaternion.LookRotation(aimRay.direction)
                    }, false);

                    ProjectileManager.instance.FireProjectile(
                        FireVoidMissiles.projectilePrefab, //prefab
                        aimRay.origin, //position
                        Util.QuaternionSafeLookRotation(aimRay.direction), //rotation
                        characterBody.gameObject, //owner
                        characterBody.damage * StaticValues.machineFormDamageCoefficient, //damage
                        100f, //force
                        characterBody.RollCrit(), //crit
                        DamageColorIndex.Default, //damage color
                        null, //target
                        -1); //speed }} 

                    ProjectileManager.instance.FireProjectile(
                        FireVoidMissiles.projectilePrefab, //prefab
                        aimRay.origin, //position
                        Util.QuaternionSafeLookRotation(aimRay.direction), //rotation
                        characterBody.gameObject, //owner
                        characterBody.damage * StaticValues.machineFormDamageCoefficient, //damage
                        100f, //force
                        characterBody.RollCrit(), //crit
                        DamageColorIndex.Default, //damage color
                        null, //target
                        -1); //speed }} 

                    //shoot bullet to closest target
                    BullseyeSearch search = new BullseyeSearch
                    {

                        teamMaskFilter = TeamMask.GetEnemyTeams(characterBody.teamComponent.teamIndex),
                        filterByLoS = true,
                        searchOrigin = characterBody.corePosition,
                        searchDirection = UnityEngine.Random.onUnitSphere,
                        sortMode = BullseyeSearch.SortMode.Distance,
                        maxDistanceFilter = StaticValues.machineFormRadius,
                        maxAngleFilter = 360f
                    };

                    search.RefreshCandidates();
                    search.FilterOutGameObject(characterBody.gameObject);


                    List<HurtBox> target = search.GetResults().ToList<HurtBox>();
                    foreach (HurtBox singularTarget in target)
                    {
                        if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                        {
                            var bulletAttack = new BulletAttack
                            {
                                bulletCount = 1,
                                aimVector = singularTarget.transform.position - characterBody.corePosition,
                                origin = characterBody.corePosition,
                                damage = characterBody.damage * StaticValues.machineFormDamageCoefficient,
                                damageColorIndex = DamageColorIndex.Default,
                                damageType = DamageType.Generic,
                                falloffModel = BulletAttack.FalloffModel.None,
                                maxDistance = StaticValues.machineFormRadius,
                                force = 100f,
                                hitMask = LayerIndex.CommonMasks.bullet,
                                minSpread = 0f,
                                maxSpread = 0f,
                                isCrit = characterBody.RollCrit(),
                                owner = characterBody.gameObject,
                                smartCollision = false,
                                procChainMask = default(ProcChainMask),
                                procCoefficient = 1f,
                                radius = 1f,
                                sniper = false,
                                stopperMask = LayerIndex.world.mask,
                                weapon = null,
                                tracerEffectPrefab = EntityStates.Commando.CommandoWeapon.FirePistol2.tracerEffectPrefab,
                                spreadPitchScale = 0f,
                                spreadYawScale = 0f,
                                queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                                hitEffectPrefab = EntityStates.Commando.CommandoWeapon.FirePistol2.hitEffectPrefab,

                            };
                            bulletAttack.Fire();

                        }
                    }


                }
            }
        }

        public void Reversal()
        {

            //reversal buff effect- moving buildup charge
            if (characterBody.HasBuff(Buffs.reversalBuff))
            {
                if (characterBody.isSprinting)
                {
                    reversalTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                    if (reversalTimer > StaticValues.reversalStepRate / characterBody.moveSpeed)
                    {
                        reversalTimer = 0f;
                        int reversalBuffCount = characterBody.GetBuffCount(Buffs.reversalBuffStacks);
                        characterBody.ApplyBuff(Buffs.reversalBuffStacks.buffIndex, reversalBuffCount + 1);
                    }
                }
            }
        }

        public void DoubleTime()
        {

            //double time slow effect
            if (characterBody.HasBuff(Buffs.doubleTimeBuff))
            {
                if (doubleTimeTimer <= 1f)
                {
                    doubleTimeTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                }
                else if (doubleTimeTimer > 1f)
                {
                    ApplyDoubleTimeDebuff();
                    doubleTimeTimer = 0f;
                }

                if (!doubleTimeIndicatorInstance)
                {
                    CreateDoubleTimeIndicator();
                }
            }
            else if (!characterBody.HasBuff(Buffs.doubleTimeBuff))
            {
                if (doubleTimeIndicatorInstance)
                {
                    doubleTimeIndicatorInstance.SetActive(false);
                    EntityState.Destroy(doubleTimeIndicatorInstance.gameObject);
                }
            }
            //double time buff removal
            if (characterBody.HasBuff(Buffs.doubleTimeBuffStacks))
            {
                if (doubleTimeStacksTimer <= StaticValues.doubleTimeThreshold)
                {
                    doubleTimeStacksTimer += Time.fixedDeltaTime;
                }
                else if (doubleTimeStacksTimer > StaticValues.doubleTimeThreshold)
                {
                    doubleTimeStacksTimer = 0f;
                    int doubleTimeStacksBuffcount = characterBody.GetBuffCount(Buffs.doubleTimeBuffStacks);
                    characterBody.ApplyBuff(Buffs.doubleTimeBuffStacks.buffIndex, Mathf.RoundToInt(doubleTimeStacksBuffcount / 2));
                }
            }
        }

        public void BarbedSpikes()
        {

            if (characterBody.HasBuff(Buffs.barbedSpikesBuff))
            {
                if (!barbedSpikesIndicatorInstance)
                {
                    CreateBarbedSpikesIndicator();
                }

                if (barbedSpikesTimer <= StaticValues.barbedSpikesBuffThreshold)
                {
                    barbedSpikesTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                }
                else if (barbedSpikesTimer > StaticValues.barbedSpikesBuffThreshold)
                {
                    barbedSpikesTimer = 0f;
                    BullseyeSearch search = new BullseyeSearch
                    {

                        teamMaskFilter = TeamMask.GetEnemyTeams(characterBody.teamComponent.teamIndex),
                        filterByLoS = false,
                        searchOrigin = characterBody.corePosition,
                        searchDirection = UnityEngine.Random.onUnitSphere,
                        sortMode = BullseyeSearch.SortMode.Distance,
                        maxDistanceFilter = StaticValues.barbedSpikesRadius * characterBody.damage / characterBody.baseDamage,
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
        }

        public void AuraOfBlight()
        {

            if (characterBody.HasBuff(Buffs.auraOfBlightBuff))
            {
                if (auraOfBlightTimer <= StaticValues.auraOfBlightBuffThreshold)
                {
                    auraOfBlightTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                }
                else if (auraOfBlightTimer > StaticValues.auraOfBlightBuffThreshold)
                {
                    auraOfBlightTimer = 0f;
                    ApplyBlight();

                }
                if (!this.auraOfBlightIndicatorInstance)
                {
                    CreateAuraOfBlightIndicator();
                }
            }
            else if (!characterBody.HasBuff(Buffs.auraOfBlightBuff))
            {
                if (auraOfBlightIndicatorInstance)
                {
                    auraOfBlightIndicatorInstance.SetActive(false);
                    EntityState.Destroy(auraOfBlightIndicatorInstance.gameObject);
                }
            }
        }

        public void GachaBuff()
        {

            //gacha buff timer
            if (characterBody.HasBuff(Buffs.gachaBuff))
            {
                int gachaBuffcount = characterBody.GetBuffCount(Buffs.gachaBuff);

                if (gachaBuffcount < StaticValues.gachaBuffThreshold)
                {
                    if (gachaBuffThreshold <= 1f)
                    {
                        gachaBuffThreshold += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                    }
                    else if (gachaBuffThreshold > 1f)
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
        }

        public void OmniBoost()
        {

            //omniboost buff expire timer
            //if (characterBody.HasBuff(Buffs.omniboostBuffStacks))
            //{
            //    if (omniboostTimer <= StaticValues.omniboostBuffTimer)
            //    {
            //        omniboostTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
            //    }
            //    else if (omniboostTimer > StaticValues.omniboostBuffTimer)
            //    {
            //        int buffCountToApply = characterBody.GetBuffCount(Buffs.omniboostBuffStacks.buffIndex);
            //        characterBody.ApplyBuff(Buffs.omniboostBuffStacks.buffIndex, buffCountToApply - 1);
            //        omniboostTimer = 0f;                    

            //    }
            //}
        }

        public void CaptainBuff()
        {

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
        }
        public void OverloadingWorm()
        {

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
                        overloadingtimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
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
        }

        public void MagmaWorm()
        {

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
                        magmawormtimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
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
        }

        public void RoboballMini()
        {

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
                            int solusBuffCount = characterBody.GetBuffCount(Buffs.roboballminiattackspeedBuff.buffIndex);
                            characterBody.ApplyBuff(Modules.Buffs.roboballminiattackspeedBuff.buffIndex, solusBuffCount + 1);
                        }
                        else
                        {
                            roboballTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;

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
                            int solusBuffCount = characterBody.GetBuffCount(Buffs.roboballminiattackspeedBuff.buffIndex);
                            characterBody.ApplyBuff(Modules.Buffs.roboballminiattackspeedBuff.buffIndex, solusBuffCount + 1);
                        }
                        else
                        {
                            roboballTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;

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
        }

        public void MiniMushrum()
        {


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
        }

        public void AlphaShield()
        {

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
                    else alphaconstructshieldtimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                }

            }
        }

        public void StoneForm()
        {

            //stoneform buff still effect
            if (characterBody.HasBuff(Buffs.stoneFormStillBuff.buffIndex))
            {
                if (stoneformStillbuffTimer <= 1f)
                {
                    stoneformStillbuffTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                }
                else if (stoneformStillbuffTimer > 1f)
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
        }

        public void StandingStill()
        {
            //Standing still/not moving buffs
            if (characterBody.moveSpeed == 0)
            {
                //stoneform buff
                if (characterBody.HasBuff(Modules.Buffs.stoneFormBuff.buffIndex))
                {
                    if (stoneFormTimer <= StaticValues.stoneFormWaitDuration)
                    {
                        stoneFormTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                    }
                    else if (stoneFormTimer > StaticValues.stoneFormWaitDuration)
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
                    mortarTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                    if (mortarTimer > Modules.StaticValues.mortarbaseDuration / (characterBody.attackSpeed))
                    {
                        int hermitbuffcount = characterBody.GetBuffCount(Buffs.hermitcrabmortararmorBuff.buffIndex);
                        characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortararmorBuff.buffIndex, hermitbuffcount + 1);
                        mortarTimer = 0f;
                        FireMortar();

                    }

                }
                else if (!characterBody.HasBuff(Modules.Buffs.hermitcrabmortarBuff))
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
                    voidmortarTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                    if (voidmortarTimer > Modules.StaticValues.voidmortarbaseDuration / (characterBody.armor / characterBody.baseArmor))
                    {
                        int voidbarnaclebuffcount = characterBody.GetBuffCount(Buffs.voidbarnaclemortarattackspeedBuff.buffIndex);
                        characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarattackspeedBuff.buffIndex, voidbarnaclebuffcount + 1);
                        attackSpeedGain = Modules.StaticValues.voidmortarattackspeedGain * characterBody.GetBuffCount(Modules.Buffs.voidbarnaclemortarattackspeedBuff);
                        voidmortarTimer = 0f;
                        FireMortar();
                    }
                }
                else if (!characterBody.HasBuff(Modules.Buffs.voidbarnaclemortarBuff))
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
                    voidjailerTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                    if (voidjailerTimer > StaticValues.voidjailerInterval / (num / characterBody.baseMoveSpeed))
                    {
                        voidjailerTimer = 0f;
                        VoidJailerPull();
                    }
                }
            }
        }

        public void VerminJump()
        {

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
        }

        public void LarvaJump()
        {

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
                    blastAttack.AddModdedDamageType(Modules.Damage.shiggyDecay);
                    blastAttack.Fire();


                }

                if (!characterBody.characterMotor.isGrounded)
                {
                    larvaTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
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
                    blastAttack.damageType = DamageType.Generic;
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
        public void FixedUpdate()
        {
            if(characterBody != null)
            {

                if (characterBody.hasEffectiveAuthority)
                {
                    //Buff effects
                    OFAFO();
                    WeatherReport();
                    MachineForm();
                    Reversal();
                    DoubleTime();
                    BarbedSpikes();
                    AuraOfBlight();
                    GachaBuff();
                    OmniBoost();
                    CaptainBuff();
                    OverloadingWorm();
                    MagmaWorm();
                    RoboballMini();
                    MiniMushrum();
                    AlphaShield();
                    StoneForm();
                    StandingStill();
                    VerminJump();
                    LarvaJump();

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
        
     

        public void UpdateIndicator()
        {
            //weather report indicator
            if (this.weatherReportIndicatorInstance)
            {
                this.weatherReportIndicatorInstance.transform.parent = characterBody.transform;
                this.weatherReportIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.weatherReportRadius;
                //this.weatherReportIndicatorInstance.transform.localPosition = Vector3.zero;
            }
            if (this.barbedSpikesIndicatorInstance)
            {
                this.barbedSpikesIndicatorInstance.transform.parent = characterBody.transform;
                this.barbedSpikesIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.barbedSpikesRadius * (characterBody.damage / characterBody.baseDamage);
                //this.barbedSpikesIndicatorInstance.transform.localPosition = Vector3.zero;
            }
            if (this.mortarIndicatorInstance)
            {
                this.mortarIndicatorInstance.transform.parent = characterBody.transform;
                this.mortarIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.mortarRadius * (characterBody.armor / characterBody.baseArmor);
                //this.mortarIndicatorInstance.transform.localPosition = Vector3.zero;
			}
			if (this.voidmortarIndicatorInstance)
            {
                this.voidmortarIndicatorInstance.transform.parent = characterBody.transform;
                this.voidmortarIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.voidmortarRadius * (characterBody.attackSpeed);
                //this.voidmortarIndicatorInstance.transform.localPosition = Vector3.zero;
			}
            if(this.doubleTimeIndicatorInstance)
            {
                this.doubleTimeIndicatorInstance.transform.parent = characterBody.transform;
                this.doubleTimeIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.doubleTimeRadius;
                //this.doubleTimeIndicatorInstance.transform.localPosition = Vector3.zero;
            }
            if(this.auraOfBlightIndicatorInstance)
            {
                this.auraOfBlightIndicatorInstance.transform.parent = characterBody.transform;
                this.auraOfBlightIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.auraOfBlightBuffRadius;
                //this.auraOfBlightIndicatorInstance.transform.localPosition = Vector3.zero;
            }
        }


        //weather report Indicator 
        private void CreateWeatherReportIndicator()
        {
            if (EntityStates.Huntress.ArrowRain.areaIndicatorPrefab)
            {
                this.weatherReportIndicatorInstance = Object.Instantiate<GameObject>(EntityStates.Huntress.ArrowRain.areaIndicatorPrefab);
                this.weatherReportIndicatorInstance.SetActive(true);

                this.weatherReportIndicatorInstance.transform.parent = characterBody.transform;
                this.weatherReportIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.weatherReportRadius;
                this.weatherReportIndicatorInstance.transform.localPosition = Vector3.zero;

            }
        }

        //aura of blight buff 
        public void CreateAuraOfBlightIndicator()
        {
            if (Assets.auraOfBlightIndicator)
            {
                this.auraOfBlightIndicatorInstance= Object.Instantiate<GameObject>(Assets.auraOfBlightIndicator);
                this.auraOfBlightIndicatorInstance.SetActive(true);

                this.auraOfBlightIndicatorInstance.transform.parent = characterBody.transform;
                this.auraOfBlightIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.auraOfBlightBuffRadius;
                this.auraOfBlightIndicatorInstance.transform.localPosition = Vector3.zero;

            }

        }

        //double time buff 
        public void CreateDoubleTimeIndicator()
        {
            if (Assets.doubleTimeIndicator)
            {
                this.doubleTimeIndicatorInstance = Object.Instantiate<GameObject>(Assets.doubleTimeIndicator);
                this.doubleTimeIndicatorInstance.SetActive(true);

                this.doubleTimeIndicatorInstance.transform.parent = characterBody.transform;
                this.doubleTimeIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.doubleTimeRadius;
                this.doubleTimeIndicatorInstance.transform.localPosition = Vector3.zero;

            }

        }
        //barbed spikes indicator
        public void CreateBarbedSpikesIndicator()
        {
            if (Assets.barbedSpikesIndicator)
            {
                this.barbedSpikesIndicatorInstance = Object.Instantiate<GameObject>(Assets.barbedSpikesIndicator);
                this.barbedSpikesIndicatorInstance.SetActive(true);

                this.barbedSpikesIndicatorInstance.transform.parent = characterBody.transform;
                this.barbedSpikesIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.barbedSpikesRadius * (characterBody.damage / characterBody.baseDamage);
                this.barbedSpikesIndicatorInstance.transform.localPosition = Vector3.zero;

            }
        }
        //hermit crab mortar
        public void CreateMortarIndicator()
		{
			if (Assets.hermitCrabMortarIndicator)
            {
                this.mortarIndicatorInstance = Object.Instantiate<GameObject>(Assets.hermitCrabMortarIndicator);
                this.mortarIndicatorInstance.SetActive(true);

                this.mortarIndicatorInstance.transform.parent = characterBody.transform;
                this.mortarIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.mortarRadius * (characterBody.armor / characterBody.baseArmor);
                this.mortarIndicatorInstance.transform.localPosition = Vector3.zero;

            }
		}
        //void barnacle mortar	
        public void CreateVoidMortarIndicator()
		{
			if (Assets.voidBarnacleMortarIndicator)
            {
                this.voidmortarIndicatorInstance = Object.Instantiate<GameObject>(Assets.voidBarnacleMortarIndicator);
                this.voidmortarIndicatorInstance.SetActive(true);

                this.voidmortarIndicatorInstance.transform.parent = characterBody.transform;
                this.voidmortarIndicatorInstance.transform.localScale = Vector3.one * Modules.StaticValues.voidmortarRadius * (characterBody.attackSpeed);
                this.voidmortarIndicatorInstance.transform.localPosition = Vector3.zero;

            }
		}

        //code for both mortars
        public void FireMortar()
        {
            MortarOrb mortarOrb = new MortarOrb
            {
                attacker = characterBody.gameObject,
                damageColorIndex = DamageColorIndex.Default,
                damageValue = characterBody.damage * Modules.StaticValues.mortarDamageCoefficient * characterBody.attackSpeed * (characterBody.armor / characterBody.baseArmor),
                origin = characterBody.corePosition,
                procChainMask = new ProcChainMask(),
                procCoefficient = 1f,
                isCrit = Util.CheckRoll(characterBody.crit, characterBody.master),
                teamIndex = characterBody.teamComponent.teamIndex,
            };
            if (mortarOrb.target = mortarOrb.PickNextTarget(mortarOrb.origin, Modules.StaticValues.mortarRadius * characterBody.attackSpeed * (characterBody.armor / characterBody.baseArmor)))
            {
                OrbManager.instance.AddOrb(mortarOrb);
            }

            EffectManager.SpawnEffect(EntityStates.HermitCrab.FireMortar.mortarMuzzleflashEffect, new EffectData
            {
                origin = characterBody.corePosition,
                scale = 1f,
                rotation = Quaternion.identity,

            }, true);

        

            //BullseyeSearch search = new BullseyeSearch
            //{

            //    teamMaskFilter = TeamMask.GetEnemyTeams(characterBody.teamComponent.teamIndex),
            //    filterByLoS = false,
            //    searchOrigin = characterBody.corePosition,
            //    searchDirection = UnityEngine.Random.onUnitSphere,
            //    sortMode = BullseyeSearch.SortMode.Distance,
            //    maxDistanceFilter = Modules.StaticValues.mortarRadius * characterBody.attackSpeed * (characterBody.armor / characterBody.baseArmor),
            //    maxAngleFilter = 360f
            //};

            //search.RefreshCandidates();
            //search.FilterOutGameObject(characterBody.gameObject);

            //HurtBox target = this.search.GetResults().FirstOrDefault<HurtBox>();

            //if (target.healthComponent && target.healthComponent.body)
            //{

            //    LightningOrb lightningOrb = new LightningOrb();
            //    lightningOrb.attacker = characterBody.gameObject;
            //    lightningOrb.bouncedObjects = null;
            //    lightningOrb.bouncesRemaining = 0;
            //    lightningOrb.damageCoefficientPerBounce = 1f;
            //    lightningOrb.damageColorIndex = DamageColorIndex.Default;
            //    lightningOrb.damageValue = characterBody.damage * Modules.StaticValues.mortarDamageCoefficient * characterBody.attackSpeed * (characterBody.armor / characterBody.baseArmor);
            //    lightningOrb.isCrit = characterBody.RollCrit();
            //    lightningOrb.lightningType = LightningOrb.LightningType.MageLightning;
            //    lightningOrb.origin = characterBody.corePosition;
            //    lightningOrb.procChainMask = default(ProcChainMask);
            //    lightningOrb.procCoefficient = 1f;
            //    lightningOrb.range = 0f;
            //    lightningOrb.teamIndex = characterBody.teamComponent.teamIndex;
            //    lightningOrb.target = target;
            //    OrbManager.instance.AddOrb(lightningOrb);

            //    //EffectManager.SpawnEffect(EntityStates.HermitCrab.FireMortar.mortarMuzzleflashEffect, new EffectData
            //    //{
            //    //    origin = characterBody.corePosition,
            //    //    scale = 1f,
            //    //    rotation = Util.QuaternionSafeLookRotation(target.transform.position - characterBody.corePosition),

            //    //}, true);
            //}
            
        }

        //     MortarOrb mortarOrb = new MortarOrb
        //{
        //	attacker = characterBody.gameObject,
        //	damageColorIndex = DamageColorIndex.Default,
        //	damageValue = characterBody.damage * Modules.StaticValues.mortarDamageCoefficient * characterBody.attackSpeed * (characterBody.armor/characterBody.baseArmor),
        //	origin = characterBody.corePosition,
        //	procChainMask = default(ProcChainMask),
        //	procCoefficient = 1f,
        //	isCrit = Util.CheckRoll(characterBody.crit, characterBody.master),
        //	teamIndex = characterBody.GetComponent<TeamComponent>()?.teamIndex ?? TeamIndex.Neutral,
        //};
        //if (mortarOrb.target = mortarOrb.PickNextTarget(mortarOrb.origin, Modules.StaticValues.mortarRadius * characterBody.attackSpeed * (characterBody.armor/characterBody.baseArmor)))
        //{
        //	OrbManager.instance.AddOrb(mortarOrb);
        //}

        //         EffectManager.SpawnEffect(EntityStates.HermitCrab.FireMortar.mortarMuzzleflashEffect, new EffectData
        //         {
        //             origin = characterBody.corePosition,
        //             scale = 1f,
        //             rotation = Util.QuaternionSafeLookRotation(mortarOrb.target.transform.position - characterBody.corePosition),

        //         }, true);

        //     }

        //overloading orb
        public void OverloadingFire()
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


