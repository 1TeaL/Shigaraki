//using System;
//using System.Collections.Generic;
//using System.Text;
//using RoR2;
//using R2API.Networking.Interfaces;
//using UnityEngine;
//using UnityEngine.Networking;
//using DittoMod.SkillStates;

//namespace DittoMod.Modules.Networking
//{
//    public class ForceCounterState : INetMessage
//    {
//        NetworkInstanceId IDNet;
//        Vector3 enemyPos;

//        public ForceCounterState()
//        {

//        }

//        public ForceCounterState(NetworkInstanceId IDNet, Vector3 enemyPos)
//        {
//            this.IDNet = IDNet;
//            this.enemyPos = enemyPos;       
//        }

//        public void Deserialize(NetworkReader reader)
//        {
//            enemyPos = reader.ReadVector3();
//            IDNet = reader.ReadNetworkId();
//        }

//        public void OnReceived()
//        {
//            GameObject masterobj = Util.FindNetworkObject(IDNet);
//            if (!masterobj)
//            {
//                Debug.Log("masterobj not found");
//                return;
//            }
//            CharacterMaster charmast = masterobj.GetComponent<CharacterMaster>();
//            if (!charmast)
//            {
//                Debug.Log("charmast not found");
//                return;
//            }
//            GameObject charbodyobj = charmast.GetBodyObject();
//            if (!charbodyobj)
//            {
//                Debug.Log("charbodyobj not found");
//                return;
//            }
//            EntityStateMachine[] statemachines = charbodyobj.GetComponents<EntityStateMachine>();
//            foreach (EntityStateMachine statemachine in statemachines)
//            {
//                if (statemachine.customName == "Body")
//                {
//                    statemachine.SetState(new DangerSenseCounter
//                    {
//                        enemyPosition = enemyPos,
//                    });
//                }
//            }
//        }

//        public void Serialize(NetworkWriter writer)
//        {
//            writer.Write(enemyPos);
//            writer.Write(IDNet);
//        }
//    }
//}
