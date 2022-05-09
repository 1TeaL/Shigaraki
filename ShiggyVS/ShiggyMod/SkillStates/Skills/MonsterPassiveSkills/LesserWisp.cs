using ShiggyMod.Modules.Survivors;
using EntityStates;
using RoR2;

namespace ShiggyMod.SkillStates
{
    public class LesserWisp : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";

        private float duration = 1f;
        public ShiggyController Shiggycon;
        public ShiggyMasterController Shiggymastercon;
        public HurtBox Target;


        public override void OnEnter()
        {
            base.OnEnter();

            PlayCrossfade("RightArm, Override", "RightArmPull", 0.1f);
            AkSoundEngine.PostEvent(1201419547, base.gameObject);

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
    }
}