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
using Object = UnityEngine.Object;
using static UnityEngine.ParticleSystem.PlaybackState;
using EntityStates.VoidMegaCrab.BackWeapon;
using RiskOfOptions.Components.Panel;

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
        private float OFATimer;
        private float voidFormTimer;
        private float overclockTimer;
        private float deathAuraTimer;
        private float OFAFOTimer;
        public float OFAFOTimeMultiplier;
        private float finalReleaseTimer;
        private float lastTapTime;
        private float tapSpeed = StaticValues.finalReleaseTapSpeed;
        private bool doubleTap;

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
        private float theWorldTimer;

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
            if (!energySystem)
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

                //one for all for one buff
                if (characterBody.HasBuff(Buffs.OFAFOBuff))
                {
                    //check energy if enough to use ability
                    if (energySystem.currentplusChaos < energySystem.maxPlusChaos * StaticValues.OFAFOEnergyCostCoefficient)
                    {
                        characterBody.ApplyBuff(Buffs.OFAFOBuff.buffIndex, 0);
                    }

                    //doubling the speed for all timers- besides this one
                    OFAFOTimeMultiplier = StaticValues.OFAFOETimeMultiplierCoefficient;

                    if (OFAFOTimer < StaticValues.OFAFOThreshold)
                    {
                        OFAFOTimer += Time.fixedDeltaTime;
                    }
                    else if (OFAFOTimer >= StaticValues.OFAFOThreshold)
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
                        if (deathAuraTimer < StaticValues.deathAuraThreshold)
                        {
                            deathAuraTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                        }
                        else if (deathAuraTimer >= StaticValues.deathAuraThreshold)
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

                //the world buff energy cost
                if (characterBody.HasBuff(Buffs.theWorldBuff))
                {
                    if (theWorldTimer < 1f)
                    {
                        theWorldTimer += Time.fixedDeltaTime;
                    }
                    else if (theWorldTimer >= 1f)
                    {
                        //energy cost
                        float plusChaosflatCost = (StaticValues.theWorldEnergyCost * energySystem.maxPlusChaos) - (energySystem.costflatplusChaos);
                        if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;

                        float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                        if (plusChaosCost < 0f) plusChaosCost = 0f;
                        energySystem.SpendplusChaos(plusChaosCost);

                    }
                }


                //void form buff
                if (characterBody.HasBuff(Buffs.voidFormBuff))
                {
                    if (voidFormTimer < StaticValues.voidFormThreshold)
                    {
                        voidFormTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                    }
                    else if (voidFormTimer >= StaticValues.voidFormThreshold)
                    {
                        voidFormTimer = 0f;
                        //take damage every second based off current hp, cleanse self as well
                        new SpendHealthNetworkRequest(characterBody.masterObjectId, characterBody.healthComponent.combinedHealth * StaticValues.voidFormHealthCostCoefficient).Send(NetworkDestination.Clients);

                        Util.CleanseBody(characterBody, true, false, false, true, true, true);
                    }
                }

                //OFA
                if (characterBody.HasBuff(Buffs.OFABuff))
                {
                    //play ofa particles here

                    if (OFATimer < StaticValues.OFAThreshold)
                    {
                        OFATimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
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
                                //constantly draining energy cost for air walk
                                float plusChaosflatCost = StaticValues.airwalkEnergyFraction - (energySystem.costflatplusChaos * StaticValues.costFlatContantlyDrainingCoefficient);
                                if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;

                                float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                                if (plusChaosCost < 0f) plusChaosCost = 0f;
                                energySystem.SpendplusChaos(plusChaosCost);
                                characterBody.ApplyBuff(Modules.Buffs.airwalkBuff.buffIndex, 1);

                                //if falling down
                                if (characterBody.characterMotor.velocity.y < 0)
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
                            mechStanceTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
                            if (mechStanceTimer >= StaticValues.mechStanceStepRate / characterBody.moveSpeed)
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
                        windshieldTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;
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
                        multTimer += Time.fixedDeltaTime * OFAFOTimeMultiplier;

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




                //jellyfish heal buff
                if (characterBody.HasBuff(Modules.Buffs.jellyfishHealStacksBuff.buffIndex))
                {

                    if (jellyfishtimer > 1f)
                    {
                        int jellyfishBuffCount = characterBody.GetBuffCount(Modules.Buffs.jellyfishHealStacksBuff.buffIndex);
                        int jellyfishBuffCountToReduce = Mathf.RoundToInt(characterBody.maxHealth * (1 - StaticValues.JellyfishHealTickRate));
                        int jellyfishBuffCountToApply = jellyfishBuffCount - jellyfishBuffCountToReduce;
                        if (jellyfishBuffCountToApply < 2)
                        {
                            jellyfishBuffCountToApply = 2;
                        }
                        if (jellyfishBuffCount > 1)
                        {
                            if (jellyfishBuffCount >= 2)

                                characterBody.ApplyBuff(Modules.Buffs.jellyfishHealStacksBuff.buffIndex, jellyfishBuffCountToApply);
                            jellyfishtimer = 0f;
                        }
                    }
                }
                else jellyfishtimer += Time.fixedDeltaTime;
                
            }
        }    





        public void Update()
        {

            //final release buff
            if (characterBody.HasBuff(Buffs.finalReleaseBuff))
            {
                if(energySystem.currentplusChaos < 1f)
                {
                    new SetMugetsuStateMachine(characterBody.masterObjectId).Send(NetworkDestination.Clients);
                }
                //energy cost
                float plusChaosflatCost = (StaticValues.finalReleaseEnergyCost) - (energySystem.costflatplusChaos * StaticValues.costFlatContantlyDrainingCoefficient);
                if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;

                float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                if (plusChaosCost < 0f) plusChaosCost = 0f;
                energySystem.SpendplusChaos(plusChaosCost);

                //double tap to shunpo
                if (inputBank.jump.down)
                {
                    lastTapTime = Time.time;
                }
                if(inputBank.jump.down && ((Time.time - lastTapTime) < tapSpeed || doubleTap))
                {
                    doubleTap = true;
                }
                else
                {
                    doubleTap = false;
                }
                
                if(doubleTap)
                {
                    new SetShunpoStateMachine(characterBody.masterObjectId).Send(NetworkDestination.Clients);
                }

                //fire a getsuga tenshou if holding a button down
                if (finalReleaseTimer >= 0f)
                {
                    finalReleaseTimer -= Time.deltaTime * OFAFOTimeMultiplier;
                }
                if (finalReleaseTimer < 0f)
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
                        finalReleaseTimer += StaticValues.finalReleaseThreshold / characterBody.attackSpeed;


                        new SetGetsugaStateMachine(characterBody.masterObjectId).Send(NetworkDestination.Clients);

                    }
                }
                
            }


            //update indicator
            IndicatorUpdater();

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

        private void IndicatorUpdater()
        {

            //death aura indicator
            if (this.deathAuraIndicatorInstance)
            {
                this.deathAuraIndicatorInstance.transform.localScale = Vector3.one * (StaticValues.deathAuraRadius + StaticValues.deathAuraRadiusStacks * characterBody.GetBuffCount(Buffs.deathAuraBuff));
                this.deathAuraIndicatorInstance.transform.localPosition = characterBody.corePosition;
            }

            //the world indicator + more update stuff
            if (characterBody.HasBuff(Buffs.theWorldBuff))
            {
                //check for energy- if none then remove buff
                if (energySystem.currentplusChaos > StaticValues.theWorldEnergyCost * energySystem.maxPlusChaos)
                {
                    //radius increases overtime
                    overclockTimer += Time.deltaTime * OFAFOTimeMultiplier;
                    float maxRadius = overclockTimer * StaticValues.theWorldCoefficient;
                    if (maxRadius > 400f)
                    {
                        maxRadius = 400f;
                    }


                    //arrow rain prefab instance, expanding over time to indicate the radius
                    if (!theWorldIndicatorInstance)
                    {
                        CreateTheWorldIndicator();
                    }
                    if (this.theWorldIndicatorInstance)
                    {
                        this.theWorldIndicatorInstance.transform.localScale = Vector3.one * maxRadius;
                        this.theWorldIndicatorInstance.transform.localPosition = characterBody.corePosition;
                    }
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

                                SetStateOnHurt component = singularTarget.healthComponent.body.healthComponent.GetComponent<SetStateOnHurt>();
                                bool flag = component == null;
                                if (!flag)
                                {
                                    bool canBeHitStunned = component.canBeFrozen;
                                    if (canBeHitStunned)
                                    {
                                        component.SetFrozen(1);
                                        bool flag2 = singularTarget.healthComponent.body.characterMotor;
                                        if (flag2)
                                        {
                                            singularTarget.healthComponent.body.characterMotor.velocity = Vector3.zero;
                                        }
                                    }
                                }
                            }
                        }

                        
                    }


                }
                else if (energySystem.currentplusChaos <= 1f)
                {
                    characterBody.ApplyBuff(Buffs.theWorldBuff.buffIndex, 0);
                }

                
            }
            else if (!characterBody.HasBuff(Buffs.theWorldBuff))
            {
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
                        maxDistanceFilter = StaticValues.theWorldMaxRadius,
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
                            if (singularTarget.healthComponent.body.HasBuff(Buffs.theWorldDebuff))
                            {
                                singularTarget.healthComponent.body.ApplyBuff(Buffs.theWorldDebuff.buffIndex, 0);
                            }
                        }
                    }

                    //freeze projectile 
                    Collider[] array = Physics.OverlapSphere(characterBody.corePosition, 250f, LayerIndex.projectile.mask);
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
                                component.enabled = false;

                            }
                        }
                    }
                }


            }
        }

        //death Aura Indicator 
        private void CreateDeathAuraIndicator()
        {
            if (Assets.deathAuraIndicator)
            {
                this.deathAuraIndicatorInstance = Object.Instantiate<GameObject>(Assets.deathAuraIndicator);
                this.deathAuraIndicatorInstance.SetActive(true);

                this.deathAuraIndicatorInstance.transform.localScale = Vector3.one * (Modules.StaticValues.deathAuraRadius + StaticValues.deathAuraRadiusStacks * characterBody.GetBuffCount(Buffs.deathAuraBuff));
                this.deathAuraIndicatorInstance.transform.localPosition = characterBody.corePosition;

            }
        }

        //overclock indicator
        private void CreateTheWorldIndicator()
        {
            if (Assets.theWorldIndicator)
            {
                this.theWorldIndicatorInstance = Object.Instantiate<GameObject>(Assets.theWorldIndicator);
                this.theWorldIndicatorInstance.SetActive(true);

                this.theWorldIndicatorInstance.transform.localScale = Vector3.one * StaticValues.theWorldCoefficient;
                this.theWorldIndicatorInstance.transform.localPosition = characterBody.corePosition;

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


