using R2API.Networking.Interfaces;
using RoR2;
using UnityEngine.Networking;

namespace ShiggyMod.Modules.Networking
{
    internal class QuirkInventoryAddRequest : INetMessage
    {
        private NetworkInstanceId masterId;
        private int quirkId;

        public QuirkInventoryAddRequest() { }

        public QuirkInventoryAddRequest(NetworkInstanceId masterId, int quirkId)
        {
            this.masterId = masterId;
            this.quirkId = quirkId;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(masterId);
            writer.Write(quirkId);
        }

        public void Deserialize(NetworkReader reader)
        {
            masterId = reader.ReadNetworkId();
            quirkId = reader.ReadInt32();
        }

        public void OnReceived()
        {
            if (!NetworkServer.active) return;

            var masterGO = Util.FindNetworkObject(masterId);
            if (!masterGO) return;

            var master = masterGO.GetComponent<CharacterMaster>();
            if (!master) return;

            var inv = ShiggyMod.Modules.Quirks.QuirkInventory.Ensure(master);
            inv.Server_Add((ShiggyMod.Modules.Quirks.QuirkId)quirkId);
        }
    }
}
