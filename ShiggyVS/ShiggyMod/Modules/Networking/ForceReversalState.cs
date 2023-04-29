﻿using System;
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
        NetworkInstanceId IDNet;
        NetworkInstanceId enemyNetID;

        //don't network
        private GameObject bodyObj;

        public ForceReversalState()
        {

        }

        public ForceReversalState(NetworkInstanceId netID, NetworkInstanceId enemyNetID)
        {
            this.IDNet = IDNet;
            this.enemyNetID = enemyNetID;       
        }

        public void Deserialize(NetworkReader reader)
        {
            enemyNetID = reader.ReadNetworkId();
            IDNet = reader.ReadNetworkId();
        }
        public void Serialize(NetworkWriter writer)
        {
            writer.Write(enemyNetID);
            writer.Write(IDNet);
        }
        public void OnReceived()
        {
            if (NetworkServer.active)
            {
                GameObject masterobj = Util.FindNetworkObject(IDNet);
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
                    if (statemachine.customName == "Body")
                    {
                        statemachine.SetState(new ReversalState
                        {
                            enemycharBody = enemycharBody,
                        });
                    }
                }

            }
        }

    }
}