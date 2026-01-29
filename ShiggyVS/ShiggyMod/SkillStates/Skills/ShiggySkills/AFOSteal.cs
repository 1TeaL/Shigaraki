using EntityStates;
using ExtraSkillSlots;
using R2API.Networking;
using RoR2;
using RoR2.Skills;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;

namespace ShiggyMod.SkillStates
{
    public class AFOSteal : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public ShiggyController Shiggycon;
        public ShiggyMasterController Shiggymastercon;
        private ExtraInputBankTest extrainputBankTest;
        private ExtraSkillLocator extraskillLocator;

        public override void OnEnter()
        {
            base.OnEnter();
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            Shiggymastercon = characterBody.master.gameObject.GetComponent<ShiggyMasterController>();
            extraskillLocator = base.GetComponent<ExtraSkillLocator>();
            extrainputBankTest = outer.GetComponent<ExtraInputBankTest>();

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("FullBody, Override", "AFOSteal", "Attack.playbackRate", 1f, 0.1f);

            characterBody.ApplyBuff(RoR2Content.Buffs.HiddenInvincibility.buffIndex, 1, 1f);

        }

        public override void OnExit()
        {
            base.OnExit();
            //characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
            //characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
            //characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
            //characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
            //extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
            //extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
            //extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);
            //extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.chooseDef, GenericSkill.SkillOverridePriority.Contextual);

            //if (Shiggymastercon.skillListToOverrideOnRespawn[0] != null)
            //{
            //    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggymastercon.skillListToOverrideOnRespawn[0], GenericSkill.SkillOverridePriority.Contextual);
            //}
            //else
            //{
            //    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.decayDef, GenericSkill.SkillOverridePriority.Contextual);
            //}
            //if (Shiggymastercon.skillListToOverrideOnRespawn[1] != null)
            //{
            //    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggymastercon.skillListToOverrideOnRespawn[1], GenericSkill.SkillOverridePriority.Contextual);
            //}
            //else
            //{
            //    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.bulletlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            //}
            //if (Shiggymastercon.skillListToOverrideOnRespawn[2] != null)
            //{
            //    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggymastercon.skillListToOverrideOnRespawn[2], GenericSkill.SkillOverridePriority.Contextual);
            //}
            //else
            //{
            //    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.aircannonDef, GenericSkill.SkillOverridePriority.Contextual);
            //}
            //if (Shiggymastercon.skillListToOverrideOnRespawn[3] != null)
            //{
            //    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggymastercon.skillListToOverrideOnRespawn[3], GenericSkill.SkillOverridePriority.Contextual);
            //}
            //else
            //{
            //    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.multiplierDef, GenericSkill.SkillOverridePriority.Contextual);
            //}
            //if (Shiggymastercon.skillListToOverrideOnRespawn[4] != null)
            //{
            //    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggymastercon.skillListToOverrideOnRespawn[4], GenericSkill.SkillOverridePriority.Contextual);
            //}
            //else
            //{
            //    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.emptySkillDef, GenericSkill.SkillOverridePriority.Contextual);
            //}
            //if (Shiggymastercon.skillListToOverrideOnRespawn[5] != null)
            //{
            //    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggymastercon.skillListToOverrideOnRespawn[5], GenericSkill.SkillOverridePriority.Contextual);
            //}
            //else
            //{
            //    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.emptySkillDef, GenericSkill.SkillOverridePriority.Contextual);
            //}
            //if (Shiggymastercon.skillListToOverrideOnRespawn[6] != null)
            //{
            //    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggymastercon.skillListToOverrideOnRespawn[6], GenericSkill.SkillOverridePriority.Contextual);
            //}
            //else
            //{
            //    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.emptySkillDef, GenericSkill.SkillOverridePriority.Contextual);
            //}
            //if (Shiggymastercon.skillListToOverrideOnRespawn[7] != null)
            //{
            //    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggymastercon.skillListToOverrideOnRespawn[7], GenericSkill.SkillOverridePriority.Contextual);
            //}
            //else
            //{
            //    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.emptySkillDef, GenericSkill.SkillOverridePriority.Contextual);
            //}

            //CheckQuirksForBuffs(characterBody);

            //Shiggymastercon.writeToAFOSkillList(null, 0);
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

        protected virtual void AddSkill1()
        {
            Shiggymastercon.writeToSkillList(Shiggymastercon.storedAFOSkill[0], 0);
            this.outer.SetNextStateToMain();
            return;
        }
        protected virtual void AddSkill2()
        {
            Shiggymastercon.writeToSkillList(Shiggymastercon.storedAFOSkill[0], 1);
            this.outer.SetNextStateToMain();
            return;
        }
        protected virtual void AddSkill3()
        {
            Shiggymastercon.writeToSkillList(Shiggymastercon.storedAFOSkill[0], 2);
            this.outer.SetNextStateToMain();
            return;
        }
        protected virtual void AddSkill4()
        {
            Shiggymastercon.writeToSkillList(Shiggymastercon.storedAFOSkill[0], 3);
            this.outer.SetNextStateToMain();
            return;
        }
        protected virtual void AddExtraSkill1()
        {
            Shiggymastercon.writeToSkillList(Shiggymastercon.storedAFOSkill[0], 4);
            this.outer.SetNextStateToMain();
            return;
        }
        protected virtual void AddExtraSkill2()
        {
            Shiggymastercon.writeToSkillList(Shiggymastercon.storedAFOSkill[0], 5);
            this.outer.SetNextStateToMain();
            return;
        }
        protected virtual void AddExtraSkill3()
        {
            Shiggymastercon.writeToSkillList(Shiggymastercon.storedAFOSkill[0], 6);
            this.outer.SetNextStateToMain();
            return;
        }
        protected virtual void AddExtraSkill4()
        {
            Shiggymastercon.writeToSkillList(Shiggymastercon.storedAFOSkill[0], 7);
            this.outer.SetNextStateToMain();
            return;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge > 1f)
            {
                this.outer.SetNextStateToMain();
                return;

            }

            if (base.isAuthority)
            {
                //if (inputBank.skill1.down)
                //{
                //    AddSkill1();

                //}
                //if (inputBank.skill2.down)
                //{
                //    AddSkill2();

                //}
                //if (inputBank.skill3.down)
                //{
                //    AddSkill3();

                //}
                //if (inputBank.skill4.down)
                //{
                //    AddSkill4();

                //}
                //if (extrainputBankTest.extraSkill1.down)
                //{
                //    AddExtraSkill1();

                //}
                //if (extrainputBankTest.extraSkill2.down)
                //{
                //    AddExtraSkill2();

                //}
                //if (extrainputBankTest.extraSkill3.down)
                //{
                //    AddExtraSkill3();

                //}
                //if (extrainputBankTest.extraSkill4.down)
                //{
                //    AddExtraSkill4();

                //}
            }
        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
