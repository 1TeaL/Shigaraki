using System;
using System.Collections.Generic;
using System.Text;
using BepInEx.Configuration;
using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using DittoMod.Equipment;
using DittoMod.Modules.Survivors;

namespace DittoMod.Modules.Equipment
{
    class TMTransform : EquipmentBase<TMTransform>
    {
        public override string EquipmentName => "TM00 - Transform?";

        public override string EquipmentLangTokenName => "TM_TRANSFORM";

        public override string EquipmentPickupDesc => "Turn yourself into a Ditto.";

        public override string EquipmentFullDescription => "Turn yourself into a Ditto. yea.";

        public override string EquipmentLore => "The power of science enables the creation of more Ditto.";

        public override GameObject EquipmentModel => Modules.Assets.DittoEquipmentPrefab;
            
        public override Sprite EquipmentIcon => Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("TMtransformSprite");

        public override bool CanDrop => false;

        public override bool IsLunar => true;

        public override float Cooldown => Modules.StaticValues.TransformEquipmentCooldown;

        public static GameObject ItemBodyModelPrefab;

        public override ItemDisplayRuleDict CreateItemDisplayRules()
        {
            ItemDisplayRuleDict rules = new ItemDisplayRuleDict();
            //no model for becomeditto.
            return rules;
        }

        public override void Hooks()
        {
            
        }

        public override void Init()
        {
            //CreateConfig(config);
            CreateLang();
            //CreateEffect();
            //CreateInteractable();
            //CreateSound();
            CreateEquipment();
            Hooks();
        }

        private void CreateEffect() {

        }

        protected override bool ActivateEquipment(EquipmentSlot slot)
        {
            CharacterBody charBody = slot.characterBody;

            if (!slot.characterBody || !slot.characterBody.teamComponent) return false;
            if (!slot.characterBody.master) { return false; }

            var component = slot.characterBody.master.gameObject.GetComponent<DittoHandler>();
            if (!component)
            {
                slot.characterBody.master.gameObject.AddComponent<DittoHandler>();
                component = slot.characterBody.master.gameObject.GetComponent<DittoHandler>();
            }

            if (slot.stock <= 0 || !component) { return false; }

            component.BecomeDitto();
            return true;
        }

        public class DittoHandler : MonoBehaviour
        {
            public DittoController dittocon;
            public DittoMasterController dittomastercon;
            private CharacterMaster characterMaster;
            private CharacterBody body;

            public void Awake()
            {
                characterMaster = this.gameObject.GetComponent<CharacterMaster>();
                dittocon = this.gameObject.GetComponent<DittoController>();
                dittomastercon = characterMaster.gameObject.GetComponent<DittoMasterController>();
            }

            public void Start()
            {
            }

            //Turns into Ditto
            public void BecomeDitto()
            {
                AkSoundEngine.PostEvent(1719197672, this.gameObject);
                //var oldHealth = body.healthComponent.health / body.healthComponent.fullHealth;
                if (characterMaster.bodyPrefab.name == "CaptainBody")
                {
                    characterMaster.inventory.RemoveItem(RoR2Content.Items.CaptainDefenseMatrix, 1);
                }
                if (characterMaster.bodyPrefab.name == "HereticBody")
                {
                    characterMaster.inventory.RemoveItem(RoR2Content.Items.LunarPrimaryReplacement, 1);
                    characterMaster.inventory.RemoveItem(RoR2Content.Items.LunarSecondaryReplacement, 1);
                    characterMaster.inventory.RemoveItem(RoR2Content.Items.LunarSpecialReplacement, 1);
                    characterMaster.inventory.RemoveItem(RoR2Content.Items.LunarUtilityReplacement, 1);
                }

                if (characterMaster.bodyPrefab.name != "DittoBody")
                {
                    //characterMaster.bodyPrefab = BodyCatalog.FindBodyPrefab("DittoBody");
                    //body = characterMaster.Respawn(characterMaster.GetBody().transform.position, characterMaster.GetBody().transform.rotation);
                    //characterMaster.GetBody().AddBuff(Modules.Buffs.transformBuff.buffIndex);
                    characterMaster.TransformBody("DittoBody");

                    body = characterMaster.GetBody();

                    dittocon.transformed = false;
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

                    dittomastercon.transformed = false;
                    dittomastercon.assaultvest = false;
                    dittomastercon.choiceband = false;
                    dittomastercon.choicescarf = false;
                    dittomastercon.choicespecs = false;
                    dittomastercon.leftovers = false;
                    dittomastercon.lifeorb = false;
                    dittomastercon.luckyegg = false;
                    dittomastercon.rockyhelmet = false;
                    dittomastercon.scopelens = false;
                    dittomastercon.shellbell = false;
                    dittomastercon.assaultvest2 = false;
                    dittomastercon.choiceband2 = false;
                    dittomastercon.choicescarf2 = false;
                    dittomastercon.choicespecs2 = false;
                    dittomastercon.leftovers2 = false;
                    dittomastercon.lifeorb2 = false;
                    dittomastercon.luckyegg2 = false;
                    dittomastercon.rockyhelmet2 = false;
                    dittomastercon.scopelens2 = false;
                    dittomastercon.shellbell2 = false;
                    //if (Config.copyHealth.Value)
                    //    body.healthComponent.health = body.healthComponent.fullHealth * oldHealth;

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
                }
                
            }
        }
    }
}
