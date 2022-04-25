using ShiggyMod.Modules.Survivors;
using EntityStates;
using RoR2;
using UnityEngine;
using System.Collections.Generic;
using ShiggyMod.Modules;
using UnityEngine.Networking;
using RoR2.ExpansionManagement;

namespace ShiggyMod.SkillStates
{
    public class AFOPrimary : BaseSkillState
    {

        private float duration = 1f;
        private float fireTime = 0.2f;
        private bool hasFired;
        public ShiggyController Shiggycon;
        public ShiggyMasterController Shiggymastercon;
        public HurtBox Target;


        public override void OnEnter()
        {
            base.OnEnter();


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
                    CheckBody(Target);
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


        private void CheckBody(HurtBox hurtBox)
        {

        }


        private void StealQuirk(HurtBox hurtBox)
        {
            //List<string> blacklist = new List<string>();
            //blacklist.Add("DroneCommanderBody");
            //blacklist.Add("ExplosivePotDestructibleBody");
            //blacklist.Add("SulfurPodBody");
            //blacklist.Add("ShiggyBody");
            //blacklist.Add("AffixEarthHealerBody");
            //blacklist.Add("MinorConstructAttachableBody");
            //blacklist.Add("ClayGrenadierBody");
            //blacklist.Add("SMMaulingRockLarge");
            //blacklist.Add("SMMaulingRockMedium");
            //blacklist.Add("SMMaulingRockSmall");
            //blacklist.Add("VultureEggBody");

            //List<string> speciallist = new List<string>();
            //speciallist.Add("NullifierBody");
            //speciallist.Add("VoidJailerBody");
            //speciallist.Add("MinorConstructBody");
            //speciallist.Add("MinorConstructOnKillBody");
            //speciallist.Add("MiniVoidRaidCrabBodyPhase1");
            //speciallist.Add("MiniVoidRaidCrabBodyPhase2");
            //speciallist.Add("MiniVoidRaidCrabBodyPhase3");
            //speciallist.Add("ElectricWormBody");
            //speciallist.Add("MagmaWormBody");
            //speciallist.Add("BeetleQueen2Body");
            //speciallist.Add("TitanBody");
            //speciallist.Add("TitanGoldBody");
            //speciallist.Add("VagrantBody");
            //speciallist.Add("GravekeeperBody");
            //speciallist.Add("ClayBossBody");
            //speciallist.Add("RoboBallBossBody");
            //speciallist.Add("SuperRoboBallBossBody");
            //speciallist.Add("MegaConstructBody");
            //speciallist.Add("VoidInfestorBody");
            //speciallist.Add("VoidBarnacleBody");
            //speciallist.Add("MegaConstructBody");
            //speciallist.Add("VoidMegaCrabBody");
            //speciallist.Add("GrandParentBody");
            //speciallist.Add("ImpBossBody");
            //speciallist.Add("BrotherBody");
            //speciallist.Add("BrotherHurtBody");
            //speciallist.Add("ScavBody");


            var name = BodyCatalog.GetBodyName(hurtBox.healthComponent.body.bodyIndex);
            GameObject newbodyPrefab = BodyCatalog.FindBodyPrefab(name);

            if (newbodyPrefab.name == "MinorConstructBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");
                Shiggymastercon.alphaconstructQuirk = true;
                base.characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
            }
            if (newbodyPrefab.name == "VultureBody")
            {
                Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.vultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "BeetleBody")
            {
                Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

                Shiggymastercon.alphaconstructQuirk = true;
                base.characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "BeetleGuardBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "BisonBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "FlyingVerminBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "VerminBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "BellBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "ClayGrenadierBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "ClayBruiserBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "GupBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "GipBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "GeepBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "GreaterWispBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "HermitCrabBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "ImpBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "JellyfishBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "AcidLarvaBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "LemurianBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "WispBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "LunarExploderBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "LunarGolemBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "LunarWispBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "MiniMushroomBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "ParentBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "RoboBallMiniBody" | newbodyPrefab.name == "RoboBallGreenBuddyBody" | newbodyPrefab.name == "RoboBallRedBuddyBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "GolemBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "VoidBarnacleBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "VoidJailerBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "NullifierBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }

            if (newbodyPrefab.name == "BeetleQueen2Body")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "TitanBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "TitanGoldBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "GravekeeperBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "VagrantBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "MagmaWormBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "ElectricWormBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "ClayBossBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "RoboBallBossBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "SuperRoboBallBossBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "MegaConstructBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "VoidMegaCrabBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "ScavBody")
            {
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }

            else
            {
                Shiggymastercon.transformed = false;
                Chat.AddMessage("No Quirk to <style=cIsUtility>Steal!</style>");
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