using ShiggyMod.Modules.Survivors;
using EntityStates;
using RoR2;
using UnityEngine;
using System.Collections.Generic;
using ShiggyMod.Modules;
using UnityEngine.Networking;
using RoR2.ExpansionManagement;
using ExtraSkillSlots;
using ShiggyMod.Modules.Networking;
using R2API.Networking;
using R2API.Networking.Interfaces;

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