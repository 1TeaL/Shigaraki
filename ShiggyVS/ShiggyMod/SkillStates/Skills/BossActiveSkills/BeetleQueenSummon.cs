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
            duration = baseDuration / attackSpeedStat;
            base.characterBody.SetAimTimer(this.duration);

            Shiggycon = gameObject.GetComponent<ShiggyController>();

            //check if client able to summon dlc survivors

            if (characterBody.hasAuthority)
            {
                randomSurvivor = UnityEngine.Random.RandomRangeInt(0, 12);
            }

            Summon(randomSurvivor);

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);

            base.PlayCrossfade("LeftArm, Override", "LHandSnap", "Attack.playbackRate", duration, 0.05f);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            AkSoundEngine.PostEvent("ShiggyGacha", base.gameObject);




        }

        public void Summon(int rand)
        {
            //Debug.Log("randomsurvivor " + randomSurvivor);
            if(rand == 0)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, "VoidSurvivorBody", "VoidSurvivorMonsterMaster").Send(NetworkDestination.Clients);
            }
            if (rand == 1)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, "CommandoBody", "CommandoMonsterMaster").Send(NetworkDestination.Clients);
            }
            if (rand == 2)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, "CrocoBody", "CrocoMonsterMaster").Send(NetworkDestination.Clients);
            }
            if (rand == 3)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, "MageBody", "MageMonsterMaster").Send(NetworkDestination.Clients);
            }
            if (rand == 4)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, "Bandit2Body", "Bandit2MonsterMaster").Send(NetworkDestination.Clients);
            }
            if (rand == 5)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, "CaptainBody", "CaptainMonsterMaster").Send(NetworkDestination.Clients);
            }
            if (rand == 6)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, "EngiBody", "EngiMonsterMaster").Send(NetworkDestination.Clients);
            }
            if (rand == 7)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, "HuntressBody", "HuntressMonsterMaster").Send(NetworkDestination.Clients);
            }
            if (rand == 8)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, "LoaderBody", "LoaderMonsterMaster").Send(NetworkDestination.Clients);
            }
            if (rand == 9)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, "ToolbotBody", "ToolbotMonsterMaster").Send(NetworkDestination.Clients);
            }
            if (rand == 10)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, "MercBody", "MercMonsterMaster").Send(NetworkDestination.Clients);
            }
            if (rand == 11)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, "TreebotBody", "TreebotMonsterMaster").Send(NetworkDestination.Clients);
            }
            if (rand == 12)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, "RailgunnerBody", "RailgunnerMonsterMaster").Send(NetworkDestination.Clients);
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
