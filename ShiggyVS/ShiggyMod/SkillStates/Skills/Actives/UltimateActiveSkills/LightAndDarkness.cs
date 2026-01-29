using R2API.Networking;
using RoR2;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class LightAndDarkness : Skill
    {
        //double time + omniboost
        private string muzzleString = "RHand";
        private GameObject blastEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/SonicBoomEffect");
        private EnergySystem energySystem;

        public override void OnEnter()
        {
            base.OnEnter();
            duration = baseDuration;
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
                    //if player in light form can also give them light and darkness 
                    if (characterBody.HasBuff(Buffs.lightFormBuff))
                    {
                        characterBody.ApplyBuff(Buffs.lightFormBuff.buffIndex, 0);
                        characterBody.ApplyBuff(Buffs.lightAndDarknessFormBuff.buffIndex, 1);
                    }
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
                    //if player in darkness form can also give them light and darkness 
                    if (characterBody.HasBuff(Buffs.darknessFormBuff))
                    {
                        characterBody.ApplyBuff(Buffs.darknessFormBuff.buffIndex, 0);
                        characterBody.ApplyBuff(Buffs.lightAndDarknessFormBuff.buffIndex, 1);
                    }
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
            EffectManager.SpawnEffect(Modules.ShiggyAsset.engiShieldEffect, new EffectData
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