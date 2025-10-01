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

namespace ShiggyMod.SkillStates
{
    public class HalcyoniteGreed : Skill
    {
        public override void OnEnter()
        {
            base.OnEnter();
            if (NetworkServer.active)
            {
                if (characterBody.HasBuff(Buffs.halcyoniteGreedBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.halcyoniteGreedBuff.buffIndex, 0);
                }
                else if (!characterBody.HasBuff(Buffs.halcyoniteGreedBuff))
                {
                    characterBody.ApplyBuff(Modules.Buffs.halcyoniteGreedBuff.buffIndex, 1);

                    //find half of the total money player owns
                    uint halfMoney = (uint)Mathf.RoundToInt(characterBody.master.money / 2f);
                    int greedBuffStacksToGive = Mathf.RoundToInt(halfMoney / StaticValues.halcyoniteGreedBuffGoldPerStack);

                    characterBody.master.money -= halfMoney;
                    characterBody.ApplyBuff(Buffs.halcyoniteGreedStacksBuff.buffIndex, greedBuffStacksToGive, StaticValues.halcyoniteGreedInterval);
                }
            }
        }

    }
}