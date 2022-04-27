﻿using BepInEx;
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

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[module: UnverifiableCode]

namespace ShiggyMod
{
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.KingEnderBrine.ExtraSkillSlots", BepInDependency.DependencyFlags.HardDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID, MODNAME, MODVERSION)]
    [R2APISubmoduleDependency(new string[]
    {
        "PrefabAPI",
        "LanguageAPI",
        "SoundAPI",
        "DotAPI"
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
        public const string MODVERSION = "0.0.1";

        // a prefix for name tokens to prevent conflicts- please capitalize all name tokens for convention
        public const string developerPrefix = "TEAL";

        internal List<SurvivorBase> Survivors = new List<SurvivorBase>();

        private GameObject effectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/LightningStakeNova");
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
            Modules.States.RegisterStates(); // register states for networking
            Modules.Buffs.RegisterBuffs(); // add and register custom buffs/debuffs
            Modules.Dots.RegisterDots(); // add and register custom dots
            Modules.Projectiles.RegisterProjectiles(); // add and register custom projectiles
            Modules.Tokens.AddTokens(); // register name tokens
            Modules.ItemDisplays.PopulateDisplays(); // collect item display prefabs for use in our display rules

            // survivor initialization
            new Shiggy().Initialize();



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
        }

        //lifesteal
        private void GlobalEventManager_OnDamageDealt(DamageReport report)
        {

            bool flag = !report.attacker || !report.attackerBody;
            if (!flag && report.attackerBody.HasBuff(Modules.Buffs.shellbellBuff))
            {
                int buffnumber = report.attackerBody.GetBuffCount(Modules.Buffs.shellbellBuff);
                if (buffnumber > 0)
                {
                    if (buffnumber >= 1 && buffnumber < 2)
                    {
                        CharacterBody attackerBody = report.attackerBody;
                        attackerBody.healthComponent.Heal(report.damageDealt * Modules.StaticValues.shellbelllifesteal, default(ProcChainMask), true);
                    }
                    if (buffnumber >= 2)
                    {
                        CharacterBody attackerBody = report.attackerBody;
                        attackerBody.healthComponent.Heal(report.damageDealt * Modules.StaticValues.shellbelllifesteal2, default(ProcChainMask), true);
                    }
                }

            }
        }

        private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo damageInfo)
        {
            //alpha construct shield
            bool flag = (damageInfo.damageType & DamageType.BypassArmor) > DamageType.Generic;
            if (!flag && self.body.HasBuff(Modules.Buffs.alphashieldonBuff.buffIndex) && damageInfo.damage > 0f)
            {
                EffectData effectData2 = new EffectData
                {
                    origin = damageInfo.position,
                    rotation = Util.QuaternionSafeLookRotation((damageInfo.force != Vector3.zero) ? damageInfo.force : UnityEngine.Random.onUnitSphere)
                };
                EffectManager.SpawnEffect(HealthComponent.AssetReferences.bearVoidEffectPrefab, effectData2, true);
                damageInfo.rejected = true;
                self.body.RemoveBuff(Modules.Buffs.alphashieldonBuff.buffIndex);
                self.body.SetBuffCount(Modules.Buffs.alphashieldoffBuff.buffIndex, 10);
                
            }

            
            orig.Invoke(self, damageInfo);
        }

        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            //buffs 
            orig.Invoke(self);

            if (self.HasBuff(Buffs.flyBuff))
            {
                self.moveSpeed *= 1.5f;
                self.acceleration *= 2f;
            }
            if (self.HasBuff(Buffs.beetleBuff))
            {
                self.damage *= 1.2f;
            }
            if (self.HasBuff(Buffs.decayDebuff))
            {
                float decaybuffcount = self.GetBuffCount(Buffs.decayDebuff);
                self.attackSpeed *= Mathf.Pow(0.98f, decaybuffcount);
                self.moveSpeed *= Mathf.Pow(0.98f, decaybuffcount);
                if(decaybuffcount >= Modules.StaticValues.decayInstaKillThreshold)
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
            }

        }

        private void CharacterBody_OnDeathStart(On.RoR2.CharacterBody.orig_OnDeathStart orig, CharacterBody self)
        {
            orig(self);
            if (self.baseNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_NAME")
            {
                AkSoundEngine.PostEvent(3468082827, this.gameObject);
            }

        }

        private void CharacterModel_Awake(On.RoR2.CharacterModel.orig_Awake orig, CharacterModel self)
        {
            orig(self);
            if (self.gameObject.name.Contains("ShiggyDisplay"))
            {
                AkSoundEngine.PostEvent(3468082827, self.gameObject);

            }

        }
        private void CharacterModel_UpdateOverlays(On.RoR2.CharacterModel.orig_UpdateOverlays orig, CharacterModel self)
        {
            orig(self);

            if (self)
            {
                if (self.body)
                {
                    this.OverlayFunction(Modules.Assets.alphaconstructShieldBuff, self.body.HasBuff(Modules.Buffs.alphashieldonBuff), self);
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
