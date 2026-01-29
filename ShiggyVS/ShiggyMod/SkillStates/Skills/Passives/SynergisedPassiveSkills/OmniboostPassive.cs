using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class OmniboostPassive : Skill
    {
        //Beetle + Lesser wisp
        public override void OnEnter()
        {
            base.OnEnter();
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.omniboostBuff.buffIndex, 1);
            }
        }

    }
}