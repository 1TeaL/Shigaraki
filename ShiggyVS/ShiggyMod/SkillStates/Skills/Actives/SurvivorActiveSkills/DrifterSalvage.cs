using EntityStates;
using ExtraSkillSlots;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.ExpansionManagement;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Networking;
using ShiggyMod.Modules.Survivors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.ParticleSystem.PlaybackState;

namespace ShiggyMod.SkillStates
{
    public class DrifterSalvage : Skill
    {

        private BullseyeSearch search;
        private List<HurtBox> trackingTargets;

        private string muzzleString;
        private ChildLocator childLocator;
        private GameObject chargeEffect;


        private Xoroshiro128Plus rng;
        private CharacterMotor.WalkSpeedPenaltyModifier walkSpeedModifier;
        public float moveSpeedCoefficient = 0.6f;
        public float firstDropDelay = 1f;
        public float delayBetweenDrops = 1f;
        public float dropForce = 15f;
        public float dropForceIncreasePerItem = 5f;
        public PickupDropTable tempItemsDropTable;
        private int itemsToDrop;
        private int itemsDropped;
        private float itemDropTimeoutAuthority;
        private bool _isFirstDrop;
        private bool droppedItemServer;
        private float currentTriggerTime;
        private Transform _muzzle;
        private float checkingTime
        {
            get
            {
                if (!this._isFirstDrop)
                {
                    return this.delayBetweenDrops;
                }
                return this.firstDropDelay;
            }
        }

        public override void OnEnter()
        {
            base.OnEnter(); 



            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", 1f);
            int randomAnim = UnityEngine.Random.RandomRangeInt(1, 3);
            PlayCrossfade("LeftArm, Override", "LHandSnap" + randomAnim, "Attack.playbackRate", fireTime, 0.1f);
            this.muzzleString = "LHand";


            Transform modelTransform = base.GetModelTransform();
            if (modelTransform)
            {
                this.childLocator = modelTransform.GetComponent<ChildLocator>();
                this.animator = modelTransform.GetComponent<Animator>();
            }


            Shiggycon = gameObject.GetComponent<ShiggyController>();


            this.currentTriggerTime = 0f;
            this.itemsToDrop = StaticValues.drifterSalvageItems;
            if (this.itemsDropped == 0)
            {
                //check if able to afford the first time
                if (!energySystem.CanAffordPlusChaos(StaticValues.drifterSalvagePlusChaosSpend))
                {
                    outer.SetNextStateToMain();
                    return;
                }
                else if(energySystem.CanAffordPlusChaos(StaticValues.drifterSalvagePlusChaosSpend))
                {
                    energySystem.SpendplusChaos(energySystem.GetFinalPlusChaosCost(StaticValues.drifterSalvagePlusChaosSpend));
                }

                    this._isFirstDrop = true;
                Util.PlaySound("Play_GG_CHAR_Drifter_Special_Salvage", base.gameObject);
                if (base.characterMotor)
                {
                    this.walkSpeedModifier = new CharacterMotor.WalkSpeedPenaltyModifier
                    {
                        penalty = 1f - this.moveSpeedCoefficient
                    };
                    base.characterMotor.AddWalkSpeedPenalty(this.walkSpeedModifier);
                }
            }
            if (NetworkServer.active)
            {
                this.rng = new Xoroshiro128Plus(Run.instance.treasureRng.nextUlong);
                ChildLocator modelChildLocator = base.GetModelChildLocator();
                if (modelChildLocator)
                {
                    this._muzzle = modelChildLocator.FindChild(muzzleString);
                }
            }
            base.StartAimMode(this.checkingTime + 0.5f, false);
            this.itemDropTimeoutAuthority = this.checkingTime + 0.05f;

        }
        public override void OnExit()
        {
            base.OnExit();
            if (base.characterMotor && this.walkSpeedModifier != null)
            {
                base.characterMotor.RemoveWalkSpeedPenalty(this.walkSpeedModifier);
            }
        }

        public override void FixedUpdate()
        {
            if (NetworkServer.active && !this.droppedItemServer)
            {
                this.currentTriggerTime += base.GetDeltaTime();
                if (this.currentTriggerTime >= this.checkingTime)
                {
                    this.DropTempItemServer();
                }
            }
            if (base.isAuthority && (base.fixedAge > this.itemDropTimeoutAuthority || this.droppedItemServer))
            {
                this.SetNextStateAuthority();
            }
        }
        private void SetNextStateAuthority()
        {
            if (!base.isAuthority)
            {
                return;
            }
            if (this.itemsDropped >= this.itemsToDrop - 1)
            {
                this.outer.SetNextStateToMain();
                return;
            }
            DrifterSalvage salvage = EntityStateCatalog.InstantiateState(typeof(DrifterSalvage)) as DrifterSalvage;
            salvage.itemsDropped = this.itemsDropped + 1;
            this.outer.SetNextState(salvage);
        }

        private void DropTempItemServer()
        {
            if (!NetworkServer.active || this.droppedItemServer)
            {
                return;
            }
            Transform modelTransform = base.GetModelTransform();
            float y = 12f;
            Vector3 b = modelTransform.right * (3f * (float)(this.itemsDropped + 1) - 6f);
            Vector3 vector = modelTransform.forward * this.dropForce + b;
            vector = new Vector3(vector.x, y, vector.z);
            Vector3 vector2 = this._muzzle ? this._muzzle.position : base.transform.position;
            if (this.tempItemsDropTable)
            {
                UniquePickup uniquePickup = this.tempItemsDropTable.GeneratePickup(this.rng);
                uniquePickup.decayValue = 1f;
                if (base.characterBody.isPlayerControlled)
                {
                    PickupDropletController.CreatePickupDroplet(uniquePickup, vector2, vector, false, false);
                }
                else
                {
                    PickupDef pickupDef = PickupCatalog.GetPickupDef(uniquePickup.pickupIndex);
                    ItemIndex itemIndex = pickupDef.itemIndex;
                    ScrapperController.CreateItemTakenOrb(vector2, base.gameObject, itemIndex);
                    base.characterBody.inventory.GiveItemTemp(itemIndex, 1f);
                    string baseToken = (base.teamComponent.teamIndex == TeamIndex.Player) ? "PLAYER_PICKUP" : "MONSTER_PICKUP";
                    Chat.SendBroadcastChat(new Chat.PlayerPickupChatMessage
                    {
                        subjectAsCharacterBody = base.characterBody,
                        baseToken = baseToken,
                        pickupToken = Language.GetStringFormatted("ITEM_MODIFIER_TEMP", new object[]
                        {
                            Language.GetStringFormatted(pickupDef.nameToken, Array.Empty<object>())
                        }),
                        pickupColor = pickupDef.baseColor,
                        pickupQuantity = 1U
                    });
                }
            }
            this.droppedItemServer = true;
        }


        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.Write((byte)this.itemsDropped);
        }
        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            this.itemsDropped = (int)reader.ReadByte();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }

    }
}