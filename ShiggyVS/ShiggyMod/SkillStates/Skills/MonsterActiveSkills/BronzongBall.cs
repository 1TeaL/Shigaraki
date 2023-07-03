using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using System.Globalization;
using EntityStates.Bell.BellWeapon;
using System.Collections.Generic;
using RoR2.Projectile;

namespace ShiggyMod.SkillStates
{
    public class BronzongBall : BaseSkillState
    {
        public float baseDuration = 1f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.bronzongballDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 500f;
        public  float selfForce = 500f;
        private float speedOverride = -1f;

        private float prepDuration;
        private float timeBetweenPreps;
        private float barrageDuration;
        private float timeBetweenBarrages;
        private float basePrepDuration = 1f;
        private float baseTimeBetweenPreps;
        private float baseBarrageDuration = 1f;
        private float baseTimeBetweenBarrages;
        public int totalBombs = Modules.StaticValues.maxballCount;

        private ChildLocator childLocator;
        private int currentBombIndex;
        private float perProjectileStopwatch;
        private List<GameObject> preppedBombInstances = new List<GameObject>();

        public override void OnEnter()
        {
            base.OnEnter();
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
            //base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }

            this.prepDuration = basePrepDuration / this.attackSpeedStat;
            this.timeBetweenPreps = (prepDuration/ totalBombs) -0.05f;
            this.barrageDuration = baseBarrageDuration / this.attackSpeedStat;
            this.timeBetweenBarrages = (barrageDuration/ totalBombs) -0.05f;
            Transform modelTransform = base.GetModelTransform();
            if (modelTransform)
            {
                this.childLocator = modelTransform.GetComponent<ChildLocator>();
            }

            EffectManager.SimpleMuzzleFlash(ChargeTrioBomb.muzzleflashPrefab, base.gameObject, this.FindTargetChildStringFromBombIndex(), false);


            Shiggycon = gameObject.GetComponent<ShiggyController>();
            

        }

        private string FindTargetChildStringFromBombIndex()
        {
            return string.Format(CultureInfo.InvariantCulture, "Bronzong{0}", this.currentBombIndex);
        }

        private Transform FindTargetChildTransformFromBombIndex()
        {
            string childName = this.FindTargetChildStringFromBombIndex();
            return this.childLocator.FindChild(childName);
        }

        public override void OnExit()
        {
            base.OnExit();
            foreach (GameObject obj in this.preppedBombInstances)
            {
                EntityState.Destroy(obj);
            }
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.perProjectileStopwatch += Time.fixedDeltaTime;
            if (base.fixedAge < this.prepDuration)
            {
                if (this.perProjectileStopwatch > this.timeBetweenPreps && currentBombIndex < totalBombs)
                {
                    this.currentBombIndex++;
                    this.perProjectileStopwatch = 0f;
                    Transform transform = this.FindTargetChildTransformFromBombIndex();
                    if (transform)
                    {
                        //GameObject item = UnityEngine.Object.Instantiate<GameObject>(ChargeTrioBomb.preppedBombPrefab, transform);
                        GameObject item = UnityEngine.Object.Instantiate<GameObject>(ChargeTrioBomb.bombProjectilePrefab, transform);
                        preppedBombInstances.Add(item);
                        return;
                    }
                }
            }
            else if (base.fixedAge < this.prepDuration + this.barrageDuration)
            {
                if (this.perProjectileStopwatch > this.timeBetweenBarrages && this.currentBombIndex > 0)
                {
                    this.perProjectileStopwatch = 0f;
                    Ray aimRay = base.GetAimRay();
                    Transform transform2 = this.FindTargetChildTransformFromBombIndex();
                    if (transform2)
                    {
                        if (base.isAuthority)
                        {
                            ProjectileManager.instance.FireProjectile(
                                ChargeTrioBomb.bombProjectilePrefab, //prefab
                                transform2.position, //position
                                Util.QuaternionSafeLookRotation(aimRay.direction), //rotation
                                base.gameObject, //owner
                                this.damageStat * damageCoefficient, //damage
                                force, //force
                                Util.CheckRoll(this.critStat, base.characterBody.master), //crit
                                DamageColorIndex.Default, //damage color
                                null, //target
                                speedOverride); //speed
                            Rigidbody component = base.GetComponent<Rigidbody>();
                            if (component)
                            {
                                component.AddForceAtPosition(-selfForce * transform2.forward, transform2.position);
                            }
                        }
                    }
                    this.currentBombIndex--;
                    EntityState.Destroy(this.preppedBombInstances[this.currentBombIndex]);
                    return;
                }
            }
            else if (base.isAuthority)
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
