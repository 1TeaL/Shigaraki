using ShiggyMod.Modules.Survivors;
using EntityStates;
using RoR2;
using UnityEngine;
using System.Collections.Generic;
using ShiggyMod.Modules;
using UnityEngine.Networking;
using RoR2.ExpansionManagement;
using ExtraSkillSlots;
using R2API.Networking;
using System;
using ShiggyMod.Modules.Networking;
using R2API.Networking.Interfaces;

namespace ShiggyMod.SkillStates
{
    public class GravitationalDownforce : Skill
    {
        //Void jailer + solus control unit

        public float baseDuration = 1f;
        public float duration;
        private float fireTime;
        private BulletAttack bulletAttack;
        public ShiggyController Shiggycon;
        private DamageType damageType;

        private float force = StaticValues.gravitationalDownforceForce;
        private string muzzleString;
        private Vector3 direction = Vector3.down;
        private bool hasFired = false;
        
        private ChildLocator childLocator;
        private Animator animator;
        private GameObject chargeEffect;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            fireTime = duration / 2f;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("LeftArm, Override", "LeftArmPunch", "Attack.playbackRate", fireTime, 0.1f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
            this.muzzleString = "LHand";


            Transform modelTransform = base.GetModelTransform();
            if (modelTransform)
            {
                this.childLocator = modelTransform.GetComponent<ChildLocator>();
                this.animator = modelTransform.GetComponent<Animator>();
            }


            Util.PlaySound(EntityStates.VoidJailer.Weapon.ChargeCapture.enterSoundString, base.gameObject);

            Shiggycon = gameObject.GetComponent<ShiggyController>();

            if (EntityStates.RoboBallBoss.Weapon.ChargeEyeblast.chargeEffectPrefab)
            {
                this.chargeEffect = UnityEngine.Object.Instantiate<GameObject>(EntityStates.RoboBallBoss.Weapon.ChargeEyeblast.chargeEffectPrefab, this.childLocator.FindChild(muzzleString).position, Util.QuaternionSafeLookRotation(aimRay.direction.normalized));
                this.chargeEffect.transform.parent = this.childLocator.FindChild(muzzleString);
            }

            


            PlayCrossfade("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", fireTime, 0.1f);
        }
        public override void OnExit()
        {
            base.OnExit();
            if (this.chargeEffect)
            {
                EntityState.Destroy(this.chargeEffect);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(base.fixedAge > fireTime && !hasFired)
            {
                hasFired = true;
                new PeformDirectionalForceNetworkRequest(characterBody.masterObjectId, direction, force, damageStat *StaticValues.gravitationalDownforceDamageCoefficient, StaticValues.gravitationalDownforceRange).Send(NetworkDestination.Clients);
            }
            if(base.fixedAge > duration)
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