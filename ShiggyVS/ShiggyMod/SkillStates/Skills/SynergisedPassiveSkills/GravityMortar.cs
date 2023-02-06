using ShiggyMod.Modules.Survivors;
using EntityStates;
using RoR2;
using UnityEngine;
using System.Collections.Generic;
using ShiggyMod.Modules;
using UnityEngine.Networking;
using RoR2.ExpansionManagement;
using ExtraSkillSlots;
using R2API.Networking;

namespace ShiggyMod.SkillStates.SynergisedPassiveSkills
{
    public class GravityMortar : Skill
    {
        //void barnacle- main + void jailer
        public override void OnEnter()
        {
            base.OnEnter();
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.acridBuff.buffIndex, 1);
            }
        }

    }
}