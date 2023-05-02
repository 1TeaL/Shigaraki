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
    public class DecayAwakenedPassive : Skill
    {
        //Decay plus ultra + aura of poison
        public float baseDuration = 1f;
        public float duration;
        private string muzzleString = "RHand";

        public override void OnEnter()
        {
            base.OnEnter();

            if (!characterBody.HasBuff(Buffs.decayAwakenedBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.decayAwakenedBuff.buffIndex, 1);
            }


        }
        public override void OnExit()
        {
            base.OnExit();
        }

    }
}