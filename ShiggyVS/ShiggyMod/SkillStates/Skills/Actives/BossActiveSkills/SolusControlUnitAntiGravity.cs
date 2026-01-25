using EntityStates;
using EntityStates.RoboBallBoss.Weapon;
using RoR2;
using RoR2.Projectile;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class SolusControlUnitAntiGravity : Skill
    {
        private DamageType damageType;


        private float radius = 50f;
        private float damageCoefficient = Modules.StaticValues.soluscontrolunitDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 1f;
        private float speedOverride = -1f;
        private int knockupCount = 1;
        private Vector2 randomPositionRadius;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            base.PlayCrossfade("LeftArm, Override", "LHandLift", "Attack.playbackRate", duration, 0.05f);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }

            EffectManager.SimpleMuzzleFlash(FireDelayKnockup.muzzleEffectPrefab, base.gameObject, "Chest", false);
			
			if (NetworkServer.active)
			{
				BullseyeSearch bullseyeSearch = new BullseyeSearch();
				bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
				if (base.teamComponent)
				{
					bullseyeSearch.teamMaskFilter.RemoveTeam(base.teamComponent.teamIndex);
				}
				bullseyeSearch.maxDistanceFilter = radius;
				bullseyeSearch.maxAngleFilter = 360f;
				bullseyeSearch.searchOrigin = aimRay.origin;
				bullseyeSearch.searchDirection = aimRay.direction;
				bullseyeSearch.filterByLoS = false;
				bullseyeSearch.sortMode = BullseyeSearch.SortMode.Angle;
				bullseyeSearch.RefreshCandidates();
				List<HurtBox> list = bullseyeSearch.GetResults().ToList<HurtBox>();
				int num = 0;
				for (int i = 0; i < this.knockupCount; i++)
				{
					if (num >= list.Count)
					{
						num = 0;
					}
					HurtBox hurtBox = list[num];
					if (hurtBox)
					{
						Vector2 vector = UnityEngine.Random.insideUnitCircle * this.randomPositionRadius;
						Vector3 vector2 = hurtBox.transform.position + new Vector3(vector.x, 0f, vector.y);
						RaycastHit raycastHit;
						if (Physics.Raycast(new Ray(vector2 + Vector3.up * 1f, Vector3.down), out raycastHit, 200f, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
						{
							vector2 = raycastHit.point;
						}
						//ProjectileManager.instance.FireProjectile(FireDelayKnockup.projectilePrefab, vector2, Quaternion.identity, base.gameObject, this.damageStat, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);

                        ProjectileManager.instance.FireProjectile(
                            FireDelayKnockup.projectilePrefab, //prefab
                            vector2, //position
                            Quaternion.identity, //rotation
                            base.gameObject, //owner
                            this.damageStat * damageCoefficient, //damage
                            force, //force
                            Util.CheckRoll(this.critStat, base.characterBody.master), //crit
                            DamageColorIndex.Default, //damage color
                            null, //target
                            speedOverride); //speed }}   

                    }
					num++;
				}
			}

            

        }

        public override void OnExit()
        {
            base.OnExit();
            PlayCrossfade("RightArm, Override", "Empty", "Attack.playbackRate", 0.1f, 0.1f);
            PlayCrossfade("LeftArm, Override", "Empty", "Attack.playbackRate", 0.1f, 0.1f);
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
