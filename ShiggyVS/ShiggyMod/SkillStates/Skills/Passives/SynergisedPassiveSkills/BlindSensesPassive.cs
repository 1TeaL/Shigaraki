using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class BlindSensesPassive : Skill
    {
        //blind pest + blind vermin
        public override void OnEnter()
        {
            base.OnEnter();
            exitOnDuration = true;
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.blindSensesBuff.buffIndex, 1);
            }
        }

    }
}