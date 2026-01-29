using R2API.Networking.Interfaces;
using RoR2;
using ShiggyMod.SkillStates;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.Modules.Networking
{
    /// <summary>
    /// Server -> Clients: play AFO steal VFX between attacker and victim.
    /// Adds AFOEffectController on victim locally on each client.
    /// </summary>
    internal class AFOStealVfxRequest : INetMessage
    {
        private NetworkInstanceId attackerMasterId;
        private NetworkInstanceId victimBodyId;

        public AFOStealVfxRequest() { }

        public AFOStealVfxRequest(NetworkInstanceId attackerMasterId, NetworkInstanceId victimBodyId)
        {
            this.attackerMasterId = attackerMasterId;
            this.victimBodyId = victimBodyId;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(attackerMasterId);
            writer.Write(victimBodyId);
        }

        public void Deserialize(NetworkReader reader)
        {
            attackerMasterId = reader.ReadNetworkId();
            victimBodyId = reader.ReadNetworkId();
        }

        public void OnReceived()
        {
            // Clients only (host client included). If server runs this too, it’s harmless.
            var attackerMasterObj = Util.FindNetworkObject(attackerMasterId);
            var victimBodyObj = Util.FindNetworkObject(victimBodyId);
            if (!attackerMasterObj || !victimBodyObj) return;

            var attackerMaster = attackerMasterObj.GetComponent<CharacterMaster>();
            var victimBody = victimBodyObj.GetComponent<CharacterBody>();
            if (!attackerMaster || !victimBody) return;

            var attackerBody = attackerMaster.GetBody();
            if (!attackerBody) return;

            // Find RHand
            Transform rHand = null;
            var cl = attackerBody.GetComponentInChildren<ChildLocator>();
            if (cl) rHand = cl.FindChild("RHand");
            if (!rHand && attackerBody.modelLocator && attackerBody.modelLocator.modelTransform)
                rHand = attackerBody.modelLocator.modelTransform; // fallback, not ideal but safe

            // Add the controller locally (must be done per-client; adding on server won't replicate)
            var con = victimBody.gameObject.GetComponent<AFOEffectController>();
            if (!con) con = victimBody.gameObject.AddComponent<AFOEffectController>();

            con.attackerBody = attackerBody;
            if (rHand) con.RHandChild = rHand;
        }
    }
}
