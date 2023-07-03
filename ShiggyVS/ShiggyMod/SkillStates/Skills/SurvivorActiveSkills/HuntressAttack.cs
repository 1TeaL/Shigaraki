using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using EntityStates.Huntress;
using RoR2.Orbs;
using EntityStates.Huntress.HuntressWeapon;

namespace ShiggyMod.SkillStates
{
    public class HuntressAttack : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float baseDuration = 1f;
        public float duration;
        private float critBaseArrowReloadDuration = 0.05f;
        private float baseArrowReloadDuration = 0.1f;
        private float arrowReloadDuration;
        public ShiggyController Shiggycon;
        private DamageType damageType;
        public HurtBox Target;


        private float damageCoefficient = Modules.StaticValues.huntressDamageCoefficient;
        private float procCoefficient = Modules.StaticValues.huntressProcCoefficient;
        private Transform modelTransform;
        private CharacterModel characterModel;
        private ChildLocator childLocator;
        private Animator animator;
        private bool isCrit;
        private int firedArrowCount;
        private GameObject muzzleflashEffectPrefab;
        private int maxArrowCount = Modules.StaticValues.huntressmaxArrowCount/2;
        private int critMaxArrowCount = Modules.StaticValues.huntressmaxArrowCount;
        private float arrowReloadTimer;
        private string muzzleString = "RHand";

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            if (base.characterBody)
            {
                base.characterBody.SetAimTimer(this.duration + 1f);
            }
            damageType = DamageType.Generic;

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
            //base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            Transform modelTransform = base.GetModelTransform();
            if (modelTransform)
            {
                this.childLocator = modelTransform.GetComponent<ChildLocator>();
                this.animator = modelTransform.GetComponent<Animator>();
            }
            Util.PlayAttackSpeedSound(EntityStates.Huntress.HuntressWeapon.IdleTracking.attackSoundString, base.gameObject, this.attackSpeedStat);


            if (Shiggycon.trackingTarget.teamIndex == TeamIndex.Monster || Shiggycon.trackingTarget.teamIndex == TeamIndex.Neutral || Shiggycon.trackingTarget.teamIndex == TeamIndex.Void)
            {
                Target = Shiggycon.GetTrackingTarget();
            }
            if (!Target)
            {
                return;
            }
            this.isCrit = Util.CheckRoll(base.characterBody.crit, base.characterBody.master);
            if (this.isCrit)
            {
                this.muzzleflashEffectPrefab = FireFlurrySeekingArrow.critMuzzleflashEffectPrefab;
                this.maxArrowCount = critMaxArrowCount;
                this.arrowReloadDuration = critBaseArrowReloadDuration / (attackSpeedStat);
            }
            else
            {
                this.muzzleflashEffectPrefab = IdleTracking.muzzleflashEffectPrefab;
                this.maxArrowCount = maxArrowCount;
                this.arrowReloadDuration = baseArrowReloadDuration / (attackSpeedStat);
            }
        }
        public override void OnExit()
        {
            base.OnExit();
			this.FireOrbArrow();
        }
        protected virtual GenericDamageOrb CreateArrowOrb()
        {
            return new HuntressFlurryArrowOrb();
        }
        private void FireOrbArrow()
        {
            if (this.firedArrowCount >= this.maxArrowCount || this.arrowReloadTimer > 0f || !NetworkServer.active)
            {
                return;
            }
            this.firedArrowCount++;
            this.arrowReloadTimer = this.arrowReloadDuration;
            GenericDamageOrb genericDamageOrb = this.CreateArrowOrb();
            genericDamageOrb.damageValue = base.characterBody.damage * damageCoefficient;
            genericDamageOrb.isCrit = this.isCrit;
            genericDamageOrb.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
            genericDamageOrb.attacker = base.gameObject;
            genericDamageOrb.procCoefficient = procCoefficient;
            genericDamageOrb.damageType = damageType;
            
            HurtBox hurtBox = this.Target;
            if (hurtBox)
            {
                Transform transform = this.childLocator.FindChild(this.muzzleString);
                EffectManager.SimpleMuzzleFlash(this.muzzleflashEffectPrefab, base.gameObject, this.muzzleString, true);
                genericDamageOrb.origin = transform.position;
                genericDamageOrb.target = hurtBox;
                OrbManager.instance.AddOrb(genericDamageOrb);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.arrowReloadTimer -= Time.fixedDeltaTime;
            this.FireOrbArrow();
            if (base.fixedAge > this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }


        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
        public override void OnSerialize(NetworkWriter writer)
        {
            writer.Write(HurtBoxReference.FromHurtBox(this.Target));
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            this.Target = reader.ReadHurtBoxReference().ResolveHurtBox();
        }
    }
}
