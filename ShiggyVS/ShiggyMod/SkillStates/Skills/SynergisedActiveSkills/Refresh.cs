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
    public class Refresh : Skill
    {
        private ExtraSkillLocator extraskillLocator;
        private EnergySystem energysys;
        //Lunar golem + Lunar exploder
        public override void OnEnter()
        {
            base.OnEnter();

            //play animation?

            AkSoundEngine.PostEvent("ShiggyGacha", base.gameObject);
            EffectManager.SpawnEffect(Assets.lunarGolemSmokeEffect, new EffectData
            {
                origin = base.transform.position,
                scale = 1f,
                rotation = Quaternion.LookRotation(Vector3.up),

            }, false);
            //add one stock to all
            if (base.skillLocator.primary.skillDef != Shiggy.refreshDef)
            {
                skillLocator.primary.AddOneStock();
            }
            if (base.skillLocator.secondary.skillDef != Shiggy.refreshDef)
            {
                skillLocator.secondary.AddOneStock();
            }
            if (base.skillLocator.utility.skillDef != Shiggy.refreshDef)
            {
                skillLocator.utility.AddOneStock();
            }
            if (base.skillLocator.special.skillDef != Shiggy.refreshDef)
            {
                skillLocator.special.AddOneStock();
            }

            extraskillLocator = gameObject.GetComponent<ExtraSkillLocator>();

            if (extraskillLocator.extraFirst.skillDef != Shiggy.refreshDef)
            {
                extraskillLocator.extraFirst.AddOneStock();
            }
            if (extraskillLocator.extraSecond.skillDef != Shiggy.refreshDef)
            {
                extraskillLocator.extraSecond.AddOneStock();
            }
            if (extraskillLocator.extraThird.skillDef != Shiggy.refreshDef)
            {
                extraskillLocator.extraThird.AddOneStock();
            }
            if (extraskillLocator.extraFourth.skillDef != Shiggy.refreshDef)
            {
                extraskillLocator.extraFourth.AddOneStock();
            }

            //energy refill
            energysys = gameObject.GetComponent<EnergySystem>();
            energysys.GainplusChaos(energysys.currentplusChaos * StaticValues.refreshEnergyCoefficient);


        }

    }
}