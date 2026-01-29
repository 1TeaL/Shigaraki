using EntityStates;
using R2API.Networking;
using R2API.Networking.Interfaces;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Networking;
using ShiggyMod.Modules.Survivors;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ShiggyMod.SkillStates
{
    public class SolusDistributorPlantMine : Skill
    {
        int numberToSpawn;

        public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);

            Shiggycon = gameObject.GetComponent<ShiggyController>();


            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);

            int randomAnim = UnityEngine.Random.Range(1, 2);
            base.PlayCrossfade("LeftArm, Override", "LHandSnap" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            //base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", duration, 0.05f);
            AkSoundEngine.PostEvent("ShiggyGacha", base.gameObject);



        }




        public override void OnExit()
        {
            base.OnExit();
            //PlayCrossfade("RightArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
            //PlayCrossfade("LeftArm, Override", "BufferEmpty", "Attack.playbackRate", 0.1f, 0.1f);
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if(base.fixedAge > fireTime && !hasFired)
            {
                hasFired = true;

                new SpawnBodyNetworkRequest(characterBody.masterObjectId, Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_SolusMine.SolusMineBody_prefab).WaitForCompletion(), Addressables.LoadAssetAsync<GameObject>(RoR2BepInExPack.GameAssetPaths.Version_1_39_0.RoR2_DLC3_SolusMine.SolusMineMaster_prefab).WaitForCompletion(), Buffs.solusPrimedBuff.buffIndex).Send(NetworkDestination.Server);
            }


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
