using System;
using System.Collections.Generic;
using EntityStates;
using EntityStates.VagrantMonster;
using R2API;
using R2API.Networking;
using RoR2;
using ShiggyMod.Modules;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.Networking;
using EntityStates.Merc;
using System.Linq;

namespace ShiggyMod.SkillStates
{
    internal class ExtremeSpeed : BaseSkillState
    {
        //mach punch + thunder clap
		private float baseDuration = 1f;
        private float duration;
        internal float radius;
		internal float distance = StaticValues.extremeSpeedDistance;
		private Vector3 startPos;
		private Vector3 endPos;
        private Vector3 theSpot;
        internal Vector3 moveVec;
        private int numberOfHits;

        private Transform modelTransform;
        private string muzzleString = "RHand";
		private GameObject explosionPrefab = Assets.loaderOmniImpactLightningEffect;
        private GameObject muzzlePrefab = Assets.loaderMuzzleFlashEffect;
        public GameObject blastEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/SonicBoomEffect");
        private readonly BullseyeSearch search = new BullseyeSearch();

        public override void OnEnter()
        {
			
			base.OnEnter();
			duration = baseDuration / attackSpeedStat;
            numberOfHits = Mathf.RoundToInt(StaticValues.extremeSpeedNumberOfHits * attackSpeedStat);
            if(numberOfHits < StaticValues.extremeSpeedNumberOfHits)
            {
                numberOfHits = StaticValues.extremeSpeedNumberOfHits;
            }

            base.characterMotor.velocity = Vector3.zero;
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            base.PlayCrossfade("FullBody, Override", "FullBodyDash", "Attack.playbackRate", duration, 0.05f);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
            //Util.PlaySound(FireMegaNova.novaSoundString, base.gameObject);
            Util.PlaySound(EvisDash.endSoundString, base.gameObject);
            EffectManager.SimpleMuzzleFlash(this.blastEffectPrefab, base.gameObject, this.muzzleString, false);
			
			Vector3 startPos = characterBody.transform.position;
			Ray aimRay = base.GetAimRay();

            float sprintMultiplier = 1f;
            if (characterBody.isSprinting)
            {
                sprintMultiplier = 1.5f;
            }
			moveVec = (aimRay.direction * distance * (moveSpeedStat/characterBody.baseMoveSpeed))/sprintMultiplier;
            base.characterMotor.rootMotion += this.moveVec;

            if (this.modelTransform)
            {
                TemporaryOverlay temporaryOverlay = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
                temporaryOverlay.duration = duration;
                temporaryOverlay.animateShaderAlpha = true;
                temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                temporaryOverlay.destroyComponentOnEnd = true;
                temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matVagrantEnergized");
                temporaryOverlay.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
            }
            EffectManager.SimpleMuzzleFlash(EvisDash.blinkPrefab, base.gameObject, this.muzzleString, false);
            EffectManager.SimpleMuzzleFlash(Modules.Assets.muzzleflashMageLightningLargePrefab, base.gameObject, this.muzzleString, false);
            CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
        }
		public override void OnExit()
		{
			base.OnExit();
            endPos = characterBody.transform.position;
            ApplyComponent();
            CreateBlinkEffect(Util.GetCorePosition(base.gameObject));

        }
		public override void FixedUpdate()
		{
			base.FixedUpdate();


            if (base.fixedAge >= duration && base.isAuthority)
            {
				this.outer.SetNextStateToMain();
			}
		}

        private void CreateBlinkEffect(Vector3 origin)
        {
            EffectData effectData = new EffectData();
            effectData.rotation = Util.QuaternionSafeLookRotation(characterDirection.forward);
            effectData.origin = origin;
            EffectManager.SpawnEffect(EvisDash.blinkPrefab, effectData, false);
        }

        public void ApplyComponent()
        {
            theSpot = Vector3.Lerp(startPos, endPos, 0.5f);

            search.teamMaskFilter = TeamMask.GetEnemyTeams(base.GetTeam());
            search.filterByLoS = false;
            search.searchOrigin = theSpot;
            search.searchDirection = UnityEngine.Random.onUnitSphere;
            search.sortMode = BullseyeSearch.SortMode.Distance;
            search.maxDistanceFilter = (endPos - startPos).magnitude / 2;
            search.maxAngleFilter = 360f;


            search.RefreshCandidates();
            search.FilterOutGameObject(base.gameObject);



            List<HurtBox> target = search.GetResults().ToList<HurtBox>();
            foreach (HurtBox singularTarget in target)
            {
                if (singularTarget.healthComponent.body && singularTarget.healthComponent)
                {
                    int buffcount = singularTarget.healthComponent.body.GetBuffCount(Modules.Buffs.extremeSpeedHitsDebuff.buffIndex);
                    if (NetworkServer.active)
                    {
                        singularTarget.healthComponent.body.ApplyBuff(Modules.Buffs.extremeSpeedHitsDebuff.buffIndex, numberOfHits + buffcount);
                    }
                    ExtremeSpeedComponent extremeCon = singularTarget.healthComponent.body.gameObject.GetComponent<ExtremeSpeedComponent>();

                    if (extremeCon)
                    {
                        extremeCon.numberOfHits += numberOfHits;
                        extremeCon.timer = 0;
                    }
                    if (!extremeCon)
                    {
                        extremeCon = singularTarget.healthComponent.body.gameObject.AddComponent<ExtremeSpeedComponent>();
                        extremeCon.charbody = singularTarget.healthComponent.body;
                        extremeCon.shiggycharbody = characterBody;
                        extremeCon.numberOfHits = numberOfHits;
                        extremeCon.damage = base.damageStat * Modules.StaticValues.extremeSpeedDamageCoefficient;
                        extremeCon.interval = StaticValues.extremeSpeedIntervals / attackSpeedStat;
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