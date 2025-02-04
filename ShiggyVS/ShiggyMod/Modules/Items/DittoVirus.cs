﻿//using BepInEx.Configuration;
//using ShiggyMod.Items;
//using ShiggyMod.Modules.Survivors;
//using R2API;
//using RoR2;
//using RoR2.Skills;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.Networking;

//namespace ShiggyMod.Modules.Items
//{
//    public class ShiggyVirus : ItemBase<ShiggyVirus>
//    {

//        public static GameObject ItemBodyModelPrefab;

//        public float procChance { get; private set; } = 1f;
//        public float stackChance { get; private set; } = 100f;
//        public float capChance { get; private set; } = 100f;
//        public override string ItemName => "Shiggy Virus";

//        public override string ItemLangTokenName => "SHIGGY_VIRUS";

//        public override string ItemPickupDesc => $"Chance to transform an enemy into a Shiggy after 5 seconds.";

//        public override string ItemFullDescription => $"<style=cIsUtility>{procChance}% </style>"
//            + $"<style=cStack>(+ {stackChance}% per stack, up to {capChance}%)</style>"
//            + " chance to <style=cWorldEvent>transform</style> an enemy into a Shiggy with "
//            + "<style=cIsUtility>random items equipped</style>"
//            + ".";

//        public override string ItemLore => "The day will come when Shiggy's rule the world. ";

//        public override ItemTag[] ItemTags => new ItemTag[] { ItemTag.Utility };

//        public override ItemTier Tier => ItemTier.Lunar;

//        public override GameObject ItemModel => Modules.Asset.ShiggyItemPrefab;

//        public override Sprite ItemIcon => Modules.Asset.mainAssetBundle.LoadAsset<Sprite>("texShiggyIcon");


//        public static SkillDef avestSkillDef = Shiggy.avestSkillDef;
//        public static SkillDef cbandSkillDef = Shiggy.cbandSkillDef;
//        public static SkillDef cscarfSkillDef = Shiggy.cscarfSkillDef;
//        public static SkillDef cspecSkillDef = Shiggy.cspecSkillDef;
//        public static SkillDef leftSkillDef = Shiggy.leftSkillDef;
//        public static SkillDef loSkillDef = Shiggy.loSkillDef;
//        public static SkillDef luckySkillDef = Shiggy.luckySkillDef;
//        public static SkillDef rhSkillDef = Shiggy.rhSkillDef;
//        public static SkillDef scopeSkillDef = Shiggy.scopeSkillDef;
//        public static SkillDef shellSkillDef = Shiggy.shellSkillDef;


//        public override void Init(ConfigFile config)
//        {
//            CreateLang();
//            CreateItem();
//            Hooks();
//            CreateItemDisplayRules();
//        }
//        public override ItemDisplayRuleDict CreateItemDisplayRules()
//        {
//            ItemDisplayRuleDict rules = new ItemDisplayRuleDict();
//            //no model for becomeShiggy.
//            return rules;
//        }

//        public override void Hooks()
//        {

//            //On.RoR2.HealthComponent.TakeDamage += HealthComponent_TakeDamage;
//            On.RoR2.GlobalEventManager.OnHitEnemy += On_GEMOnHitEnemy;

//        }
//        private void On_GEMOnHitEnemy(On.RoR2.GlobalEventManager.orig_OnHitEnemy orig, GlobalEventManager self, DamageInfo damageInfo, GameObject victim)
//        {
//            orig(self, damageInfo, victim);

//            List<string> speciallist = new List<string>();
//            speciallist.Add("NullifierBody");
//            speciallist.Add("VoidJailerBody");
//            speciallist.Add("MinorConstructBody");
//            speciallist.Add("MinorConstructOnKillBody");
//            speciallist.Add("MiniVoidRaidCrabBodyPhase1");
//            speciallist.Add("MiniVoidRaidCrabBodyPhase2");
//            speciallist.Add("MiniVoidRaidCrabBodyPhase3");
//            speciallist.Add("ElectricWormBody");
//            speciallist.Add("MagmaWormBody");
//            speciallist.Add("BeetleQueen2Body");
//            speciallist.Add("TitanBody");
//            speciallist.Add("TitanGoldBody");
//            speciallist.Add("VagrantBody");
//            speciallist.Add("GravekeeperBody");
//            speciallist.Add("ClayBossBody");
//            speciallist.Add("RoboBallBossBody");
//            speciallist.Add("SuperRoboBallBossBody");
//            speciallist.Add("MegaConstructBody");
//            speciallist.Add("VoidInfestorBody");
//            speciallist.Add("VoidBarnacleBody");
//            speciallist.Add("MegaConstructBody");
//            speciallist.Add("VoidMegaCrabBody");
//            speciallist.Add("GrandParentBody");
//            speciallist.Add("ImpBossBody");
//            speciallist.Add("BrotherBody");
//            speciallist.Add("BrotherHurtBody");
//            speciallist.Add("ShiggyBody");

//            if (!NetworkServer.active || !victim || !damageInfo.attacker || damageInfo.procCoefficient <= 0f || damageInfo.procChainMask.HasProc(ProcType.Missile)) return;

//            var vicb = victim.GetComponent<CharacterBody>();

//            CharacterBody oldBody = vicb;
//            CharacterBody body = damageInfo.attacker.GetComponent<CharacterBody>();
//            if (!body || !vicb || !vicb.healthComponent || !vicb.mainHurtBox) return;

//            CharacterMaster chrm = body.master;
//            if (!chrm) return;

//            int icnt = GetCount(body);
//            if (icnt == 0) return;

//            icnt--;
//            float m2Proc = procChance;
//            if (icnt > 0) m2Proc += stackChance * icnt;
//            if (m2Proc > capChance) m2Proc = capChance;
//            if (!Util.CheckRoll(m2Proc * damageInfo.procCoefficient, chrm))
//            {
//                if (!vicb.isChampion)
//                {
//                    if (speciallist.Contains(vicb.master.bodyPrefab.name))
//                    {
//                        //self.body.ApplyBuff(Modules.Buffs.transformBuff.buffIndex, 1);
//                        vicb.master.TransformBody("ShiggyBody");

//                        body = vicb.master.GetBody();
//                        AkSoundEngine.PostEvent(1531773223, vicb.gameObject);

//                        int rand = UnityEngine.Random.Range(0, 8);
//                        if (rand == 0)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.avestSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }
//                        if (rand == 1)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.cbandSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }
//                        if (rand == 2)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.cscarfSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }
//                        if (rand == 3)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.cspecSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }
//                        if (rand == 4)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.leftSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }
//                        if (rand == 5)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.loSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }
//                        if (rand == 6)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.luckySkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }
//                        if (rand == 7)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.scopeSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }
//                        if (rand == 8)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.shellSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }
//                        int rand2 = UnityEngine.Random.Range(0, 8);
//                        if (rand2 == 0)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.avestSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }
//                        if (rand2 == 1)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.cbandSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }
//                        if (rand2 == 2)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.cscarfSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }
//                        if (rand2 == 3)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.cspecSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }
//                        if (rand2 == 4)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.leftSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }
//                        if (rand2 == 5)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.loSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }
//                        if (rand2 == 6)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.luckySkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }
//                        if (rand2 == 7)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.scopeSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }
//                        if (rand2 == 8)
//                        {
//                            body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.shellSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//                        }

//                        var oldHealth = oldBody.healthComponent.health / oldBody.healthComponent.fullHealth;
//                        body.healthComponent.health = body.healthComponent.fullHealth * oldHealth;
//                        body.healthComponent.health = oldBody.healthComponent.health;
//                        body.baseMaxHealth = oldBody.baseMaxHealth;
//                        body.levelMaxHealth = oldBody.levelMaxHealth;
//                        body.maxHealth = oldBody.maxHealth;
//                        body.baseRegen = oldBody.regen;
//                        body.baseJumpCount = oldBody.baseJumpCount;
//                        body.maxJumpCount = oldBody.maxJumpCount;
//                        body.maxJumpHeight = oldBody.maxJumpHeight;
//                        body.jumpPower = oldBody.jumpPower;
//                        body.baseJumpPower = oldBody.baseJumpPower;


//                    }

//                }

//            }


//        }

//        //private void HealthComponent_TakeDamage(On.RoR2.HealthComponent.orig_TakeDamage orig, HealthComponent self, DamageInfo di)
//        //{


//        //    orig(self, di);


//        //    CharacterBody oldBody = self.body;
//        //    CharacterBody body;

//        //    if (di == null || di.rejected || !di.attacker || di.attacker == self.gameObject) return;

//        //    var cb = di.attacker.GetComponent<CharacterBody>();
//        //    if (cb)
//        //    {
//        //        var icnt = GetCount(cb);
//        //        if (icnt < 1) return;
//        //        var proc = cb.master ? Util.CheckRoll(procChance, cb.master) : Util.CheckRoll(procChance);
//        //        if (proc)
//        //        {
//        //            if (!self.body.isChampion)
//        //            {
//        //                if (self.body.master.bodyPrefab.name != "ShiggyBody")
//        //                {
//        //                    //self.body.ApplyBuff(Modules.Buffs.transformBuff.buffIndex, 1);
//        //                    self.body.master.TransformBody("ShiggyBody");
//        //                    body = self.body;

//        //                    AkSoundEngine.PostEvent(1531773223, self.gameObject);

//        //                    int rand = UnityEngine.Random.Range(0, 8);
//        //                    if (rand == 0)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.avestSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }
//        //                    if (rand == 1)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.cbandSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }
//        //                    if (rand == 2)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.cscarfSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }
//        //                    if (rand == 3)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.cspecSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }
//        //                    if (rand == 4)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.leftSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }
//        //                    if (rand == 5)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.loSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }
//        //                    if (rand == 6)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.luckySkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }
//        //                    if (rand == 7)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.scopeSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }
//        //                    if (rand == 8)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.shellSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }
//        //                    int rand2 = UnityEngine.Random.Range(0, 8);
//        //                    if (rand2 == 0)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.avestSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }
//        //                    if (rand2 == 1)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.cbandSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }
//        //                    if (rand2 == 2)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.cscarfSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }
//        //                    if (rand2 == 3)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.cspecSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }
//        //                    if (rand2 == 4)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.leftSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }
//        //                    if (rand2 == 5)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.loSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }
//        //                    if (rand2 == 6)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.luckySkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }
//        //                    if (rand2 == 7)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.scopeSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }
//        //                    if (rand2 == 8)
//        //                    {
//        //                        body.skillLocator.secondary.SetSkillOverride(body.skillLocator.secondary, Shiggy.shellSkillDef, GenericSkill.SkillOverridePriority.Contextual);
//        //                    }

//        //                    body = self.body;

//        //                    var oldHealth = body.healthComponent.health / body.healthComponent.fullHealth;
//        //                    body.healthComponent.health = body.healthComponent.fullHealth * oldHealth;
//        //                    body.healthComponent.health = oldBody.healthComponent.health;
//        //                    body.baseMaxHealth = oldBody.baseMaxHealth;
//        //                    body.levelMaxHealth = oldBody.levelMaxHealth;
//        //                    body.maxHealth = oldBody.maxHealth;
//        //                    body.baseRegen = oldBody.regen;
//        //                    body.baseJumpCount = oldBody.baseJumpCount;
//        //                    body.maxJumpCount = oldBody.maxJumpCount;
//        //                    body.maxJumpHeight = oldBody.maxJumpHeight;
//        //                    body.jumpPower = oldBody.jumpPower;
//        //                    body.baseJumpPower = oldBody.baseJumpPower;


//        //                }

//        //            }
//        //        }
//        //    }
//        //}
//    }
//}