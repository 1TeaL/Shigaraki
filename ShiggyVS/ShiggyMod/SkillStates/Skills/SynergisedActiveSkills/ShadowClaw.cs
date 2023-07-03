using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using RoR2.UI;
using R2API.Networking;
using ShiggyMod.Modules;
using System;
using Random = UnityEngine.Random;
using EntityStates.ImpMonster;
using R2API;

namespace ShiggyMod.SkillStates
{
    //imp + bandit
    public class ShadowClaw : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float baseDuration = 0.5f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType = DamageType.BonusToLowHealth | DamageType.ResetCooldownsOnKill;
        public HurtBox Target;
        private Animator animator;
        private int numberOfHits = 1;
        private int totalHits;
        private float fireInterval;
        private float stopwatch;


        private float radius = StaticValues.shadowClawRadius;
        private float damageCoefficient = StaticValues.shadowClawDamageCoefficient;
        private float procCoefficient = StaticValues.shadowClawProcCoefficient;
        private CrosshairUtils.OverrideRequest crosshairOverrideRequest;
        public GameObject crosshairOverridePrefab = Assets.banditCrosshair;
        private BlastAttack blastAttack;
        private Vector3 randRelPos;
        private Transform modelTransform;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            fireInterval = duration * StaticValues.shadowClawInterval;
            totalHits = Mathf.RoundToInt(StaticValues.shadowClawHits * attackSpeedStat);

            Ray aimRay = base.GetAimRay();
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            Shiggycon = gameObject.GetComponent<ShiggyController>();

            radius *= attackSpeedStat;
            if(radius < StaticValues.shadowClawRadius)
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
            int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
            //base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
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
            base.FixedUpdate();
            if (base.IsKeyDownAuthority())
            {
                base.characterMotor.walkSpeedPenaltyCoefficient = 1f - base.fixedAge/StaticValues.shadowClawMovespeedCharge;
                if(base.characterMotor.walkSpeedPenaltyCoefficient < 0.3f)
                {
                    base.characterMotor.walkSpeedPenaltyCoefficient = 0.3f;
                }
                //PlayAnimation("RightArm, Override", "RightArmOut", "Attack.playbackRate", duration);
            }
            if (base.fixedAge >= this.duration && base.isAuthority && !base.IsKeyDownAuthority())
            {

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


                        EffectManager.SpawnEffect(Assets.impBossGroundSlamEffect, new EffectData
                        {
                            origin = base.characterBody.corePosition + randRelPos,
                            scale = radius/3f,
                            rotation = Quaternion.identity,

                        }, true);

                    }
                    else if (numberOfHits >= totalHits)
                    {
                        randRelPos = new Vector3((float)Random.Range(-radius, radius) / 2f, (float)Random.Range(-radius, radius) / 2f, (float)Random.Range(-radius, radius) / 2f);
                        blastAttack.position = characterBody.corePosition;
                        blastAttack.Fire();
                        EffectManager.SpawnEffect(Assets.impBossGroundSlamEffect, new EffectData
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
