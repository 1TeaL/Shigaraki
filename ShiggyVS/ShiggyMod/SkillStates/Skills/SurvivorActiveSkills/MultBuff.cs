using ShiggyMod.Modules.Survivors;
using EntityStates;
using RoR2;
using UnityEngine;
using System.Collections.Generic;
using ShiggyMod.Modules;
using UnityEngine.Networking;
using RoR2.ExpansionManagement;
using ExtraSkillSlots;
using EntityStates.Toolbot;

namespace ShiggyMod.SkillStates
{
    public class MultBuff : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float baseDuration = 1f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;
        public HurtBox Target;
        private Animator animator;


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.decayDamageCoeffecient;
        private float procCoefficient = 1f;
        private float force = 1f;
        private float speedOverride = -1f;
        private uint loopSoundID;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            Shiggycon = gameObject.GetComponent<ShiggyController>();


            damageType = DamageType.Generic;
            base.OnEnter();
            if (NetworkServer.active)
            {
                characterBody.AddBuff(Modules.Buffs.multBuff);
            }

            this.loopSoundID = Util.PlaySound(ToolbotDualWield.startLoopSoundString, base.gameObject);

        }

        public override void OnExit()
        {
            base.OnExit();

            Util.PlaySound(ToolbotDualWield.exitSoundString, base.gameObject);
            Util.PlaySound(ToolbotDualWield.stopLoopSoundString, base.gameObject);
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