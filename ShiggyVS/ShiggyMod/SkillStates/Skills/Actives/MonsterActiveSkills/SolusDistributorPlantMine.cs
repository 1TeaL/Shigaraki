using EntityStates;
using EntityStates.MinePod;
using R2API.Networking;
using RoR2;
using RoR2.Projectile;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class SolusDistributorPlantMine : Skill
    {

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
            base.PlayCrossfade("LeftArm, Override", "LHandLift", "Attack.playbackRate", duration, 0.05f);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            if (base.isAuthority)
            {
                if (Modules.Config.allowVoice.Value) { AkSoundEngine.PostEvent("ShiggyAttack", base.gameObject); }
            }

            this.duration = baseDuration / this.attackSpeedStat;

            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.greaterwispBuff.buffIndex, 1, Modules.StaticValues.greaterwispballbuffDuration);
                //characterBody.AddTimedBuffAuthority(Modules.Buffs.greaterwispBuff.buffIndex, Modules.StaticValues.greaterwispballbuffDuration);
            }

            
            //EffectManager.SpawnEffect(Modules.ShiggyAsset.chargegreaterwispBall, new EffectData
            //{
            //    origin = FindModelChild(LHand).position,
            //    scale = radius,
            //    rotation = Quaternion.LookRotation(base.transform.position)

            //}, false);

            //EffectManager.SpawnEffect(Modules.ShiggyAsset.chargegreaterwispBall, new EffectData
            //{
            //    origin = FindModelChild(RHand).position,
            //    scale = radius,
            //    rotation = Quaternion.LookRotation(base.transform.position)

            //}, false);


        }
        private void SpawnMine()
        {
            if (base.characterBody.master.GetDeployableCount(DeployableSlot.MinePodMine) >= base.characterBody.master.GetDeployableSameSlotLimit(DeployableSlot.MinePodMine))
            {
                return;
            }
            DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(MinePlant.mineSpawnCard, new DirectorPlacementRule
            {
                placementMode = DirectorPlacementRule.PlacementMode.Direct,
                minDistance = 0f,
                maxDistance = 0f,
                position = characterBody.footPosition + characterDirection.forward * 5f,
            }, RoR2Application.rng);
            directorSpawnRequest.summonerBodyObject = base.gameObject;
            directorSpawnRequest.onSpawnedServer = new Action<SpawnCard.SpawnResult>(OnSpawned);
            GameObject gameObject = DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
            if (gameObject)
            {
                CharacterMaster component = gameObject.GetComponent<CharacterMaster>();
                Deployable deployable = gameObject.AddComponent<Deployable>();
                deployable.onUndeploy = new UnityEvent();
                deployable.onUndeploy.AddListener(new UnityAction(component.TrueKill));
                base.characterBody.master.AddDeployable(deployable, DeployableSlot.MinePodMine);
                EffectManager.SimpleEffect(MinePlant.minePlantVFX, gameObject.transform.position, Quaternion.identity, true);
            }
            Util.PlayAttackSpeedSound(MinePlant.deploySoundEvent, base.gameObject, this.attackSpeedStat);
        }

        private void OnSpawned(SpawnCard.SpawnResult result)
        {
            if (result.success)
            {
                CharacterMaster characterMaster = base.characterBody.master;
                if (characterMaster && characterMaster.minionOwnership.ownerMaster)
                {
                    characterMaster = characterMaster.minionOwnership.ownerMaster;
                }
                CharacterMaster component = result.spawnedInstance.GetComponent<CharacterMaster>();
                component.minionOwnership.SetOwner(characterMaster);


                component.teamIndex = characterMaster.teamIndex;
                component.inventory = characterMaster.inventory;

                CharacterBody body = component.GetComponent<CharacterBody>();
                body.ApplyBuff(Buffs.solusPrimedBuff.buffIndex, 1);

                Inventory component2 = component.GetComponent<Inventory>();
                if (component2)
                {
                    //component2.CopyEquipmentFrom(base.characterBody.inventory);
                    if (base.characterBody.inventory.GetItemCountEffective(RoR2Content.Items.Ghost) > 0)
                    {
                        component2.GiveItemPermanent(RoR2Content.Items.Ghost, 1);
                        component2.GiveItemPermanent(RoR2Content.Items.HealthDecay, 30);
                        component2.GiveItemPermanent(RoR2Content.Items.BoostDamage, 150);
                    }
                    EliteDef eliteDefFromEquipmentIndex = EliteCatalog.GetEliteDefFromEquipmentIndex(base.characterBody.inventory.currentEquipmentIndex);
                    if (eliteDefFromEquipmentIndex != null)
                    {
                        float num = (eliteDefFromEquipmentIndex != null) ? eliteDefFromEquipmentIndex.healthBoostCoefficient : 1f;
                        float num2 = (eliteDefFromEquipmentIndex != null) ? eliteDefFromEquipmentIndex.damageBoostCoefficient : 1f;
                        component.inventory.GiveItemPermanent(RoR2Content.Items.BoostHp, Mathf.RoundToInt((num - 1f) * 10f));
                        component.inventory.GiveItemPermanent(RoR2Content.Items.BoostDamage, Mathf.RoundToInt((num2 - 1f) * 10f));
                    }
                }
            }
        }




        public override void OnExit()
        {
            base.OnExit();
            PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
        }


        public override void FixedUpdate()
        {
            if (base.fixedAge > fireTime && !hasFired)
            {
                hasFired = true;
                SpawnMine();
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
