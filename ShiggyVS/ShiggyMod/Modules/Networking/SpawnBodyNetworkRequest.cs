using R2API;
using R2API.Networking.Interfaces;
using RoR2;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;


namespace ShiggyMod.Modules.Networking
{
    internal class SpawnBodyNetworkRequest : INetMessage
    {
        //Network these ones.
        private string bodyName;
        private string masterName;
        NetworkInstanceId netID;

        //Don't network these.

        public SpawnBodyNetworkRequest()
        {

        }

        public SpawnBodyNetworkRequest(NetworkInstanceId netID, string bodyName, string masterName)
        {
            this.netID = netID;
            this.bodyName = bodyName;
            this.masterName = masterName;
        }

        public void Deserialize(NetworkReader reader)
        {
            netID = reader.ReadNetworkId();
            bodyName = reader.ReadString();
            masterName = reader.ReadString();
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netID);
            writer.Write(bodyName);
            writer.Write(masterName);
        }

        public void OnReceived()
        {
            if (NetworkServer.active)
            {
                GameObject masterobject = Util.FindNetworkObject(netID);
                CharacterMaster charMaster = masterobject.GetComponent<CharacterMaster>();
                CharacterBody charBody = charMaster.GetBody();

                GameObject monsterMaster = PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/" + masterName), masterName + bodyName, true);
                //monsterMaster = MasterCatalog.FindMasterPrefab(bodyName);
                //var monsterMaster = MasterCatalog.FindMasterPrefab(bodyName);
                //var bodyGameObject = Object.Instantiate(monsterMaster.gameObject, charBody.transform.position + charBody.characterDirection.forward * 2f, Quaternion.identity);
                //var master = bodyGameObject.GetComponent<CharacterMaster>();

                //master.teamIndex = TeamIndex.Player;
                //NetworkServer.Spawn(bodyGameObject);
                //master.bodyPrefab = BodyCatalog.FindBodyPrefab(bodyName);
                //master.SpawnBody(charBody.transform.position + charBody.characterDirection.forward * 2f, Quaternion.identity);

                MasterSummon summonAlly = new MasterSummon();
                summonAlly.masterPrefab = monsterMaster;
                summonAlly.ignoreTeamMemberLimit = true;
                summonAlly.summonerBodyObject = masterobject;
                summonAlly.teamIndexOverride = TeamIndex.Player;
                summonAlly.inventoryToCopy = charBody.inventory;
                summonAlly.position = charBody.transform.position + charBody.characterDirection.forward * 2f;
                summonAlly.rotation = charBody.transform.rotation;
                summonAlly.useAmbientLevel = true;

                if (summonAlly != null)
                {
                    CharacterMaster master = summonAlly.Perform();
                }



            }

        }
    }
}
