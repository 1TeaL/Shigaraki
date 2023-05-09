using RoR2;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;
using EntityStates;
using ShiggyMod.Modules.Survivors;
using System.Collections.Generic;
using EntityStates.Huntress;
using R2API.Networking;
using R2API;
using static UnityEngine.ParticleSystem.PlaybackState;
using ShiggyMod.Modules;

namespace ShiggyMod.SkillStates
{
    public class BeetleGuardSlam : BaseSkillState
    {
        public static float dropForce = 80f;
        public  float dropTimer;
        private ShiggyController Shiggycon;
        public static float slamRadius = 10f;
        public static float slamProcCoefficient = 1f;
        public static float slamForce = 1000f;
        private float damageCoefficient = Modules.StaticValues.beetleguardSlamDamageCoefficient;

        private bool hasDropped;
        private Vector3 flyVector = Vector3.zero;
        private Transform modelTransform;
        private GameObject slamIndicatorInstance;
        private Ray downRay;

        protected DamageType damageType;
        public ShiggyController shiggycon;
        private Vector3 theSpot;

        //private NemforcerGrabController grabController;

        public override void OnEnter()
        {
            base.OnEnter();
            this.modelTransform = base.GetModelTransform();
            this.flyVector = Vector3.up;
            damageType = DamageType.Stun1s;

            dropTimer = 1f;


            Shiggycon = gameObject.GetComponent<ShiggyController>();
            


            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            //PlayCrossfade("Fullbody, Override", "Slam", "Attack.playbackRate", 1f, 0.1f);


            base.characterBody.bodyFlags |= CharacterBody.BodyFlags.IgnoreFallDamage;
            base.characterMotor.Motor.ForceUnground();
            base.characterMotor.velocity = Vector3.zero;

            //base.gameObject.layer = LayerIndex.fakeActor.intVal;
            base.characterMotor.Motor.RebuildCollidableLayers();
        }


        public override void Update()
        {
            base.Update();

            if (this.slamIndicatorInstance) this.UpdateSlamIndicator();
        }
        protected virtual void OnHitEnemyAuthority()
        {
            base.healthComponent.AddBarrierAuthority((healthComponent.fullCombinedHealth / 20) * (this.moveSpeedStat / 7) * dropTimer);
            

        }
        protected virtual void OnHitEnemyAuthority(BlastAttack.Result result)
        {
            foreach (BlastAttack.HitPoint hitpoint in result.hitPoints)
            {
                //gain barrier per hit
                base.healthComponent.AddBarrierAuthority((healthComponent.fullCombinedHealth * StaticValues.beetleguardSlamBarrierCoefficient) * (this.moveSpeedStat / 7) * dropTimer);

            }

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            dropTimer += Time.fixedDeltaTime / 10;
            if (!this.hasDropped)
            {
                this.StartDrop();
            }

            if (!this.slamIndicatorInstance)
            {
                this.CreateIndicator();
            }
            if (this.slamIndicatorInstance)
            {
                this.UpdateSlamIndicator();
            }

            if (this.hasDropped && base.isAuthority && !base.characterMotor.disableAirControlUntilCollision)
            {
                this.LandingImpact();
                this.outer.SetNextStateToMain();
            }

        }

        private void StartDrop()
        {
            this.hasDropped = true;

            base.characterMotor.disableAirControlUntilCollision = true;
            base.characterMotor.velocity.y = -dropForce;

            //base.PlayAnimation("Fullbody, Override", "ManchesterSmashExit", "Attack.playbackRate", jumpDuration / 3f);
            bool active = NetworkServer.active;
            if (active)
            {
                base.characterBody.ApplyBuff(RoR2Content.Buffs.HiddenInvincibility.buffIndex, 1);
            }

        }

        private void CreateIndicator()
        {
            if (EntityStates.Huntress.ArrowRain.areaIndicatorPrefab)
            {
                this.slamIndicatorInstance = Object.Instantiate<GameObject>(ArrowRain.areaIndicatorPrefab);
                this.slamIndicatorInstance.SetActive(true);

            }
        }
        private void UpdateSlamIndicator()
        {
            if (this.slamIndicatorInstance)
            {
                this.slamIndicatorInstance.transform.localScale = Vector3.one * slamRadius * dropTimer;
                this.slamIndicatorInstance.transform.localPosition = base.transform.position;
            }
        }
        private void LandingImpact()
        {

            if (base.isAuthority)
            {
                AkSoundEngine.PostEvent("ShiggyMelee", base.gameObject);
                Ray aimRay = base.GetAimRay();
                
                base.characterMotor.velocity *= 0.1f;

                BlastAttack blastAttack = new BlastAttack();
                blastAttack.radius = slamRadius * dropTimer;
                blastAttack.procCoefficient = slamProcCoefficient;
                blastAttack.position = base.characterBody.footPosition;
                blastAttack.attacker = base.gameObject;
                blastAttack.crit = base.RollCrit();
                blastAttack.baseDamage = base.characterBody.damage * damageCoefficient * (moveSpeedStat / 7) * dropTimer;
                blastAttack.falloffModel = BlastAttack.FalloffModel.None;
                blastAttack.baseForce = slamForce;
                blastAttack.teamIndex = base.teamComponent.teamIndex;
                blastAttack.damageType = damageType;
                blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
                blastAttack.AddModdedDamageType(Modules.Damage.shiggyDecay);

                BlastAttack.Result result = blastAttack.Fire();
                if (result.hitCount > 0)
                {
                    this.OnHitEnemyAuthority(result);
                }

                EffectManager.SpawnEffect(EntityStates.BeetleGuardMonster.GroundSlam.slamEffectPrefab, new EffectData
                {
                    origin = base.characterBody.footPosition,
                    scale = slamRadius * dropTimer,
                }, true);


            }
        }


        public override void OnExit()
        {

            this.PlayAnimation("Fullbody, Override", "BufferEmpty");
            if (this.slamIndicatorInstance)
                this.slamIndicatorInstance.SetActive(false);
            EntityState.Destroy(this.slamIndicatorInstance);



            base.characterBody.bodyFlags &= ~CharacterBody.BodyFlags.IgnoreFallDamage;


            if (NetworkServer.active && base.characterBody.HasBuff(RoR2Content.Buffs.HiddenInvincibility)) base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);

            base.gameObject.layer = LayerIndex.defaultLayer.intVal;
            base.characterMotor.Motor.RebuildCollidableLayers();
            base.OnExit();
        }



        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}