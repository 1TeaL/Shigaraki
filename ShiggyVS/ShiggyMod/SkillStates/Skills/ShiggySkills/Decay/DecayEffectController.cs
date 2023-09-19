using RoR2;
using System;
using ShiggyMod.Modules.Survivors;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;
using HG;

namespace ShiggyMod.SkillStates
{
    internal class DecayEffectController : MonoBehaviour
    {
        public CharacterBody charBody;
        public CharacterBody attackerBody;
        private GameObject effectObj = Modules.Assets.decaybuffEffect;
        public float timer;

        public void Start()
        {
            charBody = gameObject.GetComponent<CharacterBody>();
            //effectObj = UnityEngine.Object.Instantiate(Modules.Assets.decaybuffEffect, charBody.corePosition, Quaternion.identity);
            //effectObj.transform.parent = charBody.gameObject.transform;
            //effectObj = EffectManager.SimpleEffect(Modules.Assets.decaybuffEffect, this.transform.position, Quaternion.identity, true);

            //EffectManager.SpawnEffect(effectObj, new EffectData
            //{
            //    origin = charBody.corePosition,
            //    scale = 1f,
            //    rotation = Quaternion.identity,

            //}, true);

        }

       
        public void ApplyDoT()
        {
            BullseyeSearch search = new BullseyeSearch
            {

                teamMaskFilter = TeamMask.GetEnemyTeams(TeamIndex.Player),
                filterByLoS = false,
                searchOrigin = charBody.corePosition,
                searchDirection = UnityEngine.Random.onUnitSphere,
                sortMode = BullseyeSearch.SortMode.Distance,
                maxDistanceFilter = Modules.StaticValues.decayspreadRadius,
                maxAngleFilter = 360f
            };

            search.RefreshCandidates();
            search.FilterOutGameObject(charBody.gameObject);



            List<HurtBox> target = search.GetResults().ToList<HurtBox>();
            foreach (HurtBox singularTarget in target)
            {
                if (singularTarget)
                {
                    if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                    {
                        InflictDotInfo info = new InflictDotInfo();
                        info.attackerObject = attackerBody.gameObject;
                        info.victimObject = singularTarget.healthComponent.body.gameObject;
                        info.duration = Modules.StaticValues.decayDamageTimer;
                        info.dotIndex = Modules.Dots.decayDot;

                        DotController.InflictDot(ref info);
                    }
                    EffectManager.SpawnEffect(Modules.Assets.decayspreadEffect, new EffectData
                    {
                        origin = singularTarget.healthComponent.body.corePosition,
                        scale = 1f,

                    }, true);

                    DecayEffectController controller = singularTarget.gameObject.GetComponent<DecayEffectController>();
                    if (!controller)
                    {
                        controller = singularTarget.gameObject.AddComponent<DecayEffectController>();
                        controller.attackerBody = attackerBody;
                    }
                    //EffectManager.SpawnEffect(Modules.Assets.decaybuffEffect, new EffectData
                    //{
                    //    origin = singularTarget.healthComponent.body.corePosition,
                    //    scale = 1f,
                    //    rotation = Quaternion.identity,

                    //}, true);
                }
            }
        }
        //public void ApplyDotToSelf()
        //{
        //    //Debug.Log("ApplyingDoTtoself");
        //    InflictDotInfo info = new InflictDotInfo();
        //    info.attackerObject = attackerBody.gameObject;
        //    info.victimObject = charBody.gameObject;
        //    info.duration = Modules.StaticValues.decayDamageTimer;
        //    info.dotIndex = Modules.Dots.decayDot;

        //    DotController.InflictDot(ref info);
        //    EffectManager.SpawnEffect(Modules.Assets.decayspreadEffect, new EffectData
        //    {
        //        origin = charBody.corePosition,
        //        scale = 1f,

        //    }, true);
        //}

        public void FixedUpdate()
        {
            if (charBody)
            {
                //If buff isn't present, destroy the effect and self.
                if (!charBody.HasBuff(Modules.Buffs.decayDebuff))
                {
                    Destroy(effectObj);
                    Destroy(this);
                }

                timer += Time.fixedDeltaTime;
                if (timer > Modules.StaticValues.decayadditionalTimer)
                {
                    //Debug.Log("ApplyingDoTfromController");
                    timer = 0;
                    ApplyDoT();
                    //ApplyDotToSelf();
                    //EffectManager.SpawnEffect(effectObj, new EffectData
                    //{
                    //    origin = charBody.corePosition,
                    //    scale = 1f,
                    //    rotation = Quaternion.identity,

                    //}, true);
                }
            }
            else if (!charBody)
            {
                Destroy(effectObj);
                Destroy(this);
            }
        }

        public void OnDestroy()
        {
            Destroy(effectObj);
        }
    }
}
