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
using System;
using static UnityEngine.UI.Image;

namespace ShiggyMod.SkillStates
{
    public class HyperRegenerationPassive : Skill
    {
        //Decay plus ultra + aura of poison
        private string muzzleString = "RHand";

        public override void OnEnter()
        {
            base.OnEnter();

            if (!characterBody.HasBuff(Buffs.hyperRegenerationBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.hyperRegenerationBuff.buffIndex, 1);
            }


        }
        public override void OnExit()
        {
            base.OnExit();
        }

    }
}