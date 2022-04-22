using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;

namespace ShiggyMod.SkillStates
{
    public class ChoiceScarf : BaseSkillState
    {
        public float duration = 0.1f;
        public ShiggyController Shiggycon;

        public override void OnEnter()
        {
            base.OnEnter();

            Shiggycon = base.GetComponent<ShiggyController>();
            if (!characterBody.HasBuff(Modules.Buffs.choicescarfBuff))
            {
                {
                    if (Shiggycon.choicescarf = false && base.skillLocator.secondary.skillNameToken == ShiggyPlugin.developerPrefix + "_Shiggy_BODY_CHOICESCARF_NAME")
                    {
                        Shiggycon.choicescarf = true;
                        characterBody.AddBuff(Modules.Buffs.choicescarfBuff);
                        AkSoundEngine.PostEvent(1531773223, this.gameObject);
                    }
                    if (Shiggycon.choicescarf2 = false && base.skillLocator.utility.skillNameToken == ShiggyPlugin.developerPrefix + "_Shiggy_BODY_CHOICESCARF_NAME")
                    {
                        Shiggycon.choicescarf2 = true;
                        characterBody.AddBuff(Modules.Buffs.choicescarfBuff);
                        AkSoundEngine.PostEvent(1531773223, this.gameObject);
                    }

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