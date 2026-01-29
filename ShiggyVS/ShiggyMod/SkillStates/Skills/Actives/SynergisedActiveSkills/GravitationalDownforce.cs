using EntityStates;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Networking;
using ShiggyMod.Modules.Survivors;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class GravitationalDownforce : Skill
    {
        //Void jailer + solus control unit

        private BulletAttack bulletAttack;

        private float force = StaticValues.gravitationalDownforceForce;
        private string muzzleString;
        private Vector3 direction = Vector3.down;

        private ChildLocator childLocator;
        private GameObject chargeEffect;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("LeftArm, Override", "LHandDown", "Attack.playbackRate", fireTime, 0.1f);
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
            if (base.fixedAge > fireTime && !hasFired)
            {
                hasFired = true;
                new PeformDirectionalForceNetworkRequest(characterBody.masterObjectId, direction, force, damageStat * StaticValues.gravitationalDownforceDamageCoefficient, StaticValues.gravitationalDownforceRange).Send(NetworkDestination.Clients);
            }
            if (base.fixedAge > duration)
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