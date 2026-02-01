using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class BigBangPassive : Skill
    {
        //Clay templar + wandering vagrant
        public override void OnEnter()
        {
            base.OnEnter();
            exitOnDuration = true;
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.bigbangBuff.buffIndex, 1);
            }
        }

    }
}