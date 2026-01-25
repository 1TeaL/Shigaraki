using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Projectile;
using R2API.Networking;
using IL.RoR2.Achievements.Bandit2;
using ShiggyMod.Modules;

namespace ShiggyMod.SkillStates
{
    public class SolusFactorUnleashed : Skill
    {
        //Solus Accelerate, Solus Plant Mine, Solus Invalidate, Solus Extract, Solus Priming, Solus Transport
        public static GameObject effectPrefab;

        private float radius = 0.5f;
        private float damageCoefficient = Modules.StaticValues.greaterwispballDamageCoefficient;
        private float procCoefficient = 1f;
        private float force = 1000f;
        private float speedOverride = -1f;
        private GameObject chargeEffectLeft;
        private GameObject chargeEffectRight;
        private string LHand = "LHand";
        private string RHand = "RHand";


        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            //base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            base.PlayCrossfade("FullBody, Override", "BothHandFist", "Attack.playbackRate", duration, 0.05f);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }

            //calc how many seconds of duration you would get per 10 chaos, then spend all regardless
            int buffDuration = Mathf.RoundToInt(energySystem.currentplusChaos / StaticValues.solusFactorUnleashedEnergyPerStack);
            energySystem.SpendplusChaos(energySystem.currentplusChaos);
            characterBody.ApplyBuff(Modules.Buffs.solusSuperPrimedBuff.buffIndex, 1, buffDuration);

        }

        public override void OnExit()
        {
            base.OnExit();
            PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
        }


        public override void FixedUpdate()
        {           
            if(base.fixedAge > fireTime && !hasFired)
            {
                hasFired = true;

                if (ShiggyAsset.solusFactorMuzzleEffectPrefab1)
                {
                    Vector3 randRelPos = new Vector3((float)Random.Range(-4, 4) / 4f, (float)Random.Range(-4, 4) / 4f, (float)Random.Range(-4, 4) / 4f);

                    EffectData effectData = new EffectData
                    {
                        scale = 1f,
                        origin = FindModelChild(LHand).position + randRelPos,
                    };
                    EffectManager.SpawnEffect(ShiggyAsset.solusFactorMuzzleEffectPrefab1, effectData, true);
                }
                if (ShiggyAsset.solusFactorMuzzleEffectPrefab1)
                {
                    Vector3 randRelPos = new Vector3((float)Random.Range(-4, 4) / 4f, (float)Random.Range(-4, 4) / 4f, (float)Random.Range(-4, 4) / 4f);

                    EffectData effectData = new EffectData
                    {
                        scale = 1f,
                        origin = FindModelChild(LHand).position + randRelPos,
                    };
                    EffectManager.SpawnEffect(ShiggyAsset.solusFactorMuzzleEffectPrefab1, effectData, true);
                }
                if (ShiggyAsset.solusFactorMuzzleEffectPrefab2)
                {
                    Vector3 randRelPos = new Vector3((float)Random.Range(-4, 4) / 4f, (float)Random.Range(-4, 4) / 4f, (float)Random.Range(-4, 4) / 4f);

                    EffectData effectData = new EffectData
                    {
                        scale = 1f,
                        origin = FindModelChild(LHand).position + randRelPos,
                    };
                    EffectManager.SpawnEffect(ShiggyAsset.solusFactorMuzzleEffectPrefab2, effectData, true);
                }
                if (ShiggyAsset.solusFactorMuzzleEffectPrefab3)
                {
                    Vector3 randRelPos = new Vector3((float)Random.Range(-4, 4) / 4f, (float)Random.Range(-4, 4) / 4f, (float)Random.Range(-4, 4) / 4f);

                    EffectData effectData = new EffectData
                    {
                        scale = 1f,
                        origin = FindModelChild(LHand).position + randRelPos,
                    };
                    EffectManager.SpawnEffect(ShiggyAsset.solusFactorMuzzleEffectPrefab3, effectData, true);
                }
                if (ShiggyAsset.solusFactorMuzzleEffectPrefab4)
                {
                    Vector3 randRelPos = new Vector3((float)Random.Range(-4, 4) / 4f, (float)Random.Range(-4, 4) / 4f, (float)Random.Range(-4, 4) / 4f);

                    EffectData effectData = new EffectData
                    {
                        scale = 1f,
                        origin = FindModelChild(RHand).position + randRelPos,
                    };
                    EffectManager.SpawnEffect(ShiggyAsset.solusFactorMuzzleEffectPrefab4, effectData, true);
                }
                if (ShiggyAsset.solusFactorMuzzleEffectPrefab5)
                {
                    Vector3 randRelPos = new Vector3((float)Random.Range(-4, 4) / 4f, (float)Random.Range(-4, 4) / 4f, (float)Random.Range(-4, 4) / 4f);

                    EffectData effectData = new EffectData
                    {
                        scale = 1f,
                        origin = FindModelChild(RHand).position + randRelPos,
                    };
                    EffectManager.SpawnEffect(ShiggyAsset.solusFactorMuzzleEffectPrefab5, effectData, true);
                }
                if (ShiggyAsset.solusFactorMuzzleEffectPrefab6)
                {
                    Vector3 randRelPos = new Vector3((float)Random.Range(-4, 4) / 4f, (float)Random.Range(-4, 4) / 4f, (float)Random.Range(-4, 4) / 4f);

                    EffectData effectData = new EffectData
                    {
                        scale = 1f,
                        origin = FindModelChild(RHand).position + randRelPos,
                    };
                    EffectManager.SpawnEffect(ShiggyAsset.solusFactorMuzzleEffectPrefab6, effectData, true);
                }
            }
            if (base.fixedAge >= this.duration && base.isAuthority)
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
