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
    public class EquipmentDropNetworked : INetMessage
    {
        PickupIndex pickup;
        Vector3 position;
        Vector3 velocity;
        private object netId;

        public EquipmentDropNetworked()
        {

        }

        public EquipmentDropNetworked(PickupIndex pickup, Vector3 position, Vector3 velocity)
        {
            this.pickup = pickup;
            this.position = position;
            this.velocity = velocity;
        }

        public EquipmentDropNetworked(object netId)
        {
            this.netId = netId;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(pickup);
            writer.Write(position);
            writer.Write(velocity);
        }
        public void Deserialize(NetworkReader reader)
        {
            pickup = reader.ReadPickupIndex();
            position = reader.ReadVector3();
            velocity = reader.ReadVector3();
        }

        public void OnReceived()
        {
            if (NetworkServer.active)
            {
                PickupDropletController.CreatePickupDroplet(pickup, position, velocity);
            }
        }

    }
}
