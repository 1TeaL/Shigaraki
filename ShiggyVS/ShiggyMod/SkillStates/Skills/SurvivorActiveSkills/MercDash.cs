using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using EntityStates.Merc;
using R2API.Networking;
using ShiggyMod.SkillStates.BaseStates;
using System.Collections.Generic;
using System;
using EntityStates.Treebot.Weapon;
using RoR2.Audio;
using ShiggyMod.Modules;
using R2API;

namespace ShiggyMod.SkillStates
{
    public class MercDash : BaseSkillState

    {
        public ShiggyController shiggyCon;
        protected string hitboxName = "AroundHitbox";

        protected DamageType damageType = DamageType.ApplyMercExpose;
        protected float damageCoefficient = StaticValues.mercDamageCoefficient;
        protected float procCoefficient = StaticValues.mercProcCoefficient;
        protected float pushForce = StaticValues.mercpushForce;
        protected Vector3 bonusForce = StaticValues.mercbonusForce;
        protected float baseDuration = StaticValues.mercbaseDuration;
        protected float attackStartTime = StaticValues.mercattackStartTime;
        protected float attackEndTime = StaticValues.mercattackEndTime;
        protected float hitStopDuration = StaticValues.merchitStopDuration;
        protected float attackRecoil = StaticValues.mercattackRecoil;

        protected string swingSoundString = "";
        protected string hitSoundString = "";
        protected string muzzleString = "Swing1";
        protected GameObject swingEffectPrefab = Modules.Assets.shiggySwingEffect;
        protected GameObject hitEffectPrefab = Modules.Assets.shiggyHitImpactEffect;
        protected NetworkSoundEventIndex impactSound = Modules.Assets.hitSoundEffect.index;

        public float duration;
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
        private float speedCoefficient = StaticValues.mercSpeedCoefficient;
        private float speedCoefficientOnExit = StaticValues.mercSpeedCoefficientOnExit;

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
            shiggyCon = base.gameObject.GetComponent<ShiggyController>();
            this.duration = this.baseDuration / this.attackSpeedStat;
            this.hasFired = false;
            this.animator = base.GetModelAnimator();
            base.StartAimMode(0.5f + this.duration, false);
            base.characterBody.outOfCombatStopwatch = 0f;
            this.animator.SetBool("attacking", true);
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

            //play animation here

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
                TemporaryOverlay temporaryOverlay = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
                temporaryOverlay.duration = duration;
                temporaryOverlay.animateShaderAlpha = true;
                temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                temporaryOverlay.destroyComponentOnEnd = true;
                temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matMercEnergized");
                temporaryOverlay.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
            }
            base.characterDirection.forward = base.characterMotor.velocity.normalized;
            if (NetworkServer.active)
            {
                base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
            }

            Util.PlaySound(Assaulter2.endSoundString, base.gameObject);
            shiggyCon.boolswordAuraL = true;
            
        }

        private void CreateBlinkEffect(Vector3 origin)
        {
            EffectData effectData = new EffectData();
            effectData.rotation = Util.QuaternionSafeLookRotation(this.forwardDirection);
            effectData.origin = origin;
            EffectManager.SpawnEffect(EvisDash.blinkPrefab, effectData, false);
            if (this.modelTransform)
            {
                TemporaryOverlay temporaryOverlay = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
                temporaryOverlay.duration = duration;
                temporaryOverlay.animateShaderAlpha = true;
                temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                temporaryOverlay.destroyComponentOnEnd = true;
                temporaryOverlay.originalMaterial = RoR2.LegacyResourcesAPI.Load<Material>("Materials/matHuntressFlashBright");
                temporaryOverlay.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
                TemporaryOverlay temporaryOverlay2 = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
                temporaryOverlay2.duration = 0.7f;
                temporaryOverlay2.animateShaderAlpha = true;
                temporaryOverlay2.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                temporaryOverlay2.destroyComponentOnEnd = true;
                temporaryOverlay2.originalMaterial = RoR2.LegacyResourcesAPI.Load<Material>("Materials/matHuntressFlashExpanded");
                temporaryOverlay2.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
            }
        }

        public override void OnExit()
        {
            shiggyCon.boolswordAuraL = false;
            if (!this.hasFired) this.FireAttack();

            base.OnExit();
            if (NetworkServer.active)
            {
                base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
            }
            base.characterMotor.velocity *= speedCoefficientOnExit;
            base.SmallHop(base.characterMotor, 1f);
            //Util.PlaySound(EvisDash.endSoundString, base.gameObject);
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

            if (Assaulter2.selfOnHitOverlayEffectPrefab)
            {
                EffectData effectData = new EffectData
                {
                    origin = base.transform.position,
                    genericFloat = this.hitStopDuration / this.attackSpeedStat
                    
                };
                effectData.SetNetworkedObjectReference(base.gameObject);
                EffectManager.SpawnEffect(Assaulter2.selfOnHitOverlayEffectPrefab, effectData, true);
            }
        }

        private void FireAttack()
        {
            if (!this.hasFired)
            {
                this.hasFired = true;
                Util.PlayAttackSpeedSound(this.swingSoundString, base.gameObject, this.attackSpeedStat);
                this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));

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
            }


            if (this.stopwatch >= this.duration && base.isAuthority)
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
