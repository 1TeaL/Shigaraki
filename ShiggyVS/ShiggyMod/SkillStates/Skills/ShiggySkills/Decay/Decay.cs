using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using System;
using System.Collections.Generic;
using UnityEngine.Networking;
using ShiggyMod.SkillStates.BaseStates;
using R2API;

namespace ShiggyMod.SkillStates
{
    public class Decay : BaseMeleeAttack
    {
        public override void OnEnter()
        {

            AkSoundEngine.PostEvent(4108468048, base.gameObject);

            this.hitboxName = "AroundHitbox";

            this.damageCoefficient = Modules.StaticValues.decayattackDamageCoeffecient;
            this.procCoefficient = 1f;
            this.pushForce = 500f;
            this.damageType = DamageType.Generic;
            this.baseDuration = 1f;
            this.attackStartTime = 0.2f;
            this.attackEndTime = 0.6f;
            this.baseEarlyExitTime = 0.8f;
            this.hitStopDuration = 0.1f;
            this.attackRecoil = 0.5f;
            this.hitHopVelocity = 7f;

            this.swingSoundString = "ShiggyMelee";
            this.hitSoundString = "ShiggyMelee";
            this.muzzleString = "RHand";
            this.swingEffectPrefab = Modules.Assets.decayspreadEffect;
            this.hitEffectPrefab = Modules.Assets.decayspreadEffect;
            DamageAPI.AddModdedDamageType(this.attack, Modules.Damage.shiggyDecay);

            this.impactSound = Modules.Assets.decayHitSoundEffect.index;


            base.OnEnter();
        }

        //public override void OnExit()
        //{
        //    base.OnExit();
        //    //if (NetworkServer.active)
        //    //{
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.acridBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.impbossBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.commandoBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.captainBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.loaderBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.greaterwispBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.claydunestriderBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.alphashieldonBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.beetleBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.minimushrumBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.larvajumpBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.pestjumpBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.vagrantBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.magmawormBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.gupspikeBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.hermitcrabmortarBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.impbossBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.lesserwispBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.lunarexploderBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.roboballminiBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.stonetitanBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.verminsprintBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.voidbarnaclemortarBuff);
        //    //    base.characterBody.RemoveBuff(Modules.Buffs.voidjailerBuff);
        //    //}
        //}


        //protected override void PlayAttackAnimation()
        //{
        //    base.PlayAttackAnimation();
        //}

        //protected override void PlaySwingEffect()
        //{
        //    base.PlaySwingEffect();
        //}

        //protected override void OnHitEnemyAuthority()
        //{
        //    base.OnHitEnemyAuthority();

        //}

        protected override void SetNextState()
        {
            //int index = this.swingIndex;
            //if (index == 0) index = 1;
            //else index = 0;

            this.outer.SetNextState(new Decay
            {
            });
        }

        public override void OnExit()
        {
            base.OnExit();
        }

    }
}



