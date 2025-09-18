namespace ShiggyMod.Modules.Networking
{
    using R2API.Networking.Interfaces;
    using RoR2;
    using UnityEngine;
    using UnityEngine.Networking;

    internal class ApexResetSlotRequest : INetMessage
    {
        // Networked
        private NetworkInstanceId netID;
        private byte slotIndex;

        public ApexResetSlotRequest() { }

        public ApexResetSlotRequest(NetworkInstanceId netID, byte slotIndex)
        {
            this.netID = netID;
            this.slotIndex = slotIndex;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netID);
            writer.Write(slotIndex);
        }

        public void Deserialize(NetworkReader reader)
        {
            netID = reader.ReadNetworkId();
            slotIndex = reader.ReadByte();
        }

        public void OnReceived()
        {
            if (!NetworkServer.active) return;

            var masterGO = Util.FindNetworkObject(netID);
            if (!masterGO) return;

            var master = masterGO.GetComponent<CharacterMaster>();
            if (master == null) return;

            var ctrl = masterGO.GetComponent<ShiggyMod.Modules.Quirks.ApexSurgeryController>();
            if (ctrl == null) return;

            var body = master.GetBody();
            if (body == null) return;

            ctrl.TryAutoResetSlotByIndex(slotIndex);
        }
    }
}