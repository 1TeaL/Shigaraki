using RoR2;
using System;
using ShiggyMod.Modules.Survivors;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    internal class DecayEffectController : MonoBehaviour
    {
        public CharacterBody charbody;
        private GameObject effectObj;

        public void Start()
        {
            charbody = this.gameObject.GetComponent<CharacterBody>();
            effectObj = Instantiate(Modules.Assets.decaybuffEffect, this.transform.position, Quaternion.identity);
            //effectObj = EffectManager.SimpleEffect(Modules.Assets.decaybuffEffect, this.transform.position, Quaternion.identity, true);
        }

        public void Update()
        {
            //Handle transform of effectObj
            if (effectObj)
            {
                effectObj.transform.position = this.transform.position;
            }
        }

        public void FixedUpdate()
        {
            if (charbody)
            {
                //If buff isn't present, destroy the effect and self.
                if (!charbody.HasBuff(Modules.Buffs.decayDebuff))
                {
                    Destroy(effectObj);
                    Destroy(this);
                }
            }

            if (!charbody)
            {
                Destroy(effectObj);
            }
        }

        public void OnDestroy()
        {
            Destroy(effectObj);
        }
    }
}
