using System;
using EntityStates;
using EntityStates.VagrantMonster;
using R2API;
using RoR2;
using ShiggyMod.Modules;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    internal class MachPunchRelease : BaseSkillState
    {
        internal float damageMult;
        internal float radius;
        private GameObject muzzlePrefab = Assets.loaderMuzzleFlashEffect;
        //private string lMuzzleString = "LFinger";
        private string rMuzzleString = "RHand";
        internal Vector3 moveVec;
		//private GameObject explosionPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/MageLightningBombExplosion");
		private GameObject explosionPrefab = Assets.loaderOmniImpactLightningEffect;
		private float baseForce = StaticValues.machPunchForce;
		public float procCoefficient = StaticValues.machPunchProcCoefficient;

		public GameObject blastEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/SonicBoomEffect");



		public override void OnEnter()
        {
			
			base.OnEnter();
            base.characterMotor.velocity = Vector3.zero;
            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            int randomAnim = UnityEngine.Random.RandomRangeInt(0, 5);
			//base.PlayCrossfade("LeftArm, Override", "L" + randomAnim, "Attack.playbackRate", duration, 0.05f);
			base.PlayCrossfade("RightArm, Override", "R" + randomAnim, "Attack.playbackRate", 0.5f, 0.05f);
            //Util.PlaySound(FireMegaNova.novaSoundString, base.gameObject);
            AkSoundEngine.PostEvent(3289116818, this.gameObject);
			//EffectManager.SimpleMuzzleFlash(this.muzzlePrefab, base.gameObject, this.lMuzzleString, false);
			EffectManager.SimpleMuzzleFlash(this.muzzlePrefab, base.gameObject, this.rMuzzleString, false);
            base.characterMotor.rootMotion += this.moveVec;
            //base.characterMotor.velocity += this.moveVec * 2;

        }
        public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Frozen;
		}
		public override void OnExit()
		{

			for (int i = 0; i <= 20; i++)
			{
				float num = 60f;
				Quaternion rotation = Util.QuaternionSafeLookRotation(base.characterDirection.forward.normalized);
				float num2 = 0.01f;
				rotation.x += UnityEngine.Random.Range(-num2, num2) * num;
				rotation.y += UnityEngine.Random.Range(-num2, num2) * num;
				EffectManager.SpawnEffect(this.blastEffectPrefab, new EffectData
				{
					origin = base.characterBody.corePosition,
					scale = this.radius * 2,
					rotation = rotation
				}, false);
			}
				
			bool isAuthority = base.isAuthority;
			if (isAuthority)
			{
				BlastAttack blastAttack = new BlastAttack();

				blastAttack.position = base.characterBody.corePosition;
				blastAttack.baseDamage = this.damageStat * this.damageMult;
				blastAttack.baseForce = this.baseForce * this.damageMult;
				blastAttack.radius = this.radius;
				blastAttack.attacker = base.gameObject;
				blastAttack.inflictor = base.gameObject;
				blastAttack.teamIndex = base.teamComponent.teamIndex;
				blastAttack.crit = base.RollCrit();
				blastAttack.procChainMask = default(ProcChainMask);
				blastAttack.procCoefficient = procCoefficient;
				blastAttack.falloffModel = BlastAttack.FalloffModel.None;
				blastAttack.damageColorIndex = DamageColorIndex.Default;
				blastAttack.damageType = DamageType.Stun1s;
				blastAttack.attackerFiltering = AttackerFiltering.Default;

				DamageAPI.AddModdedDamageType(blastAttack, Damage.shiggyDecay);

				if (blastAttack.Fire().hitCount > 0)
				{
					this.OnHitEnemyAuthority();
				}
			}
			base.OnExit();

		}
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			bool flag = base.fixedAge >= 0.1f && base.isAuthority;
			if (flag)
			{
				this.outer.SetNextStateToMain();
			}
		}
		protected virtual void OnHitEnemyAuthority()
		{
			Ray aimRay = base.GetAimRay();
            AkSoundEngine.PostEvent("ShiggyStrongAttack", base.gameObject);

            EffectData effectData = new EffectData
			{
				scale = this.radius,
				origin = base.characterBody.corePosition,
				rotation = Quaternion.LookRotation(new Vector3(aimRay.direction.x, aimRay.direction.y, aimRay.direction.z)),
			};
			EffectManager.SpawnEffect(this.explosionPrefab, effectData, true);
		}
	}


}