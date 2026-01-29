namespace ShiggyMod.Modules.Networking
{
    using R2API.Networking;
    using R2API.Networking.Interfaces;
    using RoR2;
    using ShiggyMod.Modules.Quirks;
    using ShiggyMod.Modules.Survivors;
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

            Debug.Log($"[StealQuirkRequest] recv attacker={attackerMasterId} victim={victimBodyId} cost={cost}");


            var inv = QuirkInventory.Ensure(attackerMaster);
            if (!inv)
            {
                new StealQuirkResult(attackerMasterId, victimBodyId, (int)QuirkId.None, false, "MissingInventory", false, 0f)
                    .Send(NetworkDestination.Clients);
                return;
            }

            bool isElite;
            QuirkId resolved = ResolveQuirkFromVictim_Server(victimBody, out string reason, out isElite);
            if (resolved == QuirkId.None)
            {
                new StealQuirkResult(attackerMasterId, victimBodyId, (int)QuirkId.None, false, reason, false, 0f)
                    .Send(NetworkDestination.Clients);
                return;
            }

            if (inv.Has(resolved))
            {
                new StealQuirkResult(attackerMasterId, victimBodyId, (int)resolved, false, "AlreadyOwned", false, 0f)
                    .Send(NetworkDestination.Clients);
                return;
            }

            // stun only on real steals
            var ssoh = victimBody.healthComponent ? victimBody.healthComponent.GetComponent<SetStateOnHurt>() : null;
            if (ssoh != null && ssoh.canBeHitStunned)
            {
                ssoh.SetPain();
                ssoh.SetStun(2f);
                if (victimBody.characterMotor) victimBody.characterMotor.velocity = Vector3.zero;
                if (victimBody.rigidbody) victimBody.rigidbody.velocity = Vector3.zero;
            }

            // apply
            inv.Server_Add(resolved);

            // elite equipment drop (server only)
            if (isElite)
            {
                var eq = QuirkRegistry.GetEliteEquipmentForId(resolved);
                if (eq)
                {
                    // inline droplet spawn (don’t rely on client authority)
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

                // if you still want elite debuff lockout
                victimBody.ApplyBuff(ShiggyMod.Modules.Buffs.eliteDebuff.buffIndex, 1, 60f);
            }

            // SUCCESS: echo cost back so owning client spends locally
            if (cost < 0f) cost = 0f;
            new StealQuirkResult(attackerMasterId, victimBodyId, (int)resolved, true, "OK", true, cost)
                .Send(NetworkDestination.Clients);

        }




        private static QuirkId ResolveQuirkFromVictim_Server(CharacterBody victim, out string reason, out bool isElite)
        {
            isElite = false;
            reason = "OK";
            if (!victim) { reason = "NoVictim"; return QuirkId.None; }

            if (QuirkRegistry.TryGetEliteQuirkId(victim, out var eliteId))
            {
                isElite = true;

                if (victim.HasBuff(ShiggyMod.Modules.Buffs.eliteDebuff.buffIndex))
                {
                    reason = "EliteDebuffed";
                    return QuirkId.None;
                }

                return eliteId;
            }

            string bodyName = BodyCatalog.GetBodyName(victim.bodyIndex);
            if (QuirkTargetingMap.TryGet(bodyName, out var id) && id != QuirkId.None)
                return id;

            reason = "NoMapping";
            return QuirkId.None;
        }

    }
}
