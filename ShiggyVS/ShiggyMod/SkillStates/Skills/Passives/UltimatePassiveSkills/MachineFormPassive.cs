using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class MachineFormPassive : Skill
    {
        //Missile flurry + mech stance
        public override void OnEnter()
        {
            base.OnEnter();
            exitOnDuration = true;
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.machineFormBuff.buffIndex, 1);
            }
        }

    }
}