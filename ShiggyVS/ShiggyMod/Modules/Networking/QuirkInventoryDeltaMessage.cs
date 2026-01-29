using R2API.Networking.Interfaces;
using RoR2;
using System;
using UnityEngine.Networking;

namespace ShiggyMod.Modules.Networking
{
    internal class QuirkInventoryDeltaMessage : INetMessage
    {
        private NetworkInstanceId masterId;
        private int[] added;
        private int[] craftedAuto;
        private bool cleared;

        public QuirkInventoryDeltaMessage() { }

        public QuirkInventoryDeltaMessage(NetworkInstanceId masterId, int[] added, int[] craftedAuto, bool cleared)
        {
            this.masterId = masterId;
            this.added = added ?? Array.Empty<int>();
            this.craftedAuto = craftedAuto ?? Array.Empty<int>();
            this.cleared = cleared;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(masterId);

            writer.Write((ushort)added.Length);
            for (int i = 0; i < added.Length; i++) writer.Write(added[i]);

            writer.Write((ushort)craftedAuto.Length);
            for (int i = 0; i < craftedAuto.Length; i++) writer.Write(craftedAuto[i]);

            writer.Write(cleared);
        }

        public void Deserialize(NetworkReader reader)
        {
            masterId = reader.ReadNetworkId();

            int aCount = reader.ReadUInt16();
            added = new int[aCount];
            for (int i = 0; i < aCount; i++) added[i] = reader.ReadInt32();

            int cCount = reader.ReadUInt16();
            craftedAuto = new int[cCount];
            for (int i = 0; i < cCount; i++) craftedAuto[i] = reader.ReadInt32();

            cleared = reader.ReadBoolean();
        }
        public void OnReceived()
        {
            if (!NetworkClient.active) return; // add this

            var masterGO = Util.FindNetworkObject(masterId);
            if (!masterGO) return;

            var master = masterGO.GetComponent<CharacterMaster>();
            if (!master) return;

            var inv = ShiggyMod.Modules.Quirks.QuirkInventory.Ensure(master);

            var addedIds = new ShiggyMod.Modules.Quirks.QuirkId[added.Length];
            for (int i = 0; i < added.Length; i++) addedIds[i] = (ShiggyMod.Modules.Quirks.QuirkId)added[i];

            var craftedIds = new ShiggyMod.Modules.Quirks.QuirkId[craftedAuto.Length];
            for (int i = 0; i < craftedAuto.Length; i++) craftedIds[i] = (ShiggyMod.Modules.Quirks.QuirkId)craftedAuto[i];

            inv.Client_ApplyDelta(addedIds, craftedIds, cleared);
        }

    }
}
