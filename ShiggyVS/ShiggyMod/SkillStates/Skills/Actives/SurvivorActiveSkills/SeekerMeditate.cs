using EntityStates;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Networking;
using ShiggyMod.Modules.Survivors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class SeekerMeditate : Skill
    {

        private BullseyeSearch search;
        private List<HurtBox> trackingTargets;

        private string muzzleString;
        private Vector3 direction = Vector3.down;
        private float range;
        private ChildLocator childLocator;
        private GameObject chargeEffect;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            range = StaticValues.seekerMeditateRadius * attackSpeedStat;
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("FullBody, Override", "BothHandSnap2", "Attack.playbackRate", fireTime, 0.1f);
            this.muzzleString = "LHand";


            Transform modelTransform = base.GetModelTransform();
            if (modelTransform)
            {
                this.childLocator = modelTransform.GetComponent<ChildLocator>();
                this.animator = modelTransform.GetComponent<Animator>();
            }


            Shiggycon = gameObject.GetComponent<ShiggyController>();

            search = new BullseyeSearch();



        }
        public override void OnExit()
        {
            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.fixedAge > fireTime && !hasFired)
            {

                hasFired = true;


                //search for target
                SearchForTarget(characterBody);
                //Damage target and stun
                HealTargets(characterBody);


            }
            if (base.fixedAge > duration)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }


        private void HealTargets(CharacterBody charBody)
        {

            if (trackingTargets.Count > 0)
            {
                foreach (HurtBox singularTarget in trackingTargets)
                {

                    if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                    {

                        EffectManager.SpawnEffect(ShiggyAsset.seekerChakraEffect, new EffectData
                        {
                            origin = characterBody.corePosition,
                            scale = 1f,
                            rotation = Quaternion.LookRotation(direction),

                        }, true);

                        //heal 25% Max HP- can overheal to give barrier
                        float healAmount = singularTarget.healthComponent.fullHealth * StaticValues.seekerMeditateCoeffecient;
                        float missingHP = singularTarget.healthComponent.fullHealth - singularTarget.healthComponent.health;
                        float extraBarrier = healAmount - missingHP;

                        if (missingHP >= singularTarget.healthComponent.fullHealth * StaticValues.seekerMeditateCoeffecient)
                        {
                            new HealNetworkRequest(singularTarget.healthComponent.body.masterObjectId, healAmount).Send(NetworkDestination.Clients);
                        }
                        else if (missingHP < singularTarget.healthComponent.fullHealth * StaticValues.seekerMeditateCoeffecient)
                        {
                            new HealNetworkRequest(singularTarget.healthComponent.body.masterObjectId, missingHP).Send(NetworkDestination.Clients);
                            singularTarget.healthComponent.AddBarrierAuthority(extraBarrier);

                        }
                    }
                }
            }
        }

        private void SearchForTarget(CharacterBody charBody)
        {
            TeamMask mask = TeamMask.none;
            mask.AddTeam(TeamIndex.Player);
            search.teamMaskFilter = mask;
            this.search.filterByLoS = true;
            this.search.searchOrigin = charBody.transform.position;
            this.search.searchDirection = Vector3.up;
            this.search.sortMode = BullseyeSearch.SortMode.Distance;
            this.search.maxDistanceFilter = range;
            this.search.maxAngleFilter = 360f;
            this.search.RefreshCandidates();
            this.trackingTargets = this.search.GetResults().ToList<HurtBox>();
        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}