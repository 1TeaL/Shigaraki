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
        public float firetime;
        public bool hasFired;
        public float pushRange;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            firetime = duration / 2f;
            pushRange = Modules.StaticValues.vulturePushRange * attackSpeedStat;
            if(pushRange < Modules.StaticValues.vulturePushRange) 
            {
                pushRange = Modules.StaticValues.vulturePushRange;
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

            if(base.fixedAge > firetime && !hasFired)
            {
                hasFired= true;

                new PerformForceNetworkRequest(characterBody.masterObjectId, base.GetAimRay().origin - GetAimRay().direction, base.GetAimRay().direction, pushRange, characterBody.damage * Modules.StaticValues.vultureDamageCoefficient).Send(NetworkDestination.Clients);
            }

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
