using R2API.Networking;
using ShiggyMod.Modules;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class HalcyoniteGreed : Skill
    {
        public override void OnEnter()
        {
            base.OnEnter();
            baseDuration = 0.1f;
            this.duration = this.baseDuration / this.attackSpeedStat;
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            base.PlayCrossfade("LeftArm, Override", "LHandFist", "Attack.playbackRate", duration, 0.05f);
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