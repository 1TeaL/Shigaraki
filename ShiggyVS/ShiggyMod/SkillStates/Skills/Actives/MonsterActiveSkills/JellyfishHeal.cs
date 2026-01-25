using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using EntityStates.JellyfishMonster;
using UnityEngine.Networking;
using R2API.Networking;
using ShiggyMod.Modules.Networking;
using ShiggyMod.Modules;
using R2API.Networking.Interfaces;

namespace ShiggyMod.SkillStates
{
    public class JellyfishRegenerate : Skill
    {
        private DamageType damageType;


        private float healthCostFraction = 0.5f;
        private float radius;
        private float procCoefficient = 2f;
        private float force = 1f;
        private uint soundID;
        private GameObject chargeEffect;
        public HurtBox Target;
        private Vector3 theSpot;

        public override void OnEnter()
        {
            base.OnEnter(); 
            radius = 30 * (attackSpeedStat/3);
            damageType = new DamageTypeCombo(DamageType.Stun1s, DamageTypeExtended.Generic, DamageSource.Secondary);
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);

            
            PlayCrossfade("LeftArm, Override", "LHandDetonate", "Attack.playbackRate", duration, 0.1f);
            Transform modelTransform = base.GetModelTransform();
            //AkSoundEngine.PostEvent("ShiggyExplosion", base.gameObject);
            this.soundID = Util.PlaySound(JellyNova.chargingSoundString, base.gameObject);
            
            

        }

        public override void OnExit()
        {
            base.OnExit();
            PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            AkSoundEngine.StopPlayingID(this.soundID);
            
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if(base.fixedAge >= fireTime && !hasFired)
            {
                hasFired = true;
                new HealNetworkRequest(characterBody.masterObjectId, characterBody.GetBuffCount(Buffs.JellyfishRegenerateStacksBuff)).Send(NetworkDestination.Clients);
                characterBody.ApplyBuff(Buffs.JellyfishRegenerateStacksBuff.buffIndex, 1);
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
