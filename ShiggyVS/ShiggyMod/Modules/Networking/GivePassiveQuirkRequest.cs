using R2API.Networking.Interfaces;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.Modules.Networking
{
    internal class GivePassiveQuirkRequest : INetMessage
    {
        private NetworkInstanceId attackerMasterId;
        private NetworkInstanceId targetBodyId;
        private int buffIndex;     // BuffIndex is an int under the hood

        public GivePassiveQuirkRequest() { }

        public GivePassiveQuirkRequest(NetworkInstanceId attackerMasterId, NetworkInstanceId targetBodyId, int buffIndex)
        {
            this.attackerMasterId = attackerMasterId;
            this.targetBodyId = targetBodyId;
            this.buffIndex = buffIndex;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(attackerMasterId);
            writer.Write(targetBodyId);
            writer.Write(buffIndex);
        }

        public void Deserialize(NetworkReader reader)
        {
            attackerMasterId = reader.ReadNetworkId();
            targetBodyId = reader.ReadNetworkId();
            buffIndex = reader.ReadInt32();
        }

        public void OnReceived()
        {
            if (!NetworkServer.active) return;

            var masterGO = Util.FindNetworkObject(attackerMasterId);
            var targetGO = Util.FindNetworkObject(targetBodyId);
            if (!masterGO || !targetGO) return;

            var attackerMaster = masterGO.GetComponent<CharacterMaster>();
            var targetBody = targetGO.GetComponent<CharacterBody>();
            if (!attackerMaster || !targetBody) return;

            var attackerBody = attackerMaster.GetBody();
            if (!attackerBody) return;

            var shiggy = attackerBody.GetComponent<ShiggyMod.Modules.Survivors.ShiggyController>();
            if (!shiggy) return;

            //shiggy.ServerGivePassiveBuff(targetBody, (BuffIndex)buffIndex);
        }
    }
}
