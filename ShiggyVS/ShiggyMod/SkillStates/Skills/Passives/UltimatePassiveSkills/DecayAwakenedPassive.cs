using R2API.Networking;
using ShiggyMod.Modules;

namespace ShiggyMod.SkillStates
{
    public class DecayAwakenedPassive : Skill
    {
        //Decay plus ultra + aura of poison
        public float baseDuration = 1f;
        public float duration;
        private string muzzleString = "RHand";

        public override void OnEnter()
        {
            base.OnEnter();
            exitOnDuration = true;

            if (!characterBody.HasBuff(Buffs.decayAwakenedBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.decayAwakenedBuff.buffIndex, 1);
            }


        }
        public override void OnExit()
        {
            base.OnExit();
        }

    }
}