using ShiggyMod.Modules.Survivors;
using EntityStates;
using RoR2;
using UnityEngine;
using System.Collections.Generic;
using ShiggyMod.Modules;
using UnityEngine.Networking;
using RoR2.ExpansionManagement;
using ExtraSkillSlots;
using ShiggyMod.Modules.Networking;
using R2API.Networking;
using R2API.Networking.Interfaces;

namespace ShiggyMod.SkillStates
{
    public class AFO : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";

        private float duration = 1.2f;
        private float fireTime = 0.2f;
        private bool hasFired;
        private bool hasQuirk;
        private ExtraInputBankTest extrainputBankTest;
        private ExtraSkillLocator extraskillLocator;
        public ShiggyController Shiggycon;
        public ShiggyMasterController Shiggymastercon;
        public HurtBox Target;


        public override void OnEnter()
        {
            base.OnEnter();
            //if (NetworkServer.active)
            //{
            //    characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
            //    characterBody.AddBuff(Modules.Buffs.beetleBuff);
            //    characterBody.AddBuff(Modules.Buffs.minimushrumBuff);
            //    characterBody.AddBuff(Modules.Buffs.larvajumpBuff);
            //    characterBody.AddBuff(Modules.Buffs.pestjumpBuff);
            //    characterBody.AddBuff(Modules.Buffs.vagrantBuff);
            //    characterBody.AddBuff(Modules.Buffs.magmawormBuff);
            //    characterBody.AddBuff(Modules.Buffs.gupspikeBuff);
            //    characterBody.AddBuff(Modules.Buffs.hermitcrabmortarBuff);
            //    characterBody.AddBuff(Modules.Buffs.impbossBuff);
            //    characterBody.AddBuff(Modules.Buffs.lesserwispBuff);
            //    characterBody.AddBuff(Modules.Buffs.lunarexploderBuff);
            //    characterBody.AddBuff(Modules.Buffs.roboballminiBuff);
            //    characterBody.AddBuff(Modules.Buffs.stonetitanBuff);
            //    characterBody.AddBuff(Modules.Buffs.verminsprintBuff);
            //    characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarBuff);
            //    characterBody.AddBuff(Modules.Buffs.voidjailerBuff);
            //}

            CheckQuirks();
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("RightArm, Override", "RightArmPull","Attack.playbackRate", duration, 0.1f);


            extraskillLocator = base.GetComponent<ExtraSkillLocator>();

            extrainputBankTest = outer.GetComponent<ExtraInputBankTest>();


            Shiggycon = base.GetComponent<ShiggyController>();
            Shiggymastercon = characterBody.master.gameObject.GetComponent<ShiggyMasterController>();
            if (Shiggycon && base.isAuthority)
            {
                Target = Shiggycon.GetTrackingTarget();
            }

            if (!Target)
            {
                return;
            }
            hasFired = false;
            hasQuirk = false;
        }

        public override void OnExit()
        {
            base.OnExit();
            CheckQuirks();
        }



        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (Config.holdButtonAFO.Value)
            {
                if (base.fixedAge >= this.fireTime && !this.hasFired)
                {
                    hasFired = true;
                    if (Target)
                    {

                        //Debug.Log("Target");
                        //Debug.Log(BodyCatalog.FindBodyPrefab(BodyCatalog.GetBodyName(Target.healthComponent.body.bodyIndex)));
                        //AkSoundEngine.PostEvent(1719197672, this.gameObject);
                        StealQuirk(Target);


                        this.outer.SetNextStateToMain();
                        return;
                    }
                }
                if (base.IsKeyDownAuthority())
                {
                    if (base.fixedAge >= 3f)
                    {
                        if (extrainputBankTest.extraSkill1.down)
                        {
                            Chat.AddMessage("<style=cIsUtility>Quirks</style> Removed");
                            RemovePrimary();
                            RemoveExtra1();
                        }
                        if (extrainputBankTest.extraSkill2.down)
                        {
                            Chat.AddMessage("<style=cIsUtility>Quirks</style> Removed");
                            RemoveSecondary();
                            RemoveExtra2();
                        }
                        if (extrainputBankTest.extraSkill3.down)
                        {
                            Chat.AddMessage("<style=cIsUtility>Quirks</style> Removed");
                            RemoveUtility();
                            RemoveExtra3();
                        }
                        if (extrainputBankTest.extraSkill4.down)
                        {
                            Chat.AddMessage("<style=cIsUtility>Quirks</style> Removed");
                            RemoveSpecial();
                            RemoveExtra4();
                        }
                        CheckQuirks();
                        this.outer.SetNextStateToMain();
                        return;
                    }

                }
                else if (base.fixedAge >= duration && base.isAuthority)
                {
                    this.outer.SetNextStateToMain();
                    return;
                }

            }
            else if (!Config.holdButtonAFO.Value)
            {
                if (base.IsKeyDownAuthority())
                {
                    if (base.fixedAge >= 1f && !this.hasFired)
                    {
                        hasFired = true;
                        if (Target)
                        {

                            Debug.Log("Target");
                            Debug.Log(BodyCatalog.FindBodyPrefab(BodyCatalog.GetBodyName(Target.healthComponent.body.bodyIndex)));
                            //AkSoundEngine.PostEvent(1719197672, this.gameObject);
                            StealQuirk(Target);


                            this.outer.SetNextStateToMain();
                            return;
                        }
                    }
                    if(base.fixedAge >= 3f)
                    {
                        if (extrainputBankTest.extraSkill1.down)
                        {
                            Chat.AddMessage("<style=cIsUtility>Quirks</style> Removed");
                            RemovePrimary();
                            RemoveExtra1();
                            Shiggymastercon.writeToSkillList(null, 0);
                            Shiggymastercon.writeToSkillList(null, 4);
                        }
                        if (extrainputBankTest.extraSkill2.down)
                        {
                            Chat.AddMessage("<style=cIsUtility>Quirks</style> Removed");
                            RemoveSecondary();
                            RemoveExtra2();
                            Shiggymastercon.writeToSkillList(null, 1);
                            Shiggymastercon.writeToSkillList(null, 5);
                        }
                        if (extrainputBankTest.extraSkill3.down)
                        {
                            Chat.AddMessage("<style=cIsUtility>Quirks</style> Removed");
                            RemoveUtility();
                            RemoveExtra3();
                            Shiggymastercon.writeToSkillList(null, 2);
                            Shiggymastercon.writeToSkillList(null, 6);
                        }
                        if (extrainputBankTest.extraSkill4.down)
                        {
                            Chat.AddMessage("<style=cIsUtility>Quirks</style> Removed");
                            RemoveSpecial();
                            RemoveExtra4();
                            Shiggymastercon.writeToSkillList(null, 3);
                            Shiggymastercon.writeToSkillList(null, 7);
                        }
                        CheckQuirks();
                        this.outer.SetNextStateToMain();
                        return;
                    }
                }
                else
                {
                    if (base.fixedAge >= this.duration && base.isAuthority)
                    {
                        this.outer.SetNextStateToMain();
                        return;
                    }
                }

            }


        }

        public void dropEquipment(EquipmentDef def)
        {
            if (base.isAuthority)
            {
                new EquipmentDropNetworked(PickupCatalog.FindPickupIndex(def.equipmentIndex),
                    base.transform.position + Vector3.up * 1.5f,
                    Vector3.up * 20f + base.transform.forward * 2f).Send(NetworkDestination.Clients);
            }
            //PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(def.equipmentIndex), base.transform.position + Vector3.up * 1.5f, Vector3.up * 20f + base.transform.forward * 2f);
        }

        private void StealQuirk(HurtBox hurtBox)
        {
            var name = BodyCatalog.GetBodyName(hurtBox.healthComponent.body.bodyIndex);
            GameObject newbodyPrefab = BodyCatalog.FindBodyPrefab(name);

            Shiggymastercon = characterBody.master.gameObject.GetComponent<ShiggyMasterController>();


            if (extrainputBankTest.extraSkill1.down)
            {
                AkSoundEngine.PostEvent(3192656820, characterBody.gameObject);
                
                if (hurtBox.healthComponent.body.isElite)
                {
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixBlue))
                    {
                        Chat.AddMessage("Stole Overloading <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Lightning.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixHaunted))
                    {
                        Chat.AddMessage("Stole Celestine <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Haunted.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixLunar))
                    {
                        Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Lunar.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixPoison))
                    {
                        Chat.AddMessage("Stole Malachite <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Poison.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixRed))
                    {
                        Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Fire.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixWhite))
                    {
                        Chat.AddMessage("Stole Glacial <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Ice.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteEarth))
                    {
                        Chat.AddMessage("Stole Mending <style=cIsUtility>Quirk!</style>");
                        dropEquipment(DLC1Content.Elites.Earth.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteVoid))
                    {
                        Chat.AddMessage("Stole Void <style=cIsUtility>Quirk!</style>");
                        dropEquipment(DLC1Content.Elites.Void.eliteEquipmentDef);
                    }
                }

                if (newbodyPrefab.name == "VultureBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

                    characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
                }
                if (newbodyPrefab.name == "BeetleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.beetleBuff);
                }
                if (newbodyPrefab.name == "BeetleGuardBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BisonBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.bisonchargeDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "FlyingVerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.pestjumpBuff);
                }
                if (newbodyPrefab.name == "VerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.verminsprintBuff);
                }
                if (newbodyPrefab.name == "BellBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayGrenadierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayBruiserBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LemurianBruiserBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fire Blast Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.elderlemurianfireblastDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.elderlemurianfireblastDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.gupspikeBuff);
                }
                if (newbodyPrefab.name == "GreaterWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spirit Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "HermitCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.hermitcrabmortarBuff);
                }
                if (newbodyPrefab.name == "ImpBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "JellyfishBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.jellyfishnovaDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "AcidLarvaBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.larvajumpBuff);
                }
                if (newbodyPrefab.name == "LemurianBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "WispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.lesserwispBuff);
                }
                if (newbodyPrefab.name == "LunarExploderBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.lunarexploderBuff);
                }
                if (newbodyPrefab.name == "LunarGolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MiniMushroomBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.minimushrumBuff);
                }
                if (newbodyPrefab.name == "ParentBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Solus Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.roboballminiBuff);
                }
                if (newbodyPrefab.name == "GolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidBarnacleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarBuff);
                }
                if (newbodyPrefab.name == "VoidJailerBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 4);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.voidjailerBuff);
                }
                if (newbodyPrefab.name == "NullifierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
                }


                if (newbodyPrefab.name == "BeetleQueen2Body")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ImpBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Bleed Quirk</style> Get!");

                    characterBody.AddBuff(Modules.Buffs.impbossBuff);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    Shiggymastercon.writeToSkillList(Shiggy.impbosspassiveDef, 4);
                }
                if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.stonetitanBuff);
                }
                if (newbodyPrefab.name == "GrandParentBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Solar Flare Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.grandparentsunDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GravekeeperBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VagrantBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.vagrantBuff);
                }
                if (newbodyPrefab.name == "MagmaWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.magmawormBuff);
                }
                if (newbodyPrefab.name == "ElectricWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.overloadingwormBuff);
                }
                if (newbodyPrefab.name == "ClayBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Tar Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MegaConstructBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidMegaCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ScavBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
                }

                if (newbodyPrefab.name == "Bandit2Body")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lights Out Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.banditlightsoutDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.banditlightsoutDef, GenericSkill.SkillOverridePriority.Contextual);
                }

                if (newbodyPrefab.name == "CaptainBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Defensive Microbots Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.captainpassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.captainpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "CommandoBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Double Tap Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.commandopassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.commandopassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "CrocoBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Poison Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.acridpassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.acridpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "EngiBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Turret Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.engiturretDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.engiturretDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                //if (newbodyPrefab.name == "HereticBody")
                //{

                //    hasQuirk = true;
                //    Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

                //    Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 0);
                //    RemovePrimary();
                //    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
                //}
                if (newbodyPrefab.name == "HuntressBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Flurry Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.huntressattackDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.huntressattackDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LoaderBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Scrap Barrier Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.loaderpassiveDef, 4);
                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.loaderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MageBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Elementality Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.artificerflamethrowerDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MercBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Eviscerate Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.mercdashDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.mercdashDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ToolbotBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Power Stance Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.multbuffDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "TreebotBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Seed Barrage Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.rexmortarDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.rexmortarDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RailgunnerBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Cryocharged Railgun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.railgunnercryoDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.railgunnercryoDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidSurvivorBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Cleanse Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voidfiendcleanseDef, 0);
                    RemovePrimary();
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.voidfiendcleanseDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (hasQuirk = false)
                {
                    //Shiggymastercon.transformed = false;
                    Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
                }

            }

            if (extrainputBankTest.extraSkill2.down)
            {
                AkSoundEngine.PostEvent(3192656820, characterBody.gameObject);
                if (hurtBox.healthComponent.body.isElite)
                {
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixBlue))
                    {
                        Chat.AddMessage("Stole Overloading <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Lightning.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixHaunted))
                    {
                        Chat.AddMessage("Stole Celestine <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Haunted.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixLunar))
                    {
                        Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Lunar.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixPoison))
                    {
                        Chat.AddMessage("Stole Malachite <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Poison.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixRed))
                    {
                        Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Fire.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixWhite))
                    {
                        Chat.AddMessage("Stole Glacial <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Ice.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteEarth))
                    {
                        Chat.AddMessage("Stole Mending <style=cIsUtility>Quirk!</style>");
                        dropEquipment(DLC1Content.Elites.Earth.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteVoid))
                    {
                        Chat.AddMessage("Stole Void <style=cIsUtility>Quirk!</style>");
                        dropEquipment(DLC1Content.Elites.Void.eliteEquipmentDef);
                    }
                }
                if (newbodyPrefab.name == "VultureBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
                }
                if (newbodyPrefab.name == "BeetleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.beetleBuff);
                }
                if (newbodyPrefab.name == "BeetleGuardBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BisonBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.bisonchargeDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "FlyingVerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.pestjumpBuff);
                }
                if (newbodyPrefab.name == "VerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.verminsprintBuff);
                }
                if (newbodyPrefab.name == "BellBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayGrenadierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayBruiserBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LemurianBruiserBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fire Blast Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.elderlemurianfireblastDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.elderlemurianfireblastDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.gupspikeBuff);
                }
                if (newbodyPrefab.name == "GreaterWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spirit Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "HermitCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.hermitcrabmortarBuff);
                }
                if (newbodyPrefab.name == "ImpBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "JellyfishBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.jellyfishnovaDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "AcidLarvaBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.larvajumpBuff);
                }
                if (newbodyPrefab.name == "LemurianBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "WispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.lesserwispBuff);
                }
                if (newbodyPrefab.name == "LunarExploderBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.lunarexploderBuff);
                }
                if (newbodyPrefab.name == "LunarGolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MiniMushroomBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.minimushrumBuff);
                }
                if (newbodyPrefab.name == "ParentBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Solus Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.roboballminiBuff);
                }
                if (newbodyPrefab.name == "GolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidBarnacleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarBuff);
                }
                if (newbodyPrefab.name == "VoidJailerBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 5);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.voidjailerBuff);
                }
                if (newbodyPrefab.name == "NullifierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
                }


                if (newbodyPrefab.name == "BeetleQueen2Body")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ImpBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Bleed Quirk</style> Get!");

                    characterBody.AddBuff(Modules.Buffs.impbossBuff);
                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    Shiggymastercon.writeToSkillList(Shiggy.impbosspassiveDef, 5);
                }
                if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.stonetitanBuff);
                }
                if (newbodyPrefab.name == "GrandParentBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Solar Flare Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.grandparentsunDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GravekeeperBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VagrantBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.vagrantBuff);
                }
                if (newbodyPrefab.name == "MagmaWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.magmawormBuff);
                }
                if (newbodyPrefab.name == "ElectricWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.overloadingwormBuff);
                }
                if (newbodyPrefab.name == "ClayBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Tar Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MegaConstructBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidMegaCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ScavBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
                }

                if (newbodyPrefab.name == "Bandit2Body")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lights Out Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.banditlightsoutDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.banditlightsoutDef, GenericSkill.SkillOverridePriority.Contextual);
                }

                if (newbodyPrefab.name == "CaptainBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Defensive Microbots Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.captainpassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.captainpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "CommandoBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Double Tap Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.commandopassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.commandopassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "CrocoBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Poison Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.acridpassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.acridpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "EngiBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Turret Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.engiturretDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.engiturretDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                //if (newbodyPrefab.name == "HereticBody")
                //{

                //    hasQuirk = true;
                //    Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

                //    Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 1);
                //    RemoveSecondary();
                //    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
                //}
                if (newbodyPrefab.name == "HuntressBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Flurry Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.huntressattackDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.huntressattackDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LoaderBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Scrap Barrier Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.loaderpassiveDef, 5);
                    RemoveExtra2();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.loaderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MageBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Elementality Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.artificerflamethrowerDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MercBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Eviscerate Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.mercdashDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.mercdashDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ToolbotBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Power Stance Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.multbuffDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "TreebotBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Seed Barrage Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.rexmortarDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.rexmortarDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RailgunnerBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Cryocharged Railgun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.railgunnercryoDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.railgunnercryoDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidSurvivorBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Cleanse Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voidfiendcleanseDef, 1);
                    RemoveSecondary();
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voidfiendcleanseDef, GenericSkill.SkillOverridePriority.Contextual);
                }

                if (hasQuirk = false)
                {
                    //Shiggymastercon.transformed = false;
                    Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
                }

            }
            if (extrainputBankTest.extraSkill3.down)
            {
                AkSoundEngine.PostEvent(3192656820, characterBody.gameObject);
                if (hurtBox.healthComponent.body.isElite)
                {
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixBlue))
                    {
                        Chat.AddMessage("Stole Overloading <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Lightning.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixHaunted))
                    {
                        Chat.AddMessage("Stole Celestine <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Haunted.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixLunar))
                    {
                        Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Lunar.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixPoison))
                    {
                        Chat.AddMessage("Stole Malachite <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Poison.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixRed))
                    {
                        Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Fire.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixWhite))
                    {
                        Chat.AddMessage("Stole Glacial <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Ice.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteEarth))
                    {
                        Chat.AddMessage("Stole Mending <style=cIsUtility>Quirk!</style>");
                        dropEquipment(DLC1Content.Elites.Earth.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteVoid))
                    {
                        Chat.AddMessage("Stole Void <style=cIsUtility>Quirk!</style>");
                        dropEquipment(DLC1Content.Elites.Void.eliteEquipmentDef);
                    }
                }
                if (newbodyPrefab.name == "VultureBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
                }
                if (newbodyPrefab.name == "BeetleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.beetleBuff);
                }
                if (newbodyPrefab.name == "BeetleGuardBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BisonBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.bisonchargeDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "FlyingVerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.pestjumpBuff);
                }
                if (newbodyPrefab.name == "VerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.verminsprintBuff);
                }
                if (newbodyPrefab.name == "BellBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayGrenadierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayBruiserBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LemurianBruiserBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fire Blast Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.elderlemurianfireblastDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.elderlemurianfireblastDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.gupspikeBuff);
                }
                if (newbodyPrefab.name == "GreaterWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spirit Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "HermitCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.hermitcrabmortarBuff);
                }
                if (newbodyPrefab.name == "ImpBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "JellyfishBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.jellyfishnovaDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "AcidLarvaBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.larvajumpBuff);
                }
                if (newbodyPrefab.name == "LemurianBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "WispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.lesserwispBuff);
                }
                if (newbodyPrefab.name == "LunarExploderBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.lunarexploderBuff);
                }
                if (newbodyPrefab.name == "LunarGolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MiniMushroomBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.minimushrumBuff);
                }
                if (newbodyPrefab.name == "ParentBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Solus Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.roboballminiBuff);
                }
                if (newbodyPrefab.name == "GolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidBarnacleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarBuff);
                }
                if (newbodyPrefab.name == "VoidJailerBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 6);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.voidjailerBuff);
                }
                if (newbodyPrefab.name == "NullifierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
                }


                if (newbodyPrefab.name == "BeetleQueen2Body")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ImpBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Bleed Quirk</style> Get!");

                    characterBody.AddBuff(Modules.Buffs.impbossBuff);
                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    Shiggymastercon.writeToSkillList(Shiggy.impbosspassiveDef, 6);
                }
                if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.stonetitanBuff);
                }
                if (newbodyPrefab.name == "GrandParentBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Solar Flare Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.grandparentsunDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GravekeeperBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VagrantBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.vagrantBuff);
                }
                if (newbodyPrefab.name == "MagmaWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.magmawormBuff);
                }
                if (newbodyPrefab.name == "ElectricWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.overloadingwormBuff);
                }
                if (newbodyPrefab.name == "ClayBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Tar Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MegaConstructBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidMegaCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ScavBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
                }

                if (newbodyPrefab.name == "Bandit2Body")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lights Out Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.banditlightsoutDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.banditlightsoutDef, GenericSkill.SkillOverridePriority.Contextual);
                }

                if (newbodyPrefab.name == "CaptainBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Defensive Microbots Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.captainpassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.captainpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "CommandoBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Double Tap Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.commandopassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.commandopassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "CrocoBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Poison Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.acridpassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.acridpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "EngiBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Turret Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.engiturretDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.engiturretDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                //if (newbodyPrefab.name == "HereticBody")
                //{

                //    hasQuirk = true;
                //    Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

                //    Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 2);
                //    RemoveUtility();
                //    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
                //}
                if (newbodyPrefab.name == "HuntressBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Flurry Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.huntressattackDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.huntressattackDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LoaderBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Scrap Barrier Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.loaderpassiveDef, 6);
                    RemoveExtra3();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.loaderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MageBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Elementality Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.artificerflamethrowerDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MercBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Eviscerate Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.mercdashDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.mercdashDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ToolbotBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Power Stance Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.multbuffDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "TreebotBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Seed Barrage Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.rexmortarDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.rexmortarDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RailgunnerBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Cryocharged Railgun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.railgunnercryoDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.railgunnercryoDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidSurvivorBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Cleanse Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voidfiendcleanseDef, 2);
                    RemoveUtility();
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.voidfiendcleanseDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (hasQuirk = false)
                {
                    //Shiggymastercon.transformed = false;
                    Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
                }
            }
            if (extrainputBankTest.extraSkill4.down)
            {
                AkSoundEngine.PostEvent(3192656820, characterBody.gameObject);
                if (hurtBox.healthComponent.body.isElite)
                {
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixBlue))
                    {
                        Chat.AddMessage("Stole Overloading <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Lightning.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixHaunted))
                    {
                        Chat.AddMessage("Stole Celestine <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Haunted.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixLunar))
                    {
                        Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Lunar.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixPoison))
                    {
                        Chat.AddMessage("Stole Malachite <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Poison.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixRed))
                    {
                        Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Fire.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixWhite))
                    {
                        Chat.AddMessage("Stole Glacial <style=cIsUtility>Quirk!</style>");
                        dropEquipment(RoR2Content.Elites.Ice.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteEarth))
                    {
                        Chat.AddMessage("Stole Mending <style=cIsUtility>Quirk!</style>");
                        dropEquipment(DLC1Content.Elites.Earth.eliteEquipmentDef);
                    }
                    if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteVoid))
                    {
                        Chat.AddMessage("Stole Void <style=cIsUtility>Quirk!</style>");
                        dropEquipment(DLC1Content.Elites.Void.eliteEquipmentDef);
                    }
                }
                if (newbodyPrefab.name == "VultureBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
                }
                if (newbodyPrefab.name == "BeetleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.beetleBuff);
                }
                if (newbodyPrefab.name == "BeetleGuardBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BisonBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.bisonchargeDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "FlyingVerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.pestjumpBuff);
                }
                if (newbodyPrefab.name == "VerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.verminsprintBuff);
                }
                if (newbodyPrefab.name == "BellBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayGrenadierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayBruiserBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LemurianBruiserBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fire Blast Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.elderlemurianfireblastDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.elderlemurianfireblastDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.gupspikeBuff);
                }
                if (newbodyPrefab.name == "GreaterWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spirit Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "HermitCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.hermitcrabmortarBuff);
                }
                if (newbodyPrefab.name == "ImpBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "JellyfishBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.jellyfishnovaDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "AcidLarvaBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.larvajumpBuff);
                }
                if (newbodyPrefab.name == "LemurianBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "WispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.lesserwispBuff);
                }
                if (newbodyPrefab.name == "LunarExploderBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.lunarexploderBuff);
                }
                if (newbodyPrefab.name == "LunarGolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MiniMushroomBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.minimushrumBuff);
                }
                if (newbodyPrefab.name == "ParentBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Solus Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.roboballminiBuff);
                }
                if (newbodyPrefab.name == "GolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidBarnacleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarBuff);
                }
                if (newbodyPrefab.name == "VoidJailerBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 7);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.voidjailerBuff);
                }
                if (newbodyPrefab.name == "NullifierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
                }


                if (newbodyPrefab.name == "BeetleQueen2Body")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ImpBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Bleed Quirk</style> Get!");

                    characterBody.AddBuff(Modules.Buffs.impbossBuff);
                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    Shiggymastercon.writeToSkillList(Shiggy.impbosspassiveDef, 7);
                }
                if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.stonetitanBuff);
                }
                if (newbodyPrefab.name == "GrandParentBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Solar Flare Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.grandparentsunDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GravekeeperBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VagrantBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.vagrantBuff);
                }
                if (newbodyPrefab.name == "MagmaWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.magmawormBuff);
                }
                if (newbodyPrefab.name == "ElectricWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.AddBuff(Modules.Buffs.overloadingwormBuff);
                }
                if (newbodyPrefab.name == "ClayBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Tar Boost Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MegaConstructBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidMegaCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ScavBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
                }

                if (newbodyPrefab.name == "Bandit2Body")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lights Out Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.banditlightsoutDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.banditlightsoutDef, GenericSkill.SkillOverridePriority.Contextual);
                }

                if (newbodyPrefab.name == "CaptainBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Defensive Microbots Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.captainpassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.captainpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "CommandoBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Double Tap Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.commandopassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.commandopassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "CrocoBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Poison Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.acridpassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.acridpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "EngiBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Turret Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.engiturretDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.engiturretDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                //if (newbodyPrefab.name == "HereticBody")
                //{

                //    hasQuirk = true;
                //    Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

                //    Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 3);
                //    RemoveSpecial();
                //    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
                //}
                if (newbodyPrefab.name == "HuntressBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Flurry Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.huntressattackDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.huntressattackDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LoaderBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Scrap Barrier Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.loaderpassiveDef, 7);
                    RemoveExtra4();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.loaderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MageBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Elementality Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.artificerflamethrowerDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MercBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Eviscerate Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.mercdashDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.mercdashDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ToolbotBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Power Stance Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.multbuffDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "TreebotBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Seed Barrage Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.rexmortarDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.rexmortarDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RailgunnerBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Cryocharged Railgun Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.railgunnercryoDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.railgunnercryoDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidSurvivorBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Cleanse Quirk</style> Get!");

                    Shiggymastercon.writeToSkillList(Shiggy.voidfiendcleanseDef, 3);
                    RemoveSpecial();
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.voidfiendcleanseDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (hasQuirk = false)
                {
                    //Shiggymastercon.transformed = false;
                    Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
                }
            }
        }

        public void RemoveExtra1()
        {
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.acridpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.commandopassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.captainpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.loaderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

        }

        public void RemoveExtra2()
        {
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.acridpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.commandopassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.captainpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.loaderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

        }

        public void RemoveExtra3()
        {
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.acridpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.commandopassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.captainpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.loaderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

        }

        public void RemoveExtra4()
        {
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.impbosspassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.acridpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.commandopassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.captainpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.loaderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

        }

        public void RemovePrimary()
        {
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.elderlemurianfireblastDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.banditlightsoutDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.engiturretDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.huntressattackDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.mercdashDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.railgunnercryoDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.rexmortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.voidfiendcleanseDef, GenericSkill.SkillOverridePriority.Contextual);

        }
        public void RemoveSecondary()
        {
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.elderlemurianfireblastDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.banditlightsoutDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.engiturretDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.huntressattackDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.mercdashDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.railgunnercryoDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.rexmortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voidfiendcleanseDef, GenericSkill.SkillOverridePriority.Contextual);

        }

        public void RemoveUtility()
        {
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.elderlemurianfireblastDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.banditlightsoutDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.engiturretDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.huntressattackDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.mercdashDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.railgunnercryoDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.rexmortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.voidfiendcleanseDef, GenericSkill.SkillOverridePriority.Contextual);

        }

        public void RemoveSpecial()
        {
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.elderlemurianfireblastDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.artificerflamethrowerDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.banditlightsoutDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.engiturretDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.huntressattackDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.mercdashDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.railgunnercryoDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.rexmortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.voidfiendcleanseDef, GenericSkill.SkillOverridePriority.Contextual);

        }

        public void CheckQuirks()
        {
            extraskillLocator = characterBody.gameObject.GetComponent<ExtraSkillLocator>();
            //check passive
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "ALPHACONSTRUCT_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "ALPHACONSTRUCT_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "ALPHACONSTRUCT_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "ALPHACONSTRUCT_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.alphashieldonBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.alphashieldonBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "ALPHACONSTRUCT_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);

                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "BEETLE_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "BEETLE_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "BEETLE_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "BEETLE_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.beetleBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.beetleBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "BEETLE_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "BEETLE_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "BEETLE_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "BEETLE_NAME")

            {
                if (NetworkServer.active)
                {
                    Debug.Log("Addingbeetle");
                    characterBody.AddBuff(Modules.Buffs.beetleBuff);
                }

            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "GUP_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "GUP_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "GUP_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "GUP_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.gupspikeBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.gupspikeBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "GUP_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "GUP_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "GUP_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "GUP_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.gupspikeBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "LARVA_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "LARVA_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "LARVA_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "LARVA_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.larvajumpBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.larvajumpBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "LARVA_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "LARVA_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "LARVA_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "LARVA_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.larvajumpBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "LESSERWISP_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "LESSERWISP_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "LESSERWISP_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "LESSERWISP_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.lesserwispBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.lesserwispBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "LESSERWISP_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "LESSERWISP_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "LESSERWISP_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "LESSERWISP_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.lesserwispBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "LUNAREXPLODER_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "LUNAREXPLODER_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "LUNAREXPLODER_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "LUNAREXPLODER_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.lunarexploderBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.lunarexploderBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "LUNAREXPLODER_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "LUNAREXPLODER_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "LUNAREXPLODER_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "LUNAREXPLODER_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.lunarexploderBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "HERMITCRAB_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "HERMITCRAB_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "HERMITCRAB_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "HERMITCRAB_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.hermitcrabmortarBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.hermitcrabmortarBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "HERMITCRAB_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "HERMITCRAB_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "HERMITCRAB_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "HERMITCRAB_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.hermitcrabmortarBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "PEST_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "PEST_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "PEST_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "PEST_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.pestjumpBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.pestjumpBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "PEST_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "PEST_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "PEST_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "PEST_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.pestjumpBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "VERMIN_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "VERMIN_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "VERMIN_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "VERMIN_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.verminsprintBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.verminsprintBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "VERMIN_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "VERMIN_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "VERMIN_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "VERMIN_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.verminsprintBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "MINIMUSHRUM_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "MINIMUSHRUM_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "MINIMUSHRUM_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "MINIMUSHRUM_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.minimushrumBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.minimushrumBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "MINIMUSHRUM_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "MINIMUSHRUM_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "MINIMUSHRUM_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "MINIMUSHRUM_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.minimushrumBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "ROBOBALLMINI_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "ROBOBALLMINI_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "ROBOBALLMINI_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "ROBOBALLMINI_NAME")    
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.roboballminiBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.roboballminiBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "ROBOBALLMINI_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "ROBOBALLMINI_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "ROBOBALLMINI_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "ROBOBALLMINI_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.roboballminiBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "VOIDBARNACLE_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "VOIDBARNACLE_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "VOIDBARNACLE_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "VOIDBARNACLE_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.voidbarnaclemortarBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.voidbarnaclemortarBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDBARNACLE_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDBARNACLE_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "VOIDBARNACLE_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDBARNACLE_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "VOIDJAILER_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "VOIDJAILER_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "VOIDJAILER_NAME"
                & extraskillLocator.extraFourth.skillNameToken != prefix + "VOIDJAILER_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.voidjailerBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.voidjailerBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDJAILER_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDJAILER_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "VOIDJAILER_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDJAILER_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.voidjailerBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "IMPBOSS_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "IMPBOSS_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "IMPBOSS_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "IMPBOSS_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.impbossBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.impbossBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "IMPBOSS_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "IMPBOSS_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "IMPBOSS_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "IMPBOSS_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.impbossBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "STONETITAN_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "STONETITAN_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "STONETITAN_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "STONETITAN_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.stonetitanBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.stonetitanBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "STONETITAN_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "STONETITAN_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "STONETITAN_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "STONETITAN_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.stonetitanBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "MAGMAWORM_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "MAGMAWORM_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "MAGMAWORM_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "MAGMAWORM_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.magmawormBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.magmawormBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "MAGMAWORM_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "MAGMAWORM_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "MAGMAWORM_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "MAGMAWORM_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.magmawormBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "OVERLOADINGWORM_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "OVERLOADINGWORM_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "OVERLOADINGWORM_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "OVERLOADINGWORM_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.overloadingwormBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.overloadingwormBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "OVERLOADINGWORM_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.overloadingwormBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "VAGRANT_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "VAGRANT_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "VAGRANT_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "VAGRANT_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.vagrantBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.vagrantBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "VAGRANT_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "VAGRANT_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "VAGRANT_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "VAGRANT_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.vagrantBuff);
                }
            }


            if (extraskillLocator.extraFirst.skillNameToken != prefix + "ACRID_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "ACRID_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "ACRID_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "ACRID_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.acridBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.acridBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "ACRID_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "ACRID_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "ACRID_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "ACRID_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.acridBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "COMMANDO_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "COMMANDO_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "COMMANDO_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "COMMANDO_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.commandoBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.commandoBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "COMMANDO_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "COMMANDO_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "COMMANDO_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "COMMANDO_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.commandoBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "CAPTAIN_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "CAPTAIN_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "CAPTAIN_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "CAPTAIN_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.captainBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.captainBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "CAPTAIN_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "CAPTAIN_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "CAPTAIN_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "CAPTAIN_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.captainBuff);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "LOADER_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "LOADER_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "LOADER_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "LOADER_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.loaderBuff))
                {
                    characterBody.RemoveBuff(Modules.Buffs.loaderBuff);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "LOADER_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "LOADER_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "LOADER_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "LOADER_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.AddBuff(Modules.Buffs.loaderBuff);
                }
            }

            ////check active
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "VULTURE_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "VULTURE_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "VULTURE_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "VULTURE_NAME")
            //{
            //	if (!alloyvultureflyDef)
            //	{
            //		alloyvultureflyDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "VULTURE_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "VULTURE_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "VULTURE_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "VULTURE_NAME")
            //{
            //	alloyvultureflyDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEGUARD_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLEGUARD_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLEGUARD_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "BEETLEGUARD_NAME")
            //{
            //	if (!beetleguardslamDef)
            //	{
            //		beetleguardslamDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "BEETLEGUARD_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "BEETLEGUARD_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "BEETLEGUARD_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "BEETLEGUARD_NAME")
            //{
            //	beetleguardslamDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "BRONZONG_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "BRONZONG_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "BRONZONG_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "BRONZONG_NAME")
            //{
            //	if (!bronzongballDef)
            //	{
            //		bronzongballDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "BRONZONG_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "BRONZONG_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "BRONZONG_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "BRONZONG_NAME")
            //{
            //	bronzongballDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "APOTHECARY_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "APOTHECARY_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "APOTHECARY_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "APOTHECARY_NAME")
            //{
            //	if (!clayapothecarymortarDef)
            //	{
            //		clayapothecarymortarDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "APOTHECARY_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "APOTHECARY_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "APOTHECARY_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "APOTHECARY_NAME")
            //{
            //	clayapothecarymortarDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "TEMPLAR_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "TEMPLAR_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "TEMPLAR_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "TEMPLAR_NAME")
            //{
            //	if (!claytemplarminigunDef)
            //	{
            //		claytemplarminigunDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "TEMPLAR_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "TEMPLAR_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "TEMPLAR_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "TEMPLAR_NAME")
            //{
            //	claytemplarminigunDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "GREATERWISP_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "GREATERWISP_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "GREATERWISP_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "GREATERWISP_NAME")
            //{
            //	if (!greaterwispballDef)
            //	{
            //		greaterwispballDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "GREATERWISP_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "GREATERWISP_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "GREATERWISP_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "GREATERWISP_NAME")
            //{
            //	greaterwispballDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "IMP_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "IMP_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "IMP_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "IMP_NAME")
            //{
            //	if (!impblinkDef)
            //	{
            //		impblinkDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "IMP_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "IMP_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "IMP_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "IMP_NAME")
            //{
            //	impblinkDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "JELLYFISH_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "JELLYFISH_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "JELLYFISH_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "JELLYFISH_NAME")
            //{
            //	if (!jellyfishnovaDef)
            //	{
            //		jellyfishnovaDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "JELLYFISH_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "JELLYFISH_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "JELLYFISH_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "JELLYFISH_NAME")
            //{
            //	jellyfishnovaDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "LEMURIAN_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "LEMURIAN_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "LEMURIAN_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "LEMURIAN_NAME")
            //{
            //	if (!lemurianfireballDef)
            //	{
            //		lemurianfireballDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "LEMURIAN_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "LEMURIAN_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "LEMURIAN_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "LEMURIAN_NAME")
            //{
            //	lemurianfireballDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARGOLEM_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARGOLEM_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARGOLEM_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "LUNARGOLEM_NAME")
            //{
            //	if (!lunargolemshotsDef)
            //	{
            //		lunargolemshotsDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "LUNARGOLEM_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "LUNARGOLEM_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "LUNARGOLEM_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "LUNARGOLEM_NAME")
            //{
            //	lunargolemshotsDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARWISP_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARWISP_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARWISP_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "LUNARWISP_NAME")
            //{
            //	if (!lunarwispminigunDef)
            //	{
            //		lunarwispminigunDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "LUNARWISP_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "LUNARWISP_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "LUNARWISP_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "LUNARWISP_NAME")
            //{
            //	lunarwispminigunDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "PARENT_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "PARENT_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "PARENT_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "PARENT_NAME")
            //{
            //	if (!parentteleportDef)
            //	{
            //		parentteleportDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "PARENT_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "PARENT_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "PARENT_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "PARENT_NAME")
            //{
            //	parentteleportDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "STONEGOLEM_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "STONEGOLEM_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "STONEGOLEM_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "STONEGOLEM_NAME")
            //{
            //	if (!stonegolemlaserDef)
            //	{
            //		stonegolemlaserDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "STONEGOLEM_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "STONEGOLEM_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "STONEGOLEM_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "STONEGOLEM_NAME")
            //{
            //	stonegolemlaserDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDJAILER_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDJAILER_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDJAILER_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "VOIDJAILER_NAME")
            //{
            //	if (!voidjailerpassiveDef)
            //	{
            //		voidjailerpassiveDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "VOIDJAILER_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "VOIDJAILER_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "VOIDJAILER_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "VOIDJAILER_NAME")
            //{
            //	voidjailerpassiveDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEQUEEN_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLEQUEEN_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLEQUEEN_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "BEETLEQUEEN_NAME")
            //{
            //	if (!beetlequeenshotgunDef)
            //	{
            //		beetlequeenshotgunDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "BEETLEQUEEN_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "BEETLEQUEEN_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "BEETLEQUEEN_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "BEETLEQUEEN_NAME")
            //{
            //	beetlequeenshotgunDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "GRANDPARENT_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "GRANDPARENT_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "GRANDPARENT_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "GRANDPARENT_NAME")
            //{
            //	if (!grandparentsunDef)
            //	{
            //		grandparentsunDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "GRANDPARENT_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "GRANDPARENT_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "GRANDPARENT_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "GRANDPARENT_NAME")
            //{
            //	grandparentsunDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "GROVETENDER_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "GROVETENDER_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "GROVETENDER_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "GROVETENDER_NAME")
            //{
            //	if (!grovetenderhookDef)
            //	{
            //		grovetenderhookDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "GROVETENDER_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "GROVETENDER_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "GROVETENDER_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "GROVETENDER_NAME")
            //{
            //	grovetenderhookDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME")
            //{
            //	if (!claydunestriderballDef)
            //	{
            //		claydunestriderballDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "CLAYDUNESTRIDER_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "CLAYDUNESTRIDER_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "CLAYDUNESTRIDER_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "CLAYDUNESTRIDER_NAME")
            //{
            //	claydunestriderballDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME")
            //{
            //	if (!soluscontrolunityknockupDef)
            //	{
            //		soluscontrolunityknockupDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "SOLUSCONTROLUNIT_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "SOLUSCONTROLUNIT_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "SOLUSCONTROLUNIT_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "SOLUSCONTROLUNIT_NAME")
            //{
            //	soluscontrolunityknockupDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "XICONSTRUCT_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "XICONSTRUCT_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "XICONSTRUCT_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "XICONSTRUCT_NAME")
            //{
            //	if (!xiconstructbeamDef)
            //	{
            //		xiconstructbeamDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "XICONSTRUCT_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "XICONSTRUCT_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "XICONSTRUCT_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "XICONSTRUCT_NAME")
            //{
            //	xiconstructbeamDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "VOIDDEVASTATOR_NAME")
            //{
            //	if (!voiddevastatorhomingDef)
            //	{
            //		voiddevastatorhomingDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "VOIDDEVASTATOR_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "VOIDDEVASTATOR_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "VOIDDEVASTATOR_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "VOIDDEVASTATOR_NAME")
            //{
            //	voiddevastatorhomingDef = false;
            //}
            //if (characterBody.skillLocator.primary.skillNameToken == prefix + "SCAVENGER_NAME"
            //	|| characterBody.skillLocator.secondary.skillNameToken == prefix + "SCAVENGER_NAME"
            //	|| characterBody.skillLocator.utility.skillNameToken == prefix + "SCAVENGER_NAME"
            //	|| characterBody.skillLocator.special.skillNameToken == prefix + "SCAVENGER_NAME")
            //{
            //	if (!scavengerthqwibDef)
            //	{
            //		scavengerthqwibDef = true;
            //	}
            //}
            //else if (characterBody.skillLocator.primary.skillNameToken != prefix + "SCAVENGER_NAME"
            //	&& characterBody.skillLocator.secondary.skillNameToken != prefix + "SCAVENGER_NAME"
            //	&& characterBody.skillLocator.utility.skillNameToken != prefix + "SCAVENGER_NAME"
            //	&& characterBody.skillLocator.special.skillNameToken != prefix + "SCAVENGER_NAME")
            //{
            //	scavengerthqwibDef = false;
            //}

        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

        public override void OnSerialize(NetworkWriter writer)
        {
            writer.Write(HurtBoxReference.FromHurtBox(this.Target));
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            this.Target = reader.ReadHurtBoxReference().ResolveHurtBox();
        }
    }
}