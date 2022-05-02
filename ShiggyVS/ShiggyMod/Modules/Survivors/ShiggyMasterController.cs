using EntityStates;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShiggyMod.Modules.Survivors
{
    [RequireComponent(typeof(CharacterBody))]
    [RequireComponent(typeof(TeamComponent))]
    [RequireComponent(typeof(InputBankTest))]
    public class ShiggyMasterController : MonoBehaviour
    {
        public ShiggyController Shiggycon;
        public ShiggyMasterController Shiggymastercon;
        private CharacterMaster characterMaster;
        private CharacterBody body;


        public bool alphaconstructQuirk;

        private void Awake()
        {
            alphaconstructQuirk = false;

            On.RoR2.CharacterModel.Awake += CharacterModel_Awake;

        }


        private void Start()
        {

            alphaconstructQuirk = false;

            characterMaster = gameObject.GetComponent<CharacterMaster>();
            CharacterBody self = characterMaster.GetBody();

            Shiggymastercon = characterMaster.gameObject.GetComponent<ShiggyMasterController>();
            Shiggycon = self.gameObject.GetComponent<ShiggyController>();

        }




        private void CharacterModel_Awake(On.RoR2.CharacterModel.orig_Awake orig, CharacterModel self)
        {
            orig(self);
            if (self.gameObject.name.Contains("ShiggyDisplay"))
            {
                alphaconstructQuirk = false;


            }

        }


        private void FixedUpdate()
        {
            characterMaster = gameObject.GetComponent<CharacterMaster>();
            CharacterBody self = characterMaster.GetBody();



        }

    }
}
