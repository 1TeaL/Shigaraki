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
using ShiggyMod.Modules.Networking;
using R2API.Networking.Interfaces;

namespace ShiggyMod.SkillStates
{
    public class FinalRelease : Skill
    {
        //wind shield + wind slash
        public float baseDuration = 0.5f;
        public float duration;
        private string muzzleString = "RHand";
        private GameObject blastEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/SonicBoomEffect");

        public override void OnEnter()
        {
            base.OnEnter();
            //play bankai animation- maybe the number one instrumentals? particles too like as if you're holding zangetsu

            Ray aimRay = base.GetAimRay();
            EffectManager.SpawnEffect(EntityStates.Merc.Uppercut.swingEffectPrefab, new EffectData
            {
                origin = FindModelChild(muzzleString).position,
                scale = 1f,
                rotation = Quaternion.LookRotation(aimRay.direction),

            }, true);
            EffectManager.SpawnEffect(EntityStates.Merc.WhirlwindAir.swingEffectPrefab, new EffectData
            {
                origin = FindModelChild(muzzleString).position,
                scale = 1f,
                rotation = Quaternion.LookRotation(aimRay.direction),

            }, true);
            //EffectManager.SpawnEffect(Assets.commandoOmniExplosionVFXEffect, new EffectData
            //{
            //    origin = muzz,
            //    scale = 1f,
            //    rotation = Quaternion.identity,

            //}, true);
            if (!characterBody.HasBuff(Buffs.finalReleaseBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.finalReleaseBuff.buffIndex, 1);
            }
            else
            if (characterBody.HasBuff(Buffs.finalReleaseBuff.buffIndex))
            {
                new SetMugetsuStateMachine(characterBody.masterObjectId).Send(NetworkDestination.Clients);
            }


        }
        public override void OnExit()
        {
            base.OnExit();
        }

    }
}