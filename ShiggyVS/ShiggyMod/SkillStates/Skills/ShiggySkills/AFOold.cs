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
    public class AFOold : BaseSkillState
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
            //    characterBody.ApplyBuff(Modules.Buffs.alphashieldonBuff);
            //    characterBody.ApplyBuff(Modules.Buffs.beetleBuff);
            //    characterBody.ApplyBuff(Modules.Buffs.minimushrumBuff);
            //    characterBody.ApplyBuff(Modules.Buffs.larvajumpBuff);
            //    characterBody.ApplyBuff(Modules.Buffs.pestjumpBuff);
            //    characterBody.ApplyBuff(Modules.Buffs.vagrantBuff);
            //    characterBody.ApplyBuff(Modules.Buffs.magmawormBuff);
            //    characterBody.ApplyBuff(Modules.Buffs.gupspikeBuff);
            //    characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortarBuff);
            //    characterBody.ApplyBuff(Modules.Buffs.impbossBuff);
            //    characterBody.ApplyBuff(Modules.Buffs.lesserwispBuff);
            //    characterBody.ApplyBuff(Modules.Buffs.lunarexploderBuff);
            //    characterBody.ApplyBuff(Modules.Buffs.roboballminiBuff);
            //    characterBody.ApplyBuff(Modules.Buffs.stonetitanBuff);
            //    characterBody.ApplyBuff(Modules.Buffs.verminsprintBuff);
            //    characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarBuff);
            //    characterBody.ApplyBuff(Modules.Buffs.voidjailerBuff);
            //}

            //CheckQuirksForBuffs();
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
            //CheckQuirksForBuffs();
        }



        public override void FixedUpdate()
        {
            base.FixedUpdate();

            //if (Config.holdButtonAFO.Value)
            //{
            //    if (base.fixedAge >= this.fireTime && !this.hasFired)
            //    {
            //        hasFired = true;
            //        if (Target)
            //        {

            //            //Debug.Log("Target");
            //            //Debug.Log(BodyCatalog.FindBodyPrefab(BodyCatalog.GetBodyName(Target.healthComponent.body.bodyIndex)));
            //            //AkSoundEngine.PostEvent(1719197672, this.gameObject);
            //            StealQuirk(Target);


            //            this.outer.SetNextStateToMain();
            //            return;
            //        }
            //    }
            //    if (base.IsKeyDownAuthority())
            //    {
            //        if (base.fixedAge >= 3f)
            //        {
            //            if (extrainputBankTest.extraSkill1.down)
            //            {
            //                Chat.AddMessage("<style=cIsUtility>Quirks</style> Removed");
            //                RemovePrimary();
            //                RemoveExtra1();
            //            }
            //            if (extrainputBankTest.extraSkill2.down)
            //            {
            //                Chat.AddMessage("<style=cIsUtility>Quirks</style> Removed");
            //                RemoveSecondary();
            //                RemoveExtra2();
            //            }
            //            if (extrainputBankTest.extraSkill3.down)
            //            {
            //                Chat.AddMessage("<style=cIsUtility>Quirks</style> Removed");
            //                RemoveUtility();
            //                RemoveExtra3();
            //            }
            //            if (extrainputBankTest.extraSkill4.down)
            //            {
            //                Chat.AddMessage("<style=cIsUtility>Quirks</style> Removed");
            //                RemoveSpecial();
            //                RemoveExtra4();
            //            }
            //            //CheckQuirksForBuffs();
            //            this.outer.SetNextStateToMain();
            //            return;
            //        }

            //    }
            //    else if (base.fixedAge >= duration && base.isAuthority)
            //    {
            //        this.outer.SetNextStateToMain();
            //        return;
            //    }

            //}
            //else if (!Config.holdButtonAFO.Value)
            //{
            //    if (base.IsKeyDownAuthority() && base.isAuthority)
            //    {
            //        if (base.fixedAge >= 1f && !this.hasFired)
            //        {
            //            hasFired = true;
            //            if (Target)
            //            {

            //                Debug.Log("Target");
            //                Debug.Log(BodyCatalog.FindBodyPrefab(BodyCatalog.GetBodyName(Target.healthComponent.body.bodyIndex)));
            //                //AkSoundEngine.PostEvent(1719197672, this.gameObject);
            //                StealQuirk(Target);


            //                this.outer.SetNextStateToMain();
            //                return;
            //            }
            //        }
            //        if(base.fixedAge >= 3f)
            //        {
                        
            //            Chat.AddMessage("<style=cIsUtility>Choose which Quirk to Remove</style>");
                        
            //            //CheckQuirksForBuffs();
            //            this.outer.SetNextStateToMain();
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        if (base.fixedAge >= this.duration && base.isAuthority)
            //        {
            //            this.outer.SetNextStateToMain();
            //            return;
            //        }
            //    }

            //}


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

            //AkSoundEngine.PostEvent("ShiggyAFO", characterBody.gameObject);

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

                if (newbodyPrefab.name == "VultureBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.alloyvultureWindBlastDef, 0);
                }
                if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.alphacontructpassiveDef, 0);

                    characterBody.ApplyBuff(Modules.Buffs.alphashieldonBuff.buffIndex);
                }
                if (newbodyPrefab.name == "BeetleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.beetlepassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.beetleBuff.buffIndex);
                }
                if (newbodyPrefab.name == "BeetleGuardBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.beetleguardslamDef, 0);
                }
                if (newbodyPrefab.name == "BisonBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.bisonchargeDef, 0);
                }
                if (newbodyPrefab.name == "FlyingVerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.pestpassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.pestjumpBuff.buffIndex);
                }
                if (newbodyPrefab.name == "VerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.verminpassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.verminsprintBuff.buffIndex);
                }
                if (newbodyPrefab.name == "BellBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.bronzongballDef, 0);
                }
                if (newbodyPrefab.name == "ClayGrenadierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.clayapothecarymortarDef, 0);
                }
                if (newbodyPrefab.name == "ClayBruiserBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.claytemplarminigunDef, 0);
                }
                if (newbodyPrefab.name == "LemurianBruiserBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fire Blast Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.elderlemurianfireblastDef, 0);
                }
                if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.guppassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.gupspikeBuff.buffIndex);
                }
                if (newbodyPrefab.name == "GreaterWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spirit Boost Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.greaterWispBuffDef, 0);
                }
                if (newbodyPrefab.name == "HermitCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.hermitcrabpassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortarBuff.buffIndex);
                }
                if (newbodyPrefab.name == "ImpBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.impblinkDef, 0);
                }
                if (newbodyPrefab.name == "JellyfishBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.jellyfishHealDef, 0);
                }
                if (newbodyPrefab.name == "AcidLarvaBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.larvapassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.larvajumpBuff.buffIndex);
                }
                if (newbodyPrefab.name == "LemurianBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.lemurianfireballDef, 0);
                }
                if (newbodyPrefab.name == "WispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.lesserwisppassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.lesserwispBuff.buffIndex);
                }
                if (newbodyPrefab.name == "LunarExploderBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.lunarexploderpassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.lunarexploderBuff.buffIndex);
                }
                if (newbodyPrefab.name == "LunarGolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.lunargolemSlideDef, 0);
                }
                if (newbodyPrefab.name == "LunarWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.lunarwispminigunDef, 0);
                }
                if (newbodyPrefab.name == "MiniMushroomBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.minimushrumpassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.minimushrumBuff.buffIndex);
                }
                if (newbodyPrefab.name == "ParentBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.parentteleportDef, 0);
                }
                if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Solus Boost Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.roboballminibpassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.roboballminiBuff.buffIndex);
                }
                if (newbodyPrefab.name == "GolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.stonegolemlaserDef, 0);
                }
                if (newbodyPrefab.name == "VoidBarnacleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.voidbarnaclepassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarBuff.buffIndex);
                }
                if (newbodyPrefab.name == "VoidJailerBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.voidjailerpassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.voidjailerBuff.buffIndex);
                }
                if (newbodyPrefab.name == "NullifierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.voidreaverportalDef, 0);
                }


                if (newbodyPrefab.name == "BeetleQueen2Body")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.beetlequeenSummonDef, 0);
                }
                if (newbodyPrefab.name == "ImpBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Bleed Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.impbosspassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.impbossBuff.buffIndex);
                }
                if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.stonetitanpassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.stonetitanBuff.buffIndex);
                }
                if (newbodyPrefab.name == "GrandParentBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Solar Flare Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.grandparentsunDef, 0);
                }
                if (newbodyPrefab.name == "GravekeeperBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.grovetenderChainDef, 0);
                }
                if (newbodyPrefab.name == "VagrantBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.vagrantpassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.vagrantBuff.buffIndex);
                }
                if (newbodyPrefab.name == "MagmaWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.magmawormpassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.magmawormBuff.buffIndex);
                }
                if (newbodyPrefab.name == "ElectricWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.overloadingwormpassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.overloadingwormBuff.buffIndex);
                }
                if (newbodyPrefab.name == "ClayBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Tar Boost Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.claydunestriderbuffDef, 0);
                }
                if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.soluscontrolunityknockupDef, 0);
                }
                if (newbodyPrefab.name == "MegaConstructBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.xiconstructbeamDef, 0);
                }
                if (newbodyPrefab.name == "VoidMegaCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.voiddevastatorhomingDef, 0);
                }
                if (newbodyPrefab.name == "ScavBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.scavengerthqwibDef, 0);
                }

                if (newbodyPrefab.name == "Bandit2Body")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lights Out Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.banditlightsoutDef, 0);
                }

                if (newbodyPrefab.name == "CaptainBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Defensive Microbots Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.captainpassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.captainBuff.buffIndex);
                }
                if (newbodyPrefab.name == "CommandoBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Double Tap Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.commandopassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.commandoBuff.buffIndex);
                }
                if (newbodyPrefab.name == "CrocoBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Poison Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.acridpassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.acridBuff.buffIndex);
                }
                if (newbodyPrefab.name == "EngiBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Turret Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.engiturretDef, 0);
                }
                //if (newbodyPrefab.name == "HereticBody")
                //{

                //    hasQuirk = true;
                //    Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

                //    Shiggymastercon.writeToAFOSkillList(Shiggy.scavengerthqwibDef, 0);
                //    RemovePrimary();
                //    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
                //}
                if (newbodyPrefab.name == "HuntressBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Flurry Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.huntressattackDef, 0);
                }
                if (newbodyPrefab.name == "LoaderBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Scrap Barrier Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.loaderpassiveDef, 0);
                    characterBody.ApplyBuff(Modules.Buffs.loaderBuff.buffIndex);
                }
                if (newbodyPrefab.name == "MageBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Elementality Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.artificerflamethrowerDef, 0);
                }
                if (newbodyPrefab.name == "MercBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Eviscerate Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.mercdashDef, 0);
                }
                if (newbodyPrefab.name == "ToolbotBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Power Stance Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.multbuffDef, 0);
                }
                if (newbodyPrefab.name == "TreebotBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Seed Barrage Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.rexmortarDef, 0);
                }
                if (newbodyPrefab.name == "RailgunnerBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Cryocharged Railgun Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.railgunnercryoDef, 0);
                }
                if (newbodyPrefab.name == "VoidSurvivorBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Cleanse Quirk</style> Get!");

                    Shiggymastercon.writeToAFOSkillList(Shiggy.voidfiendcleanseDef, 0);
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
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.alloyvultureWindBlastDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.greaterWispBuffDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.jellyfishHealDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.lunargolemSlideDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.beetlequeenSummonDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.grovetenderChainDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.claydunestriderbuffDef, GenericSkill.SkillOverridePriority.Contextual);
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
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.alloyvultureWindBlastDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.greaterWispBuffDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.jellyfishHealDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lunargolemSlideDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.beetlequeenSummonDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.grovetenderChainDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.claydunestriderbuffDef, GenericSkill.SkillOverridePriority.Contextual);
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
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.alloyvultureWindBlastDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.greaterWispBuffDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.jellyfishHealDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.lunargolemSlideDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.beetlequeenSummonDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.grovetenderChainDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.claydunestriderbuffDef, GenericSkill.SkillOverridePriority.Contextual);
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
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.alloyvultureWindBlastDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.greaterWispBuffDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.jellyfishHealDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.lunargolemSlideDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.beetlequeenSummonDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.grandparentsunDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.grovetenderChainDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.claydunestriderbuffDef, GenericSkill.SkillOverridePriority.Contextual);
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

        //public void CheckQuirksForBuffs()
        //    {
        //        extraskillLocator = characterBody.gameObject.GetComponent<ExtraSkillLocator>();
        //        //check passive
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "ALPHACONSTRUCT_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "ALPHACONSTRUCT_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "ALPHACONSTRUCT_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "ALPHACONSTRUCT_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.alphashieldonBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.alphashieldonBuff.buffIndex, 0);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "ALPHACONSTRUCT_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.alphashieldonBuff.buffIndex, 1);

        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "BEETLE_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "BEETLE_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "BEETLE_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "BEETLE_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.beetleBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.beetleBuff.buffIndex, 0);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "BEETLE_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "BEETLE_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "BEETLE_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "BEETLE_NAME")

        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.beetleBuff.buffIndex, 1);
        //            }

        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "GUP_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "GUP_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "GUP_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "GUP_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.gupspikeBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.gupspikeBuff.buffIndex, 0);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "GUP_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "GUP_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "GUP_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "GUP_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.gupspikeBuff.buffIndex, 1);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "LARVA_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "LARVA_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "LARVA_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "LARVA_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.larvajumpBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.larvajumpBuff.buffIndex, 0);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "LARVA_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "LARVA_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "LARVA_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "LARVA_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.larvajumpBuff.buffIndex, 1);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "LESSERWISP_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "LESSERWISP_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "LESSERWISP_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "LESSERWISP_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.lesserwispBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.lesserwispBuff.buffIndex, 0);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "LESSERWISP_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "LESSERWISP_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "LESSERWISP_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "LESSERWISP_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.lesserwispBuff.buffIndex, 1);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "LUNAREXPLODER_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "LUNAREXPLODER_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "LUNAREXPLODER_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "LUNAREXPLODER_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.lunarexploderBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.lunarexploderBuff.buffIndex, 0);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "LUNAREXPLODER_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "LUNAREXPLODER_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "LUNAREXPLODER_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "LUNAREXPLODER_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.lunarexploderBuff.buffIndex, 1);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "HERMITCRAB_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "HERMITCRAB_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "HERMITCRAB_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "HERMITCRAB_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.hermitcrabmortarBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortarBuff.buffIndex, 0);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "HERMITCRAB_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "HERMITCRAB_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "HERMITCRAB_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "HERMITCRAB_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortarBuff.buffIndex, 1);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "PEST_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "PEST_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "PEST_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "PEST_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.pestjumpBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.pestjumpBuff.buffIndex, 0);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "PEST_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "PEST_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "PEST_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "PEST_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.pestjumpBuff.buffIndex, 1);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "VERMIN_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "VERMIN_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "VERMIN_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "VERMIN_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.verminsprintBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.verminsprintBuff.buffIndex, 0);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "VERMIN_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "VERMIN_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "VERMIN_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "VERMIN_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.verminsprintBuff.buffIndex, 1);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "MINIMUSHRUM_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "MINIMUSHRUM_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "MINIMUSHRUM_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "MINIMUSHRUM_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.minimushrumBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.minimushrumBuff.buffIndex, 0);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "MINIMUSHRUM_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "MINIMUSHRUM_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "MINIMUSHRUM_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "MINIMUSHRUM_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.minimushrumBuff.buffIndex, 1);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "ROBOBALLMINI_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "ROBOBALLMINI_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "ROBOBALLMINI_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "ROBOBALLMINI_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.roboballminiBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.roboballminiBuff.buffIndex, 0);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "ROBOBALLMINI_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "ROBOBALLMINI_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "ROBOBALLMINI_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "ROBOBALLMINI_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.roboballminiBuff.buffIndex, 1);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "VOIDBARNACLE_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "VOIDBARNACLE_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "VOIDBARNACLE_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "VOIDBARNACLE_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.voidbarnaclemortarBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarBuff.buffIndex, 0);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDBARNACLE_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDBARNACLE_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "VOIDBARNACLE_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDBARNACLE_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarBuff.buffIndex, 1);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "VOIDJAILER_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "VOIDJAILER_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "VOIDJAILER_NAME"
        //            & extraskillLocator.extraFourth.skillNameToken != prefix + "VOIDJAILER_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.voidjailerBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.voidjailerBuff.buffIndex, 0);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDJAILER_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDJAILER_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "VOIDJAILER_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDJAILER_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.voidjailerBuff.buffIndex, 1);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "IMPBOSS_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "IMPBOSS_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "IMPBOSS_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "IMPBOSS_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.impbossBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.impbossBuff.buffIndex, 0);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "IMPBOSS_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "IMPBOSS_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "IMPBOSS_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "IMPBOSS_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.impbossBuff.buffIndex, 1);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "STONETITAN_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "STONETITAN_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "STONETITAN_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "STONETITAN_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.stonetitanBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.stonetitanBuff.buffIndex, 0);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "STONETITAN_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "STONETITAN_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "STONETITAN_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "STONETITAN_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.stonetitanBuff.buffIndex, 1);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "MAGMAWORM_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "MAGMAWORM_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "MAGMAWORM_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "MAGMAWORM_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.magmawormBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.magmawormBuff.buffIndex, 0);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "MAGMAWORM_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "MAGMAWORM_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "MAGMAWORM_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "MAGMAWORM_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.magmawormBuff.buffIndex, 1);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "OVERLOADINGWORM_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "OVERLOADINGWORM_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "OVERLOADINGWORM_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "OVERLOADINGWORM_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.overloadingwormBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.overloadingwormBuff.buffIndex, 0);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "OVERLOADINGWORM_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.overloadingwormBuff);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "VAGRANT_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "VAGRANT_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "VAGRANT_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "VAGRANT_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.vagrantBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.vagrantBuff);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "VAGRANT_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "VAGRANT_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "VAGRANT_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "VAGRANT_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.vagrantBuff);
        //            }
        //        }


        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "ACRID_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "ACRID_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "ACRID_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "ACRID_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.acridBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.acridBuff);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "ACRID_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "ACRID_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "ACRID_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "ACRID_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.acridBuff);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "COMMANDO_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "COMMANDO_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "COMMANDO_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "COMMANDO_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.commandoBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.commandoBuff);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "COMMANDO_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "COMMANDO_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "COMMANDO_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "COMMANDO_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.commandoBuff);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "CAPTAIN_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "CAPTAIN_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "CAPTAIN_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "CAPTAIN_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.captainBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.captainBuff);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "CAPTAIN_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "CAPTAIN_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "CAPTAIN_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "CAPTAIN_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.captainBuff);
        //            }
        //        }
        //        if (extraskillLocator.extraFirst.skillNameToken != prefix + "LOADER_NAME"
        //            && extraskillLocator.extraSecond.skillNameToken != prefix + "LOADER_NAME"
        //            && extraskillLocator.extraThird.skillNameToken != prefix + "LOADER_NAME"
        //            && extraskillLocator.extraFourth.skillNameToken != prefix + "LOADER_NAME")
        //        {
        //            if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.loaderBuff))
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.loaderBuff);
        //            }
        //        }
        //        else if (extraskillLocator.extraFirst.skillNameToken == prefix + "LOADER_NAME"
        //            || extraskillLocator.extraSecond.skillNameToken == prefix + "LOADER_NAME"
        //            || extraskillLocator.extraThird.skillNameToken == prefix + "LOADER_NAME"
        //            || extraskillLocator.extraFourth.skillNameToken == prefix + "LOADER_NAME")
        //        {
        //            if (NetworkServer.active)
        //            {
        //                characterBody.ApplyBuff(Modules.Buffs.loaderBuff);
        //            }
        //        }

        //    }
        
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