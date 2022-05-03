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
    public class AFOPrimary : BaseSkillState
    {

        private float duration = 1f;
        private float fireTime = 0.2f;
        private bool hasFired;
        private bool hasQuirk;
        public ShiggyController Shiggycon;
        public ShiggyMasterController Shiggymastercon;
        public HurtBox Target;


        public override void OnEnter()
        {
            base.OnEnter();
            hasQuirk = false;

            if (NetworkServer.active)
            {
                base.characterBody.AddBuff(Modules.Buffs.hermitcrabmortarBuff);
                base.characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarBuff);

            }

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

                var name = BodyCatalog.GetBodyName(hurtBox.healthComponent.body.bodyIndex);
                GameObject newbodyPrefab = BodyCatalog.FindBodyPrefab(name);

                ExtraSkillLocator extraskillLocator = base.GetComponent<ExtraSkillLocator>();

                if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");
                    Shiggymastercon.alphaconstructQuirk = true;
                    base.characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VultureBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BeetleBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

                    Shiggymastercon.alphaconstructQuirk = true;
                    base.characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.alloyvultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BeetleGuardBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BisonBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "FlyingVerminBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VerminBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "BellBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayGrenadierBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayBruiserBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GupBody" | newbodyPrefab.name == "GipBody" | newbodyPrefab.name == "GeepBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GreaterWispBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "HermitCrabBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ImpBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "JellyfishBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "AcidLarvaBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LemurianBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");
                    base.characterBody.AddBuff(Modules.Buffs.spikeBuff);

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "WispBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarExploderBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarGolemBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "LunarWispBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MiniMushroomBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ParentBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");
                                        
                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GolemBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidBarnacleBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidJailerBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "NullifierBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }

                if (newbodyPrefab.name == "BeetleQueen2Body")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "TitanBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "TitanGoldBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "GravekeeperBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VagrantBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MagmaWormBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ElectricWormBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ClayBossBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "RoboBallBossBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "SuperRoboBallBossBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "MegaConstructBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "VoidMegaCrabBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (newbodyPrefab.name == "ScavBody")
                {
                    hasQuirk = true;
                    Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                    base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
                }

                if (hasQuirk = false)
                {
                    //Shiggymastercon.transformed = false;
                    Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
                }

            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
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