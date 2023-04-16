using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;

namespace ShiggyMod.SkillStates
{
    public class VoidFiendCleanse : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float baseDuration = 0.3f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;
        private Vector3 forwardVector;
        private Transform modelTransform;
        private uint soundID;
        public HurtBox Target;
        private Animator animator;


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.decayDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 1f;
        private float speedOverride = -1f;
        private CharacterModel characterModel;
        private HurtBoxGroup hurtboxGroup;
        private GameObject blinkVfxInstance;
        public AnimationCurve forwardSpeed;
        public AnimationCurve upSpeed;
        public float speedCoefficient = 20f;
        private float overlayDuration = 1f;
        private Vector3 blinkVector;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            Shiggycon = gameObject.GetComponent<ShiggyController>();


            damageType = DamageType.Generic;
            //this.soundID = Util.PlaySound(this.beginSoundString, base.gameObject);
            this.forwardVector = ((base.inputBank.moveVector == Vector3.zero) ? base.characterDirection.forward : base.inputBank.moveVector).normalized;
            this.modelTransform = base.GetModelTransform();
            if (this.modelTransform)
            {
                this.characterModel = this.modelTransform.GetComponent<CharacterModel>();
                this.hurtboxGroup = this.modelTransform.GetComponent<HurtBoxGroup>();
            }
            if (this.characterModel)
            {
                this.characterModel.invisibilityCount++;
            }
            if (this.hurtboxGroup)
            {
                HurtBoxGroup hurtBoxGroup = this.hurtboxGroup;
                int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter + 1;
                hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
            }
            if (NetworkServer.active)
            {
                Util.CleanseBody(base.characterBody, true, false, false, true, true, false);
            }
            this.blinkVfxInstance = UnityEngine.Object.Instantiate<GameObject>(Modules.Assets.voidfiendblinkVFX);
            this.blinkVfxInstance.transform.SetParent(base.transform, false);
            this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));

            this.blinkVector = this.GetBlinkVector();

        }

        protected virtual Vector3 GetBlinkVector()
        {
            return base.inputBank.aimDirection;
        }
        private void CreateBlinkEffect(Vector3 origin)
        {
            EffectData effectData = new EffectData();
            effectData.rotation = Util.QuaternionSafeLookRotation(base.characterDirection.forward);
            effectData.origin = origin;
            EffectManager.SpawnEffect(Modules.Assets.voidfiendblinkmuzzleEffect, effectData, false);
        }
        public override void OnExit()
        {
            base.OnExit();
            AkSoundEngine.StopPlayingID(this.soundID);
            if (!this.outer.destroying)
            {
                //Util.PlaySound(this.endSoundString, base.gameObject);
                this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
            }
            if (this.blinkVfxInstance)
            {
                VfxKillBehavior.KillVfxObject(this.blinkVfxInstance);
            }
            if (this.characterModel)
            {
                this.characterModel.invisibilityCount--;
            }
            if (this.hurtboxGroup)
            {
                HurtBoxGroup hurtBoxGroup = this.hurtboxGroup;
                int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter - 1;
                hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
            }
            this.modelTransform = base.GetModelTransform();
            if (this.modelTransform)
            {
                TemporaryOverlay temporaryOverlay = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
                temporaryOverlay.duration = this.overlayDuration;
                temporaryOverlay.animateShaderAlpha = true;
                temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                temporaryOverlay.destroyComponentOnEnd = true;
                temporaryOverlay.originalMaterial = Modules.Assets.voidfiendblinkMaterial;
                temporaryOverlay.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
            }
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.characterMotor && base.characterDirection)
            {
                if (base.characterMotor && base.characterDirection)
                {
                    base.characterMotor.Motor.ForceUnground();
                    base.characterMotor.velocity = Vector3.zero;
                    base.characterMotor.rootMotion += this.blinkVector * (this.moveSpeedStat * this.speedCoefficient * Time.fixedDeltaTime);
                }
                //if (this.blinkVfxInstance)
                //{
                //    this.blinkVfxInstance.transform.forward = velocity;
                //}
            }
            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
            }
        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
