using EntityStates;
using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;
using EntityStates.GolemMonster;
using System.Linq;

namespace ShiggyMod.SkillStates
{
    public class StoneGolemLaserCharge : BaseSkillState
    {
        public float baseDuration = 1f;
        public float duration;
        public ShiggyController Shiggycon;
        private DamageType damageType;


		private float baseRadius = 2f;
		private float radius;
        private float damageCoefficient;
        private float procCoefficient = 1f;
        private float force = 1000f;
        private float speedOverride = -1f;

        private Ray modifiedAimRay;
		public Vector3 laserDirection;
        private uint chargePlayID;
        private GameObject chargeEffect;
        private GameObject laserEffect;
        private LineRenderer laserLineComponent;
        private float flashTimer;
        private bool laserOn;
        private float chargePercent;
        private float maxCharge;
		private int baseMaxCharge = 2;

		public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / this.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.characterBody.SetAimTimer(this.duration);

			base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            PlayCrossfade("RightArm, Override", "RightArmOut", "Attack.playbackRate", duration, 0.05f);

            float[] source = new float[]
			{
				this.attackSpeedStat,
				4f
			};

			this.maxCharge = (float)this.baseMaxCharge / source.Min();

			Transform modelTransform = base.GetModelTransform();
			this.chargePlayID = Util.PlayAttackSpeedSound(ChargeLaser.attackSoundString, base.gameObject, this.attackSpeedStat);
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild("LHand");
					if (transform)
					{
						if (ChargeLaser.effectPrefab)
						{
							this.chargeEffect = UnityEngine.Object.Instantiate<GameObject>(ChargeLaser.effectPrefab, transform.position, transform.rotation);
							this.chargeEffect.transform.parent = transform;
							ScaleParticleSystemDuration component2 = this.chargeEffect.GetComponent<ScaleParticleSystemDuration>();
							if (component2)
							{
								component2.newDuration = this.duration;
							}
						}
						if (ChargeLaser.laserPrefab)
						{
							this.laserEffect = UnityEngine.Object.Instantiate<GameObject>(ChargeLaser.laserPrefab, transform.position, transform.rotation);
							this.laserEffect.transform.parent = transform;
							this.laserLineComponent = this.laserEffect.GetComponent<LineRenderer>();
						}
					}
				}
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration);
			}
			this.flashTimer = 0f;
			this.laserOn = true;


			Shiggycon = gameObject.GetComponent<ShiggyController>();
			

		}

        public override void OnExit()
        {
            base.OnExit();
			AkSoundEngine.StopPlayingID(this.chargePlayID);
			base.OnExit();
			if (this.chargeEffect)
			{
				EntityState.Destroy(this.chargeEffect);
			}
			if (this.laserEffect)
			{
				EntityState.Destroy(this.laserEffect);
			}
		}
		public override void Update()
		{
			base.Update();
			if (this.laserEffect && this.laserLineComponent)
			{
				float num = 1000f;
				Ray aimRay = base.GetAimRay();
				Vector3 position = this.laserEffect.transform.parent.position;
				Vector3 point = aimRay.GetPoint(num);
				this.laserDirection = point - position;
				RaycastHit raycastHit;
				if (Physics.Raycast(aimRay, out raycastHit, num, LayerIndex.world.mask | LayerIndex.entityPrecise.mask))
				{
					point = raycastHit.point;
				}
				this.laserLineComponent.SetPosition(0, position);
				this.laserLineComponent.SetPosition(1, point);
				float num2;
				if (this.duration - base.age > 0.5f)
				{
					num2 = base.age / this.duration;
				}
				else
				{
					this.flashTimer -= Time.deltaTime;
					if (this.flashTimer <= 0f)
					{
						this.laserOn = !this.laserOn;
						this.flashTimer = 0.033333335f;
					}
					num2 = (this.laserOn ? 1f : 0f);
				}
				num2 *= ChargeLaser.laserMaxWidth;
				this.laserLineComponent.startWidth = num2;
				this.laserLineComponent.endWidth = num2;
			}
		}

		public override void FixedUpdate()
        {
            base.FixedUpdate();
			bool flag = base.IsKeyDownAuthority();
			if (flag)
			{
				PlayAnimation("RightArm, Override", "RightArmOut", "Attack.playbackRate", duration);
				this.chargePercent = base.fixedAge / this.maxCharge;
				this.damageCoefficient = (Modules.StaticValues.stonegolemDamageCoefficient + 1f * (this.chargePercent * Modules.StaticValues.stonegolemDamageCoefficient));
				this.radius = (this.baseRadius * this.damageCoefficient + 20f) / 4f;
			}
            else
			{
				if (base.fixedAge >= this.duration && base.isAuthority)
				{
					StoneGolemLaserFire fireLaser = new StoneGolemLaserFire();
					fireLaser.laserDirection = this.laserDirection;
					fireLaser.damageCoefficient = this.damageCoefficient;
					fireLaser.radius = this.radius;
					this.outer.SetNextState(fireLaser);
					return;
				}

			}
		}




        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }

    }
}
