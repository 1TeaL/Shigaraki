using EntityStates;
using EntityStates.ParentMonster;
using R2API;
using R2API.Networking;
using RoR2;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class SolusTransporterTransport : Skill
    {

        public bool hasTeleported;

        private float radius = 10f;
        private float damageCoefficient = Modules.StaticValues.parentDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 1000f;

        private BlastAttack blastAttack;


        public override void OnEnter()
        {
            base.OnEnter();
            hasFired = false;
            hasTeleported = false;
            damageType = new DamageTypeCombo(DamageType.Stun1s, DamageTypeExtended.Generic, DamageSource.Secondary);

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            //base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            //base.PlayCrossfade("RightArm, Override", "RArmPunch", "Attack.playbackRate", duration, 0.05f);
            //if (base.isAuthority)
            //{
            //    if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            //}
            //PlayAnimation("FullBody, Override", "Slam", "Attack.playbackRate", fireTime * 2f);

            Shiggycon = gameObject.GetComponent<ShiggyController>();
            if (Shiggycon && base.isAuthority)
            {
                Target = Shiggycon.GetTrackingTarget();
            }
            if (!Target)
            {
                return;
            }

                        

        }


        public override void OnExit()
        {
            base.OnExit();
            //this.PlayAnimation("Fullbody, Override", "BufferEmpty");
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            if(!hasTeleported && Target)
            {
                hasTeleported = true;
                base.characterMotor.velocity = Vector3.zero;
                base.characterMotor.Motor.SetPositionAndRotation(Target.healthComponent.body.transform.position, Target.healthComponent.body.transform.rotation, true);



                int stacks = Target.healthComponent.body.GetBuffCount(Buffs.solusPrimedDebuff);

                //return stock if has primed
                if (Target.healthComponent.body.HasBuff(Buffs.solusPrimedDebuff))
                {
                    Shiggymastercon.GrantStockIfSkillPresent(characterBody, Shiggy.solustransportertransportDef, 1);
                }
                Target.healthComponent.body.ApplyBuff(Buffs.solusPrimedDebuff.buffIndex, stacks + 1);


            }

            if (base.fixedAge > this.fireTime && !hasFired && base.isAuthority)
            {
                hasFired = true;
                EffectManager.SpawnEffect(Modules.ShiggyAsset.solusTransporterBlastEffectPrefab, new EffectData
                {
                    origin = characterBody.corePosition,
                    scale = 1f,
                }, true);
            }

            if ((base.fixedAge >= this.duration && base.isAuthority))
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
