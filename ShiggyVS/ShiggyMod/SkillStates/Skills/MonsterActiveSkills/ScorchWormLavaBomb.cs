using EntityStates;
using EntityStates.Scorchling;
using ExtraSkillSlots;
using R2API.Networking;
using RoR2;
using RoR2.ExpansionManagement;
using RoR2.Projectile;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class LavaBomb : Skill
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
            Transform transform = base.characterBody.modelLocator.modelTransform.GetComponent<ChildLocator>().FindChild("RHand");
            Ray ray = new Ray(transform.position, transform.forward);
            Ray ray2 = new Ray(ray.origin, Vector3.up);
            BullseyeSearch bullseyeSearch = new BullseyeSearch();
            bullseyeSearch.searchOrigin = ray.origin;
            bullseyeSearch.searchDirection = ray.direction;
            bullseyeSearch.filterByLoS = false;
            bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
            if (base.teamComponent)
            {
                bullseyeSearch.teamMaskFilter.RemoveTeam(base.teamComponent.teamIndex);
            }
            bullseyeSearch.sortMode = BullseyeSearch.SortMode.Angle;
            bullseyeSearch.RefreshCandidates();
            HurtBox hurtBox = bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
            bool flag = false;
            Vector3 a = Vector3.zero;
            RaycastHit raycastHit;
            if (hurtBox)
            {
                a = hurtBox.transform.position;
                flag = true;
            }
            else if (Physics.Raycast(ray, out raycastHit, 1000f, LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.Ignore))
            {
                a = raycastHit.point;
                flag = true;
            }
            float magnitude = ScorchlingLavaBomb.projectileVelocity;
            if (flag)
            {
                Vector3 vector = a - ray2.origin;
                Vector2 a2 = new Vector2(vector.x, vector.z);
                float magnitude2 = a2.magnitude;
                Vector2 vector2 = a2 / magnitude2;
                if (magnitude2 < ScorchlingLavaBomb.minimumDistance)
                {
                    magnitude2 = ScorchlingLavaBomb.minimumDistance;
                }
                float y = Trajectory.CalculateInitialYSpeed(ScorchlingLavaBomb.timeToTarget, vector.y);
                float num = magnitude2 / ScorchlingLavaBomb.timeToTarget;
                Vector3 direction = new Vector3(vector2.x * num, y, vector2.y * num);
                magnitude = direction.magnitude;
                ray2.direction = direction;
            }
            Quaternion rotation = Util.QuaternionSafeLookRotation(ray2.direction);
            ProjectileManager.instance.FireProjectileWithoutDamageType(ScorchlingLavaBomb.mortarProjectilePrefab, ray2.origin, rotation, base.gameObject, this.damageStat * ScorchlingLavaBomb.mortarDamageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, magnitude);
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