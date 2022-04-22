using EntityStates;
using RoR2;
using UnityEngine;
using DittoMod.Modules.Survivors;

namespace DittoMod.SkillStates
{
    public class ChoiceScarf : BaseSkillState
    {
        public float duration = 0.1f;
        public DittoController dittocon;

        public override void OnEnter()
        {
            base.OnEnter();

            dittocon = base.GetComponent<DittoController>();
            if (!characterBody.HasBuff(Modules.Buffs.choicescarfBuff))
            {
                {
                    if (dittocon.choicescarf = false && base.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_CHOICESCARF_NAME")
                    {
                        dittocon.choicescarf = true;
                        characterBody.AddBuff(Modules.Buffs.choicescarfBuff);
                        AkSoundEngine.PostEvent(1531773223, this.gameObject);
                    }
                    if (dittocon.choicescarf2 = false && base.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_CHOICESCARF_NAME")
                    {
                        dittocon.choicescarf2 = true;
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