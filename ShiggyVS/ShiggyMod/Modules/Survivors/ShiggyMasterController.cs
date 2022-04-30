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
        public float alphaconstructshieldtimer;


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

            if (self.hasEffectiveAuthority)
            {
                if (self.HasBuff(Modules.Buffs.alphashieldoffBuff.buffIndex))
                {

                    if (alphaconstructshieldtimer > 1f)
                    {
                        int buffCountToApply = self.GetBuffCount(Modules.Buffs.alphashieldoffBuff.buffIndex);
                        if (buffCountToApply > 1)
                        {
                            if (buffCountToApply >= 2)
                            {
                                self.RemoveBuff(Modules.Buffs.alphashieldoffBuff.buffIndex);
                                alphaconstructshieldtimer = 0f;
                            }
                        }
                        else
                        {
                            self.RemoveBuff(Modules.Buffs.alphashieldoffBuff.buffIndex);
                            self.AddBuff(Modules.Buffs.alphashieldonBuff);

                        }
                    }

                    else alphaconstructshieldtimer += Time.fixedDeltaTime;
                }

            }


        }

    }
}
