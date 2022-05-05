using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using RoR2.Audio;

namespace ShiggyMod.SkillStates
{
    public class XiConstructBeam : BaseSkillState
    {
        public float baseDuration = 0.3f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;


        private float radius = 15f;
        private float damageCoefficient = Modules.StaticValues.decayDamageCoeffecient;
        private float procCoefficient = 1f;
        private float force = 1f;
        private float speedOverride = -1f;
        private float fireTimer;
        private Transform modelTransform;
        public string muzzleString = "LHand";
        private GameObject laserEffectInstance;
        public LoopSoundDef loopSoundDef = Modules.Assets.xiconstructsound;
        private LoopSoundManager.SoundLoopPtr loopPtr;
        private GameObject laserPrefab;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);

            EffectManager.SimpleMuzzleFlash(Modules.Assets.xiconstructmuzzleEffect, base.gameObject, muzzleString, false);
            if (this.loopSoundDef)
            {
                this.loopPtr = LoopSoundManager.PlaySoundLoopLocal(base.gameObject, this.loopSoundDef);
            }
            this.fireTimer = 0f;
            this.modelTransform = base.GetModelTransform();
            if (this.modelTransform)
            {
                ChildLocator component = this.modelTransform.GetComponent<ChildLocator>();
                if (component)
                {
                    Transform transform = component.FindChild(this.muzzleString);
        
                    this.laserEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.laserPrefab, transform.position, transform.rotation);
                    this.laserEffectInstance.transform.parent = transform;
                    this.laserEffectInstanceEndTransform = this.laserEffectInstance.GetComponent<ChildLocator>().FindChild("LHand");
                    
                }
            }



        }

        public override void OnExit()
        {
            base.OnExit();
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextState(new XiConstructBeamFire());
            }
        }




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
