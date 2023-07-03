using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using R2API.Networking;

namespace ShiggyMod.SkillStates
{
    public class GreaterWispBuff : BaseSkillState
    {
        public float baseDuration = 0.1f;
        public float duration;
        public float fireTime;
        public ShiggyController Shiggycon;
        private DamageType damageType;
        public bool hasFired;

        public static GameObject effectPrefab;

        private float radius = 0.5f;
        private float damageCoefficient = Modules.StaticValues.greaterwispballDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 1000f;
        private float speedOverride = -1f;
        private GameObject chargeEffectLeft;
        private GameObject chargeEffectRight;
        private string LHand = "LHand";
        private string RHand = "RHand";

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            this.fireTime = duration / 3;
            hasFired = false;
            Ray aimRay = base.GetAimRay();
            //base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
            base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }

            this.duration = baseDuration / this.attackSpeedStat;

            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.greaterwispBuff.buffIndex, 1, Modules.StaticValues.greaterwispballbuffDuration);
                //characterBody.AddTimedBuffAuthority(Modules.Buffs.greaterwispBuff.buffIndex, Modules.StaticValues.greaterwispballbuffDuration);
            }

            Shiggycon = gameObject.GetComponent<ShiggyController>();
            
            EffectManager.SpawnEffect(Modules.Assets.chargegreaterwispBall, new EffectData
            {
                origin = FindModelChild(LHand).position,
                scale = radius,
                rotation = Quaternion.LookRotation(base.transform.position)

            }, false);

            EffectManager.SpawnEffect(Modules.Assets.chargegreaterwispBall, new EffectData
            {
                origin = FindModelChild(RHand).position,
                scale = radius,
                rotation = Quaternion.LookRotation(base.transform.position)

            }, false);
        }

        public override void OnExit()
        {
            base.OnExit();
            PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
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
