using EntityStates;
using R2API.Networking;
using RoR2;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using UnityEngine;

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

            if (!hasTeleported && Target)
            {
                hasTeleported = true;
                base.characterMotor.velocity = Vector3.zero;
                base.characterMotor.Motor.SetPositionAndRotation(Target.healthComponent.body.transform.position, Target.healthComponent.body.transform.rotation, true);



                int stacks = Target.healthComponent.body.GetBuffCount(Buffs.solusPrimedDebuff);

                //return stock if has primed
                if (Target.healthComponent.body.HasBuff(Buffs.solusPrimedDebuff))
                {
                    if(skillLocator.primary.skillDef == Shiggy.solustransportertransportDef)
                    {
                        skillLocator.primary.AddOneStock();
                    }
                    if (skillLocator.secondary.skillDef == Shiggy.solustransportertransportDef)
                    {
                        skillLocator.secondary.AddOneStock();
                    }
                    if (skillLocator.utility.skillDef == Shiggy.solustransportertransportDef)
                    {
                        skillLocator.utility.AddOneStock();
                    }
                    if (skillLocator.special.skillDef == Shiggy.solustransportertransportDef)
                    {
                        skillLocator.special.AddOneStock();
                    }
                    if (extraskillLocator.extraFirst.skillDef == Shiggy.solustransportertransportDef)
                    {
                        extraskillLocator.extraFirst.AddOneStock();
                    }
                    if (extraskillLocator.extraSecond.skillDef == Shiggy.solustransportertransportDef)
                    {
                        extraskillLocator.extraSecond.AddOneStock();
                    }
                    if (extraskillLocator.extraThird.skillDef == Shiggy.solustransportertransportDef)
                    {
                        extraskillLocator.extraThird.AddOneStock();
                    }
                    if (extraskillLocator.extraFourth.skillDef == Shiggy.solustransportertransportDef)
                    {
                        extraskillLocator.extraFourth.AddOneStock();
                    }
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
