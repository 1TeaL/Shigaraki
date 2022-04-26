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
        private bool hasQuirk;
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
            //List<string> quirklist = new List<string>();
            //quirklist.Add("NullifierBody");
            //quirklist.Add("VoidJailerBody");
            //quirklist.Add("MinorConstructBody");
            //quirklist.Add("MinorConstructOnKillBody");
            //quirklist.Add("ElectricWormBody");
            //quirklist.Add("MagmaWormBody");
            //quirklist.Add("BeetleQueen2Body");
            //quirklist.Add("TitanBody");
            //quirklist.Add("TitanGoldBody");
            //quirklist.Add("VagrantBody");
            //quirklist.Add("GravekeeperBody");
            //quirklist.Add("ClayBossBody");
            //quirklist.Add("RoboBallBossBody");
            //quirklist.Add("SuperRoboBallBossBody");
            //quirklist.Add("MegaConstructBody");
            //quirklist.Add("VoidInfestorBody");
            //quirklist.Add("VoidBarnacleBody");
            //quirklist.Add("MegaConstructBody");
            //quirklist.Add("VoidMegaCrabBody");
            //quirklist.Add("GrandParentBody");
            //quirklist.Add("ImpBossBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("VultureBody");
            //quirklist.Add("BeetleBody");
            //quirklist.Add("BeetleGuardBody");
            //quirklist.Add("BisonBody");
            //quirklist.Add("FlyingVerminBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");
            //quirklist.Add("ScavBody");


            var name = BodyCatalog.GetBodyName(hurtBox.healthComponent.body.bodyIndex);
            GameObject newbodyPrefab = BodyCatalog.FindBodyPrefab(name);

            if (newbodyPrefab.name == "MinorConstructBody" | newbodyPrefab.name == "MinorConstructOnKillBody")
            {
                hasQuirk = true;
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");
                Shiggymastercon.alphaconstructQuirk = true;
                base.characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
            }
            if (newbodyPrefab.name == "VultureBody")
            {
                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Flight Quirk</style> Get!");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.vultureflyDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "BeetleBody")
            {
                hasQuirk = true;
                Chat.AddMessage("<style=cIsUtility>Fireball Quirk</style> Get!");

                Shiggymastercon.alphaconstructQuirk = true;
                base.characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
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
            if (newbodyPrefab.name == "GupBody")
            {
                hasQuirk = true;
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "GipBody")
            {
                hasQuirk = true;
                Chat.AddMessage("Stole Lemurian's <style=cIsUtility>Quirk!</style>");

                base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, Shiggy.lemurianfireballDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (newbodyPrefab.name == "GeepBody")
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