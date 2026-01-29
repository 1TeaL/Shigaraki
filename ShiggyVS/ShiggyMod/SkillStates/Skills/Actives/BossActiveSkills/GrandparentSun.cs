using EntityStates;
using EntityStates.GrandParent;
using RoR2;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class GrandparentSun : Skill
    {
        public float timer;
        public EnergySystem energySystem;

        public Vector3 sunSpawnPosition;
        private GameObject sunInstance;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("Fullbody, Override", "FullBodyTheWorldCrossArm", "Attack.playbackRate", duration, 0.05f);
            base.GetModelAnimator().SetBool("attacking", true);
            AkSoundEngine.PostEvent("ShiggyExplosion", base.gameObject);

            base.characterMotor.Motor.ForceUnground();
            Shiggycon = base.GetComponent<ShiggyController>();
            energySystem = base.GetComponent<EnergySystem>();
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
                TemporaryOverlayInstance temporaryOverlay = TemporaryOverlayManager.AddOverlay(modelTransform.gameObject);
                temporaryOverlay.duration = this.baseDuration;
                temporaryOverlay.animateShaderAlpha = true;
                temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                temporaryOverlay.destroyComponentOnEnd = true;
                temporaryOverlay.originalMaterial = RoR2.LegacyResourcesAPI.Load<Material>("Materials/matGrandparentTeleportOutBoom");
                temporaryOverlay.AddToCharacterModel(modelTransform.GetComponent<CharacterModel>());
            }

            if (NetworkServer.active)
            {
                //this.sunSpawnPosition = Target.healthComponent.body.corePosition;
                //Vector3? vector = this.sunSpawnPosition;
                this.sunSpawnPosition = characterBody.corePosition + Vector3.up * 2f;
                //this.sunSpawnPosition = ((vector != null) ? vector : ChannelSun.FindSunSpawnPosition(base.transform.position));
                if (this.sunSpawnPosition != null && !sunInstance)
                {
                    this.sunInstance = CreateSun(this.sunSpawnPosition);
                }

            }
            if (sunInstance)
            {
                float plusChaosflatCost = (StaticValues.grandparentSunEnergyCost) - (energySystem.costflatplusChaos);
                if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;

                float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                if (plusChaosCost < 0f) plusChaosCost = 0f;
                energySystem.SpendplusChaos(plusChaosCost);
            }

        }
        //private GameObject CreateSun(GameObject prefab, Vector3 sunSpawnPosition)
        //{

        //    GameObject sun = UnityEngine.Object.Instantiate<GameObject>(prefab, sunSpawnPosition, Quaternion.identity);
        //    sun.GetComponent<GenericOwnership>().ownerObject = base.gameObject;
        //    NetworkServer.Spawn(sun);
        //    return sun;
        //}
        private GameObject CreateSun(Vector3 sunSpawnPosition)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ChannelSun.sunPrefab, sunSpawnPosition, Quaternion.identity);
            gameObject.GetComponent<GenericOwnership>().ownerObject = base.gameObject;
            NetworkServer.Spawn(gameObject);
            return gameObject;
        }
        public override void OnExit()
        {

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

            if (NetworkServer.active)
            {
                //this.sunSpawnPosition = Target.healthComponent.body.corePosition;
                //Vector3? vector = this.sunSpawnPosition;
                this.sunSpawnPosition = characterBody.corePosition + Vector3.up * 10f;
                //this.sunSpawnPosition = ((vector != null) ? vector : ChannelSun.FindSunSpawnPosition(base.transform.position));
                if (this.sunSpawnPosition != null && !sunInstance)
                {
                    this.sunInstance = CreateSun(this.sunSpawnPosition);
                }

            }

            if (base.isAuthority && base.fixedAge >= duration)
            {
                //PlayCrossfade("Fullbody, Override", "ConstantFullBodyTheWorld", "Attack.playbackRate", 0.5f, 0.05f);
                if (!base.IsKeyDownAuthority())
                {
                    base.GetModelAnimator().SetBool("attacking", false);
                    this.outer.SetNextStateToMain();
                    return;
                }
            }
            if (sunInstance)
            {
                if (timer < duration)
                {
                    timer += Time.fixedDeltaTime;

                }
                else if (timer >= duration)
                {
                    timer = 0f;
                    float plusChaosflatCost = (StaticValues.grandparentSunEnergyCost) - (energySystem.costflatplusChaos);
                    if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;

                    float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                    if (plusChaosCost < 0f) plusChaosCost = 0f;

                    if (energySystem.currentplusChaos < plusChaosCost)
                    {
                        energySystem.TriggerGlow(0.3f, 0.3f, Color.black);
                        this.outer.SetNextStateToMain();
                        return;
                    }
                    else if (energySystem.currentplusChaos >= plusChaosCost)
                    {
                        energySystem.SpendplusChaos(plusChaosCost);
                    }
                }

            }
        }





        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }

    }
}
