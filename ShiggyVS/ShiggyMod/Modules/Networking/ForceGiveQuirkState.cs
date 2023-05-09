using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using R2API.Networking.Interfaces;
using UnityEngine;
using UnityEngine.Networking;
using ShiggyMod.SkillStates;

namespace ShiggyMod.Modules.Networking
{
    public class ForceGiveQuirkState : INetMessage
    {
        //network
        NetworkInstanceId netID;
        NetworkInstanceId enemyNetID;

        //don't network
        private GameObject bodyObj;

        public ForceGiveQuirkState()
        {

        }

        public ForceGiveQuirkState(NetworkInstanceId netID, NetworkInstanceId enemyNetID)
        {
            this.netID = netID;
            this.enemyNetID = enemyNetID;       
        }

        public void Deserialize(NetworkReader reader)
        {
            enemyNetID = reader.ReadNetworkId();
            netID = reader.ReadNetworkId();
        }
        public void Serialize(NetworkWriter writer)
        {
            writer.Write(enemyNetID);
            writer.Write(netID);
        }
        public void OnReceived()
        {
            if (NetworkServer.active)
            {
                GameObject masterobj = Util.FindNetworkObject(netID);
                if (!masterobj)
                {
                    Debug.Log("masterobj not found");
                    return;
                }
                CharacterMaster charmast = masterobj.GetComponent<CharacterMaster>();
                if (!charmast)
                {
                    Debug.Log("charmast not found");
                    return;
                }
                GameObject charbodyobj = charmast.GetBodyObject();
                if (!charbodyobj)
                {
                    Debug.Log("charbodyobj not found");
                    return;
                }

                GameObject enemymasterobject = Util.FindNetworkObject(enemyNetID);
                CharacterMaster enemycharMaster = enemymasterobject.GetComponent<CharacterMaster>();
                CharacterBody enemycharBody = enemycharMaster.GetBody();
                bodyObj = enemycharBody.gameObject;


                EntityStateMachine[] statemachines = charbodyobj.GetComponents<EntityStateMachine>();
                foreach (EntityStateMachine statemachine in statemachines)
                {
                    if (statemachine.customName == "Weapon")
                    {
                        statemachine.SetState(new GiveSkill
                        {
                            enemycharBody = enemycharBody,
                        });
                    }
                }

            }
        }

    }
}
