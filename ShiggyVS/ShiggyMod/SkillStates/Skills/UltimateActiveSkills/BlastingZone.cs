﻿using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using EntityStates;
using System.Collections.Generic;
using System.Linq;
using static RoR2.BlastAttack;
using R2API.Networking.Interfaces;
using R2API.Networking;
using System;
using ShiggyMod.Modules;
using R2API.Utils;

namespace ShiggyMod.SkillStates
{
    public class BlastingZone : BaseSkillState
    {
        //Orbital strike + blast burn
        public static float blastRadius = StaticValues.blastingZoneRadius;

        public float range = StaticValues.blastingZoneRadius;
        public float rangeaddition = StaticValues.blastingZoneRangeAddition;
        public float force = 200f;
        private BlastAttack blastAttack;
        public Vector3 theSpot;
        public Vector3 directionExtension;
        public float interval;
        public int currentHits = 0;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            //hasFired = false;
            theSpot = aimRay.origin + range * aimRay.direction;
            directionExtension = aimRay.direction * rangeaddition;
            base.StartAimMode(1f, true);

            base.characterMotor.disableAirControlUntilCollision = false;

            //create a giant blade of energy particle?

            //play squall blasting zone animation?
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("FullBody, Override", "StLouis45", "Attack.playbackRate", StaticValues.blastingZoneWindup, 0.01f);


            blastAttack = new BlastAttack();
            blastAttack.radius = blastRadius;
            blastAttack.procCoefficient = StaticValues.blastingZoneProcCoefficient;
            blastAttack.position = theSpot;
            blastAttack.damageType = default;
            blastAttack.attacker = base.gameObject;
            blastAttack.crit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master);
            blastAttack.baseDamage = base.characterBody.damage * Modules.StaticValues.blastingZoneDamageCoefficient;
            blastAttack.falloffModel = BlastAttack.FalloffModel.None;
            blastAttack.baseForce = force;
            blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
            blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;

        }

        protected virtual void OnHitEnemyAuthority(BlastAttack.Result result)
        {
            foreach (BlastAttack.HitPoint hitpoint in result.hitPoints)
            {
                //apply debuff to enemies hit
                hitpoint.hurtBox.healthComponent.body.ApplyBuff(Buffs.blastingZoneBurnDebuff.buffIndex, StaticValues.blastingZoneDebuffStackApplication);
                BlastingZoneBurnComponent blazCon = hitpoint.hurtBox.healthComponent.body.gameObject.GetComponent<BlastingZoneBurnComponent>();
                if (!blazCon)
                {
                    blazCon = hitpoint.hurtBox.healthComponent.body.gameObject.AddComponent<BlastingZoneBurnComponent>();
                    blazCon.attackerBody = characterBody;
                    //add recognition to the characterbody and whatnot
                }
            }

        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Ray aimRay = base.GetAimRay();



            if ((base.fixedAge >= StaticValues.blastingZoneWindup) && base.isAuthority)
            {
                if (interval < StaticValues.blastingZoneInterval / attackSpeedStat)
                {
                    interval += Time.fixedDeltaTime;
                }
                else if (interval >= StaticValues.blastingZoneInterval / attackSpeedStat)
                {
                    interval = 0f;
                    if(currentHits < StaticValues.blastingZoneTotalHits)
                    {
                        currentHits++;
                        blastAttack.position = theSpot;
                        theSpot += directionExtension;
                        interval = 0f;
                        BlastAttack.Result result = blastAttack.Fire();
                        if (result.hitCount > 0)
                        {
                            this.OnHitEnemyAuthority(result);
                        }
                        //test which one to use
                        EffectManager.SpawnEffect(Modules.Assets.magmaWormOrbExplosionPrefab, new EffectData
                        {
                            origin = theSpot,
                            scale = blastRadius,
                            rotation = Util.QuaternionSafeLookRotation(aimRay.direction)

                        }, true);
                        EffectManager.SpawnEffect(Modules.Assets.magmaWormImpactExplosionPrefab, new EffectData
                        {
                            origin = theSpot,
                            scale = blastRadius,
                            rotation = Util.QuaternionSafeLookRotation(aimRay.direction)

                        }, true);
                    }
                    else if (currentHits >= StaticValues.blastingZoneTotalHits)
                    {
                        this.outer.SetNextStateToMain();
                        return;
                    }
                }
                
            }


        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}