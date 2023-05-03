using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Networking;
using R2API.Networking;
using R2API.Networking.Interfaces;
using ShiggyMod.Modules;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.Linq;
using System;
using static UnityEngine.ParticleSystem.PlaybackState;

namespace ShiggyMod.SkillStates
{
    public class BlastingZoneBurnComponent : MonoBehaviour

    {
        public CharacterBody charBody;
        public CharacterBody attackerBody;
        public float timer;

        public void Start()
        {
            charBody = gameObject.GetComponent<CharacterBody>();

        }

        public void ApplyBurn()
        {
            //halve stacks each pulse
            int debuffCount = charBody.GetBuffCount(Buffs.blastingZoneBurnDebuff);
            int debuffDecreaseAmount = Mathf.RoundToInt(debuffCount / 2f);

            if(debuffCount - debuffDecreaseAmount < 0)
            {
                debuffDecreaseAmount = debuffCount;
            }

            new BlastingZoneDebuffDamageRequest(charBody.masterObjectId, charBody.healthComponent.fullCombinedHealth * Modules.StaticValues.blastingZoneDebuffDamagePerStack * debuffCount);

            charBody.ApplyBuff(Buffs.blastingZoneBurnDebuff.buffIndex, debuffCount - debuffDecreaseAmount);
            EffectManager.SpawnEffect(Modules.Assets.strongerBurnEffectPrefab, new EffectData
            {
                origin = charBody.corePosition,
                scale = 1f,
                rotation = Quaternion.identity

            }, true);
        }

        public void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;
            if (timer > Modules.StaticValues.blastingZoneDebuffInterval)
            {
                timer = 0;
                //burn self, minus one stack
                ApplyBurn();
            }
            if (charBody)
            {
                //If buff isn't present, destroy the effect and self.
                if (!charBody.HasBuff(Modules.Buffs.blastingZoneBurnDebuff))
                {
                    Destroy(this);
                }
            }
            else if (!charBody)
            {
                Destroy(this);
            }
        }


    }
}
