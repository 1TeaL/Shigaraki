using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using EntityStates.VoidSurvivor;

namespace ShiggyMod.SkillStates
{
    public class RapidPierce : BaseSkillState
    {
        public float baseDuration = 0.7f;
        public float duration;
        private float fireTime;
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
            this.duration = this.baseDuration / this.attackSpeedStat;
            this.fireTime = 0.2f * this.duration;
            hasFired = false;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("LeftArm, Override", "LeftArmPunch","Attack.playbackRate", fireTime, 0.1f);
            AkSoundEngine.PostEvent(3660048432, base.gameObject);
            this.muzzleString = "RHand";
            //EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
            //EffectManager.SimpleMuzzleFlash(Modules.Assets.voidfiendblinkmuzzleEffect, base.gameObject, this.muzzleString, false);


            Shiggycon = gameObject.GetComponent<ShiggyController>();
            
        }

        public override void OnExit()
        {
            base.OnExit();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge >= this.fireTime && !hasFired)
            {
                //PlayCrossfade("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", fireTime, 0.1f);
                hasFired = true;

                Ray aimRay = base.GetAimRay();
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
                    radius = 1.5f,
                    sniper = false,
                    stopperMask = LayerIndex.noCollision.mask,
                    weapon = null,
                    tracerEffectPrefab = Modules.Assets.railgunnerSnipeLightTracerEffect,
                    spreadPitchScale = 0f,
                    spreadYawScale = 0f,
                    queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                    hitEffectPrefab = EntityStates.Commando.CommandoWeapon.FirePistol2.hitEffectPrefab,

                };
                bulletAttack.Fire();
            }


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
