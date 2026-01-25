using EntityStates;
using EntityStates.JellyfishMonster;
using HG;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Networking;
using ShiggyMod.Modules.Survivors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.ParticleSystem.PlaybackState;

namespace ShiggyMod.SkillStates
{
    public class SolusInvalidatorInvalidate : Skill
    {
        private BullseyeSearch search;
        private List<HurtBox> trackingTargets;

        private float angle;
        private float range;
        private uint soundID;
        private GameObject chargeEffect;
        private Vector3 theSpot;

        public override void OnEnter()
        {
            base.OnEnter();
            search = new BullseyeSearch();
            range = StaticValues.solusInvalidatorInvalidateRange * (attackSpeedStat/3);
            damageType = new DamageTypeCombo(DamageType.Stun1s, DamageTypeExtended.Generic, DamageSource.Secondary);
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);

            
            PlayCrossfade("LeftArm, Override", "LHandDetonate", "Attack.playbackRate", duration, 0.05f);
            Transform modelTransform = base.GetModelTransform();
            //AkSoundEngine.PostEvent("ShiggyExplosion", base.gameObject);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }


        }

        public override void OnExit()
        {
            base.OnExit();
            PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            AkSoundEngine.StopPlayingID(this.soundID);
            
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if(base.fixedAge >= fireTime && !hasFired)
            {
                hasFired = true;
                //search for target
                SearchForTarget(characterBody);
                //Apply primed + shock or freeze
                InvalidateTargets(characterBody);

            }

            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }


        private void InvalidateTargets(CharacterBody charBody)
        {

            if (trackingTargets.Count > 0)
            {
                foreach (HurtBox singularTarget in trackingTargets)
                {

                    if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                    {

                        int stacks = singularTarget.healthComponent.body.GetBuffCount(Buffs.solusPrimedDebuff);
                        SetStateOnHurt component = singularTarget.healthComponent.GetComponent<SetStateOnHurt>();
                        bool flag = component == null;

                        if (!singularTarget.healthComponent.body.HasBuff(Buffs.solusPrimedDebuff) && !flag)
                        {
                            bool canBeHitStunned = component.canBeStunned;
                            if (canBeHitStunned)
                            {
                                component.SetStun(StaticValues.solusInvalidatorInvalidateEffectDuration);
                            }
                            
                        }
                        else if (singularTarget.healthComponent.body.HasBuff(Buffs.solusPrimedDebuff) && !flag)
                        {
                            bool canBeHitStunned = component.canBeFrozen;
                            if (canBeHitStunned)
                            {
                                component.SetFrozen(StaticValues.solusInvalidatorInvalidateEffectDuration);
                            }                           

                        }


                        singularTarget.healthComponent.body.ApplyBuff(Buffs.solusPrimedDebuff.buffIndex, stacks + 1);



                        //EffectManager.SpawnEffect(EntityStates., new EffectData
                        //{
                        //    origin = singularTarget.transform.position,
                        //    scale = 1f,
                        //    rotation = Quaternion.LookRotation(direction),

                        //}, true);

                    }
                }
            }
        }

        private void SearchForTarget(CharacterBody charBody)
        {
            Ray aimRay = base.GetAimRay(); 
            if (Shiggycon.trackingTarget)
            {
                theSpot = Shiggycon.GetTrackingTarget().healthComponent.body.corePosition;
                range /= 2f;
                angle = 360f;
            }
            else if(!Shiggycon.trackingTarget)
            {
                theSpot = charBody.transform.position;
                angle = 180f;
            }

            this.search.teamMaskFilter = TeamMask.GetUnprotectedTeams(charBody.teamComponent.teamIndex);
            this.search.filterByLoS = true;
            this.search.searchOrigin = theSpot;
            this.search.searchDirection = aimRay.direction.normalized;
            this.search.sortMode = BullseyeSearch.SortMode.Distance;
            this.search.maxDistanceFilter = range;
            this.search.maxAngleFilter = angle;
            this.search.RefreshCandidates();
            this.search.FilterOutGameObject(charBody.gameObject);
            this.trackingTargets = this.search.GetResults().ToList<HurtBox>();


            EffectManager.SpawnEffect(Modules.ShiggyAsset.solusInvalidatorBlastEffectPrefab, new EffectData
            {
                origin = theSpot,
                scale = range,
            }, true);
        }


        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
