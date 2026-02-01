using EntityStates;
using ExtraSkillSlots;
using RoR2.Audio;
using RoR2.UI;
using ShiggyMod.Modules.Survivors;
using UnityEngine;

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
            SetSkillDef(Shiggy.railgunnercryoDef);
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
            base.FixedUpdate();


            if (!IsHeldDown())
            {
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
        }



        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
