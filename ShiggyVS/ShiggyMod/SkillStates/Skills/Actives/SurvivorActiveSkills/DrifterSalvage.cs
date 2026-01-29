using EntityStates;
using RoR2;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.SkillStates
{
    public class DrifterSalvage : Skill
    {
        private string muzzleString;
        private ChildLocator childLocator;
        private Animator animator;

        private Xoroshiro128Plus rng;
        private CharacterMotor.WalkSpeedPenaltyModifier walkSpeedModifier;

        public float moveSpeedCoefficient = 0.6f;
        public float firstDropDelay = StaticValues.universalFiretime;
        public float delayBetweenDrops = StaticValues.universalFiretime;
        public float dropForce = 15f;
        public float dropForceIncreasePerItem = 5f;

        private int itemsToDrop;
        public int itemsDropped;

        private float itemDropTimeoutAuthority;
        private bool _isFirstDrop;
        private bool droppedItemServer;
        private float currentTriggerTime;

        private Transform _muzzle;

        // Per-item spending: if we fail to pay for the *current* item, stop the whole chain.
        private bool stopChain;

        // Cache of run-legal items (prevents error items)
        private static readonly System.Collections.Generic.List<ItemIndex> allRunLegalItemsCache
            = new System.Collections.Generic.List<ItemIndex>(256);

        private static int allRunLegalItemsCacheRunInstanceId = -1;

        private float checkingTime
        {
            get { return !_isFirstDrop ? delayBetweenDrops : firstDropDelay; }
        }

        public override void OnEnter()
        {
            base.OnEnter();

            itemsToDrop = StaticValues.drifterSalvageItems;

            // ============================
            // Pay per item (20 each)
            // ============================
            // Only authority decides to stop the chain; server will obey via replicated stopChain.
            if (base.isAuthority)
            {
                float costPerItem = StaticValues.drifterSalvagePlusChaosSpend; // set to 20

                if (!energySystem.CanAffordPlusChaos(costPerItem))
                {
                    // Can't pay for this item -> end chain.
                    stopChain = true;
                    droppedItemServer = true;      // prevents server drop path
                    itemDropTimeoutAuthority = 0f; // allows immediate exit

                    outer.SetNextStateToMain();
                    return;
                }

                // Spend for THIS item drop
                energySystem.SpendplusChaos(energySystem.GetFinalPlusChaosCost(costPerItem));
            }

            // ============================
            // Visuals/anim setup
            // ============================
            base.characterBody.SetAimTimer(this.duration);
            base.GetModelAnimator().SetFloat("Attack.playbackRate", 1f);

            int randomAnim = UnityEngine.Random.Range(1, 3);
            PlayCrossfade("LeftArm, Override", "LHandSnap" + randomAnim, "Attack.playbackRate", fireTime, 0.1f);

            muzzleString = "LHand";

            Transform modelTransform = base.GetModelTransform();
            if (modelTransform)
            {
                childLocator = modelTransform.GetComponent<ChildLocator>();
                animator = modelTransform.GetComponent<Animator>();
            }

            Shiggycon = gameObject.GetComponent<ShiggyController>();

            currentTriggerTime = 0f;

            // First-drop-only effects (slow + sound)
            if (itemsDropped == 0)
            {
                _isFirstDrop = true;

                Util.PlaySound("Play_GG_CHAR_Drifter_Special_Salvage", base.gameObject);

                if (base.characterMotor)
                {
                    walkSpeedModifier = new CharacterMotor.WalkSpeedPenaltyModifier
                    {
                        penalty = 1f - moveSpeedCoefficient
                    };
                    base.characterMotor.AddWalkSpeedPenalty(walkSpeedModifier);
                }
            }

            // Server RNG + muzzle
            if (NetworkServer.active)
            {
                rng = new Xoroshiro128Plus(Run.instance.treasureRng.nextUlong);

                ChildLocator modelChildLocator = base.GetModelChildLocator();
                if (modelChildLocator)
                {
                    _muzzle = modelChildLocator.FindChild(muzzleString);
                }
            }

            base.StartAimMode(checkingTime + 0.5f, false);
            itemDropTimeoutAuthority = checkingTime + 0.05f;
        }

        public override void OnExit()
        {
            if (base.characterMotor && walkSpeedModifier != null)
            {
                base.characterMotor.RemoveWalkSpeedPenalty(walkSpeedModifier);
            }

            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            // If we've decided to stop the chain, exit cleanly.
            if (stopChain)
            {
                if (base.isAuthority)
                    outer.SetNextStateToMain();
                return;
            }

            if (NetworkServer.active && !droppedItemServer)
            {
                currentTriggerTime += base.GetDeltaTime();

                if (currentTriggerTime >= checkingTime)
                {
                    DropTempItemServer();
                }
            }

            if (base.isAuthority && (base.fixedAge > itemDropTimeoutAuthority || droppedItemServer))
            {
                SetNextStateAuthority();
            }
        }

        private void SetNextStateAuthority()
        {
            if (!base.isAuthority) return;

            if (stopChain)
            {
                outer.SetNextStateToMain();
                return;
            }

            if (itemsDropped >= itemsToDrop - 1)
            {
                outer.SetNextStateToMain();
                return;
            }

            DrifterSalvage next = EntityStateCatalog.InstantiateState(typeof(DrifterSalvage)) as DrifterSalvage;
            next.itemsDropped = itemsDropped + 1;

            // IMPORTANT: propagate stopChain if it ever becomes true (belt and suspenders)
            next.stopChain = this.stopChain;

            outer.SetNextState(next);
        }

        private static void RebuildRunLegalItemCacheIfNeeded()
        {
            if (Run.instance == null) return;

            int runId = Run.instance.GetInstanceID();
            if (runId == allRunLegalItemsCacheRunInstanceId && allRunLegalItemsCache.Count > 0)
                return;

            allRunLegalItemsCacheRunInstanceId = runId;
            allRunLegalItemsCache.Clear();

            ItemMask mask = Run.instance.availableItems;

            foreach (ItemIndex itemIndex in ItemCatalog.allItems)
            {
                if (itemIndex == ItemIndex.None) continue;
                if (!mask.Contains(itemIndex)) continue;

                ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
                if (itemDef == null) continue;

                if (itemDef.hidden) continue;
                if (itemDef.tier == ItemTier.NoTier) continue;

                PickupIndex pickupIndex = PickupCatalog.FindPickupIndex(itemIndex);
                if (pickupIndex == PickupIndex.none) continue;

                PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
                if (pickupDef == null) continue;

                allRunLegalItemsCache.Add(itemIndex);
            }
        }

        private void DropTempItemServer()
        {
            if (stopChain)
            {
                droppedItemServer = true;
                return;
            }

            if (!NetworkServer.active || droppedItemServer)
                return;

            Transform modelTransform = base.GetModelTransform();
            float y = 12f;

            Vector3 sideways = modelTransform.right * (3f * (itemsDropped + 1) - 6f);
            Vector3 vel = modelTransform.forward * dropForce + sideways;
            vel = new Vector3(vel.x, y, vel.z);

            Vector3 dropPos = _muzzle ? _muzzle.position : base.transform.position;

            RebuildRunLegalItemCacheIfNeeded();

            if (allRunLegalItemsCache.Count == 0)
            {
                droppedItemServer = true;
                return;
            }

            int pick = rng.RangeInt(0, allRunLegalItemsCache.Count);
            ItemIndex itemIndex = allRunLegalItemsCache[pick];

            PickupIndex pickupIndex = PickupCatalog.FindPickupIndex(itemIndex);
            if (pickupIndex == PickupIndex.none)
            {
                droppedItemServer = true;
                return;
            }

            UniquePickup uniquePickup = new UniquePickup(pickupIndex)
            {
                decayValue = 1f
            };

            if (base.characterBody.isPlayerControlled)
            {
                PickupDropletController.CreatePickupDroplet(uniquePickup, dropPos, vel, false, false);
            }
            else
            {
                PickupDef pickupDef = PickupCatalog.GetPickupDef(uniquePickup.pickupIndex);
                if (pickupDef != null)
                {
                    ItemIndex giveIndex = pickupDef.itemIndex;

                    ScrapperController.CreateItemTakenOrb(dropPos, base.gameObject, giveIndex);
                    base.characterBody.inventory.GiveItemTemp(giveIndex, 1f);

                    string baseToken = (base.teamComponent.teamIndex == TeamIndex.Player) ? "PLAYER_PICKUP" : "MONSTER_PICKUP";
                    Chat.SendBroadcastChat(new Chat.PlayerPickupChatMessage
                    {
                        subjectAsCharacterBody = base.characterBody,
                        baseToken = baseToken,
                        pickupToken = Language.GetStringFormatted(
                            "ITEM_MODIFIER_TEMP",
                            Language.GetStringFormatted(pickupDef.nameToken)
                        ),
                        pickupColor = pickupDef.baseColor,
                        pickupQuantity = 1U
                    });
                }
            }

            droppedItemServer = true;
        }


        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.Write((byte)itemsDropped);
            writer.Write(stopChain);
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            itemsDropped = (int)reader.ReadByte();
            stopChain = reader.ReadBoolean();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}
