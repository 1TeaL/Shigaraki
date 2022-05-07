using ShiggyMod.Modules.Survivors;
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
    public class HermitCrab : BaseSkillState
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
            //PlayAnimation("Body", "BonusJump", "Attack.playbackRate", duration / 2);
        }

        public override void OnExit()
        {
            base.OnExit();
        }



        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge >= this.fireTime && !this.hasFired)
            {
                hasFired = true;
                if (Target)
                {

                    Debug.Log("Target");
                    Debug.Log(BodyCatalog.FindBodyPrefab(BodyCatalog.GetBodyName(Target.healthComponent.body.bodyIndex)));
                    //AkSoundEngine.PostEvent(1719197672, this.gameObject);
                    StealQuirk(Target);


                    return;
                }
            }

            {
                if (base.fixedAge >= this.duration && base.isAuthority)
                {
                    this.outer.SetNextStateToMain();
                    return;
                }
            }

        }

        public void dropEquipment(EquipmentDef def)
        {
            PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(def.equipmentIndex), base.transform.position + Vector3.up * 1.5f, Vector3.up * 20f + base.transform.forward * 2f);

        }


        private void StealQuirk(HurtBox hurtBox)
        {
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

            var name = BodyCatalog.GetBodyName(hurtBox.healthComponent.body.bodyIndex);
            GameObject newbodyPrefab = BodyCatalog.FindBodyPrefab(name);

            if (base.IsKeyDownAuthority() && extrainputBankTest.extraSkill1.down)
            {
                if (newbodyPrefab.name == "VultureBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BeetleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BeetleGuardBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BisonBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "FlyingVerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BellBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayGrenadierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayBruiserBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GreaterWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spirit Ball Control Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "HermitCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ImpBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "JellyfishBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "AcidLarvaBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LemurianBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "WispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarExploderBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarGolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MiniMushroomBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ParentBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Glide Quirk</style> Get!");

                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidBarnacleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidJailerBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "NullifierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
                }


                if (newbodyPrefab.name == "BeetleQueen2Body")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GravekeeperBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VagrantBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MagmaWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ElectricWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

                    RemoveExtra1();
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Rolling Clay Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MegaConstructBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidMegaCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ScavBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

                    RemovePrimary();
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
                }

                if (hasQuirk = false)
                {
                    //Shiggymastercon.transformed = false;
                    Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
                }

            }

            if (base.IsKeyDownAuthority() && extrainputBankTest.extraSkill2.down)
            {
                if (newbodyPrefab.name == "VultureBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BeetleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BeetleGuardBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BisonBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "FlyingVerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BellBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayGrenadierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayBruiserBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GreaterWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spirit Ball Control Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "HermitCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ImpBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "JellyfishBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "AcidLarvaBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LemurianBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "WispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarExploderBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarGolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MiniMushroomBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ParentBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Glide Quirk</style> Get!");

                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidBarnacleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidJailerBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "NullifierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
                }


                if (newbodyPrefab.name == "BeetleQueen2Body")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GravekeeperBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VagrantBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MagmaWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ElectricWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

                    RemoveExtra2();
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Rolling Clay Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MegaConstructBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidMegaCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ScavBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

                    RemoveSecondary();
                    base.skillLocator.secondary.SetSkillOverride(base.skillLocator.secondary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
                }

                if (hasQuirk = false)
                {
                    //Shiggymastercon.transformed = false;
                    Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
                }
            }
            if (base.IsKeyDownAuthority() && extrainputBankTest.extraSkill3.down)
            {
                if (newbodyPrefab.name == "VultureBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BeetleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BeetleGuardBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BisonBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "FlyingVerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BellBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayGrenadierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayBruiserBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GreaterWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spirit Ball Control Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "HermitCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ImpBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "JellyfishBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "AcidLarvaBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LemurianBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "WispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarExploderBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarGolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MiniMushroomBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ParentBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Glide Quirk</style> Get!");

                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidBarnacleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidJailerBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "NullifierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
                }


                if (newbodyPrefab.name == "BeetleQueen2Body")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GravekeeperBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VagrantBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MagmaWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ElectricWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

                    RemoveExtra3();
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Rolling Clay Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MegaConstructBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidMegaCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ScavBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

                    RemoveUtility();
                    base.skillLocator.utility.SetSkillOverride(base.skillLocator.utility, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
                }

                if (hasQuirk = false)
                {
                    //Shiggymastercon.transformed = false;
                    Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
                }
            }
            if (base.IsKeyDownAuthority() && extrainputBankTest.extraSkill4.down)
            {
                if (newbodyPrefab.name == "VultureBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Barrier Quirk</style> Get!");

                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.alphacontructpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BeetleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Strength Boost Quirk</style> Get!");

                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.beetlepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BeetleGuardBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fast Drop Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BisonBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Charging Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "FlyingVerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Jump Boost Quirk</style> Get!");

                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.pestpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VerminBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Super Speed Quirk</style> Get!");

                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.verminpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BellBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiked Ball Control Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayGrenadierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay AirStrike Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayBruiserBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Clay Minigun Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spiky Body Quirk</style> Get!");

                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.guppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GreaterWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Spirit Ball Control Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "HermitCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Mortar Quirk</style> Get!");

                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.hermitcrabpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ImpBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blink Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "JellyfishBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nova Explosion Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "AcidLarvaBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Jump Quirk</style> Get!");

                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.larvapassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LemurianBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "WispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Ranged Boost Quirk</style> Get!");

                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.lesserwisppassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarExploderBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Aura Quirk</style> Get!");

                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.lunarexploderpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarGolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Slide Reset Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarWispBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lunar Minigun Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MiniMushroomBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Healing Aura Quirk</style> Get!");

                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.minimushrumpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ParentBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Teleport Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Glide Quirk</style> Get!");

                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.roboballminibpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GolemBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Laser Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidBarnacleBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Mortar Quirk</style> Get!");

                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.voidbarnaclepassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidJailerBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Gravity Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.voidjailerpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "NullifierBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Nullifier Artillery Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
                }


                if (newbodyPrefab.name == "BeetleQueen2Body")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Acid Shotgun Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "TitanBody" | newbodyPrefab.name == "TitanGoldBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Stone Skin Quirk</style> Get!");

                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GravekeeperBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Hook Shotgun Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VagrantBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Vagrant's Orb Quirk</style> Get!");

                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MagmaWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Blazing Aura Quirk</style> Get!");

                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ElectricWormBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Lightning Aura Quirk</style> Get!");

                    RemoveExtra4();
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Rolling Clay Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallBossBody" | newbodyPrefab.name == "SuperRoboBallBossBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Anti Gravity Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MegaConstructBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Beam Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidMegaCrabBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Void Missiles Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ScavBody")
                {

                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Throw Thqwibs Quirk</style> Get!");

                    RemoveSpecial();
                    base.skillLocator.special.SetSkillOverride(base.skillLocator.special, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);
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
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

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
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

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
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

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
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.stonetitanpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.magmawormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.overloadingwormpassiveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.vagrantpassiveDef, GenericSkill.SkillOverridePriority.Contextual);

        }

        public void RemovePrimary()
        {
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);

        }
        public void RemoveSecondary()
        {
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.secondary.UnsetSkillOverride(base.skillLocator.secondary, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);

        }

        public void RemoveUtility()
        {
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);

        }

        public void RemoveSpecial()
        {
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.beetleguardslamDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.bisonchargeDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.bronzongballDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.clayapothecarymortarDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.claytemplarminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.greaterwispballDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.impblinkDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.jellyfishnovaDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.lunargolemslideDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.lunarwispminigunDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.parentteleportDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.stonegolemlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.voidreaverportalDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.beetlequeenshotgunDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.grovetenderhookDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.claydunestriderballDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.soluscontrolunityknockupDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.xiconstructbeamDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.voiddevastatorhomingDef, GenericSkill.SkillOverridePriority.Contextual);
            base.skillLocator.special.UnsetSkillOverride(base.skillLocator.special, Shiggy.scavengerthqwibDef, GenericSkill.SkillOverridePriority.Contextual);

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