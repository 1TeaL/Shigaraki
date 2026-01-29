// EquipLoadoutResult.cs
using R2API.Networking.Interfaces;
using RoR2;
using ShiggyMod.Modules.Quirks;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.Modules.Networking
{
    /// <summary>
    /// Server -> Owner client: re-apply the same loadout locally so non-host gets overrides.
    /// </summary>
    internal class EquipLoadoutResult : INetMessage
    {
        private NetworkInstanceId masterId;
        private EquipLoadoutRequest.SelectedQuirkLoadoutNet netLoadout;

        public EquipLoadoutResult() { }

        public EquipLoadoutResult(NetworkInstanceId masterId, SelectedQuirkLoadout loadout)
        {
            this.masterId = masterId;
            this.netLoadout = EquipLoadoutRequest.SelectedQuirkLoadoutNet.From(loadout);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(masterId);
            netLoadout.Serialize(writer);
        }

        public void Deserialize(NetworkReader reader)
        {
            masterId = reader.ReadNetworkId();
            netLoadout = EquipLoadoutRequest.SelectedQuirkLoadoutNet.Deserialize(reader);
        }

        public void OnReceived()
        {
            // Client-only
            if (!NetworkClient.active) return;

            var masterGO = Util.FindNetworkObject(masterId);
            if (!masterGO) return;

            var master = masterGO.GetComponent<CharacterMaster>();
            if (!master) return;

            var body = master.GetBody();
            if (!body) return;

            // Only the owning client should apply to its body
            if (!body.hasEffectiveAuthority) return;

            var loadout = netLoadout.ToRuntime();

            // Apply overrides locally (no persistence, no server mutation)
            QuirkEquip.ApplyClientLocal(body, loadout);
        }
    }
}
