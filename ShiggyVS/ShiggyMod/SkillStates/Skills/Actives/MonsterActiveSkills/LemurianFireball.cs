using EmotesAPI;
using EntityStates;
using EntityStates.LemurianMonster;
using ExtraSkillSlots;
using Rewired.ComponentControls.Data;
using RoR2;
using RoR2.Projectile;
using ShiggyMod.Modules.Survivors;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class LemurianFireball : Skill
    {

        public static GameObject effectPrefab;

        private string muzzleString;
        private float damageCoefficient = Modules.StaticValues.lemurianfireballDamageCoefficient;
        private float force = 1f;
        private float speedOverride = -1f;
        private GameObject chargeVfxInstance;
        //public bool isContinued;
        public float timer;
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
            Ray aimRay = base.GetAimRay();
            baseDuration = 0.5f;
            duration = baseDuration / attackSpeedStat;


            // Resolve which button we should track for "hold to fire"
            _heldSlot = ResolveHeldSlot();
            if (_heldSlot == HeldSlot.None)
                _heldSlot = HeldSlot.Primary; // safe fallback

            base.characterBody.SetAimTimer(this.duration);
            this.muzzleString = "LHand";
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            this.animator = base.GetModelAnimator();
            base.GetModelAnimator().SetBool("attacking", true);
            //this.animator.SetBool("attacking", true);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat* 2f);
            PlayCrossfade("LeftArm, Override", "LArmOutStart", "Attack.playbackRate", duration / 2, 0.1f);
            //PlayCrossfade("LeftArm, Override", "LeftArmPunch", "Attack.playbackRate", duration/2, 0.1f);
            if (transform && ChargeFireball.chargeVfxPrefab)
            {
                this.chargeVfxInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeFireball.chargeVfxPrefab, FindModelChild(this.muzzleString).position, Util.QuaternionSafeLookRotation(aimRay.direction));
                this.chargeVfxInstance.transform.parent = FindModelChild(this.muzzleString).transform;
            }

            

            

        }
        public void FireBall()
        {
            Ray aimRay = base.GetAimRay();
            
            EffectManager.SimpleMuzzleFlash(FireFireball.effectPrefab, base.gameObject, muzzleString, false);
            
            bool isAuthority = base.isAuthority;
            if (isAuthority)
            {

                ProjectileManager.instance.FireProjectile(
                    Modules.Projectiles.lemurianFireBall, //prefab
                    aimRay.origin, //position
                    Util.QuaternionSafeLookRotation(aimRay.direction), //rotation
                    base.gameObject, //owner
                    this.damageStat * damageCoefficient, //damage
                    force, //force
                    Util.CheckRoll(this.critStat, base.characterBody.master), //crit
                    DamageColorIndex.Default, //damage color
                    null, //target
                    speedOverride); //speed }


                //ProjectileManager.instance.FireProjectile(
                //    FireFireball.projectilePrefab, //prefab
                //    FindModelChild(this.muzzleString).position, //position
                //    Util.QuaternionSafeLookRotation(aimRay.direction), //rotation
                //    base.gameObject, //owner
                //    0f, //damage
                //    0f, //force
                //    Util.CheckRoll(this.critStat, base.characterBody.master), //crit
                //    DamageColorIndex.Default, //damage color
                //    null, //target
                //    speedOverride); //speed }
            }

        }

        public override void OnExit()
        {
            base.OnExit();
            base.GetModelAnimator().SetBool("attacking", false);
            //PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            //PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            if (this.chargeVfxInstance)
            {
                EntityState.Destroy(this.chargeVfxInstance);
            }
        }


        public override void FixedUpdate()
        {
            base.characterBody.SetAimTimer(1f);
            timer += Time.fixedDeltaTime;
            if (timer >= this.fireTime && base.isAuthority)
            {
                timer -= fireTime;
                FireBall();
            }

            if (base.fixedAge >= this.duration && base.isAuthority && IsHeldDown())
            {
                this.outer.SetNextStateToMain();
                return;

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
