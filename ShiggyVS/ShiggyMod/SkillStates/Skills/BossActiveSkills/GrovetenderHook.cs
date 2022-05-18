using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using EntityStates.GravekeeperBoss;
using RoR2.Projectile;
using System.Collections.Generic;
using System.Linq;

namespace ShiggyMod.SkillStates
{
    public class GrovetenderHook : BaseSkillState
    {
        public float fireAge;
        public float fireTimer;
        public float baseDuration = 1f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.grovetenderDamageCoeffecient;
        private float procCoefficient = 1f;
        private float force = -1000f;
        private float speedOverride = -1f;
        private Animator modelAnimator;
        public string muzzleString = "LHand";
        private float projectileCount;
        private float spread = 5f;
        private float maxWeight;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            projectileCount = 5 * (uint)Shiggycon.projectileCount;
            fireTimer = (duration / projectileCount);

            GetMaxWeight();
            this.modelAnimator = base.GetModelAnimator();
            ChildLocator component = this.modelAnimator.GetComponent<ChildLocator>();
            if (component)
            {
                component.FindChild(muzzleString);
            }
            Util.PlayAttackSpeedSound(FireHook.soundString, base.gameObject, this.attackSpeedStat);
            EffectManager.SimpleMuzzleFlash(FireHook.muzzleflashEffectPrefab, base.gameObject, muzzleString, false);

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("LeftArm, Override", "LeftArmPunch", "Attack.playbackRate", duration, 0.1f);
            AkSoundEngine.PostEvent(3660048432, base.gameObject);

            this.FireSingleHook(aimRay, 0f, 0f);

            Shiggycon = gameObject.GetComponent<ShiggyController>();
            damageCoefficient *= Shiggycon.rangedMultiplier;
        }
        private void FireSingleHook(Ray aimRay, float bonusPitch, float bonusYaw)
        {
            Vector3 forward = Util.ApplySpread(aimRay.direction, 0f, 0f, 1f, 1f, bonusYaw, bonusPitch);

            ProjectileManager.instance.FireProjectile(
                FireHook.projectilePrefab, //prefab
                aimRay.origin, //position
                Util.QuaternionSafeLookRotation(forward), //rotation
                base.gameObject, //owner
                this.damageStat * damageCoefficient, //damage
                -maxWeight*16f, //force
                Util.CheckRoll(this.critStat, base.characterBody.master), //crit
                DamageColorIndex.Default, //damage color
                null, //target
                speedOverride); //speed
        }
        public override void OnExit()
        {
            base.OnExit();
            PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            //PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
        }


        public void GetMaxWeight()
        {
            Ray aimRay = base.GetAimRay();
            BullseyeSearch search = new BullseyeSearch
            {

                teamMaskFilter = TeamMask.GetEnemyTeams(base.GetTeam()),
                filterByLoS = false,
                searchOrigin = aimRay.origin + 20 * aimRay.direction,
                searchDirection = UnityEngine.Random.onUnitSphere,
                sortMode = BullseyeSearch.SortMode.Distance,
                maxDistanceFilter = 100f,
                maxAngleFilter = 360f
            };

            search.RefreshCandidates();
            search.FilterOutGameObject(base.gameObject);



            List<HurtBox> target = search.GetResults().ToList<HurtBox>();
            foreach (HurtBox singularTarget in target)
            {
                if (singularTarget)
                {
                    if (singularTarget.healthComponent && singularTarget.healthComponent.body)
                    {
                        if (singularTarget.healthComponent.body.characterMotor)
                        {
                            if (singularTarget.healthComponent.body.characterMotor.mass > maxWeight)
                            {
                                maxWeight = singularTarget.healthComponent.body.characterMotor.mass;
                            }
                        }
                        else if (singularTarget.healthComponent.body.rigidbody)
                        {
                            if (singularTarget.healthComponent.body.rigidbody.mass > maxWeight)
                            {
                                maxWeight = singularTarget.healthComponent.body.rigidbody.mass;
                            }
                        }
                    }
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if(fireAge >= fireTimer)
            {
                if (NetworkServer.active)
                {
                    fireAge = 0f;
                    Ray aimRay = base.GetAimRay();
                    //for (int i = 0; i < projectileCount; i++)
                    //{
                    float bonusPitch = UnityEngine.Random.Range(-spread, spread) / 2f;
                    float bonusYaw = UnityEngine.Random.Range(-spread, spread) / 2f;
                    this.FireSingleHook(aimRay, bonusPitch, bonusYaw);
                    //}
                }

            }
            else
            {
                fireAge += Time.fixedDeltaTime;
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
