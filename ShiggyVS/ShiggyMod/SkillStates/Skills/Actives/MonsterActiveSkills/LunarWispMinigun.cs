using EntityStates;
using EntityStates.LunarWisp;
using ExtraSkillSlots;
using RoR2;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class LunarWispMinigun : Skill
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";

        public float vfxTimerOn;
        public float vfxTimerOff;


        private float damageCoefficient = Modules.StaticValues.lunarwispminigunDamageCoefficient;
        private float procCoefficient = 0.2f;
        private float force = 60f;
        private string muzzleString;
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

        // ---- hold-to-fire binding (resolved once) ----
        private enum HeldSlot
        {
            None,
            Primary, Secondary, Utility, Special,
            Extra1, Extra2, Extra3, Extra4
        }

        private HeldSlot _heldSlot;

        public override void OnEnter()
        {
            base.OnEnter();
            damageType = new DamageTypeCombo(DamageType.CrippleOnHit, DamageTypeExtended.Generic, DamageSource.Secondary);
            keepFiring = true;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(2f);
            this.muzzleString = "LHand";

            this.animator = base.GetModelAnimator();
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            this.animator.SetBool("attacking", true);
            

            PlayCrossfade("LeftArm, Override", "LArmOutStart", "Attack.playbackRate", baseFireInterval, 0.1f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            // Resolve which button we should track for "hold to fire"
            _heldSlot = ResolveHeldSlot();
            if (_heldSlot == HeldSlot.None)
                _heldSlot = HeldSlot.Primary; // safe fallback
            baseBulletCount = StaticValues.lunarwispminigunBullets;

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
            //PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            //PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
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

            //PlayCrossfade("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", baseFireInterval, 0.1f);

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
                falloffModel = BulletAttack.FalloffModel.DefaultBullet,
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
            base.characterBody.SetAimTimer(1f);
            if (!IsHeldDown())
            {
                if (base.isAuthority)
                {
                    this.outer.SetNextStateToMain();
                }
                return;
            }

            // Fire loop
            fireTimer -= Time.fixedDeltaTime;
            if (fireTimer <= 0f)
            {
                fireTimer += (baseFireInterval / this.attackSpeedStat);
                OnFireShared();
            }

        }


        private HeldSlot ResolveHeldSlot()
        {
            // Base slots
            var sl = characterBody ? characterBody.skillLocator : null;
            if (sl != null)
            {
                if (sl.primary != null && sl.primary.skillDef == Shiggy.claytemplarminigunDef) return HeldSlot.Primary;
                if (sl.secondary != null && sl.secondary.skillDef == Shiggy.claytemplarminigunDef) return HeldSlot.Secondary;
                if (sl.utility != null && sl.utility.skillDef == Shiggy.claytemplarminigunDef) return HeldSlot.Utility;
                if (sl.special != null && sl.special.skillDef == Shiggy.claytemplarminigunDef) return HeldSlot.Special;
            }

            // Extra slots
            var extras = GetComponent<ExtraSkillLocator>();
            if (extras != null)
            {
                if (extras.extraFirst != null && extras.extraFirst.skillDef == Shiggy.claytemplarminigunDef) return HeldSlot.Extra1;
                if (extras.extraSecond != null && extras.extraSecond.skillDef == Shiggy.claytemplarminigunDef) return HeldSlot.Extra2;
                if (extras.extraThird != null && extras.extraThird.skillDef == Shiggy.claytemplarminigunDef) return HeldSlot.Extra3;
                if (extras.extraFourth != null && extras.extraFourth.skillDef == Shiggy.claytemplarminigunDef) return HeldSlot.Extra4;
            }

            return HeldSlot.None;
        }

        private bool IsHeldDown()
        {
            if (!inputBank) return false;

            // Base buttons
            switch (_heldSlot)
            {
                case HeldSlot.Primary: return inputBank.skill1.down;
                case HeldSlot.Secondary: return inputBank.skill2.down;
                case HeldSlot.Utility: return inputBank.skill3.down;
                case HeldSlot.Special: return inputBank.skill4.down;
            }

            // Extra buttons
            var extraInput = GetComponent<ExtraInputBankTest>();
            if (!extraInput) return false;

            switch (_heldSlot)
            {
                case HeldSlot.Extra1: return extraInput.extraSkill1.down;
                case HeldSlot.Extra2: return extraInput.extraSkill2.down;
                case HeldSlot.Extra3: return extraInput.extraSkill3.down;
                case HeldSlot.Extra4: return extraInput.extraSkill4.down;
                default: return false;
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
