using EntityStates;
using ExtraSkillSlots;
using RoR2;
using ShiggyMod.Modules.Survivors;

namespace ShiggyMod.SkillStates
{
    public class EmptySkill : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";

        private float duration = 0.1f;
        private ExtraInputBankTest extrainputBankTest;
        private ExtraSkillLocator extraskillLocator;
        public ShiggyController Shiggycon;
        public ShiggyMasterController Shiggymastercon;
        public HurtBox Target;


        public override void OnEnter()
        {
            base.OnEnter();

        }

        public override void OnExit()
        {
            base.OnExit();
        }



        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }




        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}