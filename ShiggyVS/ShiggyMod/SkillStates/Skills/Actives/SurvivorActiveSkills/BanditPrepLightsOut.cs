using EntityStates;
using RoR2;
using RoR2.UI;
using ShiggyMod.Modules.Survivors;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class BanditPrepLightsOut : Skill
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.decayDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 1f;
        private float speedOverride = -1f;
        private CrosshairUtils.OverrideRequest crosshairOverrideRequest;
        public GameObject crosshairOverridePrefab = Modules.ShiggyAsset.banditCrosshair;

        public override void OnEnter()
        {
            base.OnEnter();
            baseDuration = 0.5f;
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            Util.PlaySound(EntityStates.Bandit2.StealthMode.enterStealthSound, base.gameObject);

            base.characterBody.AddTimedBuffAuthority(RoR2Content.Buffs.Cloak.buffIndex, Modules.StaticValues.banditcloakDuration);
            base.characterBody.AddTimedBuffAuthority(RoR2Content.Buffs.CloakSpeed.buffIndex, Modules.StaticValues.banditcloakDuration);
            PlayAnimation("RightArm, Override", "RHandFingerGunStart", "Attack.playbackRate", 1f);
            if (this.crosshairOverridePrefab)
            {
                this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, this.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
            }

        }

        public override void OnExit()
        {
            CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
            if (overrideRequest != null)
            {
                overrideRequest.Dispose();
            }
            base.OnExit();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.inputBank.skill1.down && characterBody.skillLocator.primary.skillDef == Shiggy.banditlightsoutDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill2.down && characterBody.skillLocator.secondary.skillDef == Shiggy.banditlightsoutDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill3.down && characterBody.skillLocator.utility.skillDef == Shiggy.banditlightsoutDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill4.down && characterBody.skillLocator.special.skillDef == Shiggy.banditlightsoutDef)
            {

                keepFiring = true;
            }
            else if (extrainputBankTest.extraSkill1.down && extraskillLocator.extraFirst.skillDef == Shiggy.banditlightsoutDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill2.down && extraskillLocator.extraSecond.skillDef == Shiggy.banditlightsoutDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill3.down && extraskillLocator.extraThird.skillDef == Shiggy.banditlightsoutDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill4.down && extraskillLocator.extraFourth.skillDef == Shiggy.banditlightsoutDef)
            {

                keepFiring = true;
            }
            else
            {
                keepFiring = false;
            }

            if (base.characterBody)
            {
                base.characterBody.SetAimTimer(this.duration);
            }
            if (base.fixedAge >= this.duration && base.isAuthority && !keepFiring)
            {
                this.outer.SetNextState(new BanditFireLightsOut());
                return;
            }
        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
