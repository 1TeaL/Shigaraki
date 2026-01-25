using EntityStates;
using ExtraSkillSlots;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Orbs;
using RoR2.Skills;
using ShiggyMod.Modules.Networking;
using ShiggyMod.Modules.Quirks;
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
        public ApexSurgeryController Apexcon;
        private CharacterMaster characterMaster;
        private CharacterBody self;


		private ExtraSkillLocator extraskillLocator;

		public bool primarygiven;
		public bool secondarygiven;
		public bool utilitygiven;
		public bool specialgiven;
		public bool extra1given;
		public bool extra2given;
		public bool extra3given;
		public bool extra4given;

		private float checkQuirkTimer;

		public SkillDef[] skillListToOverrideOnRespawn;
		public SkillDef[] storedAFOSkill;


        


        public void Awake()
        {

			On.RoR2.CharacterBody.Start += CharacterBody_Start;
            On.RoR2.Run.Start += Run_Start;

            skillListToOverrideOnRespawn = new SkillDef[8];
            storedAFOSkill = new SkillDef[1];
            
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

            for (int i = 0; i < 8; i++) writeToSkillList(null, i);

            writeToAFOSkillList(null, 0);

            QuirkInventory.ResetSeedFlagForNextRun();   
            QuirkInventory.SeedStartingQuirksFromConfig();

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

			Apexcon = characterMaster.gameObject.GetComponent<ShiggyMod.Modules.Quirks.ApexSurgeryController>();
            if (!Apexcon)
			{
				characterMaster.gameObject.AddComponent<ShiggyMod.Modules.Quirks.ApexSurgeryController>();
			}

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

							if (Config.allowAllSkills.Value)
							{
								skillListToOverrideOnRespawn[0] = self.skillLocator.primary.skillDef;
                                skillListToOverrideOnRespawn[1] = self.skillLocator.secondary.skillDef;
                                skillListToOverrideOnRespawn[2] = self.skillLocator.utility.skillDef;
                                skillListToOverrideOnRespawn[3] = self.skillLocator.special.skillDef;
                                skillListToOverrideOnRespawn[4] = extraskillLocator.extraFirst.skillDef;
                                skillListToOverrideOnRespawn[5] = extraskillLocator.extraSecond.skillDef;
                                skillListToOverrideOnRespawn[6] = extraskillLocator.extraThird.skillDef;
                                skillListToOverrideOnRespawn[7] = extraskillLocator.extraFourth.skillDef;

                            }

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


                            }
                            CheckQuirksForBuffs(self);

                        }

					}

				}


			}
		}


		public void Update()
		{

            if (Input.GetKeyDown(Config.OpenQuirkMenuHotkey.Value.MainKey) && self.hasEffectiveAuthority)
            {
                if (!ShiggyMod.Modules.Quirks.QuirkUI.IsOpen)
                {
                    ShiggyMod.Modules.Quirks.QuirkUI.Show(self, extraskillLocator);
                    //(msg, dur) => energySystem.quirkGetInformation(msg, dur));
                }
            }
        }

		public void FixedUpdate()
		{

			if (characterMaster)
			{
				self = characterMaster.GetBody();
			}
			//if (self)
			//{
			//	if (extraskillLocator == null)
			//		extraskillLocator = self.gameObject.GetComponent<ExtraSkillLocator>();

			//	if (checkQuirkTimer < 1f)
			//	{
			//		checkQuirkTimer += Time.fixedDeltaTime;

			//	}
			//	else if (checkQuirkTimer >= 1f)
			//	{
			//		CheckQuirksForBuffs(self);
			//		checkQuirkTimer = 0f;
			//	}

			//}


			if (!characterMaster.gameObject)
			{
				Destroy(Shiggymastercon);
			}



        }
        public void GrantStockIfSkillPresent(CharacterBody body, SkillDef targetSkillDef, int stockToAdd = 1)
        {
            if (!NetworkServer.active || !body || !targetSkillDef)
                return;

            void TryAdd(GenericSkill skill)
            {
                if (!skill || skill.skillDef != targetSkillDef)
                    return;

                int newStock = Mathf.Min(skill.stock + stockToAdd, skill.maxStock);
                skill.stock = newStock;
            }

            // Vanilla skill slots
            var locator = body.skillLocator;
            if (locator)
            {
                TryAdd(locator.primary);
                TryAdd(locator.secondary);
                TryAdd(locator.utility);
                TryAdd(locator.special);
            }

            // Extra skill slots
            var extra = body.GetComponent<ExtraSkillSlots.ExtraSkillLocator>();
            if (extra)
            {
                TryAdd(extra.extraFirst);
                TryAdd(extra.extraSecond);
                TryAdd(extra.extraThird);
                TryAdd(extra.extraFourth);
            }
        }

        public void CheckQuirksForBuffs(CharacterBody body)
        {
            if (!body) return;

            // Get currently equipped quirks from slots
            var equipped = QuirkEquipUtils.GetEquippedQuirks(body);

            // Clear all registry-defined buffs
            foreach (var rec in QuirkRegistry.All.Values)
            {
                if (rec.Buff != null)
                    body.ApplyBuff(rec.Buff.buffIndex, 0);
            }

            // Apply buffs for the ones actually equipped
            foreach (var q in equipped)
            {
                if (QuirkRegistry.TryGet(q, out var rec) && rec.Buff != null)
                {
                    body.ApplyBuff(rec.Buff.buffIndex, 1);
                    // Debug: show which passive buff was applied
                    Debug.Log($"[CheckQuirksForBuffs] Applied {rec.Buff.name} from {q}");
                }
            }
        }

        //public void CheckQuirksForBuffs(CharacterBody characterBody)
        //{
        //	//check passive

        //	foreach (var skillname in StaticValues.passiveToBuff)
        //	{

        //		if (SearchSkillSlotsForQuirks(StaticValues.skillNameToSkillDef[skillname.Key], characterBody))
        //		{
        //			characterBody.ApplyBuff(StaticValues.passiveToBuff[skillname.Key].buffIndex, 1);
        //		}
        //		else if (SearchSkillSlotsForQuirks(StaticValues.skillNameToSkillDef[skillname.Key], characterBody))
        //		{
        //			characterBody.ApplyBuff(StaticValues.passiveToBuff[skillname.Key].buffIndex, 0);
        //		}
        //	}

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


    }
}
