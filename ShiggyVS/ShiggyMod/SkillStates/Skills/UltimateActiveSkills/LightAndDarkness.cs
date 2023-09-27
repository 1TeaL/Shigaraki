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
    public class LightAndDarkness : Skill
    {
        //double time + omniboost
        public float baseDuration = 1f;
        public float duration;
        private string muzzleString = "RHand";
        private GameObject blastEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/SonicBoomEffect");
        private EnergySystem energySystem;

        public override void OnEnter()
        {
            base.OnEnter();
            duration= baseDuration;
            energySystem = GetComponent<EnergySystem>();


            if (characterBody.HasBuff(Buffs.lightAndDarknessFormBuff.buffIndex))
            {
                characterBody.ApplyBuff(Buffs.lightAndDarknessFormBuff.buffIndex, 0);
            }
            else
            {
                if (energySystem.currentplusChaos == energySystem.maxPlusChaos / 2f)
                {
                    characterBody.ApplyBuff(Buffs.lightFormBuff.buffIndex, 0);
                    characterBody.ApplyBuff(Buffs.darknessFormBuff.buffIndex, 0);
                    if (!characterBody.HasBuff(Buffs.lightAndDarknessFormBuff.buffIndex))
                    {
                        characterBody.ApplyBuff(Buffs.lightAndDarknessFormBuff.buffIndex, 1);
                    }
                }
                else if (energySystem.currentplusChaos > energySystem.maxPlusChaos / 2f)
                {
                    if (characterBody.HasBuff(Buffs.darknessFormBuff.buffIndex))
                    {
                        characterBody.ApplyBuff(Buffs.darknessFormBuff.buffIndex, 0);
                        characterBody.ApplyBuff(Buffs.lightAndDarknessFormBuff.buffIndex, 1);
                    }
                    else if (!characterBody.HasBuff(Buffs.darknessFormBuff.buffIndex))
                    {
                        characterBody.ApplyBuff(Buffs.lightFormBuff.buffIndex, 1);
                    }

                }
                else
                if (energySystem.currentplusChaos < energySystem.maxPlusChaos / 2f)
                {
                    if (characterBody.HasBuff(Buffs.lightFormBuff.buffIndex))
                    {
                        characterBody.ApplyBuff(Buffs.lightFormBuff.buffIndex, 0);
                        characterBody.ApplyBuff(Buffs.lightAndDarknessFormBuff.buffIndex, 1);
                    }
                    else if (!characterBody.HasBuff(Buffs.lightFormBuff.buffIndex))
                    {
                        characterBody.ApplyBuff(Buffs.darknessFormBuff.buffIndex, 1);
                    }
                }

            }


            //play animation and maybe particles? drive form sounds?
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            base.PlayAnimation("FullBody, Override", "FullBodyUnleash", "Attack.playbackRate", duration);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            AkSoundEngine.PostEvent("ShiggyDriveForm", base.gameObject);

            Ray aimRay = base.GetAimRay();
            EffectManager.SpawnEffect(Assets.engiShieldEffect, new EffectData
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
        }

    }
}