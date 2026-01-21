using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using RoR2.UI;
using RoR2.Audio;

namespace ShiggyMod.SkillStates
{
    public class RailgunnerCryoCharge : Skill
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.decayDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 1f;
        private float speedOverride = -1f;
        private CrosshairUtils.OverrideRequest crosshairOverrideRequest;
        public GameObject crosshairOverridePrefab = Modules.ShiggyAsset.railgunnercryoCrosshair;
        public LoopSoundDef loopSoundDef = Modules.ShiggyAsset.railgunnercryochargingSound;
        private LoopSoundManager.SoundLoopPtr loopPtr;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            this.animator.SetBool("attacking", true);
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            PlayAnimation("LeftArm, Override", "LArmAimStart", "Attack.playbackRate", duration);
            if (this.loopSoundDef)
            {
                this.loopPtr = LoopSoundManager.PlaySoundLoopLocal(base.gameObject, this.loopSoundDef);
            }
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
            LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
            base.OnExit();
        }


        public override void FixedUpdate()
        {

            if (base.inputBank.skill1.down && characterBody.skillLocator.primary.skillDef == Shiggy.railgunnercryoDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill2.down && characterBody.skillLocator.secondary.skillDef == Shiggy.railgunnercryoDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill3.down && characterBody.skillLocator.utility.skillDef == Shiggy.railgunnercryoDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill4.down && characterBody.skillLocator.special.skillDef == Shiggy.railgunnercryoDef)
            {

                keepFiring = true;
            }
            else if (extrainputBankTest.extraSkill1.down && extraskillLocator.extraFirst.skillDef == Shiggy.railgunnercryoDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill2.down && extraskillLocator.extraSecond.skillDef == Shiggy.railgunnercryoDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill3.down && extraskillLocator.extraThird.skillDef == Shiggy.railgunnercryoDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill4.down && extraskillLocator.extraFourth.skillDef == Shiggy.railgunnercryoDef)
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
                CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
                if (overrideRequest != null)
                {
                    overrideRequest.Dispose();
                }
                this.outer.SetNextState(new RailgunnerCryoFire());
                return;
            }
        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
