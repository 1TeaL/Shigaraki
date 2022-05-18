using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class Multiplier : BaseSkillState
    {
        public float baseDuration = 0.1f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;


        private float radius = 15f;
        private float damageCoefficient = 1f;
        private float procCoefficient = 1f;
        private float force = 1f;
        private float speedOverride =1f;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);

            if (NetworkServer.active)
            {
                base.characterBody.AddBuff(Modules.Buffs.multiplierBuff);
                base.characterBody.AddBuff(Modules.Buffs.acridBuff);
                base.characterBody.AddBuff(Modules.Buffs.impbossBuff);
                //base.characterBody.AddBuff(Modules.Buffs.alphashieldonBuff);
                //base.characterBody.AddBuff(Modules.Buffs.beetleBuff);
                //base.characterBody.AddBuff(Modules.Buffs.minimushrumBuff);
                //base.characterBody.AddBuff(Modules.Buffs.larvajumpBuff);
                //base.characterBody.AddBuff(Modules.Buffs.pestjumpBuff);
                //base.characterBody.AddBuff(Modules.Buffs.vagrantBuff);
                //base.characterBody.AddBuff(Modules.Buffs.magmawormBuff);
                //base.characterBody.AddBuff(Modules.Buffs.gupspikeBuff);
                //base.characterBody.AddBuff(Modules.Buffs.hermitcrabmortarBuff);
                //base.characterBody.AddBuff(Modules.Buffs.impbossBuff);
                //base.characterBody.AddBuff(Modules.Buffs.lesserwispBuff);
                //base.characterBody.AddBuff(Modules.Buffs.lunarexploderBuff);
                //base.characterBody.AddBuff(Modules.Buffs.roboballminiBuff);
                //base.characterBody.AddBuff(Modules.Buffs.stonetitanBuff);
                //base.characterBody.AddBuff(Modules.Buffs.verminsprintBuff);
                //base.characterBody.AddBuff(Modules.Buffs.voidbarnaclemortarBuff);
                //base.characterBody.AddBuff(Modules.Buffs.voidjailerBuff);
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
