using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;

namespace ShiggyMod.SkillStates
{
    public class AssaultVest : BaseSkillState
    {
        public float duration = 0.1f;
        public ShiggyController Shiggycon;

        public override void OnEnter()
        {
            base.OnEnter();

            Shiggycon = base.GetComponent<ShiggyController>();
            if (!characterBody.HasBuff(Modules.Buffs.leftoversBuff))
            {
                if (Shiggycon.leftovers = false && base.skillLocator.secondary.skillNameToken == ShiggyPlugin.developerPrefix + "_Shiggy_BODY_LEFTOVERS_NAME")
                {
                    Shiggycon.leftovers = true;
                    characterBody.AddBuff(Modules.Buffs.assaultvestBuff);
                    AkSoundEngine.PostEvent(1531773223, this.gameObject);
                }
                if (Shiggycon.leftovers2 = false && base.skillLocator.utility.skillNameToken == ShiggyPlugin.developerPrefix + "_Shiggy_BODY_LEFTOVERS_NAME")
                {
                    Shiggycon.leftovers2 = true;
                    characterBody.AddBuff(Modules.Buffs.leftoversBuff);
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
