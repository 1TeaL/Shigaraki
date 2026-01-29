using EntityStates;
using EntityStates.ImpMonster;
using R2API;
using R2API.Networking;
using RoR2;
using RoR2.UI;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShiggyMod.SkillStates
{
    //imp + bandit
    public class ShadowClaw : Skill
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        private int numberOfHits;
        private int totalHits;
        private float fireInterval;
        private float stopwatch;


        private float radius = StaticValues.shadowClawRadius;
        private float damageCoefficient = StaticValues.shadowClawDamageCoefficient;
        private float procCoefficient = StaticValues.shadowClawProcCoefficient;
        private CrosshairUtils.OverrideRequest crosshairOverrideRequest;
        public GameObject crosshairOverridePrefab = Modules.ShiggyAsset.banditCrosshair;
        private BlastAttack blastAttack;
        private Vector3 randRelPos;
        private Transform modelTransform;

        public override void OnEnter()
        {
            baseDuration = 0.5f;
            base.OnEnter();
            numberOfHits = 0;
            damageType = new DamageTypeCombo(DamageType.BonusToLowHealth | DamageType.ResetCooldownsOnKill, DamageTypeExtended.Generic, DamageSource.Secondary);
            this.duration = this.baseDuration / this.attackSpeedStat;
            fireInterval = duration * StaticValues.shadowClawInterval;
            totalHits = Mathf.RoundToInt(StaticValues.shadowClawHits * attackSpeedStat);

            Ray aimRay = base.GetAimRay();
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            radius *= attackSpeedStat;
            if (radius < StaticValues.shadowClawRadius)
            {
                radius = StaticValues.shadowClawRadius;
            }

            base.characterBody.ApplyBuff(RoR2Content.Buffs.Cloak.buffIndex, 1);
            base.characterBody.ApplyBuff(RoR2Content.Buffs.CloakSpeed.buffIndex, 1);

            PlayAnimation("RightArm, Override", "RightArmOut", "Attack.playbackRate", 1f);
            if (this.crosshairOverridePrefab)
            {
                this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, this.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
            }

            blastAttack = new BlastAttack();
            blastAttack.radius = radius;
            blastAttack.procCoefficient = procCoefficient;
            blastAttack.position = base.characterBody.corePosition;
            blastAttack.attacker = base.gameObject;
            blastAttack.crit = characterBody.RollCrit();
            blastAttack.baseDamage = damageStat * damageCoefficient;
            blastAttack.falloffModel = BlastAttack.FalloffModel.None;
            blastAttack.baseForce = 400f;
            blastAttack.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
            blastAttack.damageType = damageType;
            blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;

            DamageAPI.AddModdedDamageType(blastAttack, Damage.shiggyDecay);

            this.modelTransform = base.GetModelTransform();
            if (this.modelTransform && BlinkState.destealthMaterial)
            {
                TemporaryOverlay temporaryOverlay = this.animator.gameObject.AddComponent<TemporaryOverlay>();
                temporaryOverlay.duration = duration + fireInterval * totalHits;
                temporaryOverlay.destroyComponentOnEnd = true;
                temporaryOverlay.originalMaterial = BlinkState.destealthMaterial;
                temporaryOverlay.inspectorCharacterModel = this.animator.gameObject.GetComponent<CharacterModel>();
                temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                temporaryOverlay.animateShaderAlpha = true;

            }
            Util.PlaySound(EntityStates.Bandit2.StealthMode.enterStealthSound, base.gameObject);
        }

        public override void OnExit()
        {
            base.OnExit();

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);

            this.animator.SetBool("attacking", true);
            base.PlayCrossfade("RightArm, Override", "RArmSwipe1Start", "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }

            Util.PlaySound(EntityStates.Bandit2.StealthMode.exitStealthSound, base.gameObject);
            CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
            if (overrideRequest != null)
            {
                overrideRequest.Dispose();
            }

            if (characterBody.HasBuff(RoR2Content.Buffs.Cloak))
            {
                base.characterBody.ApplyBuff(RoR2Content.Buffs.Cloak.buffIndex, 0);
            }
            if (characterBody.HasBuff(RoR2Content.Buffs.CloakSpeed))
            {
                base.characterBody.ApplyBuff(RoR2Content.Buffs.CloakSpeed.buffIndex, 0);
            }
        }


        public override void FixedUpdate()
        {

            if (base.inputBank.skill1.down && characterBody.skillLocator.primary.skillDef == Shiggy.orbitalStrikeDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill2.down && characterBody.skillLocator.secondary.skillDef == Shiggy.orbitalStrikeDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill3.down && characterBody.skillLocator.utility.skillDef == Shiggy.orbitalStrikeDef)
            {

                keepFiring = true;
            }
            else if (base.inputBank.skill4.down && characterBody.skillLocator.special.skillDef == Shiggy.orbitalStrikeDef)
            {

                keepFiring = true;
            }
            else if (extrainputBankTest.extraSkill1.down && extraskillLocator.extraFirst.skillDef == Shiggy.orbitalStrikeDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill2.down && extraskillLocator.extraSecond.skillDef == Shiggy.orbitalStrikeDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill3.down && extraskillLocator.extraThird.skillDef == Shiggy.orbitalStrikeDef)
            {

                keepFiring = true;
            }
            if (extrainputBankTest.extraSkill4.down && extraskillLocator.extraFourth.skillDef == Shiggy.orbitalStrikeDef)
            {

                keepFiring = true;
            }
            else
            {
                keepFiring = false;
            }

            if (keepFiring)
            {
                base.characterMotor.walkSpeedPenaltyCoefficient = 1f - base.fixedAge / StaticValues.shadowClawMovespeedCharge;
                if (base.characterMotor.walkSpeedPenaltyCoefficient < 0.3f)
                {
                    base.characterMotor.walkSpeedPenaltyCoefficient = 0.3f;
                }
                //PlayAnimation("RightArm, Override", "RightArmOut", "Attack.playbackRate", duration);
            }
            if (base.fixedAge >= this.duration && base.isAuthority && !keepFiring)
            {
                if (!hasFired)
                {
                    hasFired = true;
                    this.animator.SetBool("attacking", true);
                    base.PlayCrossfade("RightArm, Override", "RArmSwipe1Release", "Attack.playbackRate", duration, 0.05f);

                }
                base.characterMotor.walkSpeedPenaltyCoefficient = 1f;
                if (stopwatch < fireInterval)
                {
                    stopwatch += Time.fixedDeltaTime;
                }
                else if (stopwatch >= fireInterval)
                {

                    if (numberOfHits < totalHits)
                    {
                        stopwatch = 0f;
                        numberOfHits++;

                        randRelPos = new Vector3((float)Random.Range(-radius, radius) / 2f, (float)Random.Range(-radius, radius) / 2f, (float)Random.Range(-radius, radius) / 2f);
                        blastAttack.position = characterBody.corePosition;
                        blastAttack.Fire();


                        EffectManager.SpawnEffect(Modules.ShiggyAsset.impBossGroundSlamEffect, new EffectData
                        {
                            origin = base.characterBody.corePosition + randRelPos,
                            scale = radius / 3f,
                            rotation = Quaternion.identity,

                        }, true);

                    }
                    else if (numberOfHits >= totalHits)
                    {
                        randRelPos = new Vector3((float)Random.Range(-radius, radius) / 2f, (float)Random.Range(-radius, radius) / 2f, (float)Random.Range(-radius, radius) / 2f);
                        blastAttack.position = characterBody.corePosition;
                        blastAttack.Fire();
                        EffectManager.SpawnEffect(Modules.ShiggyAsset.impBossGroundSlamEffect, new EffectData
                        {
                            origin = base.characterBody.corePosition + randRelPos,
                            scale = radius / 3f,
                            rotation = Quaternion.identity,

                        }, true);
                        this.outer.SetNextStateToMain();
                        return;
                    }
                }

            }
        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
