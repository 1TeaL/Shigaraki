using R2API.Networking;
using ShiggyMod.Modules;

namespace ShiggyMod.SkillStates
{
    public class HyperRegenerationPassive : Skill
    {
        //Decay plus ultra + aura of poison
        private string muzzleString = "RHand";

        public override void OnEnter()
        {
            base.OnEnter();

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