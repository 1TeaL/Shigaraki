using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class IngrainPassive : Skill
    {
        //Clay dunestrider + mushrum
        public override void OnEnter()
        {
            base.OnEnter();
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.ingrainBuff.buffIndex, 1);
            }
        }

    }
}