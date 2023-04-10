using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using EntityStates.Mage.Weapon;
using RoR2.UI;
using ExtraSkillSlots;

namespace ShiggyMod.SkillStates
{
    public class ArtificerIceWall : BaseSkillState
    {
        private ExtraInputBankTest extrainputBankTest;
        private ExtraSkillLocator extraskillLocator;
        private bool skillSwapped;
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float baseDuration = 1f;
        public float duration;
        public ShiggyController Shiggycon;
        public HurtBox Target;
        private float damageCoefficient = Modules.StaticValues.artificericewallDamageCoefficient;

        public static GameObject areaIndicatorPrefab;
        private GameObject areaIndicatorInstance;
        private bool goodPlacement;
        private CrosshairUtils.OverrideRequest crosshairOverrideRequest;
        private float stopwatch;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = PrepWall.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration + 2f);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", duration, 0.1f);

            AkSoundEngine.PostEvent("ShiggyAirCannon", base.gameObject);
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            extraskillLocator = base.GetComponent<ExtraSkillLocator>();
            extrainputBankTest = outer.GetComponent<ExtraInputBankTest>();
            skillSwapped = false;
            

            Util.PlaySound(PrepWall.prepWallSoundString, base.gameObject);
            this.areaIndicatorInstance = UnityEngine.Object.Instantiate<GameObject>(PrepWall.areaIndicatorPrefab);
            this.UpdateAreaIndicator();
            //PlayCrossfade("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", duration, 0.1f);
            //PlayCrossfade("RightArm, Override", "RightArmOut", "Attack.playbackRate", duration, 0.1f);

        }
        private void UpdateAreaIndicator()
        {
            bool flag = this.goodPlacement;
            this.goodPlacement = false;
            this.areaIndicatorInstance.SetActive(true);
            if (this.areaIndicatorInstance)
            {
                float num = PrepWall.maxDistance;
                float num2 = 0f;
                Ray aimRay = base.GetAimRay();
                RaycastHit raycastHit;
                if (Physics.Raycast(CameraRigController.ModifyAimRayIfApplicable(aimRay, base.gameObject, out num2), out raycastHit, num + num2, LayerIndex.world.mask))
                {
                    this.areaIndicatorInstance.transform.position = raycastHit.point;
                    this.areaIndicatorInstance.transform.up = raycastHit.normal;
                    this.areaIndicatorInstance.transform.forward = -aimRay.direction;
                    this.goodPlacement = (Vector3.Angle(Vector3.up, raycastHit.normal) < PrepWall.maxSlopeAngle);
                }
                if (flag != this.goodPlacement || this.crosshairOverrideRequest == null)
                {
                    CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
                    if (overrideRequest != null)
                    {
                        overrideRequest.Dispose();
                    }
                    GameObject crosshairPrefab = this.goodPlacement ? PrepWall.goodCrosshairPrefab : PrepWall.badCrosshairPrefab;
                    this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, crosshairPrefab, CrosshairUtils.OverridePriority.Skill);
                }
            }
            this.areaIndicatorInstance.SetActive(this.goodPlacement);
        }
        public override void Update()
        {
            base.Update();
            this.UpdateAreaIndicator();
        }
        public override void OnExit()
        {
            base.OnExit();

            if (base.skillLocator.primary.skillNameToken == prefix + "ARTIFICERICEWALL_NAME")
            {
                characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (base.skillLocator.secondary.skillNameToken == prefix + "ARTIFICERICEWALL_NAME")
            {
                characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (base.skillLocator.utility.skillNameToken == prefix + "ARTIFICERICEWALL_NAME")
            {
                characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
            }
            if (base.skillLocator.special.skillNameToken == prefix + "ARTIFICERICEWALL_NAME")
            {
                characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
            }

            if (!this.outer.destroying)
            {
                if (this.goodPlacement)
                {
                    PlayCrossfade("LeftArm, Override", "LeftArmPunch", "Attack.playbackRate", duration, 0.1f);
                    Util.PlaySound(PrepWall.fireSoundString, base.gameObject);
                    if (this.areaIndicatorInstance && base.isAuthority)
                    {
                        EffectManager.SimpleMuzzleFlash(PrepWall.muzzleflashEffect, base.gameObject, "LHand", true);
                        //EffectManager.SimpleMuzzleFlash(PrepWall.muzzleflashEffect, base.gameObject, "MuzzleRight", true);
                        Vector3 forward = this.areaIndicatorInstance.transform.forward;
                        forward.y = 0f;
                        forward.Normalize();
                        Vector3 vector = Vector3.Cross(Vector3.up, forward);
                        bool crit = Util.CheckRoll(this.critStat, base.characterBody.master);

                        if (base.HasBuff(Modules.Buffs.multiplierBuff))
                        {
                            IceWall(vector);
                            IceWall(vector);
                            IceWall(vector);
                        }
                        else
                        {
                            IceWall(vector);
                        }
                    }
                }
                else
                {
                    if(base.skillLocator.primary.skillNameToken == prefix + "ARTIFICERICEWALL_NAME")
                    {
                        base.skillLocator.primary.AddOneStock();
                    }
                    if (base.skillLocator.secondary.skillNameToken == prefix + "ARTIFICERICEWALL_NAME")
                    {
                        base.skillLocator.secondary.AddOneStock();
                    }
                    if (base.skillLocator.utility.skillNameToken == prefix + "ARTIFICERICEWALL_NAME")
                    {
                        base.skillLocator.utility.AddOneStock();
                    }
                    if (base.skillLocator.special.skillNameToken == prefix + "ARTIFICERICEWALL_NAME")
                    {
                        base.skillLocator.special.AddOneStock();
                    }
                    if (extraskillLocator.extraFirst.skillNameToken == prefix + "ARTIFICERICEWALL_NAME")
                    {
                        extraskillLocator.extraFirst.AddOneStock();
                    }
                    if (extraskillLocator.extraSecond.skillNameToken == prefix + "ARTIFICERICEWALL_NAME")
                    {
                        extraskillLocator.extraSecond.AddOneStock();
                    }
                    if (extraskillLocator.extraThird.skillNameToken == prefix + "ARTIFICERICEWALL_NAME")
                    {
                        extraskillLocator.extraThird.AddOneStock();
                    }
                    if (extraskillLocator.extraFourth.skillNameToken == prefix + "ARTIFICERICEWALL_NAME")
                    {
                        extraskillLocator.extraFourth.AddOneStock();
                    }
                    PlayCrossfade("LeftArm, Override", "Empty", "Attack.playbackRate", 0.1f, 0.1f);
                }
            }
            EntityState.Destroy(this.areaIndicatorInstance.gameObject);
            CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
            if (overrideRequest != null)
            {
                overrideRequest.Dispose();
            }
        }

        public void IceWall(Vector3 vector)
        {

            ProjectileManager.instance.FireProjectile(PrepWall.projectilePrefab, this.areaIndicatorInstance.transform.position + Vector3.up, Util.QuaternionSafeLookRotation(vector), base.gameObject, this.damageStat * damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
            ProjectileManager.instance.FireProjectile(PrepWall.projectilePrefab, this.areaIndicatorInstance.transform.position + Vector3.up, Util.QuaternionSafeLookRotation(-vector), base.gameObject, this.damageStat * damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);

        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.IsKeyDownAuthority() && !skillSwapped)
            {
                skillSwapped = true;
                if (base.inputBank.skill1.down)
                {
                    characterBody.skillLocator.primary.UnsetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.skillLocator.primary.SetSkillOverride(characterBody.skillLocator.primary, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (base.inputBank.skill2.down)
                {
                    characterBody.skillLocator.secondary.UnsetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.skillLocator.secondary.SetSkillOverride(characterBody.skillLocator.secondary, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (base.inputBank.skill3.down)
                {
                    characterBody.skillLocator.utility.UnsetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.skillLocator.utility.SetSkillOverride(characterBody.skillLocator.utility, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (base.inputBank.skill4.down)
                {
                    characterBody.skillLocator.special.UnsetSkillOverride(characterBody.skillLocator.special, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                    characterBody.skillLocator.special.SetSkillOverride(characterBody.skillLocator.special, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (extrainputBankTest.extraSkill1.down)
                {
                    extraskillLocator.extraFirst.UnsetSkillOverride(extraskillLocator.extraFirst, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                    extraskillLocator.extraFirst.SetSkillOverride(extraskillLocator.extraFirst, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (extrainputBankTest.extraSkill2.down)
                {
                    extraskillLocator.extraSecond.UnsetSkillOverride(extraskillLocator.extraSecond, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                    extraskillLocator.extraSecond.SetSkillOverride(extraskillLocator.extraSecond, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (extrainputBankTest.extraSkill3.down)
                {
                    extraskillLocator.extraThird.UnsetSkillOverride(extraskillLocator.extraThird, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                    extraskillLocator.extraThird.SetSkillOverride(extraskillLocator.extraThird, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
                }
                if (extrainputBankTest.extraSkill4.down)
                {
                    extraskillLocator.extraFourth.UnsetSkillOverride(extraskillLocator.extraFourth, Shiggy.artificericewallDef, GenericSkill.SkillOverridePriority.Contextual);
                    extraskillLocator.extraFourth.SetSkillOverride(extraskillLocator.extraFourth, Shiggy.artificerlightningorbDef, GenericSkill.SkillOverridePriority.Contextual);
                }
            }
            PlayCrossfade("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", duration, 0.1f);
            this.stopwatch += Time.fixedDeltaTime;
            if (this.stopwatch >= this.duration && !base.IsKeyDownAuthority() && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
            }
        }



        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
