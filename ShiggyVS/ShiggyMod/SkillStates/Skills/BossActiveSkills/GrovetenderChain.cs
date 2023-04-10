﻿using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using EntityStates.GravekeeperBoss;
using RoR2.Projectile;
using System.Collections.Generic;
using System.Linq;
using IL.RoR2.UI;
using R2API.Networking;

namespace ShiggyMod.SkillStates
{
    public class GrovetenderChain : BaseSkillState
    { 
        public float baseDuration = 1f;
        public float duration;
        public ShiggyController Shiggycon;


        private float baseradius = Modules.StaticValues.grovetenderRadius;
        private float radius;
        private float force = -1000f;
        private Animator modelAnimator;
        public string muzzleString = "LHand";
        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            radius = baseradius * attackSpeedStat;
            if(radius < baseradius)
            {
                radius= baseradius;
            }
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            ChainNearby();
            this.modelAnimator = base.GetModelAnimator();
            ChildLocator component = this.modelAnimator.GetComponent<ChildLocator>();
            if (component)
            {
                component.FindChild(muzzleString);
            }
            Util.PlayAttackSpeedSound(FireHook.soundString, base.gameObject, this.attackSpeedStat);
            EffectManager.SimpleMuzzleFlash(FireHook.muzzleflashEffectPrefab, base.gameObject, muzzleString, false);

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("LeftArm, Override", "LeftArmPunch", "Attack.playbackRate", duration, 0.1f);
            AkSoundEngine.PostEvent(3660048432, base.gameObject);


            Shiggycon = gameObject.GetComponent<ShiggyController>();
            
        }
        public override void OnExit()
        {
            base.OnExit();
            PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            //PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
        }


        public void ChainNearby()
        {
            Ray aimRay = base.GetAimRay();
            BullseyeSearch search = new BullseyeSearch
            {

                teamMaskFilter = TeamMask.GetEnemyTeams(base.GetTeam()),
                filterByLoS = false,
                searchOrigin = aimRay.origin,
                searchDirection = UnityEngine.Random.onUnitSphere,
                sortMode = BullseyeSearch.SortMode.Distance,
                maxDistanceFilter = radius,
                maxAngleFilter = 360f
            };

            search.RefreshCandidates();
            search.FilterOutGameObject(base.gameObject);



            List<HurtBox> target = search.GetResults().ToList<HurtBox>();
            foreach (HurtBox singularTarget in target)
            {
                if (singularTarget)
                {
                    if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                    {
                        singularTarget.healthComponent.body.ApplyBuff(Modules.Buffs.grovetenderChainDebuff.buffIndex, 1, Modules.StaticValues.grovetenderDuration);

                        EffectManager.SpawnEffect(FireHook.muzzleflashEffectPrefab, new EffectData
                        {
                            origin = singularTarget.transform.position,
                            scale = 1f,
                            rotation = Quaternion.LookRotation(characterBody.transform.position)

                        }, true);
                    }
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();


            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}