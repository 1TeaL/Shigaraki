using R2API.Networking.Interfaces;
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
        NetworkInstanceId enemynetID;
        float health;

        //Don't network these.
        GameObject bodyObj;
        GameObject enemybodyObj;

        public BlastingZoneDebuffDamageRequest()
        {

        }

        public BlastingZoneDebuffDamageRequest(NetworkInstanceId netID, NetworkInstanceId enemynetID, float health)
        {
            this.netID = netID;
            this.enemynetID = enemynetID;
            this.health = health;
        }

        public void Deserialize(NetworkReader reader)
        {
            netID = reader.ReadNetworkId();
            enemynetID = reader.ReadNetworkId();
            health = reader.ReadSingle();
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netID);
            writer.Write(enemynetID);
            writer.Write(health);
        }

        public void OnReceived()
        {

            GameObject masterobject = Util.FindNetworkObject(netID);
            CharacterMaster charMaster = masterobject.GetComponent<CharacterMaster>();
            CharacterBody charBody = charMaster.GetBody();
            bodyObj = charBody.gameObject;

            GameObject enemymasterobject = Util.FindNetworkObject(enemynetID);
            CharacterMaster enemycharMaster = enemymasterobject.GetComponent<CharacterMaster>();
            CharacterBody enemycharBody = enemycharMaster.GetBody();
            enemybodyObj = enemycharBody.gameObject;

            //deal health damage
            if (NetworkServer.active && charBody.healthComponent)
            {
                DamageInfo damageInfo = new DamageInfo();
                damageInfo.damage = health;
                damageInfo.position = enemycharBody.transform.position;
                damageInfo.force = Vector3.zero;
                damageInfo.damageColorIndex = DamageColorIndex.Bleed;
                damageInfo.crit = false;
                damageInfo.attacker = bodyObj;
                damageInfo.damageType = DamageType.DoT;
                damageInfo.procCoefficient = 0f;
                damageInfo.procChainMask = default(ProcChainMask);
                enemycharBody.healthComponent.TakeDamage(damageInfo);
            }

            

        }
    }
}
