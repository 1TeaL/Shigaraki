// GivePassiveRequest.cs
using R2API.Networking.Interfaces;
using RoR2;
using ShiggyMod.Modules.Quirks;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;

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

            var giverBody = giverMaster.GetBody();
            var targetBody = targetMaster.GetBody();
            if (!giverBody || !targetBody) return;

            var id = (QuirkId)quirkIdInt;

            // Validate quirk record: must be passive + have BuffDef (since you apply via buff)
            if (!QuirkRegistry.TryGet(id, out var rec)) return;
            if (rec.Category != QuirkCategory.Passive) return;
            if (rec.BuffDef == null) return;

            // Validate team/range (your helper)
            if (!ShiggyMod.Modules.Quirks.QuirkGrant.ValidateGiverAndTarget(giverMaster, targetBody))
                return;

            // Validate giver OWNS the quirk (owned-only rule)
            var giverInv = QuirkInventory.Ensure(giverMaster);
            if (giverInv == null) return;
            if (!giverInv.Has(id)) return;

            // Apply
            bool applied = ShiggyMod.Modules.Quirks.QuirkGrant.ApplyPassiveByIdServer(targetBody, id);
            if (!applied) return;

            // Feedback (server-side -> send to giver's client)
            // IMPORTANT: don't try to use EnergySystem on master; it's on the body.
            var energySystem = giverBody.GetComponent<ShiggyMod.Modules.Survivors.EnergySystem>();
            if (energySystem != null)
            {
                var disp = QuirkRegistry.GetDisplayName(id);
                var targetName = SafeUserName(targetBody);
                energySystem.quirkGetInformation($"<style=cIsUtility>Gave {disp} to {targetName}.</style>", 1f);
            }
        }

        private static string SafeUserName(CharacterBody body)
        {
            try { return body ? body.GetUserName() : "(None)"; }
            catch { return body ? body.name : "(None)"; }
        }

    }
}
