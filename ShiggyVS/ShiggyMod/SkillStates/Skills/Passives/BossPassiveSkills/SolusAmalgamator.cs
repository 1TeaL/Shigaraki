using R2API.Networking;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class SolusAmalgamator : Skill
    {
        public override void OnEnter()
        {
            base.OnEnter();
            exitOnDuration = true;
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.solusamalgamatorEquipmentBoostBuff.buffIndex, 1);
            }
        }

    }
}