using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using ExtraSkillSlots;

namespace ShiggyMod.SkillStates
{
    public class RemoveSkill : BaseSkillState
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



        }

        public override void OnExit()
        {
            base.OnExit();
            characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
            characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);
            extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.removeDef, GenericSkill.SkillOverridePriority.Contextual);

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
        }

        protected virtual void RemoveSkill1()
        {
            Shiggymastercon.writeToSkillList(null, 0);

        }
        protected virtual void RemoveSkill2()
        {
            Shiggymastercon.writeToSkillList(null, 1);
            this.outer.SetNextStateToMain();
            return;
        }
        protected virtual void RemoveSkill3()
        {
            Shiggymastercon.writeToSkillList(null, 2);
            this.outer.SetNextStateToMain();
            return;
        }
        protected virtual void RemoveSkill4()
        {
            Shiggymastercon.writeToSkillList(null, 3);
            this.outer.SetNextStateToMain();
            return;
        }
        protected virtual void RemoveExtraSkill1()
        {
            Shiggymastercon.writeToSkillList(null, 4);
            this.outer.SetNextStateToMain();
            return;
        }
        protected virtual void RemoveExtraSkill2()
        {
            Shiggymastercon.writeToSkillList(null, 5);
            this.outer.SetNextStateToMain();
            return;
        }
        protected virtual void RemoveExtraSkill3()
        {
            Shiggymastercon.writeToSkillList(null, 6);
            this.outer.SetNextStateToMain();
            return;
        }
        protected virtual void RemoveExtraSkill4()
        {
            Shiggymastercon.writeToSkillList(null, 7);
            this.outer.SetNextStateToMain();
            return;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.isAuthority)
            {
                if (inputBank.skill1.down)
                {
                    RemoveSkill1();

                }
                if (inputBank.skill2.down)
                {
                    RemoveSkill2();

                }
                if (inputBank.skill3.down)
                {
                    RemoveSkill3();

                }
                if (inputBank.skill4.down)
                {
                    RemoveSkill4();

                }
                if (extrainputBankTest.extraSkill1.down)
                {
                    RemoveExtraSkill1();

                }
                if (extrainputBankTest.extraSkill2.down)
                {
                    RemoveExtraSkill2();

                }
                if (extrainputBankTest.extraSkill3.down)
                {
                    RemoveExtraSkill3();

                }
                if (extrainputBankTest.extraSkill4.down)
                {
                    RemoveExtraSkill4();

                }
            }
        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
