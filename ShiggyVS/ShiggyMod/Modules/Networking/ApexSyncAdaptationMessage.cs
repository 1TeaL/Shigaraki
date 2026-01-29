namespace ShiggyMod.Modules.Networking
{
    using R2API.Networking.Interfaces;
    using RoR2;
    using ShiggyMod.Modules.Quirks;
    using UnityEngine.Networking;

    internal class ApexSyncAdaptationMessage : INetMessage
    {
        private NetworkInstanceId masterNetId;
        private int adaptation;

        public ApexSyncAdaptationMessage() { }

        public ApexSyncAdaptationMessage(NetworkInstanceId masterNetId, int adaptation)
        {
            this.masterNetId = masterNetId;
            this.adaptation = adaptation;
        }

        public void Serialize(NetworkWriter w)
        {
            w.Write(masterNetId);
            w.Write(adaptation);
        }

        public void Deserialize(NetworkReader r)
        {
            masterNetId = r.ReadNetworkId();
            adaptation = r.ReadInt32();
        }

        public void OnReceived()
        {
            if (!NetworkClient.active) return;

            var masterGO = Util.FindNetworkObject(masterNetId);
            if (!masterGO) return;

            var master = masterGO.GetComponent<CharacterMaster>();
            if (!master) return;

            // Only the owning local user should apply
            var localMaster = LocalUserManager.GetFirstLocalUser()?.cachedMaster;
            if (!localMaster || localMaster != master) return;

            var ctrl = master.GetComponent<ApexSurgeryController>();
            if (!ctrl) return;

            ctrl.Client_ApplyAdaptation(adaptation); // you add this method
        }
    }
}
