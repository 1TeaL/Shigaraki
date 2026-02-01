using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class Loader : Skill
    {
        public override void OnEnter()
        {
            base.OnEnter();
            exitOnDuration = true;
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.loaderBuff.buffIndex, 1);
            }
        }

    }
}