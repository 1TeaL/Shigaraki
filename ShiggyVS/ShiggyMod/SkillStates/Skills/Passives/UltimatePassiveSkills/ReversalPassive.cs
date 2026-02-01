using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class ReversalPassive : Skill
    {
        //barrier jelly + blind senses
        public override void OnEnter()
        {
            base.OnEnter();
            exitOnDuration = true;
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.reversalBuff.buffIndex, 1);
            }
        }

    }
}