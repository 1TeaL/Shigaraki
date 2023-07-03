using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using EntityStates;
using System.Collections.Generic;
using System.Linq;
using ShiggyMod.Modules.Survivors;
using EntityStates.ParentMonster;
using R2API;

namespace ShiggyMod.SkillStates
{
    public class ParentTeleport : BaseSkillState
    {

        public bool hasTeleported;
        public bool hasFired;
        public float baseDuration = 1f;
        public float duration;
        public float fireTime;
        public ShiggyController Shiggycon;
        private DamageType damageType;

        private float radius = 10f;
        private float damageCoefficient = Modules.StaticValues.parentDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 1000f;

        private BlastAttack blastAttack;

        public HurtBox Target;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / base.attackSpeedStat;
            this.fireTime = this.duration / 3f;
            hasFired = false;
            hasTeleported = false;
            damageType = DamageType.Stun1s;

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
            //base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
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



            blastAttack = new BlastAttack();
            blastAttack.radius = radius;
            blastAttack.procCoefficient = procCoefficient;
            blastAttack.attacker = base.gameObject;
            blastAttack.crit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master);
            blastAttack.baseDamage = base.damageStat * damageCoefficient;
            blastAttack.falloffModel = BlastAttack.FalloffModel.None;
            blastAttack.baseForce = force;
            blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
            blastAttack.damageType = damageType;
            blastAttack.attackerFiltering = AttackerFiltering.Default;


            Shiggycon = gameObject.GetComponent<ShiggyController>();
            

        }


        public override void OnExit()
        {
            base.OnExit();
            this.PlayAnimation("Fullbody, Override", "BufferEmpty");
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            if(!hasTeleported && Target)
            {
                hasTeleported = true;
                base.characterMotor.velocity = Vector3.zero;
                base.characterMotor.Motor.SetPositionAndRotation(Target.healthComponent.body.transform.position, Target.healthComponent.body.transform.rotation, true);
            }

            Vector3 latestposition = base.transform.position;
            if (base.fixedAge > this.fireTime && !hasFired && base.isAuthority)
            {
                AkSoundEngine.PostEvent("ShiggyMelee", base.gameObject);
                hasFired = true;
                blastAttack.position = latestposition;
                EffectManager.SpawnEffect(Modules.Assets.parentslamEffect, new EffectData
                {
                    origin = latestposition,
                    scale = radius,
                }, true);


                blastAttack.AddModdedDamageType(Modules.Damage.shiggyDecay);
                if (blastAttack.Fire().hitCount > 0)
                {
                    this.OnHitEnemyAuthority();
                }

            }

            if ((base.fixedAge >= this.duration && base.isAuthority))
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }
        protected virtual void OnHitEnemyAuthority()
        {

        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}
