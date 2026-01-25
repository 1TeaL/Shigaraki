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
            //decrease stacks each pulse
            int debuffCount = charBody.GetBuffCount(Buffs.blastingZoneBurnDebuff);
            int debuffDecreaseAmount = debuffCount - StaticValues.blastingZoneDebuffStackRemoval;

            if(debuffDecreaseAmount < 0)
            {
                debuffDecreaseAmount = 0;
            }

            new BlastingZoneDebuffDamageRequest(attackerBody.masterObjectId, charBody.masterObjectId, charBody.healthComponent.fullCombinedHealth * Modules.StaticValues.blastingZoneDebuffDamagePerStack * debuffCount).Send (NetworkDestination.Clients);

            EffectManager.SpawnEffect(EntityStates.LemurianMonster.FireFireball.effectPrefab, new EffectData
            {
                origin = charBody.corePosition + charBody.characterDirection.forward,
                scale = 1f,
                rotation = Quaternion.identity

            }, true);
            charBody.ApplyBuff(Buffs.blastingZoneBurnDebuff.buffIndex, debuffDecreaseAmount);
            Destroy(this);
        }

        public void FixedUpdate()
        {
            if (charBody.HasBuff(Modules.Buffs.blastingZoneBurnDebuff))
            {
                timer += Time.fixedDeltaTime;
                if (timer > Modules.StaticValues.blastingZoneDebuffInterval)
                {
                    timer = 0;
                    //burn self, minus one stack)

                    ApplyBurn();

                }

            }
            else
            //If buff isn't present, destroy the effect and self.
            if (!charBody.HasBuff(Modules.Buffs.blastingZoneBurnDebuff))
            {
                Destroy(this);
            }
        }


    }
}
