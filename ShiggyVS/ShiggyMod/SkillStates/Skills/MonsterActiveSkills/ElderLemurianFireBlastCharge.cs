using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using System.Linq;
using EntityStates.Huntress;
using EntityStates.LemurianBruiserMonster;

namespace ShiggyMod.SkillStates
{
    public class ElderLemurianFireBlastCharge : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float baseDuration = 1f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;
        public HurtBox Target;
        private Animator animator;


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.elderlemurianfireblastDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 1f;
        private float speedOverride = -1f;

        private GameObject areaIndicator;
        private float maxCharge;
        private int baseMaxCharge = 3;
        private float maxDistance;
        private float chargePercent;
        private float baseDistance = 2f;
        private RaycastHit raycastHit;
        private float hitDis;
        private float damageMult;
        private int hitCount;
        private float baseRadius = 2f;
        private Vector3 maxMoveVec;
        private Vector3 randRelPos;
        private int randFreq;
        private bool reducerFlipFlop;
        private GameObject effectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/LightningStakeNova");
        private GameObject chargeInstance;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            //PlayAnimation("RightArm, Override", "RightArmOut", "Attack.playbackRate", duration);
            PlayAnimation("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", duration);
            damageType = DamageType.Generic;
            float[] source = new float[]
            {
                this.attackSpeedStat,
                4f
            };
            this.maxCharge = (float)this.baseMaxCharge / source.Min();
            this.areaIndicator = Object.Instantiate<GameObject>(ArrowRain.areaIndicatorPrefab);
            this.areaIndicator.SetActive(true);

            Transform modelTransform = base.GetModelTransform();
            Util.PlayAttackSpeedSound(ChargeMegaFireball.attackString, base.gameObject, this.attackSpeedStat);
            if (modelTransform)
            {
                ChildLocator component = modelTransform.GetComponent<ChildLocator>();
                if (component)
                {
                    Transform transform = base.FindModelChild("LHand");
                    if (transform && ChargeMegaFireball.chargeEffectPrefab)
                    {
                        this.chargeInstance = UnityEngine.Object.Instantiate<GameObject>(ChargeMegaFireball.chargeEffectPrefab, transform.position + characterDirection.forward + Vector3.up, transform.rotation);
                        this.chargeInstance.transform.parent = transform;
                        ScaleParticleSystemDuration component2 = this.chargeInstance.GetComponent<ScaleParticleSystemDuration>();
                        if (component2)
                        {
                            component2.newDuration = 60f;
                        }
                    }
                }
            }

        }
        public void IndicatorUpdator()
        {
            Ray aimRay = base.GetAimRay();
            Vector3 direction = aimRay.direction;
            aimRay.origin = base.characterBody.corePosition;
            this.maxDistance = baseDistance;
            Physics.Raycast(aimRay.origin, aimRay.direction, out this.raycastHit, this.maxDistance);
            this.hitDis = this.raycastHit.distance;
            bool flag = this.hitDis < this.maxDistance && this.hitDis > 0f;
            if (flag)
            {
                this.maxDistance = this.hitDis;
            }
            this.damageMult = damageCoefficient + 4 * this.chargePercent * damageCoefficient;
            this.radius = (this.baseRadius * this.damageMult + 10f) / 4f;
            this.maxMoveVec = this.maxDistance * direction;
            this.areaIndicator.transform.localScale = Vector3.one * this.radius;
            this.areaIndicator.transform.localPosition = aimRay.origin + this.maxMoveVec;
        }
        public override void OnExit()
        {
            base.OnExit();
            base.characterMotor.walkSpeedPenaltyCoefficient = 1f;
            bool flag = this.areaIndicator;
            if (flag)
            {
                this.areaIndicator.SetActive(false);
                EntityState.Destroy(this.areaIndicator);
            }

            if (this.chargeInstance)
            {
                EntityState.Destroy(this.chargeInstance);
            }
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();
            //bool flag = base.fixedAge < this.maxCharge && base.IsKeyDownAuthority();
            bool flag = base.IsKeyDownAuthority();
            if (flag)
            {
                PlayAnimation("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", duration);
                this.chargePercent = base.fixedAge / this.maxCharge;
                this.IndicatorUpdator();
            }


            else
            {
                bool isAuthority = base.isAuthority;
                if (isAuthority)
                {
                    int hitcount = (int)(damageMult/ damageCoefficient);
                    ElderLemurianFireBlastFire fireblast = new ElderLemurianFireBlastFire();
                    fireblast.damageMult = this.damageMult;
                    fireblast.radius = this.radius;
                    fireblast.chargePercent = this.chargePercent;
                    fireblast.hitCount = hitcount;
                    fireblast.moveVec = this.areaIndicator.transform.localPosition;
                    this.outer.SetNextState(fireblast);
                }
            }
        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
