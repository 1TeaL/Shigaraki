using R2API.Networking.Interfaces;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.Modules.Networking
{
    internal class GivePassiveQuirkResult : INetMessage
    {
        private NetworkInstanceId attackerMasterId;
        private bool success;
        private string reason;
        private int buffIndex;

        public GivePassiveQuirkResult() { }

        public GivePassiveQuirkResult(NetworkInstanceId attackerMasterId, bool success, string reason, int buffIndex)
        {
            this.attackerMasterId = attackerMasterId;
            this.success = success;
            this.reason = reason ?? "";
            this.buffIndex = buffIndex;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(attackerMasterId);
            writer.Write(success);
            writer.Write(reason);
            writer.Write(buffIndex);
        }

        public void Deserialize(NetworkReader reader)
        {
            attackerMasterId = reader.ReadNetworkId();
            success = reader.ReadBoolean();
            reason = reader.ReadString();
            buffIndex = reader.ReadInt32();
        }

        public void OnReceived()
        {
            if (!NetworkClient.active) return;

            var masterGO = Util.FindNetworkObject(attackerMasterId);
            if (!masterGO) return;

            var master = masterGO.GetComponent<CharacterMaster>();
            if (!master || master != LocalUserManager.GetFirstLocalUser()?.cachedMaster) return;

            var body = master.GetBody();
            if (!body) return;

            var shiggy = body.GetComponent<ShiggyMod.Modules.Survivors.ShiggyController>();
            if (!shiggy) return;

            //shiggy.ClientOnGivePassiveResult(success, reason, (BuffIndex)buffIndex);
        }
    }
}
