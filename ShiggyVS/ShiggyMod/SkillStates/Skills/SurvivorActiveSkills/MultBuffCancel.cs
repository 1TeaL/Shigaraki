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
using R2API.Networking;

namespace ShiggyMod.SkillStates
{
    public class MultBuffCancel : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float baseDuration = 0.1f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;
        public HurtBox Target;
        private Animator animator;


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.decayDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 1f;
        private float speedOverride = -1f;
        private uint loopSoundID;
        private GameObject coverLeftInstance;
        private GameObject coverRightInstance;

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
                characterBody.ApplyBuff(Modules.Buffs.multBuff.buffIndex, 0);
            }

            //this.loopSoundID = Util.PlaySound(ToolbotDualWield.startLoopSoundString, base.gameObject);

        }

        public override void OnExit()
        {
            base.OnExit();
            EffectManager.SimpleMuzzleFlash(ToolbotDualWield.coverEjectEffect, base.gameObject, "LHand", false);
            EffectManager.SimpleMuzzleFlash(ToolbotDualWield.coverEjectEffect, base.gameObject, "RHand", false);
            //if (MultBuff.coverLeftInstance)
            //{
            //    EffectManager.SimpleMuzzleFlash(ToolbotDualWield.coverEjectEffect, base.gameObject, "LHand", false);
            //    EntityState.Destroy(MultBuff.coverLeftInstance);
            //}
            //if (MultBuff.coverRightInstance)
            //{
            //    EffectManager.SimpleMuzzleFlash(ToolbotDualWield.coverEjectEffect, base.gameObject, "RHand", false);
            //    EntityState.Destroy(MultBuff.coverRightInstance);
            //}
            AkSoundEngine.StopPlayingID(MultBuff.loopSoundID);
            Util.PlaySound(ToolbotDualWield.exitSoundString, base.gameObject);
            Util.PlaySound(ToolbotDualWield.stopLoopSoundString, base.gameObject);

            if (base.skillLocator.primary.skillNameToken == prefix + "MULTBUFFCANCEL_NAME")
            {
                characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.multbuffcancelDef, GenericSkill.SkillOverridePriority.Contextual);
                characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (base.skillLocator.secondary.skillNameToken == prefix + "MULTBUFFCANCEL_NAME")
            {
                characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.multbuffcancelDef, GenericSkill.SkillOverridePriority.Contextual);
                characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (base.skillLocator.utility.skillNameToken == prefix + "MULTBUFFCANCEL_NAME")
            {
                characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.multbuffcancelDef, GenericSkill.SkillOverridePriority.Contextual);
                characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (base.skillLocator.special.skillNameToken == prefix + "MULTBUFFCANCEL_NAME")
            {
                characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.multbuffcancelDef, GenericSkill.SkillOverridePriority.Contextual);
                characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.multbuffDef, GenericSkill.SkillOverridePriority.Contextual);
            }
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