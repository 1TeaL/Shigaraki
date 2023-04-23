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
    public class VoidForm : Skill
    {
        //void barnacle + void fiend
        public override void OnEnter()
        {
            base.OnEnter();

            Ray aimRay = base.GetAimRay();

            //play animation, also need to play particles in controller
            EffectManager.SpawnEffect(Assets.voidFiendBeamMuzzleEffect, new EffectData
            {
                origin = base.transform.position,
                scale = 1f,
                rotation = Quaternion.LookRotation(aimRay.direction),

            }, true);

            if (!characterBody.HasBuff(Buffs.voidFormBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.voidFormBuff.buffIndex, 1);
            }
            else
            if (characterBody.HasBuff(Buffs.voidFormBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.voidFormBuff.buffIndex, 0);
            }
        }

    }
}