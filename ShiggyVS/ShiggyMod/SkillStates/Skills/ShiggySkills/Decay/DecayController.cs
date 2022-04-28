using RoR2;
using UnityEngine;

namespace ShiggyMod.Modules.Survivors
{
    public class DecayController : MonoBehaviour
    {
        public CharacterBody body;
        private GameObject effectObj;
        public float stopwatch;
        public DecayController instance;
        public int secondsToAdd = 0;

        public void Awake()
        {
            body = gameObject.GetComponent<CharacterBody>();
            //effectObj = Instantiate(Modules.Assets.gloomAuraPrefab, this.transform.position, Quaternion.identity);
            stopwatch = 0f;
        }

        public void Update()
        {
            //Handle transform of effectObj
            if (effectObj)
            {
                effectObj.transform.position = this.transform.position;
            }
        }

        public void FixedUpdate()
        {
            stopwatch += Time.fixedDeltaTime;
            if (body)
            {
                //If buff isn't present, destroy the effect and self.
                if (!body.HasBuff(Modules.Buffs.decayDebuff))
                {
                    Destroy(effectObj);
                    Destroy(this);
                }
            }
            //if (stopwatch > this.buffduration + secondsToAdd)
            //{
            //    body.baseMaxHealth -= Modules.StaticValues.moteHealth;
            //    Destroy(instance);
            //}

        }
    }
}

