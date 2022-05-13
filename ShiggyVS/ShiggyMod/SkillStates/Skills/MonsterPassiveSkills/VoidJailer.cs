using ShiggyMod.Modules.Survivors;
using EntityStates;
using RoR2;

namespace ShiggyMod.SkillStates
{
    public class VoidJailer : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";

        private float duration = 1f;
        public ShiggyController Shiggycon;
        public ShiggyMasterController Shiggymastercon;
        public HurtBox Target;

        public override void OnEnter()
        {
            base.OnEnter();

            base.characterBody.AddBuff(Modules.Buffs.voidjailerBuff);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("RightArm, Override", "RightArmPull", "Attack.playbackRate", duration, 0.1f);
        }

        public override void OnExit()
        {
            base.OnExit();
            if (Shiggycon.hasQuirk = true)
            {
                AkSoundEngine.PostEvent(3192656820, base.gameObject);
            }
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
    }
}