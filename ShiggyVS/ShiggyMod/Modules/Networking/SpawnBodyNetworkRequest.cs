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
        private Vector3 location;
        private string masterName;
        NetworkInstanceId netID;

        //Don't network these.

        public SpawnBodyNetworkRequest()
        {

        }

        public SpawnBodyNetworkRequest(NetworkInstanceId netID, string masterName, string bodyName, Vector3 vector3)
        {
            this.netID = netID;
            this.masterName = masterName;
            this.bodyName = bodyName;
            location = vector3;
        }

        public void Deserialize(NetworkReader reader)
        {
            netID = reader.ReadNetworkId();
            masterName = reader.ReadString();
            bodyName = reader.ReadString();
            location = reader.ReadVector3();
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netID);
            writer.Write(masterName);
            writer.Write(bodyName);
            writer.Write(location);
        }

        public void OnReceived()
        {
            if (NetworkServer.active)
            {
                GameObject masterobject = Util.FindNetworkObject(netID);
                CharacterMaster charMaster = masterobject.GetComponent<CharacterMaster>();
                CharacterBody charBody = charMaster.GetBody();

                var monsterMaster = MasterCatalog.FindMasterPrefab(masterName);
                var bodyGameObject = Object.Instantiate(monsterMaster.gameObject, location, Quaternion.identity);
                var master = bodyGameObject.GetComponent<CharacterMaster>();


                MasterSummon summonAlly = new MasterSummon();
                summonAlly.masterPrefab = monsterMaster;
                summonAlly.ignoreTeamMemberLimit = true;
                summonAlly.summonerBodyObject = monsterMaster.GetComponent<CharacterBody>().gameObject;
                summonAlly.teamIndexOverride = TeamIndex.Player;
                summonAlly.inventoryToCopy = charBody.inventory;
                summonAlly.position = location;
                summonAlly.rotation = charBody.transform.rotation;
                summonAlly.useAmbientLevel = true;

                master = summonAlly.Perform();                

            }

        }
    }
}
