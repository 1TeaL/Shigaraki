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
    public class MultBuff : BaseSkillState
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
        public static uint loopSoundID;
        public static GameObject coverLeftInstance;
        public static GameObject coverRightInstance;
        private ExtraSkillLocator extraskillLocator;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            Shiggycon = gameObject.GetComponent<ShiggyController>();


            base.OnEnter();
            if (!characterBody.HasBuff(Buffs.multBuff.buffIndex))
            {
                characterBody.ApplyBuff(Modules.Buffs.multBuff.buffIndex, 1);
                Util.PlaySound(ToolbotDualWield.enterSoundString, base.gameObject);
                EffectManager.SimpleMuzzleFlash(Modules.Assets.multEffect, base.gameObject, "LHand", false);
                EffectManager.SimpleMuzzleFlash(Modules.Assets.multEffect, base.gameObject, "RHand", false);
                loopSoundID = Util.PlaySound(ToolbotDualWield.startLoopSoundString, base.gameObject);
            }
            else if (characterBody.HasBuff(Buffs.multBuff.buffIndex))
            {
                characterBody.ApplyBuff(Modules.Buffs.multBuff.buffIndex, 0);
                AkSoundEngine.StopPlayingID(MultBuff.loopSoundID);
                Util.PlaySound(ToolbotDualWieldEnd.enterSfx, base.gameObject);
            }

            //if (ToolbotDualWield.coverPrefab)
            //{
            //    Transform transform = base.FindModelChild("LHand");
            //    Transform transform2 = base.FindModelChild("RHand");
            //    if (transform)
            //    {
            //        coverLeftInstance = UnityEngine.Object.Instantiate<GameObject>(ToolbotDualWield.coverPrefab, transform.position, transform.rotation, transform);
            //    }
            //    if (transform2)
            //    {
            //        coverRightInstance = UnityEngine.Object.Instantiate<GameObject>(ToolbotDualWield.coverPrefab, transform2.position, transform2.rotation, transform2);
            //    }
            //}

        }

        public override void OnExit()
        {
            base.OnExit();


            if (characterBody.HasBuff(Buffs.multBuff.buffIndex))
            {
                Util.PlaySound(ToolbotDualWield.exitSoundString, base.gameObject);
                Util.PlaySound(ToolbotDualWield.stopLoopSoundString, base.gameObject);
            }
            else if (!characterBody.HasBuff(Buffs.multBuff.buffIndex))
            {
                AkSoundEngine.StopPlayingID(MultBuff.loopSoundID);
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