using EntityStates;
using EntityStates.Merc;
using R2API;
using RoR2;
using RoR2.Audio;
using ShiggyMod.Modules;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class Thunderclap : BaseSkillState

    {
        //bison + overloading worm

        protected string hitboxName = "AroundHitbox";

        protected DamageType damageType = new DamageTypeCombo(DamageType.Shock5s, DamageTypeExtended.Generic, DamageSource.Secondary);
        protected float damageCoefficient = StaticValues.thunderclapDamageCoefficient;
        protected float procCoefficient = StaticValues.thunderclapprocCoefficient;
        protected float pushForce = StaticValues.thunderclappushForce;
        protected Vector3 bonusForce = StaticValues.thunderclapbonusForce;
        protected float baseDuration = StaticValues.thunderclapbaseDuration;
        protected float attackStartTime = StaticValues.thunderclapattackStartTime;
        protected float attackEndTime = StaticValues.thunderclapattackEndTime;
        protected float hitStopDuration = StaticValues.thunderclaphitStopDuration;
        protected float attackRecoil = StaticValues.thunderclapattackRecoil;
        protected float radius = StaticValues.thunderclapRadius;

        protected string swingSoundString = "";
        protected string hitSoundString = "";
        protected string muzzleString = "Swing2";
        protected GameObject swingEffectPrefab = Modules.ShiggyAsset.shiggySwingEffect;
        protected GameObject hitEffectPrefab = Modules.ShiggyAsset.shiggyHitImpactEffect;
        protected NetworkSoundEventIndex impactSound = Modules.ShiggyAsset.hitSoundEffect.index;

        private float preDashTimer;
        public float duration;
        public float preDashDuration;
        private bool hasFired;
        private float hitPauseTimer;
        private OverlapAttack attack;
        protected bool inHitPause;
        protected float stopwatch;
        protected Animator animator;
        private BaseState.HitStopCachedState hitStopCachedState;
        private Vector3 storedVelocity;
        private Vector3 forwardDirection;

        //movement
        private Transform modelTransform;
        private Vector3 dashVector;
        private float speedCoefficient = StaticValues.thunderclapSpeedCoefficient;
        private float speedCoefficientOnExit = StaticValues.thunderclapSpeedCoefficientOnExit;
        private BlastAttack blastAttack;

        private Vector3 dashVelocity
        {
            get
            {
                return this.dashVector * this.moveSpeedStat * speedCoefficient;
            }
        }

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            this.preDashDuration = StaticValues.thunderclapbasePreDashDuration / this.attackSpeedStat;
            this.hasFired = false;
            this.animator = base.GetModelAnimator();
            base.StartAimMode(0.5f + this.duration, false);
            base.characterBody.outOfCombatStopwatch = 0f;
            this.animator.SetBool("attacking", false);

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            base.PlayCrossfade("FullBody, Override", "FullBodyDash", "Attack.playbackRate", duration, 0.05f);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }

            HitBoxGroup hitBoxGroup = null;
            Transform modelTransform = base.GetModelTransform();

            if (modelTransform)
            {
                hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == this.hitboxName);
            }
            if (base.isAuthority && base.inputBank && base.characterDirection)
            {
                this.forwardDirection = ((base.inputBank.moveVector == Vector3.zero) ? base.characterDirection.forward : base.inputBank.moveVector).normalized;
            }

            //play animation

            this.attack = new OverlapAttack();
            this.attack.damageType = this.damageType;
            this.attack.attacker = base.gameObject;
            this.attack.inflictor = base.gameObject;
            this.attack.teamIndex = base.GetTeam();
            this.attack.damage = this.damageCoefficient * this.damageStat;
            this.attack.procCoefficient = this.procCoefficient;
            this.attack.hitEffectPrefab = this.hitEffectPrefab;
            this.attack.forceVector = this.bonusForce;
            this.attack.pushAwayForce = this.pushForce;
            this.attack.hitBoxGroup = hitBoxGroup;
            this.attack.isCrit = base.RollCrit();
            this.attack.impactSound = this.impactSound;
            //movement code

            this.dashVector = base.inputBank.aimDirection;
            base.gameObject.layer = LayerIndex.fakeActor.intVal;
            base.characterMotor.Motor.RebuildCollidableLayers();
            base.characterMotor.Motor.ForceUnground();
            base.characterMotor.velocity = Vector3.zero;
            this.modelTransform = base.GetModelTransform();
            if (this.modelTransform)
            {
                TemporaryOverlayInstance temporaryOverlay = TemporaryOverlayManager.AddOverlay(modelTransform.gameObject);
                temporaryOverlay.duration = duration * 3;
                temporaryOverlay.animateShaderAlpha = true;
                temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                temporaryOverlay.destroyComponentOnEnd = true;
                temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matVagrantEnergized");
                temporaryOverlay.AddToCharacterModel(modelTransform.GetComponent<CharacterModel>());
            }
            base.characterDirection.forward = base.characterMotor.velocity.normalized;
            if (NetworkServer.active)
            {
                base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
            }


            //blast attack for end 
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

            Util.PlaySound(EvisDash.endSoundString, base.gameObject);
            this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
        }

        private void CreateBlinkEffect(Vector3 origin)
        {
            EffectData effectData = new EffectData();
            effectData.rotation = Util.QuaternionSafeLookRotation(this.forwardDirection);
            effectData.origin = origin;
            EffectManager.SpawnEffect(EvisDash.blinkPrefab, effectData, false);
        }

        public override void OnExit()
        {
            if (!this.hasFired) this.FireAttack();

            base.OnExit();
            if (NetworkServer.active)
            {
                base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
            }
            //blast attack at the end
            blastAttack.position = characterBody.corePosition;
            blastAttack.Fire();
            EffectManager.SpawnEffect(Modules.ShiggyAsset.lightningNovaEffectPrefab, new EffectData
            {
                origin = characterBody.corePosition,
                scale = radius,
                rotation = Quaternion.identity
            }, true);

            base.characterMotor.velocity *= speedCoefficientOnExit;
            Util.PlaySound(Assaulter2.endSoundString, base.gameObject);
            //this.PlayAnimation("FullBody, Override", "EvisLoopExit");
            base.gameObject.layer = LayerIndex.defaultLayer.intVal;
            base.characterMotor.Motor.RebuildCollidableLayers();



            this.animator.SetBool("attacking", false);
        }

        protected virtual void PlaySwingEffect()
        {
            EffectManager.SimpleMuzzleFlash(this.swingEffectPrefab, base.gameObject, this.muzzleString, true);
        }

        protected virtual void OnHitEnemyAuthority()
        {
            Util.PlaySound(this.hitSoundString, base.gameObject);


            if (!this.inHitPause && this.hitStopDuration > 0f)
            {
                this.storedVelocity = base.characterMotor.velocity;
                this.hitStopCachedState = base.CreateHitStopCachedState(base.characterMotor, this.animator, "Slash.playbackRate");
                this.hitPauseTimer = this.hitStopDuration / this.attackSpeedStat;
                this.inHitPause = true;
            }

            if (Modules.ShiggyAsset.overloadingEliteEffect)
            {
                EffectData effectData = new EffectData
                {
                    origin = base.transform.position,
                    genericFloat = this.hitStopDuration / this.attackSpeedStat

                };
                effectData.SetNetworkedObjectReference(base.gameObject);
                EffectManager.SpawnEffect(Modules.ShiggyAsset.overloadingEliteEffect, effectData, true); ;
            }
        }

        private void FireAttack()
        {
            if (!this.hasFired)
            {
                this.hasFired = true;
                Util.PlayAttackSpeedSound(this.swingSoundString, base.gameObject, this.attackSpeedStat);

                if (base.isAuthority)
                {
                    this.PlaySwingEffect();
                    base.AddRecoil(-1f * this.attackRecoil, -2f * this.attackRecoil, -0.5f * this.attackRecoil, 0.5f * this.attackRecoil);
                }
            }

            if (base.isAuthority)
            {
                if (this.attack.Fire())
                {
                    this.OnHitEnemyAuthority();
                }
            }
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            //stay still, move after prep
            if (preDashTimer < preDashDuration * 1f)
            {
                preDashTimer += Time.fixedDeltaTime;
                characterBody.characterMotor.velocity.y = 0f;
                //play prestance anim here
            }
            else if (preDashTimer >= preDashDuration * 1f)
            {
                if (!animator.GetBool("attacking"))
                {
                    this.animator.SetBool("attacking", true);
                }

                this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
                if (this.modelTransform)
                {
                    TemporaryOverlayInstance temporaryOverlay = TemporaryOverlayManager.AddOverlay(new GameObject());
                    temporaryOverlay.duration = duration;
                    temporaryOverlay.animateShaderAlpha = true;
                    temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                    temporaryOverlay.destroyComponentOnEnd = true;
                    temporaryOverlay.originalMaterial = RoR2.LegacyResourcesAPI.Load<Material>("Materials/matIsShocked");
                }

                this.hitPauseTimer -= Time.fixedDeltaTime;

                if (this.hitPauseTimer <= 0f && this.inHitPause)
                {
                    base.ConsumeHitStopCachedState(this.hitStopCachedState, base.characterMotor, this.animator);
                    this.inHitPause = false;
                    base.characterMotor.velocity = this.storedVelocity;
                }

                if (!this.inHitPause)
                {
                    this.stopwatch += Time.fixedDeltaTime;
                    //keep moving if not in hitpause
                    base.characterBody.isSprinting = true;
                    base.characterMotor.rootMotion += this.dashVelocity * Time.fixedDeltaTime;
                    base.characterDirection.forward = this.dashVelocity;
                    base.characterDirection.moveVector = this.dashVelocity;
                }
                else
                {
                    if (base.characterMotor) base.characterMotor.velocity = Vector3.zero;
                    if (this.animator) this.animator.SetFloat("Swing.playbackRate", 0f);
                }

                if (this.stopwatch >= (this.duration * this.attackStartTime) && this.stopwatch <= (this.duration * this.attackEndTime))
                {
                    this.FireAttack();

                    if (animator.GetBool("attacking"))
                    {
                        this.animator.SetBool("attacking", false);
                    }
                }


                if (this.stopwatch >= this.duration && base.isAuthority)
                {
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
