using ShiggyMod.Modules.Survivors;
using EntityStates;
using RoR2;
using UnityEngine;
using System.Collections.Generic;
using ShiggyMod.Modules;
using UnityEngine.Networking;
using RoR2.ExpansionManagement;
using ExtraSkillSlots;
using R2API.Networking;

namespace ShiggyMod.SkillStates
{
    public class GenesisController : MonoBehaviour
    {
        public CharacterBody shiggyBody;
        public CharacterBody charBody;
        private GameObject laserEffect;
        public HurtBox Target;
        public Vector3 startPosition;
        public Vector3 endPosition;
        private Transform laserEffectEndTransform;

        public float duration = 1f;
        public float fireInterval;
        public float timer;
        public int numberOfHits = 0;
        public int totalHits;

        public void Start()
        {
            charBody = gameObject.GetComponent<CharacterBody>();
            fireInterval = duration / totalHits;
            startPosition = Target.transform.position + Vector3.up * StaticValues.genesisStartHeight;
            endPosition = Target.transform.position;


            laserEffect = UnityEngine.Object.Instantiate(Assets.xiconstructBeamLaser, startPosition, Quaternion.LookRotation(Vector3.down));
            //laserEffect.transform.parent = Target.transform;
            laserEffectEndTransform = laserEffect.GetComponent<ChildLocator>().FindChild("LaserEnd");
            laserEffectEndTransform.position = endPosition;

            EffectManager.SpawnEffect(Assets.xiconstructbeamEffect, new EffectData
            {
                origin = startPosition,
                scale = 1f,
                rotation = Quaternion.LookRotation(Vector3.down),

            }, true);
        }    

        public void Update()
        {
            //Handle transform of effectObj
            if (laserEffect)
            {
                laserEffectEndTransform.position = endPosition;
            }
        }


        public void FixedUpdate()
        {
            if (numberOfHits < totalHits)
            {
                if (timer < fireInterval)
                {
                    timer += Time.fixedDeltaTime;
                }
                else if (timer >= fireInterval)
                {
                    numberOfHits++;
                    timer = 0f;

                    Debug.Log("fire bullet");
                    new BulletAttack
                    {
                        bulletCount = 1,
                        aimVector = Vector3.down,
                        origin = startPosition,
                        damage = shiggyBody.damage * StaticValues.genesisDamageCoefficient,
                        damageColorIndex = DamageColorIndex.Default,
                        damageType = default,
                        falloffModel = BulletAttack.FalloffModel.None,
                        maxDistance = StaticValues.genesisStartHeight + 5f,
                        force = 100f,
                        hitMask = LayerIndex.CommonMasks.bullet,
                        minSpread = 0f,
                        maxSpread = 0f,
                        isCrit = shiggyBody.RollCrit(),
                        owner = shiggyBody.gameObject,
                        smartCollision = false,
                        procChainMask = default(ProcChainMask),
                        procCoefficient = StaticValues.genesisProcCoefficient,
                        radius = 3f,
                        sniper = false,
                        stopperMask = LayerIndex.noCollision.mask,
                        weapon = null,
                        //tracerEffectPrefab = Modules.Assets.VoidFiendBeamTracer,
                        spreadPitchScale = 0f,
                        spreadYawScale = 0f,
                        queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                        hitEffectPrefab = EntityStates.Commando.CommandoWeapon.FirePistol2.hitEffectPrefab,
                    }.Fire();
                    
                }
            }
            else if (numberOfHits >= totalHits)
            {
                Destroy(laserEffect);
                Destroy(this);
            }

            if (!charBody)
            {
                Destroy(laserEffect);
                Destroy(this);
            }
        }
    }
}