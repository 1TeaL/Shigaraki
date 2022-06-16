using EntityStates;
using ExtraSkillSlots;
using R2API.Networking;
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
        private CharacterBody self;


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

		public void OnDestroy()
        {
			On.RoR2.CharacterBody.Start -= CharacterBody_Start;
			On.RoR2.Run.Start -= Run_Start;
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
			self = characterMaster.GetBody();

            Shiggymastercon = characterMaster.gameObject.GetComponent<ShiggyMasterController>();
            Shiggycon = self.gameObject.GetComponent<ShiggyController>();

			extraskillLocator = self.gameObject.GetComponent<ExtraSkillLocator>();
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

            if (self)
			{
                if (self.master)
				{
					if (self.master.bodyPrefab == BodyCatalog.FindBodyPrefab("ShiggyBody"))
					{
						if (self.master.gameObject.GetComponent<ShiggyMasterController>())
						{

							primarygiven = false;
							secondarygiven = false;
							utilitygiven = false;
							specialgiven = false;
							extra1given = false;
							extra2given = false;
							extra3given = false;
							extra4given = false;

							if (!gameObject.GetComponent<ShiggyController>())
							{
								Shiggycon = self.gameObject.AddComponent<ShiggyController>();
							}
							else
							{
								Shiggycon = self.gameObject.GetComponent<ShiggyController>();
							}
							extraskillLocator = self.gameObject.GetComponent<ExtraSkillLocator>();


							if (Config.retainLoadout.Value)
							{
								if (skillListToOverrideOnRespawn[0] != null && !primarygiven)
								{
									primarygiven = true;
									self.skillLocator.primary.SetSkillOverride(self.skillLocator.primary, skillListToOverrideOnRespawn[0], GenericSkill.SkillOverridePriority.Contextual);
								}
								else if (skillListToOverrideOnRespawn[0] == null && !primarygiven)
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
									CheckQuirks(4, self);
									extra1given = true;
									extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, skillListToOverrideOnRespawn[4], GenericSkill.SkillOverridePriority.Contextual);
								}
								else if (skillListToOverrideOnRespawn[4] == null && !extra1given)
								{
									extra1given = true;
								}
								if (skillListToOverrideOnRespawn[5] != null && !extra2given)
								{
									CheckQuirks(5, self);
									extra2given = true;
									extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, skillListToOverrideOnRespawn[5], GenericSkill.SkillOverridePriority.Contextual);
								}
								else if (skillListToOverrideOnRespawn[5] == null && !extra2given)
								{
									extra2given = true;
								}
								if (skillListToOverrideOnRespawn[6] != null && !extra3given)
								{
									CheckQuirks(6, self);
									extra3given = true;
									extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, skillListToOverrideOnRespawn[6], GenericSkill.SkillOverridePriority.Contextual);
								}
								else if (skillListToOverrideOnRespawn[6] == null && !extra3given)
								{
									extra3given = true;
								}
								if (skillListToOverrideOnRespawn[7] != null && !extra4given)
								{
									CheckQuirks(7, self);
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
							

			}
		}




        public void FixedUpdate()
        {
   //         if (characterMaster)
   //         {
			//	self = characterMaster.GetBody();
   //         }
   //         if (self)
			//{
			//	if (!self.gameObject.GetComponent<ExtraSkillLocator>())
			//	{
			//		extraskillLocator = self.gameObject.GetComponent<ExtraSkillLocator>();
			//	}

			//}


			if (!characterMaster.gameObject)
			{
				Destroy(Shiggymastercon);
			}
		}
				
		public void CheckQuirks(int num, CharacterBody body)
        {
			if (skillListToOverrideOnRespawn[num] == Shiggy.alphacontructpassiveDef)
			{
				body.ApplyBuff(Buffs.alphashieldonBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.beetlepassiveDef)
			{
				body.ApplyBuff(Buffs.beetleBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.pestpassiveDef)
			{
				body.ApplyBuff(Buffs.pestjumpBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.verminpassiveDef)
			{
				body.ApplyBuff(Buffs.verminsprintBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.guppassiveDef)
			{
				body.ApplyBuff(Buffs.gupspikeBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.hermitcrabpassiveDef)
			{
				body.ApplyBuff(Buffs.hermitcrabmortarBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.larvapassiveDef)
			{
				body.ApplyBuff(Buffs.larvajumpBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.lesserwisppassiveDef)
			{
				body.ApplyBuff(Buffs.lesserwispBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.lunarexploderpassiveDef)
			{
				body.ApplyBuff(Buffs.lunarexploderBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.minimushrumpassiveDef)
			{
				body.ApplyBuff(Buffs.minimushrumBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.roboballminibpassiveDef)
			{
				body.ApplyBuff(Buffs.roboballminiBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.voidbarnaclepassiveDef)
			{
				body.ApplyBuff(Buffs.voidbarnaclemortarBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.voidjailerpassiveDef)
			{
				body.ApplyBuff(Buffs.voidjailerBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.impbosspassiveDef)
			{
				body.ApplyBuff(Buffs.impbossBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.stonetitanpassiveDef)
			{
				body.ApplyBuff(Buffs.stonetitanBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.magmawormpassiveDef)
			{
				body.ApplyBuff(Buffs.magmawormBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.overloadingwormpassiveDef)
			{
				body.ApplyBuff(Buffs.overloadingwormBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.vagrantpassiveDef)
			{
				body.ApplyBuff(Buffs.vagrantBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.acridpassiveDef)
			{
				body.ApplyBuff(Buffs.acridBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.commandopassiveDef)
			{
				body.ApplyBuff(Buffs.commandoBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.captainpassiveDef)
			{
				body.ApplyBuff(Buffs.captainBuff.buffIndex, 1, -1);
			}
			if (skillListToOverrideOnRespawn[num] == Shiggy.loaderpassiveDef)
			{
				body.ApplyBuff(Buffs.loaderBuff.buffIndex, 1, -1);
			}
		}

	}
}
