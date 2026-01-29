namespace ShiggyMod.Modules.Networking
{
    using R2API.Networking.Interfaces;
    using RoR2;
    using ShiggyMod.Modules.Quirks;
    using UnityEngine;
    using UnityEngine.Networking;

    internal class ApexResetSlotRequest : INetMessage
    {
        // This MUST be CharacterMaster.netId (NetworkIdentity netId)
        private NetworkInstanceId masterNetId;
        private byte slotIndex;

        public ApexResetSlotRequest() { }

        public ApexResetSlotRequest(NetworkInstanceId masterNetId, byte slotIndex)
        {
            this.masterNetId = masterNetId;
            this.slotIndex = slotIndex;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(masterNetId);
            writer.Write(slotIndex);
        }

        public void Deserialize(NetworkReader reader)
        {
            masterNetId = reader.ReadNetworkId();
            slotIndex = reader.ReadByte();
        }

        public void OnReceived()
        {


            Debug.Log($"[Apex] RECV reset idx={slotIndex} netID={masterNetId}");

            var masterGO = Util.FindNetworkObject(masterNetId);
            if (!masterGO) { Debug.LogWarning("[Apex] masterGO null"); return; }

            var master = masterGO.GetComponent<CharacterMaster>();
            if (!master) { Debug.LogWarning("[Apex] CharacterMaster missing"); return; }

            if (!master.hasEffectiveAuthority){return; }            

            var ctrl = master.GetComponent<ShiggyMod.Modules.Quirks.ApexSurgeryController>();
            if (!ctrl) { Debug.LogWarning("[Apex] ApexSurgeryController missing on master"); return; }

            // IMPORTANT: server-side controller must have a body reference, otherwise it can't resolve slots.
            // If body isn't spawned yet, do nothing (or queue it).
            if (!master.GetBody())
            {
                Debug.LogWarning("[Apex] master has no body yet (cannot reset)");
                return;
            }

            Debug.Log($"[Apex] SERVER applying reset slot={slotIndex}");
            ctrl.TryAutoResetSlotByIndex_Server(slotIndex);
        }

    }
}
