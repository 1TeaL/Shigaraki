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
        public bool impbosspassiveDef;
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

        public void Awake()
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

            impbosspassiveDef = false;
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

        public void Run_Start(On.RoR2.Run.orig_Start orig, Run self)
        {
			orig.Invoke(self);

			writeToSkillList(null, 0);
			writeToSkillList(null, 1);
			writeToSkillList(null, 2);
			writeToSkillList(null, 3);
			writeToSkillList(null, 4);
			writeToSkillList(null, 5);
			writeToSkillList(null, 6);
			writeToSkillList(null, 7);
		}

        public void writeToSkillList(SkillDef skillDef, int index)
		{
			skillListToOverrideOnRespawn[index] = skillDef;
		}

        public void Start()
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

            impbosspassiveDef = false;
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


        public void CharacterBody_Start(On.RoR2.CharacterBody.orig_Start orig, CharacterBody self)
        {
            orig.Invoke(self);


            primarygiven = false;
            secondarygiven = false;
            utilitygiven = false;
            specialgiven = false;
            extra1given = false;
            extra2given = false;
            extra3given = false;
            extra4given = false;

			if (self.master.gameObject.GetComponent<ShiggyMasterController>())
			{
				if (self.master.bodyPrefab == BodyCatalog.FindBodyPrefab("ShiggyBody"))
				{
					extraskillLocator = self.gameObject.GetComponent<ExtraSkillLocator>();
				}
			}
			////Shiggycon.alphacontructpassiveDef = false;
			////Shiggycon.beetlepassiveDef = false;
			////Shiggycon.pestpassiveDef = false;
			////Shiggycon.verminpassiveDef = false;
			////Shiggycon.guppassiveDef = false;
			////Shiggycon.hermitcrabpassiveDef = false;
			////Shiggycon.larvapassiveDef = false;
			////Shiggycon.lesserwisppassiveDef = false;
			////Shiggycon.lunarexploderpassiveDef = false;
			////Shiggycon.minimushrumpassiveDef = false;
			////Shiggycon.roboballminibpassiveDef = false;
			////Shiggycon.voidbarnaclepassiveDef = false;
			////Shiggycon.voidjailerpassiveDef = false;

			////               Shiggycon.impbosspassiveDef = false;
			////               Shiggycon.stonetitanpassiveDef = false;
			////Shiggycon.magmawormpassiveDef = false;
			////Shiggycon.overloadingwormpassiveDef = false;


			////Shiggycon.alloyvultureflyDef = false;
			////Shiggycon.beetleguardslamDef = false;
			////Shiggycon.bisonchargeDef = false;
			////Shiggycon.bronzongballDef = false;
			////Shiggycon.clayapothecarymortarDef = false;
			////Shiggycon.claytemplarminigunDef = false;
			////Shiggycon.greaterwispballDef = false;
			////Shiggycon.impblinkDef = false;
			////Shiggycon.jellyfishnovaDef = false;
			////Shiggycon.lemurianfireballDef = false;
			////Shiggycon.lunargolemshotsDef = false;
			////Shiggycon.lunarwispminigunDef = false;
			////Shiggycon.parentteleportDef = false;
			////Shiggycon.stonegolemlaserDef = false;
			////Shiggycon.voidreaverportalDef = false;

			////Shiggycon.beetlequeenshotgunDef = false;
			////Shiggycon.grovetenderhookDef = false;
			////Shiggycon.claydunestriderballDef = false;
			//               //Shiggycon.grandparentsunDef = false;
			////Shiggycon.soluscontrolunityknockupDef = false;
			////Shiggycon.xiconstructbeamDef = false;
			////Shiggycon.voiddevastatorhomingDef = false;
			////Shiggycon.scavengerthqwibDef = false;


			//primarygiven = false;
			//secondarygiven = false;
			//utilitygiven = false;
			//specialgiven = false;
			//extra1given = false;
			//extra2given = false;
			//extra3given = false;
			//extra4given = false;


			//           }


			//       }

		}




        public void FixedUpdate()
        {
            characterMaster = gameObject.GetComponent<CharacterMaster>();
            CharacterBody self = characterMaster.GetBody();


			//add skills next stage/respawn
			if (Config.retainLoadout.Value)
			{
				if (skillListToOverrideOnRespawn[0] != null && !primarygiven)
				{
					primarygiven = true; 
					self.skillLocator.primary.SetSkillOverride(self.skillLocator.primary, skillListToOverrideOnRespawn[0], GenericSkill.SkillOverridePriority.Contextual);
				}
                else if(skillListToOverrideOnRespawn[0] == null && !primarygiven)
                {
                    primarygiven = true;
                }
				if (skillListToOverrideOnRespawn[1] != null && !secondarygiven)
				{
					secondarygiven = true;
					self.skillLocator.secondary.SetSkillOverride(self.skillLocator.secondary, skillListToOverrideOnRespawn[1], GenericSkill.SkillOverridePriority.Contextual);
                }
                else if (skillListToOverrideOnRespawn[1] == null && !secondarygiven)
                {
                    secondarygiven = true;
                }
                if (skillListToOverrideOnRespawn[2] != null && !utilitygiven)
				{
					utilitygiven = true;
					self.skillLocator.utility.SetSkillOverride(self.skillLocator.utility, skillListToOverrideOnRespawn[2], GenericSkill.SkillOverridePriority.Contextual);
                }
                else if (skillListToOverrideOnRespawn[2] == null && !utilitygiven)
                {
                    utilitygiven = true;
                }
                if (skillListToOverrideOnRespawn[3] != null && !specialgiven)
				{
					specialgiven = true;
					self.skillLocator.special.SetSkillOverride(self.skillLocator.special, skillListToOverrideOnRespawn[3], GenericSkill.SkillOverridePriority.Contextual);
                }
                else if (skillListToOverrideOnRespawn[3] == null && !specialgiven)
                {
                    specialgiven = true;
                }
                if (skillListToOverrideOnRespawn[4] != null && !extra1given)
				{
					extra1given = true;
					extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, skillListToOverrideOnRespawn[4], GenericSkill.SkillOverridePriority.Contextual);
				}
                else if (skillListToOverrideOnRespawn[4] == null && !extra1given)
                {
                    extra1given = true;
                }
                if (skillListToOverrideOnRespawn[5] != null && !extra2given)
				{
					extra2given = true;
					extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, skillListToOverrideOnRespawn[5], GenericSkill.SkillOverridePriority.Contextual);
                }
                else if (skillListToOverrideOnRespawn[5] == null && !extra2given)
                {
                    extra2given = true;
                }
                if (skillListToOverrideOnRespawn[6] != null && !extra3given)
				{
					extra3given = true;
					extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, skillListToOverrideOnRespawn[6], GenericSkill.SkillOverridePriority.Contextual);
                }
                else if (skillListToOverrideOnRespawn[6] == null && !extra3given)
                {
                    extra3given = true;
                }
                if (skillListToOverrideOnRespawn[7] != null && !extra4given)
				{
					extra4given = true;
					extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, skillListToOverrideOnRespawn[7], GenericSkill.SkillOverridePriority.Contextual);
                }
                else if (skillListToOverrideOnRespawn[7] == null && !extra4given)
                {
                    extra4given = true;
                }

            }


		}

    }
}
