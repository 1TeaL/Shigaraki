using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using EntityStates.ImpMonster;
using RoR2.Navigation;

namespace ShiggyMod.SkillStates
{
    public class ImpBlink : BaseSkillState
    {
        public float baseDuration = 0.3f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;

        private float baseblinkDistance = 25f;
        private float blinkDistance;

        private Transform modelTransform;
        private Animator animator;
        private CharacterModel characterModel;
        private HurtBoxGroup hurtboxGroup;

        private Vector3 blinkDestination = Vector3.zero;
        private Vector3 blinkStart = Vector3.zero;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            blinkDistance = baseblinkDistance * (moveSpeedStat / 7);
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);

            Util.PlaySound(BlinkState.beginSoundString, base.gameObject);
            this.modelTransform = base.GetModelTransform();
            if (this.modelTransform)
            {
                this.animator = this.modelTransform.GetComponent<Animator>();
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
            if (base.characterMotor)
            {
                base.characterMotor.enabled = false;
            }
            Vector3 b = base.inputBank.moveVector * blinkDistance;
            this.blinkDestination = base.transform.position;
            this.blinkStart = base.transform.position;
            NodeGraph groundNodes = SceneInfo.instance.groundNodes;
            NodeGraph.NodeIndex nodeIndex = groundNodes.FindClosestNode(base.transform.position + b, base.characterBody.hullClassification, float.PositiveInfinity);
            groundNodes.GetNodePosition(nodeIndex, out this.blinkDestination);
            this.blinkDestination += base.transform.position - base.characterBody.footPosition;
            this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));


        }
        private void CreateBlinkEffect(Vector3 origin)
        {
            EffectData effectData = new EffectData();
            effectData.rotation = Util.QuaternionSafeLookRotation(this.blinkDestination - this.blinkStart);
            effectData.origin = origin;
            EffectManager.SpawnEffect(BlinkState.blinkPrefab, effectData, false);
        }
        private void SetPosition(Vector3 newPosition)
        {
            if (base.characterMotor)
            {
                base.characterMotor.Motor.SetPositionAndRotation(newPosition, Quaternion.identity, true);
            }
        }
        public override void OnExit()
        {
            Util.PlaySound(BlinkState.endSoundString, base.gameObject);
            this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
            this.modelTransform = base.GetModelTransform();
            if (this.modelTransform && BlinkState.destealthMaterial)
            {
                TemporaryOverlay temporaryOverlay = this.animator.gameObject.AddComponent<TemporaryOverlay>();
                temporaryOverlay.duration = 1f;
                temporaryOverlay.destroyComponentOnEnd = true;
                temporaryOverlay.originalMaterial = BlinkState.destealthMaterial;
                temporaryOverlay.inspectorCharacterModel = this.animator.gameObject.GetComponent<CharacterModel>();
                temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                temporaryOverlay.animateShaderAlpha = true;
                
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
            if (base.characterMotor)
            {
                base.characterMotor.enabled = true;
            }
            //this.PlayAnimation("Gesture, Additive", "BlinkEnd");
            base.OnExit();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.characterMotor && base.characterDirection)
            {
                base.characterMotor.velocity = Vector3.zero;
            }
            this.SetPosition(Vector3.Lerp(this.blinkStart, this.blinkDestination, this.fixedAge / duration));


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
