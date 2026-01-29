using R2API.Networking;
using RoR2;
using ShiggyMod.Modules;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class MechStance : Skill
    {
        //Beetle guard + Mul-t
        public override void OnEnter()
        {
            base.OnEnter();

            Ray aimRay = base.GetAimRay();
            EffectManager.SpawnEffect(ShiggyAsset.multCryoExplosionEffect, new EffectData
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