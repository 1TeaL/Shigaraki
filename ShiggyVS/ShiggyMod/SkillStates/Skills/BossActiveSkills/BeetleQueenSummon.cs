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
using UnityEngine.AddressableAssets;

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

            if (NetworkServer.active)
            {
                randomSurvivor = UnityEngine.Random.RandomRangeInt(0, 16);
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
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidSurvivor.VoidSurvivorBody_prefab).WaitForCompletion(), Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidSurvivor.VoidSurvivorMonsterMaster_prefab).WaitForCompletion()).Send(NetworkDestination.Server);
            }
            if (rand == 1)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Commando.CommandoBody_prefab).WaitForCompletion(), Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Commando.CommandoMonsterMaster_prefab).WaitForCompletion()).Send(NetworkDestination.Server);
            }
            if (rand == 2)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Croco.CrocoBody_prefab).WaitForCompletion(), Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Croco.CrocoMonsterMaster_prefab).WaitForCompletion()).Send(NetworkDestination.Server);
            }
            if (rand == 3)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Mage.MageBody_prefab).WaitForCompletion(), Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Mage.MageMonsterMaster_prefab).WaitForCompletion()).Send(NetworkDestination.Server);
            }
            if (rand == 4)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bandit2.Bandit2Body_prefab).WaitForCompletion(), Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bandit2.Bandit2MonsterMaster_prefab).WaitForCompletion()).Send(NetworkDestination.Server);
            }
            if (rand == 5)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Captain.CaptainBody_prefab).WaitForCompletion(), Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Captain.CaptainBody_prefab).WaitForCompletion()).Send(NetworkDestination.Server);
            }
            if (rand == 6)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Engi.EngiBody_prefab).WaitForCompletion(), Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Engi.EngiMonsterMaster_prefab).WaitForCompletion()).Send(NetworkDestination.Server);
            }
            if (rand == 7)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Croco.CrocoBody_prefab).WaitForCompletion(), Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Croco.CrocoMonsterMaster_prefab).WaitForCompletion()).Send(NetworkDestination.Server);
            }
            if (rand == 8)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Loader.LoaderBody_prefab).WaitForCompletion(), Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Loader.LoaderMonsterMaster_prefab).WaitForCompletion()).Send(NetworkDestination.Server);
            }
            if (rand == 9)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Toolbot.ToolbotBody_prefab).WaitForCompletion(), Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Toolbot.ToolbotMonsterMaster_prefab).WaitForCompletion()).Send(NetworkDestination.Server);
            }
            if (rand == 10)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Merc.MercBody_prefab).WaitForCompletion(), Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Merc.MercMonsterMaster_prefab).WaitForCompletion()).Send(NetworkDestination.Server);
            }
            if (rand == 11)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Treebot.TreebotBody_prefab).WaitForCompletion(), Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Treebot.TreebotMonsterMaster_prefab).WaitForCompletion()).Send(NetworkDestination.Server);
            }
            if (rand == 12)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Railgunner.RailgunnerBody_prefab).WaitForCompletion(), Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Railgunner.RailgunnerMonsterMaster_prefab).WaitForCompletion()).Send(NetworkDestination.Server);
            }
            if (rand == 13)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_Chef.ChefBody_prefab).WaitForCompletion(), Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_Chef.ChefMonsterMaster_prefab).WaitForCompletion()).Send(NetworkDestination.Server);
            }
            if (rand == 14)
            {
                new SpawnBodyNetworkRequest(characterBody.masterObjectId, Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_FalseSon.FalseSonBody_prefab).WaitForCompletion(), Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_FalseSon.FalseSonMonsterMaster_prefab).WaitForCompletion()).Send(NetworkDestination.Server);
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
