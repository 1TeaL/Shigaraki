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
        public float baseDuration = 1f;
        public float duration;
        private string muzzleString = "RHand";
        private GameObject blastEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/SonicBoomEffect");
        private EnergySystem energySystem;
        private ShiggyController shiggyCon;

        public override void OnEnter()
        {
            base.OnEnter();
            duration = baseDuration;
            shiggyCon = base.gameObject.GetComponent<ShiggyController>();
            Ray aimRay = base.GetAimRay();

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            energySystem = base.gameObject.GetComponent<EnergySystem>(); 
            //check minimum energy requirement so we don't get back to back mugetsu. 
            if(energySystem.currentplusChaos < StaticValues.finalReleaseInitialEnergyRequirement)
            {
                if (characterBody.HasBuff(Buffs.finalReleaseBuff.buffIndex))
                {
                    new SetMugetsuStateMachine(characterBody.masterObjectId).Send(NetworkDestination.Clients);
                    if (base.isAuthority)
                    {
                        Shiggycon.StopFinalReleaseLoop();
                    }
                }
                else
                {
                    energySystem.TriggerGlow(0.3f, 0.3f, Color.black);
                    energySystem.quirkGetInformation($"<style=cIsUtility>Need minimum of {StaticValues.finalReleaseInitialEnergyRequirement} Plus Chaos to enter Final Release</style>", 1f);
                }
                this.outer.SetNextStateToMain();
                return;
            }
            else if (energySystem.currentplusChaos >= StaticValues.finalReleaseInitialEnergyRequirement)
            {

                characterBody.ApplyBuff(RoR2Content.Buffs.HiddenInvincibility.buffIndex, 1, 1);
                if (!characterBody.HasBuff(Buffs.finalReleaseBuff.buffIndex))
                {
                    base.PlayAnimation("FullBody, Override", "FullBodyBankai", "Attack.playbackRate", duration);
                    characterBody.ApplyBuff(Buffs.finalReleaseBuff.buffIndex, 1);
                    if(base.isAuthority)
                    {
                        shiggyCon.PlayFinalReleaseLoop();
                    }
                }
                else if (characterBody.HasBuff(Buffs.finalReleaseBuff.buffIndex))
                {
                    new SetMugetsuStateMachine(characterBody.masterObjectId).Send(NetworkDestination.Clients);
                    if (base.isAuthority)
                    {
                        Shiggycon.StopFinalReleaseLoop();
                    }
                }

                EffectManager.SpawnEffect(Modules.Assets.finalReleasePulseEffect, new EffectData
                {
                    origin = characterBody.footPosition,
                    scale = 1f,
                    rotation = Quaternion.identity,

                }, true);

            }



        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(base.fixedAge> duration)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }

    }
}