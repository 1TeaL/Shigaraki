using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using EntityStates.Captain;
using EntityStates.Captain.Weapon;
using RoR2.UI;
using EntityStates.Treebot.Weapon;
using EmotesAPI;
using System;
using EntityStates.Huntress;
using Object = UnityEngine.Object;
using ExtraSkillSlots;

namespace ShiggyMod.SkillStates
{
    //captain + void reaver/nullifier
    public class OrbitalStrike : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float baseDuration = 0.7f;
        public float duration;
        public float durationAfterTimer;
        public ShiggyController Shiggycon;
        public HurtBox Target;
        private Animator animator;
        public bool hasFired = false;
        private Vector3 point;
        private GameObject aimSphere;
        private float maxDistance = Modules.StaticValues.orbitalStrikeMaxDistance;

        private float damageCoefficient = Modules.StaticValues.orbitalStrikeDamageCoefficient;
        private float force = 400f;
        private string muzzleName = "LHand";
        private GameObject effectMuzzleInstance;
        private CrosshairUtils.OverrideRequest crosshairOverrideRequest;
        private Ray aimRay;

        private ExtraSkillLocator extraSkillLocator;
        private ExtraInputBankTest extraInputBank;


        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            extraSkillLocator = gameObject.GetComponent<ExtraSkillLocator>();
            extraInputBank = gameObject.GetComponent<ExtraInputBankTest>();

            //EffectManager.SimpleMuzzleFlash(EntityStates.Captain.Weapon.SetupAirstrike, base.gameObject, muzzleName, false);
            Util.PlaySound(SetupAirstrike.enterSoundString, base.gameObject);
            Util.PlaySound("Play_captain_shift_active_loop", base.gameObject);

            Transform transform = base.FindModelChild(muzzleName);
            if (transform)
            {
                this.effectMuzzleInstance = UnityEngine.Object.Instantiate<GameObject>(SetupAirstrike.effectMuzzlePrefab, transform);
            }
            if (SetupAirstrike.crosshairOverridePrefab)
            {
                this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, SetupAirstrike.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
            }
            PlayAnimation("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", duration);

            this.aimSphere = Object.Instantiate<GameObject>(ArrowRain.areaIndicatorPrefab);
            aimSphere.SetActive(true);

        }

        public override void OnExit()
        {
            base.OnExit();

            Util.PlaySound(SetupAirstrike.exitSoundString, base.gameObject);
            Util.PlaySound("Stop_captain_shift_active_loop", base.gameObject);
            if (this.effectMuzzleInstance)
            {
                EntityState.Destroy(this.effectMuzzleInstance);
            }
            CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
            if (overrideRequest != null)
            {
                overrideRequest.Dispose();
            }
            aimSphere.SetActive(false);
            EntityState.Destroy(this.aimSphere.gameObject);
        }
        private void Fire()
        {
            FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
            {
                projectilePrefab = Modules.Assets.captainAirStrikeProj,
                position = point,
                rotation = Quaternion.identity,
                owner = base.gameObject,
                damage = damageCoefficient * this.damageStat,
                force = force,
                crit = base.RollCrit(),
                damageColorIndex = DamageColorIndex.Default,
                target = null,
                speedOverride = 0f,
                fuseOverride = -1f,
                
            };
            ProjectileManager.instance.FireProjectile(fireProjectileInfo);

            Util.PlaySound(CallAirstrikeBase.fireAirstrikeSoundString, base.gameObject);
        }

        public override void Update()
        {
            base.Update();
            RaycastHit raycastHit;
            if (base.inputBank.GetAimRaycast(maxDistance, out raycastHit))
            {
                point = raycastHit.point;
            }
            else
            {
                point = base.inputBank.GetAimRay().GetPoint(maxDistance);
            }
            if (isAuthority)
            {
                this.aimSphere.transform.localScale = new Vector3(5f, 5f, 5f);
            }
            aimRay = base.GetAimRay();
            bool flag = Physics.Raycast(base.GetAimRay(), out raycastHit, maxDistance, LayerIndex.world.mask | LayerIndex.entityPrecise.mask);            
            if (flag)
            {
                this.aimSphere.transform.position = raycastHit.point + Vector3.up;
                this.aimSphere.transform.up = raycastHit.normal;
                this.aimSphere.transform.forward = -this.aimRay.direction;
            }
            else
            {
                Ray ray = base.GetAimRay();
                Vector3 position = ray.origin + this.maxDistance * ray.direction;
                this.aimSphere.transform.position = position;
                this.aimSphere.transform.up = raycastHit.normal;
                this.aimSphere.transform.forward = -this.aimRay.direction;
            }

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            base.characterBody.SetAimTimer(this.duration);
            if (base.IsKeyDownAuthority())
            {
                PlayAnimation("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", duration);
            }
            else if (!base.IsKeyDownAuthority())
            {
                if (inputBank.skill1.justReleased && characterBody.skillLocator.primary.skillNameToken == Shiggy.orbitalStrikeDef.skillNameToken)
                {
                    ReleaseKey();
                }
                if (inputBank.skill2.justReleased && characterBody.skillLocator.secondary.skillNameToken == Shiggy.orbitalStrikeDef.skillNameToken)
                {
                    ReleaseKey();
                }
                if (inputBank.skill3.justReleased && characterBody.skillLocator.utility.skillNameToken == Shiggy.orbitalStrikeDef.skillNameToken)
                {
                    ReleaseKey();
                }
                if (inputBank.skill4.justReleased && characterBody.skillLocator.special.skillNameToken == Shiggy.orbitalStrikeDef.skillNameToken)
                {
                    ReleaseKey();
                }
                if (extraInputBank.extraSkill1.justReleased && extraSkillLocator.extraFirst.skillNameToken == Shiggy.orbitalStrikeDef.skillNameToken)
                {
                    ReleaseKey();
                }
                if (extraInputBank.extraSkill2.justReleased && extraSkillLocator.extraSecond.skillNameToken == Shiggy.orbitalStrikeDef.skillNameToken)
                {
                    ReleaseKey();
                }
                if (extraInputBank.extraSkill3.justReleased && extraSkillLocator.extraThird.skillNameToken == Shiggy.orbitalStrikeDef.skillNameToken)
                {
                    ReleaseKey();
                }
                if (extraInputBank.extraSkill4.justReleased && extraSkillLocator.extraFourth.skillNameToken == Shiggy.orbitalStrikeDef.skillNameToken)
                {
                    ReleaseKey();
                }

                durationAfterTimer += Time.fixedDeltaTime;
                if (durationAfterTimer > duration / 2f)
                {
                    this.outer.SetNextStateToMain();
                    return;

                }
            }
        }
        public void ReleaseKey()
        {
            if (!hasFired)
            {
                hasFired = true;
                this.Fire();
                base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
                int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
                base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
                //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
                if (base.isAuthority)
                {
                    if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
                }
            }

        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
