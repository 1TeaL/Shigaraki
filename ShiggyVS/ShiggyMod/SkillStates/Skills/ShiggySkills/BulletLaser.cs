using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using EntityStates.VoidSurvivor;
using ShiggyMod.Modules;

namespace ShiggyMod.SkillStates
{
    public class BulletLaser : BaseSkillState
    {
        public float baseDuration = 1f;
        public float duration;
        private float fireTime;
        public bool hasFired;
        public ShiggyController Shiggycon;
        private DamageType damageType;

        private float range = 100f;
        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.bulletlaserDamageCoefficient;
        private float procCoefficient = Modules.StaticValues.bulletlaserProcCoefficient;
        private float force = 1f;
        private float speedOverride =1f;
        private string muzzleString;
        private uint bulletcount;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            this.fireTime = 0.2f * this.duration;
            hasFired = false;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
            base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); } 
            }
            this.muzzleString = "LHand";
            //EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
            //EffectManager.SimpleMuzzleFlash(Modules.Assets.voidfiendblinkmuzzleEffect, base.gameObject, this.muzzleString, false);


            Shiggycon = gameObject.GetComponent<ShiggyController>();
            

            bulletcount = 5 ;
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
                hasFired = true;

                Ray aimRay = base.GetAimRay();
                var bulletAttack = new BulletAttack
                {
                    bulletCount = bulletcount,
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
                    tracerEffectPrefab = Modules.Assets.VoidFiendBeamTracer,
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
