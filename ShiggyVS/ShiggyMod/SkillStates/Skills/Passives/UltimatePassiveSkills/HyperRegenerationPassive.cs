using R2API.Networking;
using ShiggyMod.Modules;

namespace ShiggyMod.SkillStates
{
    public class HyperRegenerationPassive : Skill
    {
        //Mini Mushrum + Jellyfish + Seeker + Meditate
        private string muzzleString = "RHand";

        public override void OnEnter()
        {
            base.OnEnter();

            exitOnDuration = true;
            if (!characterBody.HasBuff(Buffs.hyperRegenerationBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.hyperRegenerationBuff.buffIndex, 1);
            }


        }
        public override void OnExit()
        {
            base.OnExit();
        }

    }
}