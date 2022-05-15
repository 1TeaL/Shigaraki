using ShiggyMod.Modules.Survivors;
using EntityStates;
using RoR2;
using UnityEngine;
using System.Collections.Generic;
using ShiggyMod.Modules;
using UnityEngine.Networking;
using RoR2.ExpansionManagement;
using ExtraSkillSlots;

namespace ShiggyMod.SkillStates
{
    public class Commando : AFO
    {
        public override void OnEnter()
        {
            base.OnEnter();
            if (NetworkServer.active)
            {
                characterBody.AddBuff(Modules.Buffs.commandoBuff);
            }
        }

    }
}