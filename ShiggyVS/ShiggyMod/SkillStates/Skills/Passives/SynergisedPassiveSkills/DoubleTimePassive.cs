using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class DoubleTimePassive : Skill
    {
        //Solus probe + commando
        public override void OnEnter()
        {
            base.OnEnter();
            exitOnDuration = true;
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.doubleTimeBuff.buffIndex, 1);
            }
        }

    }
}