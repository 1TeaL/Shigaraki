using RoR2;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;
using EntityStates;
using ShiggyMod.Modules.Survivors;
using System.Collections.Generic;
using EntityStates.Loader;

namespace ShiggyMod.SkillStates
{
	public class BisonCharge : BaseSkillState
	{
		string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";
		private DamageType damageType;
		private Vector3 idealDirection;

		private float totalDuration;
		private float hitradius = 15f;
		private float damageCoefficient = Modules.StaticValues.bisonchargeDamageCoeffecient;
		private float procCoefficient = 1f;
		private float force = 1f;
        private Vector3 direction;
        private ShiggyController Shiggycon;

        public override void OnEnter()
		{
			base.OnEnter();
			Ray aimRay = base.GetAimRay();
			base.StartAimMode(aimRay, 2f, false);
			totalDuration = 0f;
			damageType = DamageType.Stun1s;
			bool isAuthority = base.isAuthority;
            Util.PlaySound(BaseChargeFist.startChargeLoopSFXString, base.gameObject);
            //Util.PlaySound(EntityStates.Bison.Charge.startSoundString, base.gameObject);
			bool flag = isAuthority;
			if (flag)
			{
				base.characterBody.baseAcceleration = 320f;
				base.characterDirection.turnSpeed = 100f;
				base.characterMotor.walkSpeedPenaltyCoefficient = 0f;
				base.cameraTargetParams.fovOverride = 90f;
				bool flag2 = base.inputBank;
				if (flag2)
				{
					this.idealDirection = base.inputBank.aimDirection;
					this.idealDirection.y = 0f;
					base.characterBody.isSprinting = true;
					this.UpdateDirection();
				}
			}

			Shiggycon = gameObject.GetComponent<ShiggyController>();
			damageCoefficient *= Shiggycon.strengthMultiplier;
		}

		private void UpdateDirection()
		{
			bool flag = base.inputBank;
			if (flag)
			{
				Vector2 vector = Util.Vector3XZToVector2XY(base.inputBank.moveVector);
				bool flag2 = vector != Vector2.zero;
				if (flag2)
				{
					vector.Normalize();
					this.idealDirection = (base.characterMotor.moveDirection.normalized + new Vector3(vector.x, 0f, vector.y).normalized).normalized;
				}
			}
		}

		private Vector3 GetIdealVelocity()
		{
			return base.characterDirection.forward * base.characterBody.moveSpeed * 2.25f;
		}

		public override void OnExit()
		{
			bool isAuthority = base.isAuthority;
			if (isAuthority)
			{
				base.characterBody.baseAcceleration = 40f;
				base.characterDirection.turnSpeed = 720f;
				base.characterMotor.walkSpeedPenaltyCoefficient = 1f;
				base.cameraTargetParams.fovOverride = -1f;
				base.characterBody.isSprinting = false;
				base.OnExit();
			}
            Util.PlaySound(BaseChargeFist.endChargeLoopSFXString, base.gameObject);
            Util.PlaySound(EntityStates.Bison.Charge.endSoundString, base.gameObject);
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();
			totalDuration += Time.fixedDeltaTime / 2;

			if (base.IsKeyDownAuthority())
            {
				Loop();
            }
            else
            {
				base.outer.SetNextStateToMain();
            }

		}


		public void Loop()
		{
			bool isAuthority = base.isAuthority;
			if (isAuthority)
			{
				Ray aimRay = base.GetAimRay();
				EffectManager.SpawnEffect(Modules.Assets.bisonEffect, new EffectData
				{
					origin = base.transform.position,
					scale = 1f,
					rotation = Quaternion.LookRotation(aimRay.direction)
				}, true);

				this.direction = base.GetAimRay().direction.normalized;
				base.characterDirection.forward = this.direction;

				base.characterBody.isSprinting = true;
				base.characterDirection.moveVector = this.idealDirection;
				base.characterMotor.rootMotion += this.GetIdealVelocity() * Time.fixedDeltaTime;
				Vector3 position = base.transform.position + base.characterDirection.forward.normalized * 0.5f;
				float radius = 0.2f;
				LayerIndex layerIndex = LayerIndex.world;
				int num = layerIndex.mask;
				layerIndex = LayerIndex.entityPrecise;
				int num2 = Physics.OverlapSphere(position, radius, num | layerIndex.mask).Length;
				bool flag2 = num2 != 0;
				if (flag2)
				{
					base.characterMotor.velocity = Vector3.zero;

					for (int i = 0; i <= 2; i += 1)
					{
						Vector3 effectPosition = base.characterBody.transform.position + (UnityEngine.Random.insideUnitSphere * 2f);
						effectPosition.y = base.characterBody.transform.position.y;
						EffectManager.SpawnEffect(EntityStates.Bison.Charge.hitEffectPrefab, new EffectData
						{
							origin = effectPosition,
							scale = hitradius * totalDuration,
							rotation = Quaternion.LookRotation(aimRay.direction)
						}, true);
					}


					BlastAttack blastAttack = new BlastAttack();
					blastAttack.radius = hitradius * totalDuration;
					blastAttack.procCoefficient = procCoefficient;
					blastAttack.position = base.transform.position;
					blastAttack.attacker = base.gameObject;
					blastAttack.crit = base.RollCrit();
					blastAttack.baseDamage = base.characterBody.damage * damageCoefficient * (moveSpeedStat / 7) * totalDuration;
					blastAttack.falloffModel = BlastAttack.FalloffModel.None;
					blastAttack.baseForce = force;
					blastAttack.teamIndex = base.teamComponent.teamIndex;
					blastAttack.damageType = damageType;
					blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;

                    Util.PlaySound(EntityStates.Bison.Headbutt.attackSoundString, base.gameObject);

					AkSoundEngine.PostEvent(3208241451, base.gameObject);
					if (base.HasBuff(Modules.Buffs.multiplierBuff))
					{
						ApplyDoT();
						ApplyDoT();
						ApplyDoT();
					}
					else
					{
						ApplyDoT();
					}
					if (blastAttack.Fire().hitCount > 0)
					{
						this.OnHitEnemyAuthority();
					}
					this.outer.SetNextStateToMain();
				}
				else
				{
					this.UpdateDirection();
				}

			}
			else
			{
				this.outer.SetNextStateToMain();
			}
		}

		public void ApplyDoT()
		{
			Ray aimRay = base.GetAimRay();
			BullseyeSearch search = new BullseyeSearch
			{

				teamMaskFilter = TeamMask.GetEnemyTeams(base.GetTeam()),
				filterByLoS = false,
				searchOrigin = base.transform.position,
				searchDirection = UnityEngine.Random.onUnitSphere,
				sortMode = BullseyeSearch.SortMode.Distance,
				maxDistanceFilter = hitradius * totalDuration,
				maxAngleFilter = 360f
			};

			search.RefreshCandidates();
			search.FilterOutGameObject(base.gameObject);



			List<HurtBox> target = search.GetResults().ToList<HurtBox>();
			foreach (HurtBox singularTarget in target)
			{
				if (singularTarget)
				{
					if (singularTarget.healthComponent && singularTarget.healthComponent.body)
					{
						InflictDotInfo info = new InflictDotInfo();
						info.attackerObject = base.gameObject;
						info.victimObject = singularTarget.healthComponent.body.gameObject;
						info.duration = Modules.StaticValues.decayDamageTimer;
						info.dotIndex = Modules.Dots.decayDot;

						DotController.InflictDot(ref info);
					}
				}
			}
		}
		protected virtual void OnHitEnemyAuthority()
		{
			base.healthComponent.AddBarrierAuthority((healthComponent.fullCombinedHealth / 10) * (this.moveSpeedStat / 7));

		}
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Frozen;
		}

	}
}
