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
using System.Linq;
using ShiggyMod.Modules.Networking;
using EntityStates.ScavMonster;

namespace ShiggyMod.SkillStates
{
    //gacha + refresh
    public class WildCard : Skill
    {
        //Refresh + gacha
        public float baseDuration = 1f;
        public float duration;
        private string muzzleString = "RHand";
        private int randomInt;
        public BullseyeSearch search;

        public override void OnEnter()
        {
            base.OnEnter();
            duration= baseDuration;
            //play animation and maybe particles? maybe snap fingers idk?

            Ray aimRay = base.GetAimRay();
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            base.PlayCrossfade("LeftArm, Override", "LHandSnap", "Attack.playbackRate", duration, 0.05f);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            AkSoundEngine.PostEvent("ShiggyGacha", base.gameObject);
            EffectManager.SpawnEffect(EntityStates.LunarGolem.ChargeTwinShot.effectPrefab, new EffectData
            {
                origin = FindModelChild(muzzleString).position,
                scale = 1f,
                rotation = Quaternion.LookRotation(aimRay.direction),

            }, true);

            randomInt = UnityEngine.Random.RandomRangeInt(0, 7);

            switch (randomInt)
            {
                case 0:
                    search = new BullseyeSearch
                    {

                        teamMaskFilter = TeamMask.all,
                        filterByLoS = false,
                        searchOrigin = characterBody.corePosition,
                        searchDirection = UnityEngine.Random.onUnitSphere,
                        sortMode = BullseyeSearch.SortMode.Distance,
                        maxDistanceFilter = StaticValues.wildcardRangeGlobal,
                        maxAngleFilter = 360f
                    };

                    search.RefreshCandidates();
                    

                    List<HurtBox> target = search.GetResults().ToList<HurtBox>();
                    foreach (HurtBox singularTarget in target)
                    {
                        if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                        {
                            //make everyone teleport
                            Vector3 randRelPos = new Vector3((float)UnityEngine.Random.Range(-StaticValues.wildcardTeleportRange, StaticValues.wildcardTeleportRange), (float)UnityEngine.Random.Range(0f, StaticValues.wildcardTeleportRange ), (float)UnityEngine.Random.Range(-StaticValues.wildcardTeleportRange, StaticValues.wildcardTeleportRange));

                            if (singularTarget.healthComponent.body.characterMotor)
                            {
                                singularTarget.healthComponent.body.characterMotor.Motor.SetPositionAndRotation(singularTarget.transform.position + randRelPos,
                                                        Quaternion.identity, true);
                            }
                            else if (singularTarget.healthComponent.body.rigidbody)
                            {
                                singularTarget.healthComponent.body.rigidbody.MovePosition(singularTarget.transform.position + randRelPos);
                            }

                        }
                    }
                    break;
                case 1:
                    //fire meteor
                    if (NetworkServer.active)
                    {
                        Func<bool> func = null;
                        func = new Func<bool>(this.FireMeteor);
                    }
                    break;
                case 2:

                    search = new BullseyeSearch
                    {

                        teamMaskFilter = TeamMask.all,
                        filterByLoS = false,
                        searchOrigin = characterBody.corePosition,
                        searchDirection = UnityEngine.Random.onUnitSphere,
                        sortMode = BullseyeSearch.SortMode.Distance,
                        maxDistanceFilter = StaticValues.wildcardRangeGlobal,
                        maxAngleFilter = 360f
                    };

                    search.RefreshCandidates();
                    

                    List<HurtBox> target2 = search.GetResults().ToList<HurtBox>();
                    foreach (HurtBox singularTarget in target2)
                    {
                        if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                        {
                            //make everyone move super fast
                            singularTarget.healthComponent.body.ApplyBuff(Buffs.wildcardSpeedBuff.buffIndex, 1, StaticValues.wildcardDuration);

                        }
                    }
                    break;
                case 3:
                    search = new BullseyeSearch
                    {

                        teamMaskFilter = TeamMask.all,
                        filterByLoS = false,
                        searchOrigin = characterBody.corePosition,
                        searchDirection = UnityEngine.Random.onUnitSphere,
                        sortMode = BullseyeSearch.SortMode.Distance,
                        maxDistanceFilter = StaticValues.wildcardRangeGlobal,
                        maxAngleFilter = 360f
                    };

                    search.RefreshCandidates();
                    

                    List<HurtBox> target3 = search.GetResults().ToList<HurtBox>();
                    foreach (HurtBox singularTarget in target3)
                    {
                        if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                        {
                            //make everyone move super slow
                            singularTarget.healthComponent.body.ApplyBuff(Buffs.wildcardSlowBuff.buffIndex, 1, StaticValues.wildcardDuration);

                        }
                    }
                    break;
                case 4:
                    search = new BullseyeSearch
                    {

                        teamMaskFilter = TeamMask.all,
                        filterByLoS = false,
                        searchOrigin = characterBody.corePosition,
                        searchDirection = UnityEngine.Random.onUnitSphere,
                        sortMode = BullseyeSearch.SortMode.Distance,
                        maxDistanceFilter = StaticValues.wildcardRangeGlobal,
                        maxAngleFilter = 360f
                    };

                    search.RefreshCandidates();
                    

                    List<HurtBox> target4 = search.GetResults().ToList<HurtBox>();
                    foreach (HurtBox singularTarget in target4)
                    {
                        if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                        {
                            //make everyone hit super hard
                            singularTarget.healthComponent.body.ApplyBuff(Buffs.wildcardDamageBuff.buffIndex, 1, StaticValues.wildcardDuration);

                        }
                    }
                    break;
                case 5:
                    search = new BullseyeSearch
                    {

                        teamMaskFilter = TeamMask.all,
                        filterByLoS = false,
                        searchOrigin = characterBody.corePosition,
                        searchDirection = UnityEngine.Random.onUnitSphere,
                        sortMode = BullseyeSearch.SortMode.Distance,
                        maxDistanceFilter = StaticValues.wildcardRangeGlobal,
                        maxAngleFilter = 360f
                    };

                    search.RefreshCandidates();
                    

                    List<HurtBox> target5 = search.GetResults().ToList<HurtBox>();
                    foreach (HurtBox singularTarget in target5)
                    {
                        if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                        {
                            //make everyone tonic buff
                            singularTarget.healthComponent.body.ApplyBuff(RoR2Content.Buffs.TonicBuff.buffIndex, 1, StaticValues.wildcardDuration);
                            if (!Util.CheckRoll(80f, singularTarget.healthComponent.body.master))
                            {
                                singularTarget.healthComponent.body.pendingTonicAfflictionCount++;
                            }

                        }
                    }
                    break;
                case 6:
                    search = new BullseyeSearch
                    {

                        teamMaskFilter = TeamMask.all,
                        filterByLoS = false,
                        searchOrigin = characterBody.corePosition,
                        searchDirection = UnityEngine.Random.onUnitSphere,
                        sortMode = BullseyeSearch.SortMode.Distance,
                        maxDistanceFilter = StaticValues.wildcardRangeGlobal,
                        maxAngleFilter = 360f
                    };

                    search.RefreshCandidates();
                    

                    List<HurtBox> target6 = search.GetResults().ToList<HurtBox>();
                    foreach (HurtBox singularTarget in target6)
                    {
                        if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                        {
                            //make everyone projectile destroy
                            singularTarget.healthComponent.body.ApplyBuff(Buffs.wildcardNoProjectileBuff.buffIndex, 1, StaticValues.wildcardDuration);

                        }
                    }
                    break;
                case 7:
                    search = new BullseyeSearch
                    {

                        teamMaskFilter = TeamMask.all,
                        filterByLoS = false,
                        searchOrigin = characterBody.corePosition,
                        searchDirection = UnityEngine.Random.onUnitSphere,
                        sortMode = BullseyeSearch.SortMode.Distance,
                        maxDistanceFilter = StaticValues.wildcardRangeGlobal,
                        maxAngleFilter = 360f
                    };

                    search.RefreshCandidates();
                    

                    List<HurtBox> target7 = search.GetResults().ToList<HurtBox>();
                    foreach (HurtBox singularTarget in target7)
                    {
                        if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                        {
                            //make everyone half hp
                            new SpendHealthNetworkRequest(singularTarget.healthComponent.body.masterObjectId, singularTarget.healthComponent.fullCombinedHealth / 2f);

                        }
                    }
                    break;
                case 8:
                    search = new BullseyeSearch
                    {

                        teamMaskFilter = TeamMask.all,
                        filterByLoS = false,
                        searchOrigin = characterBody.corePosition,
                        searchDirection = UnityEngine.Random.onUnitSphere,
                        sortMode = BullseyeSearch.SortMode.Distance,
                        maxDistanceFilter = StaticValues.wildcardRangeGlobal,
                        maxAngleFilter = 360f
                    };

                    search.RefreshCandidates();


                    List<HurtBox> target8 = search.GetResults().ToList<HurtBox>();
                    foreach (HurtBox singularTarget in target8)
                    {
                        if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                        {
                            //make everyone get a random item
                            singularTarget.healthComponent.body.inventory.GiveRandomItems(1, true, true);

                            Util.PlaySound(FindItem.sound, singularTarget.gameObject);

                        }
                    }
                    break;
                case 9:
                    search = new BullseyeSearch
                    {

                        teamMaskFilter = TeamMask.all,
                        filterByLoS = false,
                        searchOrigin = characterBody.corePosition,
                        searchDirection = UnityEngine.Random.onUnitSphere,
                        sortMode = BullseyeSearch.SortMode.Distance,
                        maxDistanceFilter = StaticValues.wildcardRangeGlobal,
                        maxAngleFilter = 360f
                    };

                    search.RefreshCandidates();


                    List<HurtBox> target9 = search.GetResults().ToList<HurtBox>();
                    foreach (HurtBox singularTarget in target9)
                    {
                        if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                        {
                            //make everyone get a random item
                            new HealNetworkRequest(singularTarget.healthComponent.body.masterObjectId, singularTarget.healthComponent.fullCombinedHealth / 2f);

                        }
                    }
                    break;
            }

        }

        private bool FireMeteor()
        {
            MeteorStormController component = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/MeteorStorm"), this.characterBody.corePosition, Quaternion.identity).GetComponent<MeteorStormController>();
            if (!component) 
            {
                UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/MeteorStorm"), this.characterBody.corePosition, Quaternion.identity).AddComponent<MeteorStormController>();
            }
            component.owner = base.gameObject;
            component.ownerDamage = this.characterBody.damage;
            component.isCrit = Util.CheckRoll(this.characterBody.crit, this.characterBody.master);
            NetworkServer.Spawn(component.gameObject);
            return true;
        }

        public override void OnExit()
        {
            base.OnExit();
        }

    }
}