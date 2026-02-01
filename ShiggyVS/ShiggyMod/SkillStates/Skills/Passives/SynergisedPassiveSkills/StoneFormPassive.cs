using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class StoneFormPassive : Skill
    {
        //Hermit crab + Stone titan
        public override void OnEnter()
        {
            base.OnEnter();
            exitOnDuration = true;
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.stoneFormBuff.buffIndex, 1);
            }
        }

    }
}