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


        private float damageCoefficient = Modules.StaticValues.lunarwispminigunDamageCoefficient;
        private float procCoefficient = 0.2f;
        private float force = 60f;
        private string muzzleString;
        private Animator animator;
        private float baseFireInterval = 0.2f;
        private float baseBulletCount;

        private Run.FixedTimeStamp critEndTime;
        private Run.FixedTimeStamp lastCritCheck;
        private float fireTimer;
        private float bulletMaxDistance = 200f;

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
            this.muzzleString = "LHand";

            this.animator = base.GetModelAnimator();
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);

            PlayCrossfade("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", baseFireInterval, 0.1f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            baseBulletCount = 3;

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


            Shiggycon = gameObject.GetComponent<ShiggyController>();
            
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
            this.animator.SetBool("attacking", false);
            PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
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
                base.characterBody.SetAimTimer(1f);
                this.OnFireAuthority();
                Ray aimRay = base.GetAimRay();
                
            }
        }
        private void OnFireAuthority()
        {
            this.UpdateCrits();
            bool isCrit = !this.critEndTime.hasPassed;

            PlayCrossfade("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", baseFireInterval, 0.1f);

            EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);

            Ray aimRay = base.GetAimRay();
            new BulletAttack
            {
                bulletCount = (uint)baseBulletCount,
                aimVector = aimRay.direction,
                //origin = FindModelChild(this.muzzleString).transform.position,
                origin = aimRay.origin,
                damage = damageCoefficient * characterBody.damage,
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

            if (this.muzzleVFXInstanceOne)
            {
                vfxTimerOff += Time.fixedDeltaTime;
                if (vfxTimerOff > 2f)
                {
                    EntityState.Destroy(this.muzzleVFXInstanceOne.gameObject);
                    this.muzzleVFXInstanceOne = null;
                    vfxTimerOff = 0f;
                }
            }

            if (base.IsKeyDownAuthority())
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



        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
