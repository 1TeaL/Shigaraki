using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class AuraOfBlightPassive : Skill
    {
        //Acrid + acid larva
        public override void OnEnter()
        {
            base.OnEnter();
            exitOnDuration = true;
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.auraOfBlightBuff.buffIndex, 1);
            }
        }

    }
}