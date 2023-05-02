using R2API.Networking.Interfaces;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using ShiggyMod.SkillStates;

namespace ShiggyMod.Modules.Networking
{
    internal class SetMugetsuStateMachine : INetMessage
    {
        NetworkInstanceId netID;

        public SetMugetsuStateMachine()
        {

        }

        public SetMugetsuStateMachine(NetworkInstanceId netID)
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
            ForceGoBeyondState();
        }

        //Lots of checks in here.
        public void ForceGoBeyondState()
        {
            GameObject masterobject = Util.FindNetworkObject(netID);

            if (!masterobject)
            {
                Debug.Log("Specified GameObject not found!");
                return;
            }
            CharacterMaster charMaster = masterobject.GetComponent<CharacterMaster>();
            if (!charMaster)
            {
                Debug.Log("charMaster failed to locate");
                return;
            }

            if (!charMaster.hasEffectiveAuthority)
            {
                return;
            }

            GameObject bodyObject = charMaster.GetBodyObject();

            EntityStateMachine[] stateMachines = bodyObject.GetComponents<EntityStateMachine>();
            //"No statemachines?"
            if (!stateMachines[0])
            {
                Debug.LogWarning("StateMachine search failed! Wrong object?");
                return;
            }

            foreach (EntityStateMachine stateMachine in stateMachines)
            {
                if (stateMachine.customName == "Body")
                {
                    stateMachine.SetState(new FinalReleaseMugetsu());
                    return;
                }
            }
        }
    }
}
