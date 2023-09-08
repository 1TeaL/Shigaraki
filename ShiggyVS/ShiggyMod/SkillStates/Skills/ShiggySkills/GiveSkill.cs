using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using ExtraSkillSlots;
using R2API.Networking;
using ShiggyMod.Modules;
using IL.RoR2.Skills;

namespace ShiggyMod.SkillStates
{
    public class GiveSkill : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public ShiggyController Shiggycon;
        public ShiggyMasterController Shiggymastercon;
        private ExtraInputBankTest extrainputBankTest;
        private ExtraSkillLocator extraskillLocator;
        public CharacterBody enemycharBody;
        public EnergySystem energySystem;

        public override void OnEnter()
        {
            base.OnEnter();
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            energySystem = gameObject.GetComponent<EnergySystem>();
            Shiggymastercon = characterBody.master.gameObject.GetComponent<ShiggyMasterController>();
            extraskillLocator = base.GetComponent<ExtraSkillLocator>();
            extrainputBankTest = outer.GetComponent<ExtraInputBankTest>();
            enemycharBody = Shiggycon.giveQuirkBody;

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("RightArm, Override", "RightArmPull", "Attack.playbackRate", 0.5f, 0.1f);
                     

        }

        public override void OnExit()
        {
            base.OnExit();
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.giveDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.giveDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.giveDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.giveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.giveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.giveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.giveDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.giveDef, GenericSkill.SkillOverridePriority.Contextual);

            if (Shiggymastercon.skillListToOverrideOnRespawn[0] != null)
            {
                characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggymastercon.skillListToOverrideOnRespawn[0], GenericSkill.SkillOverridePriority.Contextual);
            }
            else
            {
                characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.decayDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (Shiggymastercon.skillListToOverrideOnRespawn[1] != null)
            {
                characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggymastercon.skillListToOverrideOnRespawn[1], GenericSkill.SkillOverridePriority.Contextual);
            }
            else
            {
                characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.bulletlaserDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (Shiggymastercon.skillListToOverrideOnRespawn[2] != null)
            {
                characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggymastercon.skillListToOverrideOnRespawn[2], GenericSkill.SkillOverridePriority.Contextual);
            }
            else
            {
                characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.aircannonDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (Shiggymastercon.skillListToOverrideOnRespawn[3] != null)
            {
                characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggymastercon.skillListToOverrideOnRespawn[3], GenericSkill.SkillOverridePriority.Contextual);
            }
            else
            {
                characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.multiplierDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (Shiggymastercon.skillListToOverrideOnRespawn[4] != null)
            {
                extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggymastercon.skillListToOverrideOnRespawn[4], GenericSkill.SkillOverridePriority.Contextual);
            }
            else
            {
                extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.emptySkillDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (Shiggymastercon.skillListToOverrideOnRespawn[5] != null)
            {
                extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggymastercon.skillListToOverrideOnRespawn[5], GenericSkill.SkillOverridePriority.Contextual);
            }
            else
            {
                extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.emptySkillDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (Shiggymastercon.skillListToOverrideOnRespawn[6] != null)
            {
                extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggymastercon.skillListToOverrideOnRespawn[6], GenericSkill.SkillOverridePriority.Contextual);
            }
            else
            {
                extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.emptySkillDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (Shiggymastercon.skillListToOverrideOnRespawn[7] != null)
            {
                extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggymastercon.skillListToOverrideOnRespawn[7], GenericSkill.SkillOverridePriority.Contextual);
            }
            else
            {
                extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.emptySkillDef, GenericSkill.SkillOverridePriority.Contextual);
            }

            Shiggymastercon.CheckQuirksForBuffs(characterBody);

            Shiggymastercon.writeToAFOSkillList(null, 0);
        }

        protected virtual void GiveSkill1()
        {
            RoR2.Skills.SkillDef skilldef = Shiggymastercon.skillListToOverrideOnRespawn[0];

            if (StaticValues.skillDict[skilldef.skillName] == StaticValues.skillType.PASSIVE)
            {
                BuffController buffcon = enemycharBody.gameObject.GetComponent<BuffController>();
                if (!buffcon)
                {
                    enemycharBody.gameObject.AddComponent<BuffController>();
                    
                }
                enemycharBody.ApplyBuff(StaticValues.passiveToBuff[skilldef.skillName].buffIndex, 1);
            }
            else
            {
                Chat.AddMessage("<style=cIsUtility>Not a Passive Quirk!</style>");
                energySystem.quirkGetInformation("<style=cIsUtility>Not a Passive Quirk!</style>", 2f);
            }

        }
        protected virtual void GiveSkill2()
        {
            RoR2.Skills.SkillDef skilldef = Shiggymastercon.skillListToOverrideOnRespawn[1];

            if (StaticValues.skillDict[skilldef.skillName] == StaticValues.skillType.PASSIVE)
            {
                BuffController buffcon = enemycharBody.gameObject.GetComponent<BuffController>();
                if (!buffcon)
                {
                    enemycharBody.gameObject.AddComponent<BuffController>();

                }
                enemycharBody.ApplyBuff(StaticValues.passiveToBuff[skilldef.skillName].buffIndex, 1);
            }
            else
            {
                Chat.AddMessage("<style=cIsUtility>Not a Passive Quirk!</style>");
                energySystem.quirkGetInformation("<style=cIsUtility>Not a Passive Quirk!</style>", 2f);
            }
        }
        protected virtual void GiveSkill3()
        {
            RoR2.Skills.SkillDef skilldef = Shiggymastercon.skillListToOverrideOnRespawn[2];

            if (StaticValues.skillDict[skilldef.skillName] == StaticValues.skillType.PASSIVE)
            {
                BuffController buffcon = enemycharBody.gameObject.GetComponent<BuffController>();
                if (!buffcon)
                {
                    enemycharBody.gameObject.AddComponent<BuffController>();

                }
                enemycharBody.ApplyBuff(StaticValues.passiveToBuff[skilldef.skillName].buffIndex, 1);
            }
            else
            {
                Chat.AddMessage("<style=cIsUtility>Not a Passive Quirk!</style>");
                energySystem.quirkGetInformation("<style=cIsUtility>Not a Passive Quirk!</style>", 2f);
            }
        }
        protected virtual void GiveSkill4()
        {
            RoR2.Skills.SkillDef skilldef = Shiggymastercon.skillListToOverrideOnRespawn[3];

            if (StaticValues.skillDict[skilldef.skillName] == StaticValues.skillType.PASSIVE)
            {
                BuffController buffcon = enemycharBody.gameObject.GetComponent<BuffController>();
                if (!buffcon)
                {
                    enemycharBody.gameObject.AddComponent<BuffController>();

                }
                enemycharBody.ApplyBuff(StaticValues.passiveToBuff[skilldef.skillName].buffIndex, 1);
            }
            else
            {
                Chat.AddMessage("<style=cIsUtility>Not a Passive Quirk!</style>");
                energySystem.quirkGetInformation("<style=cIsUtility>Not a Passive Quirk!</style>", 2f);
            }
        }
        protected virtual void GiveExtraSkill1()
        {
            RoR2.Skills.SkillDef skilldef = Shiggymastercon.skillListToOverrideOnRespawn[4];

            if (StaticValues.skillDict[skilldef.skillName] == StaticValues.skillType.PASSIVE)
            {
                BuffController buffcon = enemycharBody.gameObject.GetComponent<BuffController>();
                if (!buffcon)
                {
                    enemycharBody.gameObject.AddComponent<BuffController>();

                }
                enemycharBody.ApplyBuff(StaticValues.passiveToBuff[skilldef.skillName].buffIndex, 1);
            }
            else
            {
                Chat.AddMessage("<style=cIsUtility>Not a Passive Quirk!</style>");
                energySystem.quirkGetInformation("<style=cIsUtility>Not a Passive Quirk!</style>", 2f);
            }
        }
        protected virtual void GiveExtraSkill2()
        {
            RoR2.Skills.SkillDef skilldef = Shiggymastercon.skillListToOverrideOnRespawn[5];

            if (StaticValues.skillDict[skilldef.skillName] == StaticValues.skillType.PASSIVE)
            {
                BuffController buffcon = enemycharBody.gameObject.GetComponent<BuffController>();
                if (!buffcon)
                {
                    enemycharBody.gameObject.AddComponent<BuffController>();

                }
                enemycharBody.ApplyBuff(StaticValues.passiveToBuff[skilldef.skillName].buffIndex, 1);
            }
            else
            {
                Chat.AddMessage("<style=cIsUtility>Not a Passive Quirk!</style>");
                energySystem.quirkGetInformation("<style=cIsUtility>Not a Passive Quirk!</style>", 2f);
            }
        }
        protected virtual void GiveExtraSkill3()
        {
            RoR2.Skills.SkillDef skilldef = Shiggymastercon.skillListToOverrideOnRespawn[6];

            if (StaticValues.skillDict[skilldef.skillName] == StaticValues.skillType.PASSIVE)
            {
                BuffController buffcon = enemycharBody.gameObject.GetComponent<BuffController>();
                if (!buffcon)
                {
                    enemycharBody.gameObject.AddComponent<BuffController>();

                }
                enemycharBody.ApplyBuff(StaticValues.passiveToBuff[skilldef.skillName].buffIndex, 1);
            }
            else
            {
                Chat.AddMessage("<style=cIsUtility>Not a Passive Quirk!</style>");
                energySystem.quirkGetInformation("<style=cIsUtility>Not a Passive Quirk!</style>", 2f);
            }
        }
        protected virtual void GiveExtraSkill4()
        {
            RoR2.Skills.SkillDef skilldef = Shiggymastercon.skillListToOverrideOnRespawn[7];

            if (StaticValues.skillDict[skilldef.skillName] == StaticValues.skillType.PASSIVE)
            {
                BuffController buffcon = enemycharBody.gameObject.GetComponent<BuffController>();
                if (!buffcon)
                {
                    enemycharBody.gameObject.AddComponent<BuffController>();

                }
                enemycharBody.ApplyBuff(StaticValues.passiveToBuff[skilldef.skillName].buffIndex, 1);
            }
            else
            {
                Chat.AddMessage("<style=cIsUtility>Not a Passive Quirk!</style>");
                energySystem.quirkGetInformation("<style=cIsUtility>Not a Passive Quirk!</style>", 2f);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.isAuthority && base.IsKeyDownAuthority())
            {
                if (inputBank.skill1.down)
                {
                    GiveSkill1();

                }
                if (inputBank.skill2.down)
                {
                    GiveSkill2();

                }
                if (inputBank.skill3.down)
                {
                    GiveSkill3();

                }
                if (inputBank.skill4.down)
                {
                    GiveSkill4();

                }
                if (extrainputBankTest.extraSkill1.down)
                {
                    GiveExtraSkill1();

                }
                if (extrainputBankTest.extraSkill2.down)
                {
                    GiveExtraSkill2();

                }
                if (extrainputBankTest.extraSkill3.down)
                {
                    GiveExtraSkill3();

                }
                if (extrainputBankTest.extraSkill4.down)
                {
                    GiveExtraSkill4();

                }
                this.outer.SetNextStateToMain();
                return;
            }
        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }

    }
}
