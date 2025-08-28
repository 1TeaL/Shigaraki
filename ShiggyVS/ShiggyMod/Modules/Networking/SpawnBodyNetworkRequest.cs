using R2API;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;


namespace ShiggyMod.Modules.Networking
{
    internal class SpawnBodyNetworkRequest : INetMessage
    {
        //Network these ones.
        private GameObject bodyPrefab;
        private GameObject masterPrefab;
        NetworkInstanceId netID;

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

        public void Deserialize(NetworkReader reader)
        {
            netID = reader.ReadNetworkId();
            bodyPrefab = reader.ReadGameObject();
            masterPrefab = reader.ReadGameObject();
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netID);
            writer.Write(bodyPrefab);
            writer.Write(masterPrefab);
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
