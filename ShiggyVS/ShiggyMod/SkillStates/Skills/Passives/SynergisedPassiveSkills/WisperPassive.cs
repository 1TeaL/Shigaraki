using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class WisperPassive : Skill
    {
        //Greater wisp + grovetender
        public override void OnEnter()
        {
            base.OnEnter();
            exitOnDuration = true;
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.wisperBuff.buffIndex, 1);
            }
        }

    }
}