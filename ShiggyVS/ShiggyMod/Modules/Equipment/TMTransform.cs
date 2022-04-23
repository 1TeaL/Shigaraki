//using System;
//using System.Collections.Generic;
//using System.Text;
//using BepInEx.Configuration;
//using R2API;
//using RoR2;
//using UnityEngine;
//using UnityEngine.Networking;
//using ShiggyMod.Equipment;
//using ShiggyMod.Modules.Survivors;

//namespace ShiggyMod.Modules.Equipment
//{
//    class TMTransform : EquipmentBase<TMTransform>
//    {
//        public override string EquipmentName => "TM00 - Transform?";

//        public override string EquipmentLangTokenName => "TM_TRANSFORM";

//        public override string EquipmentPickupDesc => "Turn yourself into a Shiggy.";

//        public override string EquipmentFullDescription => "Turn yourself into a Shiggy. yea.";

//        public override string EquipmentLore => "The power of science enables the creation of more Shiggy.";

//        public override GameObject EquipmentModel => Modules.Assets.ShiggyEquipmentPrefab;
            
//        public override Sprite EquipmentIcon => Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("TMtransformSprite");

//        public override bool CanDrop => false;

//        public override bool IsLunar => true;

//        public override float Cooldown => Modules.StaticValues.TransformEquipmentCooldown;

//        public static GameObject ItemBodyModelPrefab;

//        public override ItemDisplayRuleDict CreateItemDisplayRules()
//        {
//            ItemDisplayRuleDict rules = new ItemDisplayRuleDict();
//            //no model for becomeShiggy.
//            return rules;
//        }

//        public override void Hooks()
//        {
            
//        }

//        public override void Init()
//        {
//            //CreateConfig(config);
//            CreateLang();
//            //CreateEffect();
//            //CreateInteractable();
//            //CreateSound();
//            CreateEquipment();
//            Hooks();
//        }

//        private void CreateEffect() {

//        }

//        protected override bool ActivateEquipment(EquipmentSlot slot)
//        {
//            CharacterBody charBody = slot.characterBody;

//            if (!slot.characterBody || !slot.characterBody.teamComponent) return false;
//            if (!slot.characterBody.master) { return false; }

//            var component = slot.characterBody.master.gameObject.GetComponent<ShiggyHandler>();
//            if (!component)
//            {
//                slot.characterBody.master.gameObject.AddComponent<ShiggyHandler>();
//                component = slot.characterBody.master.gameObject.GetComponent<ShiggyHandler>();
//            }

//            if (slot.stock <= 0 || !component) { return false; }

//            component.BecomeShiggy();
//            return true;
//        }

//        public class ShiggyHandler : MonoBehaviour
//        {
//            public ShiggyController Shiggycon;
//            public ShiggyMasterController Shiggymastercon;
//            private CharacterMaster characterMaster;
//            private CharacterBody body;

//            public void Awake()
//            {
//                characterMaster = this.gameObject.GetComponent<CharacterMaster>();
//                Shiggycon = this.gameObject.GetComponent<ShiggyController>();
//                Shiggymastercon = characterMaster.gameObject.GetComponent<ShiggyMasterController>();
//            }

//            public void Start()
//            {
//            }

//            //Turns into Shiggy
//            public void BecomeShiggy()
//            {
//                AkSoundEngine.PostEvent(1719197672, this.gameObject);
//                //var oldHealth = body.healthComponent.health / body.healthComponent.fullHealth;
//                if (characterMaster.bodyPrefab.name == "CaptainBody")
//                {
//                    characterMaster.inventory.RemoveItem(RoR2Content.Items.CaptainDefenseMatrix, 1);
//                }
//                if (characterMaster.bodyPrefab.name == "HereticBody")
//                {
//                    characterMaster.inventory.RemoveItem(RoR2Content.Items.LunarPrimaryReplacement, 1);
//                    characterMaster.inventory.RemoveItem(RoR2Content.Items.LunarSecondaryReplacement, 1);
//                    characterMaster.inventory.RemoveItem(RoR2Content.Items.LunarSpecialReplacement, 1);
//                    characterMaster.inventory.RemoveItem(RoR2Content.Items.LunarUtilityReplacement, 1);
//                }

//                if (characterMaster.bodyPrefab.name != "ShiggyBody")
//                {
//                    //characterMaster.bodyPrefab = BodyCatalog.FindBodyPrefab("ShiggyBody");
//                    //body = characterMaster.Respawn(characterMaster.GetBody().transform.position, characterMaster.GetBody().transform.rotation);
//                    //characterMaster.GetBody().AddBuff(Modules.Buffs.transformBuff.buffIndex);
//                    characterMaster.TransformBody("ShiggyBody");

//                    body = characterMaster.GetBody();

//                    Shiggycon.transformed = false;
//                    Shiggycon.assaultvest = false;
//                    Shiggycon.choiceband = false;
//                    Shiggycon.choicescarf = false;
//                    Shiggycon.choicespecs = false;
//                    Shiggycon.leftovers = false;
//                    Shiggycon.lifeorb = false;
//                    Shiggycon.luckyegg = false;
//                    Shiggycon.rockyhelmet = false;
//                    Shiggycon.scopelens = false;
//                    Shiggycon.shellbell = false;
//                    Shiggycon.assaultvest2 = false;
//                    Shiggycon.choiceband2 = false;
//                    Shiggycon.choicescarf2 = false;
//                    Shiggycon.choicespecs2 = false;
//                    Shiggycon.leftovers2 = false;
//                    Shiggycon.lifeorb2 = false;
//                    Shiggycon.luckyegg2 = false;
//                    Shiggycon.rockyhelmet2 = false;
//                    Shiggycon.scopelens2 = false;
//                    Shiggycon.shellbell2 = false;

//                    Shiggymastercon.transformed = false;
//                    Shiggymastercon.assaultvest = false;
//                    Shiggymastercon.choiceband = false;
//                    Shiggymastercon.choicescarf = false;
//                    Shiggymastercon.choicespecs = false;
//                    Shiggymastercon.leftovers = false;
//                    Shiggymastercon.lifeorb = false;
//                    Shiggymastercon.luckyegg = false;
//                    Shiggymastercon.rockyhelmet = false;
//                    Shiggymastercon.scopelens = false;
//                    Shiggymastercon.shellbell = false;
//                    Shiggymastercon.assaultvest2 = false;
//                    Shiggymastercon.choiceband2 = false;
//                    Shiggymastercon.choicescarf2 = false;
//                    Shiggymastercon.choicespecs2 = false;
//                    Shiggymastercon.leftovers2 = false;
//                    Shiggymastercon.lifeorb2 = false;
//                    Shiggymastercon.luckyegg2 = false;
//                    Shiggymastercon.rockyhelmet2 = false;
//                    Shiggymastercon.scopelens2 = false;
//                    Shiggymastercon.shellbell2 = false;
//                    //if (Config.copyHealth.Value)
//                    //    body.healthComponent.health = body.healthComponent.fullHealth * oldHealth;

//                    body.RemoveBuff(RoR2Content.Buffs.OnFire);
//                    body.RemoveBuff(RoR2Content.Buffs.AffixBlue);
//                    body.RemoveBuff(RoR2Content.Buffs.AffixEcho);
//                    body.RemoveBuff(RoR2Content.Buffs.AffixHaunted);
//                    body.RemoveBuff(RoR2Content.Buffs.AffixLunar);
//                    body.RemoveBuff(RoR2Content.Buffs.AffixPoison);
//                    body.RemoveBuff(RoR2Content.Buffs.AffixRed);
//                    body.RemoveBuff(RoR2Content.Buffs.AffixWhite);
//                    body.RemoveBuff(ShiggyMod.Modules.Assets.mendingelitebuff);
//                    body.RemoveBuff(ShiggyMod.Modules.Assets.voidelitebuff);
//                }
                
//            }
//        }
//    }
//}
