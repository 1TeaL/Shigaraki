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
        private EnergySystem energySystem;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            //play bankai animation- maybe the number one instrumentals? particles too like as if you're holding zangetsu
            energySystem = base.gameObject.GetComponent<EnergySystem>(); 
            //check minimum energy requirement so we don't get back to back mugetsu. 
            if(energySystem.currentplusChaos < StaticValues.finalReleaseInitialEnergyRequirement)
            {
                if (characterBody.HasBuff(Buffs.finalReleaseBuff.buffIndex))
                {
                    new SetMugetsuStateMachine(characterBody.masterObjectId).Send(NetworkDestination.Clients);
                }
                this.outer.SetNextStateToMain();
                return;
            }
            else if (energySystem.currentplusChaos >= StaticValues.finalReleaseInitialEnergyRequirement)
            {
                EffectManager.SpawnEffect(EntityStates.Vulture.Weapon.FireWindblade.muzzleEffectPrefab, new EffectData
                {
                    origin = FindModelChild(muzzleString).position,
                    scale = 1f,
                    rotation = Quaternion.identity,

                }, true);
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



        }
        public override void OnExit()
        {
            base.OnExit();
        }

    }
}