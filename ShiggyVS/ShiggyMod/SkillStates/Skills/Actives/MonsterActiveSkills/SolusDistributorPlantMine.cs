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

            //if (NetworkServer.active)
            //{
            //    characterBody.ApplyBuff(Modules.Buffs.greaterwispBuff.buffIndex, 1, Modules.StaticValues.greaterwispballbuffDuration);
            //    //characterBody.AddTimedBuffAuthority(Modules.Buffs.greaterwispBuff.buffIndex, Modules.StaticValues.greaterwispballbuffDuration);
            //}

            
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
        private void SpawnMineServer()
        {
            if (!NetworkServer.active) return;
            if (!characterBody || !characterBody.master) return;

            var master = characterBody.master;

            // Respect deployable limit
            if (master.GetDeployableCount(DeployableSlot.MinePodMine) >= master.GetDeployableSameSlotLimit(DeployableSlot.MinePodMine))
                return;

            // Ensure spawn card exists
            var card = MinePlant.mineSpawnCard;
            if (!card)
            {
                Debug.LogError("[Shiggy] MinePlant.mineSpawnCard is null. Provide your own SpawnCard or load vanilla one.");
                return;
            }

            Vector3 pos = characterBody.footPosition + characterDirection.forward * 5f;

            var req = new DirectorSpawnRequest(
                card,
                new DirectorPlacementRule
                {
                    placementMode = DirectorPlacementRule.PlacementMode.Direct,
                    position = pos,
                    minDistance = 0f,
                    maxDistance = 0f
                },
                RoR2Application.rng);

            req.summonerBodyObject = gameObject;
            req.onSpawnedServer = OnSpawnedServer;

            var spawned = DirectorCore.instance.TrySpawnObject(req);
            if (spawned)
            {
                // Deployable tracking expects a Deployable component on the spawned OBJECT.
                // Vanilla adds it to the returned object (which is a master object for this card).
                var spawnedMaster = spawned.GetComponent<CharacterMaster>();
                if (spawnedMaster)
                {
                    var dep = spawned.AddComponent<Deployable>();
                    dep.onUndeploy = new UnityEvent();
                    dep.onUndeploy.AddListener(new UnityAction(spawnedMaster.TrueKill));
                    master.AddDeployable(dep, DeployableSlot.MinePodMine);
                }

                if (MinePlant.minePlantVFX)
                    EffectManager.SimpleEffect(MinePlant.minePlantVFX, spawned.transform.position, Quaternion.identity, true);

                Util.PlayAttackSpeedSound(MinePlant.deploySoundEvent, gameObject, attackSpeedStat);
            }
        }

        private void OnSpawnedServer(SpawnCard.SpawnResult result)
        {
            if (!result.success) return;

            var owner = characterBody.master;
            if (owner && owner.minionOwnership.ownerMaster)
                owner = owner.minionOwnership.ownerMaster;

            var spawnedMaster = result.spawnedInstance.GetComponent<CharacterMaster>();
            if (!spawnedMaster) return;

            spawnedMaster.minionOwnership.SetOwner(owner);
            spawnedMaster.teamIndex = owner.teamIndex;

            // If you need to apply buffs, do it on the BODY, not the master GameObject directly.
            var spawnedBody = spawnedMaster.GetBody();
            if (spawnedBody)
            {
                spawnedBody.AddBuff(Buffs.solusPrimedBuff); // prefer AddBuff(BuffDef) when possible
            }

            var inv = spawnedMaster.inventory;
            if (inv != null)
            {
                // Vanilla behavior
                inv.CopyEquipmentFrom(characterBody.inventory, true);

                if (characterBody.inventory.GetItemCountEffective(RoR2Content.Items.Ghost) > 0)
                {
                    inv.GiveItemPermanent(RoR2Content.Items.Ghost, 1);
                    inv.GiveItemPermanent(RoR2Content.Items.HealthDecay, 30);
                    inv.GiveItemPermanent(RoR2Content.Items.BoostDamage, 150);
                }

                var elite = EliteCatalog.GetEliteDefFromEquipmentIndex(characterBody.inventory.currentEquipmentIndex);
                if (elite != null)
                {
                    float hp = elite.healthBoostCoefficient;
                    float dmg = elite.damageBoostCoefficient;
                    inv.GiveItemPermanent(RoR2Content.Items.BoostHp, Mathf.RoundToInt((hp - 1f) * 10f));
                    inv.GiveItemPermanent(RoR2Content.Items.BoostDamage, Mathf.RoundToInt((dmg - 1f) * 10f));
                }
            }
        }





        public override void OnExit()
        {
            base.OnExit();
            //PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            //PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
        }


        public override void FixedUpdate()
        {
            if (base.fixedAge > fireTime && !hasFired)
            {
                hasFired = true;
                if (NetworkServer.active)
                    SpawnMineServer();
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
