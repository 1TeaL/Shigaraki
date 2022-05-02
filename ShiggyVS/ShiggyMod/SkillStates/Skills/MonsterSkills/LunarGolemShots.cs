using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using EntityStates.LunarGolem;
using UnityEngine.Networking;
using RoR2.Projectile;

namespace ShiggyMod.SkillStates
{
    public class LunarGolemShots : BaseSkillState
    {
        public float baseDuration = 0.2f;
        public float duration;
        public ShiggyController Shiggycon;
        public bool hasFired;
        public string muzzleString = "Bronzong1";

        public static float minLeadTime = 0.5f;
        public static float maxLeadTime = 0.5f;
        private float damageCoefficient = Modules.StaticValues.lunargolemshotsDamageCoeffecient;
        private float force = 400f;
        private float speedOverride = -1f;
        private float baseAimDelay = 0.05f;
        private float aimDelay;
        private int refireIndex;
        private int refireCount;
        public static bool useSeriesFire = true;
        private Ray initialAimRay;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            initialAimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
            hasFired = false;
            if (base.HasBuff(Modules.Buffs.multiplierBuff))
            {
                refireCount = 6 * (int)Modules.StaticValues.multiplierCoefficient;

            }
            else
            {
                refireCount = 6;
            }

            this.aimDelay = baseAimDelay / this.attackSpeedStat;
            if (base.characterBody)
            {
                base.characterBody.SetAimTimer(FireTwinShots.aimTime);
            }
            Util.PlayAttackSpeedSound(FireTwinShots.attackSoundString, base.gameObject, FireTwinShots.fireSoundPlaybackRate);


        }

        public override void OnExit()
        {
            base.OnExit();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!hasFired && aimDelay <= base.fixedAge)
            {
                hasFired = true;
                if (base.isAuthority)
                {
                    this.Fire();
                }
            }
            if (base.fixedAge < this.duration || !base.isAuthority)
            {
                return;
            }
            if (this.refireIndex < refireCount)
            {
                this.outer.SetNextState(new LunarGolemShots
                {
                    refireIndex = this.refireIndex + 1
                });
                return;
            }
            this.outer.SetNextStateToMain();

        }

        private void Fire()
        {
            Ray aimRay = base.GetAimRay();
            Quaternion a = Quaternion.LookRotation(initialAimRay.direction);
            Quaternion b = Quaternion.LookRotation(aimRay.direction);
            float num = Util.Remap(Util.Remap((float)this.refireIndex, 0f, (float)(refireCount - 1), 0f, 1f), 0f, 1f, minLeadTime, maxLeadTime) / this.aimDelay;
            Quaternion rotation = Quaternion.SlerpUnclamped(a, b, 1f + num);
            Ray ray = new Ray(aimRay.origin, rotation * Vector3.forward);
            if (this.refireIndex == 0 && FireTwinShots.dustEffectPrefab)
            {
                EffectManager.SimpleMuzzleFlash(FireTwinShots.dustEffectPrefab, base.gameObject, muzzleString, false);
            }
            int num2 = this.refireIndex;
            if (!useSeriesFire)
            {
                num2 = this.refireIndex + 3;
            }
            while (this.refireIndex <= num2)
            {
                bool flipProjectile = false;
                switch (this.refireIndex % 4)
                {
                    case 0:
                        flipProjectile = true;
                        break;
                    case 1:
                        break;
                    case 2:
                        flipProjectile = true;
                        break;
                    case 3:
                        break;
                }
                this.FireSingle(muzzleString, ray.direction, flipProjectile);
                this.refireIndex++;
            }
            this.refireIndex--;
        }
        private void FireSingle(string muzzleName, Vector3 aimDirection, bool flipProjectile)
        {
            ChildLocator modelChildLocator = base.GetModelChildLocator();
            if (modelChildLocator)
            {
                Transform transform = modelChildLocator.FindChild(muzzleName);
                if (transform)
                {
                    if (FireTwinShots.effectPrefab)
                    {
                        EffectManager.SimpleMuzzleFlash(FireTwinShots.effectPrefab, base.gameObject, muzzleName, false);
                    }
                    if (base.isAuthority)
                    {
                        ProjectileManager.instance.FireProjectile(
                            FireTwinShots.projectilePrefab, //prefab
                            FindModelChild(this.muzzleString).position, //position
                             Util.QuaternionSafeLookRotation(aimDirection, (!flipProjectile) ? Vector3.up : Vector3.down), //rotation
                            base.gameObject, //owner
                            this.damageStat * damageCoefficient, //damage
                            force, //force
                            Util.CheckRoll(this.critStat, base.characterBody.master), //crit
                            DamageColorIndex.Default, //damage color
                            null, //target
                            speedOverride); //speed }
                    }
                }
            }
        }
        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.WritePackedUInt32((uint)this.refireIndex);
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            this.refireIndex = (int)reader.ReadPackedUInt32();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
