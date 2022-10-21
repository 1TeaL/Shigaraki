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
		public bool claydunestriderbuffDef;
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
		public SkillDef[] storedAFOSkill;

        public void Awake()
        {

			On.RoR2.CharacterBody.Start += CharacterBody_Start;
            On.RoR2.Run.Start += Run_Start;

			skillListToOverrideOnRespawn = new SkillDef[8];
            storedAFOSkill = new SkillDef[1];
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
			claydunestriderbuffDef = false;
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

            writeToAFOSkillList(null, 0);

        }

        public void writeToSkillList(SkillDef skillDef, int index)
		{
			skillListToOverrideOnRespawn[index] = skillDef;
        }
        public void writeToAFOSkillList(SkillDef skillDef, int index)
        {
            storedAFOSkill[index] = skillDef;
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
			claydunestriderbuffDef = false;
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
                                    self.skillLocator.primary.SetSkillOverride(self.skillLocator.primary, Shiggy.decayDef, GenericSkill.SkillOverridePriority.Contextual);
                                }
								if (skillListToOverrideOnRespawn[1] != null && !secondarygiven)
								{
									secondarygiven = true;
									self.skillLocator.secondary.SetSkillOverride(self.skillLocator.secondary, skillListToOverrideOnRespawn[1], GenericSkill.SkillOverridePriority.Contextual);
								}
								else if (skillListToOverrideOnRespawn[1] == null && !secondarygiven)
								{
									secondarygiven = true;
                                    self.skillLocator.secondary.SetSkillOverride(self.skillLocator.secondary, Shiggy.bulletlaserDef, GenericSkill.SkillOverridePriority.Contextual);
                                }
								if (skillListToOverrideOnRespawn[2] != null && !utilitygiven)
								{
									utilitygiven = true;
									self.skillLocator.utility.SetSkillOverride(self.skillLocator.utility, skillListToOverrideOnRespawn[2], GenericSkill.SkillOverridePriority.Contextual);
								}
								else if (skillListToOverrideOnRespawn[2] == null && !utilitygiven)
								{
									utilitygiven = true;
                                    self.skillLocator.utility.SetSkillOverride(self.skillLocator.utility, Shiggy.aircannonDef, GenericSkill.SkillOverridePriority.Contextual);
                                }
								if (skillListToOverrideOnRespawn[3] != null && !specialgiven)
								{
									specialgiven = true;
									self.skillLocator.special.SetSkillOverride(self.skillLocator.special, skillListToOverrideOnRespawn[3], GenericSkill.SkillOverridePriority.Contextual);
								}
								else if (skillListToOverrideOnRespawn[3] == null && !specialgiven)
								{
									specialgiven = true;
                                    self.skillLocator.special.SetSkillOverride(self.skillLocator.special, Shiggy.multiplierDef, GenericSkill.SkillOverridePriority.Contextual);
                                }
								if (skillListToOverrideOnRespawn[4] != null && !extra1given)
								{
									extra1given = true;
									extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, skillListToOverrideOnRespawn[4], GenericSkill.SkillOverridePriority.Contextual);
								}
								else if (skillListToOverrideOnRespawn[4] == null && !extra1given)
								{
									extra1given = true;
                                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.emptySkillDef, GenericSkill.SkillOverridePriority.Contextual);
                                }
								if (skillListToOverrideOnRespawn[5] != null && !extra2given)
								{
									extra2given = true;
									extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, skillListToOverrideOnRespawn[5], GenericSkill.SkillOverridePriority.Contextual);
								}
								else if (skillListToOverrideOnRespawn[5] == null && !extra2given)
								{
									extra2given = true;
                                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.emptySkillDef, GenericSkill.SkillOverridePriority.Contextual);
                                }
								if (skillListToOverrideOnRespawn[6] != null && !extra3given)
								{
									extra3given = true;
									extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, skillListToOverrideOnRespawn[6], GenericSkill.SkillOverridePriority.Contextual);
								}
								else if (skillListToOverrideOnRespawn[6] == null && !extra3given)
								{
									extra3given = true;
                                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.emptySkillDef, GenericSkill.SkillOverridePriority.Contextual);
                                }
								if (skillListToOverrideOnRespawn[7] != null && !extra4given)
								{
									extra4given = true;
									extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, skillListToOverrideOnRespawn[7], GenericSkill.SkillOverridePriority.Contextual);
								}
								else if (skillListToOverrideOnRespawn[7] == null && !extra4given)
								{
									extra4given = true;
                                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.emptySkillDef, GenericSkill.SkillOverridePriority.Contextual);
                                }
                                CheckQuirks(self);


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


        public void CheckQuirks(CharacterBody characterBody)
        {
            extraskillLocator = characterBody.gameObject.GetComponent<ExtraSkillLocator>();
            //check passive
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "ALPHACONSTRUCT_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "ALPHACONSTRUCT_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "ALPHACONSTRUCT_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "ALPHACONSTRUCT_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "ALPHACONSTRUCT_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "ALPHACONSTRUCT_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "ALPHACONSTRUCT_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "ALPHACONSTRUCT_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.alphashieldonBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.alphashieldonBuff.buffIndex, 0);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "ALPHACONSTRUCT_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "ALPHACONSTRUCT_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.alphashieldonBuff.buffIndex);

                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "BEETLE_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "BEETLE_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "BEETLE_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "BEETLE_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "BEETLE_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "BEETLE_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "BEETLE_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "BEETLE_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.beetleBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.beetleBuff.buffIndex, 0);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "BEETLE_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "BEETLE_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "BEETLE_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "BEETLE_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "BEETLE_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "BEETLE_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "BEETLE_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "BEETLE_NAME")

            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.beetleBuff.buffIndex);
                }

            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "GUP_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "GUP_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "GUP_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "GUP_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "GUP_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "GUP_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "GUP_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "GUP_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.gupspikeBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.gupspikeBuff.buffIndex, 0);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "GUP_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "GUP_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "GUP_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "GUP_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "GUP_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "GUP_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "GUP_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "GUP_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.gupspikeBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "LARVA_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "LARVA_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "LARVA_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "LARVA_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "LARVA_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "LARVA_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "LARVA_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "LARVA_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.larvajumpBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.larvajumpBuff.buffIndex, 0);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "LARVA_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "LARVA_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "LARVA_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "LARVA_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "LARVA_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "LARVA_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "LARVA_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "LARVA_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.larvajumpBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "LESSERWISP_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "LESSERWISP_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "LESSERWISP_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "LESSERWISP_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "LESSERWISP_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "LESSERWISP_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "LESSERWISP_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "LESSERWISP_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.lesserwispBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.lesserwispBuff.buffIndex, 0);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "LESSERWISP_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "LESSERWISP_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "LESSERWISP_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "LESSERWISP_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "LESSERWISP_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "LESSERWISP_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "LESSERWISP_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "LESSERWISP_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.lesserwispBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "LUNAREXPLODER_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "LUNAREXPLODER_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "LUNAREXPLODER_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "LUNAREXPLODER_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "LUNAREXPLODER_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "LUNAREXPLODER_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "LUNAREXPLODER_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "LUNAREXPLODER_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.lunarexploderBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.lunarexploderBuff.buffIndex, 0);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "LUNAREXPLODER_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "LUNAREXPLODER_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "LUNAREXPLODER_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "LUNAREXPLODER_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "LUNAREXPLODER_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "LUNAREXPLODER_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "LUNAREXPLODER_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "LUNAREXPLODER_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.lunarexploderBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "HERMITCRAB_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "HERMITCRAB_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "HERMITCRAB_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "HERMITCRAB_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "HERMITCRAB_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "HERMITCRAB_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "HERMITCRAB_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "HERMITCRAB_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.hermitcrabmortarBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortarBuff.buffIndex, 0);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "HERMITCRAB_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "HERMITCRAB_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "HERMITCRAB_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "HERMITCRAB_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "HERMITCRAB_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "HERMITCRAB_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "HERMITCRAB_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "HERMITCRAB_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortarBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "PEST_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "PEST_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "PEST_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "PEST_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "PEST_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "PEST_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "PEST_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "PEST_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.pestjumpBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.pestjumpBuff.buffIndex, 0);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "PEST_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "PEST_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "PEST_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "PEST_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "PEST_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "PEST_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "PEST_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "PEST_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.pestjumpBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "VERMIN_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "VERMIN_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "VERMIN_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "VERMIN_NAME"
                && characterBody.skillLocator.primary.skillNameToken == prefix + "VERMIN_NAME"
                && characterBody.skillLocator.secondary.skillNameToken == prefix + "VERMIN_NAME"
                && characterBody.skillLocator.utility.skillNameToken == prefix + "VERMIN_NAME"
                && characterBody.skillLocator.special.skillNameToken == prefix + "VERMIN_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.verminsprintBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.verminsprintBuff.buffIndex, 0);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "VERMIN_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "VERMIN_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "VERMIN_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "VERMIN_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "VERMIN_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "VERMIN_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "VERMIN_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "VERMIN_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.verminsprintBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "MINIMUSHRUM_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "MINIMUSHRUM_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "MINIMUSHRUM_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "MINIMUSHRUM_NAME"
                && characterBody.skillLocator.primary.skillNameToken == prefix + "MINIMUSHRUM_NAME"
                && characterBody.skillLocator.secondary.skillNameToken == prefix + "MINIMUSHRUM_NAME"
                && characterBody.skillLocator.utility.skillNameToken == prefix + "MINIMUSHRUM_NAME"
                && characterBody.skillLocator.special.skillNameToken == prefix + "MINIMUSHRUM_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.minimushrumBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.minimushrumBuff.buffIndex, 0);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "MINIMUSHRUM_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "MINIMUSHRUM_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "MINIMUSHRUM_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "MINIMUSHRUM_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "MINIMUSHRUM_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "MINIMUSHRUM_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "MINIMUSHRUM_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "MINIMUSHRUM_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.minimushrumBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "ROBOBALLMINI_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "ROBOBALLMINI_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "ROBOBALLMINI_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "ROBOBALLMINI_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "ROBOBALLMINI_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "ROBOBALLMINI_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "ROBOBALLMINI_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "ROBOBALLMINI_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.roboballminiBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.roboballminiBuff.buffIndex, 0);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "ROBOBALLMINI_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "ROBOBALLMINI_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "ROBOBALLMINI_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "ROBOBALLMINI_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "ROBOBALLMINI_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "ROBOBALLMINI_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "ROBOBALLMINI_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "ROBOBALLMINI_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.roboballminiBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "VOIDBARNACLE_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "VOIDBARNACLE_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "VOIDBARNACLE_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "VOIDBARNACLE_NAME"
                && characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDBARNACLE_NAME"
                && characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDBARNACLE_NAME"
                && characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDBARNACLE_NAME"
                && characterBody.skillLocator.special.skillNameToken == prefix + "VOIDBARNACLE_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.voidbarnaclemortarBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarBuff.buffIndex, 0);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDBARNACLE_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDBARNACLE_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "VOIDBARNACLE_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDBARNACLE_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDBARNACLE_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDBARNACLE_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDBARNACLE_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "VOIDBARNACLE_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "VOIDJAILER_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "VOIDJAILER_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "VOIDJAILER_NAME"
                & extraskillLocator.extraFourth.skillNameToken != prefix + "VOIDJAILER_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "VOIDJAILER_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "VOIDJAILER_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "VOIDJAILER_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "VOIDJAILER_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.voidjailerBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.voidjailerBuff.buffIndex, 0);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "VOIDJAILER_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "VOIDJAILER_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "VOIDJAILER_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "VOIDJAILER_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "VOIDJAILER_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "VOIDJAILER_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "VOIDJAILER_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "VOIDJAILER_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.voidjailerBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "IMPBOSS_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "IMPBOSS_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "IMPBOSS_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "IMPBOSS_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "IMPBOSS_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "IMPBOSS_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "IMPBOSS_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "IMPBOSS_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.impbossBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.impbossBuff.buffIndex, 0);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "IMPBOSS_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "IMPBOSS_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "IMPBOSS_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "IMPBOSS_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "IMPBOSS_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "IMPBOSS_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "IMPBOSS_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "IMPBOSS_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.impbossBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "STONETITAN_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "STONETITAN_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "STONETITAN_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "STONETITAN_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "STONETITAN_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "STONETITAN_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "STONETITAN_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "STONETITAN_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.stonetitanBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.stonetitanBuff.buffIndex, 0);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "STONETITAN_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "STONETITAN_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "STONETITAN_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "STONETITAN_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "STONETITAN_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "STONETITAN_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "STONETITAN_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "STONETITAN_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.stonetitanBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "MAGMAWORM_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "MAGMAWORM_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "MAGMAWORM_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "MAGMAWORM_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "MAGMAWORM_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "MAGMAWORM_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "MAGMAWORM_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "MAGMAWORM_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.magmawormBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.magmawormBuff.buffIndex, 0);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "MAGMAWORM_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "MAGMAWORM_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "MAGMAWORM_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "MAGMAWORM_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "MAGMAWORM_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "MAGMAWORM_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "MAGMAWORM_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "MAGMAWORM_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.magmawormBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "OVERLOADINGWORM_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "OVERLOADINGWORM_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "OVERLOADINGWORM_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "OVERLOADINGWORM_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "OVERLOADINGWORM_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "OVERLOADINGWORM_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "OVERLOADINGWORM_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "OVERLOADINGWORM_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.overloadingwormBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.overloadingwormBuff.buffIndex, 0);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "OVERLOADINGWORM_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "OVERLOADINGWORM_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.overloadingwormBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "VAGRANT_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "VAGRANT_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "VAGRANT_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "VAGRANT_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "VAGRANT_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "VAGRANT_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "VAGRANT_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "VAGRANT_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.vagrantBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.vagrantBuff.buffIndex);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "VAGRANT_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "VAGRANT_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "VAGRANT_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "VAGRANT_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "VAGRANT_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "VAGRANT_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "VAGRANT_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "VAGRANT_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.vagrantBuff.buffIndex);
                }
            }


            if (extraskillLocator.extraFirst.skillNameToken != prefix + "ACRID_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "ACRID_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "ACRID_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "ACRID_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "ACRID_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "ACRID_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "ACRID_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "ACRID_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.acridBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.acridBuff.buffIndex);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "ACRID_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "ACRID_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "ACRID_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "ACRID_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "ACRID_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "ACRID_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "ACRID_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "ACRID_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.acridBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "COMMANDO_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "COMMANDO_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "COMMANDO_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "COMMANDO_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "COMMANDO_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "COMMANDO_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "COMMANDO_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "COMMANDO_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.commandoBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.commandoBuff.buffIndex);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "COMMANDO_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "COMMANDO_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "COMMANDO_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "COMMANDO_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "COMMANDO_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "COMMANDO_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "COMMANDO_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "COMMANDO_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.commandoBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "CAPTAIN_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "CAPTAIN_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "CAPTAIN_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "CAPTAIN_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "CAPTAIN_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "CAPTAIN_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "CAPTAIN_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "CAPTAIN_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.captainBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.captainBuff.buffIndex);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "CAPTAIN_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "CAPTAIN_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "CAPTAIN_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "CAPTAIN_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "CAPTAIN_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "CAPTAIN_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "CAPTAIN_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "CAPTAIN_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.captainBuff.buffIndex);
                }
            }
            if (extraskillLocator.extraFirst.skillNameToken != prefix + "LOADER_NAME"
                && extraskillLocator.extraSecond.skillNameToken != prefix + "LOADER_NAME"
                && extraskillLocator.extraThird.skillNameToken != prefix + "LOADER_NAME"
                && extraskillLocator.extraFourth.skillNameToken != prefix + "LOADER_NAME"
                && characterBody.skillLocator.primary.skillNameToken != prefix + "LOADER_NAME"
                && characterBody.skillLocator.secondary.skillNameToken != prefix + "LOADER_NAME"
                && characterBody.skillLocator.utility.skillNameToken != prefix + "LOADER_NAME"
                && characterBody.skillLocator.special.skillNameToken != prefix + "LOADER_NAME")
            {
                if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.loaderBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.loaderBuff.buffIndex);
                }
            }
            else if (extraskillLocator.extraFirst.skillNameToken == prefix + "LOADER_NAME"
                || extraskillLocator.extraSecond.skillNameToken == prefix + "LOADER_NAME"
                || extraskillLocator.extraThird.skillNameToken == prefix + "LOADER_NAME"
                || extraskillLocator.extraFourth.skillNameToken == prefix + "LOADER_NAME"
                || characterBody.skillLocator.primary.skillNameToken == prefix + "LOADER_NAME"
                || characterBody.skillLocator.secondary.skillNameToken == prefix + "LOADER_NAME"
                || characterBody.skillLocator.utility.skillNameToken == prefix + "LOADER_NAME"
                || characterBody.skillLocator.special.skillNameToken == prefix + "LOADER_NAME")
            {
                if (NetworkServer.active)
                {
                    characterBody.ApplyBuff(Modules.Buffs.loaderBuff.buffIndex);
                }
            }

        }


        //public void CheckQuirks(int num, CharacterBody body)
        //      {
        //	//remove
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.alphacontructpassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.alphashieldonBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.beetlepassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.beetleBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.pestpassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.pestjumpBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.verminpassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.verminsprintBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.guppassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.gupspikeBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.hermitcrabpassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.hermitcrabmortarBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.larvapassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.larvajumpBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.lesserwisppassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.lesserwispBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.lunarexploderpassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.lunarexploderBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.minimushrumpassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.minimushrumBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.roboballminibpassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.roboballminiBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.voidbarnaclepassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.voidbarnaclemortarBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.voidjailerpassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.voidjailerBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.impbosspassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.impbossBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.stonetitanpassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.stonetitanBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.magmawormpassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.magmawormBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.overloadingwormpassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.overloadingwormBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.vagrantpassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.vagrantBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.acridpassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.acridBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.commandopassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.commandoBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.captainpassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.captainBuff.buffIndex, 0, -1);
        //          }
        //          if (skillListToOverrideOnRespawn[num] != Shiggy.loaderpassiveDef)
        //          {
        //              body.ApplyBuff(Buffs.loaderBuff.buffIndex, 0, -1);
        //          }
        //	//add buff
        //          if (skillListToOverrideOnRespawn[num] == Shiggy.alphacontructpassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.alphashieldonBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.beetlepassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.beetleBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.pestpassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.pestjumpBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.verminpassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.verminsprintBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.guppassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.gupspikeBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.hermitcrabpassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.hermitcrabmortarBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.larvapassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.larvajumpBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.lesserwisppassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.lesserwispBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.lunarexploderpassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.lunarexploderBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.minimushrumpassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.minimushrumBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.roboballminibpassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.roboballminiBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.voidbarnaclepassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.voidbarnaclemortarBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.voidjailerpassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.voidjailerBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.impbosspassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.impbossBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.stonetitanpassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.stonetitanBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.magmawormpassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.magmawormBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.overloadingwormpassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.overloadingwormBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.vagrantpassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.vagrantBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.acridpassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.acridBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.commandopassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.commandoBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.captainpassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.captainBuff.buffIndex, 1, -1);
        //	}
        //	if (skillListToOverrideOnRespawn[num] == Shiggy.loaderpassiveDef)
        //	{
        //		body.ApplyBuff(Buffs.loaderBuff.buffIndex, 1, -1);
        //	}
        //}

    }
}
