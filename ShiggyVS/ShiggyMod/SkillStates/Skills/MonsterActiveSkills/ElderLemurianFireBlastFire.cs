using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using System.Linq;
using EntityStates.Huntress;
using EntityStates.LemurianBruiserMonster;
using EntityStates.Mage.Weapon;
using System.Collections.Generic;
using R2API;

namespace ShiggyMod.SkillStates
{
    public class ElderLemurianFireBlastFire : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float baseDuration = 1f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;
        public HurtBox Target;
        private Animator animator;
        private float procCoefficient = Modules.StaticValues.elderlemurianfireblastProcCoefficient;
        private float force = 400f;
        private float damageCoefficient = Modules.StaticValues.elderlemurianfireblastDamageCoefficient;

        public GameObject blastEffectPrefab = Modules.Assets.elderlemurianexplosionEffect;
        private string rMuzzleString = "RHand";
        internal Vector3 moveVec;
        internal float damageMult;
        internal float radius;
        internal float chargePercent;
        private int baseMaxCharge = 3;
        private float maxCharge;
        internal int hitCount;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayAnimation("RightArm, Override", "RightArmPunch", "Attack.playbackRate", duration);
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            
            Util.PlaySound(FireMegaFireball.attackString, base.gameObject);
            if (FireMegaFireball.muzzleflashEffectPrefab)
            {
                EffectManager.SimpleMuzzleFlash(FireMegaFireball.muzzleflashEffectPrefab, base.gameObject, rMuzzleString, false);
            }

            float[] source = new float[]
            {
                this.attackSpeedStat,
                4f
            };
            this.maxCharge = (float)this.baseMaxCharge / source.Min();



        }
        public override void OnExit()
        {
            base.OnExit();
            if (base.isAuthority)
            {
                float num = 60f;
                Quaternion rotation = Util.QuaternionSafeLookRotation(base.characterDirection.forward.normalized);
                float num2 = 0.01f;
                rotation.x += UnityEngine.Random.Range(-num2, num2) * num;
                rotation.y += UnityEngine.Random.Range(-num2, num2) * num;

                BlastAttack blastAttack = new BlastAttack();

                blastAttack.position = moveVec;
                blastAttack.baseDamage = this.damageStat * damageCoefficient;
                blastAttack.baseForce = this.force;
                blastAttack.radius = this.radius;
                blastAttack.attacker = base.gameObject;
                blastAttack.inflictor = base.gameObject;
                blastAttack.teamIndex = base.teamComponent.teamIndex;
                blastAttack.crit = base.RollCrit();
                blastAttack.procChainMask = default(ProcChainMask);
                blastAttack.procCoefficient = procCoefficient;
                blastAttack.falloffModel = BlastAttack.FalloffModel.None;
                blastAttack.damageColorIndex = DamageColorIndex.Default;
                blastAttack.damageType = (Util.CheckRoll(Flamethrower.ignitePercentChance, base.characterBody.master) ? DamageType.IgniteOnHit : damageType);
                blastAttack.attackerFiltering = AttackerFiltering.Default;

                blastAttack.AddModdedDamageType(Modules.Damage.shiggyDecay);

                //Debug.Log(hitCount + "hitcount");
                for (int i = 0; i < hitCount; i++)
                {
                    if(blastAttack.Fire().hitCount > 0)
                    {
                        OnHitEnemyAuthority();
                    }
                    EffectManager.SpawnEffect(this.blastEffectPrefab, new EffectData
                    {
                        origin = moveVec,
                        scale = this.radius,
                        rotation = rotation
                    }, false);
                }
                    
                

            }


        }

        protected virtual void OnHitEnemyAuthority()
        {

        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            bool flag = base.fixedAge >= 0.1f && base.isAuthority;
            if (flag)
            {
                this.outer.SetNextStateToMain();
            }
        }


        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }

    }
}
