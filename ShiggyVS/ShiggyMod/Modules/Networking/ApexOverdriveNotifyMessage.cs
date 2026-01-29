namespace ShiggyMod.Modules.Networking
{
    using R2API.Networking.Interfaces;
    using RoR2;
    using ShiggyMod.Modules.Survivors;
    using UnityEngine.Networking;

    internal class ApexOverdriveNotifyMessage : INetMessage
    {
        private NetworkInstanceId masterNetId;

        public ApexOverdriveNotifyMessage() { }

        public ApexOverdriveNotifyMessage(NetworkInstanceId masterNetId)
        {
            this.masterNetId = masterNetId;
        }

        public void Serialize(NetworkWriter w) => w.Write(masterNetId);

        public void Deserialize(NetworkReader r) => masterNetId = r.ReadNetworkId();

        public void OnReceived()
        {
            if (!NetworkClient.active) return;

            var masterGO = Util.FindNetworkObject(masterNetId);
            if (!masterGO) return;

            var master = masterGO.GetComponent<CharacterMaster>();
            if (!master) return;

            var localMaster = LocalUserManager.GetFirstLocalUser()?.cachedMaster;
            if (!localMaster || localMaster != master) return;

            var body = master.GetBody();
            if (!body) return;

            var es = body.GetComponent<EnergySystem>();
            if (es != null)
                es.quirkGetInformation("<style=cDeath>Quirk Overdrive!</style>", 4f);
            else
                Chat.AddMessage("<style=cDeath>Quirk Overdrive!</style>");
        }
    }
}
