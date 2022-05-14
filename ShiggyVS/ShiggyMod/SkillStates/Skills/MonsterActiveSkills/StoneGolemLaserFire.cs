using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using EntityStates.GolemMonster;

namespace ShiggyMod.SkillStates
{
    public class StoneGolemLaserFire : BaseSkillState
    {
        public float duration = 0.1f;
        public ShiggyController Shiggycon;
        private DamageType damageType;


		internal float radius;
		internal float damageCoefficient;
        private float procCoefficient = 1f;
        private float force = 1000f;
        private float speedOverride = -1f;

        private Ray modifiedAimRay;
		public Vector3 laserDirection;

		public override void OnEnter()
        {
            base.OnEnter();
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);
			damageType = DamageType.Stun1s;
			if (base.HasBuff(Modules.Buffs.impbossBuff))
			{
				damageType |= DamageType.BleedOnHit | DamageType.Stun1s;
			}
			if (base.HasBuff(Modules.Buffs.acridBuff))
			{
				damageType |= DamageType.PoisonOnHit | DamageType.Stun1s;
			}

			this.modifiedAimRay = base.GetAimRay();
			this.modifiedAimRay.direction = this.laserDirection;

			base.GetModelAnimator();
			Transform modelTransform = base.GetModelTransform();
			Util.PlaySound(FireLaser.attackSoundString, base.gameObject);
			string text = "LHand";

			base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
			PlayCrossfade("LeftArm, Override", "LeftArmPunch", "Attack.playbackRate", duration, 0.1f);
			AkSoundEngine.PostEvent(3660048432, base.gameObject);
			if (FireLaser.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireLaser.effectPrefab, base.gameObject, text, false);
			}
			if (base.isAuthority)
			{
				float num = 1000f;
				Vector3 vector = this.modifiedAimRay.origin + this.modifiedAimRay.direction * num;
				RaycastHit raycastHit;
				if (Physics.Raycast(this.modifiedAimRay, out raycastHit, num, LayerIndex.world.mask | LayerIndex.defaultLayer.mask | LayerIndex.entityPrecise.mask))
				{
					vector = raycastHit.point;
				}
				new BlastAttack
				{
					attacker = base.gameObject,
					inflictor = base.gameObject,
					teamIndex = TeamComponent.GetObjectTeam(base.gameObject),
					baseDamage = this.damageStat * damageCoefficient,
					baseForce = force,
					position = vector,
					radius = radius,
					falloffModel = BlastAttack.FalloffModel.None,
					bonusForce = force * this.modifiedAimRay.direction,
					damageType = damageType,
				}.Fire();
				Vector3 origin = this.modifiedAimRay.origin;
				if (modelTransform)
				{
					ChildLocator component = modelTransform.GetComponent<ChildLocator>();
					if (component)
					{
						int childIndex = component.FindChildIndex(text);
						if (FireLaser.tracerEffectPrefab)
						{
							EffectData effectData = new EffectData
							{
								origin = vector,
								start = this.modifiedAimRay.origin
							};
							effectData.SetChildLocatorTransformReference(base.gameObject, childIndex);
							EffectManager.SpawnEffect(FireLaser.tracerEffectPrefab, effectData, true);
							EffectManager.SpawnEffect(FireLaser.hitEffectPrefab, effectData, true);
						}
					}
				}
			}


		}

        public override void OnExit()
        {
            base.OnExit();
			PlayCrossfade("RightArm, Override", "Empty", "Attack.playbackRate", 0.1f, 0.1f);
			PlayCrossfade("LeftArm, Override", "Empty", "Attack.playbackRate", 0.1f, 0.1f);
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
