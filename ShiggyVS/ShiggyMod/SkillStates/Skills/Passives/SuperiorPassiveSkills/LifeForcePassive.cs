using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class LifeForcePassive : Skill
    {
        //False Son + Bison + Stone Titan
        public override void OnEnter()
        {
            base.OnEnter();
            exitOnDuration = true;
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.lifeForceBuff.buffIndex, 1);
            }
        }

    }
}