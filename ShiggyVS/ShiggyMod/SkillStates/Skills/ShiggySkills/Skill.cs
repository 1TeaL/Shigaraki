using EntityStates;
using ExtraSkillSlots;
using RoR2;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class Skill : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float baseDuration = StaticValues.universalDuration;
        public float duration;
        public ShiggyMasterController Shiggymastercon;
        public ShiggyController Shiggycon;
        public EnergySystem energySystem;
        public ExtraInputBankTest extrainputBankTest;
        public ExtraSkillLocator extraskillLocator;
        public DamageType damageType;
        public HurtBox Target;
        public Animator animator;
        public float fireTime;
        public bool keepFiring;
        public bool hasFired;


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.decayDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 1f;
        private float speedOverride = -1f;

        public enum HeldSlot
        {
            None,
            Primary, Secondary, Utility, Special,
            Extra1, Extra2, Extra3, Extra4
        }

        public HeldSlot _heldSlot;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            fireTime = duration * StaticValues.universalFiretime;
            hasFired = false;
            keepFiring = true;
            Ray aimRay = base.GetAimRay();
            //base.characterBody.SetAimTimer(this.duration);
            this.animator = base.GetModelAnimator();
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            Shiggymastercon = gameObject.GetComponent<ShiggyMasterController>();
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            energySystem = gameObject.GetComponent<EnergySystem>();
            extraskillLocator = base.GetComponent<ExtraSkillLocator>();
            extrainputBankTest = outer.GetComponent<ExtraInputBankTest>();
            damageType = new DamageTypeCombo(DamageType.Generic, DamageTypeExtended.Generic, DamageSource.Secondary);

            _heldSlot = ResolveHeldSlot();
            if (_heldSlot == HeldSlot.None)
                _heldSlot = HeldSlot.Primary; // safe fallback


        }

        public override void OnExit()
        {
            base.OnExit();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();



            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }


        public HeldSlot ResolveHeldSlot()
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

        public bool IsHeldDown()
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
