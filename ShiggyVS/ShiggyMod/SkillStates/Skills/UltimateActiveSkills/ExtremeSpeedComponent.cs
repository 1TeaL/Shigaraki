using RoR2;
using UnityEngine;
using ShiggyMod.Modules.Networking;
using R2API.Networking;
using R2API.Networking.Interfaces;
using ShiggyMod.Modules;
using Random = UnityEngine.Random;

namespace ShiggyMod.SkillStates
{
    public class ExtremeSpeedComponent : MonoBehaviour
	{
		public CharacterBody charbody;
		public CharacterBody shiggycharbody;
		public float numberOfHits;
		public float currentNumber;
		public float timer;
		public float damage;
		public float interval;

		public void Start()
		{
			charbody = this.gameObject.GetComponent<CharacterBody>();
			//effectObj = UnityEngine.Object.Instantiate<GameObject>(Modules.Assets.detroitEffect, charbody.footPosition, Quaternion.LookRotation(Vector3.up));
			//effectObj.transform.parent = charbody.gameObject.transform;

			currentNumber = 0f;

        }
	


		public void FixedUpdate()
		{

            if (charbody.healthComponent.alive)
			{
				timer += Time.fixedDeltaTime;
				if (timer > 1f)
				{
					if (currentNumber < numberOfHits)
					{
						currentNumber += 1;
						timer -= interval;
                        AkSoundEngine.PostEvent("ShiggyHitSFX", charbody.gameObject);
                        new TakeMeleeDamageForceRequest(charbody.masterObjectId, Vector3.up, StaticValues.extremeSpeedForce / 2f, damage, shiggycharbody.masterObjectId).Send(NetworkDestination.Server);
                        EffectManager.SpawnEffect(Modules.Assets.shiggyHitImpactEffect, new EffectData
                        {
                            origin = charbody.corePosition,
                            scale = 1f,
                            rotation = Quaternion.identity

                        }, true);
                    }
					else if (currentNumber == numberOfHits)
					{
						AkSoundEngine.PostEvent("ShiggyStrongAttack", charbody.gameObject);
						currentNumber += 1;
						new TakeMeleeDamageForceRequest(charbody.masterObjectId, Vector3.down, StaticValues.extremeSpeedForce, damage, shiggycharbody.masterObjectId).Send(NetworkDestination.Server);
                        EffectManager.SpawnEffect(Modules.Assets.shiggyHitImpactEffect, new EffectData
                        {
                            origin = charbody.corePosition,
                            scale = 1f,
                            rotation = Quaternion.identity

                        }, true);
                    }
					else if (currentNumber > numberOfHits)
					{

						Destroy(this);
					}

				}

			}
			else if (!charbody)
			{
				Destroy(this);
			}
		}


	}
}
