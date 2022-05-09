using EntityStates;
using ExtraSkillSlots;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShiggyMod.Modules.Survivors
{
    [RequireComponent(typeof(CharacterBody))]
    [RequireComponent(typeof(TeamComponent))]
    [RequireComponent(typeof(InputBankTest))]
    public class ShiggyMasterController : MonoBehaviour
	{
		string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";

		public ShiggyController Shiggycon;
        public ShiggyMasterController Shiggymastercon;
        private CharacterMaster characterMaster;
        private CharacterBody characterBody;


		private ExtraSkillLocator extraskillLocator;
		public bool alphacontructpassiveDef;
		public bool beetlepassiveDef;
		public bool pestpassiveDef;
		public bool verminpassiveDef;
		public bool guppassiveDef;
		public bool hermitcrabpassiveDef;
		public bool larvapassiveDef;
		public bool lesserwisppassiveDef;
		public bool lunarexploderpassiveDef;
		public bool minimushrumpassiveDef;
		public bool roboballminibpassiveDef;
		public bool voidbarnaclepassiveDef;
		public bool voidjailerpassiveDef;
		public bool stonetitanpassiveDef;
		public bool magmawormpassiveDef;
		public bool overloadingwormpassiveDef;

		public bool alloyvultureflyDef;
		public bool beetleguardslamDef;
		public bool bisonchargeDef;
		public bool bronzongballDef;
		public bool clayapothecarymortarDef;
		public bool claytemplarminigunDef;
		public bool greaterwispballDef;
		public bool impblinkDef;
		public bool jellyfishnovaDef;
		public bool lemurianfireballDef;
		public bool lunargolemshotsDef;
		public bool lunarwispminigunDef;
		public bool parentteleportDef;
		public bool stonegolemlaserDef;
		public bool voidreaverportalDef;
		public bool beetlequeenshotgunDef;
		public bool grovetenderhookDef;
		public bool claydunestriderballDef;
		public bool soluscontrolunityknockupDef;
		public bool xiconstructbeamDef;
		public bool voiddevastatorhomingDef;
		public bool scavengerthqwibDef;

		public bool primarygiven;
		public bool secondarygiven;
		public bool utilitygiven;
		public bool specialgiven;
		public bool extra1given;
		public bool extra2given;
		public bool extra3given;
		public bool extra4given;

		public SkillDef[] skillListToOverrideOnRespawn;

		private void Awake()
        {

			On.RoR2.CharacterBody.Start += CharacterBody_Start;
            On.RoR2.Run.Start += Run_Start;
			skillListToOverrideOnRespawn = new SkillDef[8];
			alphacontructpassiveDef = false;
			beetlepassiveDef = false;
			pestpassiveDef = false;
			verminpassiveDef = false;
			guppassiveDef = false;
			hermitcrabpassiveDef = false;
			larvapassiveDef = false;
			lesserwisppassiveDef = false;
			lunarexploderpassiveDef = false;
			minimushrumpassiveDef = false;
			roboballminibpassiveDef = false;
			voidbarnaclepassiveDef = false;
			voidjailerpassiveDef = false;

			stonetitanpassiveDef = false;
			magmawormpassiveDef = false;
			overloadingwormpassiveDef = false;


			alloyvultureflyDef = false;
			beetleguardslamDef = false;
			bisonchargeDef = false;
			bronzongballDef = false;
			clayapothecarymortarDef = false;
			claytemplarminigunDef = false;
			greaterwispballDef = false;
			impblinkDef = false;
			jellyfishnovaDef = false;
			lemurianfireballDef = false;
			lunargolemshotsDef = false;
			lunarwispminigunDef = false;
			parentteleportDef = false;
			stonegolemlaserDef = false;
			voidreaverportalDef = false;

			beetlequeenshotgunDef = false;
			grovetenderhookDef = false;
			claydunestriderballDef = false;
			soluscontrolunityknockupDef = false;
			xiconstructbeamDef = false;
			voiddevastatorhomingDef = false;
			scavengerthqwibDef = false;

			primarygiven = false;
			secondarygiven = false;
			utilitygiven = false;
			specialgiven = false;
			extra1given = false;
			extra2given = false;
			extra3given = false;
			extra4given = false;
		}

        private void Run_Start(On.RoR2.Run.orig_Start orig, Run self)
        {
			orig.Invoke(self);

			Shiggymastercon.writeToSkillList(null, 0);
			Shiggymastercon.writeToSkillList(null, 1);
			Shiggymastercon.writeToSkillList(null, 2);
			Shiggymastercon.writeToSkillList(null, 3);
			Shiggymastercon.writeToSkillList(null, 4);
			Shiggymastercon.writeToSkillList(null, 5);
			Shiggymastercon.writeToSkillList(null, 6);
			Shiggymastercon.writeToSkillList(null, 7);
		}

        public void writeToSkillList(SkillDef skillDef, int index)
		{
			skillListToOverrideOnRespawn[index] = skillDef;
		}

		private void Start()
        {
            characterMaster = gameObject.GetComponent<CharacterMaster>();
			characterBody = characterMaster.GetBody();

            Shiggymastercon = characterMaster.gameObject.GetComponent<ShiggyMasterController>();
            Shiggycon = characterBody.gameObject.GetComponent<ShiggyController>();

			extraskillLocator = characterBody.gameObject.GetComponent<ExtraSkillLocator>();
			alphacontructpassiveDef = false;
			beetlepassiveDef = false;
			pestpassiveDef = false;
			verminpassiveDef = false;
			guppassiveDef = false;
			hermitcrabpassiveDef = false;
			larvapassiveDef = false;
			lesserwisppassiveDef = false;
			lunarexploderpassiveDef = false;
			minimushrumpassiveDef = false;
			roboballminibpassiveDef = false;
			voidbarnaclepassiveDef = false;
			voidjailerpassiveDef = false;

			stonetitanpassiveDef = false;
			magmawormpassiveDef = false;
			overloadingwormpassiveDef = false;


			alloyvultureflyDef = false;
			beetleguardslamDef = false;
			bisonchargeDef = false;
			bronzongballDef = false;
			clayapothecarymortarDef = false;
			claytemplarminigunDef = false;
			greaterwispballDef = false;
			impblinkDef = false;
			jellyfishnovaDef = false;
			lemurianfireballDef = false;
			lunargolemshotsDef = false;
			lunarwispminigunDef = false;
			parentteleportDef = false;
			stonegolemlaserDef = false;
			voidreaverportalDef = false;

			beetlequeenshotgunDef = false;
			grovetenderhookDef = false;
			claydunestriderballDef = false;
			soluscontrolunityknockupDef = false;
			xiconstructbeamDef = false;
			voiddevastatorhomingDef = false;
			scavengerthqwibDef = false;
		}


        private void CharacterBody_Start(On.RoR2.CharacterBody.orig_Start orig, CharacterBody self)
        {
            orig.Invoke(self);


            if (self.master.gameObject.GetComponent<ShiggyMasterController>())
            {
                if (self.master.bodyPrefab == BodyCatalog.FindBodyPrefab("ShiggyBody"))
                {
					extraskillLocator = self.gameObject.GetComponent<ExtraSkillLocator>();
					Shiggycon.alphacontructpassiveDef = false;
					Shiggycon.beetlepassiveDef = false;
					Shiggycon.pestpassiveDef = false;
					Shiggycon.verminpassiveDef = false;
					Shiggycon.guppassiveDef = false;
					Shiggycon.hermitcrabpassiveDef = false;
					Shiggycon.larvapassiveDef = false;
					Shiggycon.lesserwisppassiveDef = false;
					Shiggycon.lunarexploderpassiveDef = false;
					Shiggycon.minimushrumpassiveDef = false;
					Shiggycon.roboballminibpassiveDef = false;
					Shiggycon.voidbarnaclepassiveDef = false;
					Shiggycon.voidjailerpassiveDef = false;

					Shiggycon.stonetitanpassiveDef = false;
					Shiggycon.magmawormpassiveDef = false;
					Shiggycon.overloadingwormpassiveDef = false;


					Shiggycon.alloyvultureflyDef = false;
					Shiggycon.beetleguardslamDef = false;
					Shiggycon.bisonchargeDef = false;
					Shiggycon.bronzongballDef = false;
					Shiggycon.clayapothecarymortarDef = false;
					Shiggycon.claytemplarminigunDef = false;
					Shiggycon.greaterwispballDef = false;
					Shiggycon.impblinkDef = false;
					Shiggycon.jellyfishnovaDef = false;
					Shiggycon.lemurianfireballDef = false;
					Shiggycon.lunargolemshotsDef = false;
					Shiggycon.lunarwispminigunDef = false;
					Shiggycon.parentteleportDef = false;
					Shiggycon.stonegolemlaserDef = false;
					Shiggycon.voidreaverportalDef = false;

					Shiggycon.beetlequeenshotgunDef = false;
					Shiggycon.grovetenderhookDef = false;
					Shiggycon.claydunestriderballDef = false;
					Shiggycon.soluscontrolunityknockupDef = false;
					Shiggycon.xiconstructbeamDef = false;
					Shiggycon.voiddevastatorhomingDef = false;
					Shiggycon.scavengerthqwibDef = false;


					primarygiven = false;
					secondarygiven = false;
					utilitygiven = false;
					specialgiven = false;
					extra1given = false;
					extra2given = false;
					extra3given = false;
					extra4given = false;


                }


            }

        }


        private void CharacterModel_Awake(On.RoR2.CharacterModel.orig_Awake orig, CharacterModel self)
        {
            orig(self);
            if (self.gameObject.name.Contains("ShiggyDisplay"))
            {
				//reset bools
				alphacontructpassiveDef = false;
				beetlepassiveDef = false;
				pestpassiveDef = false;
				verminpassiveDef = false;
				guppassiveDef = false;
				hermitcrabpassiveDef = false;
				larvapassiveDef = false;
				lesserwisppassiveDef = false;
				lunarexploderpassiveDef = false;
				minimushrumpassiveDef = false;
				roboballminibpassiveDef = false;
				voidbarnaclepassiveDef = false;
				voidjailerpassiveDef = false;

				stonetitanpassiveDef = false;
				magmawormpassiveDef = false;
				overloadingwormpassiveDef = false;


				alloyvultureflyDef = false;
				beetleguardslamDef = false;
				bisonchargeDef = false;
				bronzongballDef = false;
				clayapothecarymortarDef = false;
				claytemplarminigunDef = false;
				greaterwispballDef = false;
				impblinkDef = false;
				jellyfishnovaDef = false;
				lemurianfireballDef = false;
				lunargolemshotsDef = false;
				lunarwispminigunDef = false;
				parentteleportDef = false;
				stonegolemlaserDef = false;
				voidreaverportalDef = false;

				beetlequeenshotgunDef = false;
				grovetenderhookDef = false;
				claydunestriderballDef = false;
				soluscontrolunityknockupDef = false;
				xiconstructbeamDef = false;
				voiddevastatorhomingDef = false;
				scavengerthqwibDef = false;

			}

        }


        private void FixedUpdate()
        {
            characterMaster = gameObject.GetComponent<CharacterMaster>();
            CharacterBody self = characterMaster.GetBody();

			//update skill list to retain
			

			//add skills next stage/respawn
			if (Config.retainLoadout.Value)
			{
				if (skillListToOverrideOnRespawn[0] != null && !primarygiven)
				{
					primarygiven = true;
					self.skillLocator.primary.SetSkillOverride(self.skillLocator.primary, skillListToOverrideOnRespawn[0], GenericSkill.SkillOverridePriority.Contextual);
				}
				if (skillListToOverrideOnRespawn[1] != null && !secondarygiven)
				{
					secondarygiven = true;
					self.skillLocator.secondary.SetSkillOverride(self.skillLocator.secondary, skillListToOverrideOnRespawn[1], GenericSkill.SkillOverridePriority.Contextual);
				}
				if (skillListToOverrideOnRespawn[2] != null && !utilitygiven)
				{
					utilitygiven = true;
					self.skillLocator.utility.SetSkillOverride(self.skillLocator.utility, skillListToOverrideOnRespawn[2], GenericSkill.SkillOverridePriority.Contextual);
				}
				if (skillListToOverrideOnRespawn[3] != null && !specialgiven)
				{
					specialgiven = true;
					self.skillLocator.special.SetSkillOverride(self.skillLocator.special, skillListToOverrideOnRespawn[3], GenericSkill.SkillOverridePriority.Contextual);
				}
				if (skillListToOverrideOnRespawn[4] != null && !extra1given)
				{
					extra1given = true;
					extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, skillListToOverrideOnRespawn[4], GenericSkill.SkillOverridePriority.Contextual);
				}
				if (skillListToOverrideOnRespawn[5] != null && !extra2given)
				{
					extra2given = true;
					extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, skillListToOverrideOnRespawn[5], GenericSkill.SkillOverridePriority.Contextual);
				}
				if (skillListToOverrideOnRespawn[6] != null && !extra3given)
				{
					extra3given = true;
					extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, skillListToOverrideOnRespawn[6], GenericSkill.SkillOverridePriority.Contextual);
				}
				if (skillListToOverrideOnRespawn[7] != null && !extra4given)
				{
					extra4given = true;
					extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, skillListToOverrideOnRespawn[7], GenericSkill.SkillOverridePriority.Contextual);
				}

			}


		}

        private void Update()
        {
            CheckQuirks();
        }
        public void CheckQuirks()
        {
            //check passive 1
            if (extraskillLocator.extraFirst.skillNameToken == prefix + "ALPHACONSTRUCT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 4);
            }
            if (extraskillLocator.extraFirst.skillNameToken == prefix + "BEETLE_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 4);
            }
            if (extraskillLocator.extraFirst.skillNameToken == prefix + "GUP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 4);
            }
            if (extraskillLocator.extraFirst.skillNameToken == prefix + "LARVA_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 4);
            }
            if (extraskillLocator.extraFirst.skillNameToken == prefix + "LESSERWISP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 4);
            }
            if (extraskillLocator.extraFirst.skillNameToken == prefix + "LUNAREXPLODER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 4);
            }
            if (extraskillLocator.extraFirst.skillNameToken == prefix + "HERMITCRAB_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 4);
            }
            if (extraskillLocator.extraFirst.skillNameToken == prefix + "PEST_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 4);
            }
            if (extraskillLocator.extraFirst.skillNameToken == prefix + "VERMIN_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 4);
            }
            if (extraskillLocator.extraFirst.skillNameToken == prefix + "MINIMUSHRUM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 4);
            }
            if (extraskillLocator.extraFirst.skillNameToken == prefix + "ROBOBALLMINI_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 4);
            }
            if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDBARNACLE_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 4);
            }
            if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDJAILER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 4);
            }
            if (extraskillLocator.extraFirst.skillNameToken == prefix + "STONETITAN_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 4);
            }
            if (extraskillLocator.extraFirst.skillNameToken == prefix + "MAGMAWORM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 4);
            }
            if (extraskillLocator.extraFirst.skillNameToken == prefix + "OVERLOADINGWORM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 4);
            }
            if (extraskillLocator.extraFirst.skillNameToken == prefix + "VAGRANT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 4);
            }

            //check active 1
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "VULTURE_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 0);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEGUARD_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "BRONZONG_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "APOTHECARY_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "TEMPLAR_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "GREATERWISP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "IMP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "JELLYFISH_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.jellyfishnovaDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "LEMURIAN_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARGOLEM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARWISP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "PARENT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "STONEGOLEM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDREAVER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEQUEEN_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "GROVETENDER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "XICONSTRUCT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 4);
            }
            if (characterBody.skillLocator.primary.skillNameToken == prefix + "SCAVENGER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 4);
            }

            //check passive 2
            if (extraskillLocator.extraSecond.skillNameToken == prefix + "ALPHACONSTRUCT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 5);
            }
            if (extraskillLocator.extraSecond.skillNameToken == prefix + "BEETLE_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 5);
            }
            if (extraskillLocator.extraSecond.skillNameToken == prefix + "GUP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 5);
            }
            if (extraskillLocator.extraSecond.skillNameToken == prefix + "LARVA_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 5);
            }
            if (extraskillLocator.extraSecond.skillNameToken == prefix + "LESSERWISP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 5);
            }
            if (extraskillLocator.extraSecond.skillNameToken == prefix + "LUNAREXPLODER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 5);
            }
            if (extraskillLocator.extraSecond.skillNameToken == prefix + "HERMITCRAB_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 5);
            }
            if (extraskillLocator.extraSecond.skillNameToken == prefix + "PEST_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 5);
            }
            if (extraskillLocator.extraSecond.skillNameToken == prefix + "VERMIN_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 5);
            }
            if (extraskillLocator.extraSecond.skillNameToken == prefix + "MINIMUSHRUM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 5);
            }
            if (extraskillLocator.extraSecond.skillNameToken == prefix + "ROBOBALLMINI_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 5);
            }
            if (extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDBARNACLE_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 5);
            }
            if (extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDJAILER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 5);
            }
            if (extraskillLocator.extraSecond.skillNameToken == prefix + "STONETITAN_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 5);
            }
            if (extraskillLocator.extraSecond.skillNameToken == prefix + "MAGMAWORM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 5);
            }
            if (extraskillLocator.extraSecond.skillNameToken == prefix + "OVERLOADINGWORM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 5);
            }
            if (extraskillLocator.extraSecond.skillNameToken == prefix + "VAGRANT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 5);
            }

            //check active 2
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "VULTURE_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLEGUARD_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "BRONZONG_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "APOTHECARY_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "TEMPLAR_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "GREATERWISP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "IMP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "JELLYFISH_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.jellyfishnovaDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "LEMURIAN_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARGOLEM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARWISP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "PARENT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "STONEGOLEM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDREAVER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLEQUEEN_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "GROVETENDER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "XICONSTRUCT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 2);
            }
            if (characterBody.skillLocator.secondary.skillNameToken == prefix + "SCAVENGER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 2);
            }

            //check passive 3
            if (extraskillLocator.extraThird.skillNameToken == prefix + "ALPHACONSTRUCT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 6);
            }
            if (extraskillLocator.extraThird.skillNameToken == prefix + "BEETLE_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 6);
            }
            if (extraskillLocator.extraThird.skillNameToken == prefix + "GUP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 6);
            }
            if (extraskillLocator.extraThird.skillNameToken == prefix + "LARVA_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 6);
            }
            if (extraskillLocator.extraThird.skillNameToken == prefix + "LESSERWISP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 6);
            }
            if (extraskillLocator.extraThird.skillNameToken == prefix + "LUNAREXPLODER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 6);
            }
            if (extraskillLocator.extraThird.skillNameToken == prefix + "HERMITCRAB_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 6);
            }
            if (extraskillLocator.extraThird.skillNameToken == prefix + "PEST_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 6);
            }
            if (extraskillLocator.extraThird.skillNameToken == prefix + "VERMIN_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 6);
            }
            if (extraskillLocator.extraThird.skillNameToken == prefix + "MINIMUSHRUM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 6);
            }
            if (extraskillLocator.extraThird.skillNameToken == prefix + "ROBOBALLMINI_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 6);
            }
            if (extraskillLocator.extraThird.skillNameToken == prefix + "VOIDBARNACLE_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 6);
            }
            if (extraskillLocator.extraThird.skillNameToken == prefix + "VOIDJAILER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 6);
            }
            if (extraskillLocator.extraThird.skillNameToken == prefix + "STONETITAN_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 6);
            }
            if (extraskillLocator.extraThird.skillNameToken == prefix + "MAGMAWORM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 6);
            }
            if (extraskillLocator.extraThird.skillNameToken == prefix + "OVERLOADINGWORM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 6);
            }
            if (extraskillLocator.extraThird.skillNameToken == prefix + "VAGRANT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 6);
            }

            //check active 3
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "VULTURE_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLEGUARD_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "BRONZONG_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "APOTHECARY_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "TEMPLAR_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "GREATERWISP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "IMP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "JELLYFISH_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.jellyfishnovaDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "LEMURIAN_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARGOLEM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARWISP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "PARENT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "STONEGOLEM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDREAVER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLEQUEEN_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "GROVETENDER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "XICONSTRUCT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDDEVASTATOR_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 3);
            }
            if (characterBody.skillLocator.utility.skillNameToken == prefix + "SCAVENGER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 3);
            }
            //check passive 4
            if (extraskillLocator.extraFourth.skillNameToken == prefix + "ALPHACONSTRUCT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 7);
            }
            if (extraskillLocator.extraFourth.skillNameToken == prefix + "BEETLE_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 7);
            }
            if (extraskillLocator.extraFourth.skillNameToken == prefix + "GUP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 7);
            }
            if (extraskillLocator.extraFourth.skillNameToken == prefix + "LARVA_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 7);
            }
            if (extraskillLocator.extraFourth.skillNameToken == prefix + "LESSERWISP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 7);
            }
            if (extraskillLocator.extraFourth.skillNameToken == prefix + "LUNAREXPLODER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 7);
            }
            if (extraskillLocator.extraFourth.skillNameToken == prefix + "HERMITCRAB_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 7);
            }
            if (extraskillLocator.extraFourth.skillNameToken == prefix + "PEST_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 7);
            }
            if (extraskillLocator.extraFourth.skillNameToken == prefix + "VERMIN_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 7);
            }
            if (extraskillLocator.extraFourth.skillNameToken == prefix + "MINIMUSHRUM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 7);
            }
            if (extraskillLocator.extraFourth.skillNameToken == prefix + "ROBOBALLMINI_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 7);
            }
            if (extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDBARNACLE_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.voidbarnaclepassiveDef, 7);
            }
            if (extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDJAILER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 7);
            }
            if (extraskillLocator.extraFourth.skillNameToken == prefix + "STONETITAN_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 7);
            }
            if (extraskillLocator.extraFourth.skillNameToken == prefix + "MAGMAWORM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 7);
            }
            if (extraskillLocator.extraFourth.skillNameToken == prefix + "OVERLOADINGWORM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 7);
            }
            if (extraskillLocator.extraFourth.skillNameToken == prefix + "VAGRANT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 7);
            }

            //check active 4
            if (characterBody.skillLocator.special.skillNameToken == prefix + "VULTURE_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.alloyvultureflyDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "BEETLEGUARD_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.beetleguardslamDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "BRONZONG_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.bronzongballDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "APOTHECARY_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.clayapothecarymortarDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "TEMPLAR_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.claytemplarminigunDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "GREATERWISP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.greaterwispballDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "IMP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.impblinkDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "JELLYFISH_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.jellyfishnovaDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "LEMURIAN_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lemurianfireballDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "LUNARGOLEM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lunargolemslideDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "LUNARWISP_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.lunarwispminigunDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "PARENT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.parentteleportDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "STONEGOLEM_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.stonegolemlaserDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "VOIDREAVER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.voidreaverportalDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "BEETLEQUEEN_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.beetlequeenshotgunDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "GROVETENDER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.grovetenderhookDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.claydunestriderballDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.soluscontrolunityknockupDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "XICONSTRUCT_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.xiconstructbeamDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "VOIDDEVASTATOR_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.voiddevastatorhomingDef, 3);
            }
            if (characterBody.skillLocator.special.skillNameToken == prefix + "SCAVENGER_NAME")
            {
                Shiggymastercon.writeToSkillList(Shiggy.scavengerthqwibDef, 3);
            }
        }
    }
}
