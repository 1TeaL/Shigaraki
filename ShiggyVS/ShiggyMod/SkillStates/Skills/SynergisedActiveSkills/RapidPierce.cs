using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using EntityStates.VoidSurvivor;
using HG;
using static RoR2.BulletAttack;
using static RoR2.BlastAttack;
using System.Collections.Generic;
using EmotesAPI;
using System.Reflection;

namespace ShiggyMod.SkillStates
{
    public class RapidPierce : BaseSkillState
    {
        private float baseFireInterval = 1.5f;
        private float fireInterval;
        private float fireTimer;
        public int shotsHit;
        public bool hasFired;
        public ShiggyController Shiggycon;
        private DamageType damageType = DamageType.Generic;

        private float range = 200f;
        private float damageCoefficient = Modules.StaticValues.rapidPierceDamageCoefficient;
        private float procCoefficient = Modules.StaticValues.rapidPierceProcCoefficient;
        private float force = 200f;
        private string muzzleString;

        public override void OnEnter()
        {
            base.OnEnter();
            fireInterval = baseFireInterval / (this.attackSpeedStat * (1f + shotsHit / 5f));


            if (shotsHit < 0)
            {
                shotsHit = 0;
            }
            hasFired = false;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(1f);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
            //base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", fireInterval, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
            this.muzzleString = "RHand";


            Shiggycon = gameObject.GetComponent<ShiggyController>();
            
        }

        public override void OnExit()
        {
            base.OnExit();
        }
         public void Fire()
        {
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
            //base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", fireInterval, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }

            base.characterBody.AddSpreadBloom(1f);
            base.AddRecoil(-1f , 2f, -0.5f , 0.5f);
            EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
            Ray aimRay = base.GetAimRay();
            bool hasHit = false;
            Vector3 hitPoint = Vector3.zero;
            float hitDistance = 0f;
            HealthComponent hitHealthComponent = null;

            var bulletAttack = new BulletAttack
            {
                bulletCount = 1,
                aimVector = aimRay.direction,
                origin = aimRay.origin,
                damage = this.damageStat * damageCoefficient,
                damageColorIndex = DamageColorIndex.Default,
                damageType = damageType,
                falloffModel = BulletAttack.FalloffModel.None,
                maxDistance = range,
                force = force,
                hitMask = LayerIndex.CommonMasks.bullet,
                minSpread = 0f,
                maxSpread = 0f,
                isCrit = base.RollCrit(),
                owner = base.gameObject,
                muzzleName = muzzleString,
                smartCollision = false,
                procChainMask = default(ProcChainMask),
                procCoefficient = procCoefficient,
                radius = 1f,
                sniper = false,
                stopperMask = LayerIndex.noCollision.mask,
                weapon = null,
                tracerEffectPrefab = Modules.Assets.railgunnerSnipeLightTracerEffect,
                spreadPitchScale = 1f,
                spreadYawScale = 1f,
                queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                hitEffectPrefab = Modules.Assets.railgunnerHitSparkEffect,

            };
            bulletAttack.hitCallback = delegate (BulletAttack bulletAttackRef, ref BulletHit hitInfo)
            {
                var result = BulletAttack.defaultHitCallback(bulletAttackRef, ref hitInfo);
                if (hitInfo.hitHurtBox)
                {
                    hasHit = true;
                    hitPoint = hitInfo.point;
                    hitDistance = hitInfo.distance;

                    hitHealthComponent = hitInfo.hitHurtBox.healthComponent;
                    //hitHealthComponent.body.AddBuff();

                }
                return result;
            };
            bulletAttack.filterCallback = delegate (BulletAttack bulletAttackRef, ref BulletAttack.BulletHit info)
            {
                return (!info.entityObject || info.entityObject != bulletAttack.owner) && BulletAttack.defaultFilterCallback(bulletAttackRef, ref info);
            };

            bulletAttack.Fire();
            if(hasHit)
            {
                shotsHit++;
            }
            else if (!hasHit)
            {
                shotsHit -= shotsHit/2;
            }

            fireInterval = baseFireInterval / (this.attackSpeedStat * (1f + shotsHit / 5f));
        }



        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.IsKeyDownAuthority())
            {
                this.fireTimer -= Time.fixedDeltaTime;
                if (this.fireTimer <= 0f)
                {
                    float num = fireInterval;
                    this.fireTimer += num;
                    this.Fire();
                }

            }
            else
            {
                if (base.isAuthority)
                {
                    this.outer.SetNextStateToMain();
                    return;

                }

            }
        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
