using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using EntityStates.JellyfishMonster;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class JellyfishNova : BaseSkillState
    {
        public float baseDuration = 2f;
        private float fireTime;
        public float duration;
        public bool hasFired;
        public ShiggyController Shiggycon;
        private DamageType damageType;


        private float healthCostFraction = 0.5f;
        private float radius;
        private float damageCoefficient = Modules.StaticValues.jellyfishnovaDamageCoeffecient;
        private float procCoefficient = 2f;
        private float force = 1f;
        private uint soundID;
        private GameObject chargeEffect;
        private PrintController printController;
        public HurtBox Target;
        private Vector3 theSpot;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            fireTime = duration / 2;
            hasFired = false;
            radius = 30 * (attackSpeedStat/3);
            damageType = DamageType.Stun1s;
            if (base.HasBuff(Modules.Buffs.impbossBuff))
            {
                damageType |= DamageType.BleedOnHit | DamageType.Stun1s;
            }
            if (base.HasBuff(Modules.Buffs.acridBuff))
            {
                damageType |= DamageType.PoisonOnHit | DamageType.Stun1s;
            }
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);

            Shiggycon = base.GetComponent<ShiggyController>();
            if (Shiggycon && base.isAuthority)
            {
                Target = Shiggycon.GetTrackingTarget();
            }

            if (!Target)
            {
                return;
            }
            theSpot = Target.healthComponent.body.corePosition;

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("RightArm, Override", "RightArmDetonate", "Attack.playbackRate", duration, 0.1f);
            Transform modelTransform = base.GetModelTransform();
            AkSoundEngine.PostEvent(2085946697, base.gameObject);
            this.soundID = Util.PlaySound(JellyNova.chargingSoundString, base.gameObject);
            if (JellyNova.chargingEffectPrefab)
            {
                this.chargeEffect = UnityEngine.Object.Instantiate<GameObject>(JellyNova.chargingEffectPrefab, theSpot, base.transform.rotation);
                this.chargeEffect.transform.parent = Target.transform;
                this.chargeEffect.transform.localScale = new Vector3(radius, radius, radius);
                this.chargeEffect.GetComponent<ScaleParticleSystemDuration>().newDuration = fireTime;
            }

            if (modelTransform)
            {
                this.printController = modelTransform.GetComponent<PrintController>();
                if (this.printController)
                {
                    this.printController.enabled = true;
                    this.printController.printTime = this.duration;
                }
            }
            damageCoefficient *= Shiggycon.rangedMultiplier;

        }

        public override void OnExit()
        {
            base.OnExit();
            PlayCrossfade("RightArm, Override", "Empty", "Attack.playbackRate", 0.1f, 0.1f);
            PlayCrossfade("LeftArm, Override", "Empty", "Attack.playbackRate", 0.1f, 0.1f);
            AkSoundEngine.StopPlayingID(this.soundID);
            if (this.chargeEffect)
            {
                EntityState.Destroy(this.chargeEffect);
            }
            if (this.printController)
            {
                this.printController.enabled = false;
            }
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if(base.fixedAge >= fireTime && !hasFired && Target)
            {
                hasFired = true;
                Detonate();
            }

            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        private void Detonate()
        {
            this.hasFired = true;
            Util.PlaySound(JellyNova.novaSoundString, base.gameObject);
            if (this.chargeEffect)
            {
                EntityState.Destroy(this.chargeEffect);
            }
            if (JellyNova.novaEffectPrefab)
            {
                EffectManager.SpawnEffect(JellyNova.novaEffectPrefab, new EffectData
                {
                    origin = theSpot,
                    scale = radius
                }, true);
            }
            new BlastAttack
            {
                attacker = base.gameObject,
                teamIndex = TeamComponent.GetObjectTeam(base.gameObject),
                falloffModel = BlastAttack.FalloffModel.SweetSpot,
                baseDamage = this.damageStat * damageCoefficient,
                damageType = damageType,
                damageColorIndex = DamageColorIndex.Fragile,
                baseForce = force,
                position = theSpot,
                radius = radius,
                procCoefficient = procCoefficient,
                attackerFiltering = AttackerFiltering.AlwaysHitSelf,
            }.Fire();
        }


        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Pain;
        }

    }
}
