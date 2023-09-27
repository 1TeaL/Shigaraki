using BepInEx;
using BepInEx.Bootstrap;
//using ShiggyMod.Equipment;
//using ShiggyMod.Items;
using ShiggyMod.Modules;
//using ShiggyMod.Modules.Networking;
using ShiggyMod.Modules.Survivors;
using ShiggyMod.SkillStates;
using EntityStates;
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
using R2API;

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[module: UnverifiableCode]

namespace ShiggyMod
{
    //neeed this!
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.sound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.language", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.prefab", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.networking", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.recalculatestats", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.damagetype", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.bepis.r2api.dot", BepInDependency.DependencyFlags.HardDependency)]

    //don't need 
    //[BepInDependency("com.bepis.r2api.loadout", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.bepis.r2api.artifactcode", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.bepis.r2api.commandhelper", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.bepis.r2api.content_management", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.bepis.r2api.damagetype", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.bepis.r2api.deployable", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.bepis.r2api.difficulty", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.bepis.r2api.director", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.bepis.r2api.dot", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.bepis.r2api.elites", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.bepis.r2api.items", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.bepis.r2api.lobbyconfig", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.bepis.r2api.orb", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.bepis.r2api.lobbyconfig", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.bepis.r2api.recalculatestats", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.bepis.r2api.sceneasset", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.bepis.r2api.tempvisualeffect", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.bepis.r2api.unlockable", BepInDependency.DependencyFlags.HardDependency)]

    [BepInDependency("com.KingEnderBrine.ExtraSkillSlots", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.rune580.riskofoptions", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.weliveinasociety.CustomEmotesAPI", BepInDependency.DependencyFlags.SoftDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID, MODNAME, MODVERSION)]

    
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
        public const string MODVERSION = "2.2.2";

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
            NetworkingAPI.RegisterMessageType<ForceGiveQuirkState>();
            NetworkingAPI.RegisterMessageType<OrbDamageRequest>();
            NetworkingAPI.RegisterMessageType<LightAndDarknessPullRequest>();
            NetworkingAPI.RegisterMessageType<BlastingZoneDebuffDamageRequest>();
            NetworkingAPI.RegisterMessageType<ExpungeNetworkRequest>();



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

            if (self.attacker)
            {
                GameObject attacker = self.attacker;
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
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.decayAwakenedBuff))
                {
                    //add decay to all blast attacks
                    self.AddModdedDamageType(Damage.shiggyDecay);
                }
            }
            return orig(self);
        }

        private void BulletAttack_Fire(On.RoR2.BulletAttack.orig_Fire orig, BulletAttack self)
        {
            orig(self);
            if (self.owner)
            {
                GameObject attacker = self.owner;
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
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.decayAwakenedBuff))
                {
                    //add decay to all bullet attacks
                    self.AddModdedDamageType(Damage.shiggyDecay);
                }
            }

        }

        private bool OverlapAttack_Fire(On.RoR2.OverlapAttack.orig_Fire orig, OverlapAttack self, List<HurtBox> hitResults)
        {

            if (self.attacker)
            {
                GameObject attacker = self.attacker;
                if (attacker.gameObject.GetComponent<CharacterBody>().baseNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
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
                if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.decayAwakenedBuff))
                {
                    //add decay to all overlap attacks
                    self.AddModdedDamageType(Damage.shiggyDecay);
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

            if (damageInfo.attacker)
            {

                var attackerBody = damageInfo.attacker.GetComponent<CharacterBody>();
                var victimBody = victim.GetComponent<CharacterBody>();
                //impboss buff just incase
                if (attackerBody.HasBuff(Buffs.impbossBuff))
                {
                    damageInfo.damageType |= DamageType.BleedOnHit;
                }
                //acrid buff just incase
                if (attackerBody.HasBuff(Buffs.acridBuff))
                {
                    damageInfo.damageType |= DamageType.PoisonOnHit;
                }
                //decay awakened buff just incase
                if (attackerBody.HasBuff(Buffs.decayAwakenedBuff))
                {
                    DamageAPI.AddModdedDamageType(damageInfo, Damage.shiggyDecay);
                }

                //final release buff stacks
                if (attackerBody.HasBuff(Buffs.finalReleaseBuff))
                {
                    if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                    {
                        int finalReleaseCount = attackerBody.GetBuffCount(Buffs.finalReleaseBuff);
                        attackerBody.ApplyBuff(Buffs.finalReleaseBuff.buffIndex, finalReleaseCount + 1);
                    }

                }

                //disable this for now- not really needed
                ////one for all for one double damage and proc buff
                //if (body.HasBuff(Modules.Buffs.OFAFOBuff) && victimBody.baseNameToken != ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
                //{
                //    if (damageInfo.damage > 0)
                //    {
                //        DamageInfo damageInfo2 = new DamageInfo();
                //        damageInfo2.damage = damageInfo.damage;
                //        damageInfo2.position = victimBody.corePosition;
                //        damageInfo2.force = Vector3.zero;
                //        damageInfo2.damageColorIndex = DamageColorIndex.Default;
                //        damageInfo2.crit = false;
                //        damageInfo2.attacker = body.gameObject;
                //        damageInfo2.inflictor = victimBody.gameObject;
                //        damageInfo2.damageType = damageInfo.damageType;
                //        damageInfo2.procCoefficient = damageInfo.procCoefficient;
                //        damageInfo2.procChainMask = default(ProcChainMask);
                //        victimBody.healthComponent.TakeDamage(damageInfo2);
                //    }
                //}

                //commando buff
                if (attackerBody.HasBuff(Modules.Buffs.commandoBuff))
                {
                    if(damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT && damageInfo.procCoefficient > 0f)
                    {
                        DamageInfo damageInfo2 = new DamageInfo();
                        damageInfo2.damage = damageInfo.damage * StaticValues.commandoDamageMultiplier;
                        damageInfo2.position = victimBody.corePosition;
                        damageInfo2.force = Vector3.zero;
                        damageInfo2.procCoefficient = StaticValues.commandoProcCoefficient;
                        damageInfo2.damageColorIndex = DamageColorIndex.Default;
                        damageInfo2.crit = false;
                        damageInfo2.attacker = attackerBody.gameObject;
                        damageInfo2.inflictor = victimBody.gameObject;
                        damageInfo2.damageType = damageInfo.damageType;
                        damageInfo2.procChainMask = default(ProcChainMask);
                        victimBody.healthComponent.TakeDamage(damageInfo2);
                    }
                }

                //vagrant buff
                if (attackerBody.HasBuff(Modules.Buffs.vagrantBuff) && !victimBody.HasBuff(Modules.Buffs.vagrantdisableBuff))
                {
                    if (damageInfo.damage / attackerBody.damage >= StaticValues.vagrantdamageThreshold && damageInfo.procCoefficient > 0f)
                    {
                        attackerBody.ApplyBuff(Modules.Buffs.vagrantBuff.buffIndex, 0);
                        attackerBody.ApplyBuff(Buffs.vagrantdisableBuff.buffIndex, StaticValues.vagrantCooldown);
                        victimBody.AddTimedBuffAuthority(Buffs.vagrantDebuff.buffIndex, StaticValues.vagrantCooldown);
                        Util.PlaySound(JellyNova.novaSoundString, base.gameObject);
                            
                        if (JellyNova.novaEffectPrefab)
                        {
                            EffectManager.SpawnEffect(JellyNova.novaEffectPrefab, new EffectData
                            {
                                origin = victimBody.transform.position,
                                scale = StaticValues.vagrantRadius * attackerBody.attackSpeed/3
                            }, true);
                        }
                        new BlastAttack
                        {
                            crit = false,
                            attacker = attackerBody.gameObject,
                            teamIndex = TeamComponent.GetObjectTeam(attackerBody.gameObject),
                            falloffModel = BlastAttack.FalloffModel.None,
                            baseDamage = attackerBody.damage * StaticValues.vagrantDamageCoefficient * attackerBody.attackSpeed / 3,
                            damageType = DamageType.Stun1s,
                            damageColorIndex = DamageColorIndex.Default,
                            baseForce = 0,
                            position = victimBody.transform.position,
                            radius = StaticValues.vagrantRadius * attackerBody.attackSpeed / 3,
                            procCoefficient = 1f,
                            attackerFiltering = AttackerFiltering.NeverHitSelf,
                        }.Fire();
                            

                    }
                }
                //tar buff
                if (attackerBody.HasBuff(Modules.Buffs.claydunestriderBuff))
                {
                    if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                    {
                        damageInfo.damageType |= DamageType.ClayGoo;
                    }
                }
                if (attackerBody.HasBuff(Modules.Buffs.greaterwispBuff))
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
                            attacker = attackerBody.gameObject,
                            teamIndex = TeamComponent.GetObjectTeam(attackerBody.gameObject),
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
                if (attackerBody.HasBuff(Buffs.elementalFusionFireBuff)
                    | attackerBody.HasBuff(Buffs.elementalFusionFreezeBuff)
                    | attackerBody.HasBuff(Buffs.elementalFusionShockBuff))
                {
                    if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT && damageInfo.procCoefficient > 0f)
                    {
                        //deal ignite, freeze, or shock damage type every 5 hits
                        var elementalBuffCount = attackerBody.GetBuffCount(Buffs.elementalFusionBuffStacks);

                        if (elementalBuffCount < StaticValues.elementalFusionThreshold)
                        {
                            attackerBody.ApplyBuff(Buffs.elementalFusionBuffStacks.buffIndex, elementalBuffCount + 1);
                                                                
                        }
                        else if (elementalBuffCount >= StaticValues.elementalFusionThreshold && victimBody)
                        {
                            if (attackerBody.HasBuff(Buffs.elementalFusionFireBuff))
                            {

                                attackerBody.ApplyBuff(Buffs.elementalFusionFireBuff.buffIndex, 0);
                                attackerBody.ApplyBuff(Buffs.elementalFusionFreezeBuff.buffIndex, 1);
                                attackerBody.ApplyBuff(Buffs.elementalFusionBuffStacks.buffIndex, 0);

                                EffectManager.SpawnEffect(Modules.Assets.artificerFireMuzzleEffect, new EffectData
                                {
                                    origin = attackerBody.corePosition,
                                    scale = 1f,
                                    rotation = Quaternion.identity,
                                }, false);

                            }
                            else if (attackerBody.HasBuff(Buffs.elementalFusionFreezeBuff))
                            {
                                attackerBody.ApplyBuff(Buffs.elementalFusionFreezeBuff.buffIndex, 0);
                                attackerBody.ApplyBuff(Buffs.elementalFusionShockBuff.buffIndex, 1);
                                attackerBody.ApplyBuff(Buffs.elementalFusionBuffStacks.buffIndex, 0);

                                EffectManager.SpawnEffect(Modules.Assets.artificerIceMuzzleEffect, new EffectData
                                {
                                    origin = attackerBody.corePosition,
                                    scale = 1f,
                                    rotation = Quaternion.identity,
                                }, false);
                            }
                            else if (attackerBody.HasBuff(Buffs.elementalFusionShockBuff))
                            {
                                attackerBody.ApplyBuff(Buffs.elementalFusionShockBuff.buffIndex, 0);
                                attackerBody.ApplyBuff(Buffs.elementalFusionFireBuff.buffIndex, 1);
                                attackerBody.ApplyBuff(Buffs.elementalFusionBuffStacks.buffIndex, 0);

                                EffectManager.SpawnEffect(Modules.Assets.artificerLightningMuzzleEffect, new EffectData
                                {
                                    origin = attackerBody.corePosition,
                                    scale = 1f,
                                    rotation = Quaternion.identity,
                                }, false);
                            }
                        }
                    }

                }


                //omniboost buff stacks
                if (attackerBody.HasBuff(Buffs.omniboostBuff))
                {
                    if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT && damageInfo.procCoefficient > 0f)
                    {
                        //add a debuff stack to the enemy, after 3 stacks gain your own buff stack
                        var omnidebuffCount = victimBody.GetBuffCount(Buffs.omniboostDebuffStacks);
                        if (omnidebuffCount < StaticValues.omniboostNumberOfHits)
                        {
                            victimBody.ApplyBuff(Buffs.omniboostDebuffStacks.buffIndex, omnidebuffCount + 1);
                            omnidebuffCount++;

                            if (omnidebuffCount >= StaticValues.omniboostNumberOfHits)
                            {
                                var omniBuffCount = attackerBody.GetBuffCount(Buffs.omniboostBuffStacks);
                                attackerBody.ApplyBuff(Buffs.omniboostBuffStacks.buffIndex, omniBuffCount + 1);
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
                            var omniBuffCount = attackerBody.GetBuffCount(Buffs.omniboostBuffStacks);
                            attackerBody.ApplyBuff(Buffs.omniboostBuffStacks.buffIndex, omniBuffCount + 1);
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
                if (attackerBody.HasBuff(Modules.Buffs.bigbangBuff))
                {

                    if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT && damageInfo.procCoefficient > 0f)
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
                                        scale = StaticValues.bigbangBuffRadius * attackerBody.attackSpeed / 3
                                    }, true);
                                }
                                new BlastAttack
                                {
                                    crit = false,
                                    attacker = attackerBody.gameObject,
                                    teamIndex = TeamComponent.GetObjectTeam(attackerBody.gameObject),
                                    falloffModel = BlastAttack.FalloffModel.None,
                                    baseDamage = damageInfo.damage * StaticValues.bigbangBuffCoefficient,
                                    damageType = DamageType.Stun1s,
                                    damageColorIndex = DamageColorIndex.Default,
                                    baseForce = 0,
                                    position = victimBody.transform.position,
                                    radius = StaticValues.bigbangBuffRadius * attackerBody.attackSpeed / 3,
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
                                    scale = StaticValues.bigbangBuffRadius * attackerBody.attackSpeed / 3
                                }, true);
                            }
                            new BlastAttack
                            {
                                crit = false,
                                attacker = attackerBody.gameObject,
                                teamIndex = TeamComponent.GetObjectTeam(attackerBody.gameObject),
                                falloffModel = BlastAttack.FalloffModel.None,
                                baseDamage = damageInfo.damage * StaticValues.bigbangBuffCoefficient,
                                damageType = DamageType.Stun1s,
                                damageColorIndex = DamageColorIndex.Default,
                                baseForce = 0,
                                position = victimBody.transform.position,
                                radius = StaticValues.bigbangBuffRadius * attackerBody.attackSpeed / 3,
                                procCoefficient = 0f,
                                attackerFiltering = AttackerFiltering.NeverHitSelf,
                            }.Fire();
                        }
                    }                        
                }

                //wisper buff
                if (attackerBody.HasBuff(Buffs.wisperBuff))
                {
                    if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT && damageInfo.procCoefficient > 0f)
                    {
                        DevilOrb devilOrb = new DevilOrb
                        {
                            origin = attackerBody.corePosition,
                            damageValue = attackerBody.damage * StaticValues.wisperBuffDamageCoefficient,
                            teamIndex = attackerBody.teamComponent.teamIndex,
                            attacker = attackerBody.gameObject,
                            damageColorIndex = DamageColorIndex.Item,
                            scale = 1f,
                            effectType = DevilOrb.EffectType.Wisp,
                            procCoefficient = 0f,
                        };
                        if (devilOrb.target = victimBody.mainHurtBox)
                        {
                            devilOrb.isCrit = Util.CheckRoll(attackerBody.crit, attackerBody.master);
                            OrbManager.instance.AddOrb(devilOrb);
                        }
                    }
                }

                //light form debuff application
                if (attackerBody.HasBuff(Buffs.lightFormBuff))
                {
                    if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT && damageInfo.procCoefficient > 0f)
                    {
                        int lightcount = victimBody.GetBuffCount(Buffs.lightFormDebuff);
                        victimBody.ApplyBuff(Buffs.lightFormDebuff.buffIndex, lightcount + 1);

                    }
                }
                //darkness form debuff application
                if (attackerBody.HasBuff(Buffs.darknessFormBuff))
                {
                    if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT && damageInfo.procCoefficient > 0f)
                    {
                        int darkcount = victimBody.GetBuffCount(Buffs.darknessFormDebuff);
                        victimBody.ApplyBuff(Buffs.darknessFormDebuff.buffIndex, darkcount + 1);

                    }
                }

                //light form debuff effect
                if (victimBody.HasBuff(Buffs.lightFormDebuff))
                {
                    if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT && damageInfo.procCoefficient > 0f)
                    {
                        new OrbDamageRequest(victimBody.masterObjectId, damageInfo.damage, attackerBody.masterObjectId).Send(NetworkDestination.Clients);
                    }
                }

                //light and darkness form debuff application
                if (attackerBody.HasBuff(Buffs.lightAndDarknessFormBuff))
                {
                    if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT && damageInfo.procCoefficient > 0f)
                    {
                        int buffcount = victimBody.GetBuffCount(Buffs.lightAndDarknessFormDebuff);
                        victimBody.ApplyBuff(Buffs.lightAndDarknessFormDebuff.buffIndex, buffcount + 1);

                    }
                }
                //light and darkness form debuff effect
                if (victimBody.HasBuff(Buffs.lightAndDarknessFormDebuff))
                {
                    if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT && damageInfo.procCoefficient > 0f)
                    {
                        int buffcount = victimBody.GetBuffCount(Buffs.lightAndDarknessFormDebuff);
                        new LightAndDarknessPullRequest(attackerBody.masterObjectId, victimBody.corePosition, Vector3.up, StaticValues.lightAndDarknessRange + StaticValues.lightAndDarknessRangeAddition * buffcount, 0f, damageInfo.damage * (StaticValues.lightAndDarknessBonusDamage * buffcount), 360f, true).Send(NetworkDestination.Clients);
                        
                    }                   

                }

                //limit break buff health cost
                if (attackerBody.HasBuff(Buffs.limitBreakBuff))
                {
                    if (damageInfo.damage > 0 && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                    {
                        //Debug.Log("deal damage to self");

                        new SpendHealthNetworkRequest(attackerBody.masterObjectId, attackerBody.healthComponent.fullHealth * StaticValues.limitBreakHealthCostCoefficient).Send(NetworkDestination.Clients);

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

                    if (Modules.Config.allowVoice.Value)
                    {
                        AkSoundEngine.PostEvent("ShiggyDeath", self.gameObject);
                    }
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

                //omniboost buff stacks halve on kill
                if (damageReport.attackerBody.HasBuff(Buffs.omniboostBuffStacks))
                {
                    if (damageReport.damageInfo.damage > 0 && damageReport.attackerBody.hasEffectiveAuthority)
                    {
                        int omniBoostBuffStacksBuffCount = damageReport.attackerBody.GetBuffCount(Buffs.omniboostBuffStacks);
                        damageReport.attackerBody.ApplyBuff(Buffs.doubleTimeBuffStacks.buffIndex, omniBoostBuffStacksBuffCount/2);
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

            //loader passive
            if (!flag && report.attackerBody.HasBuff(Modules.Buffs.loaderBuff))
            {
                CharacterBody attackerBody = report.attackerBody;
                attackerBody.healthComponent.AddBarrierAuthority(report.damageDealt * StaticValues.loaderBarrierGainCoefficient);
            }

        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            //orig(self, damageInfo);

            orig(self, damageInfo);

            if (self)
            {
                if (damageInfo.attacker)
                {
                    var victimBody = self.body;
                    var attackerBody = damageInfo.attacker.GetComponent<CharacterBody>();
                    if (attackerBody && victimBody)
                    {

                        //death aura buff and debuff
                        if (victimBody.HasBuff(Buffs.deathAuraDebuff))
                        {
                            if (damageInfo.damageType == DamageType.DoT)
                            {
                                damageInfo.damage *= (1 + victimBody.GetBuffCount(Buffs.deathAuraDebuff) * StaticValues.deathAuraDebuffCoefficient);
                            }
                        }
                        if (attackerBody.HasBuff(Buffs.deathAuraBuff))
                        {
                            if (damageInfo.damageType == DamageType.DoT)
                            {
                                damageInfo.damage *= (1 + attackerBody.GetBuffCount(Buffs.deathAuraBuff) * StaticValues.deathAuraBuffCoefficient);
                            }
                        }


                        //multiplier spend energy
                        if (attackerBody.HasBuff(Buffs.multiplierBuff))
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
                                        attackerBody.ApplyBuff(Buffs.multiplierBuff.buffIndex, 0);
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


                        //supernova stacks self damage but dont check attacker
                        if (victimBody.HasBuff(Modules.Buffs.supernovaBuff.buffIndex))
                        {

                            int currentStacks = victimBody.GetBuffCount(Buffs.supernovaBuff.buffIndex) - 1;
                            int damageDealt = Mathf.RoundToInt(damageInfo.damage);

                            int buffTotal = (damageDealt) + currentStacks;



                            if (buffTotal < Mathf.RoundToInt(StaticValues.supernovaHealthThreshold * victimBody.healthComponent.fullCombinedHealth))
                            {
                                victimBody.ApplyBuff(Modules.Buffs.supernovaBuff.buffIndex, buffTotal);
                            }
                            else if (buffTotal >= Mathf.RoundToInt(StaticValues.supernovaHealthThreshold * victimBody.healthComponent.fullCombinedHealth))
                            {
                                victimBody.ApplyBuff(Modules.Buffs.supernovaBuff.buffIndex, 1);

                                Vector3 position = victimBody.transform.position;
                                Util.PlaySound(FireMegaNova.novaSoundString, victimBody.gameObject);
                                EffectManager.SpawnEffect(FireMegaNova.novaEffectPrefab, new EffectData
                                {
                                    origin = position,
                                    scale = StaticValues.supernovaRadius,
                                    rotation = Quaternion.LookRotation(self.transform.position)

                                }, true);

                                Transform modelTransform = victimBody.gameObject.GetComponent<ModelLocator>().modelTransform;
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
                                    attacker = victimBody.gameObject,
                                    baseDamage = victimBody.damage * StaticValues.supernovaDamageCoefficient,
                                    baseForce = FireMegaNova.novaForce,
                                    bonusForce = Vector3.zero,
                                    attackerFiltering = AttackerFiltering.NeverHitSelf,
                                    crit = victimBody.RollCrit(),
                                    damageColorIndex = DamageColorIndex.Default,
                                    damageType = DamageType.Generic,
                                    falloffModel = BlastAttack.FalloffModel.None,
                                    inflictor = victimBody.gameObject,
                                    position = position,
                                    procChainMask = default(ProcChainMask),
                                    procCoefficient = StaticValues.supernovaProcCoefficient,
                                    radius = StaticValues.supernovaRadius,
                                    losType = BlastAttack.LoSType.NearestHit,
                                    teamIndex = victimBody.teamComponent.teamIndex,
                                    impactEffect = EffectCatalog.FindEffectIndexFromPrefab(FireMegaNova.novaImpactEffectPrefab)
                                }.Fire();

                            }
                        }

                        //expunge damage bonus
                        //if (attackerBody.baseNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
                        //{
                        //    if (damageInfo.damageType == DamageType.BypassArmor)
                        //    {

                        //}


                        //darkness form debuff effect
                        if (victimBody.HasBuff(Buffs.darknessFormDebuff))
                        {
                            if (damageInfo.damageType != DamageType.DoT && damageInfo.procCoefficient > 0f)
                            {
                                int darkcount = victimBody.GetBuffCount(Buffs.darknessFormDebuff);
                                float darknum = damageInfo.damage * Modules.StaticValues.darkFormBonusDamage * darkcount;
                                damageInfo.damage += darknum;

                                //DamageInfo darkDamage = new DamageInfo();
                                //darkDamage.damage = darknum;
                                //darkDamage.position = victimBody.transform.position;
                                //darkDamage.force = Vector3.zero;
                                //darkDamage.damageColorIndex = DamageColorIndex.WeakPoint;
                                //darkDamage.crit = false;
                                //darkDamage.attacker = attackerBody.gameObject;
                                //darkDamage.damageType = DamageType.Generic;
                                //darkDamage.procCoefficient = 0f;
                                //darkDamage.procChainMask = default(ProcChainMask);
                                //victimBody.healthComponent.TakeDamage(darkDamage);

                            }
                        }

                        //expose
                        if (victimBody.HasBuff(RoR2Content.Buffs.MercExpose) && attackerBody.baseNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
                        {
                            victimBody.RemoveBuff(RoR2Content.Buffs.MercExpose);
                            float num2 = attackerBody.damage * Modules.StaticValues.exposeDamageCoefficient;
                            damageInfo.damage += num2;
                            SkillLocator skillLocator = attackerBody.skillLocator;
                            if (skillLocator)
                            {
                                skillLocator.DeductCooldownFromAllSkillsServer(1f);
                            }
                            EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.mercExposeConsumeEffectPrefab, damageInfo.position, Vector3.up, true);
                        }

                        //decay modded damage type to apply decay
                        if (DamageAPI.HasModdedDamageType(damageInfo, Modules.Damage.shiggyDecay))
                        {
                            int decayBuffCount = victimBody.GetBuffCount(Buffs.decayDebuff);
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
                                controller.attackerBody = attackerBody;
                            }

                        }

                        //for self damage
                        bool flag = (damageInfo.damageType & DamageType.BypassArmor) > DamageType.Generic;
                        //jellyfish heal stacks
                        if (victimBody.HasBuff(Modules.Buffs.jellyfishHealStacksBuff.buffIndex) && !flag && damageInfo.damage > 0f && attackerBody != victimBody)
                        {
                            int currentStacks = victimBody.GetBuffCount(Buffs.jellyfishHealStacksBuff.buffIndex) - 1;
                            int damageDealt = Mathf.RoundToInt(damageInfo.damage);

                            int buffTotal = (damageDealt) / 2 + currentStacks;


                            victimBody.ApplyBuff(Modules.Buffs.jellyfishHealStacksBuff.buffIndex, buffTotal);

                        }
                        //gargoyle protection buff
                        if (victimBody.HasBuff(Buffs.gargoyleProtectionBuff) && !flag && damageInfo.damage > 0f && attackerBody != victimBody)
                        {
                            //reduce damage and reflect that portion back
                            damageInfo.damage -= damageInfo.damage * StaticValues.gargoyleProtectionDamageReductionCoefficient;

                            DamageInfo damageInfo2 = new DamageInfo();
                            damageInfo2.damage = damageInfo.damage * StaticValues.gargoyleProtectionDamageReductionCoefficient;
                            damageInfo2.position = damageInfo.attacker.transform.position;
                            damageInfo2.force = Vector3.zero;
                            damageInfo2.damageColorIndex = DamageColorIndex.WeakPoint;
                            damageInfo2.crit = false;
                            damageInfo2.attacker = victimBody.gameObject;
                            damageInfo2.damageType = DamageType.BypassArmor;
                            damageInfo2.procCoefficient = 0f;
                            damageInfo2.procChainMask = default(ProcChainMask);
                            attackerBody.healthComponent.TakeDamage(damageInfo2);

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

                        //reversal effect
                        if (victimBody.HasBuff(Buffs.reversalBuffStacks) && !flag && damageInfo.damage > 0f && attackerBody != victimBody)
                        {
                            new ForceReversalState(victimBody.masterObjectId, attackerBody.transform.position).Send(NetworkDestination.Server);
                            damageInfo.force = Vector3.zero;
                            damageInfo.rejected = true;
                            //do counterattack as well

                            if (victimBody.HasBuff(Buffs.blindSensesBuff.buffIndex))
                            {
                                //blind senses damage 
                                LightningOrb lightningOrb2 = new LightningOrb();
                                lightningOrb2.attacker = victimBody.gameObject;
                                lightningOrb2.bouncedObjects = null;
                                lightningOrb2.bouncesRemaining = 0;
                                lightningOrb2.damageCoefficientPerBounce = 1f;
                                lightningOrb2.damageColorIndex = DamageColorIndex.Item;
                                lightningOrb2.damageValue = damageInfo.damage * StaticValues.blindSensesDamageCoefficient;
                                lightningOrb2.isCrit = victimBody.RollCrit();
                                lightningOrb2.lightningType = LightningOrb.LightningType.RazorWire;
                                lightningOrb2.origin = victimBody.corePosition;
                                lightningOrb2.procChainMask = default(ProcChainMask);
                                lightningOrb2.procChainMask.AddProc(ProcType.Thorns);
                                lightningOrb2.procCoefficient = 1f;
                                lightningOrb2.damageType = DamageType.Stun1s;
                                lightningOrb2.range = 0f;
                                lightningOrb2.teamIndex = victimBody.teamComponent.teamIndex;
                                lightningOrb2.target = damageInfo.attacker.gameObject.GetComponent<CharacterBody>().mainHurtBox;
                                OrbManager.instance.AddOrb(lightningOrb2);


                                EffectData effectData = new EffectData
                                {
                                    origin = damageInfo.position,
                                    rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                                };
                                EffectManager.SpawnEffect(HealthComponent.AssetReferences.bearEffectPrefab, effectData, true);

                            }
                        }

                        //blind senses buff
                        if (victimBody.HasBuff(Buffs.blindSensesBuff.buffIndex) && !flag && damageInfo.damage > 0f && attackerBody != victimBody)
                        {

                            if (Util.CheckRoll(StaticValues.blindSensesBlockChance + Util.ConvertAmplificationPercentageIntoReductionPercentage(StaticValues.blindSensesBlockChance * (float)victimBody.inventory.GetItemCount(RoR2Content.Items.Bear)), victimBody.master))
                            {

                                //blind senses damage 
                                LightningOrb lightningOrb = new LightningOrb();
                                lightningOrb.attacker = victimBody.gameObject;
                                lightningOrb.bouncedObjects = null;
                                lightningOrb.bouncesRemaining = 0;
                                lightningOrb.damageCoefficientPerBounce = 1f;
                                lightningOrb.damageColorIndex = DamageColorIndex.Item;
                                lightningOrb.damageValue = damageInfo.damage * StaticValues.blindSensesDamageCoefficient;
                                lightningOrb.isCrit = victimBody.RollCrit();
                                lightningOrb.lightningType = LightningOrb.LightningType.RazorWire;
                                lightningOrb.origin = victimBody.corePosition;
                                lightningOrb.procChainMask = default(ProcChainMask);
                                lightningOrb.procChainMask.AddProc(ProcType.Thorns);
                                lightningOrb.procCoefficient = 1f;
                                lightningOrb.damageType = DamageType.Stun1s;
                                lightningOrb.range = 0f;
                                lightningOrb.teamIndex = victimBody.teamComponent.teamIndex;
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
                        if (victimBody.HasBuff(Buffs.stoneFormStillBuff.buffIndex) && !flag && damageInfo.damage > 0f && attackerBody != victimBody)
                        {
                            damageInfo.force = Vector3.zero;
                            if (Util.CheckRoll(StaticValues.stoneFormBlockChance, victimBody.master))
                            {

                                //blind senses damage 
                                if (victimBody.HasBuff(Buffs.blindSensesBuff.buffIndex))
                                {
                                    LightningOrb lightningOrb = new LightningOrb();
                                    lightningOrb.attacker = victimBody.gameObject;
                                    lightningOrb.bouncedObjects = null;
                                    lightningOrb.bouncesRemaining = 0;
                                    lightningOrb.damageCoefficientPerBounce = 1f;
                                    lightningOrb.damageColorIndex = DamageColorIndex.Item;
                                    lightningOrb.damageValue = damageInfo.damage * StaticValues.blindSensesDamageCoefficient;
                                    lightningOrb.isCrit = victimBody.RollCrit();
                                    lightningOrb.lightningType = LightningOrb.LightningType.RazorWire;
                                    lightningOrb.origin = victimBody.corePosition;
                                    lightningOrb.procChainMask = default(ProcChainMask);
                                    lightningOrb.procChainMask.AddProc(ProcType.Thorns);
                                    lightningOrb.procCoefficient = 1f;
                                    lightningOrb.damageType = DamageType.Stun1s;
                                    lightningOrb.range = 0f;
                                    lightningOrb.teamIndex = victimBody.teamComponent.teamIndex;
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
                        if (victimBody.HasBuff(Buffs.stonetitanBuff.buffIndex) && !flag && damageInfo.damage > 0f && attackerBody != victimBody)
                        {
                            if (self.combinedHealthFraction < 0.5f && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                            {
                                damageInfo.force = Vector3.zero;
                                damageInfo.damage -= victimBody.armor;
                                if (damageInfo.damage < 0f)
                                {
                                    self.Heal(Mathf.Abs(damageInfo.damage), default(RoR2.ProcChainMask), true);
                                    damageInfo.damage = 0f;

                                }

                            }
                            else
                            {
                                damageInfo.force = Vector3.zero;
                                damageInfo.damage = Mathf.Max(1f, damageInfo.damage - victimBody.armor);
                            }
                        }

                        //alpha construct shield
                        if (victimBody.HasBuff(Modules.Buffs.alphashieldonBuff.buffIndex) && !flag && damageInfo.damage > 0f && attackerBody != victimBody)
                        {
                            EffectData effectData2 = new EffectData
                            {
                                origin = damageInfo.position,
                                rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                            };
                            EffectManager.SpawnEffect(HealthComponent.AssetReferences.bearVoidEffectPrefab, effectData2, true);
                            damageInfo.rejected = true;
                            victimBody.ApplyBuff(Modules.Buffs.alphashieldonBuff.buffIndex, 0);
                            victimBody.ApplyBuff(Modules.Buffs.alphashieldoffBuff.buffIndex, StaticValues.alphaconstructCooldown);


                        }
                        //spike buff
                        if (victimBody.HasBuff(Modules.Buffs.gupspikeBuff.buffIndex) && !flag && damageInfo.damage > 0f && attackerBody != victimBody)
                        {
                            //Spike buff

                            blastAttack = new BlastAttack();
                            blastAttack.radius = Modules.StaticValues.spikedamageRadius;
                            blastAttack.procCoefficient = 0.5f;
                            blastAttack.position = self.transform.position;
                            blastAttack.attacker = self.gameObject;
                            blastAttack.crit = Util.CheckRoll(victimBody.crit, victimBody.master);
                            blastAttack.baseDamage = victimBody.damage * Modules.StaticValues.spikedamageCoefficient;
                            blastAttack.falloffModel = BlastAttack.FalloffModel.None;
                            blastAttack.baseForce = 100f;
                            blastAttack.teamIndex = TeamComponent.GetObjectTeam(victimBody.gameObject);
                            blastAttack.damageType = DamageType.Generic | DamageType.BleedOnHit;
                            blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;

                            DamageAPI.AddModdedDamageType(blastAttack, Damage.shiggyDecay);


                            blastAttack.Fire();


                            EffectManager.SpawnEffect(Modules.Assets.GupSpikeEffect, new EffectData
                            {
                                origin = self.transform.position,
                                scale = Modules.StaticValues.spikedamageRadius / 3,
                                rotation = Quaternion.LookRotation(self.transform.position)

                            }, true);

                        }

                        //orig(self, damageInfo);

                    }

                }
                

            }


        }

        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            //buffs 
            if (self?.healthComponent)
            {
                orig.Invoke(self);

                //wildcard buffs
                if (self.HasBuff(Buffs.wildcardDamageBuff))
                {
                    self.damage *= StaticValues.wildcardDamageCoefficient;
                }
                if (self.HasBuff(Buffs.wildcardSpeedBuff))
                {
                    self.moveSpeed *= StaticValues.wildcardSpeedCoefficient;
                }
                if (self.HasBuff(Buffs.wildcardSlowBuff))
                {
                    self.moveSpeed /= StaticValues.wildcardSpeedCoefficient;
                }
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
                    if (Modules.Config.allowVoice.Value)
                    {
                        AkSoundEngine.PostEvent("ShiggyDekuCollab", self.gameObject);
                    }
                }
                else
                {
                    if (Modules.Config.allowVoice.Value) 
                    { 
                        AkSoundEngine.PostEvent("ShiggyEntrance", self.gameObject); 
                    }
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
                    this.OverlayFunction(Modules.Assets.limitBreakBuffMat, self.body.HasBuff(Modules.Buffs.limitBreakBuff), self);
                    this.OverlayFunction(Modules.Assets.voidFormBuffMat, self.body.HasBuff(Modules.Buffs.voidFormBuff), self);
                    this.OverlayFunction(Modules.Assets.voidFormBuffMat, self.body.HasBuff(Modules.Buffs.decayDebuff), self);
                    this.OverlayFunction(EntityStates.ImpMonster.BlinkState.destealthMaterial, self.body.HasBuff(Modules.Buffs.deathAuraBuff), self);
                    this.OverlayFunction(Modules.Assets.deathAuraBuffMat, self.body.HasBuff(Modules.Buffs.deathAuraDebuff), self); 
                    this.OverlayFunction(EntityStates.ImpMonster.BlinkState.destealthMaterial, self.body.HasBuff(Modules.Buffs.darknessFormBuff), self);
                    this.OverlayFunction(Modules.Assets.lightFormBuffMat, self.body.HasBuff(Modules.Buffs.lightFormBuff), self);
                    this.OverlayFunction(Modules.Assets.lightAndDarknessMat, self.body.HasBuff(Modules.Buffs.lightAndDarknessFormBuff), self);
                    this.OverlayFunction(Modules.Assets.blastingZoneBurnMat, self.body.HasBuff(Modules.Buffs.blastingZoneBurnDebuff), self);
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
