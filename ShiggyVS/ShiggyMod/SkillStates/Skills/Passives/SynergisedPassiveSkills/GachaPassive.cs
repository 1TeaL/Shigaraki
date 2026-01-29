using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class GachaPassive : Skill
    {
        //Beetle queen + Scavenger
        public override void OnEnter()
        {
            base.OnEnter();
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.gachaBuff.buffIndex, 1);
            }
        }

    }
}