using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using EntityStates.Merc;

namespace ShiggyMod.SkillStates
{
    public class MercDash : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        private float stopwatch;
        public static float smallHopVelocity = 0.5f;
        public static float dashPrepDuration = 0.1f;
        public static float dashDuration = 0.2f;
        public static float speedCoefficient = 20f;
        public ShiggyController Shiggycon;
        private DamageType damageType;
        public HurtBox Target;

        public static float overlapSphereRadius = 7f;
        public static float lollypopFactor = 1f;
        private Vector3 dashVector = Vector3.zero;
        private Animator animator;
        private CharacterModel characterModel;
        private Transform modelTransform;
        private bool isDashing;
        private CameraTargetParams.AimRequest aimRequest;
        public static GameObject blinkPrefab;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            this.modelTransform = base.GetModelTransform();
            if (base.cameraTargetParams)
            {
                this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
            }
            if (this.modelTransform)
            {
                this.animator = this.modelTransform.GetComponent<Animator>();
                this.characterModel = this.modelTransform.GetComponent<CharacterModel>();
            }
            if (base.isAuthority)
            {
                base.SmallHop(base.characterMotor, smallHopVelocity);
            }
            if (NetworkServer.active)
            {
                base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
            }
            PlayCrossfade("FullBody, Override", "Slam", "Attack.playbackRate", dashPrepDuration, 0.1f);
            this.dashVector = base.inputBank.aimDirection;
            base.characterDirection.forward = this.dashVector;
            base.StartAimMode(dashPrepDuration, true);

        }

        private void CreateBlinkEffect(Vector3 origin)
        {
            EffectData effectData = new EffectData();
            effectData.rotation = Util.QuaternionSafeLookRotation(this.dashVector);
            effectData.origin = origin;
            EffectManager.SpawnEffect(EvisDash.blinkPrefab, effectData, false);
        }
        public override void OnExit()
        {
            base.OnExit();
            Util.PlaySound(EvisDash.endSoundString, base.gameObject);
            base.characterMotor.velocity *= 0.1f;
            base.SmallHop(base.characterMotor, smallHopVelocity);
            CameraTargetParams.AimRequest aimRequest = this.aimRequest;
            if (aimRequest != null)
            {
                aimRequest.Dispose();
            }
            if (NetworkServer.active)
            {
                base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
            }
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.stopwatch += Time.fixedDeltaTime;
            if (this.stopwatch > dashPrepDuration && !this.isDashing)
            {
                this.isDashing = true;
                this.dashVector = base.inputBank.aimDirection;
                this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
                if (this.modelTransform)
                {
                    TemporaryOverlay temporaryOverlay = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
                    temporaryOverlay.duration = 0.6f;
                    temporaryOverlay.animateShaderAlpha = true;
                    temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                    temporaryOverlay.destroyComponentOnEnd = true;
                    temporaryOverlay.originalMaterial = RoR2.LegacyResourcesAPI.Load<Material>("Materials/matHuntressFlashBright");
                    temporaryOverlay.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
                    TemporaryOverlay temporaryOverlay2 = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
                    temporaryOverlay2.duration = 0.7f;
                    temporaryOverlay2.animateShaderAlpha = true;
                    temporaryOverlay2.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                    temporaryOverlay2.destroyComponentOnEnd = true;
                    temporaryOverlay2.originalMaterial = RoR2.LegacyResourcesAPI.Load<Material>("Materials/matHuntressFlashExpanded");
                    temporaryOverlay2.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
                }
            }
            bool flag = this.stopwatch >= dashDuration + dashPrepDuration;
            if (this.isDashing)
            {
                if (base.characterMotor && base.characterDirection)
                {
                    base.characterMotor.rootMotion += this.dashVector * (this.moveSpeedStat * speedCoefficient * Time.fixedDeltaTime);
                }
                if (base.isAuthority)
                {
                    Collider[] array = Physics.OverlapSphere(base.transform.position, base.characterBody.radius + overlapSphereRadius * (flag ? lollypopFactor : 1f), LayerIndex.entityPrecise.mask);
                    for (int i = 0; i < array.Length; i++)
                    {
                        HurtBox component = array[i].GetComponent<HurtBox>();
                        if (component && component.healthComponent != base.healthComponent)
                        {
                            MercDashAttack nextState = new MercDashAttack();
                            this.outer.SetNextState(nextState);
                            return;
                        }
                    }
                }
            }
            if (flag && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
            }
        }


        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}
