// GivePassiveRequest.cs
using R2API.Networking.Interfaces;
using RoR2;
using ShiggyMod.Modules.Quirks;
using UnityEngine;
using UnityEngine.Networking;
using static ShiggyMod.Modules.Quirks.QuirkRegistry;

namespace ShiggyMod.Modules.Networking
{
    /// <summary>
    /// Client -> Server: give a PASSIVE quirk (by QuirkId) from 'giver' to 'allyTarget'.
    /// Server validates team/range/ownership/category and applies via Buff.
    /// </summary>
    internal class GivePassiveRequest : INetMessage
    {
        private NetworkInstanceId giverMasterId;
        private NetworkInstanceId targetMasterId;
        private int quirkIdInt;

        public GivePassiveRequest() { }

        public GivePassiveRequest(NetworkInstanceId giverMasterId, NetworkInstanceId targetMasterId, QuirkId quirkId)
        {
            this.giverMasterId = giverMasterId;
            this.targetMasterId = targetMasterId;
            this.quirkIdInt = (int)quirkId;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(giverMasterId);
            writer.Write(targetMasterId);
            writer.Write(quirkIdInt);
        }

        public void Deserialize(NetworkReader reader)
        {
            giverMasterId = reader.ReadNetworkId();
            targetMasterId = reader.ReadNetworkId();
            quirkIdInt = reader.ReadInt32();
        }

        public void OnReceived()
        {
            if (!NetworkServer.active) return;

            var giverObj = Util.FindNetworkObject(giverMasterId);
            var targetObj = Util.FindNetworkObject(targetMasterId);
            if (!giverObj || !targetObj) return;

            var giverMaster = giverObj.GetComponent<CharacterMaster>();
            var targetMaster = targetObj.GetComponent<CharacterMaster>();
            if (!giverMaster || !targetMaster) return;

            var targetBody = targetMaster.GetBody();
            if (!targetBody) return;

            var id = (QuirkId)quirkIdInt;


            // Only allow PASSIVE category & owned (if you want “owned-only”)
            if (!QuirkRegistry.TryGet(id, out var rec) || rec.Category != QuirkCategory.Passive)
                return;


            // Validate team/range
            if (!ShiggyMod.Modules.Quirks.QuirkGrant.ValidateGiverAndTarget(giverMaster, targetBody))
                return;

            // --- Apply ---
            bool applied = ShiggyMod.Modules.Quirks.QuirkGrant.ApplyPassiveByIdServer(targetBody, id);
            if (!applied) return;

            // Feedback
            var disp = QuirkRegistry.GetDisplayName(id);
            Chat.AddMessage($"<style=cIsUtility>Gave {disp} to {targetBody.GetUserName()}.</style>");
        }
    }
}
