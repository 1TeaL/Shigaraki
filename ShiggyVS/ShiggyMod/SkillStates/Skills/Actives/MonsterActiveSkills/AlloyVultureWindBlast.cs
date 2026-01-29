using EntityStates;
using R2API.Networking;
using R2API.Networking.Interfaces;
using ShiggyMod.Modules.Networking;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class AlloyVultureWindBlast : Skill
    {
        public float pushRange;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            pushRange = Modules.StaticValues.vulturePushRange * attackSpeedStat;
            if (pushRange < Modules.StaticValues.vulturePushRange)
            {
                pushRange = Modules.StaticValues.vulturePushRange;
            }

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            //base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            base.PlayCrossfade("RightArm, Override", "RArmBlast", "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
            AkSoundEngine.PostEvent("ShiggyAirCannon", base.gameObject);


        }

        public override void OnExit()
        {
            base.OnExit();
        }



        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge > fireTime && !hasFired)
            {
                hasFired = true;
                new PerformForceNetworkRequest(characterBody.masterObjectId, base.GetAimRay().origin - GetAimRay().direction, base.GetAimRay().direction, pushRange, pushRange, characterBody.damage * Modules.StaticValues.vultureDamageCoefficient, Modules.StaticValues.vulturePushAngle, true).Send(NetworkDestination.Clients);
            }

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
