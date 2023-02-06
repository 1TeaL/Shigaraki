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

namespace ShiggyMod.SkillStates.Skills.SynergisedPassiveSkills
{
    public class TrueFlight : Skill
    {
        //(vulture + solus probe) + solus control unit
        public override void OnEnter()
        {
            base.OnEnter();
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Buffs.acridBuff.buffIndex, 1);
            }
        }

    }
}