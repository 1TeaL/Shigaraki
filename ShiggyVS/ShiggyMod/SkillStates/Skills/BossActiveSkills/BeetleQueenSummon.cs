using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using EntityStates.BeetleQueenMonster;
using RoR2.Projectile;
using System;
using R2API.Networking;
using ShiggyMod.Modules.Networking;
using R2API.Networking.Interfaces;
using RoR2.ExpansionManagement;
using System.Xml.Linq;

namespace ShiggyMod.SkillStates
{
    public class BeetleQueenSummon : BaseSkillState
    {


        public float baseDuration = 1f;
        public float duration;
        public ShiggyController Shiggycon;

        private int randomSurvivor;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);

            Shiggycon = gameObject.GetComponent<ShiggyController>();

            //check if client able to summon dlc survivors

            randomSurvivor = UnityEngine.Random.RandomRangeInt(1, 13);

            Summon();


        }

        public void Summon()
        {
            //Debug.Log("randomsurvivor " + randomSurvivor);

            switch (randomSurvivor)
            {
                case 1:
                    new SpawnBodyNetworkRequest(characterBody.masterObjectId, "CommandoBody", "CommandoMonsterMaster").Send(NetworkDestination.Clients);
                    break;
                case 2:
                    new SpawnBodyNetworkRequest(characterBody.masterObjectId, "CrocoBody", "CrocoMonsterMaster").Send(NetworkDestination.Clients);
                    break;
                case 3:
                    new SpawnBodyNetworkRequest(characterBody.masterObjectId, "MageBody", "MageMonsterMaster").Send(NetworkDestination.Clients);
                    break;
                case 4:
                    new SpawnBodyNetworkRequest(characterBody.masterObjectId, "Bandit2Body", "Bandit2MonsterMaster").Send(NetworkDestination.Clients);
                    break;
                case 5:
                    new SpawnBodyNetworkRequest(characterBody.masterObjectId, "CaptainBody", "CaptainMonsterMaster").Send(NetworkDestination.Clients);
                    break;
                case 6:
                    new SpawnBodyNetworkRequest(characterBody.masterObjectId, "EngiBody", "EngiMonsterMaster").Send(NetworkDestination.Clients);
                    break;
                case 7:
                    new SpawnBodyNetworkRequest(characterBody.masterObjectId, "HuntressBody", "HuntressMonsterMaster").Send(NetworkDestination.Clients);
                    break;
                case 8:
                    new SpawnBodyNetworkRequest(characterBody.masterObjectId, "LoaderBody", "LoaderMonsterMaster").Send(NetworkDestination.Clients);
                    break;
                case 9:
                    new SpawnBodyNetworkRequest(characterBody.masterObjectId, "ToolbotBody", "ToolbotMonsterMaster").Send(NetworkDestination.Clients);
                    break;
                case 10:
                    new SpawnBodyNetworkRequest(characterBody.masterObjectId, "MercBody", "MercMonsterMaster").Send(NetworkDestination.Clients);
                    break;
                case 11:
                    new SpawnBodyNetworkRequest(characterBody.masterObjectId, "TreebotBody", "TreebotMonsterMaster").Send(NetworkDestination.Clients);
                    break;
                case 12:
                        new SpawnBodyNetworkRequest(characterBody.masterObjectId, "RailgunnerBody", "RailgunnerMonsterMaster").Send(NetworkDestination.Clients);
                    break;
                case 13:
                        new SpawnBodyNetworkRequest(characterBody.masterObjectId, "VoidSurvivorBody", "VoidSurvivorMonsterMaster").Send(NetworkDestination.Clients);
                    break;
                //case 14:
                //    //summon deku if he exists otherwise commando
                //    new SpawnBodyNetworkRequest(characterBody.masterObjectId, "CommandoBody", "CommandoMonsterMaster").Send(NetworkDestination.Clients);
                //    break;
                    

            }
        }



        public override void OnExit()
        {
            base.OnExit();
            PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            //PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();



            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
