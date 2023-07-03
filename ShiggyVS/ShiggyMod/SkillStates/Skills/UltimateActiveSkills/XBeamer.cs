using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using EntityStates.Mage.Weapon;
using ExtraSkillSlots;
using static RoR2.BulletAttack;
using HG;
using ShiggyMod.Modules;
using RoR2.Audio;
using System;
using Object = UnityEngine.Object;
using EntityStates.VoidRaidCrab;

namespace ShiggyMod.SkillStates
{
    public class XBeamer : BaseSkillState
    {
        //rapid pierce + sweeping beam

        public EnergySystem energySystem;
        public ShiggyController Shiggycon;
        private DamageType damageType;
        public HurtBox Target;
        public GameObject flamethrowerEffectPrefab = Modules.Assets.artificerfireEffect;

        private float force = 1f;
        private float procCoefficientPerTick = Modules.StaticValues.xBeamerProcCoefficient;
        public float beamDuration = StaticValues.xBeamerDuration;
        public float damageCoefficient = Modules.StaticValues.xBeamerDamageCoefficient;
        public float tickFrequency = StaticValues.xBeamerTickFrequency;
        public float ignitePercentChance;
        public float recoilForce = 5f;
        private float tickDamageCoefficient;
        public float baseEntryDuration = 1f;
        private float entryDuration;
        private float damageMult;
        private float radius;
        private float chargePercent;
        private float baseRadius = StaticValues.xBeamerRadius;

        private ChildLocator childLocator;
        private string muzzleStringR = "RHand";
        private string muzzleStringL = "LHand";
        public LoopSoundDef loopSoundDef = Modules.Assets.xiconstructsound;
        private LoopSoundManager.SoundLoopPtr loopPtr;
        private GameObject RchargeVfxInstance;
        private GameObject LchargeVfxInstance;
        private GameObject areaIndicator;
        private GameObject laserEffect;
        public Vector3 startPosition;
        public Vector3 endPosition;
        private Transform laserEffectEndTransform;

        public enum BeamState
        {
            CHARGE,
            ACTIVE,

        }
        private BeamState state;
        private float stopwatch;
        private float activeStopwatch;
        private bool playWindupAnimation;

        public override void OnEnter()
        {
            base.OnEnter();
            energySystem = gameObject.GetComponent<EnergySystem>();
            float plusChaosflatCost = (StaticValues.xBeamerBaseEnergyCost) - (energySystem.costflatplusChaos);
            if (plusChaosflatCost < 0f) plusChaosflatCost = 0f;

            float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
            if (plusChaosCost < 0f) plusChaosCost = 0f;
            if (energySystem.currentplusChaos < plusChaosCost)
            {
                energySystem.TriggerGlow(0.3f, 0.3f, Color.black);
                this.outer.SetNextStateToMain();
                return;
            }
            else
            {
                energySystem.SpendplusChaos(plusChaosCost);
            }


            state = BeamState.CHARGE;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.entryDuration);
            entryDuration = baseEntryDuration / attackSpeedStat;

            //animation with right arm in front like tsuna from hitman reborn doing the blast
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            base.PlayAnimation("FullBody, Override", "FullBodyXBurner", "Attack.playbackRate", entryDuration);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            //find shooting direction from the beginning, animation to link up too
            startPosition = FindModelChild(this.muzzleStringR).position + characterDirection.forward;
            endPosition = FindModelChild(this.muzzleStringR).position + aimRay.direction * StaticValues.xBeamerDistance;

            Transform modelTransform = base.GetModelTransform();
            if (modelTransform)
            {
                this.childLocator = modelTransform.GetComponent<ChildLocator>();
            }
            //stop character from moving while charging
            characterBody.characterMotor.velocity = Vector3.zero;
            characterBody.characterMotor.disableAirControlUntilCollision = true;
            characterBody.characterMotor.useGravity = false;
            if (base.characterMotor)
            {
                base.characterMotor.enabled = false;
            }


            //effects
            if (transform && EntityStates.VoidJailer.Capture.chargeVfxPrefab)
            {
                this.RchargeVfxInstance = UnityEngine.Object.Instantiate<GameObject>(EntityStates.VoidJailer.Capture.chargeVfxPrefab, FindModelChild(this.muzzleStringR).position, Util.QuaternionSafeLookRotation(aimRay.direction));
                this.RchargeVfxInstance.transform.parent = FindModelChild(this.muzzleStringR).transform;
            }
            if (transform && EntityStates.VoidJailer.Capture.chargeVfxPrefab)
            {
                this.LchargeVfxInstance = UnityEngine.Object.Instantiate<GameObject>(EntityStates.VoidJailer.Capture.chargeVfxPrefab, FindModelChild(this.muzzleStringL).position, Util.QuaternionSafeLookRotation(aimRay.direction));
                this.LchargeVfxInstance.transform.parent = FindModelChild(this.muzzleStringL).transform;
            }
            //if(Assets.xBeamerIndicator)
            //{
            //    this.areaIndicator = Object.Instantiate<GameObject>(Assets.xBeamerIndicator);
            //    this.areaIndicator.SetActive(true);
            //    this.areaIndicator.transform.localScale = Vector3.one * this.radius;
            //    this.areaIndicator.transform.localPosition = childLocator.FindChild(this.muzzleStringR).position;
            //}
            if (this.loopSoundDef)
            {
                this.loopPtr = LoopSoundManager.PlaySoundLoopLocal(base.gameObject, this.loopSoundDef);
            }
        }

        public void Update()
        {
            //Handle transform of effectObj
            if (laserEffect)
            {
                laserEffectEndTransform.position = endPosition;
            }
            //IndicatorUpdator();
        }

        public void IndicatorUpdator()
        {
            if (areaIndicator)
            {
                this.areaIndicator.transform.localScale = Vector3.one * this.radius;
                this.areaIndicator.transform.localPosition = childLocator.FindChild(this.muzzleStringR).position;
            }
        }

        public void Charge()
        {

            this.chargePercent = base.fixedAge / StaticValues.xBeamerChargeCoefficient;
            this.damageMult = Modules.StaticValues.xBeamerDamageCoefficient + StaticValues.xBeamerChargeCoefficient * (this.chargePercent * Modules.StaticValues.xBeamerDamageCoefficient);
            this.radius = (this.baseRadius * this.damageMult + 10f) / 4f;
            //this.areaIndicator.transform.localPosition = FindModelChild(this.muzzleStringR).position;
            tickDamageCoefficient = damageMult / beamDuration;
        }

        public override void OnExit()
        {
            base.OnExit();
            characterBody.characterMotor.disableAirControlUntilCollision = false;
            characterBody.characterMotor.useGravity = true;
            if (base.characterMotor)
            {
                base.characterMotor.enabled = true;
            }

            if (this.RchargeVfxInstance)
            {
                EntityState.Destroy(this.RchargeVfxInstance);
            }
            if (this.LchargeVfxInstance)
            {
                EntityState.Destroy(this.LchargeVfxInstance);
            }
            if (laserEffect)
            {
                Destroy(laserEffect);
            }
            LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
            base.PlayCrossfade("FullBody, Override", "BufferEmpty", "Attack.playbackRate", 0.05f, 0.05f);

        }
        private void FireBeam()
        {
            EffectManager.SimpleMuzzleFlash(Modules.Assets.xiconstructbeamEffect, base.gameObject, muzzleStringR, false);
            if (base.isAuthority)
            {
                Ray aimRay = base.GetAimRay();


                base.characterBody.AddSpreadBloom(1f);
                base.AddRecoil(-1f, -2f, -0.5f, 0.5f);
                var bulletAttack = new BulletAttack
                {
                    owner = base.gameObject,
                    weapon = base.gameObject,
                    origin = aimRay.origin,
                    aimVector = endPosition-startPosition,
                    minSpread = 0f,
                    damage = this.tickDamageCoefficient * this.damageStat,
                    force = force,
                    muzzleName = muzzleStringR,
                    hitEffectPrefab = EntityStates.Commando.CommandoWeapon.FirePistol2.hitEffectPrefab,// find good one SpinBeamAttack.beamImpactEffectPrefab too strong
                    isCrit = characterBody.RollCrit(),
                    radius = 4f,
                    falloffModel = BulletAttack.FalloffModel.None,
                    stopperMask = LayerIndex.world.mask,
                    procCoefficient = procCoefficientPerTick,
                    maxDistance = StaticValues.xBeamerDistance,
                    smartCollision = true,
                    tracerEffectPrefab = Modules.Assets.VoidFiendBeamTracer,
                    damageType = DamageType.Stun1s,
                };
                bulletAttack.Fire();
                if (base.characterMotor)
                {
                    base.characterMotor.ApplyForce(aimRay.direction * -recoilForce, false, true);
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            base.PlayCrossfade("FullBody, Override", "FullBodyXBurner", "Attack.playbackRate", entryDuration, 0.05f);
            switch (state)
            {
                case BeamState.CHARGE:


                    this.chargePercent = base.fixedAge / StaticValues.xBeamerChargeCoefficient;
                    if (base.IsKeyDownAuthority())
                    {
                        Charge();
                    }
                    if (!base.IsKeyDownAuthority())
                    {
                        state = BeamState.ACTIVE;
                    }
                    //energy cost
                    float plusChaosflatCost = (StaticValues.xBeamerEnergyCost) - (energySystem.costflatplusChaos * StaticValues.costFlatContantlyDrainingCoefficient);
                    if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;

                    float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                    if (plusChaosCost < 0f) plusChaosCost = 0f;
                    energySystem.SpendplusChaos(plusChaosCost);
                    if (energySystem.currentplusChaos < 1f)
                    {
                        state = BeamState.ACTIVE;
                    }

                    break;
                //case BeamState.BEGIN:
                //    //play animation here, once done go to next state
                //    if (!playWindupAnimation)
                //    {
                //        playWindupAnimation = true;
                //        //play animation once- update no real animation to play i suppose
                //    }

                //    //animationTimer += Time.fixedDeltaTime;
                //    //if (animationTimer > 0.2f)
                //    //{
                //    //    state = BeamState.ACTIVE;
                //    //    //if (areaIndicator)
                //    //    //{
                //    //    //    this.areaIndicator.SetActive(false);
                //    //    //    EntityState.Destroy(this.areaIndicator);
                //    //    //}
                //    //}
                //    break;
                case BeamState.ACTIVE:

                    if (!laserEffect)
                    {
                        laserEffect = UnityEngine.Object.Instantiate(Assets.xiconstructBeamLaser, startPosition, Quaternion.identity);
                        //laserEffect.transform.parent = FindModelChild(this.muzzleStringR).transform;                        
                        laserEffectEndTransform = laserEffect.GetComponent<ChildLocator>().FindChild("LaserEnd");
                        laserEffectEndTransform.position = endPosition;
                    }

                    stopwatch += Time.fixedDeltaTime;
                    float num = 1f / tickFrequency / this.attackSpeedStat;
                    if (this.stopwatch > num)
                    {
                        this.stopwatch -= num;
                        this.FireBeam();
                    }
                    activeStopwatch += Time.fixedDeltaTime;
                    if (activeStopwatch > beamDuration)
                    {
                        this.outer.SetNextStateToMain();
                        return;

                    }

                    break;

            }

        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
