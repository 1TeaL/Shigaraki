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
using System;
using RoR2.Items;
using R2API.Networking.Interfaces;
using EntityStates.VagrantMonster;
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
        public const string MODVERSION = "1.3.1";

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
        

        private void Awake()
        {
            instance = this;
            ShiggyCharacterBody = null;
            ShiggyPlugin.instance = this;

            // load assets and read config
            Modules.Assets.Initialize();
            Modules.Config.ReadConfig();
            Modules.Damage.SetupModdedDamage(); //setup modded damage
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

            Modules.StaticValues.LoadDictionary(); // load dictionary to differentiate between passives and actives
            //networking
            NetworkingAPI.RegisterMessageType<EquipmentDropNetworked>();
            NetworkingAPI.RegisterMessageType<HealNetworkRequest>();
            NetworkingAPI.RegisterMessageType<SpawnBodyNetworkRequest>();
            NetworkingAPI.RegisterMessageType<PerformForceNetworkRequest>();
            NetworkingAPI.RegisterMessageType<PeformDirectionalForceNetworkRequest>();
            NetworkingAPI.RegisterMessageType<ItemDropNetworked>();
            NetworkingAPI.RegisterMessageType<SpendHealthNetworkRequest>();
            NetworkingAPI.RegisterMessageType<DisableSlideStateMachine>();
            NetworkingAPI.RegisterMessageType<SetTheWorldFreezeOnBodyRequest>();
            NetworkingAPI.RegisterMessageType<TakeMeleeDamageForceRequest>();
            NetworkingAPI.RegisterMessageType<ForceReversalState>();
            NetworkingAPI.RegisterMessageType<SetShunpoStateMachine>();
            NetworkingAPI.RegisterMessageType<SetMugetsuStateMachine>();
            NetworkingAPI.RegisterMessageType<SetGetsugaStateMachine>();



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
            On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;
            On.RoR2.Orbs.LightningOrb.OnArrival += LightningOrb_OnArrival;
            On.RoR2.OverlapAttack.Fire += OverlapAttack_Fire;
            On.RoR2.BulletAttack.Fire += BulletAttack_Fire;
            On.RoR2.BlastAttack.Fire += BlastAttack_Fire;
           

            if (Chainloader.PluginInfos.ContainsKey("com.weliveinasociety.CustomEmotesAPI"))
            {
                On.RoR2.SurvivorCatalog.Init += SurvivorCatalog_Init;
            }
        }

        private BlastAttack.Result BlastAttack_Fire(On.RoR2.BlastAttack.orig_Fire orig, BlastAttack self)
        {

            GameObject attacker = self.attacker;
            if (attacker)
            {
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.acridBuff))
                {
                    //add poison to all blast attacks
                    self.damageType |= DamageType.PoisonOnHit;
                }
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.impbossBuff))
                {
                    //add bleed to all blast attacks
                    self.damageType |= DamageType.BleedOnHit;
                }
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionFireBuff))
                {
                    //add ignite to all blast attacks
                    self.damageType |= DamageType.IgniteOnHit;
                }
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionFreezeBuff))
                {
                    //add freeze to all blast attacks
                    self.damageType |= DamageType.Freeze2s;
                }
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionShockBuff))
                {
                    //add freeze to all blast attacks
                    self.damageType |= DamageType.Shock5s;
                }
            }
            return orig(self);
        }

        private void BulletAttack_Fire(On.RoR2.BulletAttack.orig_Fire orig, BulletAttack self)
        {
            orig(self);
            GameObject attacker = self.owner;
            if (attacker)
            {
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.acridBuff))
                {
                    //add poison to all bullet attacks
                    self.damageType |= DamageType.PoisonOnHit;
                }
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.impbossBuff))
                {
                    //add bleed to all bullet attacks
                    self.damageType |= DamageType.BleedOnHit;
                }
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionFireBuff))
                {
                    //add ignite to all bullet attacks
                    self.damageType |= DamageType.IgniteOnHit;
                }
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionFreezeBuff))
                {
                    //add freeze to all bullet attacks
                    self.damageType |= DamageType.Freeze2s;
                }
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionShockBuff))
                {
                    //add freeze to all bullet attacks
                    self.damageType |= DamageType.Shock5s;
                }
            }

        }

        private bool OverlapAttack_Fire(On.RoR2.OverlapAttack.orig_Fire orig, OverlapAttack self, List<HurtBox> hitResults)
        {

            GameObject attacker = self.attacker;
            if (attacker)
            {
                if(attacker.gameObject.GetComponent<CharacterBody>().baseNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
                {
                    //add decay to all overlap attacks
                    self.AddModdedDamageType(Damage.shiggyDecay);
                }
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.acridBuff))
                {
                    //add poison to all overlap attacks
                    self.damageType |= DamageType.PoisonOnHit;
                }
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.impbossBuff))
                {
                    //add bleed to all overlap attacks
                    self.damageType |= DamageType.BleedOnHit;
                }
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionFireBuff))
                {
                    //add ignite to all overlap attacks
                    self.damageType |= DamageType.IgniteOnHit;
                }
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionFreezeBuff))
                {
                    //add freeze to all overlap attacks
                    self.damageType |= DamageType.Freeze2s;
                }
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionShockBuff))
                {
                    //add freeze to all overlap attacks
                    self.damageType |= DamageType.Shock5s;
                }
            }
            return orig(self, hitResults);
        }

        private void LightningOrb_OnArrival(On.RoR2.Orbs.LightningOrb.orig_OnArrival orig, LightningOrb self)
        {
            orig(self);
            if (self.attacker.gameObject.GetComponent<CharacterBody>().baseNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
            {
                if (self.lightningType == LightningOrb.LightningType.HuntressGlaive)
                {
                    //new BlastAttack
                    //{
                    //    attacker = self.attacker.gameObject,
                    //    teamIndex = TeamComponent.GetObjectTeam(self.attacker.gameObject),
                    //    falloffModel = BlastAttack.FalloffModel.None,
                    //    baseDamage = self.damageValue,
                    //    damageType = DamageType.Generic,
                    //    damageColorIndex = DamageColorIndex.Default,
                    //    baseForce = -1000f,
                    //    position = self.target.transform.position,
                    //    radius = StaticValues.blackholeGlaiveBlastRange,
                    //    procCoefficient = StaticValues.blackholeGlaiveProcCoefficient,
                    //    attackerFiltering = AttackerFiltering.NeverHitSelf,
                    //}.Fire();
                    new PerformForceNetworkRequest(self.attacker.gameObject.GetComponent<CharacterBody>().masterObjectId, self.target.transform.position, Vector3.up, StaticValues.blackholeGlaiveBlastRange, 0f, self.damageValue, 360f, false).Send(NetworkDestination.Clients);

                    EffectManager.SpawnEffect(Modules.Assets.voidMegaCrabExplosionEffect, new EffectData
                    {
                        origin = self.target.transform.position,
                        scale = StaticValues.blackholeGlaiveBlastRange
                    }, true);
                    

                }
            }

        }

        private void RecalculateStatsAPI_GetStatCoefficients(CharacterBody sender, RecalculateStatsAPI.StatHookEventArgs args)
        {
            if (sender?.healthComponent)
            {
                if (sender)
                {
                    //roboballmini attackspeed buff
                    if (sender.HasBuff(Buffs.roboballminiattackspeedBuff))
                    {
                        args.baseAttackSpeedAdd += (StaticValues.roboballattackspeedMultiplier * sender.GetBuffCount(Buffs.roboballminiattackspeedBuff));

                    }
                    //claydunestrider buff
                    if (sender.HasBuff(Buffs.claydunestriderBuff))
                    {
                        args.baseAttackSpeedAdd += (StaticValues.claydunestriderAttackSpeed - 1);

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
                    if (sender.HasBuff(Buffs.airwalkBuff))
                    {
                        sender.acceleration *= 2f;                        
                    }
                    //beetlebuff
                    if (sender.HasBuff(Buffs.beetleBuff))
                    {
                        args.baseDamageAdd += StaticValues.beetleFlatDamage;
                    }
                    //lesserwispbuff
                    if (sender.HasBuff(Buffs.lesserwispBuff))
                    {
                        args.baseAttackSpeedAdd += StaticValues.lesserwispFlatAttackSpeed;
                    }
                    //lunar exploder
                    if (sender.HasBuff(Buffs.lunarexploderBuff))
                    {
                        args.baseShieldAdd += sender.maxHealth * StaticValues.lunarexploderShieldCoefficient;
                    }
                    //omniboost buff
                    if (sender.HasBuff(Buffs.omniboostBuff))
                    {
                        args.damageMultAdd += StaticValues.omniboostBuffCoefficient;
                        args.attackSpeedMultAdd += StaticValues.omniboostBuffCoefficient;
                    }
                    //omniboost buff
                    if (sender.HasBuff(Buffs.omniboostBuffStacks))
                    {
                        int omniboostBuffcount = sender.GetBuffCount(Buffs.omniboostBuffStacks);
                        args.damageMultAdd += StaticValues.omniboostBuffStackCoefficient * omniboostBuffcount;
                        args.attackSpeedMultAdd += StaticValues.omniboostBuffStackCoefficient * omniboostBuffcount;
                    }
                    //ingrain buff
                    if (sender.HasBuff(Buffs.ingrainBuff))
                    {
                        args.baseRegenAdd += StaticValues.ingrainBuffHealthRegen * sender.healthComponent.fullCombinedHealth;
                    }
                    //ofa buff
                    if (sender.HasBuff(Buffs.OFABuff))
                    {
                        args.armorAdd += sender.armor * (1 + StaticValues.OFACoefficient);
                        args.attackSpeedMultAdd += StaticValues.OFACoefficient;
                        args.moveSpeedMultAdd += StaticValues.OFACoefficient;
                    }
                    //double time buff
                    if (sender.HasBuff(Buffs.doubleTimeBuffStacks))
                    {
                        int doubletimeBuffcount = sender.GetBuffCount(Buffs.doubleTimeBuffStacks);
                        args.damageMultAdd += StaticValues.doubleTimeCoefficient * doubletimeBuffcount;
                        args.attackSpeedMultAdd += StaticValues.doubleTimeCoefficient * doubletimeBuffcount;
                        args.moveSpeedMultAdd += StaticValues.doubleTimeCoefficient * doubletimeBuffcount;
                    }
                    //double time debuff
                    if (sender.HasBuff(Buffs.doubleTimeDebuff))
                    {
                        args.attackSpeedMultAdd -= StaticValues.doubleTimeSlowCoefficient;
                        args.moveSpeedMultAdd -= StaticValues.doubleTimeSlowCoefficient;
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


                if (body && victimBody)
                {
                    //impboss buff just incase
                    if (body.HasBuff(Buffs.impbossBuff))
                    {
                        damageInfo.damageType |= DamageType.BleedOnHit;
                    }
                    //acrid buff just incase
                    if (body.HasBuff(Buffs.acridBuff))
                    {
                        damageInfo.damageType |= DamageType.PoisonOnHit;
                    }

                    //final release buff stacks
                    if (body.HasBuff(Buffs.finalReleaseBuff))
                    {
                        if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                        {
                            int finalReleaseCount = body.GetBuffCount(Buffs.finalReleaseBuff);
                            body.ApplyBuff(Buffs.finalReleaseBuff.buffIndex, finalReleaseCount + 1);
                        }

                    }

                    //one for all for one double damage and proc buff
                    if (body.HasBuff(Modules.Buffs.OFAFOBuff))
                    {
                        if (damageInfo.damage > 0)
                        {
                            DamageInfo damageInfo2 = new DamageInfo();
                            damageInfo2.damage = damageInfo.damage;
                            damageInfo2.position = victimBody.corePosition;
                            damageInfo2.force = Vector3.zero;
                            damageInfo2.damageColorIndex = DamageColorIndex.Default;
                            damageInfo2.crit = false;
                            damageInfo2.attacker = body.gameObject;
                            damageInfo2.inflictor = victimBody.gameObject;
                            damageInfo2.damageType = damageInfo.damageType;
                            damageInfo2.procCoefficient = damageInfo.procCoefficient;
                            damageInfo2.procChainMask = default(ProcChainMask);
                            victimBody.healthComponent.TakeDamage(damageInfo2);
                        }
                    }

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
                            damageInfo2.damageColorIndex = DamageColorIndex.Default;
                            damageInfo2.crit = false;
                            damageInfo2.attacker = body.gameObject;
                            damageInfo2.inflictor = victimBody.gameObject;
                            damageInfo2.damageType = damageInfo.damageType;
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
                                crit = false,
                                attacker = damageInfo.attacker.gameObject,
                                teamIndex = TeamComponent.GetObjectTeam(damageInfo.attacker.gameObject),
                                falloffModel = BlastAttack.FalloffModel.None,
                                baseDamage = body.damage * StaticValues.vagrantDamageCoefficient * body.attackSpeed / 3,
                                damageType = DamageType.Stun1s,
                                damageColorIndex = DamageColorIndex.Default,
                                baseForce = 0,
                                position = victimBody.transform.position,
                                radius = StaticValues.vagrantRadius * body.attackSpeed / 3,
                                procCoefficient = 1f,
                                attackerFiltering = AttackerFiltering.NeverHitSelf,
                            }.Fire();
                            

                        }
                    }
                    //tar buff
                    if (body.HasBuff(Modules.Buffs.claydunestriderBuff))
                    {
                        if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                        {
                            damageInfo.damageType |= DamageType.ClayGoo;
                        }
                    }
                    if (body.HasBuff(Modules.Buffs.greaterwispBuff))
                    {
                        if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT && damageInfo.procCoefficient != 0f)
                        {
                            EffectManager.SpawnEffect(Modules.Assets.chargegreaterwispBall, new EffectData
                            {
                                origin = victimBody.transform.position,
                                scale = StaticValues.greaterwispballRadius,
                                rotation = Util.QuaternionSafeLookRotation(damageInfo.force)
                            }, true);
                            new BlastAttack
                            {
                                crit = false,
                                attacker = damageInfo.attacker.gameObject,
                                teamIndex = TeamComponent.GetObjectTeam(damageInfo.attacker.gameObject),
                                falloffModel = BlastAttack.FalloffModel.None,
                                baseDamage = damageInfo.damage * StaticValues.greaterwispballDamageCoefficient,
                                damageType = damageInfo.damageType,
                                damageColorIndex = DamageColorIndex.Default,
                                baseForce = 0,
                                procChainMask = damageInfo.procChainMask,
                                position = victimBody.transform.position,
                                radius = StaticValues.greaterwispballRadius,
                                procCoefficient = 0f,
                                attackerFiltering = AttackerFiltering.NeverHitSelf,
                            }.Fire();
                            
                        }
                    }

                    //elemntal fusion cycling stacks
                    if (body.HasBuff(Buffs.elementalFusionFireBuff)
                        | body.HasBuff(Buffs.elementalFusionFreezeBuff)
                        | body.HasBuff(Buffs.elementalFusionShockBuff))
                    {
                        if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                        {
                            //deal ignite, freeze, or shock damage type every 5 hits
                            var elementalBuffCount = body.GetBuffCount(Buffs.elementalFusionBuffStacks);

                            if (elementalBuffCount < StaticValues.elementalFusionThreshold)
                            {
                                body.ApplyBuff(Buffs.elementalFusionBuffStacks.buffIndex, elementalBuffCount + 1);
                                                                
                            }
                            else if (elementalBuffCount >= StaticValues.elementalFusionThreshold && victimBody)
                            {
                                if (body.HasBuff(Buffs.elementalFusionFireBuff))
                                {

                                    body.ApplyBuff(Buffs.elementalFusionFireBuff.buffIndex, 0);
                                    body.ApplyBuff(Buffs.elementalFusionFreezeBuff.buffIndex, 1);
                                    body.ApplyBuff(Buffs.elementalFusionBuffStacks.buffIndex, 0);

                                    EffectManager.SpawnEffect(Modules.Assets.artificerFireMuzzleEffect, new EffectData
                                    {
                                        origin = body.corePosition,
                                        scale = 1f,
                                        rotation = Quaternion.identity,
                                    }, false);

                                }
                                else if (body.HasBuff(Buffs.elementalFusionFreezeBuff))
                                {
                                    body.ApplyBuff(Buffs.elementalFusionFreezeBuff.buffIndex, 0);
                                    body.ApplyBuff(Buffs.elementalFusionShockBuff.buffIndex, 1);
                                    body.ApplyBuff(Buffs.elementalFusionBuffStacks.buffIndex, 0);

                                    EffectManager.SpawnEffect(Modules.Assets.artificerIceMuzzleEffect, new EffectData
                                    {
                                        origin = body.corePosition,
                                        scale = 1f,
                                        rotation = Quaternion.identity,
                                    }, false);
                                }
                                else if (body.HasBuff(Buffs.elementalFusionShockBuff))
                                {
                                    body.ApplyBuff(Buffs.elementalFusionShockBuff.buffIndex, 0);
                                    body.ApplyBuff(Buffs.elementalFusionFireBuff.buffIndex, 1);
                                    body.ApplyBuff(Buffs.elementalFusionBuffStacks.buffIndex, 0);

                                    EffectManager.SpawnEffect(Modules.Assets.artificerLightningMuzzleEffect, new EffectData
                                    {
                                        origin = body.corePosition,
                                        scale = 1f,
                                        rotation = Quaternion.identity,
                                    }, false);
                                }
                            }
                        }

                    }


                    //omniboost buff stacks
                    if (body.HasBuff(Buffs.omniboostBuff))
                    {
                        if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                        {
                            //add a debuff stack to the enemy, after 3 stacks gain your own buff stack
                            var omnidebuffCount = victimBody.GetBuffCount(Buffs.omniboostDebuffStacks);
                            if (omnidebuffCount < StaticValues.omniboostNumberOfHits)
                            {
                                victimBody.ApplyBuff(Buffs.omniboostDebuffStacks.buffIndex, omnidebuffCount + 1);
                                omnidebuffCount++;

                                if (omnidebuffCount >= StaticValues.omniboostNumberOfHits)
                                {
                                    var omniBuffCount = body.GetBuffCount(Buffs.omniboostBuffStacks);
                                    body.ApplyBuff(Buffs.omniboostBuffStacks.buffIndex, omniBuffCount + 1);
                                    victimBody.ApplyBuff(Buffs.omniboostDebuffStacks.buffIndex, 0);

                                    EffectManager.SpawnEffect(EntityStates.Wisp1Monster.FireEmbers.hitEffectPrefab, new EffectData
                                    {
                                        origin = victimBody.transform.position,
                                        scale = 1f
                                    }, false);
                                }
                            }
                            else if (omnidebuffCount >= StaticValues.omniboostNumberOfHits)
                            {
                                var omniBuffCount = body.GetBuffCount(Buffs.omniboostBuffStacks);
                                body.ApplyBuff(Buffs.omniboostBuffStacks.buffIndex, omniBuffCount + 1);
                                victimBody.ApplyBuff(Buffs.omniboostDebuffStacks.buffIndex, 0);

                                EffectManager.SpawnEffect(EntityStates.Wisp1Monster.FireEmbers.hitEffectPrefab, new EffectData
                                {
                                    origin = victimBody.transform.position,
                                    scale = 1f
                                }, false);
                            }
                        }
                            

                    }
                    //bigbang buff
                    if (body.HasBuff(Modules.Buffs.bigbangBuff))
                    {

                        if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                        {
                            int bigbangCount = victimBody.GetBuffCount(Modules.Buffs.bigbangDebuff);
                            if (bigbangCount < StaticValues.bigbangBuffThreshold)
                            {
                                victimBody.ApplyBuff(Buffs.bigbangDebuff.buffIndex, bigbangCount + 1);
                                bigbangCount++;
                                if (bigbangCount >= StaticValues.bigbangBuffThreshold)
                                {
                                    victimBody.ApplyBuff(Buffs.bigbangDebuff.buffIndex, 0);
                                    if (EntityStates.VagrantMonster.ExplosionAttack.novaEffectPrefab)
                                    {
                                        EffectManager.SpawnEffect(EntityStates.VagrantMonster.ExplosionAttack.novaEffectPrefab, new EffectData
                                        {
                                            origin = victimBody.transform.position,
                                            scale = StaticValues.bigbangBuffRadius * body.attackSpeed / 3
                                        }, true);
                                    }
                                    new BlastAttack
                                    {
                                        crit = false,
                                        attacker = damageInfo.attacker.gameObject,
                                        teamIndex = TeamComponent.GetObjectTeam(damageInfo.attacker.gameObject),
                                        falloffModel = BlastAttack.FalloffModel.None,
                                        baseDamage = victimBody.healthComponent.fullCombinedHealth * StaticValues.bigbangBuffHealthCoefficient,
                                        damageType = DamageType.Stun1s,
                                        damageColorIndex = DamageColorIndex.Default,
                                        baseForce = 0,
                                        position = victimBody.transform.position,
                                        radius = StaticValues.bigbangBuffRadius * body.attackSpeed / 3,
                                        procCoefficient = 0f,
                                        attackerFiltering = AttackerFiltering.NeverHitSelf,
                                    }.Fire();
                                }
                            }
                            else if (bigbangCount >= StaticValues.bigbangBuffThreshold)
                            {
                                victimBody.ApplyBuff(Buffs.bigbangDebuff.buffIndex, 0);
                                if (EntityStates.VagrantMonster.ExplosionAttack.novaEffectPrefab)
                                {
                                    EffectManager.SpawnEffect(EntityStates.VagrantMonster.ExplosionAttack.novaEffectPrefab, new EffectData
                                    {
                                        origin = victimBody.transform.position,
                                        scale = StaticValues.bigbangBuffRadius * body.attackSpeed / 3
                                    }, true);
                                }
                                new BlastAttack
                                {
                                    crit = false,
                                    attacker = damageInfo.attacker.gameObject,
                                    teamIndex = TeamComponent.GetObjectTeam(damageInfo.attacker.gameObject),
                                    falloffModel = BlastAttack.FalloffModel.None,
                                    baseDamage = victimBody.healthComponent.fullCombinedHealth * StaticValues.bigbangBuffHealthCoefficient,
                                    damageType = DamageType.Stun1s,
                                    damageColorIndex = DamageColorIndex.Default,
                                    baseForce = 0,
                                    position = victimBody.transform.position,
                                    radius = StaticValues.bigbangBuffRadius * body.attackSpeed / 3,
                                    procCoefficient = 0f,
                                    attackerFiltering = AttackerFiltering.NeverHitSelf,
                                }.Fire();
                            }
                        }                        
                    }

                    //wisper buff
                    if (body.HasBuff(Buffs.wisperBuff))
                    {
                        if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT && damageInfo.procCoefficient > 0f)
                        {
                            DevilOrb devilOrb = new DevilOrb
                            {
                                origin = body.corePosition,
                                damageValue = body.damage * StaticValues.wisperBuffDamageCoefficient,
                                teamIndex = body.teamComponent.teamIndex,
                                attacker = body.gameObject,
                                damageColorIndex = DamageColorIndex.Item,
                                scale = 1f,
                                effectType = DevilOrb.EffectType.Wisp,
                                procCoefficient = 0f,
                            };
                            if (devilOrb.target = victimBody.mainHurtBox)
                            {
                                devilOrb.isCrit = Util.CheckRoll(body.crit, body.master);
                                OrbManager.instance.AddOrb(devilOrb);
                            }
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
                    var shiggycon = self.gameObject.GetComponent<ShiggyController>();
                    
                    AkSoundEngine.PostEvent(2108803202, self.gameObject);
                }
                var buffcon = self.gameObject.GetComponent<BuffController>();
                if (buffcon)
                {
                    if (buffcon.overloadingWard) EntityState.Destroy(buffcon.overloadingWard);
                    if (buffcon.mushroomWard) EntityState.Destroy(buffcon.mushroomWard);
                    if (buffcon.magmawormWard) EntityState.Destroy(buffcon.magmawormWard);
                    if (buffcon.mortarIndicatorInstance)
                    {
                        buffcon.mortarIndicatorInstance.SetActive(false);
                        EntityState.Destroy(buffcon.mortarIndicatorInstance.gameObject);
                    }
                    if (buffcon.voidmortarIndicatorInstance)
                    {
                        buffcon.voidmortarIndicatorInstance.SetActive(false);
                        EntityState.Destroy(buffcon.voidmortarIndicatorInstance.gameObject);
                    }
                    if (buffcon.barbedSpikesIndicatorInstance)
                    {
                        buffcon.barbedSpikesIndicatorInstance.SetActive(false);
                        EntityState.Destroy(buffcon.barbedSpikesIndicatorInstance.gameObject);
                    }
                }

            }
        }


        private void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
        {
            orig.Invoke(self, damageReport);
            

            if (damageReport.attackerBody && damageReport.victimBody)
            {
                //shiggy kill check for energy
                if (damageReport.attackerBody?.baseNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
                {
                    if (damageReport.damageInfo.damage > 0 && damageReport.attackerBody.hasEffectiveAuthority)
                    {
                        EnergySystem energySystem = damageReport.attackerBody.gameObject.GetComponent<EnergySystem>();
                        energySystem.GainplusChaos(energySystem.maxPlusChaos * StaticValues.killPlusChaosGain);
                    }
                }

                //shiggy double time kill buffs
                if (damageReport.attackerBody.HasBuff(Buffs.doubleTimeBuff))
                {
                    if (damageReport.damageInfo.damage > 0 && damageReport.attackerBody.hasEffectiveAuthority)
                    {
                        int doubleTimeStacksBuffCount = damageReport.attackerBody.GetBuffCount(Buffs.doubleTimeBuffStacks);
                        damageReport.attackerBody.ApplyBuff(Buffs.doubleTimeBuffStacks.buffIndex, doubleTimeStacksBuffCount + 1);
                    }
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
            if (!flag && report.attackerBody.HasBuff(Modules.Buffs.OFAFOBuff))
            {
                CharacterBody attackerBody = report.attackerBody;
                attackerBody.healthComponent.Heal(report.damageDealt * Modules.StaticValues.OFAFOLifestealCoefficient, default(ProcChainMask), true);

                EnergySystem energySystem = attackerBody.gameObject.GetComponent<EnergySystem>();
                if (energySystem)
                {
                    energySystem.GainplusChaos(StaticValues.OFAFOEnergyGainCoefficient * report.damageDealt);
                }

            }

        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {

            if (self)
            {
                if (damageInfo != null && damageInfo.attacker && damageInfo.attacker.GetComponent<CharacterBody>())
                {
                    //gargoyle protection buff
                    if (self.body.HasBuff(Buffs.gargoyleProtectionBuff))
                    {
                        //reduce damage and reflect that portion back
                        damageInfo.damage -= damageInfo.damage * StaticValues.gargoyleProtectionDamageReductionCoefficient;

                        DamageInfo damageInfo2 = new DamageInfo();
                        damageInfo2.damage = damageInfo.damage * StaticValues.gargoyleProtectionDamageReductionCoefficient;
                        damageInfo2.position = damageInfo.attacker.transform.position;
                        damageInfo2.force = Vector3.zero;
                        damageInfo2.damageColorIndex = DamageColorIndex.WeakPoint;
                        damageInfo2.crit = false;
                        damageInfo2.attacker = self.body.gameObject;
                        damageInfo2.damageType = DamageType.BypassArmor;
                        damageInfo2.procCoefficient = 0f;
                        damageInfo2.procChainMask = default(ProcChainMask);
                        damageInfo.attacker.GetComponent<CharacterBody>().healthComponent.TakeDamage(damageInfo2);

                        EffectData effectData = new EffectData
                        {
                            origin = damageInfo.position,
                            rotation = Quaternion.identity,
                        };
                        EffectManager.SpawnEffect(Modules.Assets.mushrumSporeImpactPrefab, effectData, true);
                        EffectData effectData2 = new EffectData
                        {
                            origin = damageInfo.attacker.transform.position,
                            rotation = Quaternion.identity,
                        };
                        EffectManager.SpawnEffect(Modules.Assets.mushrumSporeImpactPrefab, effectData2, true);
                    }

                    //death aura buff and debuff
                    if (self.body.HasBuff(Buffs.deathAuraDebuff))
                    {
                        if (damageInfo.damageType == DamageType.DoT)
                        {
                            damageInfo.damage *= (1 + self.body.GetBuffCount(Buffs.deathAuraDebuff) * StaticValues.deathAuraDebuffCoefficient);
                        }
                    }
                    if (damageInfo.attacker.GetComponent<CharacterBody>().HasBuff(Buffs.deathAuraBuff))
                    {
                        if (damageInfo.damageType == DamageType.DoT)
                        {
                            damageInfo.damage *= (1 + damageInfo.attacker.GetComponent<CharacterBody>().GetBuffCount(Buffs.deathAuraBuff) * StaticValues.deathAuraBuffCoefficient);
                        }
                    }


                    //loader passive
                    if (damageInfo.attacker.GetComponent<CharacterBody>().HasBuff(Modules.Buffs.loaderBuff))
                    {
                        damageInfo.attacker.GetComponent<CharacterBody>().healthComponent.AddBarrierAuthority(damageInfo.attacker.GetComponent<CharacterBody>().healthComponent.fullCombinedHealth * StaticValues.loaderBarrierGainCoefficient);
                    }

                    //limit break buff health cost
                    if (damageInfo.attacker.GetComponent<CharacterBody>().HasBuff(Buffs.limitBreakBuff))
                    {
                        if ((damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                        {
                            Debug.Log("deal damage to self");
                            DamageInfo damageInfo2 = new DamageInfo();
                            damageInfo2.damage = StaticValues.limitBreakHealthCostCoefficient * damageInfo.attacker.GetComponent<CharacterBody>().healthComponent.fullCombinedHealth;
                            damageInfo2.position = damageInfo.attacker.transform.position;
                            damageInfo2.force = Vector3.zero;
                            damageInfo2.damageColorIndex = DamageColorIndex.WeakPoint;
                            damageInfo2.crit = false;
                            damageInfo2.attacker = null;
                            damageInfo2.inflictor = null;
                            damageInfo2.damageType = (DamageType.NonLethal | DamageType.BypassArmor);
                            damageInfo2.procCoefficient = 0f;
                            damageInfo2.procChainMask = default(ProcChainMask);
                            damageInfo.attacker.GetComponent<CharacterBody>().healthComponent.TakeDamage(damageInfo2);
                        }
                    }

                    //multiplier spend energy
                    if (damageInfo.attacker.GetComponent<CharacterBody>().HasBuff(Buffs.multiplierBuff))
                    {

                        if ((damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                        {
                            EnergySystem energySys = damageInfo.attacker.gameObject.GetComponent<EnergySystem>();
                            if (energySys)
                            {
                                float plusChaosflatCost = StaticValues.multiplierEnergyCost - energySys.costflatplusChaos;
                                if (plusChaosflatCost < 0f) plusChaosflatCost = 0f;

                                float plusChaosCost = energySys.costmultiplierplusChaos * plusChaosflatCost;
                                if (plusChaosCost < 0f) plusChaosCost = 0f;

                                if (energySys.currentplusChaos < plusChaosCost)
                                {
                                    damageInfo.attacker.GetComponent<CharacterBody>().ApplyBuff(Buffs.multiplierBuff.buffIndex, 0);
                                    energySys.TriggerGlow(0.3f, 0.3f, Color.black);
                                }
                                else if (energySys.currentplusChaos >= plusChaosCost)
                                {
                                    damageInfo.damage *= StaticValues.multiplierCoefficient;
                                    energySys.SpendplusChaos(plusChaosCost);
                                }
                            }
                        }
                    }


                    //for self damage
                    bool flag = (damageInfo.damageType & DamageType.BypassArmor) > DamageType.Generic;
                    if (!flag && damageInfo.damage > 0f && damageInfo.attacker.gameObject.GetComponent<CharacterBody>() != self.body)
                    {

                        if (self.body.HasBuff(Buffs.reversalBuffStacks))
                        {
                            new ForceReversalState(self.body.masterObjectId, damageInfo.attacker.gameObject.GetComponent<CharacterBody>().masterObjectId).Send(NetworkDestination.Clients);
                            damageInfo.force = Vector3.zero;
                            damageInfo.rejected = true;
                            //do counterattack as well

                            if (self.body.HasBuff(Buffs.blindSensesBuff.buffIndex))
                            {
                                //fake calculating the chance to block for a bear
                                if (self.body.inventory.GetItemCount(RoR2Content.Items.Bear) > 0 && Util.CheckRoll(Util.ConvertAmplificationPercentageIntoReductionPercentage(15f * (float)self.body.inventory.GetItemCount(RoR2Content.Items.Bear)), 0f, null))
                                {
                                    //blind senses damage 
                                    LightningOrb lightningOrb = new LightningOrb();
                                    lightningOrb.attacker = self.body.gameObject;
                                    lightningOrb.bouncedObjects = null;
                                    lightningOrb.bouncesRemaining = 0;
                                    lightningOrb.damageCoefficientPerBounce = 1f;
                                    lightningOrb.damageColorIndex = DamageColorIndex.Item;
                                    lightningOrb.damageValue = damageInfo.damage * StaticValues.blindSensesDamageCoefficient;
                                    lightningOrb.isCrit = self.body.RollCrit();
                                    lightningOrb.lightningType = LightningOrb.LightningType.RazorWire;
                                    lightningOrb.origin = self.body.corePosition;
                                    lightningOrb.procChainMask = default(ProcChainMask);
                                    lightningOrb.procChainMask.AddProc(ProcType.Thorns);
                                    lightningOrb.procCoefficient = 1f;
                                    lightningOrb.damageType = DamageType.Stun1s;
                                    lightningOrb.range = 0f;
                                    lightningOrb.teamIndex = self.body.teamComponent.teamIndex;
                                    lightningOrb.target = damageInfo.attacker.gameObject.GetComponent<CharacterBody>().mainHurtBox;
                                    OrbManager.instance.AddOrb(lightningOrb);

                                }
                                //blind senses damage 
                                LightningOrb lightningOrb2 = new LightningOrb();
                                lightningOrb2.attacker = self.body.gameObject;
                                lightningOrb2.bouncedObjects = null;
                                lightningOrb2.bouncesRemaining = 0;
                                lightningOrb2.damageCoefficientPerBounce = 1f;
                                lightningOrb2.damageColorIndex = DamageColorIndex.Item;
                                lightningOrb2.damageValue = damageInfo.damage * StaticValues.blindSensesDamageCoefficient;
                                lightningOrb2.isCrit = self.body.RollCrit();
                                lightningOrb2.lightningType = LightningOrb.LightningType.RazorWire;
                                lightningOrb2.origin = self.body.corePosition;
                                lightningOrb2.procChainMask = default(ProcChainMask);
                                lightningOrb2.procChainMask.AddProc(ProcType.Thorns);
                                lightningOrb2.procCoefficient = 1f;
                                lightningOrb2.damageType = DamageType.Stun1s;
                                lightningOrb2.range = 0f;
                                lightningOrb2.teamIndex = self.body.teamComponent.teamIndex;
                                lightningOrb2.target = damageInfo.attacker.gameObject.GetComponent<CharacterBody>().mainHurtBox;
                                OrbManager.instance.AddOrb(lightningOrb2);

                                damageInfo.rejected = true;

                                EffectData effectData = new EffectData
                                {
                                    origin = damageInfo.position,
                                    rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                                };
                                EffectManager.SpawnEffect(HealthComponent.AssetReferences.bearEffectPrefab, effectData, true);
                                
                            }
                        }


                        if (self.body.HasBuff(Buffs.blindSensesBuff.buffIndex))
                        {
                            //fake calculating the chance to block for a bear
                            if (self.body.inventory.GetItemCount(RoR2Content.Items.Bear) > 0 && Util.CheckRoll(Util.ConvertAmplificationPercentageIntoReductionPercentage(15f * (float)self.body.inventory.GetItemCount(RoR2Content.Items.Bear)), 0f, null))
                            {
                                //blind senses damage 
                                LightningOrb lightningOrb = new LightningOrb();
                                lightningOrb.attacker = self.body.gameObject;
                                lightningOrb.bouncedObjects = null;
                                lightningOrb.bouncesRemaining = 0;
                                lightningOrb.damageCoefficientPerBounce = 1f;
                                lightningOrb.damageColorIndex = DamageColorIndex.Item;
                                lightningOrb.damageValue = damageInfo.damage * StaticValues.blindSensesDamageCoefficient;
                                lightningOrb.isCrit = self.body.RollCrit();
                                lightningOrb.lightningType = LightningOrb.LightningType.RazorWire;
                                lightningOrb.origin = self.body.corePosition;
                                lightningOrb.procChainMask = default(ProcChainMask);
                                lightningOrb.procChainMask.AddProc(ProcType.Thorns);
                                lightningOrb.procCoefficient = 1f;
                                lightningOrb.damageType = DamageType.Stun1s;
                                lightningOrb.range = 0f;
                                lightningOrb.teamIndex = self.body.teamComponent.teamIndex;
                                lightningOrb.target = damageInfo.attacker.gameObject.GetComponent<CharacterBody>().mainHurtBox;
                                OrbManager.instance.AddOrb(lightningOrb);
                                
                            }

                            if (Util.CheckRoll(StaticValues.blindSensesBlockChance, self.body.master))
                            {

                                //blind senses damage 
                                LightningOrb lightningOrb = new LightningOrb();
                                lightningOrb.attacker = self.body.gameObject;
                                lightningOrb.bouncedObjects = null;
                                lightningOrb.bouncesRemaining = 0;
                                lightningOrb.damageCoefficientPerBounce = 1f;
                                lightningOrb.damageColorIndex = DamageColorIndex.Item;
                                lightningOrb.damageValue = damageInfo.damage * StaticValues.blindSensesDamageCoefficient;
                                lightningOrb.isCrit = self.body.RollCrit();
                                lightningOrb.lightningType = LightningOrb.LightningType.RazorWire;
                                lightningOrb.origin = self.body.corePosition;
                                lightningOrb.procChainMask = default(ProcChainMask);
                                lightningOrb.procChainMask.AddProc(ProcType.Thorns);
                                lightningOrb.procCoefficient = 1f;
                                lightningOrb.damageType = DamageType.Stun1s;
                                lightningOrb.range = 0f;
                                lightningOrb.teamIndex = self.body.teamComponent.teamIndex;
                                lightningOrb.target = damageInfo.attacker.gameObject.GetComponent<CharacterBody>().mainHurtBox;
                                OrbManager.instance.AddOrb(lightningOrb);

                                damageInfo.rejected = true;

                                EffectData effectData = new EffectData
                                {
                                    origin = damageInfo.position,
                                    rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                                };
                                EffectManager.SpawnEffect(HealthComponent.AssetReferences.bearEffectPrefab, effectData, true);
                            }


                        }

                        //stone form passive buff
                        if (self.body.HasBuff(Buffs.stoneFormStillBuff.buffIndex))
                        {
                            damageInfo.force = Vector3.zero;
                            if (Util.CheckRoll(StaticValues.stoneFormBlockChance, self.body.master))
                            {

                                //blind senses damage 
                                if (self.body.HasBuff(Buffs.blindSensesBuff.buffIndex))
                                {
                                    LightningOrb lightningOrb = new LightningOrb();
                                    lightningOrb.attacker = self.body.gameObject;
                                    lightningOrb.bouncedObjects = null;
                                    lightningOrb.bouncesRemaining = 0;
                                    lightningOrb.damageCoefficientPerBounce = 1f;
                                    lightningOrb.damageColorIndex = DamageColorIndex.Item;
                                    lightningOrb.damageValue = damageInfo.damage * StaticValues.blindSensesDamageCoefficient;
                                    lightningOrb.isCrit = self.body.RollCrit();
                                    lightningOrb.lightningType = LightningOrb.LightningType.RazorWire;
                                    lightningOrb.origin = self.body.corePosition;
                                    lightningOrb.procChainMask = default(ProcChainMask);
                                    lightningOrb.procChainMask.AddProc(ProcType.Thorns);
                                    lightningOrb.procCoefficient = 1f;
                                    lightningOrb.damageType = DamageType.Stun1s;
                                    lightningOrb.range = 0f;
                                    lightningOrb.teamIndex = self.body.teamComponent.teamIndex;
                                    lightningOrb.target = damageInfo.attacker.gameObject.GetComponent<CharacterBody>().mainHurtBox;
                                    OrbManager.instance.AddOrb(lightningOrb);
                                }

                                damageInfo.rejected = true;

                                EffectData effectData = new EffectData
                                {
                                    origin = damageInfo.position,
                                    rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                                };
                                EffectManager.SpawnEffect(HealthComponent.AssetReferences.bearEffectPrefab, effectData, true);
                            }

                        }

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
                        //spike buff
                        if (self.body.HasBuff(Modules.Buffs.gupspikeBuff.buffIndex))
                        {
                            //Spike buff

                            blastAttack = new BlastAttack();
                            blastAttack.radius = Modules.StaticValues.spikedamageRadius;
                            blastAttack.procCoefficient = 0.5f;
                            blastAttack.position = self.transform.position;
                            blastAttack.attacker = self.gameObject;
                            blastAttack.crit = Util.CheckRoll(self.body.crit, self.body.master);
                            blastAttack.baseDamage = self.body.damage * Modules.StaticValues.spikedamageCoefficient;
                            blastAttack.falloffModel = BlastAttack.FalloffModel.None;
                            blastAttack.baseForce = 100f;
                            blastAttack.teamIndex = TeamComponent.GetObjectTeam(self.body.gameObject);
                            blastAttack.damageType = DamageType.Generic | DamageType.BleedOnHit;
                            blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;

                            DamageAPI.AddModdedDamageType(blastAttack, Damage.shiggyDecay);


                            if (damageInfo.attacker.gameObject.GetComponent<CharacterBody>().baseNameToken
                                != self.body.baseNameToken)
                            {
                                blastAttack.Fire();
                            }

                            EffectManager.SpawnEffect(Modules.Assets.GupSpikeEffect, new EffectData
                            {
                                origin = self.transform.position,
                                scale = Modules.StaticValues.spikedamageRadius / 3,
                                rotation = Quaternion.LookRotation(self.transform.position)

                            }, true);
                                                     
                        }               
                    }


                    //supernova stacks
                    if (self.body.HasBuff(Modules.Buffs.supernovaBuff.buffIndex))
                    {
                        
                        int currentStacks = self.body.GetBuffCount(Buffs.supernovaBuff.buffIndex) - 1;
                        int damageDealt = Mathf.RoundToInt(damageInfo.damage);

                        int buffTotal = (damageDealt) + currentStacks;

                  

                        if(buffTotal < Mathf.RoundToInt(StaticValues.supernovaHealthThreshold * self.body.healthComponent.fullCombinedHealth))
                        {
                            self.body.ApplyBuff(Modules.Buffs.supernovaBuff.buffIndex, buffTotal);
                        }
                        else if (buffTotal >= Mathf.RoundToInt(StaticValues.supernovaHealthThreshold * self.body.healthComponent.fullCombinedHealth))
                        {
                            self.body.ApplyBuff(Modules.Buffs.supernovaBuff.buffIndex, 1);

                            Vector3 position = self.body.transform.position;
                            Util.PlaySound(FireMegaNova.novaSoundString, self.body.gameObject);
                            EffectManager.SpawnEffect(FireMegaNova.novaEffectPrefab, new EffectData
                            {
                                origin = position,
                                scale = StaticValues.supernovaRadius,
                                rotation = Quaternion.LookRotation(self.transform.position)

                            }, true);

                            Transform modelTransform = self.body.gameObject.GetComponent<ModelLocator>().modelTransform;
                            if (modelTransform)
                            {
                                TemporaryOverlay temporaryOverlay = modelTransform.gameObject.AddComponent<TemporaryOverlay>();
                                temporaryOverlay.duration = 3f;
                                temporaryOverlay.animateShaderAlpha = true;
                                temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                                temporaryOverlay.destroyComponentOnEnd = true;
                                temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matVagrantEnergized");
                                temporaryOverlay.AddToCharacerModel(modelTransform.GetComponent<CharacterModel>());
                            }
                            new BlastAttack
                            {
                                attacker = self.body.gameObject,
                                baseDamage = self.body.damage* StaticValues.supernovaDamageCoefficient,
                                baseForce = FireMegaNova.novaForce,
                                bonusForce = Vector3.zero,
                                attackerFiltering = AttackerFiltering.NeverHitSelf,
                                crit = self.body.RollCrit(),
                                damageColorIndex = DamageColorIndex.Default,
                                damageType = DamageType.Generic,
                                falloffModel = BlastAttack.FalloffModel.None,
                                inflictor = self.body.gameObject,
                                position = position,
                                procChainMask = default(ProcChainMask),
                                procCoefficient = StaticValues.supernovaProcCoefficient,
                                radius = StaticValues.supernovaRadius,
                                losType = BlastAttack.LoSType.NearestHit,
                                teamIndex = self.body.teamComponent.teamIndex,
                                impactEffect = EffectCatalog.FindEffectIndexFromPrefab(FireMegaNova.novaImpactEffectPrefab)
                            }.Fire();
                            
                        }
                    }

                    //jellyfish heal stacks
                    if (self.body.HasBuff(Modules.Buffs.jellyfishHealStacksBuff.buffIndex))
                    {
                        int currentStacks = self.body.GetBuffCount(Buffs.jellyfishHealStacksBuff.buffIndex) - 1;
                        int damageDealt = Mathf.RoundToInt(damageInfo.damage);

                        int buffTotal = (damageDealt) / 2 + currentStacks;


                        self.body.ApplyBuff(Modules.Buffs.jellyfishHealStacksBuff.buffIndex, buffTotal);                       

                    }
                    //expunge damage bonus
                    bool expunge = (damageInfo.damageType & DamageType.BypassArmor) > 0;
                    if (expunge && damageInfo.attacker.GetComponent<CharacterBody>().baseNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
                    {
                        //deal bonus damage based on number of debuffs
                        int debuffCount = 0;
                        DotController d = DotController.FindDotController(self.gameObject);

                        foreach (BuffIndex buffType in BuffCatalog.debuffBuffIndices)
                        {
                            if (self.body.HasBuff(buffType))
                            {
                                debuffCount++;
                            }
                        }

                        DotController dotController = DotController.FindDotController(self.gameObject);
                        if (dotController)
                        {
                            for (DotController.DotIndex dotIndex = DotController.DotIndex.Bleed; dotIndex < DotController.DotIndex.Count; dotIndex++)
                            {
                                if (dotController.HasDotActive(dotIndex))
                                {
                                    debuffCount++;
                                }
                            }
                        }

                        if (self.body.HasBuff(Buffs.decayDebuff))
                        {
                            debuffCount++;
                        }

                        Debug.Log(debuffCount + "debuffcount");
                        float buffDamage = 0f;
                        float buffBaseDamage = damageInfo.damage * StaticValues.expungeDamageMultiplier;
                        buffDamage = buffBaseDamage * debuffCount;
                        damageInfo.damage += buffDamage;
                    }

                    //expose
                    if (self.body.HasBuff(RoR2Content.Buffs.MercExpose) && damageInfo.attacker.gameObject.GetComponent<CharacterBody>().baseNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
                    {
                        self.body.RemoveBuff(RoR2Content.Buffs.MercExpose);
                        float num2 = damageInfo.attacker.gameObject.GetComponent<CharacterBody>().damage * Modules.StaticValues.exposeDamageCoefficient;
                        damageInfo.damage += num2;
                        SkillLocator skillLocator = damageInfo.attacker.gameObject.GetComponent<CharacterBody>().skillLocator;
                        if (skillLocator)
                        {
                            skillLocator.DeductCooldownFromAllSkillsServer(1f);
                        }
                        EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.mercExposeConsumeEffectPrefab, damageInfo.position, Vector3.up, true);
                    }

                    //decay modded damage type to apply decay
                    if (DamageAPI.HasModdedDamageType(damageInfo, Modules.Damage.shiggyDecay) && damageInfo.attacker.gameObject.GetComponent<CharacterBody>().baseNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
                    {
                        int decayBuffCount = self.body.GetBuffCount(Buffs.decayDebuff);
                        InflictDotInfo info = new InflictDotInfo();
                        info.attackerObject = damageInfo.attacker.gameObject;
                        info.victimObject = self.gameObject;
                        info.duration = Modules.StaticValues.decayDamageTimer;
                        info.dotIndex = Modules.Dots.decayDot;


                        DotController.InflictDot(ref info);


                        DecayEffectController controller = self.gameObject.GetComponent<DecayEffectController>();
                        if (!controller)
                        {
                            controller = self.gameObject.AddComponent<DecayEffectController>();
                            controller.attackerBody = damageInfo.attacker.gameObject.GetComponent<CharacterBody>();
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

                //overclock debuff
                if (self.HasBuff(Buffs.theWorldDebuff))
                {
                    self.attackSpeed *= 0f;
                    self.moveSpeed *= 0f;


                }
                //limiter removal buff
                if (self.HasBuff(Buffs.limitBreakBuff))
                {
                    self.damage *= StaticValues.limitBreakCoefficient;
                }
                //grovetender debuff
                if (self.HasBuff(Buffs.grovetenderChainDebuff))
                {
                    self.moveSpeed *= 0f;
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

                    //DecayEffectController controller = self.gameObject.GetComponent<DecayEffectController>();
                    //if (!controller)
                    //{
                    //    controller = self.gameObject.AddComponent<DecayEffectController>();
                    //    controller.charbody = self;
                    //}

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
                    this.OverlayFunction(Modules.Assets.alphaconstructShieldBuffMat, self.body.HasBuff(Modules.Buffs.alphashieldonBuff), self);
                    this.OverlayFunction(Modules.Assets.multiplierShieldBuffMat, self.body.HasBuff(Modules.Buffs.multiplierBuff), self);
                    this.OverlayFunction(Modules.Assets.multiplierShieldBuffMat, self.body.HasBuff(Modules.Buffs.limitBreakBuff), self);
                    this.OverlayFunction(Modules.Assets.voidFormBuffMat, self.body.HasBuff(Modules.Buffs.voidFormBuff), self);
                    this.OverlayFunction(EntityStates.ImpMonster.BlinkState.destealthMaterial, self.body.HasBuff(Modules.Buffs.deathAuraBuff), self);
                    this.OverlayFunction(EntityStates.ImpMonster.BlinkState.destealthMaterial, self.body.HasBuff(Modules.Buffs.deathAuraDebuff), self);
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
