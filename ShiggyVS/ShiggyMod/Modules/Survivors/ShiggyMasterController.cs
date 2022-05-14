using EntityStates;
using ExtraSkillSlots;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

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
		public bool grandparentsunDef;
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
				characterMaster = self.gameObject.GetComponent<CharacterMaster>();
				characterBody = characterMaster.GetBody();
				Shiggycon = self.gameObject.GetComponent<ShiggyController>();
				if (self.master.bodyPrefab == BodyCatalog.FindBodyPrefab("ShiggyBody"))
				{
					extraskillLocator = self.gameObject.GetComponent<ExtraSkillLocator>();
				}
			}
			//CheckQuirks();
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

			//checkquirks
			//if (NetworkServer.active)
			//{
			//	CheckQuirks();
			//}
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
			if (extraskillLocator.extraFirst.skillNameToken == prefix + "ROBOBALLMINI_NAME"
				&& extraskillLocator.extraSecond.skillNameToken == prefix + "ROBOBALLMINI_NAME"
				&& extraskillLocator.extraThird.skillNameToken == prefix + "ROBOBALLMINI_NAME"
				&& extraskillLocator.extraFourth.skillNameToken == prefix + "ROBOBALLMINI_NAME") 
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

	}
}
