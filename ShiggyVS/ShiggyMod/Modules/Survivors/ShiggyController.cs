using EntityStates;
using EntityStates.LunarExploderMonster;
using EntityStates.MiniMushroom;
using EntityStates.VoidMegaCrab.BackWeapon;
using ExtraSkillSlots;
using IL.RoR2.Achievements.Bandit2;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RiskOfOptions.Components.Panel;
using RoR2;
using RoR2.CharacterAI;
using RoR2.Items;
using RoR2.Orbs;
using RoR2.Projectile;
using ShiggyMod.Modules.Networking;
using ShiggyMod.Modules.Quirks;
using ShiggyMod.SkillStates;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Baselib.LowLevel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem.PlaybackState;
using Object = UnityEngine.Object;

namespace ShiggyMod.Modules.Survivors
{
    public class ShiggyController : MonoBehaviour
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";


        private GameObject theWorldIndicatorInstance;
        public GameObject deathAuraIndicatorInstance;

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
        private float airwalkEnergyTimer;
        //public bool flightExpired;
        private float OFATimer;
        private float voidFormTimer;
        private float overclockTimer;
        private float deathAuraTimer;
        private float OFAFOTimer;
        public float OFAFOTimeMultiplier;
        private float finalReleaseTimer;
        private float buttonCooler;
        private int buttonCount;
        private float formTimer;
        private float halcyoniteTimer;


        //indicators
        private Ray downRay;
        public float maxTrackingDistance = 70f;
        public float maxTrackingAngle = 20f;
        public float trackerUpdateFrequency = 10f;
        private Indicator indicator;
        //private Indicator passiveindicator;
        //private Indicator activeindicator;
        public HurtBox trackingTarget;
        public HurtBox Target;
        private QuirkId _lastQuirkId;

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

        public bool larvabuffGiven;
        public bool verminjumpbuffGiven;
        private uint minimushrumsoundID;

        private ExtraInputBankTest extrainputBankTest;
        private ExtraSkillLocator extraskillLocator;

        public bool hasQuirk;
        public float quirkTimer;

        public int captainitemcount;
        private DamageType damageType = new DamageTypeCombo(DamageType.Generic, DamageTypeExtended.Generic, DamageSource.Secondary);
        private DamageType damageType2;
        private float multTimer;
        private float clayDunestriderTimer;
        private float greaterwispTimer;

        //AFO
        private bool saveSkills;
        private bool hasStolen;
        private float stealQuirkStopwatch;
        private bool hasRemoved;
        private float removeQuirkStopwatch;
        private float theWorldTimer;
        private float giveQuirkStopwatch;
        public CharacterBody giveQuirkBody;

        //Particles
        public ParticleSystem RARM;
        public ParticleSystem LARM;
        public ParticleSystem OFA;
        public ParticleSystem FINALRELEASEAURA;
        public ParticleSystem SWORDAURAL;
        public ParticleSystem SWORDAURAR;

        //particle bools
        public bool boolenoughEnergyAura;
        public bool booloneForAllAura;
        public bool boolfinalReleaseAura;
        public bool boolswordAuraL;
        public bool boolswordAuraR;

        //final release loop sound
        public uint finalReleaseLoopID;

        public void Awake()
        {

            child = GetComponentInChildren<ChildLocator>();
            anim = GetComponentInChildren<Animator>();


            if (child)
            {
                LARM = child.FindChild("lArmAura").GetComponent<ParticleSystem>();
                RARM = child.FindChild("rArmAura").GetComponent<ParticleSystem>();
                OFA = child.FindChild("OFAlightning").GetComponent<ParticleSystem>();
                FINALRELEASEAURA = child.FindChild("finalReleaseAura").GetComponent<ParticleSystem>();
                SWORDAURAL = child.FindChild("WindSwordL").GetComponent<ParticleSystem>();
                SWORDAURAR = child.FindChild("WindSwordR").GetComponent<ParticleSystem>();
            }

            LARM.Stop();
            RARM.Stop();
            OFA.Stop();
            FINALRELEASEAURA.Stop();
            SWORDAURAL.Stop();
            SWORDAURAR.Stop();

            indicator = new Indicator(gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/HuntressTrackingIndicator"));
            // name indicator
            //quirkNameIndicator = new RoR2.Indicator(gameObject, null);
            //quirkNameIndicator.visualizerPrefab = QuirkIndicatorFactory.GetOrBuildNameBillboardPrefab();

            //// owned-check indicator
            //ownedIndicator = new RoR2.Indicator(gameObject, null);
            //ownedIndicator.visualizerPrefab = QuirkIndicatorFactory.GetOrBuildOwnedCheckPrefab();

            //passiveindicator = new Indicator(gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/EngiMissileTrackingIndicator"));
            //activeindicator = new Indicator(gameObject, LegacyResourcesAPI.Load<GameObject>("Prefabs/HuntressTrackingIndicator"));
            //On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
            inputBank = gameObject.GetComponent<InputBankTest>();

            larvabuffGiven = false;
            verminjumpbuffGiven = false;


            hasStolen = false;
            hasQuirk = false;

        }


        public void Start()
        {
            characterBody = gameObject.GetComponent<CharacterBody>();
            characterMaster = characterBody.master;

            energySystem = gameObject.GetComponent<EnergySystem>();
            if (!energySystem)
            {
                energySystem = gameObject.AddComponent<EnergySystem>();
            }

            Shiggymastercon = characterMaster.gameObject.GetComponent<ShiggyMasterController>();
            if (!Shiggymastercon)
            {
                Shiggymastercon = characterMaster.gameObject.AddComponent<ShiggyMasterController>();
            }

            extraskillLocator = characterBody.gameObject.GetComponent<ExtraSkillLocator>();
            extrainputBankTest = characterBody.gameObject.GetComponent<ExtraInputBankTest>();

            QuirkHUDOverlay.EnsureFor(characterBody);

            hasStolen = false;
            hasQuirk = false;


        }



        public HurtBox GetTrackingTarget()
        {
            return this.trackingTarget;
        }

        public void OnEnable()
        {
            this.indicator.active = true;
            //this.passiveindicator.active = true;
            //this.activeindicator.active = true;
        }

        public void OnDisable()
        {
            this.indicator.active = false;
            //this.passiveindicator.active = false;
            //this.activeindicator.active = false;
        }

        public void PlayFinalReleaseLoop()
        {
            finalReleaseLoopID = AkSoundEngine.PostEvent("ShiggyBankaiMusic", characterBody.gameObject);

        }
        public void StopFinalReleaseLoop()
        {
            AkSoundEngine.StopPlayingID(finalReleaseLoopID);

        }

        public void OnDestroy()
        {
            StopFinalReleaseLoop();
            if (theWorldIndicatorInstance)
            {
                theWorldIndicatorInstance.SetActive(false);
                EntityState.Destroy(theWorldIndicatorInstance.gameObject);
            }
            if (deathAuraIndicatorInstance)
            {
                deathAuraIndicatorInstance.SetActive(false);
                EntityState.Destroy(deathAuraIndicatorInstance.gameObject);
            }
        }

        public void OFAFO()
        {

            //one for all for one buff
            if (characterBody.HasBuff(Buffs.OFAFOBuff))
            {
                //check energy if enough to use ability
                if (energySystem.currentplusChaos <= 0f)
                {
                    characterBody.ApplyBuff(Buffs.OFAFOBuff.buffIndex, 0);
                }

                //doubling the speed for all timers- besides this one
                OFAFOTimeMultiplier = StaticValues.OFAFOTimeMultiplierCoefficient;

                if (OFAFOTimer <= StaticValues.OFAFOThreshold)
                {
                    OFAFOTimer += Time.fixedDeltaTime;
                }
                else if (OFAFOTimer > StaticValues.OFAFOThreshold)
                {
                    OFAFOTimer = 0f;
                    //health cost
                    new SpendHealthNetworkRequest(characterBody.masterObjectId, StaticValues.OFAFOHealthCostCoefficient * characterBody.healthComponent.fullCombinedHealth).Send(NetworkDestination.Clients);
                    //energy cost
                    float plusChaosflatCost = (StaticValues.OFAFOEnergyCostCoefficient * energySystem.maxPlusChaos) - (energySystem.costflatplusChaos);
                    if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;

                    float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                    if (plusChaosCost < 0f) plusChaosCost = 0f;
                    energySystem.SpendplusChaos(plusChaosCost);

                }
            }
            else
            if (!characterBody.HasBuff(Buffs.OFAFOBuff))
            {
                OFAFOTimeMultiplier = 1f;
            }
        }

        public void LightForm()
        {

            //light buff
            if (characterBody.HasBuff(Buffs.lightFormBuff))
            {
                if (formTimer <= StaticValues.FormThreshold)
                {
                    formTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;

                }
                else if (formTimer > StaticValues.FormThreshold)
                {
                    formTimer = 0f;
                    //energy cost
                    float plusChaosflatCost = (StaticValues.lightFormEnergyCost) - (energySystem.costflatplusChaos);
                    if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;

                    float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                    if (plusChaosCost < 0f) plusChaosCost = 0f;
                    energySystem.SpendplusChaos(plusChaosCost);
                }
                //remove buff at 0
                if (energySystem.currentplusChaos < 1f)
                {
                    characterBody.ApplyBuff(Buffs.lightFormBuff.buffIndex, 0);
                }
            }
        }

        public void DarknessForm()
        {

            //darkness buff
            if (characterBody.HasBuff(Buffs.darknessFormBuff))
            {
                if (formTimer <= StaticValues.FormThreshold)
                {
                    formTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;

                }
                else if (formTimer > StaticValues.FormThreshold)
                {
                    formTimer = 0f;
                    //energy gain
                    energySystem.GainplusChaos(StaticValues.darkFormEnergyGain);
                }
                //remove buff at max
                if (energySystem.currentplusChaos >= energySystem.maxPlusChaos)
                {
                    characterBody.ApplyBuff(Buffs.darknessFormBuff.buffIndex, 0);
                }
            }
        }

        public void LightAndDarknessForm()
        {

            if (characterBody.HasBuff(Buffs.lightAndDarknessFormBuff))
            {
                //force energy constantly in the middle constantly
                energySystem.currentplusChaos = energySystem.maxPlusChaos / 2f;
            }
        }

        public void WildCardNoProjectileBuff()
        {

            //wildcard no projectile buff
            if (characterBody.HasBuff(Buffs.wildcardNoProjectileBuff))
            {
                //destroy projectile 
                Collider[] array = Physics.OverlapSphere(characterBody.corePosition, StaticValues.wildcardRangeGlobal, LayerIndex.projectile.mask);
                for (int i = 0; i < array.Length; i++)
                {
                    ProjectileController component = array[i].GetComponent<ProjectileController>();
                    if (component)
                    {
                        EffectData effectData = new EffectData();
                        effectData.origin = component.transform.position;
                        effectData.scale = 1f;
                        EffectManager.SpawnEffect(EntityStates.BeetleMonster.HeadbuttState.hitEffectPrefab, effectData, true);
                        Object.Destroy(component.gameObject);

                    }
                }
            }
        }

        public void DeathAura()
        {

            //death aura buff
            if (characterBody.HasBuff(Buffs.deathAuraBuff))
            {

                if (energySystem.currentplusChaos > StaticValues.deathAuraBuffEnergyCost)
                {
                    //make the death aura indicator
                    if (!deathAuraIndicatorInstance)
                    {
                        CreateDeathAuraIndicator();
                    }
                    //every 1 secs add a debuff to enemies and a buff stack to self, also drain energy
                    if (deathAuraTimer <= StaticValues.deathAuraThreshold)
                    {
                        deathAuraTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                    }
                    else if (deathAuraTimer > StaticValues.deathAuraThreshold)
                    {
                        //energy cost
                        float plusChaosflatCost = StaticValues.deathAuraBuffEnergyCost - (energySystem.costflatplusChaos);
                        if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;

                        float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                        if (plusChaosCost < 0f) plusChaosCost = 0f;
                        energySystem.SpendplusChaos(plusChaosCost);

                        deathAuraTimer = 0f;

                        //add buff to self
                        int deathAuraBuffCount = characterBody.GetBuffCount(Buffs.deathAuraBuff);
                        characterBody.ApplyBuff(Buffs.deathAuraBuff.buffIndex, deathAuraBuffCount + 1);

                        BullseyeSearch search = new BullseyeSearch
                        {

                            teamMaskFilter = TeamMask.GetEnemyTeams(characterBody.teamComponent.teamIndex),
                            filterByLoS = false,
                            searchOrigin = characterBody.corePosition,
                            searchDirection = UnityEngine.Random.onUnitSphere,
                            sortMode = BullseyeSearch.SortMode.Distance,
                            maxDistanceFilter = StaticValues.deathAuraRadius + StaticValues.deathAuraRadiusStacks * characterBody.GetBuffCount(Buffs.deathAuraBuff),
                            maxAngleFilter = 360f
                        };

                        search.RefreshCandidates();
                        search.FilterOutGameObject(characterBody.gameObject);

                        List<HurtBox> target = search.GetResults().ToList<HurtBox>();
                        foreach (HurtBox singularTarget in target)
                        {
                            if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                            {
                                //add the debuff to all enemies
                                int deathAuraDebuffCount = 0;
                                deathAuraDebuffCount = singularTarget.healthComponent.body.GetBuffCount(Buffs.deathAuraDebuff);

                                singularTarget.healthComponent.body.ApplyBuff(Buffs.deathAuraDebuff.buffIndex, deathAuraDebuffCount + 1);

                            }
                        }
                    }
                }
                else
                {
                    characterBody.ApplyBuff(Buffs.deathAuraBuff.buffIndex, 0);

                }

            }
            else if (!characterBody.HasBuff(Buffs.deathAuraBuff))
            {

                if (deathAuraIndicatorInstance)
                {
                    deathAuraIndicatorInstance.SetActive(false);
                    EntityState.Destroy(deathAuraIndicatorInstance.gameObject);

                }

            }
        }

        public void TheWorld()
        {

            //the world buff energy cost
            if (characterBody.HasBuff(Buffs.theWorldBuff))
            {
                if (theWorldTimer <= 1f)
                {
                    theWorldTimer += Time.fixedDeltaTime;
                }
                else if (theWorldTimer > 1f)
                {
                    theWorldTimer = 0f;
                    //energy cost
                    float plusChaosflatCost = (StaticValues.theWorldEnergyCost * energySystem.maxPlusChaos) - (energySystem.costflatplusChaos);
                    if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;

                    float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                    if (plusChaosCost < 0f) plusChaosCost = 0f;
                    energySystem.SpendplusChaos(plusChaosCost);

                }
            }
        }

        public void VoidForm()
        {

            //void form buff
            if (characterBody.HasBuff(Buffs.voidFormBuff))
            {
                if (voidFormTimer <= StaticValues.voidFormThreshold)
                {
                    voidFormTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                }
                else if (voidFormTimer > StaticValues.voidFormThreshold)
                {
                    voidFormTimer = 0f;
                    //take damage every second based off current hp, cleanse self as well
                    new SpendHealthNetworkRequest(characterBody.masterObjectId, characterBody.healthComponent.combinedHealth * StaticValues.voidFormHealthCostCoefficient).Send(NetworkDestination.Clients);

                    CleanseSystem.CleanseBodyServer(characterBody, true, false, false, true, true, true);
                }
            }
        }

        public void OFABuff()
        {

            //OFA
            if (characterBody.HasBuff(Buffs.OFABuff))
            {
                //play ofa particles here

                if (OFATimer <= StaticValues.OFAThreshold)
                {
                    OFATimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                }
                else if (OFATimer > StaticValues.OFAThreshold)
                {
                    OFATimer = 0f;
                    //take damage every second based off current hp
                    new SpendHealthNetworkRequest(characterBody.masterObjectId, characterBody.healthComponent.combinedHealth * StaticValues.OFAHealthCostCoefficient).Send(NetworkDestination.Clients);
                }
            }
        }

        public void AirWalk()
        {

            //air walk
            if(characterBody.characterMotor.jumpCount >= characterBody.maxJumpCount)
            {
                if (energySystem.currentplusChaos > 1f)
                {
                    if (characterBody.inputBank.jump.down)
                    {
                        airwalkTimer += Time.deltaTime;
                        if (airwalkTimer > 0.5f)
                        {
                            //constantly draining energy cost for air walk - based off % of max energy
                            float plusChaosflatCost = StaticValues.airwalkEnergyFraction * energySystem.maxPlusChaos - (energySystem.costflatplusChaos);
                            if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;

                            float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                            if (plusChaosCost < 0f) plusChaosCost = 0f;

                            if (airwalkEnergyTimer <= 1f)
                            {
                                airwalkEnergyTimer += Time.deltaTime;
                            }
                            else if (airwalkEnergyTimer > 1f)
                            {
                                energySystem.SpendplusChaos(plusChaosCost);
                                airwalkEnergyTimer = 0f;
                            }

                            characterBody.ApplyBuff(Modules.Buffs.airwalkBuff.buffIndex, 1);


                            //direction checks for mouse  
                            Vector3 moveVector = inputBank.moveVector;
                            Vector3 aimDirection = inputBank.aimDirection;
                            Vector3 normalized = new Vector3(aimDirection.x, 0f, aimDirection.z).normalized;
                            Vector3 up = base.transform.up;
                            Vector3 normalized2 = Vector3.Cross(up, normalized).normalized;

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
                                //flightExpired = false;
                            }
                            else if (!characterBody.inputBank.skill1.down
                            && !characterBody.inputBank.skill2.down
                            && !characterBody.inputBank.skill3.down
                            && !characterBody.inputBank.skill4.down
                            && !extrainputBankTest.extraSkill1.down
                            && !extrainputBankTest.extraSkill2.down
                            && !extrainputBankTest.extraSkill3.down
                            && !extrainputBankTest.extraSkill4.down)
                            {

                                //check if you're holding no direction so you go up
                                if (characterBody.inputBank.moveVector == Vector3.zero)
                                {
                                    characterBody.characterMotor.velocity.y += 2f;
                                    
                                    //characterBody.characterMotor.velocity.y = characterBody.moveSpeed;
                                    //characterBody.characterMotor.rootMotion += Vector3.up * characterBody.moveSpeed * Time.fixedDeltaTime;
                                }
                                else if (characterBody.inputBank.moveVector != Vector3.zero)
                                {
                                    //check if the direction you're holding is your aim direction, then go in that direction (allowing you to go up or down)
                                    if (Vector3.Dot(inputBank.moveVector, normalized) >= 0.8f)
                                    {
                                        characterBody.characterMotor.velocity = characterBody.inputBank.aimDirection * characterBody.moveSpeed * 2f;
                                        //characterBody.characterMotor.rootMotion += characterBody.inputBank.aimDirection * characterBody.moveSpeed * Time.fixedDeltaTime;
                                    }
                                    else
                                    {
                                        //otherwise if not then maintain height        
                                        if (characterBody.characterMotor.velocity.y <= 0f)
                                        {
                                            characterBody.characterMotor.velocity.y = 0f;
                                        }
                                        characterBody.characterMotor.rootMotion += characterBody.inputBank.moveVector * characterBody.moveSpeed * Time.deltaTime;
                                    }
                                }
                                //flightExpired = true;
                                //characterBody.characterMotor.velocity.y = characterBody.moveSpeed;

                            }


                            //before air walk timer runs out, can rise regardless besides while using a skill
                            //if (airwalkTimer <= StaticValues.airwalkThreshold)
                            //{

                            //    if (characterBody.inputBank.skill1.down
                            //    | characterBody.inputBank.skill2.down
                            //    | characterBody.inputBank.skill3.down
                            //    | characterBody.inputBank.skill4.down
                            //    | extrainputBankTest.extraSkill1.down
                            //    | extrainputBankTest.extraSkill2.down
                            //    | extrainputBankTest.extraSkill3.down
                            //    | extrainputBankTest.extraSkill4.down)
                            //    {
                            //        characterBody.characterMotor.velocity.y = 0f;
                            //        flightExpired = false;
                            //    }
                            //    else
                            //    {
                            //        flightExpired = true;
                            //        //characterBody.characterMotor.velocity.y = characterBody.moveSpeed;
                            //        characterBody.characterMotor.velocity = characterBody.inputBank.aimDirection * characterBody.moveSpeed;
                            //    }
                            //}
                            //after airwalk timer, need to ensure not holding any skill or any move direction to rise
                            //else if (airwalkTimer > StaticValues.airwalkThreshold)
                            //{
                            //    flightExpired = true;

                            //    if (characterBody.inputBank.skill1.down
                            //    | characterBody.inputBank.skill2.down
                            //    | characterBody.inputBank.skill3.down
                            //    | characterBody.inputBank.skill4.down
                            //    | extrainputBankTest.extraSkill1.down
                            //    | extrainputBankTest.extraSkill2.down
                            //    | extrainputBankTest.extraSkill3.down
                            //    | extrainputBankTest.extraSkill4.down)
                            //    {
                            //        characterBody.characterMotor.velocity.y = 0f;
                            //    }

                            //    if (characterBody.inputBank.moveVector == Vector3.zero)
                            //    {
                            //        characterBody.characterMotor.velocity.y = characterBody.moveSpeed;
                            //    }
                            //    else
                            //    {
                            //        characterBody.characterMotor.velocity.y = 0f;
                            //    }
                            //}

                        }


                    }

                    //move in the direction you're moving at a normal speed
                    //if (characterBody.inputBank.moveVector != Vector3.zero)
                    //{
                    //    //characterBody.characterMotor.velocity = characterBody.inputBank.moveVector * (characterBody.moveSpeed);
                    //    characterBody.characterMotor.rootMotion += characterBody.inputBank.moveVector * characterBody.moveSpeed * Time.fixedDeltaTime;
                    //    //characterBody.characterMotor.disableAirControlUntilCollision = false;
                    //}


                }
            }

            if (characterBody.characterMotor.isGrounded)
            {
                //remove airwalk buff when landed
                //flightExpired = false;
                airwalkTimer = 0f;
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.airwalkBuff.buffIndex, 0);
                }
            }
        }
    


        public void MechStance()
        {

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
                        mechStanceTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                        if (mechStanceTimer > StaticValues.mechStanceStepRate / characterBody.moveSpeed)
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
                            blastAttack.baseDamage = characterBody.damage * StaticValues.mechStanceDamageCoefficient * (characterBody.moveSpeed / characterBody.baseMoveSpeed);
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
        }

        public void WindShield()
        {

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


                if (windshieldTimer <= 1f)
                {
                    windshieldTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                }
                if (windshieldTimer > 1f)
                {
                    windshieldTimer = 0f;
                    new BlastAttack
                    {
                        crit = false,
                        attacker = characterBody.gameObject,
                        teamIndex = TeamComponent.GetObjectTeam(characterBody.gameObject),
                        falloffModel = BlastAttack.FalloffModel.None,
                        baseDamage = characterBody.damage * StaticValues.windShieldDamageCoefficient,
                        damageType = new DamageTypeCombo(DamageType.Stun1s, DamageTypeExtended.Generic, DamageSource.Secondary),
                        damageColorIndex = DamageColorIndex.Default,
                        baseForce = 0,
                        procChainMask = new ProcChainMask(),
                        position = characterBody.transform.position,
                        radius = StaticValues.windShieldRadius,
                        procCoefficient = 0.001f,
                        attackerFiltering = AttackerFiltering.NeverHitSelf,
                    }.Fire();

                    EffectManager.SpawnEffect(Modules.ShiggyAsset.engiShieldEffect, new EffectData
                    {
                        origin = characterBody.transform.position,
                        scale = StaticValues.windShieldRadius,
                        rotation = Quaternion.LookRotation(characterBody.characterDirection.forward)

                    }, false);
                }
            }
        }

        public void MultBuff()
        {

            //multbuff buff
            if (characterBody.HasBuff(Modules.Buffs.multBuff))
            {
                if (multTimer > 1f)
                {
                    multTimer = 0f;
                    EffectManager.SpawnEffect(Modules.ShiggyAsset.multEffect, new EffectData
                    {
                        origin = child.FindChild("LHand").position,
                        scale = 1f,
                        rotation = Quaternion.LookRotation(characterBody.transform.position)

                    }, false);

                    EffectManager.SpawnEffect(Modules.ShiggyAsset.multEffect, new EffectData
                    {
                        origin = child.FindChild("RHand").position,
                        scale = 1f,
                        rotation = Quaternion.LookRotation(characterBody.transform.position)

                    }, false);
                }
                else
                {
                    multTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;

                }
            }
        }

        public void ClayDunestrider()
        {

            //claydunestrider buff
            if (characterBody.HasBuff(Modules.Buffs.claydunestriderBuff))
            {
                if (clayDunestriderTimer > 1f)
                {
                    clayDunestriderTimer = 0f;
                    EffectManager.SpawnEffect(Modules.ShiggyAsset.claydunestriderEffect, new EffectData
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
        }

        public void GreaterWisp()
        {

            //Greaterwisp buff
            if (characterBody.HasBuff(Modules.Buffs.greaterwispBuff))
            {
                if (greaterwispTimer > 1f)
                {
                    greaterwispTimer = 0f;
                    EffectManager.SpawnEffect(Modules.ShiggyAsset.chargegreaterwispBall, new EffectData
                    {
                        origin = child.FindChild("LHand").position,
                        scale = 1f,
                        rotation = Quaternion.LookRotation(characterBody.transform.position)

                    }, false);

                    EffectManager.SpawnEffect(Modules.ShiggyAsset.chargegreaterwispBall, new EffectData
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
        }

        public void VagrantDisableBuff()
        {

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
                else vagranttimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
            }
        }

        public void JellyFishHealStacks()
        {

            //jellyfish heal buff
            if (characterBody.HasBuff(Modules.Buffs.jellyfishHealStacksBuff.buffIndex))
            {

                if (jellyfishtimer > 1f)
                {
                    int jellyfishBuffCount = characterBody.GetBuffCount(Modules.Buffs.jellyfishHealStacksBuff.buffIndex);
                    int jellyfishBuffCountToReduce = Mathf.RoundToInt(characterBody.healthComponent.fullHealth * (StaticValues.JellyfishHealTickRate));
                    int jellyfishBuffCountToApply = jellyfishBuffCount - jellyfishBuffCountToReduce;
                    if (jellyfishBuffCountToApply < 2)
                    {
                        jellyfishBuffCountToApply = 2;
                    }
                    if (jellyfishBuffCount > 1)
                    {
                        if (jellyfishBuffCount >= 2)
                        {
                            characterBody.ApplyBuff(Modules.Buffs.jellyfishHealStacksBuff.buffIndex, jellyfishBuffCountToApply);
                            jellyfishtimer = 0f;
                        }

                    }
                    else if (jellyfishBuffCount <= 1)
                    {
                        jellyfishtimer = 0f;
                    }
                }
                else if(jellyfishtimer <= 1f)
                {
                    jellyfishtimer += Time.fixedDeltaTime;
                }
            }
        }

        public void HalcyoniteGreedBuff()
        {
            //halcyonite greed buff
            if (characterBody.HasBuff(Buffs.halcyoniteGreedBuff))
            {
                if(halcyoniteTimer > StaticValues.halcyoniteGreedInterval)
                {
                    //find half of the total money player owns
                    uint halfMoney = (uint)Mathf.RoundToInt(characterBody.master.money * StaticValues.halcyoniteGreedGoldRatio);
                    int greedBuffStacksToGive = Mathf.RoundToInt(halfMoney / StaticValues.halcyoniteGreedBuffGoldPerStack);

                    characterBody.master.money -= halfMoney;
                    characterBody.ApplyBuff(Buffs.halcyoniteGreedStacksBuff.buffIndex, greedBuffStacksToGive, StaticValues.halcyoniteGreedInterval);

                }
                else if (halcyoniteTimer <= StaticValues.halcyoniteGreedInterval)
                {
                    halcyoniteTimer += Time.fixedDeltaTime;
                }
            }

        }
        private void UpdateDefaultIndicator()
        {
            HurtBox hurtBox = this.trackingTarget;
            if (!hurtBox)
            {
                this.indicator.active = false;
                return;
            }

            this.indicator.active = true;
            this.indicator.targetTransform = hurtBox.transform;

        }

        public void FixedUpdate()
        {
            this.trackerUpdateStopwatch += Time.fixedDeltaTime;
            if (this.trackerUpdateStopwatch >= 1f / this.trackerUpdateFrequency)
            {
                this.trackerUpdateStopwatch -= 1f / this.trackerUpdateFrequency;

                // 1) your existing scan:
                Ray aimRay = new Ray(this.inputBank.aimOrigin, this.inputBank.aimDirection);
                this.SearchForTarget(aimRay);

                UpdateDefaultIndicator();
                

            }
            //UpdateQuirkOverlayIndicators();




            if (characterBody)
            {
                if (characterBody.hasEffectiveAuthority)
                {
                    OFAFO();
                    LightForm();
                    DarknessForm();
                    LightAndDarknessForm();
                    WildCardNoProjectileBuff();
                    DeathAura();
                    TheWorld();
                    OFABuff();
                    MechStance();
                    MultBuff();
                    ClayDunestrider();
                    GreaterWisp();
                    VagrantDisableBuff();
                    JellyFishHealStacks();
                    HalcyoniteGreedBuff();
                }
            }
        }    


        public void Particles()
        {

            //particle effects
            //arm aura
            if (characterBody.hasEffectiveAuthority)
            {

                if (boolenoughEnergyAura || energySystem.currentplusChaos > StaticValues.AFOEnergyCost)
                {
                    if (LARM.isStopped)
                    {
                        LARM.Play();
                    }
                    if (RARM.isStopped)
                    {
                        RARM.Play();
                    }

                }
                else
                {
                    if (LARM.isPlaying)
                    {
                        LARM.Stop();
                    }
                    if (RARM.isPlaying)
                    {
                        RARM.Stop();
                    }
                }

                //sword aura L
                if (boolswordAuraL || characterBody.HasBuff(Buffs.finalReleaseBuff))
                {
                    if (SWORDAURAL.isStopped)
                    {
                        SWORDAURAL.Play();
                    }
                }
                else if (!boolswordAuraL && !characterBody.HasBuff(Buffs.finalReleaseBuff))
                {
                    if (SWORDAURAL.isPlaying)
                    {
                        SWORDAURAL.Stop();
                    }

                }
                //sword aura R
                if (boolswordAuraR || characterBody.HasBuff(Buffs.finalReleaseBuff))
                {
                    if (SWORDAURAR.isStopped)
                    {
                        SWORDAURAR.Play();
                    }
                }
                else if (!boolswordAuraR && !characterBody.HasBuff(Buffs.finalReleaseBuff))
                {
                    if (SWORDAURAR.isPlaying)
                    {
                        SWORDAURAR.Stop();
                    }

                }
                //final release aura and music
                if (characterBody.HasBuff(Buffs.finalReleaseBuff))
                {
                    if (FINALRELEASEAURA.isStopped)
                    {
                        FINALRELEASEAURA.Play();
                    }

                }
                else if (!characterBody.HasBuff(Buffs.finalReleaseBuff))
                {
                    if (FINALRELEASEAURA.isPlaying)
                    {
                        FINALRELEASEAURA.Stop();
                    }
                }
                //OFA aura
                if (booloneForAllAura || characterBody.HasBuff(Buffs.OFABuff) || characterBody.HasBuff(Buffs.OFAFOBuff) || characterBody.HasBuff(Buffs.limitBreakBuff))
                {
                    if (OFA.isStopped)
                    {
                        OFA.Play();
                    }
                }
                else if (!booloneForAllAura && !characterBody.HasBuff(Buffs.OFABuff) && !characterBody.HasBuff(Buffs.OFAFOBuff) && !characterBody.HasBuff(Buffs.limitBreakBuff))
                {
                    if (OFA.isPlaying)
                    {
                        OFA.Stop();
                    }
                }
                //final release buff
                if (characterBody.HasBuff(Buffs.finalReleaseBuff))
                {
                    if (energySystem.currentplusChaos <= 0f)
                    {
                        //Debug.Log("mugetsu");
                        StopFinalReleaseLoop();
                        new SetMugetsuStateMachine(characterBody.masterObjectId).Send(NetworkDestination.Clients);
                    }

                    //sprint to shunpo
                    if (buttonCooler > 0f)
                    {
                        buttonCooler -= Time.deltaTime * OFAFOTimeMultiplier * characterBody.attackSpeed;

                    }
                    if (buttonCooler <= 0f)
                    {
                        if (inputBank.sprint.justPressed)
                        {
                            //energy cost- same as getsuga
                            float plusChaosflatCost = (StaticValues.finalReleaseEnergyCost) - (energySystem.costflatplusChaos);
                            if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;

                            float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                            if (plusChaosCost < 0f) plusChaosCost = 0f;
                            energySystem.SpendplusChaos(plusChaosCost);

                            new SetShunpoStateMachine(characterBody.masterObjectId).Send(NetworkDestination.Clients);
                            buttonCooler += StaticValues.finalReleaseThreshold;

                        }
                    }



                    //fire a getsuga tenshou if holding a button down
                    if (finalReleaseTimer > 0f)
                    {
                        finalReleaseTimer -= Time.deltaTime * OFAFOTimeMultiplier * characterBody.attackSpeed;
                    }
                    if (finalReleaseTimer <= 0f)
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

                            //energy cost
                            float plusChaosflatCost = (StaticValues.finalReleaseEnergyCost) - (energySystem.costflatplusChaos);
                            if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;

                            float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                            if (plusChaosCost < 0f) plusChaosCost = 0f;
                            energySystem.SpendplusChaos(plusChaosCost);

                            finalReleaseTimer += StaticValues.finalReleaseThreshold;


                            Debug.Log("getsuga");
                            new SetGetsugaStateMachine(characterBody.masterObjectId).Send(NetworkDestination.Clients);

                            Ray aimRay = characterBody.inputBank.GetAimRay();

                            EffectManager.SpawnEffect(EntityStates.Vulture.Weapon.FireWindblade.muzzleEffectPrefab, new EffectData
                            {
                                origin = child.FindChild("RHand").position,
                                scale = 1f,
                                rotation = Quaternion.LookRotation(aimRay.direction),

                            }, true);


                            //ProjectileManager.instance.FireProjectile(
                            //    Modules.Asset.mercWindProj, //prefab
                            //    aimRay.origin, //position
                            //    Util.QuaternionSafeLookRotation(aimRay.direction), //rotation
                            //    base.gameObject, //owner
                            //    characterBody.damage* StaticValues.finalReleaseDamageCoefficient, //damage
                            //    200f, //force
                            //    characterBody.RollCrit(), //crit
                            //    DamageColorIndex.Default, //damage color
                            //    null, //target
                            //    -1); //speed }


                        }
                    }

                }
            }
        }

        public void SaveSkills()
        {

            if (characterBody.hasEffectiveAuthority && !saveSkills)
            {
                saveSkills = true;
                //write starting skills to the skill list for shiggymaster
                if (characterBody.skillLocator)
                {
                    Shiggymastercon.writeToSkillList(characterBody.skillLocator.primary.skillDef, 0);
                    Shiggymastercon.writeToSkillList(characterBody.skillLocator.secondary.skillDef, 1);
                    Shiggymastercon.writeToSkillList(characterBody.skillLocator.utility.skillDef, 2);
                    Shiggymastercon.writeToSkillList(characterBody.skillLocator.special.skillDef, 3);

                }
                if (extraskillLocator)
                {
                    Shiggymastercon.writeToSkillList(extraskillLocator.extraFirst.skillDef, 4);
                    Shiggymastercon.writeToSkillList(extraskillLocator.extraSecond.skillDef, 5);
                    Shiggymastercon.writeToSkillList(extraskillLocator.extraThird.skillDef, 6);
                    Shiggymastercon.writeToSkillList(extraskillLocator.extraFourth.skillDef, 7);
                }

                Debug.Log(Shiggymastercon.skillListToOverrideOnRespawn[0].skillName + "skill1");
                Debug.Log(Shiggymastercon.skillListToOverrideOnRespawn[1].skillName + "skill2");
                Debug.Log(Shiggymastercon.skillListToOverrideOnRespawn[2].skillName + "skill3");
                Debug.Log(Shiggymastercon.skillListToOverrideOnRespawn[3].skillName + "skill4");
                Debug.Log(Shiggymastercon.skillListToOverrideOnRespawn[4].skillName + "skill5");
                Debug.Log(Shiggymastercon.skillListToOverrideOnRespawn[5].skillName + "skill6");
                Debug.Log(Shiggymastercon.skillListToOverrideOnRespawn[6].skillName + "skill7");
                Debug.Log(Shiggymastercon.skillListToOverrideOnRespawn[7].skillName + "skill8");
            }
        }

        public void AFO()
        {


            //steal quirk

            //check if button is released for stealing quirk
            if(Input.GetKeyUp(Config.AFOHotkey.Value.MainKey) && characterBody.hasEffectiveAuthority)
            {
                hasStolen = false;
                hasQuirk = false;
                stealQuirkStopwatch = 0f;
            }



            if (trackingTarget)
            {

                if (Input.GetKeyDown(Config.AFOHotkey.Value.MainKey) && characterBody.hasEffectiveAuthority)
                {
                    stealQuirkStopwatch += Time.deltaTime;
                    if (!this.hasStolen && stealQuirkStopwatch > Config.holdButtonAFO.Value && Shiggymastercon.storedAFOSkill[0] == null)
                    {

                        Debug.Log("attempting steal");

                        //check if close enough to activate
                        if (!Config.allowRangedAFO.Value)
                        {
                            var body = trackingTarget.healthComponent.body;
                            float distance = Vector2.Distance(body.corePosition, characterBody.aimOrigin);
                            if (distance > Config.maxAFORange.Value)
                            {
                                Chat.AddMessage($"<style=cIsUtility>Out of AFO Range!</style>");
                                energySystem.quirkGetInformation($"<style=cIsUtility>Out of AFO Range!</style>", 1f);
                                return;
                            }
                        }

                        //energy cost
                        float plusChaosflatCost = (StaticValues.AFOEnergyCost) - (energySystem.costflatplusChaos);
                        if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;

                        float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                        if (plusChaosCost < 0f) plusChaosCost = 0f;

                        if (energySystem.currentplusChaos < plusChaosCost)
                        {
                            Chat.AddMessage($"<style=cIsUtility>Need {plusChaosCost} Plus Chaos!</style>");
                            energySystem.quirkGetInformation($"<style=cIsUtility>Need {plusChaosCost} Plus Chaos!</style>", 1f);
                        }
                        else if (energySystem.currentplusChaos >= plusChaosCost)
                        {
                            energySystem.SpendplusChaos(plusChaosCost);

                            hasStolen = true;
                            Debug.Log("Target");
                            Debug.Log(BodyCatalog.FindBodyPrefab(BodyCatalog.GetBodyName(trackingTarget.healthComponent.body.bodyIndex)));
                            StealQuirk(trackingTarget);

                        }

                    }

                    //Debug.Log(hasStolen + "hasstolen");

                }

                if (UnityEngine.Input.GetKeyDown(Config.AFOGiveHotkey.Value.MainKey) && characterBody.hasEffectiveAuthority && trackingTarget.teamIndex == TeamIndex.Player)
                {
                    giveQuirkStopwatch += Time.deltaTime;
                    if (!this.hasStolen && giveQuirkStopwatch > Config.holdButtonAFO.Value)
                    {

                        //energy cost
                        float plusChaosflatCost = (StaticValues.AFOEnergyCost) - (energySystem.costflatplusChaos);
                        if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;

                        float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                        if (plusChaosCost < 0f) plusChaosCost = 0f;

                        if (energySystem.currentplusChaos < plusChaosCost)
                        {
                            Chat.AddMessage($"<style=cIsUtility>Need {plusChaosCost} Plus Chaos!</style>");
                            energySystem.quirkGetInformation($"<style=cIsUtility>Need {plusChaosCost} Plus Chaos!</style>", 1f);
                        }
                        else if (energySystem.currentplusChaos >= plusChaosCost)
                        {
                            energySystem.SpendplusChaos(plusChaosCost);

                            hasStolen = true;
                            Debug.Log("Target");
                            Debug.Log(BodyCatalog.FindBodyPrefab(BodyCatalog.GetBodyName(trackingTarget.healthComponent.body.bodyIndex)));
                            if (Modules.Config.allowVoice.Value)
                            {
                                AkSoundEngine.PostEvent("ShiggyAFO", this.gameObject);
                            }
                            GiveQuirk(trackingTarget);

                        }

                    }

                    //Debug.Log(hasStolen + "hasstolen");

                }
            }

            //remove quirk

            ////check if button is released for removing quirk
            //if (UnityEngine.Input.GetKeyUp(Config.RemoveHotkey.Value.MainKey))
            //{
            //    hasRemoved = false;
            //    removeQuirkStopwatch = 0f;
            //}

            //if (UnityEngine.Input.GetKeyDown(Config.RemoveHotkey.Value.MainKey) && characterBody.hasEffectiveAuthority)
            //{

            //    removeQuirkStopwatch += Time.deltaTime;
            //    if (!this.hasRemoved && removeQuirkStopwatch > Config.holdButtonAFO.Value)
            //    {
            //        hasRemoved = true;

            //        //choose what quirk to remove
            //        Chat.AddMessage("<style=cIsUtility>Choose which Quirk to Remove</style>");
            //        energySystem.quirkGetInformation("<style=cIsUtility>Choose which Quirk to Remove</style>", 2f);
            //        //unset skills but not to unwrite the skilllist
            //        //RemovePrimary();
            //        //RemoveSecondary();
            //        //RemoveUtility();
            //        //RemoveSpecial();
            //        //RemoveExtra1();
            //        //RemoveExtra2();
            //        //RemoveExtra3();
            //        //RemoveExtra4();

            //        characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggymastercon.skillListToOverrideOnRespawn[0], GenericSkill.SkillOverridePriority.Contextual);
            //        characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggymastercon.skillListToOverrideOnRespawn[1], GenericSkill.SkillOverridePriority.Contextual);
            //        characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggymastercon.skillListToOverrideOnRespawn[2], GenericSkill.SkillOverridePriority.Contextual);
            //        characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggymastercon.skillListToOverrideOnRespawn[3], GenericSkill.SkillOverridePriority.Contextual);
            //        extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggymastercon.skillListToOverrideOnRespawn[4], GenericSkill.SkillOverridePriority.Contextual);
            //        extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggymastercon.skillListToOverrideOnRespawn[5], GenericSkill.SkillOverridePriority.Contextual);
            //        extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggymastercon.skillListToOverrideOnRespawn[6], GenericSkill.SkillOverridePriority.Contextual);
            //        extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggymastercon.skillListToOverrideOnRespawn[7], GenericSkill.SkillOverridePriority.Contextual);


            //        //override skills to removedef
            //        characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
            //        characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
            //        characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
            //        characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
            //        extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
            //        extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
            //        extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
            //        extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
            //    }


            //}
        }

        public void Update()
        {
            //AirWalk();
            SaveSkills();
            Particles();
            //update indicator
            IndicatorUpdater();

            AFO();

        }


        private void IndicatorUpdater()
        {
            if (characterBody.hasEffectiveAuthority)
            {
                //death aura indicator
                if (this.deathAuraIndicatorInstance)
                {
                    this.deathAuraIndicatorInstance.transform.parent = characterBody.transform;
                    this.deathAuraIndicatorInstance.transform.localScale = Vector3.one * (StaticValues.deathAuraRadius + StaticValues.deathAuraRadiusStacks * characterBody.GetBuffCount(Buffs.deathAuraBuff));
                    //this.deathAuraIndicatorInstance.transform.localPosition = Vector3.zero;
                }
            }


            //the world indicator

            //the world indicator + more update stuff
            if (characterBody.HasBuff(Buffs.theWorldBuff))
            {
                //check for energy- if none then remove buff
                if (energySystem.currentplusChaos > StaticValues.theWorldEnergyCost * energySystem.maxPlusChaos)
                {
                    //radius increases overtime
                    overclockTimer += Time.deltaTime * OFAFOTimeMultiplier;
                    float maxRadius = overclockTimer * StaticValues.theWorldCoefficient * StaticValues.theWorldEnergyCost * energySystem.maxPlusChaos;
                    if (maxRadius > StaticValues.theWorldMaxRadius)
                    {
                        maxRadius = StaticValues.theWorldMaxRadius;
                    }
                    if (characterBody.hasEffectiveAuthority)
                    {

                        //arrow rain prefab instance, expanding over time to indicate the radius
                        if (!theWorldIndicatorInstance)
                        {
                            CreateTheWorldIndicator();
                        }

                        if (this.theWorldIndicatorInstance)
                        {
                            this.theWorldIndicatorInstance.transform.parent = characterBody.transform;
                            this.theWorldIndicatorInstance.transform.localScale = Vector3.one * maxRadius;
                            //this.theWorldIndicatorInstance.transform.localPosition = Vector3.zero;
                        }
                    }

                    if (characterBody.isServer)
                    {
                        //freeze projectile 
                        Collider[] array = Physics.OverlapSphere(characterBody.corePosition, maxRadius, LayerIndex.projectile.mask);
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
                                    EffectManager.SpawnEffect(EntityStates.BeetleMonster.HeadbuttState.hitEffectPrefab, effectData, false);
                                    //UnityEngine.Object.Destroy(array[i].gameObject);
                                    Object.Destroy(component.gameObject);
                                }
                            }
                        }

                        //stop enemies from moving
                        BullseyeSearch search = new BullseyeSearch
                        {

                            teamMaskFilter = TeamMask.GetEnemyTeams(characterBody.teamComponent.teamIndex),
                            filterByLoS = false,
                            searchOrigin = characterBody.corePosition,
                            searchDirection = UnityEngine.Random.onUnitSphere,
                            sortMode = BullseyeSearch.SortMode.Distance,
                            maxDistanceFilter = maxRadius,
                            maxAngleFilter = 360f
                        };

                        search.RefreshCandidates();
                        search.FilterOutGameObject(characterBody.gameObject);

                        List<HurtBox> target = search.GetResults().ToList<HurtBox>();
                        foreach (HurtBox singularTarget in target)
                        {
                            if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                            {
                                //stop time for all enemies within this radius
                                if (!singularTarget.healthComponent.body.HasBuff(Buffs.theWorldDebuff))
                                {
                                    singularTarget.healthComponent.body.ApplyBuff(Buffs.theWorldDebuff.buffIndex, 1);

                                    new SetTheWorldFreezeOnBodyRequest(singularTarget.healthComponent.body.masterObjectId).Send(NetworkDestination.Clients);


                                    if (singularTarget.healthComponent.body.master)
                                    {
                                        singularTarget.healthComponent.body.master.enabled = false;
                                        BaseAI[] aiComponents = singularTarget.healthComponent.body.master.aiComponents;
                                        int num2 = aiComponents.Length;
                                        for (int j = 0; j < num2; j++)
                                        {
                                            bool flag9 = aiComponents[j];
                                            if (flag9)
                                            {
                                                aiComponents[j].enabled = false;
                                            }
                                        }
                                    }

                                }
                            }


                        }
                    }
                }
            }
            else if (energySystem.currentplusChaos < 1f && characterBody.HasBuff(Buffs.theWorldBuff))
            {
                characterBody.ApplyBuff(Buffs.theWorldBuff.buffIndex, 0);
                //make sure to reset the timer and instance size 
                overclockTimer = 0f;
                if (this.theWorldIndicatorInstance)
                {
                    this.theWorldIndicatorInstance.SetActive(false);
                    EntityState.Destroy(theWorldIndicatorInstance);

                    //allow time to move for enemies
                    BullseyeSearch search = new BullseyeSearch
                    {

                        teamMaskFilter = TeamMask.GetEnemyTeams(characterBody.teamComponent.teamIndex),
                        filterByLoS = false,
                        searchOrigin = characterBody.corePosition,
                        searchDirection = UnityEngine.Random.onUnitSphere,
                        sortMode = BullseyeSearch.SortMode.Distance,
                        maxDistanceFilter = StaticValues.theWorldMaxRadius * 2f,
                        maxAngleFilter = 360f
                    };

                    search.RefreshCandidates();
                    search.FilterOutGameObject(characterBody.gameObject);

                    List<HurtBox> target = search.GetResults().ToList<HurtBox>();
                    foreach (HurtBox singularTarget in target)
                    {
                        if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                        {
                            //time o ugokidasu
                            if (singularTarget.healthComponent.body.HasBuff(Buffs.theWorldDebuff))
                            {
                                try
                                {
                                    singularTarget.healthComponent.body.ApplyBuff(Buffs.theWorldDebuff.buffIndex, 0);


                                    if (singularTarget.healthComponent.body.master)
                                    {
                                        singularTarget.healthComponent.body.master.enabled = true;
                                        BaseAI[] aiComponents = singularTarget.healthComponent.body.master.aiComponents;
                                        int num2 = aiComponents.Length;
                                        for (int j = 0; j < num2; j++)
                                        {
                                            bool flag9 = aiComponents[j];
                                            if (flag9)
                                            {
                                                aiComponents[j].enabled = true;
                                            }
                                        }
                                    }

                                    if (singularTarget.healthComponent.body.characterMotor)
                                    {
                                        singularTarget.healthComponent.body.characterMotor.enabled = true;
                                    }
                                    Animator animCom = singularTarget.healthComponent.body.modelLocator.modelTransform.GetComponent<Animator>();
                                    if (animCom)
                                    {
                                        animCom.enabled = true;
                                    }
                                }
                                catch
                                {
                                    Debug.LogWarning("failed to unstop everything");
                                }

                            }
                        }
                    }
                }
            }

        }

        //death Aura Indicator 
        private void CreateDeathAuraIndicator()
        {
            if (ShiggyAsset.deathAuraIndicator)
            {
                this.deathAuraIndicatorInstance = Object.Instantiate<GameObject>(ShiggyAsset.deathAuraIndicator);
                this.deathAuraIndicatorInstance.SetActive(true);

                this.deathAuraIndicatorInstance.transform.parent = characterBody.transform;
                this.deathAuraIndicatorInstance.transform.localScale = Vector3.one * (Modules.StaticValues.deathAuraRadius + StaticValues.deathAuraRadiusStacks * characterBody.GetBuffCount(Buffs.deathAuraBuff));
                this.deathAuraIndicatorInstance.transform.localPosition = Vector3.zero;

            }
        }

        //overclock indicator
        private void CreateTheWorldIndicator()
        {
            if (ShiggyAsset.theWorldIndicator)
            {
                this.theWorldIndicatorInstance = Object.Instantiate<GameObject>(ShiggyAsset.theWorldIndicator);
                this.theWorldIndicatorInstance.SetActive(true);

                this.theWorldIndicatorInstance.transform.parent = characterBody.transform;
                this.theWorldIndicatorInstance.transform.localScale = Vector3.one * StaticValues.theWorldCoefficient;
                this.theWorldIndicatorInstance.transform.localPosition = Vector3.zero;

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
        private void GiveQuirk(HurtBox hurtBox)
        {
            AFOEffectController AFOCon = hurtBox.healthComponent.body.gameObject.AddComponent<AFOEffectController>();
            AFOCon.attackerBody = characterBody;
            AFOCon.RHandChild = child.FindChild("RHand").transform;

            Chat.AddMessage("<style=cIsUtility>Choose a Passive Quirk to Give</style>");
            energySystem.quirkGetInformation("<style=cIsUtility>Choose a Passive Quirk to Give</style>", 2f);


            var name = BodyCatalog.GetBodyName(hurtBox.healthComponent.body.bodyIndex);
            GameObject newbodyPrefab = BodyCatalog.FindBodyPrefab(name);
            //new ForceGiveQuirkState(characterBody.masterObjectId, hurtBox.healthComponent.body.masterObjectId).Send(NetworkDestination.Clients);
            giveQuirkBody = hurtBox.healthComponent.body;

            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggymastercon.skillListToOverrideOnRespawn[0], GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggymastercon.skillListToOverrideOnRespawn[1], GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggymastercon.skillListToOverrideOnRespawn[2], GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggymastercon.skillListToOverrideOnRespawn[3], GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggymastercon.skillListToOverrideOnRespawn[4], GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggymastercon.skillListToOverrideOnRespawn[5], GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggymastercon.skillListToOverrideOnRespawn[6], GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggymastercon.skillListToOverrideOnRespawn[7], GenericSkill.SkillOverridePriority.Contextual);

            characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.giveDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.giveDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.giveDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.giveDef, GenericSkill.SkillOverridePriority.Contextual);

            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.giveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.giveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.giveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.giveDef, GenericSkill.SkillOverridePriority.Contextual);

        }

        // ---------------------------------------------
        // Steal (AFO) – body name → Quirk, elites, auto-craft toasts
        // ---------------------------------------------
        private void StealQuirk(HurtBox hurtBox)
        {
            if (hurtBox == null || hurtBox.healthComponent == null) return;

            var body = hurtBox.healthComponent.body;
            if (!body) return;


            string bodyName = BodyCatalog.GetBodyName(body.bodyIndex);
            Debug.Log($"[StealQuirk] targetBody={bodyName} (index={body.bodyIndex})");

            // SFX
            if (Modules.Config.allowVoice.Value)
                AkSoundEngine.PostEvent(bodyName == "DekuBody" ? "ShiggyOFAGet" : "ShiggyAFO", characterBody.gameObject);

            bool grantedAny = false;

            // Quick sanity: registry must be bound or we can't resolve IDs/skills.
            if (QuirkRegistry.All == null || QuirkRegistry.All.Count == 0)
            {
                Debug.LogWarning("[StealQuirk] QuirkRegistry not bound yet. Call QuirkRegistry.BindQuirkRegistry() earlier (e.g., after content packs build and before the run).");
            }

            // 1) Elite affix → passive + equipment
            if (QuirkRegistry.TryGetEliteQuirkId(body, out var eliteId))
            {
                Debug.Log($"[StealQuirk] Elite affix detected -> {eliteId}");
                if (!body.HasBuff(Buffs.eliteDebuff.buffIndex))
                {
                    bool added = QuirkInventory.Add(eliteId);
                    Debug.Log($"[StealQuirk] Add(eliteId={eliteId}) -> {added}");
                    if (added)
                    {
                        ToastQuirkPickup(eliteId);

                        if (QuirkInventory.LastAutoCrafted.Count > 0)
                            Debug.Log($"[StealQuirk] Auto-crafted: {string.Join(", ", QuirkInventory.LastAutoCrafted.Select(QuirkInventory.QuirkPickupUI.MakeNiceName))}");

                        foreach (var crafted in QuirkInventory.LastAutoCrafted)
                            ToastQuirkPickup(crafted);
                    }
                    else
                    {
                        energySystem.quirkGetInformation("Already own this Elite Quirk.", 1.5f);
                    }

                    var eq = QuirkRegistry.GetEliteEquipmentForId(eliteId);
                    if (eq) dropEquipment(eq);

                    body.ApplyBuff(Buffs.eliteDebuff.buffIndex, 1, 60);
                    grantedAny = true;
                }
                else
                {
                    Chat.AddMessage("Can't steal <style=cIsUtility>Elite Quirk</style> until debuff is gone.");
                }
            }

            // 2) Body name map → QuirkId
            if (QuirkTargetingMap.TryGet(bodyName, out var id) && id != QuirkId.None)
            {
                Debug.Log($"[StealQuirk] Body map resolved {bodyName} -> {id}");

                // Optional: verify that this id has a bound SkillDef (so it can appear in the picker).
                if (!QuirkRegistry.TryGet(id, out var rec))
                {
                    Debug.LogWarning($"[StealQuirk] Registry has no record for {id}. Did you call QuirkRegistry.Build()/BindQuirkRegistry()?");
                }
                else
                {
                    Debug.Log($"[StealQuirk] Registry record ok. SkillDef={(rec.Skill ? rec.Skill.name : "null")} Category={rec.Category}");
                }

                bool newlyOwned = QuirkInventory.Add(id);
                Debug.Log($"[StealQuirk] Add(id={id}) -> {newlyOwned}. Owned now: {QuirkInventory.Owned.Count}");

                if (newlyOwned)
                {
                    // Visual flourish
                    var AFOCon = hurtBox.healthComponent.body.gameObject.AddComponent<AFOEffectController>();
                    AFOCon.attackerBody = characterBody;
                    AFOCon.RHandChild = child.FindChild("RHand").transform;

                    ToastQuirkPickup(id);
                    grantedAny = true;

                    if (QuirkInventory.LastAutoCrafted.Count > 0)
                        Debug.Log($"[StealQuirk] Auto-crafted: {string.Join(", ", QuirkInventory.LastAutoCrafted.Select(QuirkInventory.QuirkPickupUI.MakeNiceName))}");

                    foreach (var crafted in QuirkInventory.LastAutoCrafted)
                        ToastQuirkPickup(crafted);

                    // The QuirkUI subscribes to QuirkInventory.OnOwnedChanged and will rebuild its pool.
                    // If the picker is open right now and you want an immediate refresh, you can nudge it:
                    if (QuirkUI.Current != null)
                    {
                        Debug.Log("[StealQuirk] QuirkUI is open; nudging it to rebuild its pool.");
                        // The UI already listens to OnOwnedChanged, but in case timing matters:
                        // (This assumes you added a public method to force-refresh the open picker list, otherwise it will
                        // rebuild next time you open the modal. If not present, this log is enough to confirm the event fired.)
                        // QuirkUI.Current.ForceRefreshOpenPicker(); // (optional if you add such a method)
                    }
                }
                else
                {
                    Chat.AddMessage("<style=cDeath>Already have this Quirk!</style>");
                    energySystem.quirkGetInformation("Already have this Quirk!", 2f);
                }

                // force overlay to refresh ownership check immediately
                _lastQuirkId = QuirkId.None;
            }
            else
            {
                Debug.Log($"[StealQuirk] No body->quirk mapping for {bodyName} (or mapped to None).");
            }

            //stop the enemy in their tracks
            SetStateOnHurt component = body.healthComponent.GetComponent<SetStateOnHurt>();
            bool flag = component == null;
            if (!flag)
            {
                bool canBeHitStunned = component.canBeHitStunned;
                if (canBeHitStunned)
                {
                    component.SetPain();
                    if (body.characterMotor)
                    {
                        body.characterMotor.velocity = Vector3.zero;
                    }
                    if (body.rigidbody != null)
                    {
                        body.rigidbody.velocity = Vector3.zero;
                    }
                }
            }

            // 3) Refund if we truly found nothing
            if (!grantedAny)
            {
                Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
                energySystem.quirkGetInformation("No Quirk to <style=cIsUtility>Steal!</style>", 2f);

                float plusChaosflatCost = StaticValues.AFOEnergyCost - energySystem.costflatplusChaos;
                if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;
                float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                if (plusChaosCost < 0f) plusChaosCost = 0f;
                energySystem.GainplusChaos(plusChaosCost);
            }
        }






        void ToastQuirkPickup(QuirkId id)
        {
            var msg = QuirkInventory.QuirkPickupUI.BuildPickupText(id);
            Chat.AddMessage(msg);
            energySystem.quirkGetInformation(msg, 2f);
        }

    }



}


