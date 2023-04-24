using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using RoR2.Skills;
using UnityEngine.Networking;
using R2API.Networking;
using ShiggyMod.Modules.Networking;
using R2API.Networking.Interfaces;

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
            if(pushRange < Modules.StaticValues.vulturePushRange) 
            {
                pushRange = Modules.StaticValues.vulturePushRange;
            } 

            AkSoundEngine.PostEvent("ShiggyAirCannon", base.gameObject);

            new PerformForceNetworkRequest(characterBody.masterObjectId, base.GetAimRay().origin - GetAimRay().direction, base.GetAimRay().direction, pushRange, pushRange, characterBody.damage * Modules.StaticValues.vultureDamageCoefficient, Modules.StaticValues.vulturePushAngle, true).Send(NetworkDestination.Clients);

        }

        public override void OnExit()
        {
            base.OnExit();
        }



        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge >= duration)
            {

                this.outer.SetNextStateToMain();
            }
        }
    




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
