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
    public class MechStance : Skill
    {
        //Beetle guard + Mul-t
        public override void OnEnter()
        {
            base.OnEnter();

            Ray aimRay = base.GetAimRay();
            EffectManager.SpawnEffect(Assets.multCryoExplosionEffect, new EffectData
            {
                origin = base.transform.position,
                scale = 1f,
                rotation = Quaternion.LookRotation(aimRay.direction),

            }, true);

            if (!characterBody.HasBuff(Buffs.mechStanceBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.mechStanceBuff.buffIndex, 1);
                base.characterBody.bodyFlags |= CharacterBody.BodyFlags.IgnoreFallDamage;
            }
            else
            if (characterBody.HasBuff(Buffs.mechStanceBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.mechStanceBuff.buffIndex, 0);
                base.characterBody.bodyFlags &= ~CharacterBody.BodyFlags.IgnoreFallDamage;
            }
        }

    }
}