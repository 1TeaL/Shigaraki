using EntityStates;
using ExtraSkillSlots;
using RoR2;
using RoR2.Skills;
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
        protected bool exitOnDuration = false; // default behavior

        public SkillDef skillDef;

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


        }

        public override void OnExit()
        {
            base.OnExit();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();



            if (base.fixedAge >= this.duration && base.isAuthority && exitOnDuration)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }
        public void SetSkillDef(SkillDef skillDefofSKill)
        {
            this.skillDef = skillDefofSKill;

        }

        public bool IsHeldDown()
        {

            bool held =
                (inputBank.skill1.down && (characterBody.skillLocator.primary.skillDef = skillDef)) ||
                (inputBank.skill2.down && (characterBody.skillLocator.secondary.skillDef = skillDef)) ||
                (inputBank.skill3.down && (characterBody.skillLocator.utility.skillDef = skillDef)) ||
                (inputBank.skill4.down && (characterBody.skillLocator.special.skillDef = skillDef));

            var extraInput = GetComponent<ExtraInputBankTest>();
            var extras = GetComponent<ExtraSkillLocator>();
            if (!held && extraInput && extras)
            {
                held =
                    (extraInput.extraSkill1.down && (extras.extraFirst.skillDef = skillDef)) ||
                    (extraInput.extraSkill2.down && (extras.extraSecond.skillDef = skillDef)) ||
                    (extraInput.extraSkill3.down && (extras.extraThird.skillDef = skillDef)) ||
                    (extraInput.extraSkill4.down && (extras.extraFourth.skillDef = skillDef));
            }

            return held;
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
