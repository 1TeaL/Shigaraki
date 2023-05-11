using EntityStates;
using ExtraSkillSlots;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Orbs;
using RoR2.Skills;
using ShiggyMod.Modules.Networking;
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

		public bool alloyvultureWindBlastDef;
		public bool beetleguardslamDef;
		public bool bisonchargeDef;
		public bool bronzongballDef;
		public bool clayapothecarymortarDef;
		public bool claytemplarminigunDef;
		public bool greaterWispBuffDef;
		public bool impblinkDef;
		public bool jellyfishHealDef;
		public bool lemurianfireballDef;
		public bool lunargolemshotsDef;
		public bool lunarwispminigunDef;
		public bool parentteleportDef;
		public bool stonegolemlaserDef;
		public bool voidreaverportalDef;
		public bool beetlequeenSummonDef;
		public bool grandparentsunDef;
		public bool grovetenderChainDef;
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


			alloyvultureWindBlastDef = false;
			beetleguardslamDef = false;
			bisonchargeDef = false;
			bronzongballDef = false;
			clayapothecarymortarDef = false;
			claytemplarminigunDef = false;
			greaterWispBuffDef = false;
			impblinkDef = false;
			jellyfishHealDef = false;
			lemurianfireballDef = false;
			lunargolemshotsDef = false;
			lunarwispminigunDef = false;
			parentteleportDef = false;
			stonegolemlaserDef = false;
			voidreaverportalDef = false;

			beetlequeenSummonDef = false;
			grovetenderChainDef = false;
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


			alloyvultureWindBlastDef = false;
			beetleguardslamDef = false;
			bisonchargeDef = false;
			bronzongballDef = false;
			clayapothecarymortarDef = false;
			claytemplarminigunDef = false;
			greaterWispBuffDef = false;
			impblinkDef = false;
			jellyfishHealDef = false;
			lemurianfireballDef = false;
			lunargolemshotsDef = false;
			lunarwispminigunDef = false;
			parentteleportDef = false;
			stonegolemlaserDef = false;
			voidreaverportalDef = false;

			beetlequeenSummonDef = false;            
			grovetenderChainDef = false;
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
                                CheckQuirksForBuffs(self);


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


        public void CheckQuirksForBuffs(CharacterBody characterBody)
        {
            //check passive

			foreach (var skillname in StaticValues.passiveToBuff)
			{

                if (SearchSkillSlotsForQuirks(StaticValues.skillNameToSkillDef[skillname.Key], characterBody))
                {
                    characterBody.ApplyBuff(StaticValues.passiveToBuff[skillname.Key].buffIndex, 1);
                }
                else if (SearchSkillSlotsForQuirks(StaticValues.skillNameToSkillDef[skillname.Key], characterBody))
                {
                    characterBody.ApplyBuff(StaticValues.passiveToBuff[skillname.Key].buffIndex, 0);
                }
            }

			//foreach (KeyValuePair<string, BuffDef> entry in StaticValues.passiveToBuff)
			//{
   //             if (SearchSkillSlotsForQuirks(StaticValues.skillNameToSkillDef[entry.Key], characterBody))
   //             {
   //                 characterBody.ApplyBuff(StaticValues.passiveToBuff[entry.Key].buffIndex, 1);
   //             }
   //             else if (SearchSkillSlotsForQuirks(StaticValues.skillNameToSkillDef[entry.Key], characterBody))
   //             {
   //                 characterBody.ApplyBuff(StaticValues.passiveToBuff[entry.Key].buffIndex, 0);
   //             }

                
   //         }

   //         SkillDef primarySkill = characterBody.skillLocator.primary.skillDef;
   //         string primaryName = characterBody.skillLocator.primary.skillDef.name;
			//if (StaticValues.skillDict[primaryName] == StaticValues.skillType.PASSIVE)
			//{
			//	if(SearchQuirksForBuffs(primarySkill, characterBody))
   //             {
   //                 characterBody.ApplyBuff(StaticValues.passiveToBuff[primaryName], 1);
   //             }
			//}

   //         if (SearchQuirksForBuffs(StaticValues.b, characterBody))
   //         {
   //             if (NetworkServer.active)
   //             {
   //                 characterBody.ApplyBuff(Modules.Buffs.alphashieldonBuff.buffIndex);

   //             }
   //         }
   //         else
   //         {
   //             if (NetworkServer.active && characterBody.HasBuff(Modules.Buffs.alphashieldonBuff))
   //             {
   //                 characterBody.ApplyBuff(Modules.Buffs.alphashieldonBuff.buffIndex, 0);
   //             }

   //         }
        }
        //public bool IsQuirkHave(string skillName, CharacterBody characterBody)
        //{
        //    extraskillLocator = characterBody.gameObject.GetComponent<ExtraSkillLocator>();

        //    return !(extraskillLocator.extraFirst.skillNameToken != prefix + skillName
        //        && extraskillLocator.extraSecond.skillNameToken != prefix + skillName
        //        && extraskillLocator.extraThird.skillNameToken != prefix + skillName
        //        && extraskillLocator.extraFourth.skillNameToken != prefix + skillName
        //        && characterBody.skillLocator.primary.skillNameToken != prefix + skillName
        //        && characterBody.skillLocator.secondary.skillNameToken != prefix + skillName
        //        && characterBody.skillLocator.utility.skillNameToken != prefix + skillName
        //        && characterBody.skillLocator.special.skillNameToken != prefix + skillName);
        //}

        public bool SearchSkillSlotsForQuirks(SkillDef skillDef, CharacterBody characterBody)
        {
            extraskillLocator = characterBody.gameObject.GetComponent<ExtraSkillLocator>();

            return !(extraskillLocator.extraFirst.skillDef != skillDef
                && extraskillLocator.extraSecond.skillDef != skillDef
                && extraskillLocator.extraThird.skillDef != skillDef
                && extraskillLocator.extraFourth.skillDef != skillDef
                && characterBody.skillLocator.primary.skillDef != skillDef
                && characterBody.skillLocator.secondary.skillDef != skillDef
                && characterBody.skillLocator.utility.skillDef != skillDef
                && characterBody.skillLocator.special.skillDef != skillDef);
        }

        //public bool SearchSpecialQuirks(SkillDef skillDef1, SkillDef skillDef2, SkillDef skillDef3, SkillDef skillDef4, CharacterBody characterBody)
        //{
        //    extraskillLocator = characterBody.gameObject.GetComponent<ExtraSkillLocator>();

        //    return !(extraskillLocator.extraFirst.skillDef != skillDef
        //        && extraskillLocator.extraSecond.skillDef != skillDef
        //        && extraskillLocator.extraThird.skillDef != skillDef
        //        && extraskillLocator.extraFourth.skillDef != skillDef
        //        && characterBody.skillLocator.primary.skillDef != skillDef
        //        && characterBody.skillLocator.secondary.skillDef != skillDef
        //        && characterBody.skillLocator.utility.skillDef != skillDef
        //        && characterBody.skillLocator.special.skillDef != skillDef);
        //}

    }
}
