using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Audio;
using static RoR2.BulletAttack;

namespace ShiggyMod.SkillStates
{
    public class XiConstructBeam : BaseSkillState
    {
        public float baseDuration = 0.3f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;
        private Ray aimRay;


        private bool beamPlay;
        private float radius = 10f;
        private float range = 200f;
        private float damageCoefficient = Modules.StaticValues.xiconstructDamageCoefficient;
        private float procCoefficient = Modules.StaticValues.xiconstructProcCoefficient;
        private float force = 100f;
        private float fireTimer;
        public string muzzleString = "RHand";
        public LoopSoundDef loopSoundDef = Modules.Assets.xiconstructsound;
        private LoopSoundManager.SoundLoopPtr loopPtr;

        private GameObject beam;
        private ParticleSystem mainBeam;
        private BulletAttack attack;
        private BlastAttack blastAttack;
        private float fireInterval = 0.1f;
        private Animator animator;

        public override void OnEnter()
        {
            base.OnEnter();
            updateAimRay();
            this.duration = this.baseDuration / this.attackSpeedStat;
            base.characterBody.SetAimTimer(this.duration);
            damageType = DamageType.Generic;
            Shiggycon = gameObject.GetComponent<ShiggyController>();


            this.animator = base.GetModelAnimator();
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("RightArm, Override", "RightArmOut", "Attack.playbackRate", duration, 0.1f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }

            EffectManager.SimpleMuzzleFlash(Modules.Assets.xiconstructbeamEffect, base.gameObject, muzzleString, false);
            if (this.loopSoundDef)
            {
                this.loopPtr = LoopSoundManager.PlaySoundLoopLocal(base.gameObject, this.loopSoundDef);
            }
            this.fireTimer = 0f;

            base.characterBody.SetAimTimer(2f);

            beam = UnityEngine.Object.Instantiate(Modules.Assets.beam);
            if (NetworkServer.active)
            {
                NetworkServer.Spawn(beam);
            }
            mainBeam = beam.transform.GetChild(0).GetComponent<ParticleSystem>();
            mainBeam.Stop();
            beamPlay = false;

            attack = new BulletAttack
            {
                bulletCount = 1,
                aimVector = aimRay.direction,
                origin = FindModelChild(this.muzzleString).transform.position,
                damage = damageStat * damageCoefficient,
                damageColorIndex = DamageColorIndex.Default,
                damageType = damageType,
                falloffModel = BulletAttack.FalloffModel.None,
                maxDistance = range,
                force = 0f,
                hitMask = LayerIndex.CommonMasks.bullet,
                minSpread = 0f,
                maxSpread = 0f,
                isCrit = false,
                owner = base.gameObject,
                muzzleName = muzzleString,
                smartCollision = false,
                procChainMask = default(ProcChainMask),
                procCoefficient = procCoefficient,
                radius = 0.6f,
                sniper = false,
                stopperMask = LayerIndex.noCollision.mask,
                weapon = null,
                spreadPitchScale = 0f,
                spreadYawScale = 0f,
                queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                hitEffectPrefab = EntityStates.Commando.CommandoWeapon.FirePistol2.hitEffectPrefab,
                hitCallback = laserHitCallback
            };
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            
        }

        public bool laserHitCallback(BulletAttack bulletRef, ref BulletHit hitInfo)
        {
            var hurtbox = hitInfo.hitHurtBox;
            if (hurtbox)
            {
                var healthComponent = hurtbox.healthComponent;
                if (healthComponent)
                {
                    var body = healthComponent.body;
                    if (body)
                    {
                        Ray aimRay = base.GetAimRay();
                        EffectManager.SpawnEffect(Modules.Assets.xiconstructSecondMuzzleEffect, new EffectData
                        {
                            origin = healthComponent.body.corePosition,
                            scale = 1f,
                            rotation = Quaternion.LookRotation(aimRay.direction)

                        }, true);

                        blastAttack = new BlastAttack();
                        blastAttack.radius = radius;
                        blastAttack.procCoefficient = procCoefficient;
                        blastAttack.position = healthComponent.body.corePosition;
                        blastAttack.attacker = base.gameObject;
                        blastAttack.crit = base.RollCrit();
                        blastAttack.baseDamage = damageStat * damageCoefficient;
                        blastAttack.falloffModel = BlastAttack.FalloffModel.None;
                        blastAttack.baseForce = force;
                        blastAttack.teamIndex = TeamComponent.GetObjectTeam(blastAttack.attacker);
                        blastAttack.damageType = damageType;
                        blastAttack.attackerFiltering = AttackerFiltering.Default;

                        blastAttack.Fire();
                    }
                }
            }
            return false;
        }
        public override void Update()
        {
            base.Update();
            updateAimRay();
            base.characterDirection.forward = aimRay.direction;
            beam.transform.position = FindModelChild(this.muzzleString).transform.position;
            beam.transform.rotation = Quaternion.LookRotation(aimRay.direction);
        }
        public void updateAimRay()
        {
           aimRay = base.GetAimRay();
        }

        public override void OnExit()
        {
            this.animator.SetBool("attacking", false);
            PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            //PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            base.OnExit();
            LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
            mainBeam.Stop();
            if (NetworkServer.active)
            {
                NetworkServer.Destroy(beam);
            }
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.IsKeyDownAuthority())
            {
                if (!beamPlay)
                {
                    mainBeam.Play();
                    beamPlay = true;
                }
                fireTimer += Time.fixedDeltaTime;
                //Fire the laser
                if (fireTimer > fireInterval)
                {
                    PlayCrossfade("LeftArm, Override", "LeftArmOut", "Attack.playbackRate", fireInterval, 0.1f);
                    base.characterBody.SetAimTimer(2f);
                    attack.muzzleName = muzzleString;
                    attack.aimVector = aimRay.direction;
                    attack.origin = FindModelChild(this.muzzleString).position;
                    attack.Fire();
                    fireTimer = 0f;
                }

            }
            else
            {
                base.outer.SetNextStateToMain();
            }

        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
