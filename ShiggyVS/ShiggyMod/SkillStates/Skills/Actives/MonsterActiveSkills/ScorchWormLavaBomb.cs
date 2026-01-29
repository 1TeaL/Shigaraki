using EntityStates.Scorchling;
using RoR2;
using RoR2.Projectile;
using ShiggyMod.Modules;
using System;
using System.Linq;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class ScorchWormLavaBomb : Skill
    {
        public override void OnEnter()
        {
            base.OnEnter();

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            int randomAnim = UnityEngine.Random.RandomRangeInt(0, 10);
            //base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
        }
        public void Spit()
        {
            ProjectileManager.instance.FireProjectileWithoutDamageType(ScorchlingLavaBomb.mortarProjectilePrefab, base.GetAimRay().origin, Util.QuaternionSafeLookRotation(base.GetAimRay().direction), base.gameObject, this.damageStat * StaticValues.scorchWormLavaBombDamageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, ScorchlingLavaBomb.projectileVelocity);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge > fireTime && !hasFired)
            {
                hasFired = true;
                Spit();
            }

            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }
    }
}