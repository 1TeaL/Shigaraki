using EntityStates;
using ExtraSkillSlots;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Projectile;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class Skill : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float baseDuration = StaticValues.universalDuration;
        public float duration;
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

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            fireTime = duration * StaticValues.universalFiretime;
            hasFired = false;
            keepFiring = true;
            Ray aimRay = base.GetAimRay();
            //base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
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
