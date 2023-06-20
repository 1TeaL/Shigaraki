using RoR2;
using System;
using ShiggyMod.Modules.Survivors;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;

namespace ShiggyMod.SkillStates
{
    internal class AFOEffectController : MonoBehaviour
    {
        public CharacterBody charBody;
        public Transform RHandChild;
        public CharacterBody attackerBody;
        public GameObject AFOLineEffect;
        public LineRenderer AFOLineRenderer;
        private float timer;

        public void Start()
        {
            charBody = gameObject.GetComponent<CharacterBody>();

            MakeLine();
        }

        //afo line renderer effect
        public void MakeLine()
        {
            if (!AFOLineEffect)
            {
                AFOLineEffect = UnityEngine.Object.Instantiate(Modules.Assets.AFOLineRenderer, RHandChild);
                AFOLineRenderer = AFOLineEffect.GetComponent<LineRenderer>();
            }
        }

        public void Update()
        {
            //Handle transform of effectObj
            if (AFOLineEffect)
            {
                AFOLineEffect.transform.position = this.transform.position;
                LineVec();
            }
        }
        public void LineVec()
        {
            Vector3 startPos = RHandChild.position;
            Vector3 endPos = charBody.corePosition;
            int interVal = (int)Mathf.Abs(Vector3.Distance(endPos, startPos));
            if (interVal <= 0)
            {
                interVal = 2;
            }
            Vector3[] numberofpositions = new Vector3[interVal];
            for (int i = 0; i < numberofpositions.Length; i++)
            {
                numberofpositions[i] = Vector3.Lerp(startPos, endPos, (float)i / interVal);
                numberofpositions[i].z = Mathf.Lerp(startPos.z, endPos.z, (float)i / interVal);
            }
            AFOLineRenderer.positionCount = interVal;
            AFOLineRenderer.SetPositions(numberofpositions);

        }


        public void FixedUpdate()
        {

            timer += Time.fixedDeltaTime;
            if (timer > 1f)
            {
                Destroy(this);
                if (AFOLineEffect)
                {
                    Destroy(AFOLineEffect);
                }

            }
            if (charBody)
            {

            }
            else if (!charBody)
            {
                if (AFOLineEffect)
                {
                    Destroy(AFOLineEffect);
                }
            }
        }

    }
}
