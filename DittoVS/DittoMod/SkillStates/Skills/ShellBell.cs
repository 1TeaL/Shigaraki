using EntityStates;
using RoR2;
using UnityEngine;
using DittoMod.Modules.Survivors;

namespace DittoMod.SkillStates
{
    public class ShellBell : BaseSkillState
    {
        public float duration = 0.1f;
        public DittoController dittocon;

        public override void OnEnter()
        {
            base.OnEnter();

            dittocon = base.GetComponent<DittoController>();
            if (!characterBody.HasBuff(Modules.Buffs.shellbellBuff))
            {
                if (dittocon.shellbell = false && base.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_SHELLBELL_NAME")
                {
                    dittocon.shellbell = true;
                    characterBody.AddBuff(Modules.Buffs.shellbellBuff);
                    AkSoundEngine.PostEvent(1531773223, this.gameObject);
                }
                if (dittocon.shellbell2 = false && base.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_SHELLBELL_NAME")
                {
                    dittocon.shellbell2 = true;
                    characterBody.AddBuff(Modules.Buffs.shellbellBuff);
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