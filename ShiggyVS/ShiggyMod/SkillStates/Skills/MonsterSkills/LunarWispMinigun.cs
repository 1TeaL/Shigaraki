using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using EntityStates.LunarWisp;

namespace ShiggyMod.SkillStates
{
    public class LunarWispMinigun : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";

        public float vfxTimerOn;
        public float vfxTimerOff;
        public ShiggyController Shiggycon;
        private DamageType damageType;


        private float damageCoefficient = Modules.StaticValues.lunarwispminigunDamageCoeffecient;
        private float procCoefficient = 0.2f;
        private float force = 60f;
        private string muzzleString;
        private float baseFireInterval = 0.2f;
        private float baseBulletCount;

        private Run.FixedTimeStamp critEndTime;
        private Run.FixedTimeStamp lastCritCheck;
        private float fireTimer;
        private float bulletMaxDistance = 100f;

        private Transform muzzleTransformOne;
        private GameObject muzzleVFXInstanceOne;
        private uint windLoopSoundID;
        private uint shootLoopSoundID;

        public override void OnEnter()
        {
            base.OnEnter();
            damageType = DamageType.CrippleOnHit;

            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(2f);
            this.muzzleString = "RHand";


            if (base.HasBuff(Modules.Buffs.multiplierBuff))
            {
                baseBulletCount = 3f * Modules.StaticValues.multiplierCoefficient;
            }
            else
            {
                baseBulletCount = 3f;
            }

            this.muzzleTransformOne = base.FindModelChild(this.muzzleString);
            if (this.muzzleTransformOne && FireLunarGuns.muzzleVfxPrefab)
            {
                this.muzzleVFXInstanceOne = UnityEngine.Object.Instantiate<GameObject>(FireLunarGuns.muzzleVfxPrefab, this.muzzleTransformOne.position, Util.QuaternionSafeLookRotation(aimRay.direction));
                this.muzzleVFXInstanceOne.transform.parent = this.muzzleTransformOne;
            }
            this.critEndTime = Run.FixedTimeStamp.negativeInfinity;
            this.lastCritCheck = Run.FixedTimeStamp.negativeInfinity;
            this.windLoopSoundID = Util.PlaySound(FireLunarGuns.windLoopSound, base.gameObject);
            this.shootLoopSoundID = Util.PlaySound(FireLunarGuns.shootLoopSound, base.gameObject);
        }
        private void UpdateCrits()
        {
            if (this.lastCritCheck.timeSince >= 1f)
            {
                this.lastCritCheck = Run.FixedTimeStamp.now;
                if (base.RollCrit())
                {
                    this.critEndTime = Run.FixedTimeStamp.now + 2f;
                }
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Util.PlaySound(FireLunarGuns.windDownSound, base.gameObject);
            if (this.muzzleVFXInstanceOne)
            {
                EntityState.Destroy(this.muzzleVFXInstanceOne.gameObject);
                this.muzzleVFXInstanceOne = null;
            }
            AkSoundEngine.StopPlayingID(this.windLoopSoundID);
            AkSoundEngine.StopPlayingID(this.shootLoopSoundID);
        }

        private void OnFireShared()
        {
            Util.PlaySound(FireLunarGuns.fireSound, base.gameObject);
            if (base.isAuthority)
            {
                this.OnFireAuthority();
                Ray aimRay = base.GetAimRay();
                
            }
        }
        private void OnFireAuthority()
        {
            this.UpdateCrits();
            bool isCrit = !this.critEndTime.hasPassed;

            //EffectManager.SimpleMuzzleFlash(FireLunarGuns.muzzleVfxPrefab, base.gameObject, this.muzzleString, false);

            Ray aimRay = base.GetAimRay();
            new BulletAttack
            {
                bulletCount = (uint)baseBulletCount,
                aimVector = aimRay.direction,
                origin = aimRay.origin,
                damage = damageCoefficient,
                damageColorIndex = DamageColorIndex.Default,
                damageType = damageType,
                falloffModel = BulletAttack.FalloffModel.None,
                maxDistance = bulletMaxDistance,
                force = force,
                hitMask = LayerIndex.CommonMasks.bullet,
                minSpread = FireLunarGuns.bulletMinSpread,
                maxSpread = FireLunarGuns.bulletMaxSpread,
                isCrit = isCrit,
                owner = base.gameObject,
                muzzleName = this.muzzleString,
                smartCollision = false,
                procChainMask = default(ProcChainMask),
                procCoefficient = procCoefficient,
                radius = 0.4f,
                sniper = false,
                stopperMask = LayerIndex.CommonMasks.bullet,
                weapon = null,
                tracerEffectPrefab = FireLunarGuns.bulletTracerEffectPrefab,
                spreadPitchScale = 1f,
                spreadYawScale = 1f,
                queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                hitEffectPrefab = FireLunarGuns.bulletHitEffectPrefab,
                HitEffectNormal = FireLunarGuns.bulletHitEffectNormal
            }.Fire();
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            vfxTimerOff += Time.fixedDeltaTime;
            if(vfxTimerOff > 1f)
            {
                vfxTimerOff = 0f;
                if (this.muzzleVFXInstanceOne)
                {
                    EntityState.Destroy(this.muzzleVFXInstanceOne.gameObject);
                    this.muzzleVFXInstanceOne = null;
                }
            }
            

            if (base.skillLocator.primary.skillNameToken == prefix + "LUNARWISP_NAME")
            {
                if (base.inputBank.skill1.down)
                {
                    this.fireTimer -= Time.fixedDeltaTime;
                    if (this.fireTimer <= 0f)
                    {
                        float num = baseFireInterval / this.attackSpeedStat;
                        this.fireTimer += num;
                        this.OnFireShared();
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
            if (base.skillLocator.secondary.skillNameToken == prefix + "LUNARWISP_NAME")
            {
                if (base.inputBank.skill2.down)
                {
                    this.fireTimer -= Time.fixedDeltaTime;
                    if (this.fireTimer <= 0f)
                    {
                        float num = baseFireInterval / this.attackSpeedStat;
                        this.fireTimer += num;
                        this.OnFireShared();
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
            if (base.skillLocator.utility.skillNameToken == prefix + "LUNARWISP_NAME")
            {
                if (base.inputBank.skill3.down)
                {
                    this.fireTimer -= Time.fixedDeltaTime;
                    if (this.fireTimer <= 0f)
                    {
                        float num = baseFireInterval / this.attackSpeedStat;
                        this.fireTimer += num;
                        this.OnFireShared();

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
            if (base.skillLocator.special.skillNameToken == prefix + "LUNARWISP_NAME")
            {
                if (base.inputBank.skill4.down)
                {
                    this.fireTimer -= Time.fixedDeltaTime;
                    if (this.fireTimer <= 0f)
                    {
                        float num = baseFireInterval / this.attackSpeedStat;
                        this.fireTimer += num;
                        this.OnFireShared();
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

        }



        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
