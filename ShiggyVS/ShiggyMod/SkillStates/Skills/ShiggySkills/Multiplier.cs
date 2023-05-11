using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using R2API.Networking;

namespace ShiggyMod.SkillStates
{
    public class Multiplier : BaseSkillState
    {
        public float baseDuration = 0.5f;
        public float duration;
        public ShiggyController Shiggycon;

        public override void OnEnter()
        {
            base.OnEnter();
            duration= baseDuration;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);

            if (NetworkServer.active)
            {
                if (!base.characterBody.HasBuff(Modules.Buffs.multiplierBuff.buffIndex))
                {
                    base.characterBody.ApplyBuff(Modules.Buffs.multiplierBuff.buffIndex, 1);

                }
                else if (base.characterBody.HasBuff(Modules.Buffs.multiplierBuff.buffIndex))
                {
                    base.characterBody.ApplyBuff(Modules.Buffs.multiplierBuff.buffIndex, 0);
                }
                //base.characterBody.ApplyBuff(Modules.Buffs.acridBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.impbossBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.commandoBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.captainBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.loaderBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.greaterwispBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.claydunestriderBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.alphashieldonBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.beetleBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.minimushrumBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.larvajumpBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.pestjumpBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.vagrantBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.magmawormBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.gupspikeBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.hermitcrabmortarBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.impbossBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.lesserwispBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.lunarexploderBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.roboballminiBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.stonetitanBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.verminsprintBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.voidbarnaclemortarBuff);
                //base.characterBody.ApplyBuff(Modules.Buffs.voidjailerBuff);
            }



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
