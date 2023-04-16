using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;

namespace ShiggyMod.SkillStates
{
    public class GrandparentSun : BaseSkillState
    {
        protected virtual GameObject sunPrefab => Modules.Assets.grandparentSunPrefab;

        public float baseDuration = 1f;
        public float duration;
        public float fireTime;
        public ShiggyController Shiggycon;
        private DamageType damageType;
        public HurtBox Target;

        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.decayDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 1f;
        private float speedOverride = -1f;

        public Vector3 sunSpawnPosition;
        private GameObject sunInstance;
        private GameObject muzzleflashEffectPrefab;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            fireTime = duration / 2f;
            this.muzzleflashEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/ExplosionSolarFlare");
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("RightArm, Override", "RightArmDetonate", "Attack.playbackRate", duration, 0.1f);
            AkSoundEngine.PostEvent(2085946697, base.gameObject);

            Shiggycon = base.GetComponent<ShiggyController>();
            if (Shiggycon && base.isAuthority)
            {
                Target = Shiggycon.GetTrackingTarget();
            }

            if (!Target)
            {
                return;
            }
            Transform modelTransform = base.GetModelTransform();
            if (modelTransform)
            {
                TemporaryOverlay temporaryOverlay = modelTransform.gameObject.AddComponent<TemporaryOverlay>();
                temporaryOverlay.duration = this.baseDuration;
                temporaryOverlay.animateShaderAlpha = true;
                temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                temporaryOverlay.destroyComponentOnEnd = true;
                temporaryOverlay.originalMaterial = RoR2.LegacyResourcesAPI.Load<Material>("Materials/matGrandparentTeleportOutBoom");
                temporaryOverlay.AddToCharacerModel(modelTransform.GetComponent<CharacterModel>());
            }

            if (NetworkServer.active && Target)
            {
                this.sunSpawnPosition = Target.healthComponent.body.corePosition;
                if (this.sunSpawnPosition != null && sunPrefab)
                {
                    this.sunInstance = this.CreateSun(sunPrefab, this.sunSpawnPosition);
                }
            }

        }
        private GameObject CreateSun(GameObject prefab, Vector3 sunSpawnPosition)
        {

            GameObject sun = UnityEngine.Object.Instantiate<GameObject>(prefab, sunSpawnPosition, Quaternion.identity);
            sun.GetComponent<GenericOwnership>().ownerObject = base.gameObject;
            NetworkServer.Spawn(sun);
            return sun;
        }


        public override void OnExit()
        {
            if ((bool)Modules.Assets.grandparentSunSpawnPrefab)
            {
                EffectManager.SimpleImpactEffect(Modules.Assets.grandparentSunSpawnPrefab, sunInstance.transform.position, Vector3.up, transmit: false);
            }

            if (NetworkServer.active && this.sunInstance)
            {
                this.sunInstance.GetComponent<GenericOwnership>().ownerObject = null;
                this.sunInstance = null;
            }
            base.OnExit();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();


            if (base.isAuthority && base.fixedAge >= fireTime)
            {
                if (!base.IsKeyDownAuthority() | base.inputBank.sprint.wasDown)
                {
                    base.characterBody.isSprinting = true;
                    this.outer.SetNextStateToMain();
                    return;
                }
            }
        }
        




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
