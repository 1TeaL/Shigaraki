using BepInEx;
using BepInEx.Bootstrap;
using EmotesAPI;
using EntityStates;
using EntityStates.JellyfishMonster;
using EntityStates.VagrantMonster;
using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using R2API.Utils;
using RoR2;
using RoR2.Items;
using RoR2.Navigation;
using RoR2.Orbs;
using RoR2.Projectile;
//using ShiggyMod.Equipment;
//using ShiggyMod.Items;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Networking;
using ShiggyMod.Modules.Quirks;
//using ShiggyMod.Modules.Networking;
using ShiggyMod.Modules.Survivors;
using ShiggyMod.Modules.UI;
using ShiggyMod.SkillStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using static ShiggyMod.Modules.Quirks.QuirkRegistry;

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
        public const string MODVERSION = "3.0.0";

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
            Modules.ShiggyAsset.Initialize();
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


            // IMPORTANT: do this BEFORE creating SkillDefs, because SkillDef creation calls QuirkIconBank.Get(...)
            Modules.Quirks.QuirkIconBank.RegisterFromRegistryData();

            // Create survivor SkillDefs (and anything else that calls QuirkIconBank.Get)
            new Shiggy().Initialize();

            // IMPORTANT: do this AFTER SkillDefs exist, so ResolveFromData can bind them and build reverse maps
            Modules.Quirks.QuirkRegistry.BindQuirkRegistry();

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

            NetworkingAPI.RegisterMessageType<ApexResetSlotRequest>();
            NetworkingAPI.RegisterMessageType<EquipLoadoutRequest>();
            NetworkingAPI.RegisterMessageType<GivePassiveRequest>();
            NetworkingAPI.RegisterMessageType<ForceQuirkOverdriveState>();
            NetworkingAPI.RegisterMessageType<SetAFOStealStateMachine>();



            // now make a content pack and add it- this part will change with the next update
            new Modules.ContentPacks().Initialize();

            RoR2.ContentManagement.ContentManager.onContentPacksAssigned += LateSetup;
            
            Hook();

        }
        private void LateSetup(HG.ReadOnlyArray<RoR2.ContentManagement.ReadOnlyContentPack> obj)
        {
            try
            {
                Modules.Quirks.QuirkIconBank.BuildAll();
                Debug.Log("[Shiggy] BindQuirkRegistry completed after content packs.");
            }
            catch (Exception e)
            {
                Debug.LogError($"[Shiggy] BindQuirkRegistry post-CPS failed: {e}");
            }
        }

        private void Start()
        {
        }


        private void Hook()
        {
            // run hooks here, disabling one is as simple as commenting out the line
            On.RoR2.CharacterBody.OnDeathStart += CharacterBody_OnDeathStart;
            On.RoR2.CharacterModel.Awake += CharacterModel_Awake;
            On.RoR2.CharacterModel.Start += CharacterModel_Start;
            RoR2.Run.onRunStartGlobal += Run_onRunStartGlobal;
            RoR2.Run.onRunDestroyGlobal += Run_onRunDestroyGlobal;
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
            CharacterBody.onBodyStartGlobal += CharacterBody_onBodyStartGlobal;

           

            if (Chainloader.PluginInfos.ContainsKey("com.weliveinasociety.CustomEmotesAPI"))
            {
                On.RoR2.SurvivorCatalog.Init += SurvivorCatalog_Init;
            }
        }

        //make sure quirk passives are added 
        private void CharacterBody_onBodyStartGlobal(CharacterBody obj)
        {
            if (!NetworkServer.active) return;
            if (!obj || !obj.master) return;

            // Only your survivor
            if (obj.bodyIndex != BodyCatalog.FindBodyIndex("ShiggyBody")) return;

            QuirkPassiveSync.SyncFromEquippedSkillsServer(obj);
        } 
        

        private static bool IsVanillaAffix(BuffDef buff)
        {
            // Expand this as needed (DLC1/DLC2 too)
            return buff == RoR2Content.Buffs.AffixRed
                || buff == RoR2Content.Buffs.AffixWhite
                || buff == RoR2Content.Buffs.AffixPoison
                || buff == RoR2Content.Buffs.AffixHaunted
                || buff == RoR2Content.Buffs.AffixBlue
                || buff == RoR2Content.Buffs.AffixLunar
                || buff == DLC1Content.Buffs.EliteEarth
                || buff == DLC1Content.Buffs.EliteVoid
                || buff == DLC2Content.Buffs.EliteAurelionite
                || buff == DLC2Content.Buffs.EliteBead;
        }

        private void Run_onRunStartGlobal(Run obj)
        {
            QuirkInventory.SeedStartingQuirksFromConfig();
        }
        private void Run_onRunDestroyGlobal(Run obj)
        {
            QuirkInventory.ResetSeedFlagForNextRun();
            QuirkInventory.Clear(); // optional: wipe at run end
        }

        private BlastAttack.Result BlastAttack_Fire(On.RoR2.BlastAttack.orig_Fire orig, BlastAttack self)
        {

            if (self.attacker)
            {
                GameObject attacker = self.attacker;
                if (attacker.GetComponent<CharacterBody>() != null)
                {
                    if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.acridBuff))
                    {
                        //add poison to all blast attacks
                        self.damageType.damageType |= DamageType.PoisonOnHit;
                    }
                    if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.impbossBuff))
                    {
                        //add bleed to all blast attacks
                        self.damageType.damageType |= DamageType.BleedOnHit;
                    }
                    if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionFireBuff))
                    {
                        //add ignite to all blast attacks
                        self.damageType.damageType |= DamageType.IgniteOnHit;
                    }
                    if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionFreezeBuff))
                    {
                        //add freeze to all blast attacks
                        self.damageType.damageType |= DamageType.Freeze2s;
                    }
                    if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionShockBuff))
                    {
                        //add freeze to all blast attacks
                        self.damageType.damageType |= DamageType.Shock5s;
                    }
                    if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.decayAwakenedBuff))
                    {
                        //add decay to all blast attacks
                        self.AddModdedDamageType(Damage.shiggyDecay);
                    }

                }
            }
            return orig(self);
        }

        private void BulletAttack_Fire(On.RoR2.BulletAttack.orig_Fire orig, BulletAttack self)
        {
            orig(self);
            if (self.owner)
            {
                if (self.owner.GetComponent<CharacterBody>() != null)
                {
                    GameObject attacker = self.owner;
                    if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.acridBuff))
                    {
                        //add poison to all bullet attacks
                        self.damageType.damageType |= DamageType.PoisonOnHit;
                    }
                    if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.impbossBuff))
                    {
                        //add bleed to all bullet attacks
                        self.damageType.damageType |= DamageType.BleedOnHit;
                    }
                    if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionFireBuff))
                    {
                        //add ignite to all bullet attacks
                        self.damageType.damageType |= DamageType.IgniteOnHit;
                    }
                    if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionFreezeBuff))
                    {
                        //add freeze to all bullet attacks
                        self.damageType.damageType |= DamageType.Freeze2s;
                    }
                    if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionShockBuff))
                    {
                        //add freeze to all bullet attacks
                        self.damageType.damageType |= DamageType.Shock5s;
                    }
                    if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.decayAwakenedBuff))
                    {
                        //add decay to all bullet attacks
                        self.AddModdedDamageType(Damage.shiggyDecay);
                    }
                }

            }

        }

        private bool OverlapAttack_Fire(On.RoR2.OverlapAttack.orig_Fire orig, OverlapAttack self, List<HurtBox> hitResults)
        {

            if (self.attacker)
            {
                if (self.attacker.GetComponent<CharacterBody>() != null)
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
                        self.damageType.damageType |= DamageType.PoisonOnHit;
                    }
                    if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.impbossBuff))
                    {
                        //add bleed to all overlap attacks
                        self.damageType.damageType |= DamageType.BleedOnHit;
                    }
                    if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionFireBuff))
                    {
                        //add ignite to all overlap attacks
                        self.damageType.damageType |= DamageType.IgniteOnHit;
                    }
                    if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionFreezeBuff))
                    {
                        //add freeze to all overlap attacks
                        self.damageType.damageType |= DamageType.Freeze2s;
                    }
                    if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.elementalFusionShockBuff))
                    {
                        //add freeze to all overlap attacks
                        self.damageType.damageType |= DamageType.Shock5s;
                    }
                    if (attacker.gameObject.GetComponent<CharacterBody>().HasBuff(Buffs.decayAwakenedBuff))
                    {
                        //add decay to all overlap attacks
                        self.AddModdedDamageType(Damage.shiggyDecay);
                    }
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

                    EffectManager.SpawnEffect(Modules.ShiggyAsset.voidMegaCrabExplosionEffect, new EffectData
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
                    //false son buff
                    if (sender.HasBuff(Buffs.falsesonStolenInheritanceBuff))
                    {
                        args.baseDamageAdd += StaticValues.falseSonHPCoefficient * sender.healthComponent.fullHealth;
                    }
                    //alloy hunter crit boost buff
                    if (sender.HasBuff(Buffs.alloyhunterCritBoostBuff))
                    {
                        args.critDamageTotalMult *= (StaticValues.alloyHunterCritBoostMultiplier);

                    }

                }
            }


        }

        private void GlobalEventManager_OnHitEnemy(On.RoR2.GlobalEventManager.orig_OnHitEnemy orig, GlobalEventManager self, DamageInfo damageInfo, GameObject victim)
        {
            var attackerBody = damageInfo.attacker ? damageInfo.attacker.GetComponent<CharacterBody>() : null;
            var victimBody = victim ? victim.GetComponent<CharacterBody>() : null;

            // =========================
            // PRE: tag the hit
            // =========================
            if (attackerBody != null)
            {
                // vanilla flags
                if (attackerBody.HasBuff(Buffs.acridBuff))
                    damageInfo.damageType |= DamageType.PoisonOnHit;

                if (attackerBody.HasBuff(Buffs.impbossBuff))
                    damageInfo.damageType |= DamageType.BleedOnHit;

                if (attackerBody.HasBuff(Buffs.elementalFusionFireBuff))
                    damageInfo.damageType |= DamageType.IgniteOnHit;

                if (attackerBody.HasBuff(Buffs.elementalFusionFreezeBuff))
                    damageInfo.damageType |= DamageType.Freeze2s;

                if (attackerBody.HasBuff(Buffs.elementalFusionShockBuff))
                    damageInfo.damageType |= DamageType.Shock5s;

                // modded type
                if (attackerBody.HasBuff(Buffs.decayAwakenedBuff))
                    damageInfo.AddModdedDamageType(Damage.shiggyDecay);

                // tar (Clay Goo) is also a tag
                if (attackerBody.HasBuff(Buffs.claydunestriderBuff))
                    damageInfo.damageType |= DamageType.ClayGoo;
            }

            // Let base game (and other mods) process the hit with your tags applied
            orig(self, damageInfo, victim);

            // =========================
            // POST: extra logic that should happen after the hit is processed
            // =========================
            if (attackerBody == null || victimBody == null)
                return;
            bool isNonDot = (damageInfo.damageType & DamageType.DoT) != DamageType.DoT;
            bool hasProcChance = damageInfo.procCoefficient > 0f;
            bool positiveDmg = damageInfo.damage > 0f;

            if(positiveDmg && isNonDot && hasProcChance)
            {
                if (attackerBody.HasBuff(Buffs.solusamalgamatorEquipmentBoostBuff))
                {
                    // seconds-based reduction
                    attackerBody.inventory.DeductActiveEquipmentCooldown(StaticValues.solusAmalgamatorEquipmentBoostCDReduction);

                }
            }

            // Solus Primed: +damage once, then consume (with optional Unleashed behavior)
            if (positiveDmg && isNonDot && hasProcChance)
            {

                // victim must be primed
                if (victimBody.HasBuff(Buffs.solusPrimedDebuff))
                {
                    int stacks = victimBody.GetBuffCount(Buffs.solusPrimedDebuff);
                    // Example: only allow Shiggy (optional)
                    // attackerAllowed = attackerBody.baseNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME";

                    // Add bonus damage as a second damage instance (cleaner than mutating the original hit)
                    // This avoids weirdness with crit/bleed/etc. already processed in orig().
                    float bonus = damageInfo.damage * StaticValues.solusPrimedDamageMult * stacks;

                    var di = new DamageInfo
                    {
                        damage = bonus,
                        position = victimBody.corePosition,
                        force = Vector3.zero,
                        crit = false, // or roll crit if you want, but usually keep false for "detonation"
                        attacker = attackerBody.gameObject,
                        inflictor = attackerBody.gameObject,
                        damageType = new DamageTypeCombo(DamageType.Generic, DamageTypeExtended.Generic, DamageSource.Secondary),
                        procCoefficient = 0f, // IMPORTANT: 0 so it doesn't proc items and recurse
                        procChainMask = default
                    };
                    victimBody.healthComponent.TakeDamage(di);

                    // Consume primed, unless Unleashed behavior says otherwise
                    bool attackerHasUnleashed = attackerBody.HasBuff(Buffs.solusSuperPrimedBuff);

                    if (!attackerHasUnleashed)
                    {
                        //reduce stacks by 1 each hit
                        victimBody.ApplyBuff(Buffs.solusPrimedDebuff.buffIndex, stacks - 1);
                    }
                    else
                    {
                        victimBody.ApplyBuff(Buffs.solusPrimedDebuff.buffIndex, stacks + 1);
                        // Unleashed extras- do a blast attack that ignites, and applies accelerant

                        new BlastAttack
                        {
                            attacker = attackerBody.gameObject,
                            teamIndex = TeamComponent.GetObjectTeam(attackerBody.gameObject),
                            crit = false,
                            falloffModel = BlastAttack.FalloffModel.None,
                            baseDamage = damageInfo.damage,
                            damageType = new DamageTypeCombo(DamageType.IgniteOnHit, DamageTypeExtended.Accelerant, DamageSource.Secondary),
                            damageColorIndex = DamageColorIndex.WeakPoint,
                            baseForce = 0f,
                            procChainMask = damageInfo.procChainMask,
                            position = victimBody.transform.position,
                            radius = StaticValues.solusFactorUnleashedBlastRadius,
                            procCoefficient = 0.1f,
                            attackerFiltering = AttackerFiltering.NeverHitSelf,
                        }.Fire();

                        var effectPrefab = ShiggyAsset.solusFactorBlastEffectPrefab;
                        if (effectPrefab)
                        {
                            EffectManager.SpawnEffect(
                                effectPrefab,
                                new EffectData
                                {
                                    origin = victimBody.transform.position,
                                    scale = StaticValues.solusFactorUnleashedBlastRadius
                                },
                                true
                            );
                        }



                    }

                }
            }

            // Final Release stacks
            if (attackerBody.HasBuff(Buffs.finalReleaseBuff) && positiveDmg && isNonDot)
            {
                int stacks = attackerBody.GetBuffCount(Buffs.finalReleaseBuff);
                attackerBody.ApplyBuff(Buffs.finalReleaseBuff.buffIndex, stacks + 1);
            }

            // Commando double-tap extra hit
            if (attackerBody.HasBuff(Buffs.commandoBuff) && positiveDmg && isNonDot && hasProcChance)
            {
                var di = new DamageInfo
                {
                    damage = damageInfo.damage * StaticValues.commandoDamageMultiplier,
                    position = victimBody.corePosition,
                    force = Vector3.zero,
                    procCoefficient = StaticValues.commandoProcCoefficient,
                    damageColorIndex = DamageColorIndex.Default,
                    crit = false,
                    attacker = attackerBody.gameObject,
                    inflictor = victimBody.gameObject,
                    damageType = new DamageTypeCombo(damageInfo.damageType, DamageTypeExtended.Generic, DamageSource.Secondary),
                    procChainMask = default
                };
                victimBody.healthComponent.TakeDamage(di);
            }
            // Chef Oil burst
            if (attackerBody.HasBuff(Buffs.chefOilBurstBuff) && positiveDmg && isNonDot && hasProcChance)
            {
                int stacks = attackerBody.GetBuffCount(Buffs.chefOilBurstStacksBuff);
                if (stacks >= StaticValues.chefOilBurstStacks)
                {
                    attackerBody.ApplyBuff(Buffs.chefOilBurstStacksBuff.buffIndex, 0);

                    EffectManager.SimpleMuzzleFlash(EntityStates.Chef.Glaze.effectPrefab, attackerBody.gameObject, "RHand", false);
                    //test to see if it works
                    EffectManager.SimpleMuzzleFlash(ShiggyAsset.chefGlazeEffectMuzzlePrefab, attackerBody.gameObject, "LHand", false);

                    Ray projectileRay = new Ray(attackerBody.aimOrigin, attackerBody.inputBank.aimDirection);
                    float x = UnityEngine.Random.Range(0f, attackerBody.spreadBloomAngle + EntityStates.Chef.Glaze.xDeviationSpread);
                    float z = UnityEngine.Random.Range(0f, 360f);
                    
                    Vector3 up = Vector3.up;
                    Vector3 axis = Vector3.Cross(up, projectileRay.direction);
                    Vector3 vector = Quaternion.Euler(0f, 0f, z) * (Quaternion.Euler(x, 0f, 0f) * Vector3.forward);
                    float y = vector.y;
                    vector.y = 0f;
                    float angle = Mathf.Atan2(vector.z, vector.x) * 57.29578f - 90f;
                    float angle2 = Mathf.Atan2(y, vector.magnitude) * 57.29578f + EntityStates.Chef.Glaze.arcAngle;
                    Vector3 forward = Quaternion.AngleAxis(angle, up) * (Quaternion.AngleAxis(angle2, axis) * projectileRay.direction);


                    FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
                    //fireProjectileInfo.projectilePrefab = EntityStates.Chef.Glaze.projectilePrefab;
                    fireProjectileInfo.projectilePrefab = ShiggyAsset.chefGlazeProjectilePrefab;
                    fireProjectileInfo.position = attackerBody.aimOrigin;
                    fireProjectileInfo.rotation = Util.QuaternionSafeLookRotation(forward);
                    fireProjectileInfo.owner = attackerBody.gameObject;
                    fireProjectileInfo.damage = attackerBody.damage * StaticValues.chefOilDamageCoefficient;
                    fireProjectileInfo.force = 0f;
                    fireProjectileInfo.crit = Util.CheckRoll(attackerBody.crit, attackerBody.master);
                    fireProjectileInfo.damageTypeOverride = new DamageTypeCombo?(new DamageTypeCombo(DamageType.WeakOnHit, DamageTypeExtended.Generic, DamageSource.Special));
                    ProjectileManager.instance.FireProjectile(fireProjectileInfo);
                }
                else if (stacks < StaticValues.chefOilBurstStacks)
                {
                    attackerBody.ApplyBuff(Buffs.chefOilBurstStacksBuff.buffIndex, stacks + 1);
                }
            }

            // Greater Wisp bonus blast
            if (attackerBody.HasBuff(Buffs.greaterwispBuff) && positiveDmg && isNonDot && hasProcChance)
            {
                if (Modules.ShiggyAsset.chargegreaterwispBall)
                {
                    EffectManager.SpawnEffect(Modules.ShiggyAsset.chargegreaterwispBall, new EffectData
                    {
                        origin = victimBody.transform.position,
                        scale = StaticValues.greaterwispballRadius,
                        rotation = Util.QuaternionSafeLookRotation(damageInfo.force)
                    }, true);
                }

                new BlastAttack
                {
                    attacker = attackerBody.gameObject,
                    teamIndex = TeamComponent.GetObjectTeam(attackerBody.gameObject),
                    crit = false,
                    falloffModel = BlastAttack.FalloffModel.None,
                    baseDamage = damageInfo.damage * StaticValues.greaterwispballDamageCoefficient,
                    damageType = new DamageTypeCombo(damageInfo.damageType, DamageTypeExtended.Generic, DamageSource.Secondary),
                    damageColorIndex = DamageColorIndex.Default,
                    baseForce = 0f,
                    procChainMask = damageInfo.procChainMask,
                    position = victimBody.transform.position,
                    radius = StaticValues.greaterwispballRadius,
                    procCoefficient = 0f,
                    attackerFiltering = AttackerFiltering.NeverHitSelf,
                }.Fire();
            }

            // Elemental Fusion cycling (fire -> freeze -> shock -> fire)
            if ((attackerBody.HasBuff(Buffs.elementalFusionFireBuff) ||
                 attackerBody.HasBuff(Buffs.elementalFusionFreezeBuff) ||
                 attackerBody.HasBuff(Buffs.elementalFusionShockBuff)) &&
                 positiveDmg && isNonDot && hasProcChance)
            {
                int stacks = attackerBody.GetBuffCount(Buffs.elementalFusionBuffStacks);
                if (stacks < StaticValues.elementalFusionThreshold)
                {
                    attackerBody.ApplyBuff(Buffs.elementalFusionBuffStacks.buffIndex, stacks + 1);
                }
                else
                {
                    // reached threshold, rotate the element and reset the counter
                    attackerBody.ApplyBuff(Buffs.elementalFusionBuffStacks.buffIndex, 0);

                    if (attackerBody.HasBuff(Buffs.elementalFusionFireBuff))
                    {
                        attackerBody.ApplyBuff(Buffs.elementalFusionFireBuff.buffIndex, 0);
                        attackerBody.ApplyBuff(Buffs.elementalFusionFreezeBuff.buffIndex, 1);
                        if (Modules.ShiggyAsset.artificerFireMuzzleEffect)
                            EffectManager.SpawnEffect(Modules.ShiggyAsset.artificerFireMuzzleEffect, new EffectData { origin = attackerBody.corePosition }, false);
                    }
                    else if (attackerBody.HasBuff(Buffs.elementalFusionFreezeBuff))
                    {
                        attackerBody.ApplyBuff(Buffs.elementalFusionFreezeBuff.buffIndex, 0);
                        attackerBody.ApplyBuff(Buffs.elementalFusionShockBuff.buffIndex, 1);
                        if (Modules.ShiggyAsset.artificerIceMuzzleEffect)
                            EffectManager.SpawnEffect(Modules.ShiggyAsset.artificerIceMuzzleEffect, new EffectData { origin = attackerBody.corePosition }, false);
                    }
                    else if (attackerBody.HasBuff(Buffs.elementalFusionShockBuff))
                    {
                        attackerBody.ApplyBuff(Buffs.elementalFusionShockBuff.buffIndex, 0);
                        attackerBody.ApplyBuff(Buffs.elementalFusionFireBuff.buffIndex, 1);
                        if (Modules.ShiggyAsset.artificerLightningMuzzleEffect)
                            EffectManager.SpawnEffect(Modules.ShiggyAsset.artificerLightningMuzzleEffect, new EffectData { origin = attackerBody.corePosition }, false);
                    }
                }
            }

            // OmniBoost stacks (debuff on victim -> buff on you)
            if (attackerBody.HasBuff(Buffs.omniboostBuff) && positiveDmg && isNonDot && hasProcChance)
            {
                var tracker = attackerBody.GetComponent<OmniboostTracker>();
                if (!tracker) tracker = attackerBody.gameObject.AddComponent<OmniboostTracker>();

                bool swapped = tracker.lastVictim && tracker.lastVictim != victimBody;

                if (swapped)
                {
                    // 1) Clear counter on the old victim so only one target can be "tracked"
                    tracker.lastVictim.ApplyBuff(Buffs.omniboostDebuffStacks.buffIndex, 0);

                    // 2) Halve your stacks immediately
                    int myStacks = attackerBody.GetBuffCount(Buffs.omniboostBuffStacks);

                    // Choose floor or ceil based on feel:
                    // floor: 5 -> 2, 1 -> 0 (harsher)
                    // ceil : 5 -> 3, 1 -> 1 (less punishing)
                    int halved = myStacks / 2; // floor
                    attackerBody.ApplyBuff(Buffs.omniboostBuffStacks.buffIndex, halved);
                }

                // Update last victim (including first hit case)
                tracker.lastVictim = victimBody;

                // Build counter on this victim
                int debuff = victimBody.GetBuffCount(Buffs.omniboostDebuffStacks);

                if (debuff + 1 < StaticValues.omniboostNumberOfHits)
                {
                    victimBody.ApplyBuff(Buffs.omniboostDebuffStacks.buffIndex, debuff + 1);
                }
                else
                {
                    victimBody.ApplyBuff(Buffs.omniboostDebuffStacks.buffIndex, 0);

                    int myStacks = attackerBody.GetBuffCount(Buffs.omniboostBuffStacks);
                    attackerBody.ApplyBuff(Buffs.omniboostBuffStacks.buffIndex, myStacks + 1);

                    EffectManager.SpawnEffect(EntityStates.Wisp1Monster.FireEmbers.hitEffectPrefab,
                        new EffectData { origin = victimBody.transform.position, scale = 1f }, false);
                }
            }
        

            // Big Bang stacking → nova
            if (attackerBody.HasBuff(Buffs.bigbangBuff) && positiveDmg && isNonDot && hasProcChance)
            {
                int stacks = victimBody.GetBuffCount(Buffs.bigbangDebuff);
                if (stacks + 1 < StaticValues.bigbangBuffThreshold)
                {
                    victimBody.ApplyBuff(Buffs.bigbangDebuff.buffIndex, stacks + 1);
                }
                else
                {
                    victimBody.ApplyBuff(Buffs.bigbangDebuff.buffIndex, 0);

                    if (EntityStates.VagrantMonster.ExplosionAttack.novaEffectPrefab)
                    {
                        EffectManager.SpawnEffect(EntityStates.VagrantMonster.ExplosionAttack.novaEffectPrefab, new EffectData
                        {
                            origin = victimBody.transform.position,
                            scale = StaticValues.bigbangBuffRadius * attackerBody.attackSpeed / 3f
                        }, true);
                    }

                    new BlastAttack
                    {
                        attacker = attackerBody.gameObject,
                        teamIndex = TeamComponent.GetObjectTeam(attackerBody.gameObject),
                        crit = false,
                        falloffModel = BlastAttack.FalloffModel.None,
                        baseDamage = damageInfo.damage * StaticValues.bigbangBuffCoefficient,
                        damageType = new DamageTypeCombo(DamageType.Stun1s, DamageTypeExtended.Generic, DamageSource.Secondary),
                        damageColorIndex = DamageColorIndex.Default,
                        baseForce = 0f,
                        position = victimBody.transform.position,
                        radius = StaticValues.bigbangBuffRadius * attackerBody.attackSpeed / 3f,
                        procCoefficient = 0f,
                        attackerFiltering = AttackerFiltering.NeverHitSelf
                    }.Fire();
                }
            }

            // Wisper (devil orb)
            if (attackerBody.HasBuff(Buffs.wisperBuff) && positiveDmg && isNonDot && hasProcChance)
            {
                DevilOrb orb = new DevilOrb
                {
                    origin = attackerBody.corePosition,
                    damageValue = attackerBody.damage * StaticValues.wisperBuffDamageCoefficient,
                    teamIndex = attackerBody.teamComponent.teamIndex,
                    attacker = attackerBody.gameObject,
                    damageColorIndex = DamageColorIndex.Item,
                    scale = 1f,
                    effectType = DevilOrb.EffectType.Wisp,
                    procCoefficient = 0f
                };

                var hb = victimBody.mainHurtBox;
                if (hb)
                {
                    orb.target = hb;
                    orb.isCrit = Util.CheckRoll(attackerBody.crit, attackerBody.master);
                    OrbManager.instance.AddOrb(orb);
                }
            }

            // Light/Darkness form debuffs & effects…
            if (attackerBody.HasBuff(Buffs.lightFormBuff) && positiveDmg && isNonDot && hasProcChance)
                victimBody.ApplyBuff(Buffs.lightFormDebuff.buffIndex, victimBody.GetBuffCount(Buffs.lightFormDebuff) + 1);

            if (attackerBody.HasBuff(Buffs.darknessFormBuff) && positiveDmg && isNonDot && hasProcChance)
                victimBody.ApplyBuff(Buffs.darknessFormDebuff.buffIndex, victimBody.GetBuffCount(Buffs.darknessFormDebuff) + 1);

            if (victimBody.HasBuff(Buffs.lightFormDebuff) && positiveDmg && isNonDot && hasProcChance)
                new OrbDamageRequest(victimBody.masterObjectId, damageInfo.damage, attackerBody.masterObjectId).Send(NetworkDestination.Clients);

            if (attackerBody.HasBuff(Buffs.lightAndDarknessFormBuff) && positiveDmg && isNonDot && hasProcChance)
                victimBody.ApplyBuff(Buffs.lightAndDarknessFormDebuff.buffIndex, victimBody.GetBuffCount(Buffs.lightAndDarknessFormDebuff) + 1);

            if (victimBody.HasBuff(Buffs.lightAndDarknessFormDebuff) && positiveDmg && isNonDot && hasProcChance)
            {
                int stacks = victimBody.GetBuffCount(Buffs.lightAndDarknessFormDebuff);
                new LightAndDarknessPullRequest(
                    attackerBody.masterObjectId,
                    victimBody.corePosition,
                    Vector3.up,
                    StaticValues.lightAndDarknessRange + StaticValues.lightAndDarknessRangeAddition * stacks,
                    0f,
                    damageInfo.damage * (StaticValues.lightAndDarknessBonusDamage * stacks),
                    360f,
                    true
                ).Send(NetworkDestination.Clients);
            }

            // Limit Break: pay health per hit
            if (attackerBody.HasBuff(Buffs.limitBreakBuff) && positiveDmg && isNonDot)
            {
                new SpendHealthNetworkRequest(attackerBody.masterObjectId,
                    attackerBody.healthComponent.fullHealth * StaticValues.limitBreakHealthCostCoefficient)
                    .Send(NetworkDestination.Clients);
            }



        }
        //keeping track of last victim
        public class OmniboostTracker : MonoBehaviour
        {
            public CharacterBody lastVictim;
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
                    if (damageReport.damageInfo.damage > 0)
                    {
                        int doubleTimeStacksBuffCount = damageReport.attackerBody.GetBuffCount(Buffs.doubleTimeBuffStacks);
                        damageReport.attackerBody.ApplyBuff(Buffs.doubleTimeBuffStacks.buffIndex, doubleTimeStacksBuffCount + 1);
                    }
                }

                //omniboost buff stacks halve on kill
                if (damageReport.attackerBody.HasBuff(Buffs.omniboostBuffStacks))
                {
                    if (damageReport.damageInfo.damage > 0)
                    {
                        int omniBoostBuffStacksBuffCount = damageReport.attackerBody.GetBuffCount(Buffs.omniboostBuffStacks);
                        damageReport.attackerBody.ApplyBuff(Buffs.omniboostBuffStacks.buffIndex, Mathf.RoundToInt(omniBoostBuffStacksBuffCount/2));
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
                    CustomEmotesAPI.ImportArmature(item.bodyPrefab, Modules.ShiggyAsset.mainAssetBundle.LoadAsset<GameObject>("humanoidShigaraki"));
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
            // =========================
            // === PRE: before apply ===
            // =========================
            if (NetworkServer.active && self && self.body && damageInfo != null && !damageInfo.rejected)
            {
                var victimBody = self.body;
                var attackerBody = damageInfo.attacker ? damageInfo.attacker.GetComponent<CharacterBody>() : null;

                // (A) Modifiers that CHANGE incoming damage should be PRE
                // death aura (DoT scaling on victim / attacker)
                if (victimBody.HasBuff(Buffs.deathAuraDebuff) && damageInfo.damageType == DamageType.DoT)
                {
                    damageInfo.damage *= (1f + victimBody.GetBuffCount(Buffs.deathAuraDebuff) * StaticValues.deathAuraDebuffCoefficient);
                }
                if (attackerBody && attackerBody.HasBuff(Buffs.deathAuraBuff) && damageInfo.damageType == DamageType.DoT)
                {
                    damageInfo.damage *= (1f + attackerBody.GetBuffCount(Buffs.deathAuraBuff) * StaticValues.deathAuraBuffCoefficient);
                }

                // multiplier (spend energy to amp damage)
                if (attackerBody && attackerBody.HasBuff(Buffs.multiplierBuff) && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                {
                    var energySys = damageInfo.attacker.GetComponent<EnergySystem>();
                    if (energySys)
                    {
                        float flatCost = Mathf.Max(0f, StaticValues.multiplierEnergyCost - energySys.costflatplusChaos);
                        float cost = Mathf.Max(0f, energySys.costmultiplierplusChaos * flatCost);
                        if (energySys.currentplusChaos < cost)
                        {
                            attackerBody.ApplyBuff(Buffs.multiplierBuff.buffIndex, 0);
                            energySys.TriggerGlow(0.3f, 0.3f, Color.black);
                        }
                        else
                        {
                            damageInfo.damage *= StaticValues.multiplierCoefficient;
                            energySys.SpendplusChaos(cost);
                        }
                    }
                }

                // darkness form: amplify non-DoT with procCoefficient > 0
                if (victimBody.HasBuff(Buffs.darknessFormDebuff) && damageInfo.procCoefficient > 0f && damageInfo.damageType != DamageType.DoT)
                {
                    int stacks = victimBody.GetBuffCount(Buffs.darknessFormDebuff);
                    damageInfo.damage += damageInfo.damage * (Modules.StaticValues.darkFormBonusDamage * stacks);
                }

                // expose: consume debuff, add damage, refund CD
                if (attackerBody &&
                    victimBody.HasBuff(RoR2Content.Buffs.MercExpose) &&
                    attackerBody.baseNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
                {
                    victimBody.RemoveBuff(RoR2Content.Buffs.MercExpose);
                    damageInfo.damage += attackerBody.damage * Modules.StaticValues.exposeDamageCoefficient;

                    var skillLocator = attackerBody.skillLocator;
                    if (skillLocator) skillLocator.DeductCooldownFromAllSkillsServer(1f);

                    EffectManager.SimpleImpactEffect(HealthComponent.AssetReferences.mercExposeConsumeEffectPrefab, damageInfo.position, Vector3.up, true);
                }

                // stone titan armor handling (reduces or clamps damage, may heal overflow)
                if (victimBody.HasBuff(Buffs.stonetitanBuff.buffIndex) && damageInfo.damage > 0f && attackerBody != victimBody)
                {
                    damageInfo.force = Vector3.zero;
                    if (self.combinedHealthFraction < 0.5f && (damageInfo.damageType & DamageType.DoT) != DamageType.DoT)
                    {
                        damageInfo.damage -= victimBody.armor;
                        if (damageInfo.damage < 0f)
                        {
                            self.Heal(-damageInfo.damage, default, true);
                            damageInfo.damage = 0f;
                        }
                    }
                    else
                    {
                        damageInfo.damage = Mathf.Max(1f, damageInfo.damage - victimBody.armor);
                    }
                }

                // Gargoyle protection: reduce damage + reflect the reduced portion
                if (attackerBody && victimBody.HasBuff(Buffs.gargoyleProtectionBuff) && damageInfo.damage > 0f && attackerBody != victimBody)
                {
                    float reflect = damageInfo.damage * StaticValues.gargoyleProtectionDamageReductionCoefficient;
                    damageInfo.damage -= reflect;

                    var reflectDI = new DamageInfo
                    {
                        damage = reflect,
                        position = damageInfo.attacker.transform.position,
                        force = Vector3.zero,
                        damageColorIndex = DamageColorIndex.WeakPoint,
                        crit = false,
                        attacker = victimBody.gameObject,
                        damageType = new DamageTypeCombo(DamageType.BypassArmor, DamageTypeExtended.Generic, DamageSource.Secondary),
                        procCoefficient = 0f,
                        procChainMask = default
                    };
                    attackerBody.healthComponent.TakeDamage(reflectDI);

                    // FX (both sides)
                    EffectManager.SpawnEffect(Modules.ShiggyAsset.mushrumSporeImpactPrefab, new EffectData { origin = damageInfo.position }, true);
                    EffectManager.SpawnEffect(Modules.ShiggyAsset.mushrumSporeImpactPrefab, new EffectData { origin = damageInfo.attacker.transform.position }, true);
                }

                // =========================
                // (B) TRUE BLOCKS (set rejected = true) must be PRE
                // =========================

                bool bypassSelf = (damageInfo.damageType & DamageType.BypassArmor) > DamageType.Generic;
                bool validHitForBlocks = !bypassSelf && damageInfo.damage > 0f && attackerBody && attackerBody != victimBody;

                // Reversal: cancel hit, counter, zero force
                if (validHitForBlocks && victimBody.HasBuff(Buffs.reversalBuffStacks))
                {
                    new ForceReversalState(victimBody.masterObjectId, attackerBody.transform.position).Send(NetworkDestination.Server);
                    damageInfo.force = Vector3.zero;
                    damageInfo.rejected = true;

                    // Optional: Blind Senses synergy on reversal (counter lightning)
                    if (victimBody.HasBuff(Buffs.blindSensesBuff.buffIndex))
                    {
                        var orb = new LightningOrb
                        {
                            attacker = victimBody.gameObject,
                            bouncesRemaining = 0,
                            damageCoefficientPerBounce = 1f,
                            damageColorIndex = DamageColorIndex.Item,
                            damageValue = damageInfo.damage * StaticValues.blindSensesDamageCoefficient,
                            isCrit = victimBody.RollCrit(),
                            lightningType = LightningOrb.LightningType.RazorWire,
                            origin = victimBody.corePosition,
                            procChainMask = default,
                            procCoefficient = 1f,
                            damageType = new DamageTypeCombo(DamageType.Stun1s, DamageTypeExtended.Generic, DamageSource.Secondary),
                            range = 0f,
                            teamIndex = victimBody.teamComponent.teamIndex,
                            target = attackerBody.mainHurtBox
                        };
                        OrbManager.instance.AddOrb(orb);

                        EffectManager.SpawnEffect(HealthComponent.AssetReferences.bearEffectPrefab, new EffectData
                        {
                            origin = damageInfo.position,
                            rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                        }, true);
                    }

                }

                // Blind Senses: % to block and counter
                if (validHitForBlocks && victimBody.HasBuff(Buffs.blindSensesBuff.buffIndex))
                {
                    float bearBonus = Util.ConvertAmplificationPercentageIntoReductionPercentage(
                        StaticValues.blindSensesBlockChance * (float)victimBody.inventory.GetItemCountEffective(RoR2Content.Items.Bear));
                    if (Util.CheckRoll(StaticValues.blindSensesBlockChance + bearBonus, victimBody.master))
                    {
                        var orb = new LightningOrb
                        {
                            attacker = victimBody.gameObject,
                            bouncesRemaining = 0,
                            damageCoefficientPerBounce = 1f,
                            damageColorIndex = DamageColorIndex.Item,
                            damageValue = damageInfo.damage * StaticValues.blindSensesDamageCoefficient,
                            isCrit = victimBody.RollCrit(),
                            lightningType = LightningOrb.LightningType.RazorWire,
                            origin = victimBody.corePosition,
                            procChainMask = default,
                            procCoefficient = 1f,
                            damageType = new DamageTypeCombo(DamageType.Stun1s, DamageTypeExtended.Generic, DamageSource.Secondary),
                            range = 0f,
                            teamIndex = victimBody.teamComponent.teamIndex,
                            target = attackerBody.mainHurtBox
                        };
                        orb.procChainMask.AddProc(ProcType.Thorns);
                        OrbManager.instance.AddOrb(orb);

                        damageInfo.rejected = true;

                        EffectManager.SpawnEffect(HealthComponent.AssetReferences.bearEffectPrefab, new EffectData
                        {
                            origin = damageInfo.position,
                            rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                        }, true);

                    }
                }

                // Stone Form (still): chance to block completely (zero force)
                if (validHitForBlocks && victimBody.HasBuff(Buffs.stoneFormStillBuff.buffIndex))
                {
                    damageInfo.force = Vector3.zero;
                    if (Util.CheckRoll(StaticValues.stoneFormBlockChance, victimBody.master))
                    {
                        // If also has Blind Senses, fire counter orb
                        if (victimBody.HasBuff(Buffs.blindSensesBuff.buffIndex))
                        {
                            var orb = new LightningOrb
                            {
                                attacker = victimBody.gameObject,
                                bouncesRemaining = 0,
                                damageCoefficientPerBounce = 1f,
                                damageColorIndex = DamageColorIndex.Item,
                                damageValue = damageInfo.damage * StaticValues.blindSensesDamageCoefficient,
                                isCrit = victimBody.RollCrit(),
                                lightningType = LightningOrb.LightningType.RazorWire,
                                origin = victimBody.corePosition,
                                procChainMask = default,
                                procCoefficient = 1f,
                                damageType = new DamageTypeCombo(DamageType.Stun1s, DamageTypeExtended.Generic, DamageSource.Secondary),
                                range = 0f,
                                teamIndex = victimBody.teamComponent.teamIndex,
                                target = attackerBody.mainHurtBox
                            };
                            OrbManager.instance.AddOrb(orb);
                        }

                        damageInfo.rejected = true;

                        EffectManager.SpawnEffect(HealthComponent.AssetReferences.bearEffectPrefab, new EffectData
                        {
                            origin = damageInfo.position,
                            rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                        }, true);

                    }
                }

                // Alpha Construct shield: one-time full block + swap buffs
                if (validHitForBlocks && victimBody.HasBuff(Buffs.alphashieldonBuff.buffIndex))
                {
                    EffectManager.SpawnEffect(HealthComponent.AssetReferences.bearVoidEffectPrefab, new EffectData
                    {
                        origin = damageInfo.position,
                        rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                    }, true);

                    damageInfo.rejected = true;
                    victimBody.ApplyBuff(Buffs.alphashieldonBuff.buffIndex, 0);
                    victimBody.ApplyBuff(Buffs.alphashieldoffBuff.buffIndex, StaticValues.alphaconstructCooldown);

                }

                if(validHitForBlocks && victimBody.HasBuff(Buffs.childBuff.buffIndex) && damageInfo.damage > self.combinedHealth && !victimBody.HasBuff(Buffs.childCDDebuff))
                {
                    damageInfo.rejected = true;

                    Vector3 origin = victimBody.corePosition;
                    Vector3 target = origin;
                    bool found = false;

                    // Find ground nodes in range
                    NodeGraph graph = SceneInfo.instance.GetNodeGraph(MapNodeGroup.GraphType.Ground);
                    if (graph != null)
                    {
                        var nodes = graph.FindNodesInRange(origin, StaticValues.childEscapeMinRange, StaticValues.childEscapeMaxRange, HullMask.Human);
                        if (nodes != null && nodes.Count > 0)
                        {
                            var node = nodes[UnityEngine.Random.Range(0, nodes.Count)];
                            if (graph.GetNodePosition(node, out var pos))
                            {
                                target = pos;
                                found = true;
                            }
                        }
                    }

                    // Fallback if no node found: small random offset
                    if (!found)
                    {
                        Vector3 offset = UnityEngine.Random.insideUnitSphere * StaticValues.childEscapeMinRange * 0.5f;
                        offset.y = 0f;
                        target = origin + offset;
                    }

                    // Teleport the body
                    TeleportHelper.TeleportBody(victimBody, target, true);

                    // --- Effects ---

                    // Exit FX at old position
                    var fxPrefab = EntityStates.ChildMonster.FrolicAway.tpEffectPrefab;
                    if (fxPrefab)
                    {
                        EffectManager.SpawnEffect(fxPrefab, new EffectData { origin = origin, scale = 1f }, true);
                        EffectManager.SpawnEffect(fxPrefab, new EffectData { origin = target, scale = 1f }, true);
                    }

                    // Sound (networked so all clients hear it)
                    //EffectManager.SimpleSoundEffect("Play_child_attack2_reappear", target, true);

                    Util.PlaySound("Play_child_attack2_reappear", victimBody.gameObject);


                    victimBody.ApplyBuff(Buffs.childCDDebuff.buffIndex, 1, StaticValues.childTeleportCD);
                    victimBody.ApplyBuff(RoR2Content.Buffs.HiddenInvincibility.buffIndex, 1, StaticValues.childEscapePostInvuln);


                }

            }

            // =========================
            // Apply base game damage
            // =========================
            orig(self, damageInfo);

            // =========================
            // === POST: after apply ===
            // =========================
            if (self && self.body && damageInfo?.attacker)
            {
                var victimBody = self.body;
                var attackerBody = damageInfo.attacker.GetComponent<CharacterBody>();
                if (attackerBody && victimBody)
                {
                    bool isNonDot = (damageInfo.damageType & DamageType.DoT) != DamageType.DoT;
                    bool hasProc = damageInfo.procCoefficient > 0f;
                    bool positiveDmg = damageInfo.damage > 0f;
                    bool bypassSelf = (damageInfo.damageType & DamageType.BypassArmor) > DamageType.Generic;


                    //apply primed debuff if you have the primed buff
                    if (attackerBody.HasBuff(Buffs.solusPrimedBuff))
                    {
                        int stacks = victimBody.GetBuffCount(Buffs.solusPrimedDebuff);
                        victimBody.ApplyBuff(Buffs.solusPrimedDebuff.buffIndex, stacks + 1);
                    }
                    // Prospector: any hit applies Primed
                    if (attackerBody.HasBuff(Buffs.solusPrimedBuff) && positiveDmg && isNonDot && hasProc)
                    {
                        // Optional: ignore self hits
                        if (attackerBody != victimBody)
                        {
                            int stacks = victimBody.GetBuffCount(Buffs.solusPrimedDebuff);
                            victimBody.ApplyBuff(Buffs.solusPrimedDebuff.buffIndex, stacks + 1);
                        }
                    }


                    // supernova stacks (self-damage tracker)
                    if (victimBody.HasBuff(Buffs.supernovaBuff.buffIndex))
                    {
                        int currentStacks = victimBody.GetBuffCount(Buffs.supernovaBuff.buffIndex) - 1;
                        int damageDealt = Mathf.RoundToInt(damageInfo.damage);
                        int total = damageDealt + currentStacks;

                        if (total < Mathf.RoundToInt(StaticValues.supernovaHealthThreshold * victimBody.healthComponent.fullCombinedHealth))
                        {
                            victimBody.ApplyBuff(Buffs.supernovaBuff.buffIndex, total);
                        }
                        else
                        {
                            victimBody.ApplyBuff(Buffs.supernovaBuff.buffIndex, 1);

                            Vector3 pos = victimBody.transform.position;
                            Util.PlaySound(FireMegaNova.novaSoundString, victimBody.gameObject);
                            EffectManager.SpawnEffect(FireMegaNova.novaEffectPrefab, new EffectData
                            {
                                origin = pos,
                                scale = StaticValues.supernovaRadius,
                                rotation = Quaternion.LookRotation(self.transform.position)
                            }, true);

                            var modelTransform = victimBody.GetComponent<ModelLocator>()?.modelTransform;
                            if (modelTransform)
                            {
                                TemporaryOverlayInstance overlay = TemporaryOverlayManager.AddOverlay(modelTransform.gameObject);
                                overlay.duration = 3f;
                                overlay.animateShaderAlpha = true;
                                overlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                                overlay.destroyComponentOnEnd = true;
                                overlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matVagrantEnergized");
                                overlay.AddToCharacterModel(modelTransform.GetComponent<CharacterModel>());
                            }

                            new BlastAttack
                            {
                                attacker = victimBody.gameObject,
                                baseDamage = victimBody.damage * StaticValues.supernovaDamageCoefficient,
                                baseForce = FireMegaNova.novaForce,
                                attackerFiltering = AttackerFiltering.NeverHitSelf,
                                crit = victimBody.RollCrit(),
                                damageColorIndex = DamageColorIndex.Default,
                                damageType = new DamageTypeCombo(DamageType.Generic, DamageTypeExtended.Generic, DamageSource.Secondary),
                                falloffModel = BlastAttack.FalloffModel.None,
                                inflictor = victimBody.gameObject,
                                position = pos,
                                procChainMask = default,
                                procCoefficient = StaticValues.supernovaProcCoefficient,
                                radius = StaticValues.supernovaRadius,
                                losType = BlastAttack.LoSType.NearestHit,
                                teamIndex = victimBody.teamComponent.teamIndex,
                                impactEffect = EffectCatalog.FindEffectIndexFromPrefab(FireMegaNova.novaImpactEffectPrefab)
                            }.Fire();
                        }
                    }

                    // apply Decay DoT on hit if the attack carried the modded type
                    if (DamageAPI.HasModdedDamageType(damageInfo, Modules.Damage.shiggyDecay))
                    {
                        var info = new InflictDotInfo
                        {
                            attackerObject = damageInfo.attacker,
                            victimObject = self.gameObject,
                            duration = Modules.StaticValues.decayDamageTimer,
                            dotIndex = Modules.Dots.decayDot
                        };
                        DotController.InflictDot(ref info);

                        var controller = self.gameObject.GetComponent<DecayEffectController>();
                        if (!controller)
                        {
                            controller = self.gameObject.AddComponent<DecayEffectController>();
                            controller.attackerBody = attackerBody;
                        }
                    }

                    // jellyfish heal stacks (based on damage taken)
                    if (victimBody.HasBuff(Buffs.JellyfishRegenerateStacksBuff.buffIndex) && !bypassSelf && positiveDmg && attackerBody != victimBody)
                    {
                        int currentStacks = victimBody.GetBuffCount(Buffs.JellyfishRegenerateStacksBuff.buffIndex) - 1;
                        int damageDealt = Mathf.RoundToInt(damageInfo.damage);
                        int total = (damageDealt / 2) + currentStacks;
                        victimBody.ApplyBuff(Buffs.JellyfishRegenerateStacksBuff.buffIndex, total);
                    }

                    // Gup spike blast (reactive; doesn’t alter incoming)
                    if (victimBody.HasBuff(Buffs.gupspikeBuff.buffIndex) && !bypassSelf && positiveDmg && attackerBody != victimBody)
                    {
                        var blast = new BlastAttack
                        {
                            radius = Modules.StaticValues.spikedamageRadius,
                            procCoefficient = 0.5f,
                            position = self.transform.position,
                            attacker = self.gameObject,
                            crit = Util.CheckRoll(victimBody.crit, victimBody.master),
                            baseDamage = victimBody.damage * Modules.StaticValues.spikedamageCoefficient,
                            falloffModel = BlastAttack.FalloffModel.None,
                            baseForce = 100f,
                            teamIndex = TeamComponent.GetObjectTeam(victimBody.gameObject),
                            damageType = new DamageTypeCombo(DamageType.BleedOnHit, DamageTypeExtended.Generic, DamageSource.Secondary),
                            attackerFiltering = AttackerFiltering.NeverHitSelf
                        };
                        DamageAPI.AddModdedDamageType(blast, Damage.shiggyDecay);
                        blast.Fire();

                        EffectManager.SpawnEffect(Modules.ShiggyAsset.GupSpikeEffect, new EffectData
                        {
                            origin = self.transform.position,
                            scale = Modules.StaticValues.spikedamageRadius / 3f,
                            rotation = Quaternion.LookRotation(self.transform.position)
                        }, true);
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

                //greed buff
                if (self.HasBuff(Buffs.halcyoniteGreedStacksBuff))
                {
                    self.damage += StaticValues.halcyoniteGreedBuffDamageCoefficient * self.GetBuffCount(Buffs.halcyoniteGreedStacksBuff);
                    self.attackSpeed += StaticValues.halcyoniteGreedBuffAttackspeedCoefficient * self.GetBuffCount(Buffs.halcyoniteGreedStacksBuff);
                }
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
                            damageInfo.damageType = new DamageTypeCombo(DamageType.WeakPointHit, DamageTypeExtended.Generic, DamageSource.Secondary);
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

        private void CharacterModel_Start(On.RoR2.CharacterModel.orig_Start orig, CharacterModel self)
        {
            orig(self);
            if (self.gameObject.name.Contains("ShiggyDisplay"))
            {
                //randomise starting animation
                Animator animator = self.gameObject.GetComponentInChildren<Animator>();
                if (animator)
                {
                    int randomInt = UnityEngine.Random.RandomRangeInt(1, 3);
                    animator.SetInteger("randomInt", randomInt);

                    Debug.Log("randomInt number " + animator.GetFloat(randomInt));

                }
            }
        }


        private void CharacterModel_Awake(On.RoR2.CharacterModel.orig_Awake orig, CharacterModel self)
        {
            orig(self);
            if (self.gameObject.name.Contains("ShiggyDisplay"))
            {

                //Animator animator = self.gameObject.GetComponentInChildren<Animator>();
                //if (animator)
                //{
                //    int randomInt = UnityEngine.Random.RandomRangeInt(1, 3);
                //    animator.SetInteger("randomInt", randomInt);

                //    Debug.Log("randomInt number " + animator.GetFloat(randomInt));

                //}
                //deku collab
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
                        AkSoundEngine.PostEvent(1896314350, self.gameObject); 
                    }
                }
            }

        }
        private void CharacterModel_UpdateOverlays(On.RoR2.CharacterModel.orig_UpdateOverlays orig, CharacterModel self)
        {
            orig(self);

            if (self)
            {
                if (self.body)
                {
                    this.OverlayFunction(Modules.ShiggyAsset.alphaconstructShieldBuffMat, self.body.HasBuff(Modules.Buffs.alphashieldonBuff), self);
                    this.OverlayFunction(Modules.ShiggyAsset.multiplierShieldBuffMat, self.body.HasBuff(Modules.Buffs.multiplierBuff), self);
                    this.OverlayFunction(Modules.ShiggyAsset.limitBreakBuffMat, self.body.HasBuff(Modules.Buffs.limitBreakBuff), self);
                    this.OverlayFunction(Modules.ShiggyAsset.voidFormBuffMat, self.body.HasBuff(Modules.Buffs.voidFormBuff), self);
                    this.OverlayFunction(Modules.ShiggyAsset.voidFormBuffMat, self.body.HasBuff(Modules.Buffs.decayDebuff), self);
                    this.OverlayFunction(EntityStates.ImpMonster.BlinkState.destealthMaterial, self.body.HasBuff(Modules.Buffs.deathAuraBuff), self);
                    this.OverlayFunction(Modules.ShiggyAsset.deathAuraBuffMat, self.body.HasBuff(Modules.Buffs.deathAuraDebuff), self); 
                    this.OverlayFunction(EntityStates.ImpMonster.BlinkState.destealthMaterial, self.body.HasBuff(Modules.Buffs.darknessFormBuff), self);
                    this.OverlayFunction(Modules.ShiggyAsset.lightFormBuffMat, self.body.HasBuff(Modules.Buffs.lightFormBuff), self);
                    this.OverlayFunction(Modules.ShiggyAsset.lightAndDarknessMat, self.body.HasBuff(Modules.Buffs.lightAndDarknessFormBuff), self);
                    this.OverlayFunction(Modules.ShiggyAsset.blastingZoneBurnMat, self.body.HasBuff(Modules.Buffs.blastingZoneBurnDebuff), self);
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
