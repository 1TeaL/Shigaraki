using EntityStates;
using RoR2;
using RoR2.Audio;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class Genesis : Skill
    {
        //Xi construct + Clay apothecary

        private float totalHits;


        private string muzzleString;

        private ChildLocator childLocator;
        public LoopSoundDef loopSoundDef = Modules.ShiggyAsset.xiconstructsound;
        private LoopSoundManager.SoundLoopPtr loopPtr;

        private BullseyeSearch search;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            totalHits = StaticValues.genesisNumberOfAttacks * attackSpeedStat;
            if (totalHits < StaticValues.genesisNumberOfAttacks)
            {
                totalHits = StaticValues.genesisNumberOfAttacks;
            }
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            //base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            base.PlayCrossfade("RightArm, Override", "RHandDown", "Attack.playbackRate", duration, 0.05f);
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

            EffectManager.SimpleMuzzleFlash(Modules.ShiggyAsset.xiconstructbeamEffect, base.gameObject, muzzleString, false);
            if (this.loopSoundDef)
            {
                this.loopPtr = LoopSoundManager.PlaySoundLoopLocal(base.gameObject, this.loopSoundDef);
            }

            Shiggycon = gameObject.GetComponent<ShiggyController>();

            //if (Asset.xiconstructbeamEffect)
            //{
            //    this.chargeEffect = UnityEngine.Object.Instantiate<GameObject>(Asset.xiconstructbeamEffect, this.childLocator.FindChild(muzzleString).position, Util.QuaternionSafeLookRotation(aimRay.direction.normalized));
            //    this.chargeEffect.transform.parent = this.childLocator.FindChild(muzzleString);
            //}


            search = new BullseyeSearch();
            SearchForTarget();


        }
        public override void OnExit()
        {
            base.OnExit();
            LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
            //if (this.chargeEffect)
            //{
            //    EntityState.Destroy(this.chargeEffect);
            //}
        }

        private void SearchForTarget()
        {
            this.search.teamMaskFilter = TeamMask.GetUnprotectedTeams(characterBody.teamComponent.teamIndex);
            this.search.filterByLoS = true;
            this.search.searchOrigin = characterBody.transform.position;
            this.search.searchDirection = Vector3.up;
            this.search.sortMode = BullseyeSearch.SortMode.Distance;
            this.search.maxDistanceFilter = StaticValues.genesisRadius;
            this.search.maxAngleFilter = 360f;
            this.search.RefreshCandidates();
            this.search.FilterOutGameObject(characterBody.gameObject);

            List<HurtBox> target = search.GetResults().ToList<HurtBox>();

            foreach (HurtBox singularTarget in target)
            {
                Debug.Log("add gencon");
                GenesisController gencon = singularTarget.healthComponent.gameObject.GetComponent<GenesisController>();


                if (gencon)
                {
                    gencon.totalHits += Mathf.RoundToInt(totalHits);
                    gencon.duration = 0f;
                }
                if (!gencon)
                {
                    gencon = singularTarget.healthComponent.gameObject.AddComponent<GenesisController>();
                    gencon.Target = singularTarget;
                    gencon.shiggyBody = characterBody;
                    gencon.totalHits = Mathf.RoundToInt(totalHits);
                }

            }

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
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