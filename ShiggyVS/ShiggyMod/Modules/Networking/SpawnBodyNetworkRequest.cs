using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;


namespace ShiggyMod.Modules.Networking
{
    internal class SpawnBodyNetworkRequest : INetMessage
    {
        //Network these ones.
        private GameObject bodyPrefab;
        private GameObject masterPrefab;
        NetworkInstanceId netID;
        private BuffIndex buffIndex;

        //Don't network these.

        public SpawnBodyNetworkRequest()
        {

        }

        public SpawnBodyNetworkRequest(NetworkInstanceId netID, GameObject bodyPrefab, GameObject masterPrefab)
        {
            this.netID = netID;
            this.bodyPrefab = bodyPrefab;
            this.masterPrefab = masterPrefab;
        }
        public SpawnBodyNetworkRequest(NetworkInstanceId netID, GameObject bodyPrefab, GameObject masterPrefab, BuffIndex buffIndex)
        {
            this.netID = netID;
            this.bodyPrefab = bodyPrefab;
            this.masterPrefab = masterPrefab;
            this.buffIndex = buffIndex;
        }

        public void Deserialize(NetworkReader reader)
        {
            netID = reader.ReadNetworkId();
            bodyPrefab = reader.ReadGameObject();
            masterPrefab = reader.ReadGameObject();
            buffIndex = reader.ReadBuffIndex();
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netID);
            writer.Write(bodyPrefab);
            writer.Write(masterPrefab);
            writer.WriteBuffIndex(buffIndex);
        }

        public void OnReceived()
        {
            if (NetworkServer.active)
            {

                GameObject masterobject = Util.FindNetworkObject(netID);
                CharacterMaster charMaster = masterobject.GetComponent<CharacterMaster>();
                CharacterBody charBody = charMaster.GetBody();

                var monsterMaster = MasterCatalog.FindMasterPrefab(masterPrefab.name);
                var bodyGameObject = UnityEngine.Object.Instantiate(monsterMaster.gameObject, charBody.corePosition + charBody.characterDirection.forward * 5f, Quaternion.identity);
                var master = bodyGameObject.GetComponent<CharacterMaster>();


                master.teamIndex = charMaster.teamIndex;
                master.inventory = charMaster.inventory;


                NetworkServer.Spawn(bodyGameObject);
                master.bodyPrefab = BodyCatalog.FindBodyPrefab(bodyPrefab.name);
                master.SpawnBody(charBody.corePosition + charBody.characterDirection.forward * 5f, Quaternion.identity);

                //apply a buff if inputted
                if (buffIndex != null)
                {
                    master.GetComponent<CharacterBody>().ApplyBuff(buffIndex, 1);
                }


                //GameObject monsterMaster = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>("R").WaitForCompletion(), masterPrefab + bodyPrefab, true);

                //MasterSummon summonAlly = new MasterSummon();
                //summonAlly.masterPrefab = monsterMaster;
                //summonAlly.ignoreTeamMemberLimit = true;
                //summonAlly.summonerBodyObject = masterobject;
                //summonAlly.teamIndexOverride = TeamIndex.Player;
                //summonAlly.inventoryToCopy = charBody.inventory;
                //summonAlly.position = charBody.transform.position + charBody.characterDirection.forward * 2f;
                //summonAlly.rotation = charBody.transform.rotation;
                //summonAlly.useAmbientLevel = true;

                //if (summonAlly != null)
                //{
                //    CharacterMaster master = summonAlly.Perform();
                //}



            }

        }
    }
}
