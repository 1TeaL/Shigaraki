using RoR2;
using System;
using ShiggyMod.Modules.Survivors;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;

namespace ShiggyMod.SkillStates
{
    internal class DecayEffectController : MonoBehaviour
    {
        public CharacterBody charbody;
        private GameObject effectObj;
        public float timer;
        public TeamMask sameTeam;

        public void Start()
        {
            sameTeam.AddTeam(TeamIndex.Monster);
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
        public void ApplyDoT()
        {
            Debug.Log("ApplyingDoTtoothers");
            BullseyeSearch search = new BullseyeSearch
            {
                teamMaskFilter = sameTeam,
                filterByLoS = false,
                searchOrigin = charbody.corePosition,
                searchDirection = UnityEngine.Random.onUnitSphere,
                sortMode = BullseyeSearch.SortMode.Distance,
                maxDistanceFilter = Modules.StaticValues.decayspreadRadius,
                maxAngleFilter = 360f
            };

            search.RefreshCandidates();
            search.FilterOutGameObject(charbody.gameObject);

            HurtBox target = search.GetResults().FirstOrDefault();

            if (target)
            {
                if (target.healthComponent && target.healthComponent.body)
                {
                    InflictDotInfo info = new InflictDotInfo();
                    info.attackerObject = null;
                    info.victimObject = target.healthComponent.body.gameObject;
                    info.duration = Modules.StaticValues.decayDamageTimer;
                    info.dotIndex = Modules.Dots.decayDot;

                    DotController.InflictDot(ref info);
                }
            }
            
        }

        public void ApplyDotToSelf()
        {
            Debug.Log("ApplyingDoTtoself");
            InflictDotInfo info = new InflictDotInfo();
            info.attackerObject = null;
            info.victimObject = charbody.healthComponent.body.gameObject;
            info.duration = Modules.StaticValues.decayDamageTimer;
            info.dotIndex = Modules.Dots.decayDot;

            DotController.InflictDot(ref info);
        }

        public void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;
            if (timer > Modules.StaticValues.decayadditionalTimer)
            {
                Debug.Log("ApplyingDoTfromController");
                timer = 0;
                ApplyDoT();
                ApplyDotToSelf();
            }
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
