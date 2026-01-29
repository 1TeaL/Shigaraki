namespace ShiggyMod.Modules.Networking
{
    using R2API.Networking.Interfaces;
    using RoR2;
    using ShiggyMod.Modules.Quirks;
    using ShiggyMod.Modules.Survivors;
    using UnityEngine;
    using UnityEngine.Networking;

    internal class StealQuirkResult : INetMessage
    {
        private NetworkInstanceId attackerMasterId;
        private NetworkInstanceId victimBodyId;

        private int quirkId;
        private bool success;
        private string reason;
        private bool playVfx;

        private float cost; // NEW

        public StealQuirkResult() { }

        public StealQuirkResult(NetworkInstanceId attackerMasterId, NetworkInstanceId victimBodyId,
            int quirkId, bool success, string reason, bool playVfx, float cost)
        {
            this.attackerMasterId = attackerMasterId;
            this.victimBodyId = victimBodyId;
            this.quirkId = quirkId;
            this.success = success;
            this.reason = reason ?? "";
            this.playVfx = playVfx;
            this.cost = cost;
        }

        public void Serialize(NetworkWriter w)
        {
            w.Write(attackerMasterId);
            w.Write(victimBodyId);
            w.Write(quirkId);
            w.Write(success);
            w.Write(reason);
            w.Write(playVfx);
            w.Write(cost); // NEW
        }

        public void Deserialize(NetworkReader r)
        {
            attackerMasterId = r.ReadNetworkId();
            victimBodyId = r.ReadNetworkId();
            quirkId = r.ReadInt32();
            success = r.ReadBoolean();
            reason = r.ReadString();
            playVfx = r.ReadBoolean();
            cost = r.ReadSingle(); // NEW
        }

        public void OnReceived()
        {
            if (!NetworkClient.active) return;

            Debug.Log($"[StealQuirkResult] recv attacker={attackerMasterId} success={success} reason={reason} cost={cost}");

            // IMPORTANT: use ClientScene here on clients (not Util.FindNetworkObject)
            var attackerMasterGO = ClientScene.FindLocalObject(attackerMasterId);
            if (!attackerMasterGO)
            {
                Debug.LogWarning($"[StealQuirkResult] attacker master object not found on client: {attackerMasterId}");
                return;
            }

            var attackerMaster = attackerMasterGO.GetComponent<CharacterMaster>();
            if (!attackerMaster) return;

            // IMPORTANT: route by netId (NOT object reference equality)
            var localUser = LocalUserManager.GetFirstLocalUser();
            var localMaster = localUser?.cachedMaster;
            if (!localMaster)
            {
                Debug.LogWarning("[StealQuirkResult] no local master");
                return;
            }

            if (localMaster.netId != attackerMaster.netId)
                return; // not our result

            var attackerBody = attackerMaster.GetBody();
            if (!attackerBody) return;

            var shiggy = attackerBody.GetComponent<ShiggyController>();
            if (!shiggy) return;

            shiggy.Client_OnStealResult((QuirkId)quirkId, success, reason, victimBodyId, playVfx, cost);
        }
    }
}
