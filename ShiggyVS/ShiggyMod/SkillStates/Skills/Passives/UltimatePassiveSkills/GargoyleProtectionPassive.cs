using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class GargoyleProtectionPassive : Skill
    {
        //Ingrain + stone form
        public override void OnEnter()
        {
            base.OnEnter();
            exitOnDuration = true;
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.gargoyleProtectionBuff.buffIndex, 1);
            }
        }

    }
}