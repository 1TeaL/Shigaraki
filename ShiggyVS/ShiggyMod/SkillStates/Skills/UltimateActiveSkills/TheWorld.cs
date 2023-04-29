﻿using ShiggyMod.Modules.Survivors;
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
    public class TheWorld : Skill
    {
        //double time + omniboost
        public float baseDuration = 1f;
        public float duration;
        private string muzzleString = "RHand";
        private GameObject blastEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/SonicBoomEffect");

        public override void OnEnter()
        {
            base.OnEnter();
            //play animation and maybe particles?

            Ray aimRay = base.GetAimRay();
            EffectManager.SpawnEffect(blastEffectPrefab, new EffectData
            {
                origin = base.characterBody.corePosition,
                scale = 1f,
                rotation = Quaternion.LookRotation(aimRay.direction),

            }, true);


        }
        public override void OnExit()
        {
            base.OnExit();
            Ray aimRay = base.GetAimRay();
            EffectManager.SpawnEffect(blastEffectPrefab, new EffectData
            {
                origin = base.characterBody.corePosition,
                scale = 1f,
                rotation = Quaternion.LookRotation(aimRay.direction),

            }, true);
            EffectManager.SimpleMuzzleFlash(Assets.commandoOmniExplosionVFXEffect, base.gameObject, this.muzzleString, false);
            //EffectManager.SpawnEffect(Assets.commandoOmniExplosionVFXEffect, new EffectData
            //{
            //    origin = muzz,
            //    scale = 1f,
            //    rotation = Quaternion.identity,

            //}, true);
            if (!characterBody.HasBuff(Buffs.theWorldBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.theWorldBuff.buffIndex, 1);
            }
            else
            if (characterBody.HasBuff(Buffs.theWorldBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.theWorldBuff.buffIndex, 0);
            }
        }

    }
}