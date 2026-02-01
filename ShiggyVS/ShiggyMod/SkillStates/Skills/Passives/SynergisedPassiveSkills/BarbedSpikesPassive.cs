using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class BarbedSpikesPassive : Skill
    {
        //Bronzong (bell body) + gup
        public override void OnEnter()
        {
            base.OnEnter();
            exitOnDuration = true;
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.barbedSpikesBuff.buffIndex, 1);
            }
        }

    }
}