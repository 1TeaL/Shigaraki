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
    public class ForceReversalState : INetMessage
    {
        //network
        NetworkInstanceId netID;
        Vector3 enemyPos;

        //don't network
        private GameObject bodyObj;

        public ForceReversalState()
        {

        }

        public ForceReversalState(NetworkInstanceId netID, Vector3 enemyPos)
        {
            this.netID = netID;
            this.enemyPos = enemyPos;       
        }

        public void Deserialize(NetworkReader reader)
        {
            enemyPos = reader.ReadVector3();
            netID = reader.ReadNetworkId();
        }
        public void Serialize(NetworkWriter writer)
        {
            writer.Write(enemyPos);
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


                EntityStateMachine[] statemachines = charbodyobj.GetComponents<EntityStateMachine>();
                foreach (EntityStateMachine statemachine in statemachines)
                {
                    if (statemachine.customName == "Body")
                    {
                        statemachine.SetState(new ReversalState
                        {
                            enemyPos = enemyPos,
                        });
                    }
                }

            }
        }

    }
}
