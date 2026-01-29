using ExtraSkillSlots;
using RoR2;
using RoR2.Skills;
using ShiggyMod.Modules.Quirks;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.Modules.Survivors
{
    public class ShiggyMasterController : MonoBehaviour
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";


        public QuirkInventory QuirkInventory;
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



        private static bool _hooksInstalled;


        public void Awake()
        {

            if (!_hooksInstalled)
            {
                _hooksInstalled = true;
                On.RoR2.CharacterBody.Start += CharacterBody_Start;
                On.RoR2.Run.Start += Run_Start;
            }

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

        //public void OnDestroy()
        //{
        //    On.RoR2.CharacterBody.Start -= CharacterBody_Start;
        //    On.RoR2.Run.Start -= Run_Start;
        //}

        private void OnDestroy()
        {
            On.RoR2.CharacterBody.Start -= CharacterBody_Start;
        }

        public void Run_Start(On.RoR2.Run.orig_Start orig, Run self)
        {
            orig.Invoke(self);

            for (int i = 0; i < 8; i++) writeToSkillList(null, i);

            writeToAFOSkillList(null, 0);

            ShiggyMod.Modules.Quirks.QuirkInventory.Ensure(characterMaster);
            if (NetworkServer.active)
                GetComponent<QuirkInventory>()?.Server_SeedStartingQuirksFromConfig();

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
            //self = characterMaster.GetBody();

            Shiggymastercon = characterMaster.gameObject.GetComponent<ShiggyMasterController>();
            //Shiggycon = self.gameObject.GetComponent<ShiggyController>();

            //extraskillLocator = self.gameObject.GetComponent<ExtraSkillLocator>();

            Apexcon = characterMaster.gameObject.GetComponent<ShiggyMod.Modules.Quirks.ApexSurgeryController>();
            if (!Apexcon)
                Apexcon = characterMaster.gameObject.AddComponent<ShiggyMod.Modules.Quirks.ApexSurgeryController>();


            ShiggyMod.Modules.Quirks.QuirkInventory.Ensure(characterMaster);
            if (NetworkServer.active)
                GetComponent<QuirkInventory>()?.Server_SeedStartingQuirksFromConfig();
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


                            Shiggycon = self.GetComponent<ShiggyController>();
                            if (!Shiggycon) Shiggycon = self.gameObject.AddComponent<ShiggyController>();

                            extraskillLocator = self.GetComponent<ExtraSkillLocator>();



                            //if (Config.allowAllSkills.Value)
                            //{
                            //    skillListToOverrideOnRespawn[0] = self.skillLocator.primary.skillDef;
                            //    skillListToOverrideOnRespawn[1] = self.skillLocator.secondary.skillDef;
                            //    skillListToOverrideOnRespawn[2] = self.skillLocator.utility.skillDef;
                            //    skillListToOverrideOnRespawn[3] = self.skillLocator.special.skillDef;
                            //    skillListToOverrideOnRespawn[4] = extraskillLocator.extraFirst.skillDef;
                            //    skillListToOverrideOnRespawn[5] = extraskillLocator.extraSecond.skillDef;
                            //    skillListToOverrideOnRespawn[6] = extraskillLocator.extraThird.skillDef;
                            //    skillListToOverrideOnRespawn[7] = extraskillLocator.extraFourth.skillDef;

                            //}

                            if (Config.retainLoadout.Value)
                            {
                                ApplyRespawnLoadout(self);
                                //if (skillListToOverrideOnRespawn[0] != null && !primarygiven)
                                //{
                                //    primarygiven = true;
                                //    self.skillLocator.primary.SetSkillOverride(self.skillLocator.primary, skillListToOverrideOnRespawn[0], GenericSkill.SkillOverridePriority.Contextual);
                                //}
                                //else if (skillListToOverrideOnRespawn[0] == null && !primarygiven)
                                //{
                                //    primarygiven = true;
                                //    self.skillLocator.primary.SetSkillOverride(self.skillLocator.primary, Shiggy.decayDef, GenericSkill.SkillOverridePriority.Contextual);
                                //}
                                //if (skillListToOverrideOnRespawn[1] != null && !secondarygiven)
                                //{
                                //    secondarygiven = true;
                                //    self.skillLocator.secondary.SetSkillOverride(self.skillLocator.secondary, skillListToOverrideOnRespawn[1], GenericSkill.SkillOverridePriority.Contextual);
                                //}
                                //else if (skillListToOverrideOnRespawn[1] == null && !secondarygiven)
                                //{
                                //    secondarygiven = true;
                                //    self.skillLocator.secondary.SetSkillOverride(self.skillLocator.secondary, Shiggy.bulletlaserDef, GenericSkill.SkillOverridePriority.Contextual);
                                //}
                                //if (skillListToOverrideOnRespawn[2] != null && !utilitygiven)
                                //{
                                //    utilitygiven = true;
                                //    self.skillLocator.utility.SetSkillOverride(self.skillLocator.utility, skillListToOverrideOnRespawn[2], GenericSkill.SkillOverridePriority.Contextual);
                                //}
                                //else if (skillListToOverrideOnRespawn[2] == null && !utilitygiven)
                                //{
                                //    utilitygiven = true;
                                //    self.skillLocator.utility.SetSkillOverride(self.skillLocator.utility, Shiggy.aircannonDef, GenericSkill.SkillOverridePriority.Contextual);
                                //}
                                //if (skillListToOverrideOnRespawn[3] != null && !specialgiven)
                                //{
                                //    specialgiven = true;
                                //    self.skillLocator.special.SetSkillOverride(self.skillLocator.special, skillListToOverrideOnRespawn[3], GenericSkill.SkillOverridePriority.Contextual);
                                //}
                                //else if (skillListToOverrideOnRespawn[3] == null && !specialgiven)
                                //{
                                //    specialgiven = true;
                                //    self.skillLocator.special.SetSkillOverride(self.skillLocator.special, Shiggy.multiplierDef, GenericSkill.SkillOverridePriority.Contextual);
                                //}
                                //if (skillListToOverrideOnRespawn[4] != null && !extra1given)
                                //{
                                //    extra1given = true;
                                //    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, skillListToOverrideOnRespawn[4], GenericSkill.SkillOverridePriority.Contextual);
                                //}
                                //else if (skillListToOverrideOnRespawn[4] == null && !extra1given)
                                //{
                                //    extra1given = true;
                                //    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.emptySkillDef, GenericSkill.SkillOverridePriority.Contextual);
                                //}
                                //if (skillListToOverrideOnRespawn[5] != null && !extra2given)
                                //{
                                //    extra2given = true;
                                //    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, skillListToOverrideOnRespawn[5], GenericSkill.SkillOverridePriority.Contextual);
                                //}
                                //else if (skillListToOverrideOnRespawn[5] == null && !extra2given)
                                //{
                                //    extra2given = true;
                                //    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.emptySkillDef, GenericSkill.SkillOverridePriority.Contextual);
                                //}
                                //if (skillListToOverrideOnRespawn[6] != null && !extra3given)
                                //{
                                //    extra3given = true;
                                //    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, skillListToOverrideOnRespawn[6], GenericSkill.SkillOverridePriority.Contextual);
                                //}
                                //else if (skillListToOverrideOnRespawn[6] == null && !extra3given)
                                //{
                                //    extra3given = true;
                                //    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.emptySkillDef, GenericSkill.SkillOverridePriority.Contextual);
                                //}
                                //if (skillListToOverrideOnRespawn[7] != null && !extra4given)
                                //{
                                //    extra4given = true;
                                //    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, skillListToOverrideOnRespawn[7], GenericSkill.SkillOverridePriority.Contextual);
                                //}
                                //else if (skillListToOverrideOnRespawn[7] == null && !extra4given)
                                //{
                                //    extra4given = true;
                                //    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.emptySkillDef, GenericSkill.SkillOverridePriority.Contextual);
                                //}


                            }
                            if (NetworkServer.active)
                                QuirkPassiveSync.SyncFromEquippedSkillsServer(self);

                        }

                    }

                }


            }
        }


        public void Update()
        {
            var body = characterMaster ? characterMaster.GetBody() : null;

            if (body && body.hasEffectiveAuthority && Input.GetKeyDown(Config.OpenQuirkMenuHotkey.Value.MainKey))
            {
                if (!ShiggyMod.Modules.Quirks.QuirkUI.IsOpen)
                {
                    ShiggyMod.Modules.Quirks.QuirkUI.Show(body, extraskillLocator);
                    // or: QuirkUI.Show(body, extraskillLocator, (msg, dur) => energySystem.quirkGetInformation(msg, dur));
                }
            }
        }


        public void FixedUpdate()
        {

            //if (characterMaster.GetBody())
            //{
            //    self = characterMaster.GetBody();
            //}
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

        [Server]
        public void SaveCurrentSlotsToRespawnList(CharacterBody body)
        {
            if (!NetworkServer.active || !body) return;

            var ex = body.GetComponent<ExtraSkillLocator>();

            writeToSkillList(body.skillLocator.primary?.skillDef, 0);
            writeToSkillList(body.skillLocator.secondary?.skillDef, 1);
            writeToSkillList(body.skillLocator.utility?.skillDef, 2);
            writeToSkillList(body.skillLocator.special?.skillDef, 3);

            writeToSkillList(ex ? ex.extraFirst?.skillDef : null, 4);
            writeToSkillList(ex ? ex.extraSecond?.skillDef : null, 5);
            writeToSkillList(ex ? ex.extraThird?.skillDef : null, 6);
            writeToSkillList(ex ? ex.extraFourth?.skillDef : null, 7);
        }
        private static readonly object OverrideSource = new object();
        [Server]
        public void ApplyRespawnLoadout(CharacterBody body)
        {
            if (!NetworkServer.active || !body) return;

            var ex = body.GetComponent<ExtraSkillLocator>();

            SkillDef GetOrDefault(int idx, SkillDef fallback) =>
                skillListToOverrideOnRespawn[idx] != null ? skillListToOverrideOnRespawn[idx] : fallback;

            body.skillLocator.primary.SetSkillOverride(OverrideSource, GetOrDefault(0, Shiggy.decayDef), GenericSkill.SkillOverridePriority.Contextual);
            body.skillLocator.secondary.SetSkillOverride(OverrideSource, GetOrDefault(1, Shiggy.bulletlaserDef), GenericSkill.SkillOverridePriority.Contextual);
            body.skillLocator.utility.SetSkillOverride(OverrideSource, GetOrDefault(2, Shiggy.aircannonDef), GenericSkill.SkillOverridePriority.Contextual);
            body.skillLocator.special.SetSkillOverride(OverrideSource, GetOrDefault(3, Shiggy.multiplierDef), GenericSkill.SkillOverridePriority.Contextual);

            if (ex)
            {
                ex.extraFirst.SetSkillOverride(OverrideSource, GetOrDefault(4, Shiggy.emptySkillDef), GenericSkill.SkillOverridePriority.Contextual);
                ex.extraSecond.SetSkillOverride(OverrideSource, GetOrDefault(5, Shiggy.emptySkillDef), GenericSkill.SkillOverridePriority.Contextual);
                ex.extraThird.SetSkillOverride(OverrideSource, GetOrDefault(6, Shiggy.emptySkillDef), GenericSkill.SkillOverridePriority.Contextual);
                ex.extraFourth.SetSkillOverride(OverrideSource, GetOrDefault(7, Shiggy.emptySkillDef), GenericSkill.SkillOverridePriority.Contextual);
            }

            QuirkPassiveSync.SyncFromEquippedSkillsServer(body);
            body.RecalculateStats();
        }


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
