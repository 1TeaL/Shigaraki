using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;

namespace ShiggyMod.SkillStates
{
    public class ChoiceBand : BaseSkillState
    {
        public float duration = 0.1f;
        public ShiggyController Shiggycon;

        public override void OnEnter()
        {
            base.OnEnter();

            Shiggycon = base.GetComponent<ShiggyController>();
            if (!characterBody.HasBuff(Modules.Buffs.choicebandBuff))
            {
                if (Shiggycon.choiceband = false && base.skillLocator.secondary.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_CHOICEBAND_NAME")
                {
                    Shiggycon.choiceband = true;
                    characterBody.AddBuff(Modules.Buffs.choicebandBuff);
                    AkSoundEngine.PostEvent(1531773223, this.gameObject);
                }
                if (Shiggycon.choiceband2 = false && base.skillLocator.utility.skillNameToken == ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_CHOICEBAND_NAME")
                {
                    Shiggycon.choiceband2 = true;
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