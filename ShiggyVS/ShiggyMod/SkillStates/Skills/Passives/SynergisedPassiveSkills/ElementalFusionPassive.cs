using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class ElementalFusionPassive : Skill
    {
        //Grandparent + artificer
        public override void OnEnter()
        {
            base.OnEnter();
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.elementalFusionFireBuff.buffIndex, 1);
            }
        }

    }
}