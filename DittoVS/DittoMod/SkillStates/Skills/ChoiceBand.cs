using EntityStates;
using RoR2;
using UnityEngine;
using DittoMod.Modules.Survivors;

namespace DittoMod.SkillStates
{
    public class ChoiceBand : BaseSkillState
    {
        public float duration = 0.1f;
        public DittoController dittocon;

        public override void OnEnter()
        {
            base.OnEnter();

            dittocon = base.GetComponent<DittoController>();
            if (!characterBody.HasBuff(Modules.Buffs.choicebandBuff))
            {
                if (dittocon.choiceband = false && base.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_CHOICEBAND_NAME")
                {
                    dittocon.choiceband = true;
                    characterBody.AddBuff(Modules.Buffs.choicebandBuff);
                    AkSoundEngine.PostEvent(1531773223, this.gameObject);
                }
                if (dittocon.choiceband2 = false && base.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_CHOICEBAND_NAME")
                {
                    dittocon.choiceband2 = true;
                    characterBody.AddBuff(Modules.Buffs.choicebandBuff);
                    AkSoundEngine.PostEvent(1531773223, this.gameObject);
                }
            }


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
            return InterruptPriority.Skill;
        }

    }
}