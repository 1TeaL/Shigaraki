using EntityStates;
using EntityStates.Huntress;
using EntityStates.Loader;
using EntityStates.VagrantMonster;
using RoR2;
using RoR2.Audio;
using ShiggyMod.Modules;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace ShiggyMod.SkillStates
{
    public class MachPunch : BaseSkillState
    {
        //parent + loader
        public float smashage;
        public float duration = 1f;
        private string muzzleString = "RHand";

        protected Animator animator;
        private GameObject areaIndicator;
        private float maxCharge;
        private int baseMaxCharge = StaticValues.machPunchBaseMaxCharge;
        private float maxDistance;
        private float chargePercent;
        private float baseDistance = StaticValues.machPunchBaseDistance;
        private RaycastHit raycastHit;
        private float hitDis;
        private float damageMult;
        private float radius;
        private float baseRadius = StaticValues.machPunchRadius;
        private Vector3 maxMoveVec;
        private Vector3 randRelPos;
        private int randFreq;
        private bool reducerFlipFlop;
        private GameObject effectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/LightningStakeNova");

        public static float healthCostFraction;
        private GameObject chargeVfxInstance;

        public override void OnEnter()
        {
            base.OnEnter();            
            float[] source = new float[]
            {
                this.attackSpeedStat,
                4f
            };
            this.maxCharge = (float)this.baseMaxCharge / source.Min();
            this.areaIndicator = Object.Instantiate<GameObject>(ArrowRain.areaIndicatorPrefab);
            this.areaIndicator.SetActive(true);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);


            Ray aimRay = base.GetAimRay();
            if (transform && BaseChargeFist.chargeVfxPrefab)
            {
                this.chargeVfxInstance = UnityEngine.Object.Instantiate<GameObject>(BaseChargeFist.chargeVfxPrefab, FindModelChild(this.muzzleString).position, Util.QuaternionSafeLookRotation(aimRay.direction));
                this.chargeVfxInstance.transform.parent = FindModelChild(this.muzzleString).transform;
            }

            //animation
            //base.PlayAnimation("RightArm, Override", "SmashCharge", "Attack.playbackRate", 1f);

            Util.PlaySound(BaseChargeFist.startChargeLoopSFXString, base.gameObject);

        }

        public void IndicatorUpdator()
        {
            Ray aimRay = base.GetAimRay();
            Vector3 direction = aimRay.direction;
            aimRay.origin = base.characterBody.corePosition;
            this.maxDistance = (1f + 4f * this.chargePercent) * this.baseDistance * (this.moveSpeedStat / 7f);
            Physics.Raycast(aimRay.origin, aimRay.direction, out this.raycastHit, this.maxDistance);
            this.hitDis = this.raycastHit.distance;
            bool flag = this.hitDis < this.maxDistance && this.hitDis > 0f;
            if (flag)
            {
                this.maxDistance = this.hitDis;
            }
            this.damageMult = Modules.StaticValues.machPunchDamageCoefficient + 2f * (this.chargePercent * Modules.StaticValues.machPunchDamageCoefficient);
            this.radius = (this.baseRadius * this.damageMult + 10f) / 4f;
            this.maxMoveVec = this.maxDistance * direction;
            this.areaIndicator.transform.localScale = Vector3.one * this.radius;
            this.areaIndicator.transform.localPosition = aimRay.origin + this.maxMoveVec;
        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
        public override void OnExit()
        {

            base.characterMotor.walkSpeedPenaltyCoefficient = 1f;
            bool flag = this.areaIndicator;
            if (flag)
            {
                this.areaIndicator.SetActive(false);
                EntityState.Destroy(this.areaIndicator);
            }
            if (this.chargeVfxInstance)
            {
                EntityState.Destroy(this.chargeVfxInstance);
            }
            Util.PlaySound(BaseChargeFist.endChargeLoopSFXString, base.gameObject);
            base.OnExit();
        }
        
        public override void FixedUpdate()
        {


            base.FixedUpdate();
            //bool flag = base.fixedAge < this.maxCharge && base.IsKeyDownAuthority();
            if (base.fixedAge < this.maxCharge && base.IsKeyDownAuthority())
            {

                //base.PlayAnimation("FullBody, Override", "SmashFullCharge", "Attack.playbackRate", 1f);
                this.chargePercent = base.fixedAge / this.maxCharge;
                this.randRelPos = new Vector3((float)Random.Range(-12, 12) / 4f, (float)Random.Range(-12, 12) / 4f, (float)Random.Range(-12, 12) / 4f);
                this.randFreq = Random.Range(baseMaxCharge * 50, this.baseMaxCharge * 100) / 100;
                bool flag2 = this.reducerFlipFlop;
                if (flag2)
                {
                    bool flag3 = (float)this.randFreq <= this.chargePercent;
                    if (flag3)
                    {
                        EffectData effectData = new EffectData
                        {
                            scale = 1f,
                            origin = base.characterBody.corePosition + this.randRelPos
                        };
                        EffectManager.SpawnEffect(this.effectPrefab, effectData, true);
                    }
                    this.reducerFlipFlop = false;
                }
                else
                {
                    this.reducerFlipFlop = true;
                }
                //base.characterMotor.walkSpeedPenaltyCoefficient = 1f - this.chargePercent / 3f;
                this.IndicatorUpdator();
            }            
            else
            {
                bool isAuthority = base.isAuthority;
                if (isAuthority)
                {
                    MachPunchRelease machPunchRelease = new MachPunchRelease();
                    machPunchRelease.damageMult = this.damageMult;
                    machPunchRelease.radius = this.radius;
                    machPunchRelease.moveVec = this.maxMoveVec;
                    this.outer.SetNextState(machPunchRelease);
                }
            }
        }
    }
}
