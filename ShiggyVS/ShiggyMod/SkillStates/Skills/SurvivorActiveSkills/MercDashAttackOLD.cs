using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using EntityStates.Merc;
using System.Linq;
using R2API.Networking;

namespace ShiggyMod.SkillStates
{
    public class MercDashAttack : BaseSkillState
    {
        string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
        public float duration = 1.2f;
        public ShiggyController Shiggycon;
        private DamageType damageType = DamageType.Generic;
        public HurtBox Target;
        private Animator animator;


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.mercDamageCoefficient;
        private float procCoefficient = Modules.StaticValues.mercProcCoefficient;

        private Transform modelTransform;
        public static GameObject blinkPrefab;
        public float basedamageFrequency = 7f;
        public float damageFrequency;
        public static string beginSoundString;
        public static string endSoundString;
        public static GameObject hitEffectPrefab;
        public static string slashSoundString;
        public static string impactSoundString;
        public static string dashSoundString;
        public static float slashPitch;
        public static float smallHopVelocity = 0.5f;
        public static float lingeringInvincibilityDuration = 0.6f;
        private CharacterModel characterModel;
        private float stopwatch;
        private float actualstopwatch;
        private float totalstopwatch;
        private float attackStopwatch;
        private bool crit;
        public static float minimumDuration = 0.5f;
        private CameraTargetParams.AimRequest aimRequest;
        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            Shiggycon = gameObject.GetComponent<ShiggyController>();
            this.stopwatch = 0;
            actualstopwatch = 0;
            totalstopwatch = 0;
            this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
            this.crit = Util.CheckRoll(this.critStat, base.characterBody.master);
            this.modelTransform = base.GetModelTransform();
            if (this.modelTransform)
            {
                this.animator = this.modelTransform.GetComponent<Animator>();
                this.characterModel = this.modelTransform.GetComponent<CharacterModel>();
            }
            if (this.characterModel)
            {
                this.characterModel.invisibilityCount++;
            }
            if (base.cameraTargetParams)
            {
                this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
            }
            if (NetworkServer.active)
            {
                base.characterBody.ApplyBuff(RoR2Content.Buffs.HiddenInvincibility.buffIndex, 1);
            }

            damageFrequency = 1 / (basedamageFrequency * this.attackSpeedStat);
            
        }

        public override void OnExit()
        {
            base.OnExit();
            Util.PlaySound(Evis.endSoundString, base.gameObject);
            this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
            this.modelTransform = base.GetModelTransform();
            if (this.modelTransform)
            {


                TemporaryOverlay temporaryOverlay = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
                temporaryOverlay.duration = 0.6f;
                temporaryOverlay.animateShaderAlpha = true;
                temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                temporaryOverlay.destroyComponentOnEnd = true;
                temporaryOverlay.originalMaterial = RoR2.LegacyResourcesAPI.Load<Material>("Materials/matMercEvisTarget");
                temporaryOverlay.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
                TemporaryOverlay temporaryOverlay2 = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
                temporaryOverlay2.duration = 0.7f;
                temporaryOverlay2.animateShaderAlpha = true;
                temporaryOverlay2.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                temporaryOverlay2.destroyComponentOnEnd = true;
                temporaryOverlay2.originalMaterial = RoR2.LegacyResourcesAPI.Load<Material>("Materials/matHuntressFlashExpanded");
                temporaryOverlay2.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
            }
            if (this.characterModel)
            {
                this.characterModel.invisibilityCount--;
            }
            CameraTargetParams.AimRequest aimRequest = this.aimRequest;
            if (aimRequest != null)
            {
                aimRequest.Dispose();
            }
            if (NetworkServer.active)
            {
                base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
                base.characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, lingeringInvincibilityDuration);
            }
            Util.PlaySound(Evis.endSoundString, base.gameObject);
            base.SmallHop(base.characterMotor, smallHopVelocity);
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            this.stopwatch += Time.fixedDeltaTime;
            actualstopwatch += Time.fixedDeltaTime;
            totalstopwatch += Time.fixedDeltaTime;
            if (totalstopwatch >= duration * 5)
            {
                this.outer.SetNextStateToMain();
            }
            this.attackStopwatch += Time.fixedDeltaTime;
            float num = damageFrequency;
            if (this.attackStopwatch >= num)
            {
                this.attackStopwatch -= num;
                HurtBox hurtBox = this.SearchForTarget();
                if (hurtBox)
                {

                    HurtBoxGroup hurtBoxGroup = hurtBox.hurtBoxGroup;
                    HurtBox hurtBox2 = hurtBoxGroup.hurtBoxes[UnityEngine.Random.Range(0, hurtBoxGroup.hurtBoxes.Length - 1)];
                    if (hurtBox2)
                    {
                        Vector3 position = hurtBox2.transform.position;
                        Vector2 normalized = UnityEngine.Random.insideUnitCircle.normalized;
                        Vector3 normal = new Vector3(normalized.x, 0f, normalized.y);
                        EffectManager.SimpleImpactEffect(Evis.hitEffectPrefab, position, normal, false);
                        Transform transform = hurtBox.hurtBoxGroup.transform;
                        TemporaryOverlay temporaryOverlay = transform.gameObject.AddComponent<TemporaryOverlay>();
                        temporaryOverlay.duration = num;
                        temporaryOverlay.animateShaderAlpha = true;
                        temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
                        temporaryOverlay.destroyComponentOnEnd = true;
                        temporaryOverlay.originalMaterial = RoR2.LegacyResourcesAPI.Load<Material>("Materials/matMercEvisTarget");
                        temporaryOverlay.AddToCharacerModel(transform.GetComponent<CharacterModel>());
                        if (NetworkServer.active)
                        {
                            DamageInfo damageInfo = new DamageInfo();
                            damageInfo.damage = damageCoefficient * this.damageStat;
                            damageInfo.attacker = base.gameObject;
                            damageInfo.procCoefficient = procCoefficient;
                            damageInfo.position = hurtBox2.transform.position;
                            damageInfo.crit = this.crit;
                            damageInfo.damageType = damageType;
                            hurtBox2.healthComponent.TakeDamage(damageInfo);
                            GlobalEventManager.instance.OnHitEnemy(damageInfo, hurtBox2.healthComponent.gameObject);
                            GlobalEventManager.instance.OnHitAll(damageInfo, hurtBox2.healthComponent.gameObject);
                        }

                    }
                }
                else if (base.isAuthority && this.stopwatch > minimumDuration)
                {
                    this.outer.SetNextStateToMain();
                }
            }
            if (base.characterMotor)
            {
                base.characterMotor.velocity = Vector3.zero;
            }
            if (this.stopwatch >= duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
            }
        }


        private void CreateBlinkEffect(Vector3 origin)
        {
            EffectData effectData = new EffectData();
            effectData.rotation = Util.QuaternionSafeLookRotation(Vector3.up);
            effectData.origin = origin;
            EffectManager.SpawnEffect(Evis.blinkPrefab, effectData, false);
        }


        private HurtBox SearchForTarget()
        {
            BullseyeSearch bullseyeSearch = new BullseyeSearch();
            bullseyeSearch.searchOrigin = base.transform.position;
            bullseyeSearch.searchDirection = UnityEngine.Random.onUnitSphere;
            bullseyeSearch.maxDistanceFilter = radius;
            bullseyeSearch.teamMaskFilter = TeamMask.GetUnprotectedTeams(base.GetTeam());
            bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
            bullseyeSearch.RefreshCandidates();
            bullseyeSearch.FilterOutGameObject(base.gameObject);
            return bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}
