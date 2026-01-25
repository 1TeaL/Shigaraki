using System;
using System.Collections.Generic;
using EntityStates;
using RoR2.Skills;
using EntityStates.Merc;
using EntityStates.Treebot.Weapon;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using R2API.Networking;
using ShiggyMod.Modules;

namespace ShiggyMod.SkillStates
{

    public class SolusExtractorExtract : Skill

    {
        public float previousMass;
        private Ray aimRay;
        private Vector3 aimRayDir;
        private Transform modelTransform;
        private CharacterModel characterModel;

        public static float hitExtraDuration = 0.44f;
        public static float minExtraDuration = 0.2f;
        public static float initialSpeedCoefficient = StaticValues.solusExtractorLungeSpeedCoefficient;
        public static float SpeedCoefficient;
        public static float finalSpeedCoefficient;
        public static float bounceForce = 4000f;
        private Vector3 bounceVector;
        private float stopwatch;
        private OverlapAttack detector;
        private OverlapAttack attack;
        protected string hitboxName2 = "SmallHitbox";
        protected string hitboxName = "SmallHitbox";
        protected float procCoefficient = 1f;
        protected float pushForce = 500f;
        protected Vector3 bonusForce = new Vector3(10f, 400f, 0f);
        protected float attackStartTime = 0.2f;
        protected float attackEndTime = 0.4f;
        protected float baseEarlyExitTime = 0.4f;
        protected float hitStopDuration = 0.15f;
        protected float attackRecoil = 0.75f;
        protected float hitHopVelocity = 250f;
        private float hitPauseTimer;
        protected bool inHitPause;
        private HitStopCachedState hitStopCachedState;
        private Vector3 storedVelocity;
        //public static GameObject muzzleEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/MuzzleFlashes/MuzzleflashMageLightningLarge");
        //public static GameObject muzzleEffectPrefab2 = RoR2.LegacyResourcesAPI.Load<GameObject>("prefabs/effects/BoostJumpEffect");
        protected GameObject swingEffectPrefab;
        protected GameObject hitEffectPrefab;
        public static string dodgeSoundString = "HenryRoll";
        public static float dodgeFOV = 82f;
        private float extraDuration;
        private float rollSpeed;
        private Vector3 forwardDirection;
        private Vector3 direction;
        private bool hasHopped;
        public float radius = 15f;
        private GameObject explosionPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/MageLightningBombExplosion");

        private BlastAttack blastAttack;



        public override void OnEnter()
        {
            base.OnEnter();
            aimRayDir = aimRay.direction;
            baseDuration = StaticValues.solusExtractorLungeDuration;
            duration = baseDuration;
            SpeedCoefficient = initialSpeedCoefficient * (attackSpeedStat / 2);
            finalSpeedCoefficient = SpeedCoefficient * 0.8f;
            float num = moveSpeedStat;
            bool isSprinting = characterBody.isSprinting;
            if (isSprinting)
            {
                num /= characterBody.sprintingSpeedMultiplier;
            }
            float num2 = (num / characterBody.baseMoveSpeed - 1f) * 0.67f;
            float num3 = num2 + 1f;
            extraDuration = Math.Max(hitExtraDuration / num3, minExtraDuration);


            characterBody.AddTimedBuffAuthority(RoR2Content.Buffs.HiddenInvincibility.buffIndex, duration / 2);
            animator = GetModelAnimator();
            animator.SetBool("attacking", true);
            characterBody.SetAimTimer(duration);
            HitBoxGroup hitBoxGroup = null;
            HitBoxGroup hitBoxGroup2 = null;
            Transform modelTransform = GetModelTransform();
            bool flag = modelTransform.gameObject.GetComponent<AimAnimator>();
            if (flag)
            {
                modelTransform.gameObject.GetComponent<AimAnimator>().enabled = false;
            }
            this.modelTransform = GetModelTransform();
            if (this.modelTransform)
            {
                animator = this.modelTransform.GetComponent<Animator>();
                characterModel = this.modelTransform.GetComponent<CharacterModel>();
            }

            //EffectManager.SimpleMuzzleFlash(ShootStyleKick.muzzleEffectPrefab, base.gameObject, "LFoot", false);
            EffectManager.SimpleMuzzleFlash(EvisDash.blinkPrefab, gameObject, "RFoot", false);


            bool flag2 = modelTransform;
            if (flag2)
            {
                hitBoxGroup = Array.Find(modelTransform.GetComponents<HitBoxGroup>(), (element) => element.groupName == hitboxName);
                hitBoxGroup2 = Array.Find(modelTransform.GetComponents<HitBoxGroup>(), (element) => element.groupName == hitboxName2);
            }


            ChargeSonicBoom chargeSonicBoom = new ChargeSonicBoom();
            Util.PlaySound(chargeSonicBoom.sound, gameObject);
            attack = new OverlapAttack();
            attack.damageType = damageType;
            attack.attacker = gameObject;
            attack.inflictor = gameObject;
            attack.teamIndex = GetTeam();
            attack.damage = characterBody.damage * Modules.StaticValues.solusExtractorLungeDamageCoefficient * num3;
            attack.procCoefficient = procCoefficient;
            attack.hitEffectPrefab = Modules.ShiggyAsset.solusExtractorImpactPrefab;
            attack.forceVector = bonusForce;
            attack.pushAwayForce = pushForce;
            attack.hitBoxGroup = hitBoxGroup;
            attack.isCrit = RollCrit();
            attack.impactSound = Modules.ShiggyAsset.hitSoundEffect.index;


            detector = new OverlapAttack();
            detector.damageType = damageType;
            detector.attacker = gameObject;
            detector.inflictor = gameObject;
            detector.teamIndex = GetTeam();
            detector.damage = 0f;
            detector.procCoefficient = 0f;
            detector.hitEffectPrefab = null;
            detector.forceVector = Vector3.zero;
            detector.pushAwayForce = 0f;
            detector.hitBoxGroup = hitBoxGroup2;
            detector.isCrit = false;
            direction = GetAimRay().direction.normalized;
            characterDirection.forward = characterMotor.velocity.normalized;

            GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            animator.SetBool("attacking", true);
            GetModelAnimator().SetBool("FullBodyDashSlashStart", false);

            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }
            Util.PlaySound(Assaulter2.endSoundString, base.gameObject);


        }

        private void RecalculateRollSpeed()
        {
            float num = moveSpeedStat;
            bool isSprinting = characterBody.isSprinting;
            if (isSprinting)
            {
                num /= characterBody.sprintingSpeedMultiplier;
            }
            rollSpeed = num * Mathf.Lerp(SpeedCoefficient, finalSpeedCoefficient, fixedAge / duration);
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();
            RecalculateRollSpeed();

            if (modelTransform)
            {
                TemporaryOverlayInstance temporaryOverlay = TemporaryOverlayManager.AddOverlay(this.modelTransform.gameObject);
                temporaryOverlay.duration = 0.6f;
                temporaryOverlay.animateShaderAlpha = true;
                temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                temporaryOverlay.destroyComponentOnEnd = true;
                temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matHuntressFlashBright");
                temporaryOverlay.AddToCharacterModel(this.modelTransform.GetComponent<CharacterModel>());
                TemporaryOverlayInstance temporaryOverlay2 = TemporaryOverlayManager.AddOverlay(this.modelTransform.gameObject);
                temporaryOverlay2.duration = 0.7f;
                temporaryOverlay2.animateShaderAlpha = true;
                temporaryOverlay2.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                temporaryOverlay2.destroyComponentOnEnd = true;
                temporaryOverlay2.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matHuntressFlashExpanded");
                temporaryOverlay2.AddToCharacterModel(this.modelTransform.GetComponent<CharacterModel>());
            }
            FireAttack();
            hitPauseTimer -= Time.fixedDeltaTime;
            bool flag = hitPauseTimer <= 0f && inHitPause;
            if (flag)
            {
                ConsumeHitStopCachedState(hitStopCachedState, characterMotor, animator);
                inHitPause = false;
                characterMotor.velocity = Vector3.zero;
                characterMotor.ApplyForce(bounceVector, true, false);
                FireSonicBoom fireSonicBoom = new FireSonicBoom();
                Util.PlaySound(fireSonicBoom.sound, gameObject);
                attack.Fire(null);
            }
            bool flag2 = !inHitPause;
            if (flag2)
            {
                stopwatch += Time.fixedDeltaTime;
            }
            else
            {
                characterDirection.forward = bounceVector * -1f;
                if (characterMotor)
                {
                    characterMotor.velocity = Vector3.zero;
                }
                if (animator)
                {
                    animator.SetFloat("Attack.playbackRate", 0f);
                }
            }
            if (!hasHopped)
            {
                characterDirection.forward = direction;
                characterMotor.velocity = Vector3.zero;
                characterMotor.rootMotion += direction * rollSpeed * Time.fixedDeltaTime;
            }
            if (isAuthority && stopwatch >= duration)
            {
                outer.SetNextStateToMain();
            }

        }

        public override void OnExit()
        {
            animator.SetBool("lungeHit", false);
            animator.SetBool("attacking", false);
            Transform modelTransform = GetModelTransform();
            bool flag = modelTransform.gameObject.GetComponent<AimAnimator>();
            if (flag)
            {
                modelTransform.gameObject.GetComponent<AimAnimator>().enabled = false;
            }
            //characterMotor.velocity /= 1.75f;
            

            base.OnExit();
        }

        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.Write(forwardDirection);
        }


        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            forwardDirection = reader.ReadVector3();
        }

        private void FireAttack()
        {
            bool isAuthority = base.isAuthority;
            if (isAuthority)
            {
                List<HurtBox> list = new List<HurtBox>();
                bool flag = detector.Fire(list);
                if (flag)
                {
                    foreach (HurtBox hurtBox in list)
                    {
                        bool flag2 = hurtBox.healthComponent && hurtBox.healthComponent.body;
                        if (flag2)
                        {
                            ForceFlinch(hurtBox.healthComponent.body);

                            int stacks = hurtBox.healthComponent.body.GetBuffCount(Buffs.solusPrimedDebuff);

                            if (hurtBox.healthComponent.body.HasBuff(Buffs.solusPrimedDebuff))
                            {
                                characterBody.skillLocator.DeductCooldownFromAllSkillsAuthority(StaticValues.solusExtractorLungeCooldownReduction * stacks);
                            }
                            else if (!hurtBox.healthComponent.body.HasBuff(Buffs.solusPrimedDebuff))
                            {
                                hurtBox.healthComponent.body.ApplyBuff(Buffs.solusPrimedDebuff.buffIndex, stacks + 1);
                            }

                        }
                    }
                    OnHitEnemyAuthority();
                    //if (dekucon.isMaxPower)
                    //{
                    //	blastAttack.Fire();
                    //	EffectManager.SpawnEffect(this.explosionPrefab, new EffectData
                    //	{
                    //		origin = base.characterBody.corePosition,
                    //		scale = this.radius,
                    //	}, false);
                    //}
                }
            }
        }

        protected virtual void OnHitEnemyAuthority()
        {

            animator.SetBool("attacking", false);
            animator.SetBool("lungeHit", true);
            characterBody.SetAimTimer(2f);

            //if(characterMotor.isGrounded)
            //{
            //    PlayCrossfade("FullBody, Override", "SanDiegoSmashAir", "Attack.playbackRate", duration, 0.1f);
            //}
            //else
            //{

            //}
            //base.skillLocator.primary.UnsetSkillOverride(base.skillLocator.primary, ShootStyleKick45.primaryDef, GenericSkill.SkillOverridePriority.Contextual);
            //base.skillLocator.primary.SetSkillOverride(base.skillLocator.primary, ShootStyleKick452.primaryDef, GenericSkill.SkillOverridePriority.Contextual);

            //AkSoundEngine.PostEvent("impactsfx", this.gameObject);

            //check inputs
            //if (IsKeyDownAuthority())
            //{
            //    if (inputBank.skill1.down)
            //    {
            //        skillLocator.primary.AddOneStock();
            //    }
            //    else
            //    if (inputBank.skill2.down)
            //    {
            //        skillLocator.secondary.AddOneStock();
            //    }
            //    else if (inputBank.skill3.down)
            //    {
            //        skillLocator.utility.AddOneStock();
            //    }

            //}
            bool flag = !hasHopped;
            if (flag)
            {

                stopwatch = duration - extraDuration;
                bool flag2 = cameraTargetParams;
                if (flag2)
                {
                    cameraTargetParams.fovOverride = -1f;
                }
                EffectManager.SimpleMuzzleFlash(ShiggyAsset.solusExtractorMuzzlePrefab, gameObject, "RHand", false);

                bool flag3 = characterMotor;
                if (flag3)
                {
                    characterMotor.velocity = Vector3.zero;
                    bounceVector = GetAimRay().direction * -1f;
                    bounceVector.y = 0.2f;
                    bounceVector *= bounceForce;
                }
                bool flag4 = !inHitPause && hitStopDuration > 0f;
                if (flag4)
                {
                    storedVelocity = characterMotor.velocity;
                    hitStopCachedState = CreateHitStopCachedState(characterMotor, animator, "Attack.playbackRate");
                    float num = moveSpeedStat;
                    bool isSprinting = characterBody.isSprinting;
                    if (isSprinting)
                    {
                        num /= characterBody.sprintingSpeedMultiplier;
                    }
                    float num2 = 1f + (num / characterBody.baseMoveSpeed - 1f);
                    hitPauseTimer = hitStopDuration / num2;
                    inHitPause = true;
                }
                hasHopped = true;
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Pain;
        }
        protected virtual void ForceFlinch(CharacterBody body)
        {

            SetStateOnHurt component = body.healthComponent.GetComponent<SetStateOnHurt>();
            bool flag = component == null;
            if (!flag)
            {
                bool canBeHitStunned = component.canBeHitStunned;
                if (canBeHitStunned)
                {
                    component.SetPain();
                    if (body.characterMotor)
                    {
                        body.characterMotor.velocity = Vector3.zero;
                    }
                    if(body.rigidbody != null)
                    {
                        body.rigidbody.velocity = Vector3.zero;
                    }
                }
            }
        }


    }
}
