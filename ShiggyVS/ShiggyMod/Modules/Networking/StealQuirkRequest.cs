namespace ShiggyMod.Modules.Networking
{
    using R2API.Networking;
    using R2API.Networking.Interfaces;
    using RoR2;
    using ShiggyMod.Modules.Quirks;
    using ShiggyMod.Modules.Survivors;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Networking;

    internal class StealQuirkRequest : INetMessage
    {
        private NetworkInstanceId attackerMasterId;
        private NetworkInstanceId victimBodyId;
        private float cost;

        public StealQuirkRequest() { }

        public StealQuirkRequest(NetworkInstanceId attackerMasterId, NetworkInstanceId victimBodyId, float cost)
        {
            this.attackerMasterId = attackerMasterId;
            this.victimBodyId = victimBodyId;
            this.cost = cost;
        }


        public void Serialize(NetworkWriter w)
        {
            w.Write(attackerMasterId);
            w.Write(victimBodyId);
            w.Write(cost);
        }

        public void Deserialize(NetworkReader r)
        {
            attackerMasterId = r.ReadNetworkId();
            victimBodyId = r.ReadNetworkId();
            cost = r.ReadSingle();
        }
        public void OnReceived()
        {
            if (!NetworkServer.active) return;

            var attackerMasterGO = Util.FindNetworkObject(attackerMasterId);
            var victimBodyGO = Util.FindNetworkObject(victimBodyId);
            if (!attackerMasterGO || !victimBodyGO) return;

            var attackerMaster = attackerMasterGO.GetComponent<CharacterMaster>();
            var victimBody = victimBodyGO.GetComponent<CharacterBody>();
            if (!attackerMaster || !victimBody) return;

            var attackerBody = attackerMaster.GetBody();
            if (!attackerBody) return;

            if (cost < 0f) cost = 0f;

            Debug.Log($"[StealQuirkRequest] recv attacker={attackerMasterId} victim={victimBodyId} cost={cost}");

            var inv = QuirkInventory.Ensure(attackerMaster);
            if (!inv)
            {
                new StealQuirkResult(attackerMasterId, victimBodyId, (int)QuirkId.None, false, "MissingInventory", false, cost)
                    .Send(NetworkDestination.Clients);
                return;
            }

            // Resolve candidates: elite + base
            var candidates = new List<QuirkId>(2);
            ResolveQuirksFromVictim_Server(victimBody, candidates, out bool victimHadElite);

            if (candidates.Count == 0)
            {
                // IMPORTANT: include cost so client can refund/clear pending
                new StealQuirkResult(attackerMasterId, victimBodyId, (int)QuirkId.None, false, "NoMapping", false, cost)
                    .Send(NetworkDestination.Clients);
                return;
            }

            // Try grant both, skipping already owned
            var granted = new List<QuirkId>(2);
            foreach (var q in candidates)
            {
                if (q == QuirkId.None) continue;
                if (inv.Has(q)) continue;

                inv.Server_Add(q);
                granted.Add(q);
            }

            if (granted.Count == 0)
            {
                // Nothing new -> fail (refund). include cost.
                new StealQuirkResult(attackerMasterId, victimBodyId, (int)QuirkId.None, false, "AlreadyOwned", false, cost)
                    .Send(NetworkDestination.Clients);
                return;
            }

            // Stun only on real steals (at least one granted)
            var ssoh = victimBody.healthComponent ? victimBody.healthComponent.GetComponent<SetStateOnHurt>() : null;
            if (ssoh != null && ssoh.canBeHitStunned)
            {
                ssoh.SetPain();
                ssoh.SetStun(2f);
                if (victimBody.characterMotor) victimBody.characterMotor.velocity = Vector3.zero;
                if (victimBody.rigidbody) victimBody.rigidbody.velocity = Vector3.zero;
            }

            // Apply elite-only side effects if elite quirk was among granted
            bool grantedElite = false;
            QuirkId eliteGrantedId = QuirkId.None;
            if (victimHadElite)
            {
                // If the first candidate was elite, you can track it more explicitly;
                // easiest is: check if granted contains any elite-id by recomputing.
                if (QuirkRegistry.TryGetEliteQuirkId(victimBody, out var eliteId) && eliteId != QuirkId.None)
                {
                    if (granted.Contains(eliteId))
                    {
                        grantedElite = true;
                        eliteGrantedId = eliteId;
                    }
                }
            }

            if (grantedElite)
            {
                var eq = QuirkRegistry.GetEliteEquipmentForId(eliteGrantedId);
                if (eq)
                {
                    PickupIndex pickup = PickupCatalog.FindPickupIndex(eq.equipmentIndex);
                    if (pickup != PickupIndex.none)
                    {
                        Vector3 pos = victimBody.corePosition + Vector3.up * 1.5f;
                        Vector3 vel = Vector3.up * 20f + victimBody.transform.forward * 2f;

                        UniquePickup unique = new UniquePickup(pickup);
                        unique.upgradeValue = 1;
                        PickupDropletController.CreatePickupDroplet(unique, pos, vel, false, false);
                    }
                }

                victimBody.ApplyBuff(ShiggyMod.Modules.Buffs.eliteDebuff.buffIndex, 1, 60f);
            }

            // Send one result per granted quirk.
            // Only LAST message carries cost so client finalizes spend/refund once.
            for (int i = 0; i < granted.Count; i++)
            {
                bool isLast = (i == granted.Count - 1);

                // Play VFX only once (first success)
                bool doVfx = (i == 0);

                new StealQuirkResult(attackerMasterId, victimBodyId, (int)granted[i], true, "OK", doVfx, isLast ? cost : 0f)
                    .Send(NetworkDestination.Clients);
            }
        }





        private static void ResolveQuirksFromVictim_Server(CharacterBody victim, List<QuirkId> outIds, out bool victimHadElite)
        {
            outIds.Clear();
            victimHadElite = false;

            if (!victim) return;

            // Elite mapping first (but do NOT early-return)
            if (QuirkRegistry.TryGetEliteQuirkId(victim, out var eliteId) && eliteId != QuirkId.None)
            {
                victimHadElite = true;

                // Elite debuff blocks ONLY elite steal, not the base/body quirk
                if (!victim.HasBuff(ShiggyMod.Modules.Buffs.eliteDebuff.buffIndex))
                    outIds.Add(eliteId);
            }

            // Base/body mapping always considered
            string bodyName = BodyCatalog.GetBodyName(victim.bodyIndex);
            if (QuirkTargetingMap.TryGet(bodyName, out var baseId) && baseId != QuirkId.None)
            {
                if (!outIds.Contains(baseId))
                    outIds.Add(baseId);
            }
        }



    }
}
