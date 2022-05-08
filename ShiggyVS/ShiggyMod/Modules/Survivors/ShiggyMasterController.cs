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

		//public void CheckQuirks()
		//{
		//	//check passive 1
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "ALPHACONSTRUCT_NAME")
		//	{
		//		Shiggymastercon.writeToSkillList(Shiggy.alphacontructpassiveDef, 4);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "BEETLE_NAME")
		//	{
		//		Shiggymastercon.writeToSkillList(Shiggy.beetlepassiveDef, 4);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "GUP_NAME")
		//	{
		//		Shiggymastercon.writeToSkillList(Shiggy.guppassiveDef, 4);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "LARVA_NAME")
		//	{
		//		Shiggymastercon.writeToSkillList(Shiggy.larvapassiveDef, 4);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "LESSERWISP_NAME")
		//	{
		//		Shiggymastercon.writeToSkillList(Shiggy.lesserwisppassiveDef, 4);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "LUNAREXPLODER_NAME")
		//	{
		//		Shiggymastercon.writeToSkillList(Shiggy.lunarexploderpassiveDef, 4);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "HERMITCRAB_NAME")
		//	{
		//		Shiggymastercon.writeToSkillList(Shiggy.hermitcrabpassiveDef, 4);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "PEST_NAME")
		//	{
		//		Shiggymastercon.writeToSkillList(Shiggy.pestpassiveDef, 4);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "VERMIN_NAME")
		//	{
		//		Shiggymastercon.writeToSkillList(Shiggy.verminpassiveDef, 4);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "MINIMUSHRUM_NAME")
		//	{
		//		Shiggymastercon.writeToSkillList(Shiggy.minimushrumpassiveDef, 4);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "ROBOBALLMINI_NAME")
		//	{
		//		Shiggymastercon.writeToSkillList(Shiggy.roboballminibpassiveDef, 4);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDBARNACLE_NAME")
		//	{
		//		Shiggymastercon.writeToSkillList(Shiggy.voidjailerpassiveDef, 4);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDJAILER_NAME")
		//	{
		//		Shiggymastercon.writeToSkillList(Shiggy.stonetitanpassiveDef, 4);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "STONETITAN_NAME")
		//	{
		//		Shiggymastercon.writeToSkillList(Shiggy.magmawormpassiveDef, 4);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "MAGMAWORM_NAME")
		//	{
		//		Shiggymastercon.writeToSkillList(Shiggy.overloadingwormpassiveDef, 4);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "OVERLOADINGWORM_NAME")
		//	{
		//		Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 4);
		//	}

		//	//check active
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "VULTURE_NAME")
		//	{
		//		Shiggymastercon.writeToSkillList(Shiggy.vagrantpassiveDef, 0);
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEGUARD_NAME")
		//	{
		//		if (!beetleguardslamDef)
		//		{
		//			beetleguardslamDef = true;
		//		}
		//	}
		//	else
		//	{
		//		beetleguardslamDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "BRONZONG_NAME")
		//	{
		//		if (!bronzongballDef)
		//		{
		//			bronzongballDef = true;
		//		}
		//	}
		//	else
		//	{
		//		bronzongballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "APOTHECARY_NAME")
		//	{
		//		if (!clayapothecarymortarDef)
		//		{
		//			clayapothecarymortarDef = true;
		//		}
		//	}
		//	else
		//	{
		//		clayapothecarymortarDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "TEMPLAR_NAME")
		//	{
		//		if (!claytemplarminigunDef)
		//		{
		//			claytemplarminigunDef = true;
		//		}
		//	}
		//	else
		//	{
		//		claytemplarminigunDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "GREATERWISP_NAME")
		//	{
		//		if (!greaterwispballDef)
		//		{
		//			greaterwispballDef = true;
		//		}
		//	}
		//	else
		//	{
		//		greaterwispballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "IMP_NAME")
		//	{
		//		if (!impblinkDef)
		//		{
		//			impblinkDef = true;
		//		}
		//	}
		//	else
		//	{
		//		impblinkDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "JELLYFISH_NAME")
		//	{
		//		if (!jellyfishnovaDef)
		//		{
		//			jellyfishnovaDef = true;
		//		}
		//	}	
		//	else
		//	{
		//		jellyfishnovaDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "LEMURIAN_NAME")
		//	{
		//		if (!lemurianfireballDef)
		//		{
		//			lemurianfireballDef = true;
		//		}
		//	}
		//	else
		//	{
		//		lemurianfireballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARGOLEM_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARGOLEM_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARGOLEM_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "LUNARGOLEM_NAME")
		//	{
		//		if (!lunargolemshotsDef)
		//		{
		//			lunargolemshotsDef = true;
		//		}
		//	}
		//	else
		//	{
		//		lunargolemshotsDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARWISP_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARWISP_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARWISP_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "LUNARWISP_NAME")
		//	{
		//		if (!lunarwispminigunDef)
		//		{
		//			lunarwispminigunDef = true;
		//		}
		//	}
		//	else
		//	{
		//		lunarwispminigunDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "PARENT_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "PARENT_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "PARENT_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "PARENT_NAME")
		//	{
		//		if (!parentteleportDef)
		//		{
		//			parentteleportDef = true;
		//		}
		//	}
		//	else
		//	{
		//		parentteleportDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "STONEGOLEM_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "STONEGOLEM_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "STONEGOLEM_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "STONEGOLEM_NAME")
		//	{
		//		if (!stonegolemlaserDef)
		//		{
		//			stonegolemlaserDef = true;
		//		}
		//	}
		//	else
		//	{
		//		stonegolemlaserDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "VOIDJAILER_NAME")
		//	{
		//		if (!voidjailerpassiveDef)
		//		{
		//			voidjailerpassiveDef = true;
		//		}
		//	}
		//	else
		//	{
		//		voidjailerpassiveDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEQUEEN_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLEQUEEN_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLEQUEEN_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "BEETLEQUEEN_NAME")
		//	{
		//		if (!beetlequeenshotgunDef)
		//		{
		//			beetlequeenshotgunDef = true;
		//		}
		//	}
		//	else
		//	{
		//		beetlequeenshotgunDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "GROVETENDER_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "GROVETENDER_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "GROVETENDER_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "GROVETENDER_NAME")
		//	{
		//		if (!grovetenderhookDef)
		//		{
		//			grovetenderhookDef = true;
		//		}
		//	}
		//	else
		//	{
		//		grovetenderhookDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME")
		//	{
		//		if (!claydunestriderballDef)
		//		{
		//			claydunestriderballDef = true;
		//		}
		//	}
		//	else
		//	{
		//		claydunestriderballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME")
		//	{
		//		if (!soluscontrolunityknockupDef)
		//		{
		//			soluscontrolunityknockupDef = true;
		//		}
		//	}
		//	else
		//	{
		//		soluscontrolunityknockupDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "XICONSTRUCT_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "XICONSTRUCT_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "XICONSTRUCT_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "XICONSTRUCT_NAME")
		//	{
		//		if (!xiconstructbeamDef)
		//		{
		//			xiconstructbeamDef = true;
		//		}
		//	}
		//	else
		//	{
		//		xiconstructbeamDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "VOIDDEVASTATOR_NAME")
		//	{
		//		if (!voiddevastatorhomingDef)
		//		{
		//			voiddevastatorhomingDef = true;
		//		}
		//	}
		//	else
		//	{
		//		voiddevastatorhomingDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "SCAVENGER_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "SCAVENGER_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "SCAVENGER_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "SCAVENGER_NAME")
		//	{
		//		if (!scavengerthqwibDef)
		//		{
		//			scavengerthqwibDef = true;
		//		}
		//	}
		//	else
		//	{
		//		scavengerthqwibDef = false;
		//	}
		//	//check passive
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "ALPHACONSTRUCT_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.alphashieldonBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "BEETLE_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "BEETLE_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "BEETLE_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "BEETLE_NAME")
		//	{
		//		Shiggycon.strengthMultiplier = StaticValues.beetlestrengthMultiplier;
		//		characterBody.AddBuff(Modules.Buffs.beetleBuff);
		//	}
		//	else
		//	{
		//		Shiggycon.strengthMultiplier = 1f;
		//		characterBody.RemoveBuff(Modules.Buffs.beetleBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "GUP_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "GUP_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "GUP_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "GUP_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.spikeBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.spikeBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "LARVA_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "LARVA_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "LARVA_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "LARVA_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.larvajumpBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.larvajumpBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "LESSERWISP_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "LESSERWISP_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "LESSERWISP_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "LESSERWISP_NAME")
		//	{
		//		rangedMultiplier = StaticValues.lesserwisprangedMultiplier;
		//		characterBody.AddBuff(Modules.Buffs.lesserwispBuff);
		//	}
		//	else
		//	{
		//		rangedMultiplier = 1f;
		//		characterBody.RemoveBuff(Modules.Buffs.lesserwispBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "LUNAREXPLODER_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "LUNAREXPLODER_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "LUNAREXPLODER_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "LUNAREXPLODER_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.lunarexploderBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.lunarexploderBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "HERMITCRAB_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "HERMITCRAB_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "HERMITCRAB_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "HERMITCRAB_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.hermitcrabmortarBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.hermitcrabmortarBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "PEST_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "PEST_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "PEST_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "PEST_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.pestjumpBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.pestjumpBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "VERMIN_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "VERMIN_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "VERMIN_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "VERMIN_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.verminsprintBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.verminsprintBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "MINIMUSHRUM_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "MINIMUSHRUM_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "MINIMUSHRUM_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "MINIMUSHRUM_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.minimushrumBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.minimushrumBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "ROBOBALLMINI_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "ROBOBALLMINI_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "ROBOBALLMINI_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "ROBOBALLMINI_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.roboballminiBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.roboballminiBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDBARNACLE_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDBARNACLE_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "VOIDBARNACLE_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDBARNACLE_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.voidbarnaclemortarBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDJAILER_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.voidjailerBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.voidjailerBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "STONETITAN_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "STONETITAN_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "STONETITAN_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "STONETITAN_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.stonetitanBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.stonetitanBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "MAGMAWORM_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "MAGMAWORM_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "MAGMAWORM_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "MAGMAWORM_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.magmawormBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.magmawormBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "OVERLOADINGWORM_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.overloadingwormBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.overloadingwormBuff);
		//	}

		//	//check active
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "VULTURE_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "VULTURE_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "VULTURE_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "VULTURE_NAME")
		//	{
		//		if (!alloyvultureflyDef)
		//		{
		//			alloyvultureflyDef = true;
		//		}
		//	}
		//	else
		//	{
		//		alloyvultureflyDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEGUARD_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLEGUARD_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLEGUARD_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "BEETLEGUARD_NAME")
		//	{
		//		if (!beetleguardslamDef)
		//		{
		//			beetleguardslamDef = true;
		//		}
		//	}
		//	else
		//	{
		//		beetleguardslamDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "BRONZONG_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "BRONZONG_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "BRONZONG_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "BRONZONG_NAME")
		//	{
		//		if (!bronzongballDef)
		//		{
		//			bronzongballDef = true;
		//		}
		//	}
		//	else
		//	{
		//		bronzongballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "APOTHECARY_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "APOTHECARY_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "APOTHECARY_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "APOTHECARY_NAME")
		//	{
		//		if (!clayapothecarymortarDef)
		//		{
		//			clayapothecarymortarDef = true;
		//		}
		//	}
		//	else
		//	{
		//		clayapothecarymortarDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "TEMPLAR_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "TEMPLAR_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "TEMPLAR_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "TEMPLAR_NAME")
		//	{
		//		if (!claytemplarminigunDef)
		//		{
		//			claytemplarminigunDef = true;
		//		}
		//	}
		//	else
		//	{
		//		claytemplarminigunDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "GREATERWISP_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "GREATERWISP_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "GREATERWISP_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "GREATERWISP_NAME")
		//	{
		//		if (!greaterwispballDef)
		//		{
		//			greaterwispballDef = true;
		//		}
		//	}
		//	else
		//	{
		//		greaterwispballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "IMP_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "IMP_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "IMP_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "IMP_NAME")
		//	{
		//		if (!impblinkDef)
		//		{
		//			impblinkDef = true;
		//		}
		//	}
		//	else
		//	{
		//		impblinkDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "JELLYFISH_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "JELLYFISH_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "JELLYFISH_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "JELLYFISH_NAME")
		//	{
		//		if (!jellyfishnovaDef)
		//		{
		//			jellyfishnovaDef = true;
		//		}
		//	}
		//	else
		//	{
		//		jellyfishnovaDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "LEMURIAN_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "LEMURIAN_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "LEMURIAN_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "LEMURIAN_NAME")
		//	{
		//		if (!lemurianfireballDef)
		//		{
		//			lemurianfireballDef = true;
		//		}
		//	}
		//	else
		//	{
		//		lemurianfireballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARGOLEM_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARGOLEM_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARGOLEM_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "LUNARGOLEM_NAME")
		//	{
		//		if (!lunargolemshotsDef)
		//		{
		//			lunargolemshotsDef = true;
		//		}
		//	}
		//	else
		//	{
		//		lunargolemshotsDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARWISP_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARWISP_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARWISP_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "LUNARWISP_NAME")
		//	{
		//		if (!lunarwispminigunDef)
		//		{
		//			lunarwispminigunDef = true;
		//		}
		//	}
		//	else
		//	{
		//		lunarwispminigunDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "PARENT_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "PARENT_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "PARENT_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "PARENT_NAME")
		//	{
		//		if (!parentteleportDef)
		//		{
		//			parentteleportDef = true;
		//		}
		//	}
		//	else
		//	{
		//		parentteleportDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "STONEGOLEM_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "STONEGOLEM_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "STONEGOLEM_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "STONEGOLEM_NAME")
		//	{
		//		if (!stonegolemlaserDef)
		//		{
		//			stonegolemlaserDef = true;
		//		}
		//	}
		//	else
		//	{
		//		stonegolemlaserDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "VOIDJAILER_NAME")
		//	{
		//		if (!voidjailerpassiveDef)
		//		{
		//			voidjailerpassiveDef = true;
		//		}
		//	}
		//	else
		//	{
		//		voidjailerpassiveDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEQUEEN_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLEQUEEN_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLEQUEEN_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "BEETLEQUEEN_NAME")
		//	{
		//		if (!beetlequeenshotgunDef)
		//		{
		//			beetlequeenshotgunDef = true;
		//		}
		//	}
		//	else
		//	{
		//		beetlequeenshotgunDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "GROVETENDER_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "GROVETENDER_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "GROVETENDER_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "GROVETENDER_NAME")
		//	{
		//		if (!grovetenderhookDef)
		//		{
		//			grovetenderhookDef = true;
		//		}
		//	}
		//	else
		//	{
		//		grovetenderhookDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME")
		//	{
		//		if (!claydunestriderballDef)
		//		{
		//			claydunestriderballDef = true;
		//		}
		//	}
		//	else
		//	{
		//		claydunestriderballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME")
		//	{
		//		if (!soluscontrolunityknockupDef)
		//		{
		//			soluscontrolunityknockupDef = true;
		//		}
		//	}
		//	else
		//	{
		//		soluscontrolunityknockupDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "XICONSTRUCT_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "XICONSTRUCT_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "XICONSTRUCT_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "XICONSTRUCT_NAME")
		//	{
		//		if (!xiconstructbeamDef)
		//		{
		//			xiconstructbeamDef = true;
		//		}
		//	}
		//	else
		//	{
		//		xiconstructbeamDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "VOIDDEVASTATOR_NAME")
		//	{
		//		if (!voiddevastatorhomingDef)
		//		{
		//			voiddevastatorhomingDef = true;
		//		}
		//	}
		//	else
		//	{
		//		voiddevastatorhomingDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "SCAVENGER_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "SCAVENGER_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "SCAVENGER_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "SCAVENGER_NAME")
		//	{
		//		if (!scavengerthqwibDef)
		//		{
		//			scavengerthqwibDef = true;
		//		}
		//	}
		//	else
		//	{
		//		scavengerthqwibDef = false;
		//	}
		//	//check passive
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "ALPHACONSTRUCT_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.alphashieldonBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "BEETLE_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "BEETLE_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "BEETLE_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "BEETLE_NAME")
		//	{
		//		strengthMultiplier = StaticValues.beetlestrengthMultiplier;
		//		characterBody.AddBuff(Modules.Buffs.beetleBuff);
		//	}
		//	else
		//	{
		//		strengthMultiplier = 1f;
		//		characterBody.RemoveBuff(Modules.Buffs.beetleBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "GUP_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "GUP_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "GUP_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "GUP_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.spikeBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.spikeBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "LARVA_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "LARVA_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "LARVA_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "LARVA_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.larvajumpBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.larvajumpBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "LESSERWISP_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "LESSERWISP_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "LESSERWISP_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "LESSERWISP_NAME")
		//	{
		//		rangedMultiplier = StaticValues.lesserwisprangedMultiplier;
		//		characterBody.AddBuff(Modules.Buffs.lesserwispBuff);
		//	}
		//	else
		//	{
		//		rangedMultiplier = 1f;
		//		characterBody.RemoveBuff(Modules.Buffs.lesserwispBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "LUNAREXPLODER_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "LUNAREXPLODER_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "LUNAREXPLODER_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "LUNAREXPLODER_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.lunarexploderBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.lunarexploderBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "HERMITCRAB_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "HERMITCRAB_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "HERMITCRAB_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "HERMITCRAB_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.hermitcrabmortarBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.hermitcrabmortarBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "PEST_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "PEST_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "PEST_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "PEST_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.pestjumpBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.pestjumpBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "VERMIN_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "VERMIN_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "VERMIN_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "VERMIN_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.verminsprintBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.verminsprintBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "MINIMUSHRUM_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "MINIMUSHRUM_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "MINIMUSHRUM_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "MINIMUSHRUM_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.minimushrumBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.minimushrumBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "ROBOBALLMINI_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "ROBOBALLMINI_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "ROBOBALLMINI_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "ROBOBALLMINI_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.roboballminiBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.roboballminiBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDBARNACLE_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDBARNACLE_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "VOIDBARNACLE_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDBARNACLE_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.voidbarnaclemortarBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDJAILER_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.voidjailerBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.voidjailerBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "STONETITAN_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "STONETITAN_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "STONETITAN_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "STONETITAN_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.stonetitanBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.stonetitanBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "MAGMAWORM_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "MAGMAWORM_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "MAGMAWORM_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "MAGMAWORM_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.magmawormBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.magmawormBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "OVERLOADINGWORM_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.overloadingwormBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.overloadingwormBuff);
		//	}

		//	//check active
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "VULTURE_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "VULTURE_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "VULTURE_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "VULTURE_NAME")
		//	{
		//		if (!alloyvultureflyDef)
		//		{
		//			alloyvultureflyDef = true;
		//		}
		//	}
		//	else
		//	{
		//		alloyvultureflyDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEGUARD_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLEGUARD_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLEGUARD_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "BEETLEGUARD_NAME")
		//	{
		//		if (!beetleguardslamDef)
		//		{
		//			beetleguardslamDef = true;
		//		}
		//	}
		//	else
		//	{
		//		beetleguardslamDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "BRONZONG_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "BRONZONG_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "BRONZONG_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "BRONZONG_NAME")
		//	{
		//		if (!bronzongballDef)
		//		{
		//			bronzongballDef = true;
		//		}
		//	}
		//	else
		//	{
		//		bronzongballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "APOTHECARY_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "APOTHECARY_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "APOTHECARY_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "APOTHECARY_NAME")
		//	{
		//		if (!clayapothecarymortarDef)
		//		{
		//			clayapothecarymortarDef = true;
		//		}
		//	}
		//	else
		//	{
		//		clayapothecarymortarDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "TEMPLAR_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "TEMPLAR_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "TEMPLAR_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "TEMPLAR_NAME")
		//	{
		//		if (!claytemplarminigunDef)
		//		{
		//			claytemplarminigunDef = true;
		//		}
		//	}
		//	else
		//	{
		//		claytemplarminigunDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "GREATERWISP_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "GREATERWISP_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "GREATERWISP_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "GREATERWISP_NAME")
		//	{
		//		if (!greaterwispballDef)
		//		{
		//			greaterwispballDef = true;
		//		}
		//	}
		//	else
		//	{
		//		greaterwispballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "IMP_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "IMP_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "IMP_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "IMP_NAME")
		//	{
		//		if (!impblinkDef)
		//		{
		//			impblinkDef = true;
		//		}
		//	}
		//	else
		//	{
		//		impblinkDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "JELLYFISH_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "JELLYFISH_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "JELLYFISH_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "JELLYFISH_NAME")
		//	{
		//		if (!jellyfishnovaDef)
		//		{
		//			jellyfishnovaDef = true;
		//		}
		//	}
		//	else
		//	{
		//		jellyfishnovaDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "LEMURIAN_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "LEMURIAN_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "LEMURIAN_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "LEMURIAN_NAME")
		//	{
		//		if (!lemurianfireballDef)
		//		{
		//			lemurianfireballDef = true;
		//		}
		//	}
		//	else
		//	{
		//		lemurianfireballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARGOLEM_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARGOLEM_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARGOLEM_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "LUNARGOLEM_NAME")
		//	{
		//		if (!lunargolemshotsDef)
		//		{
		//			lunargolemshotsDef = true;
		//		}
		//	}
		//	else
		//	{
		//		lunargolemshotsDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARWISP_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARWISP_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARWISP_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "LUNARWISP_NAME")
		//	{
		//		if (!lunarwispminigunDef)
		//		{
		//			lunarwispminigunDef = true;
		//		}
		//	}
		//	else
		//	{
		//		lunarwispminigunDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "PARENT_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "PARENT_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "PARENT_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "PARENT_NAME")
		//	{
		//		if (!parentteleportDef)
		//		{
		//			parentteleportDef = true;
		//		}
		//	}
		//	else
		//	{
		//		parentteleportDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "STONEGOLEM_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "STONEGOLEM_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "STONEGOLEM_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "STONEGOLEM_NAME")
		//	{
		//		if (!stonegolemlaserDef)
		//		{
		//			stonegolemlaserDef = true;
		//		}
		//	}
		//	else
		//	{
		//		stonegolemlaserDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "VOIDJAILER_NAME")
		//	{
		//		if (!voidjailerpassiveDef)
		//		{
		//			voidjailerpassiveDef = true;
		//		}
		//	}
		//	else
		//	{
		//		voidjailerpassiveDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEQUEEN_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLEQUEEN_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLEQUEEN_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "BEETLEQUEEN_NAME")
		//	{
		//		if (!beetlequeenshotgunDef)
		//		{
		//			beetlequeenshotgunDef = true;
		//		}
		//	}
		//	else
		//	{
		//		beetlequeenshotgunDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "GROVETENDER_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "GROVETENDER_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "GROVETENDER_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "GROVETENDER_NAME")
		//	{
		//		if (!grovetenderhookDef)
		//		{
		//			grovetenderhookDef = true;
		//		}
		//	}
		//	else
		//	{
		//		grovetenderhookDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME")
		//	{
		//		if (!claydunestriderballDef)
		//		{
		//			claydunestriderballDef = true;
		//		}
		//	}
		//	else
		//	{
		//		claydunestriderballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME")
		//	{
		//		if (!soluscontrolunityknockupDef)
		//		{
		//			soluscontrolunityknockupDef = true;
		//		}
		//	}
		//	else
		//	{
		//		soluscontrolunityknockupDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "XICONSTRUCT_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "XICONSTRUCT_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "XICONSTRUCT_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "XICONSTRUCT_NAME")
		//	{
		//		if (!xiconstructbeamDef)
		//		{
		//			xiconstructbeamDef = true;
		//		}
		//	}
		//	else
		//	{
		//		xiconstructbeamDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "VOIDDEVASTATOR_NAME")
		//	{
		//		if (!voiddevastatorhomingDef)
		//		{
		//			voiddevastatorhomingDef = true;
		//		}
		//	}
		//	else
		//	{
		//		voiddevastatorhomingDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "SCAVENGER_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "SCAVENGER_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "SCAVENGER_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "SCAVENGER_NAME")
		//	{
		//		if (!scavengerthqwibDef)
		//		{
		//			scavengerthqwibDef = true;
		//		}
		//	}
		//	else
		//	{
		//		scavengerthqwibDef = false;
		//	}
		//	//check passive
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "ALPHACONSTRUCT_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.alphashieldonBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "BEETLE_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "BEETLE_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "BEETLE_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "BEETLE_NAME")
		//	{
		//		strengthMultiplier = StaticValues.beetlestrengthMultiplier;
		//		characterBody.AddBuff(Modules.Buffs.beetleBuff);
		//	}
		//	else
		//	{
		//		strengthMultiplier = 1f;
		//		characterBody.RemoveBuff(Modules.Buffs.beetleBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "GUP_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "GUP_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "GUP_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "GUP_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.spikeBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.spikeBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "LARVA_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "LARVA_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "LARVA_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "LARVA_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.larvajumpBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.larvajumpBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "LESSERWISP_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "LESSERWISP_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "LESSERWISP_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "LESSERWISP_NAME")
		//	{
		//		rangedMultiplier = StaticValues.lesserwisprangedMultiplier;
		//		characterBody.AddBuff(Modules.Buffs.lesserwispBuff);
		//	}
		//	else
		//	{
		//		rangedMultiplier = 1f;
		//		characterBody.RemoveBuff(Modules.Buffs.lesserwispBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "LUNAREXPLODER_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "LUNAREXPLODER_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "LUNAREXPLODER_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "LUNAREXPLODER_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.lunarexploderBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.lunarexploderBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "HERMITCRAB_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "HERMITCRAB_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "HERMITCRAB_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "HERMITCRAB_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.hermitcrabmortarBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.hermitcrabmortarBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "PEST_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "PEST_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "PEST_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "PEST_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.pestjumpBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.pestjumpBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "VERMIN_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "VERMIN_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "VERMIN_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "VERMIN_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.verminsprintBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.verminsprintBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "MINIMUSHRUM_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "MINIMUSHRUM_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "MINIMUSHRUM_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "MINIMUSHRUM_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.minimushrumBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.minimushrumBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "ROBOBALLMINI_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "ROBOBALLMINI_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "ROBOBALLMINI_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "ROBOBALLMINI_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.roboballminiBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.roboballminiBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDBARNACLE_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDBARNACLE_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "VOIDBARNACLE_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDBARNACLE_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.voidbarnaclemortarBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDJAILER_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.voidjailerBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.voidjailerBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "STONETITAN_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "STONETITAN_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "STONETITAN_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "STONETITAN_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.stonetitanBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.stonetitanBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "MAGMAWORM_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "MAGMAWORM_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "MAGMAWORM_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "MAGMAWORM_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.magmawormBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.magmawormBuff);
		//	}
		//	if (extraskillLocator.extraFirst.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
		//		| extraskillLocator.extraSecond.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
		//		| extraskillLocator.extraThird.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
		//		| extraskillLocator.extraFourth.skillNameToken == prefix + "OVERLOADINGWORM_NAME")
		//	{
		//		characterBody.AddBuff(Modules.Buffs.overloadingwormBuff);
		//	}
		//	else
		//	{
		//		characterBody.RemoveBuff(Modules.Buffs.overloadingwormBuff);
		//	}

		//	//check active
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "VULTURE_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "VULTURE_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "VULTURE_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "VULTURE_NAME")
		//	{
		//		if (!alloyvultureflyDef)
		//		{
		//			alloyvultureflyDef = true;
		//		}
		//	}
		//	else
		//	{
		//		alloyvultureflyDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEGUARD_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLEGUARD_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLEGUARD_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "BEETLEGUARD_NAME")
		//	{
		//		if (!beetleguardslamDef)
		//		{
		//			beetleguardslamDef = true;
		//		}
		//	}
		//	else
		//	{
		//		beetleguardslamDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "BRONZONG_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "BRONZONG_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "BRONZONG_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "BRONZONG_NAME")
		//	{
		//		if (!bronzongballDef)
		//		{
		//			bronzongballDef = true;
		//		}
		//	}
		//	else
		//	{
		//		bronzongballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "APOTHECARY_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "APOTHECARY_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "APOTHECARY_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "APOTHECARY_NAME")
		//	{
		//		if (!clayapothecarymortarDef)
		//		{
		//			clayapothecarymortarDef = true;
		//		}
		//	}
		//	else
		//	{
		//		clayapothecarymortarDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "TEMPLAR_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "TEMPLAR_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "TEMPLAR_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "TEMPLAR_NAME")
		//	{
		//		if (!claytemplarminigunDef)
		//		{
		//			claytemplarminigunDef = true;
		//		}
		//	}
		//	else
		//	{
		//		claytemplarminigunDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "GREATERWISP_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "GREATERWISP_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "GREATERWISP_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "GREATERWISP_NAME")
		//	{
		//		if (!greaterwispballDef)
		//		{
		//			greaterwispballDef = true;
		//		}
		//	}
		//	else
		//	{
		//		greaterwispballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "IMP_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "IMP_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "IMP_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "IMP_NAME")
		//	{
		//		if (!impblinkDef)
		//		{
		//			impblinkDef = true;
		//		}
		//	}
		//	else
		//	{
		//		impblinkDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "JELLYFISH_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "JELLYFISH_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "JELLYFISH_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "JELLYFISH_NAME")
		//	{
		//		if (!jellyfishnovaDef)
		//		{
		//			jellyfishnovaDef = true;
		//		}
		//	}
		//	else
		//	{
		//		jellyfishnovaDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "LEMURIAN_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "LEMURIAN_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "LEMURIAN_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "LEMURIAN_NAME")
		//	{
		//		if (!lemurianfireballDef)
		//		{
		//			lemurianfireballDef = true;
		//		}
		//	}
		//	else
		//	{
		//		lemurianfireballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARGOLEM_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARGOLEM_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARGOLEM_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "LUNARGOLEM_NAME")
		//	{
		//		if (!lunargolemshotsDef)
		//		{
		//			lunargolemshotsDef = true;
		//		}
		//	}
		//	else
		//	{
		//		lunargolemshotsDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "LUNARWISP_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNARWISP_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "LUNARWISP_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "LUNARWISP_NAME")
		//	{
		//		if (!lunarwispminigunDef)
		//		{
		//			lunarwispminigunDef = true;
		//		}
		//	}
		//	else
		//	{
		//		lunarwispminigunDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "PARENT_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "PARENT_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "PARENT_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "PARENT_NAME")
		//	{
		//		if (!parentteleportDef)
		//		{
		//			parentteleportDef = true;
		//		}
		//	}
		//	else
		//	{
		//		parentteleportDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "STONEGOLEM_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "STONEGOLEM_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "STONEGOLEM_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "STONEGOLEM_NAME")
		//	{
		//		if (!stonegolemlaserDef)
		//		{
		//			stonegolemlaserDef = true;
		//		}
		//	}
		//	else
		//	{
		//		stonegolemlaserDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDJAILER_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "VOIDJAILER_NAME")
		//	{
		//		if (!voidjailerpassiveDef)
		//		{
		//			voidjailerpassiveDef = true;
		//		}
		//	}
		//	else
		//	{
		//		voidjailerpassiveDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLEQUEEN_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLEQUEEN_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLEQUEEN_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "BEETLEQUEEN_NAME")
		//	{
		//		if (!beetlequeenshotgunDef)
		//		{
		//			beetlequeenshotgunDef = true;
		//		}
		//	}
		//	else
		//	{
		//		beetlequeenshotgunDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "GROVETENDER_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "GROVETENDER_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "GROVETENDER_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "GROVETENDER_NAME")
		//	{
		//		if (!grovetenderhookDef)
		//		{
		//			grovetenderhookDef = true;
		//		}
		//	}
		//	else
		//	{
		//		grovetenderhookDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "CLAYDUNESTRIDER_NAME")
		//	{
		//		if (!claydunestriderballDef)
		//		{
		//			claydunestriderballDef = true;
		//		}
		//	}
		//	else
		//	{
		//		claydunestriderballDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "SOLUSCONTROLUNIT_NAME")
		//	{
		//		if (!soluscontrolunityknockupDef)
		//		{
		//			soluscontrolunityknockupDef = true;
		//		}
		//	}
		//	else
		//	{
		//		soluscontrolunityknockupDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "XICONSTRUCT_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "XICONSTRUCT_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "XICONSTRUCT_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "XICONSTRUCT_NAME")
		//	{
		//		if (!xiconstructbeamDef)
		//		{
		//			xiconstructbeamDef = true;
		//		}
		//	}
		//	else
		//	{
		//		xiconstructbeamDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDDEVASTATOR_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "VOIDDEVASTATOR_NAME")
		//	{
		//		if (!voiddevastatorhomingDef)
		//		{
		//			voiddevastatorhomingDef = true;
		//		}
		//	}
		//	else
		//	{
		//		voiddevastatorhomingDef = false;
		//	}
		//	if (characterBody.skillLocator.primary.skillNameToken == prefix + "SCAVENGER_NAME"
		//		| characterBody.skillLocator.secondary.skillNameToken == prefix + "SCAVENGER_NAME"
		//		| characterBody.skillLocator.utility.skillNameToken == prefix + "SCAVENGER_NAME"
		//		| characterBody.skillLocator.special.skillNameToken == prefix + "SCAVENGER_NAME")
		//	{
		//		if (!scavengerthqwibDef)
		//		{
		//			scavengerthqwibDef = true;
		//		}
		//	}
		//	else
		//	{
		//		scavengerthqwibDef = false;
		//	}
		//}
	}
}
