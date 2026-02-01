using RoR2;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    internal class DecayEffectController : MonoBehaviour
    {
        public CharacterBody charBody;
        public CharacterBody attackerBody;
        private GameObject effectObj = Modules.ShiggyAsset.decaybuffEffect;
        public float timer;

        public void Start()
        {
            charBody = gameObject.GetComponent<CharacterBody>();
            //effectObj = UnityEngine.Object.Instantiate(Modules.Asset.decaybuffEffect, charBody.corePosition, Quaternion.identity);
            //effectObj.transform.parent = charBody.gameObject.transform;
            //effectObj = EffectManager.SimpleEffect(Modules.Asset.decaybuffEffect, this.transform.position, Quaternion.identity, true);

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
                maxDistanceFilter = Modules.Config.DecaySpreadRadius.Value,
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
                        info.duration = Modules.Config.DecayDuration.Value;
                        info.dotIndex = Modules.Dots.decayDot;

                        DotController.InflictDot(ref info);
                    }
                    EffectManager.SpawnEffect(Modules.ShiggyAsset.decayspreadEffect, new EffectData
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
                    //EffectManager.SpawnEffect(Modules.Asset.decaybuffEffect, new EffectData
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
        //    EffectManager.SpawnEffect(Modules.Asset.decayspreadEffect, new EffectData
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
                if (timer > Modules.Config.DecayInterval.Value)
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
