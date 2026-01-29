using R2API.Networking.Interfaces;
using RoR2;
using ShiggyMod.SkillStates;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.Modules.Networking
{
    public class SetAFOStealStateMachine : INetMessage
    {
        //network
        NetworkInstanceId netID;

        public SetAFOStealStateMachine()
        {

        }

        public SetAFOStealStateMachine(NetworkInstanceId netID)
        {
            this.netID = netID;
        }

        public void Deserialize(NetworkReader reader)
        {
            netID = reader.ReadNetworkId();
        }
        public void Serialize(NetworkWriter writer)
        {
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
                        statemachine.SetState(new AFOSteal
                        {
                        });
                    }
                }

            }
        }

    }
}
