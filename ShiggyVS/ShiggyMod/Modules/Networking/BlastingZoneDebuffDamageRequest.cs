﻿using R2API.Networking.Interfaces;
using RoR2;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;


namespace ShiggyMod.Modules.Networking
{
    internal class BlastingZoneDebuffDamageRequest : INetMessage
    {
        //Network these ones.
        NetworkInstanceId netID;
        float health;

        //Don't network these.
        GameObject bodyObj;

        public BlastingZoneDebuffDamageRequest()
        {

        }

        public BlastingZoneDebuffDamageRequest(NetworkInstanceId netID, float health)
        {
            this.netID = netID;
            this.health = health;
        }

        public void Deserialize(NetworkReader reader)
        {
            netID = reader.ReadNetworkId();
            health = reader.ReadSingle();
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netID);
            writer.Write(health);
        }

        public void OnReceived()
        {

            GameObject masterobject = Util.FindNetworkObject(netID);
            CharacterMaster charMaster = masterobject.GetComponent<CharacterMaster>();
            CharacterBody charBody = charMaster.GetBody();
            bodyObj = charBody.gameObject;

            //deal health damage
            if (NetworkServer.active && charBody.healthComponent)
            {
                DamageInfo damageInfo = new DamageInfo();
                damageInfo.damage = health;
                damageInfo.position = charBody.transform.position;
                damageInfo.force = Vector3.zero;
                damageInfo.damageColorIndex = DamageColorIndex.Default;
                damageInfo.crit = false;
                damageInfo.attacker = null;
                damageInfo.inflictor = null;
                damageInfo.damageType = DamageType.DoT;
                damageInfo.procCoefficient = 0f;
                damageInfo.procChainMask = default(ProcChainMask);
                charBody.healthComponent.TakeDamage(damageInfo);
            }

            

        }
    }
}
