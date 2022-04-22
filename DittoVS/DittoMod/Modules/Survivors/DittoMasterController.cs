using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DittoMod.Modules.Survivors
{
    [RequireComponent(typeof(CharacterBody))]
    [RequireComponent(typeof(TeamComponent))]
    [RequireComponent(typeof(InputBankTest))]
    public class DittoMasterController : MonoBehaviour
    {
        public DittoController dittocon;
        public DittoMasterController dittomastercon;
        private CharacterMaster characterMaster;
        private CharacterBody body;
        public bool transformed;
        //private int buffCountToApply;
        public float transformage;
        public bool assaultvest;
        public bool choiceband;
        public bool choicescarf;
        public bool choicespecs;
        public bool leftovers;
        public bool lifeorb;
        public bool luckyegg;
        public bool rockyhelmet;
        public bool scopelens;
        public bool shellbell;
        public bool assaultvest2;
        public bool choiceband2;
        public bool choicescarf2;
        public bool choicespecs2;
        public bool leftovers2;
        public bool lifeorb2;
        public bool luckyegg2;
        public bool rockyhelmet2;
        public bool scopelens2;
        public bool shellbell2;

        private void Awake()
        {
            transformed = false;
            assaultvest = false;
            choiceband = false;
            choicescarf = false;
            choicespecs = false;
            leftovers = false;
            lifeorb = false;
            luckyegg = false;
            rockyhelmet = false;
            scopelens = false;
            shellbell = false;
            assaultvest2 = false;
            choiceband2 = false;
            choicescarf2 = false;
            choicespecs2 = false;
            leftovers2 = false;
            lifeorb2 = false;
            luckyegg2 = false;
            rockyhelmet2 = false;
            scopelens2 = false;
            shellbell2 = false;
            //On.RoR2.Stage.Start += Stage_Start;
            //On.RoR2.CharacterMaster.Respawn += CharacterMaster_Respawn;

            On.RoR2.CharacterBody.Start += CharacterBody_Start;
            //On.RoR2.CharacterBody.FixedUpdate += CharacterBody_FixedUpdate;
            On.RoR2.CharacterMaster.OnInventoryChanged += CharacterMaster_OnInventoryChanged;
            On.RoR2.CharacterModel.Awake += CharacterModel_Awake;
            On.RoR2.Run.BeginGameOver += Run_BeginGameOver;

        }

        private void Start()
        {
            transformed = false;
            assaultvest = false;
            choiceband = false;
            choicescarf = false;
            choicespecs = false;
            leftovers = false;
            lifeorb = false;
            luckyegg = false;
            rockyhelmet = false;
            scopelens = false;
            shellbell = false;
            assaultvest2 = false;
            choiceband2 = false;
            choicescarf2 = false;
            choicespecs2 = false;
            leftovers2 = false;
            lifeorb2 = false;
            luckyegg2 = false;
            rockyhelmet2 = false;
            scopelens2 = false;
            shellbell2 = false;
            characterMaster = gameObject.GetComponent<CharacterMaster>();
            //Debug.Log(transformed + "istransformed");
            CharacterBody self = characterMaster.GetBody();

            dittomastercon = characterMaster.gameObject.GetComponent<DittoMasterController>();
            dittocon = self.gameObject.GetComponent<DittoController>();

            dittocon.assaultvest = false;
            dittocon.choiceband = false;
            dittocon.choicescarf = false;
            dittocon.choicespecs = false;
            dittocon.leftovers = false;
            dittocon.lifeorb = false;
            dittocon.luckyegg = false;
            dittocon.rockyhelmet = false;
            dittocon.scopelens = false;
            dittocon.shellbell = false;
            dittocon.assaultvest2 = false;
            dittocon.choiceband2 = false;
            dittocon.choicescarf2 = false;
            dittocon.choicespecs2 = false;
            dittocon.leftovers2 = false;
            dittocon.lifeorb2 = false;
            dittocon.luckyegg2 = false;
            dittocon.rockyhelmet2 = false;
            dittocon.scopelens2 = false;
            dittocon.shellbell2 = false;

            self.SetBuffCount(Modules.Buffs.assaultvestBuff.buffIndex, 0);
            self.SetBuffCount(Modules.Buffs.choicebandBuff.buffIndex, 0);
            self.SetBuffCount(Modules.Buffs.choicescarfBuff.buffIndex, 0);
            self.SetBuffCount(Modules.Buffs.choicespecsBuff.buffIndex, 0);
            self.SetBuffCount(Modules.Buffs.leftoversBuff.buffIndex, 0);
            self.SetBuffCount(Modules.Buffs.lifeorbBuff.buffIndex, 0);
            self.SetBuffCount(Modules.Buffs.luckyeggBuff.buffIndex, 0);
            self.SetBuffCount(Modules.Buffs.rockyhelmetBuff.buffIndex, 0);
            self.SetBuffCount(Modules.Buffs.scopelensBuff.buffIndex, 0);
            self.SetBuffCount(Modules.Buffs.shellbellBuff.buffIndex, 0);

            characterMaster.luck = 0;
            if (characterMaster.GetBody().skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_LUCKYEGG_NAME" && !luckyegg)
            {
                luckyegg = true;
                dittocon.luckyegg = true;
                self.AddBuff(Modules.Buffs.luckyeggBuff);
                characterMaster.luck += 1f;
            }
            if (characterMaster.GetBody().skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_LUCKYEGG_NAME" && !luckyegg2)
            {
                luckyegg2 = true;
                dittocon.luckyegg = true;
                self.AddBuff(Modules.Buffs.luckyeggBuff);
                characterMaster.luck += 1f;
            }


            if (self.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_ASSAULTVEST_NAME" && !assaultvest)
            {
                assaultvest = true;
                dittocon.assaultvest = true;
                self.AddBuff(Modules.Buffs.assaultvestBuff);
            }
            if (self.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_CHOICEBAND_NAME" && !choiceband)
            {
                choiceband = true;
                dittocon.choiceband = true;
                self.AddBuff(Modules.Buffs.choicebandBuff);
            }
            if (self.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_CHOICESCARF_NAME" && !choicescarf)
            {
                choicescarf = true;
                dittocon.choicescarf = true;
                self.AddBuff(Modules.Buffs.choicescarfBuff);
            }
            if (self.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_CHOICESPECS_NAME" && !choicespecs)
            {
                choicespecs = true;
                dittocon.choicespecs = true;
                self.AddBuff(Modules.Buffs.choicespecsBuff);
            }
            if (self.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_LEFTOVERS_NAME" && !leftovers)
            {
                leftovers = true;
                dittocon.leftovers = true;
                self.AddBuff(Modules.Buffs.leftoversBuff);
            }
            if (self.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_LIFEORB_NAME" && !lifeorb)
            {
                lifeorb = true;
                dittocon.lifeorb = true;
                self.AddBuff(Modules.Buffs.lifeorbBuff);
            }
            //if (self.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_LUCKYEGG_NAME" && !luckyegg)
            //{
            //    luckyegg = true;
            //    self.AddBuff(Modules.Buffs.luckyeggBuff);
            //}
            if (self.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_ROCKYHELMET_NAME" && !rockyhelmet)
            {
                rockyhelmet = true;
                dittocon.rockyhelmet = true;
                self.AddBuff(Modules.Buffs.rockyhelmetBuff);
            }
            if (self.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_SCOPELENS_NAME" && !scopelens)
            {
                scopelens = true;
                dittocon.scopelens = true;
                self.AddBuff(Modules.Buffs.scopelensBuff);
            }
            if (self.skillLocator.secondary.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_SHELLBELL_NAME" && !shellbell)
            {
                shellbell = true;
                dittocon.shellbell = true;
                self.AddBuff(Modules.Buffs.shellbellBuff);
            }
            if (self.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_ASSAULTVEST_NAME" && !assaultvest2)
            {
                assaultvest2 = true;
                dittocon.assaultvest2 = true;
                self.AddBuff(Modules.Buffs.assaultvestBuff);
            }
            if (self.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_CHOICEBAND_NAME" && !choiceband2)
            {
                choiceband2 = true;
                dittocon.choiceband2 = true;
                self.AddBuff(Modules.Buffs.choicebandBuff);
            }
            if (self.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_CHOICESCARF_NAME" && !choicescarf2)
            {
                choicescarf2 = true;
                dittocon.choicescarf2 = true;
                self.AddBuff(Modules.Buffs.choicescarfBuff);
            }
            if (self.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_CHOICESPECS_NAME" && !choicespecs2)
            {
                choicespecs2 = true;
                dittocon.choicespecs2 = true;
                self.AddBuff(Modules.Buffs.choicespecsBuff);
            }
            if (self.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_LEFTOVERS_NAME" && !leftovers2)
            {
                leftovers2 = true;
                dittocon.leftovers2 = true;
                self.AddBuff(Modules.Buffs.leftoversBuff);
            }
            if (self.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_LIFEORB_NAME" && !lifeorb2)
            {
                lifeorb2 = true;
                dittocon.lifeorb2 = true;
                self.AddBuff(Modules.Buffs.lifeorbBuff);
            }
            //if (self.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_LUCKYEGG_NAME" && !luckyegg2)
            //{
            //    luckyegg2 = true;
            //    self.AddBuff(Modules.Buffs.luckyeggBuff);
            //}
            if (self.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_ROCKYHELMET_NAME" && !rockyhelmet2)
            {
                rockyhelmet2 = true;
                dittocon.rockyhelmet2 = true;
                self.AddBuff(Modules.Buffs.rockyhelmetBuff);
            }
            if (self.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_SCOPELENS_NAME" && !scopelens2)
            {
                scopelens2 = true;
                dittocon.scopelens2 = true;
                self.AddBuff(Modules.Buffs.scopelensBuff);
            }
            if (self.skillLocator.utility.skillNameToken == DittoPlugin.developerPrefix + "_DITTO_BODY_SHELLBELL_NAME" && !shellbell2)
            {
                shellbell2 = true;
                dittocon.shellbell2 = true;
                self.AddBuff(Modules.Buffs.shellbellBuff);
            }


        }


        private void Run_BeginGameOver(On.RoR2.Run.orig_BeginGameOver orig, Run self, GameEndingDef gameEndingDef)
        {
            orig(self, gameEndingDef);
            //Destroy(dittomastercon);

        }

        private void CharacterModel_Awake(On.RoR2.CharacterModel.orig_Awake orig, CharacterModel self)
        {
            orig(self);
            if (self.gameObject.name.Contains("DittoDisplay"))
            {

                transformed = false;
                assaultvest = false;
                choiceband = false;
                choicescarf = false;
                choicespecs = false;
                leftovers = false;
                lifeorb = false;
                luckyegg = false;
                rockyhelmet = false;
                scopelens = false;
                shellbell = false;
                assaultvest2 = false;
                choiceband2 = false;
                choicescarf2 = false;
                choicespecs2 = false;
                leftovers2 = false;
                lifeorb2 = false;
                luckyegg2 = false;
                rockyhelmet2 = false;
                scopelens2 = false;
                shellbell2 = false;

            }
            //Destroy(dittomastercon);


        }

        //private CharacterBody CharacterMaster_Respawn(On.RoR2.CharacterMaster.orig_Respawn orig, CharacterMaster self, Vector3 footPosition, Quaternion rotation)
        //{
        //    transformed = false;
        //    assaultvest = false;
        //    choiceband = false;
        //    choicescarf = false;
        //    choicespecs = false;
        //    leftovers = false;
        //    lifeorb = false;
        //    luckyegg = false;
        //    rockyhelmet = false;
        //    scopelens = false;
        //    shellbell = false;
        //    assaultvest2 = false;
        //    choiceband2 = false;
        //    choicescarf2 = false;
        //    choicespecs2 = false;
        //    leftovers2 = false;
        //    lifeorb2 = false;
        //    luckyegg2 = false;
        //    rockyhelmet2 = false;
        //    scopelens2 = false;
        //    shellbell2 = false;

        //    orig.Invoke(self, footPosition, rotation);
        //    return body;
        //}

        private void CharacterMaster_OnInventoryChanged(On.RoR2.CharacterMaster.orig_OnInventoryChanged orig, CharacterMaster self)
        {
            self.luck = 0;
            if (luckyegg)
            {
                self.luck += 1f;
            }
            if (luckyegg2)
            {
                self.luck += 1f;
            }
            self.luck += self.inventory.GetItemCount(RoR2Content.Items.Clover);
            self.luck -= self.inventory.GetItemCount(RoR2Content.Items.LunarBadLuck);
        }

        private void CharacterBody_Start(On.RoR2.CharacterBody.orig_Start orig, CharacterBody self)
        {
            orig(self);

            List<string> speciallist = new List<string>();
            speciallist.Add("NullifierBody");
            speciallist.Add("VoidJailerBody");
            speciallist.Add("MinorConstructBody");
            speciallist.Add("VoidInfestorBody");
            speciallist.Add("VoidBarnacleBody");
            speciallist.Add("MinorConstructOnKillBody");
            speciallist.Add("MiniVoidRaidCrabBodyPhase1");
            speciallist.Add("MiniVoidRaidCrabBodyPhase2");
            speciallist.Add("MiniVoidRaidCrabBodyPhase3");

            List<string> bosslist = new List<string>();
            bosslist.Add("ElectricWormBody");
            bosslist.Add("MagmaWormBody");
            bosslist.Add("BeetleQueen2Body");
            bosslist.Add("TitanBody");
            bosslist.Add("TitanGoldBody");
            bosslist.Add("VagrantBody");
            bosslist.Add("GravekeeperBody");
            bosslist.Add("ClayBossBody");
            bosslist.Add("RoboBallBossBody");
            bosslist.Add("SuperRoboBallBossBody");
            bosslist.Add("MegaConstructBody");
            bosslist.Add("VoidMegaCrabBody");
            bosslist.Add("GrandParentBody");
            bosslist.Add("ImpBossBody");
            bosslist.Add("BrotherBody");
            bosslist.Add("BrotherHurtBody");
            bosslist.Add("ScavBody");

            if (self.master.gameObject.GetComponent<DittoMasterController>())
            {
                if (self.master.bodyPrefab == BodyCatalog.FindBodyPrefab("DittoBody"))
                {

                    if (assaultvest && assaultvest2)
                    {
                        self.SetBuffCount(Modules.Buffs.assaultvestBuff.buffIndex, 2);
                    }
                    else if (assaultvest | assaultvest2)
                    {
                        self.SetBuffCount(Modules.Buffs.assaultvestBuff.buffIndex, 1);
                    }
                    if (choiceband && choiceband2)
                    {
                        self.SetBuffCount(Modules.Buffs.choicebandBuff.buffIndex, 2);
                    }
                    else if (choiceband | choiceband2)
                    {
                        self.SetBuffCount(Modules.Buffs.choicebandBuff.buffIndex, 1);
                    }
                    if (choicescarf && choicescarf2)
                    {
                        self.SetBuffCount(Modules.Buffs.choicescarfBuff.buffIndex, 2);
                    }
                    else if (choicescarf | choicescarf2)
                    {
                        self.SetBuffCount(Modules.Buffs.choicescarfBuff.buffIndex, 1);
                    }
                    if (choicespecs && choicespecs2)
                    {
                        self.SetBuffCount(Modules.Buffs.choicespecsBuff.buffIndex, 2);
                    }
                    else if (choicespecs | choicespecs2)
                    {
                        self.SetBuffCount(Modules.Buffs.choicespecsBuff.buffIndex, 1);
                    }
                    if (leftovers && leftovers2)
                    {
                        self.SetBuffCount(Modules.Buffs.leftoversBuff.buffIndex, 2);
                    }
                    else if (leftovers | leftovers2)
                    {
                        self.SetBuffCount(Modules.Buffs.leftoversBuff.buffIndex, 1);
                    }
                    if (lifeorb && lifeorb2)
                    {
                        self.SetBuffCount(Modules.Buffs.lifeorbBuff.buffIndex, 2);
                    }
                    else if (lifeorb | lifeorb2)
                    {
                        self.SetBuffCount(Modules.Buffs.lifeorbBuff.buffIndex, 1);
                    }
                    if (luckyegg && luckyegg2)
                    {
                        self.master.luck = 0;
                        self.SetBuffCount(Modules.Buffs.luckyeggBuff.buffIndex, 2);
                        self.master.luck += 2f;
                    }
                    else if (luckyegg | luckyegg2)
                    {
                        self.master.luck = 0;
                        self.SetBuffCount(Modules.Buffs.luckyeggBuff.buffIndex, 1);
                        self.master.luck += 1f;
                    }
                    if (rockyhelmet && rockyhelmet2)
                    {
                        self.SetBuffCount(Modules.Buffs.rockyhelmetBuff.buffIndex, 2);
                    }
                    else if (rockyhelmet | rockyhelmet2)
                    {
                        self.SetBuffCount(Modules.Buffs.rockyhelmetBuff.buffIndex, 1);
                    }
                    if (scopelens && scopelens2)
                    {
                        self.SetBuffCount(Modules.Buffs.scopelensBuff.buffIndex, 2);
                    }
                    else if (scopelens | scopelens2)
                    {
                        self.SetBuffCount(Modules.Buffs.scopelensBuff.buffIndex, 1);
                    }
                    if (shellbell && shellbell2)
                    {
                        self.SetBuffCount(Modules.Buffs.shellbellBuff.buffIndex, 2);
                    }
                    else if (shellbell | shellbell2)
                    {
                        self.SetBuffCount(Modules.Buffs.shellbellBuff.buffIndex, 1);
                    }
                    

                }
                if (self.master.bodyPrefab != BodyCatalog.FindBodyPrefab("DittoBody"))
                {
                    if (speciallist.Contains(self.master.bodyPrefab.name))
                    {
                        if (transformed)
                        {
                            self.SetBuffCount(Modules.Buffs.transformBuff.buffIndex, 30);
                        }
                    }
                    if (bosslist.Contains(self.master.bodyPrefab.name))
                    {
                        if (transformed)
                        {
                            if (Config.bossTimer.Value)
                            {
                                self.SetBuffCount(Modules.Buffs.transformBuff.buffIndex, 30);
                            }                            
                        }
                    }
                    if (Config.choiceOnTeammate.Value)
                    {
                        if (assaultvest && assaultvest2)
                        {
                            self.SetBuffCount(Modules.Buffs.assaultvestBuff.buffIndex, 2);
                        }
                        else if (assaultvest | assaultvest2)
                        {
                            self.SetBuffCount(Modules.Buffs.assaultvestBuff.buffIndex, 1);
                        }
                        if (choiceband && choiceband2)
                        {
                            self.SetBuffCount(Modules.Buffs.choicebandBuff.buffIndex, 2);
                        }
                        else if (choiceband | choiceband2)
                        {
                            self.SetBuffCount(Modules.Buffs.choicebandBuff.buffIndex, 1);
                        }
                        if (choicescarf && choicescarf2)
                        {
                            self.SetBuffCount(Modules.Buffs.choicescarfBuff.buffIndex, 2);
                        }
                        else if (choicescarf | choicescarf2)
                        {
                            self.SetBuffCount(Modules.Buffs.choicescarfBuff.buffIndex, 1);
                        }
                        if (choicespecs && choicespecs2)
                        {
                            self.SetBuffCount(Modules.Buffs.choicespecsBuff.buffIndex, 2);
                        }
                        else if (choicespecs | choicespecs2)
                        {
                            self.SetBuffCount(Modules.Buffs.choicespecsBuff.buffIndex, 1);
                        }
                        if (leftovers && leftovers2)
                        {
                            self.SetBuffCount(Modules.Buffs.leftoversBuff.buffIndex, 2);
                        }
                        else if (leftovers | leftovers2)
                        {
                            self.SetBuffCount(Modules.Buffs.leftoversBuff.buffIndex, 1);
                        }
                        if (lifeorb && lifeorb2)
                        {
                            self.SetBuffCount(Modules.Buffs.lifeorbBuff.buffIndex, 2);
                        }
                        else if (lifeorb | lifeorb2)
                        {
                            self.SetBuffCount(Modules.Buffs.lifeorbBuff.buffIndex, 1);
                        }
                        if (luckyegg && luckyegg2)
                        {
                            self.master.luck = 0;
                            self.SetBuffCount(Modules.Buffs.luckyeggBuff.buffIndex, 2);
                            self.master.luck += 2f;
                        }
                        else if (luckyegg | luckyegg2)
                        {
                            self.master.luck = 0;
                            self.SetBuffCount(Modules.Buffs.luckyeggBuff.buffIndex, 1);
                            self.master.luck += 1f;
                        }
                        if (rockyhelmet && rockyhelmet2)
                        {
                            self.SetBuffCount(Modules.Buffs.rockyhelmetBuff.buffIndex, 2);
                        }
                        else if (rockyhelmet | rockyhelmet2)
                        {
                            self.SetBuffCount(Modules.Buffs.rockyhelmetBuff.buffIndex, 1);
                        }
                        if (scopelens && scopelens2)
                        {
                            self.SetBuffCount(Modules.Buffs.scopelensBuff.buffIndex, 2);
                        }
                        else if (scopelens | scopelens2)
                        {
                            self.SetBuffCount(Modules.Buffs.scopelensBuff.buffIndex, 1);
                        }
                        if (shellbell && shellbell2)
                        {
                            self.SetBuffCount(Modules.Buffs.shellbellBuff.buffIndex, 2);
                        }
                        else if (shellbell | shellbell2)
                        {
                            self.SetBuffCount(Modules.Buffs.shellbellBuff.buffIndex, 1);
                        }

                    }

                }

            }

        }


        //private void CharacterBody_FixedUpdate(On.RoR2.CharacterBody.orig_FixedUpdate orig, CharacterBody self)
        private void FixedUpdate()
        {
            //orig(self);

            characterMaster = gameObject.GetComponent<CharacterMaster>();
            //Debug.Log(transformed + "istransformed");
            CharacterBody self = characterMaster.GetBody();

            if (self.hasEffectiveAuthority)
            {
                if (self.HasBuff(Modules.Buffs.transformBuff.buffIndex))
                {

                    if (transformage > 1f)
                    {
                        int buffCountToApply = self.GetBuffCount(Modules.Buffs.transformBuff.buffIndex);
                        if (buffCountToApply > 1)
                        {
                            if (buffCountToApply >= 2)
                            {
                                //self.SetBuffCount(Modules.Buffs.transformBuff.buffIndex, (buffCountToApply - 1));
                                self.RemoveBuff(Modules.Buffs.transformBuff.buffIndex);

                                transformage = 0;


                            }
                        }
                        else
                        {

                            if (self.master.bodyPrefab.name == "CaptainBody")
                            {
                                self.master.inventory.RemoveItem(RoR2Content.Items.CaptainDefenseMatrix, 1);
                            }
                            if (self.master.bodyPrefab.name == "HereticBody")
                            {
                                self.master.inventory.RemoveItem(RoR2Content.Items.LunarPrimaryReplacement, 1);
                                self.master.inventory.RemoveItem(RoR2Content.Items.LunarSecondaryReplacement, 1);
                                self.master.inventory.RemoveItem(RoR2Content.Items.LunarSpecialReplacement, 1);
                                self.master.inventory.RemoveItem(RoR2Content.Items.LunarUtilityReplacement, 1);
                            }

                            //self.master.bodyPrefab = BodyCatalog.FindBodyPrefab("DittoBody");
                            CharacterBody body;


                            //body = self.master.Respawn(self.master.GetBody().transform.position, self.master.GetBody().transform.rotation);

                            self.master.TransformBody("DittoBody");

                            body = self.master.GetBody();

                            body.RemoveBuff(RoR2Content.Buffs.OnFire);
                            body.RemoveBuff(RoR2Content.Buffs.AffixBlue);
                            body.RemoveBuff(RoR2Content.Buffs.AffixEcho);
                            body.RemoveBuff(RoR2Content.Buffs.AffixHaunted);
                            body.RemoveBuff(RoR2Content.Buffs.AffixLunar);
                            body.RemoveBuff(RoR2Content.Buffs.AffixPoison);
                            body.RemoveBuff(RoR2Content.Buffs.AffixRed);
                            body.RemoveBuff(RoR2Content.Buffs.AffixWhite);
                            body.RemoveBuff(DittoMod.Modules.Assets.mendingelitebuff);
                            body.RemoveBuff(DittoMod.Modules.Assets.voidelitebuff);
                            transformed = false;

                        }
                    }

                    else transformage += Time.fixedDeltaTime;
                }


            }


        }

    }
}
