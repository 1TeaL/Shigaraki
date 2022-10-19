using BepInEx;
using BepInEx.Bootstrap;
using ShiggyMod.Equipment;
using ShiggyMod.Items;
using ShiggyMod.Modules;
//using ShiggyMod.Modules.Networking;
using ShiggyMod.Modules.Survivors;
using ShiggyMod.SkillStates;
using EntityStates;
using R2API;
using R2API.Networking;
using R2API.Utils;
using RoR2;
using RoR2.Projectile;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using RoR2.Orbs;
using EmotesAPI;
using EntityStates.JellyfishMonster;
using ShiggyMod.Modules.Networking;

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[module: UnverifiableCode]

namespace ShiggyMod
{
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.KingEnderBrine.ExtraSkillSlots", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.rune580.riskofoptions", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.weliveinasociety.CustomEmotesAPI", BepInDependency.DependencyFlags.SoftDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID, MODNAME, MODVERSION)]
    [R2APISubmoduleDependency(new string[]
    {
        "PrefabAPI",
        "LanguageAPI",
        "SoundAPI",
        "DotAPI",
        "RecalculateStatsAPI",
        "NetworkingAPI",
    })]

    public class ShiggyPlugin : BaseUnityPlugin
    {
        // if you don't change these you're giving permission to deprecate the mod-
        //  please change the names to your own stuff, thanks
        //   this shouldn't even have to be said

        public static bool scepterInstalled = false;

        public ShiggyController Shiggycon;
        public ShiggyMasterController Shiggymastercon;

        public const string MODUID = "com.TeaL.ShigarakiMod";
        public const string MODNAME = "ShigarakiMod";
        public const string MODVERSION = "1.3.0";

        // a prefix for name tokens to prevent conflicts- please capitalize all name tokens for convention
        public const string developerPrefix = "TEAL";

        internal List<SurvivorBase> Survivors = new List<SurvivorBase>();

        public static ShiggyPlugin instance;
        public static CharacterBody ShiggyCharacterBody;


        //public List<ItemBase> Items = new List<ItemBase>();
        //public List<EquipmentBase> Equipments = new List<EquipmentBase>();

        //public static Dictionary<ItemBase, bool> ItemStatusDictionary = new Dictionary<ItemBase, bool>();
        //public static Dictionary<EquipmentBase, bool> EquipmentStatusDictionary = new Dictionary<EquipmentBase, bool>();
        private BlastAttack blastAttack;
        private int decayCount;

        private void Awake()
        {
            instance = this;
            ShiggyCharacterBody = null;
            ShiggyPlugin.instance = this;

            Modules.StaticValues.LoadDictionary(); // load dictionary to differentiate between passives and actives
            // load assets and read config
            Modules.Assets.Initialize();
            Modules.Config.ReadConfig();
            if (Chainloader.PluginInfos.ContainsKey("com.rune580.riskofoptions")) //risk of options support
            {
                Modules.Config.SetupRiskOfOptions();
            }
            Modules.States.RegisterStates(); // register states for networking
            Modules.Buffs.RegisterBuffs(); // add and register custom buffs/debuffs
            Modules.Dots.RegisterDots(); // add and register custom dots
            Modules.Projectiles.RegisterProjectiles(); // add and register custom projectiles
            Modules.Tokens.AddTokens(); // register name tokens
            Modules.ItemDisplays.PopulateDisplays(); // collect item display prefabs for use in our display rules

            // survivor initialization
            new Shiggy().Initialize();

            //networking
            NetworkingAPI.RegisterMessageType<EquipmentDropNetworked>();


            // now make a content pack and add it- this part will change with the next update
            new Modules.ContentPacks().Initialize();

            RoR2.ContentManagement.ContentManager.onContentPacksAssigned += LateSetup;

            Hook();

        }
        private void LateSetup(HG.ReadOnlyArray<RoR2.ContentManagement.ReadOnlyContentPack> obj)
        {
            // have to set item displays later now because they require direct object references..
            Modules.Survivors.Shiggy.instance.SetItemDisplays();
        }

        private void Hook()
        {
            // run hooks here, disabling one is as simple as commenting out the line
            On.RoR2.CharacterBody.OnDeathStart += CharacterBody_OnDeathStart;
            On.RoR2.CharacterModel.Awake += CharacterModel_Awake;
            //On.RoR2.CharacterBody.Start += CharacterBody_Start;
            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;
            On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
            //On.RoR2.TeleporterInteraction.FinishedState.OnEnter += TeleporterInteraction_FinishedState;
            GlobalEventManager.onServerDamageDealt += GlobalEventManager_OnDamageDealt;
            //On.RoR2.CharacterBody.FixedUpdate += CharacterBody_FixedUpdate;
            //On.RoR2.CharacterBody.Update += CharacterBody_Update;
            On.RoR2.CharacterModel.UpdateOverlays += CharacterModel_UpdateOverlays;
            On.RoR2.GlobalEventManager.OnHitEnemy += GlobalEventManager_OnHitEnemy;
            R2API.RecalculateStatsAPI.GetStatCoefficients += RecalculateStatsAPI_GetStatCoefficients;

            if (Chainloader.PluginInfos.ContainsKey("com.weliveinasociety.CustomEmotesAPI"))
            {
                On.RoR2.SurvivorCatalog.Init += SurvivorCatalog_Init;
            }
        }


        private void RecalculateStatsAPI_GetStatCoefficients(CharacterBody sender, RecalculateStatsAPI.StatHookEventArgs args)
        {
            if (sender)
            {

                if (sender.baseNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
                {
                    //roboballmini attackspeed buff
                    if (sender.HasBuff(Buffs.roboballminiattackspeedBuff))
                    {
                        args.baseAttackSpeedAdd += (Modules.StaticValues.roboballattackspeedMultiplier * sender.GetBuffCount(Buffs.roboballminiattackspeedBuff));

                    }
                    //claydunestrider buff
                    if (sender.HasBuff(Buffs.claydunestriderBuff))
                    {
                        args.baseAttackSpeedAdd += (StaticValues.claydunestriderAttackSpeed-1);

                    }
                    //mult buff
                    if (sender.HasBuff(Buffs.multBuff))
                    {
                        args.armorAdd += StaticValues.multArmor;
                        args.attackSpeedMultAdd += (StaticValues.multAttackspeed - 1);
                        args.moveSpeedReductionMultAdd += (1 - StaticValues.multMovespeed);

                    }
                    //stonetitanarmor buff
                    if (sender.HasBuff(Buffs.stonetitanBuff))
                    {
                        args.armorAdd += StaticValues.stonetitanarmorGain;
                    }
                    //voidbarnaclemortarattackspeed buff
                    if (sender.HasBuff(Buffs.voidbarnaclemortarattackspeedBuff))
                    {
                        args.baseAttackSpeedAdd += StaticValues.voidmortarattackspeedGain * sender.GetBuffCount(Buffs.voidbarnaclemortarattackspeedBuff);

                    }
                    //hermitcrabmortararmor buff
                    if (sender.HasBuff(Buffs.hermitcrabmortararmorBuff))
                    {
                        args.armorAdd += StaticValues.mortararmorGain * sender.GetBuffCount(Buffs.hermitcrabmortararmorBuff);
                    }
                    //verminsprint buff
                    if (sender.HasBuff(Buffs.verminsprintBuff))
                    {
                        args.moveSpeedMultAdd += (StaticValues.verminmovespeedMultiplier - 1);
                        sender.sprintingSpeedMultiplier = Modules.StaticValues.verminsprintMultiplier;
                    }
                    //fly buff
                    if (sender.HasBuff(Buffs.flyBuff))
                    {
                        args.moveSpeedMultAdd += (0.5f);
                        sender.acceleration *= 2f;
                        sender.bodyFlags |= CharacterBody.BodyFlags.IgnoreFallDamage;
                        sender.characterMotor.useGravity = false;
                    }
                    else
                    {
                        sender.bodyFlags &= ~CharacterBody.BodyFlags.IgnoreFallDamage;
                        sender.characterMotor.useGravity = true;

                    }
                    //beetlebuff
                    if (sender.HasBuff(Buffs.beetleBuff))
                    {
                        args.damageMultAdd += (StaticValues.beetledamageMultiplier - 1);
                    }
                    //lesserwispbuff
                    if (sender.HasBuff(Buffs.lesserwispBuff))
                    {
                        args.damageMultAdd += (StaticValues.lesserwispdamageMultiplier - 1);
                    }

                }

            }

        }

        private void GlobalEventManager_OnHitEnemy(On.RoR2.GlobalEventManager.orig_OnHitEnemy orig, GlobalEventManager self, DamageInfo damageInfo, GameObject victim)
        {
            orig.Invoke(self, damageInfo, victim);

            var attacker = damageInfo.attacker;
            if (attacker)
            {
                var body = attacker.GetComponent<CharacterBody>();
                var victimBody = victim.GetComponent<CharacterBody>();
                DamageType damageType2;
                if (body.HasBuff(Buffs.impbossBuff))
                {
                    damageType2 = DamageType.BleedOnHit | DamageType.Stun1s;
                }
                else
                {
                    damageType2 = DamageType.Stun1s;
                }
                if (body && victimBody)
                {
                    //commando buff
                    if (body.HasBuff(Modules.Buffs.commandoBuff))
                    {
                        if(damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                        {
                            DamageInfo damageInfo2 = new DamageInfo();
                            damageInfo2.damage = damageInfo.damage * StaticValues.commandoDamageMultiplier;
                            damageInfo2.position = victimBody.corePosition;
                            damageInfo2.force = Vector3.zero;
                            damageInfo2.procCoefficient = StaticValues.commandoProcCoefficient;
                            damageInfo2.damageColorIndex = DamageColorIndex.WeakPoint;
                            damageInfo2.crit = false;
                            damageInfo2.attacker = body.gameObject;
                            damageInfo2.inflictor = victimBody.gameObject;
                            damageInfo2.damageType = damageInfo.damageType;
                            damageInfo2.procCoefficient = 0f;
                            damageInfo2.procChainMask = default(ProcChainMask);
                            victimBody.healthComponent.TakeDamage(damageInfo2);
                        }
                    }

                    //vagrant buff
                    if (body.HasBuff(Modules.Buffs.vagrantBuff) && !victimBody.HasBuff(Modules.Buffs.vagrantdisableBuff))
                    {
                        if (damageInfo.damage / body.damage >= StaticValues.vagrantdamageThreshold)
                        {
                            body.ApplyBuff(Modules.Buffs.vagrantBuff.buffIndex, 0);
                            body.ApplyBuff(Buffs.vagrantdisableBuff.buffIndex, StaticValues.vagrantCooldown);
                            victimBody.AddTimedBuffAuthority(Buffs.vagrantDebuff.buffIndex, StaticValues.vagrantCooldown);
                            Util.PlaySound(JellyNova.novaSoundString, base.gameObject);
                            
                            if (JellyNova.novaEffectPrefab)
                            {
                                EffectManager.SpawnEffect(JellyNova.novaEffectPrefab, new EffectData
                                {
                                    origin = victimBody.transform.position,
                                    scale = StaticValues.vagrantRadius * body.attackSpeed/3
                                }, true);
                            }
                            new BlastAttack
                            {
                                attacker = damageInfo.attacker.gameObject,
                                teamIndex = TeamComponent.GetObjectTeam(damageInfo.attacker.gameObject),
                                falloffModel = BlastAttack.FalloffModel.None,
                                baseDamage = body.damage * StaticValues.vagrantDamageCoefficient * body.attackSpeed / 3,
                                damageType = damageType2,
                                damageColorIndex = DamageColorIndex.Fragile,
                                baseForce = 0,
                                position = victimBody.transform.position,
                                radius = StaticValues.vagrantRadius * body.attackSpeed / 3,
                                procCoefficient = 1f,
                                attackerFiltering = AttackerFiltering.NeverHitSelf,
                            }.Fire();
                            

                        }
                    }
                    //acrid buff
                    if (body.HasBuff(Modules.Buffs.claydunestriderBuff))
                    {
                        if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                        {
                            damageInfo.damageType |= DamageType.ClayGoo;
                        }
                    }
                    //acrid buff
                    if (body.HasBuff(Modules.Buffs.acridBuff))
                    {
                        if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                        {
                            if (victimBody.healthComponent)
                            {
                                InflictDotInfo info = new InflictDotInfo();
                                info.attackerObject = body.gameObject;
                                info.victimObject = victimBody.healthComponent.body.gameObject;
                                info.duration = Modules.StaticValues.decayDamageTimer;
                                info.dotIndex = DotController.DotIndex.Poison;

                                DotController.InflictDot(ref info);
                            }
                        }
                    }
                    //impboss buff
                    if (body.HasBuff(Modules.Buffs.impbossBuff))
                    {
                        if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                        {
                            if (victimBody.healthComponent)
                            {
                                InflictDotInfo info = new InflictDotInfo();
                                info.attackerObject = body.gameObject;
                                info.victimObject = victimBody.healthComponent.body.gameObject;
                                info.duration = Modules.StaticValues.decayDamageTimer/2;
                                info.dotIndex = DotController.DotIndex.Bleed;

                                DotController.InflictDot(ref info);
                            }
                        }
                    }
                    //greaterwisp buff
                    if (body.HasBuff(Modules.Buffs.greaterwispBuff))
                    {
                        if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT && damageInfo.procCoefficient != 0f)
                        {
                            EffectManager.SpawnEffect(Modules.Assets.chargegreaterwispBall, new EffectData
                            {
                                origin = victimBody.transform.position,
                                scale = 6f,
                                rotation = Util.QuaternionSafeLookRotation(damageInfo.force)
                            }, true);
                            new BlastAttack
                            {
                                attacker = damageInfo.attacker.gameObject,
                                teamIndex = TeamComponent.GetObjectTeam(damageInfo.attacker.gameObject),
                                falloffModel = BlastAttack.FalloffModel.None,
                                baseDamage = damageInfo.damage * StaticValues.greaterwispballDamageCoeffecient,
                                damageType = damageInfo.damageType,
                                damageColorIndex = DamageColorIndex.Count,
                                baseForce = 0,
                                procChainMask = damageInfo.procChainMask,
                                position = victimBody.transform.position,
                                radius = 6f,
                                procCoefficient = 0f,
                                attackerFiltering = AttackerFiltering.NeverHitSelf,
                            }.Fire();
                            
                        }
                    }
                }
            }
        }

        private void CharacterBody_OnDeathStart(On.RoR2.CharacterBody.orig_OnDeathStart orig, CharacterBody self)
        {
            orig.Invoke(self);

            if (self)
            {
                if (self.baseNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
                {
                    Shiggycon = self.GetComponent<ShiggyController>();

                    if (Shiggycon.overloadingWard) EntityState.Destroy(Shiggycon.overloadingWard);
                    if (Shiggycon.mushroomWard) EntityState.Destroy(Shiggycon.mushroomWard);
                    if (Shiggycon.magmawormWard) EntityState.Destroy(Shiggycon.magmawormWard);
                    if (Shiggycon.mortarIndicatorInstance) EntityState.Destroy(Shiggycon.mortarIndicatorInstance.gameObject);
                    if (Shiggycon.voidmortarIndicatorInstance) EntityState.Destroy(Shiggycon.voidmortarIndicatorInstance.gameObject);
                    AkSoundEngine.PostEvent(2108803202, this.gameObject);
                }

            }
        }

        //EMOTES
        private void SurvivorCatalog_Init(On.RoR2.SurvivorCatalog.orig_Init orig)
        {
            orig();
            foreach (var item in SurvivorCatalog.allSurvivorDefs)
            {
                //Debug.Log(item.bodyPrefab.name);
                if (item.bodyPrefab.name == "ShiggyBody")
                {
                    CustomEmotesAPI.ImportArmature(item.bodyPrefab, Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("humanoidShigaraki"));
                }
            }
        }

        private void GlobalEventManager_OnDamageDealt(DamageReport report)
        {

            bool flag = !report.attacker || !report.attackerBody;

            if (!flag && report.attackerBody.HasBuff(Modules.Buffs.claydunestriderBuff))
            {
                CharacterBody attackerBody = report.attackerBody;
                attackerBody.healthComponent.Heal(report.damageDealt * Modules.StaticValues.claydunestriderHealCoefficient, default(ProcChainMask), true);

            }
        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            if (self)
            {
                if (damageInfo != null && damageInfo.attacker && damageInfo.attacker.GetComponent<CharacterBody>())
                {
                    //multiplier remove
                    if (damageInfo.attacker.GetComponent<CharacterBody>().HasBuff(Buffs.multiplierBuff))
                    {
                        if ((damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                        {
                            damageInfo.attacker.GetComponent<CharacterBody>().ApplyBuff(Buffs.multiplierBuff.buffIndex, 0);
                        }
                    }
                    if (self.body.baseNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
                    {
                        bool flag = (damageInfo.damageType & DamageType.BypassArmor) > DamageType.Generic;
                        if (!flag && damageInfo.damage > 0f)
                        {
                            //stone titan buff
                            if (self.body.HasBuff(Buffs.stonetitanBuff.buffIndex))
                            {
                                if (self.combinedHealthFraction < 0.5f && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                                {
                                    damageInfo.force = Vector3.zero;
                                    damageInfo.damage -= self.body.armor;
                                    if (damageInfo.damage < 0f)
                                    {
                                        self.Heal(Mathf.Abs(damageInfo.damage), default(RoR2.ProcChainMask), true);
                                        damageInfo.damage = 0f;

                                    }

                                }
                                else
                                {
                                    damageInfo.force = Vector3.zero;
                                    damageInfo.damage = Mathf.Max(1f, damageInfo.damage - self.body.armor);
                                }
                            }

                            //alpha construct shield
                            if (self.body.HasBuff(Modules.Buffs.alphashieldonBuff.buffIndex))
                            {
                                if (damageInfo.attacker.gameObject.GetComponent<CharacterBody>().baseNameToken
                                    != ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
                                {
                                    EffectData effectData2 = new EffectData
                                    {
                                        origin = damageInfo.position,
                                        rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                                    };
                                    EffectManager.SpawnEffect(HealthComponent.AssetReferences.bearVoidEffectPrefab, effectData2, true);
                                    damageInfo.rejected = true;
                                    self.body.ApplyBuff(Modules.Buffs.alphashieldonBuff.buffIndex, 0);
                                    self.body.ApplyBuff(Modules.Buffs.alphashieldoffBuff.buffIndex, StaticValues.alphaconstructCooldown);
                                }

                            }
                            //spike buff
                            if (self.body.HasBuff(Modules.Buffs.gupspikeBuff.buffIndex))
                            {
                                //Spike buff

                                if (damageInfo.attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Modules.Buffs.multiplierBuff))
                                {
                                    decayCount = (int)Modules.StaticValues.multiplierCoefficient;
                                }
                                else
                                {
                                    decayCount = 1;
                                }

                                var damageInfo2 = new DamageInfo();

                                blastAttack = new BlastAttack();
                                blastAttack.radius = Modules.StaticValues.spikedamageRadius;
                                blastAttack.procCoefficient = 0.5f;
                                blastAttack.position = self.transform.position;
                                blastAttack.attacker = self.gameObject;
                                blastAttack.crit = Util.CheckRoll(self.body.crit, self.body.master);
                                blastAttack.baseDamage = self.body.damage * Modules.StaticValues.spikedamageCoefficient;
                                blastAttack.falloffModel = BlastAttack.FalloffModel.None;
                                blastAttack.baseForce = 100f;
                                blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
                                blastAttack.damageType = DamageType.Generic | DamageType.BleedOnHit;
                                blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;

                                if (damageInfo.attacker.gameObject.GetComponent<CharacterBody>().baseNameToken
                                    != ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
                                {
                                    blastAttack.Fire();
                                }

                                EffectManager.SpawnEffect(Modules.Assets.GupSpikeEffect, new EffectData
                                {
                                    origin = self.transform.position,
                                    scale = Modules.StaticValues.spikedamageRadius / 3,
                                    rotation = Quaternion.LookRotation(self.transform.position)

                                }, true);

                                BullseyeSearch search = new BullseyeSearch
                                {

                                    teamMaskFilter = TeamMask.GetEnemyTeams(self.body.teamComponent.teamIndex),
                                    filterByLoS = false,
                                    searchOrigin = self.transform.position,
                                    searchDirection = UnityEngine.Random.onUnitSphere,
                                    sortMode = BullseyeSearch.SortMode.Distance,
                                    maxDistanceFilter = Modules.StaticValues.spikedamageRadius,
                                    maxAngleFilter = 360f
                                };

                                search.RefreshCandidates();
                                search.FilterOutGameObject(self.gameObject);



                                List<HurtBox> target = search.GetResults().ToList<HurtBox>();
                                foreach (HurtBox singularTarget in target)
                                {
                                    if (singularTarget)
                                    {
                                        if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                                        {
                                            InflictDotInfo info = new InflictDotInfo();
                                            info.attackerObject = self.gameObject;
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
                        }


                    }
                }

            }
            



            orig.Invoke(self, damageInfo);
        }

        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            //buffs 
            if (self?.healthComponent)
            {
                orig.Invoke(self);
                if (self.baseNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
                {
                    //    //roboballmini attackspeed buff
                    //    if (self.HasBuff(Buffs.roboballminiattackspeedBuff))
                    //    {
                    //        self.attackSpeed += (Modules.StaticValues.roboballattackspeedMultiplier * self.GetBuffCount(Buffs.roboballminiattackspeedBuff));

                    //    }
                    //    //claydunestrider buff
                    //    if (self.HasBuff(Buffs.claydunestriderBuff))
                    //    {
                    //        self.attackSpeed *= StaticValues.claydunestriderAttackSpeed;

                    //    }
                    //    //mult buff
                    //    if (self.HasBuff(Buffs.multBuff))
                    //    {
                    //        self.armor += StaticValues.multArmor;
                    //        self.attackSpeed *= StaticValues.multAttackspeed;
                    //        self.moveSpeed *= StaticValues.multMovespeed;

                    //    }
                    //    //stonetitanarmor buff
                    //    if (self.HasBuff(Buffs.stonetitanBuff))
                    //    {
                    //        self.armor += StaticValues.stonetitanarmorGain;

                    //    }
                    //    //voidbarnaclemortarattackspeed buff
                    //    if (self.HasBuff(Buffs.voidbarnaclemortarattackspeedBuff))
                    //    {
                    //        Shiggycon = base.GetComponent<ShiggyController>();
                    //        self.attackSpeed += Modules.StaticValues.voidmortarattackspeedGain* self.GetBuffCount(Buffs.voidbarnaclemortarattackspeedBuff);

                    //    }
                    //    //hermitcrabmortararmor buff
                    //    if (self.HasBuff(Buffs.hermitcrabmortararmorBuff))
                    //    {
                    //        int mortararmorbuffcount = self.GetBuffCount(Buffs.hermitcrabmortararmorBuff);
                    //        self.armor += mortararmorbuffcount * Modules.StaticValues.mortararmorGain;
                    //    }
                    //    //verminsprint buff
                    //    if (self.HasBuff(Buffs.verminsprintBuff))
                    //    {
                    //        self.sprintingSpeedMultiplier = Modules.StaticValues.verminsprintMultiplier;
                    //        self.moveSpeed *= Modules.StaticValues.verminmovespeedMultiplier;
                    //    }
                    //    //multiplier buff
                    if (self.HasBuff(Buffs.multiplierBuff))
                    {
                        self.damage *= Modules.StaticValues.multiplierCoefficient;
                    }
                    //    //fly buff
                    //    if (self.HasBuff(Buffs.flyBuff))
                    //    {
                    //        self.moveSpeed *= 1.5f;
                    //        self.acceleration *= 2f;
                    //        self.bodyFlags |= CharacterBody.BodyFlags.IgnoreFallDamage;
                    //        self.characterMotor.useGravity = false;
                    //    }
                    //    else
                    //    {
                    //        self.bodyFlags &= ~CharacterBody.BodyFlags.IgnoreFallDamage;
                    //        self.characterMotor.useGravity = true;

                    //    }
                    //    //beetlebuff
                    //    if (self.HasBuff(Buffs.beetleBuff))
                    //    {
                    //        self.damage *= Modules.StaticValues.beetledamageMultiplier;
                    //    }
                    //    //lesserwispbuff
                    //    if (self.HasBuff(Buffs.lesserwispBuff))
                    //    {
                    //        self.damage *= Modules.StaticValues.lesserwispdamageMultiplier;
                    //    }

                }

                if (self.HasBuff(Buffs.decayDebuff))
                {
                    float decaybuffcount = self.GetBuffCount(Buffs.decayDebuff);
                    self.attackSpeed *= Mathf.Pow(0.96f, decaybuffcount);
                    self.moveSpeed *= Mathf.Pow(0.96f, decaybuffcount);
                    if (decaybuffcount >= Modules.StaticValues.decayInstaKillThreshold)
                    {
                        if (NetworkServer.active && self.healthComponent)
                        {
                            DamageInfo damageInfo = new DamageInfo();
                            damageInfo.damage = self.healthComponent.fullCombinedHealth + self.healthComponent.fullBarrier + 1;
                            damageInfo.position = self.transform.position;
                            damageInfo.force = Vector3.zero;
                            damageInfo.damageColorIndex = DamageColorIndex.Default;
                            damageInfo.crit = true;
                            damageInfo.attacker = null;
                            damageInfo.inflictor = null;
                            damageInfo.damageType = DamageType.WeakPointHit;
                            damageInfo.procCoefficient = 0f;
                            damageInfo.procChainMask = default(ProcChainMask);
                            self.healthComponent.TakeDamage(damageInfo);
                        }
                    }

                    DecayEffectController controller = self.gameObject.GetComponent<DecayEffectController>();
                    if (!controller)
                    {
                        controller = self.gameObject.AddComponent<DecayEffectController>();
                        controller.charbody = self;
                    }

                }
            }
            

        }


        private void CharacterModel_Awake(On.RoR2.CharacterModel.orig_Awake orig, CharacterModel self)
        {
            orig(self);
            //deku collab
            if (self.gameObject.name.Contains("ShiggyDisplay"))
            {
                bool dekuFound = false;
                foreach (Transform child in self.gameObject.transform.parent.parent)
                {
                    foreach (Transform child2 in child)
                    {
                        if (child2.gameObject.name.Contains("DekuDisplay"))
                        {
                            dekuFound = true;
                        }
                    }
                }
                if (dekuFound)
                {
                    AkSoundEngine.PostEvent(2848575038, self.gameObject);
                }
                else
                {
                    AkSoundEngine.PostEvent(1896314350, self.gameObject);
                }
            }
            //if (gameObject.name.Contains("DekuDisplay"))
            //{
            //    if (self.gameObject.name.Contains("ShiggyDisplay"))
            //    {
            //        AkSoundEngine.PostEvent(1899640155, self.gameObject);

            //    }
            //}

        }
        private void CharacterModel_UpdateOverlays(On.RoR2.CharacterModel.orig_UpdateOverlays orig, CharacterModel self)
        {
            orig(self);

            if (self)
            {
                if (self.body)
                {
                    this.OverlayFunction(Modules.Assets.alphaconstructShieldBuff, self.body.HasBuff(Modules.Buffs.alphashieldonBuff), self);
                    this.OverlayFunction(Modules.Assets.multiplierShieldBuff, self.body.HasBuff(Modules.Buffs.multiplierBuff), self);
                }
            }
        }

        private void OverlayFunction(Material overlayMaterial, bool condition, CharacterModel model)
        {
            if (model.activeOverlayCount >= CharacterModel.maxOverlays)
            {
                return;
            }
            if (condition)
            {
                Material[] array = model.currentOverlays;
                int num = model.activeOverlayCount;
                model.activeOverlayCount = num + 1;
                array[num] = overlayMaterial;
            }
        }

    }
}
