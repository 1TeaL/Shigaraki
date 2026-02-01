using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class VoidJailer : Skill
    {
        public override void OnEnter()
        {
            base.OnEnter();
            exitOnDuration = true;
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.voidjailerBuff.buffIndex, 1);
            }
        }

    }
}