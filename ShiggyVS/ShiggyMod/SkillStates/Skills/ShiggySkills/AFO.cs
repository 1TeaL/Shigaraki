﻿using ShiggyMod.Modules.Survivors;
using EntityStates;
using RoR2;
using UnityEngine;
using System.Collections.Generic;
using ShiggyMod.Modules;
using UnityEngine.Networking;
using RoR2.ExpansionManagement;
using ExtraSkillSlots;

namespace ShiggyMod.SkillStates
{
    public class AFO : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";

        private float duration = 1f;
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
            hasQuirk = false;

            PlayCrossfade("RightArm, Override", "RightArmPull", 0.1f);

            //extraskillLocator = base.GetComponent<ExtraSkillLocator>();

            //extrainputBankTest = outer.GetComponent<ExtraInputBankTest>();


            //Shiggycon = base.GetComponent<ShiggyController>();
            //Shiggymastercon = characterBody.master.gameObject.GetComponent<ShiggyMasterController>();
            //if (Shiggycon && base.isAuthority)
            //{
            //    Target = Shiggycon.GetTrackingTarget();
            //}

            //if (!Target)
            //{
            //    return;
            //}
            //hasFired = false;
            //hasQuirk = false;
            //PlayAnimation("Body", "BonusJump", "Attack.playbackRate", duration / 2);
        }

        public override void OnExit()
        {
            base.OnExit();
        }



        public override void FixedUpdate()
        {
            base.FixedUpdate();

            //if (base.fixedAge >= this.fireTime && !this.hasFired)
            //{
            //    hasFired = true;
            //    if (Target)
            //    {

            //        Debug.Log("Target");
            //        Debug.Log(BodyCatalog.FindBodyPrefab(BodyCatalog.GetBodyName(Target.healthComponent.body.bodyIndex)));
            //        //AkSoundEngine.PostEvent(1719197672, this.gameObject);
            //        StealQuirk(Target);


            //        return;
            //    }
            //}

            //{
                if (base.fixedAge >= this.duration && base.isAuthority)
                {
                    this.outer.SetNextStateToMain();
                    return;
                }
            //}

        }

        //public void dropEquipment(EquipmentDef def)
        //{
        //    PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(def.equipmentIndex), base.transform.position + Vector3.up * 1.5f, Vector3.up * 20f + base.transform.forward * 2f);

        //}


        //private void StealQuirk(HurtBox hurtBox)
        //{
        //    if (hurtBox.healthComponent.body.isElite)
        //    {
        //        if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixBlue))
        //        {
        //            Chat.AddMessage("Stole Overloading <style=cIsUtility>Quirk!</style>");
        //            dropEquipment(RoR2Content.Elites.Lightning.eliteEquipmentDef);
        //        }
        //        if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixHaunted))
        //        {
        //            Chat.AddMessage("Stole Celestine <style=cIsUtility>Quirk!</style>");
        //            dropEquipment(RoR2Content.Elites.Haunted.eliteEquipmentDef);
        //        }
        //        if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixLunar))
        //        {
        //            Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
        //            dropEquipment(RoR2Content.Elites.Lunar.eliteEquipmentDef);
        //        }
        //        if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixPoison))
        //        {
        //            Chat.AddMessage("Stole Malachite <style=cIsUtility>Quirk!</style>");
        //            dropEquipment(RoR2Content.Elites.Poison.eliteEquipmentDef);
        //        }
        //        if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixRed))
        //        {
        //            Chat.AddMessage("Stole Blazing <style=cIsUtility>Quirk!</style>");
        //            dropEquipment(RoR2Content.Elites.Fire.eliteEquipmentDef);
        //        }
        //        if (hurtBox.healthComponent.body.HasBuff(RoR2Content.Buffs.AffixWhite))
        //        {
        //            Chat.AddMessage("Stole Glacial <style=cIsUtility>Quirk!</style>");
        //            dropEquipment(RoR2Content.Elites.Ice.eliteEquipmentDef);
        //        }
        //        if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteEarth))
        //        {
        //            Chat.AddMessage("Stole Mending <style=cIsUtility>Quirk!</style>");
        //            dropEquipment(DLC1Content.Elites.Earth.eliteEquipmentDef);
        //        }
        //        if (hurtBox.healthComponent.body.HasBuff(DLC1Content.Buffs.EliteVoid))
        //        {
        //            Chat.AddMessage("Stole Void <style=cIsUtility>Quirk!</style>");
        //            dropEquipment(DLC1Content.Elites.Void.eliteEquipmentDef);
        //        }
        //    }

        //    var name = BodyCatalog.GetBodyName(hurtBox.healthComponent.body.bodyIndex);
        //    GameObject newbodyPrefab = BodyCatalog.FindBodyPrefab(name);

        //    Shiggymastercon = characterBody.master.gameObject.GetComponent<ShiggyMasterController>();

        //    if (base.IsKeyDownAuthority() && extrainputBankTest.extraSkill1.down)
        //    {
        //        if (newbodyPrefab.name == "VultureBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BeetleBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BeetleGuardBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BisonBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.bisonchargeDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "FlyingVerminBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VerminBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BellBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayGrenadierBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayBruiserBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 0);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GreaterWispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spirit Ball Control Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "HermitCrabBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ImpBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "JellyfishBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "AcidLarvaBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 0);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LemurianBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "WispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LunarExploderBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LunarGolemBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LunarWispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MiniMushroomBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ParentBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Glide Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 0);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GolemBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidBarnacleBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidJailerBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 4);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "NullifierBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }


        //        if (newbodyPrefab.name == "BeetleQueen2Body")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GravekeeperBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VagrantBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MagmaWormBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ElectricWormBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 4);
        //            RemoveExtra1();
        //            extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Rolling Clay Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MegaConstructBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidMegaCrabBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ScavBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 0);
        //            RemovePrimary();
        //            base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }

        //        if (hasQuirk = false)
        //        {
        //            //Shiggymastercon.transformed = false;
        //            Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
        //        }

        //    }

        //    if (base.IsKeyDownAuthority() && extrainputBankTest.extraSkill2.down)
        //    {
        //        if (newbodyPrefab.name == "VultureBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BeetleBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BeetleGuardBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BisonBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.bisonchargeDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "FlyingVerminBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VerminBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BellBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayGrenadierBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayBruiserBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 1);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GreaterWispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spirit Ball Control Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "HermitCrabBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ImpBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "JellyfishBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "AcidLarvaBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 1);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LemurianBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "WispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LunarExploderBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LunarGolemBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LunarWispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MiniMushroomBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ParentBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Glide Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 1);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GolemBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidBarnacleBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidJailerBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 5);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "NullifierBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }


        //        if (newbodyPrefab.name == "BeetleQueen2Body")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GravekeeperBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VagrantBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MagmaWormBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ElectricWormBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 5);
        //            RemoveExtra2();
        //            extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Rolling Clay Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MegaConstructBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidMegaCrabBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ScavBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 1);
        //            RemoveSecondary();
        //            base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }

        //        if (hasQuirk = false)
        //        {
        //            //Shiggymastercon.transformed = false;
        //            Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
        //        }
        //    }
        //    if (base.IsKeyDownAuthority() && extrainputBankTest.extraSkill3.down)
        //    {
        //        if (newbodyPrefab.name == "VultureBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BeetleBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BeetleGuardBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BisonBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.bisonchargeDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "FlyingVerminBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VerminBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BellBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayGrenadierBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayBruiserBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 2);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GreaterWispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spirit Ball Control Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "HermitCrabBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ImpBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "JellyfishBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "AcidLarvaBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 2);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LemurianBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "WispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LunarExploderBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LunarGolemBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LunarWispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MiniMushroomBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ParentBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Glide Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 2);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GolemBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidBarnacleBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidJailerBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 6);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "NullifierBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }


        //        if (newbodyPrefab.name == "BeetleQueen2Body")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GravekeeperBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VagrantBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MagmaWormBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ElectricWormBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 6);
        //            RemoveExtra3();
        //            extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Rolling Clay Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MegaConstructBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidMegaCrabBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ScavBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 2);
        //            RemoveUtility();
        //            base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }

        //        if (hasQuirk = false)
        //        {
        //            //Shiggymastercon.transformed = false;
        //            Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
        //        }
        //    }
        //    if (base.IsKeyDownAuthority() && extrainputBankTest.extraSkill4.down)
        //    {
        //        if (newbodyPrefab.name == "VultureBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 7);
        //            RemoveExtra3();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BeetleBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 7);
        //            RemoveExtra3();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BeetleGuardBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BisonBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.bisonchargeDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "FlyingVerminBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 7);
        //            RemoveExtra3();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VerminBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 7);
        //            RemoveExtra3();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "BellBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayGrenadierBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayBruiserBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 3);
        //            RemoveExtra3();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GreaterWispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Spirit Ball Control Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "HermitCrabBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 7);
        //            RemoveExtra3();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ImpBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "JellyfishBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "AcidLarvaBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 3);
        //            RemoveExtra3();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LemurianBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "WispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 7);
        //            RemoveExtra3();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LunarExploderBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 7);
        //            RemoveExtra3();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LunarGolemBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "LunarWispBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MiniMushroomBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 7);
        //            RemoveExtra3();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ParentBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Glide Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 3);
        //            RemoveExtra3();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GolemBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidBarnacleBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 7);
        //            RemoveExtra3();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidJailerBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 7);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "NullifierBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }


        //        if (newbodyPrefab.name == "BeetleQueen2Body")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 7);
        //            RemoveExtra3();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "GravekeeperBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VagrantBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 7);
        //            RemoveExtra3();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MagmaWormBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 7);
        //            RemoveExtra3();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ElectricWormBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 7);
        //            RemoveExtra3();
        //            extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ClayBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Rolling Clay Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "MegaConstructBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "VoidMegaCrabBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }
        //        if (newbodyPrefab.name == "ScavBody")
        //        {

        //            hasQuirk = true;
        //            Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

        //            Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 3);
        //            RemoveSpecial();
        //            base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
        //        }

        //        if (hasQuirk = false)
        //        {
        //            //Shiggymastercon.transformed = false;
        //            Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
        //        }
        //    }


        //}

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
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

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
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

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
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

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
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
        //    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

        //}

        //public void RemovePrimary()
        //{
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);

        //}
        //public void RemoveSecondary()
        //{
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);

        //}

        //public void RemoveUtility()
        //{
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);

        //}

        //public void RemoveSpecial()
        //{
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
        //    base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);

        //}
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

        //public override void OnSerialize(NetworkWriter writer)
        //{
        //    writer.Write(HurtBoxReference.FromHurtBox(this.Target));
        //}

        //public override void OnDeserialize(NetworkReader reader)
        //{
        //    this.Target = reader.ReadHurtBoxReference().ResolveHurtBox();
        //}
    }
}