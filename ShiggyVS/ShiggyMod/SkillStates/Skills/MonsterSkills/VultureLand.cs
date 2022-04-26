using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using RoR2.Skills;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class VultureLand : BaseSkillState
    {
        public static float basejumpDuration = 1f;
        public static float jumpDuration;
        public static float dropForce = 80f;

        public static float slamRadius = 15f;

        private bool hasDropped;
        private Vector3 flyVector = Vector3.zero;
        private Transform modelTransform;


        public override void OnEnter()
        {
            base.OnEnter();
            this.modelTransform = base.GetModelTransform();
            this.flyVector = Vector3.up;

            base.characterMotor.Motor.ForceUnground();
            base.characterMotor.velocity = Vector3.zero;



            //base.gameObject.layer = LayerIndex.fakeActor.intVal;
            base.characterMotor.Motor.RebuildCollidableLayers();

            base.skillLocator.utility.UnsetSkillOverride(base.skillLocator.utility, VultureFly.utilityDef, GenericSkill.SkillOverridePriority.Contextual);

            if (NetworkServer.active)
            {
                base.characterBody.RemoveBuff(ShiggyMod.Modules.Buffs.flyBuff);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!this.hasDropped)
            {
                this.StartDrop();
            }

            if (this.hasDropped && base.isAuthority && !base.characterMotor.disableAirControlUntilCollision)
            {
                this.LandingImpact();
                this.outer.SetNextStateToMain();
            }

            if (this.hasDropped && base.isAuthority && base.fixedAge > basejumpDuration)
            {
                this.LandingImpact();
                this.outer.SetNextStateToMain();
            }

        }

        private void StartDrop()
        {
            this.hasDropped = true;

            base.characterMotor.disableAirControlUntilCollision = true;
            base.characterMotor.velocity.y = -VultureLand.dropForce;

            bool active = NetworkServer.active;
            if (active)
            {
                base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
            }



        }



        private void LandingImpact()
        {

            if (base.isAuthority)
            {
                base.characterMotor.velocity *= 0.1f;
            }
        }


        public override void OnExit()
        {

            //base.PlayAnimation("FullBody, Override", "BufferEmpty");

            base.characterMotor.useGravity = true;
            base.characterBody.bodyFlags &= ~CharacterBody.BodyFlags.IgnoreFallDamage;

            if (NetworkServer.active && base.characterBody.HasBuff(RoR2Content.Buffs.HiddenInvincibility)) base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);

            base.gameObject.layer = LayerIndex.defaultLayer.intVal;
            base.characterMotor.Motor.RebuildCollidableLayers();
            base.OnExit();
        }



        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}