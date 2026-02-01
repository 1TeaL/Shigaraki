using EntityStates;
using EntityStates.LemurianMonster;
using ExtraSkillSlots;
using RoR2;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class BlastBurn : Skill
    {
        //Elder lemurian and lemurian
        private BlastAttack blastAttack;
        public float fireInterval;
        public float stopwatch;
        public float radius;
        private float damage;
        private string muzzleString = "RHand";

        private GameObject chargeVfxInstance;
        public override void OnEnter()
        {
            base.OnEnter();
            fireInterval = StaticValues.blastBurnBaseInterval / attackSpeedStat;
            radius = StaticValues.blastBurnStartRadius;

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            //base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", fireInterval, 0.05f);
            this.animator.SetBool("attacking", true);
            base.PlayCrossfade("RightArm, Override", "RArmOutStart", "Attack.playbackRate", fireInterval, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }

            Ray aimRay = base.GetAimRay();
            damage = characterBody.damage * StaticValues.blastBurnDamageCoefficient;

            blastAttack = new BlastAttack();
            blastAttack.radius = radius;
            blastAttack.procCoefficient = StaticValues.blastBurnProcCoefficient;
            blastAttack.position = aimRay.origin;
            blastAttack.attacker = base.gameObject;
            blastAttack.crit = characterBody.RollCrit();
            blastAttack.baseDamage = damage;
            blastAttack.falloffModel = BlastAttack.FalloffModel.None;
            blastAttack.baseForce = 400f;
            blastAttack.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
            blastAttack.damageType = new DamageTypeCombo(DamageType.IgniteOnHit, DamageTypeExtended.Generic, DamageSource.Secondary);
            blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;

            //play anim
            if (transform && ChargeFireball.chargeVfxPrefab)
            {
                this.chargeVfxInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeFireball.chargeVfxPrefab, FindModelChild(this.muzzleString).position, Util.QuaternionSafeLookRotation(aimRay.direction));
                this.chargeVfxInstance.transform.parent = FindModelChild(this.muzzleString).transform;
            }

            SetSkillDef(Shiggy.blastBurnDef);
        }

        public override void OnExit()
        {
            base.OnExit();
            this.animator.SetBool("attacking", false);
            if (this.chargeVfxInstance)
            {
                EntityState.Destroy(this.chargeVfxInstance);
            }

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!IsHeldDown())
            {
                if (base.isAuthority)
                {
                    this.outer.SetNextStateToMain();
                }
                return;
            }
            else
            {
                stopwatch += Time.fixedDeltaTime;
                if (stopwatch > fireInterval)
                {
                    stopwatch = 0f;
                    Ray aimRay = base.GetAimRay();
                    EffectManager.SpawnEffect(ShiggyAsset.elderlemurianexplosionEffect, new EffectData
                    {
                        origin = aimRay.origin,
                        scale = radius,
                        rotation = Quaternion.identity,

                    }, true);

                    blastAttack.crit = characterBody.RollCrit();
                    blastAttack.baseDamage = damage;
                    blastAttack.radius = radius;
                    blastAttack.position = aimRay.origin;
                    blastAttack.Fire();
                    EffectManager.SimpleMuzzleFlash(FireFireball.effectPrefab, base.gameObject, muzzleString, false);
                    int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
                    //base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", fireInterval, 0.05f);
                    base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", fireInterval, 0.05f);

                    //increment radius size after each attack
                    radius += StaticValues.blastBurnIncrementRadius;
                    damage += characterBody.damage * StaticValues.blastBurnDamageCoefficientGain;


                }
            }

        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}