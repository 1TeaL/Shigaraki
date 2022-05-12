using RoR2;
using System.Collections.Generic;
using UnityEngine;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using UnityEngine.AddressableAssets;

namespace ShiggyMod.Modules
{
    public static class Buffs
    {
        internal static List<BuffDef> buffDefs = new List<BuffDef>();

        internal static BuffDef flyBuff;
        internal static BuffDef beetleBuff;
        internal static BuffDef alphashieldonBuff;
        internal static BuffDef alphashieldoffBuff;
        internal static BuffDef decayDebuff;
        internal static BuffDef multiplierBuff;
        internal static BuffDef pestjumpBuff;
        internal static BuffDef verminsprintBuff;
        internal static BuffDef gupspikeBuff;
        internal static BuffDef hermitcrabmortarBuff;
        internal static BuffDef hermitcrabmortararmorBuff;
        internal static BuffDef larvajumpBuff;
        internal static BuffDef lesserwispBuff;
        internal static BuffDef lunarexploderBuff;
        internal static BuffDef minimushrumBuff;
        internal static BuffDef roboballminiBuff;
        internal static BuffDef voidbarnaclemortarBuff;
        internal static BuffDef voidbarnaclemortarattackspeedBuff;
        internal static BuffDef voidjailerBuff;
        internal static BuffDef impbossBuff;
        internal static BuffDef stonetitanBuff;
        internal static BuffDef vagrantBuff;
        internal static BuffDef vagrantDebuff;
        internal static BuffDef vagrantdisableBuff;
        internal static BuffDef magmawormBuff;
        internal static BuffDef overloadingwormBuff;

        internal static void RegisterBuffs()
        {
            Sprite warcryBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/WarCryOnMultiKill/bdWarCryBuff.asset").WaitForCompletion().iconSprite;
            Sprite shieldBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Common/bdArmorBoost.asset").WaitForCompletion().iconSprite;
            Sprite bleedBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Common/bdBleeding.asset").WaitForCompletion().iconSprite;
            Sprite tarBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Common/bdClayGoo.asset").WaitForCompletion().iconSprite;
            Sprite crippleBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Common/bdCripple.asset").WaitForCompletion().iconSprite;
            Sprite speedBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Bandit2/bdCloakSpeed.asset").WaitForCompletion().iconSprite;
            Sprite boostBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/RandomDamageZone/bdPowerBuff.asset").WaitForCompletion().iconSprite;
            Sprite alphashieldonBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/DLC1/BearVoid/bdBearVoidReady.asset").WaitForCompletion().iconSprite;
            Sprite alphashieldoffBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/DLC1/BearVoid/bdBearVoidCooldown.asset").WaitForCompletion().iconSprite;
            Sprite decayBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Common/bdVoidFogStrong.asset").WaitForCompletion().iconSprite;
            Sprite multiplierBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/DLC1/PrimarySkillShuriken/bdPrimarySkillShurikenBuff.asset").WaitForCompletion().iconSprite;
            Sprite jumpBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/DLC1/MoveSpeedOnKill/bdKillMoveSpeed.asset").WaitForCompletion().iconSprite;
            Sprite sprintBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/SprintOutOfCombat/bdWhipBoost.asset").WaitForCompletion().iconSprite;
            Sprite spikeBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Grandparent/bdOverheat.asset").WaitForCompletion().iconSprite;
            Sprite mortarBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/GainArmor/bdElephantArmorBoost.asset").WaitForCompletion().iconSprite;
            Sprite healBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Croco/bdCrocoRegen.asset").WaitForCompletion().iconSprite;
            Sprite attackspeedBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/EnergizedOnEquipmentUse/bdEnergized.asset").WaitForCompletion().iconSprite;
            Sprite gravityBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/KillEliteFrenzy/bdNoCooldowns.asset").WaitForCompletion().iconSprite;
            Sprite skinBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/DLC1/ImmuneToDebuff/ImmuneToDebuff.asset").WaitForCompletion().iconSprite;
            Sprite orbreadyBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/ElementalRings/bdElementalRingsReady.asset").WaitForCompletion().iconSprite;
            Sprite orbdisableBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/ElementalRings/bdElementalRingsCooldown.asset").WaitForCompletion().iconSprite;
            Sprite blazingBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/Common/bdOnFire.asset").WaitForCompletion().iconSprite;
            Sprite lightningBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/ShockNearby/bdTeslaField.asset").WaitForCompletion().iconSprite;

            flyBuff = Buffs.AddNewBuff("FlyBuff", speedBuffIcon, Color.magenta, false, false);
            beetleBuff = Buffs.AddNewBuff("StrengthBuff", boostBuffIcon, Color.grey, false, false);
            alphashieldonBuff = Buffs.AddNewBuff("ShieldOnBuff", alphashieldonBuffIcon, Color.magenta, false, false);
            alphashieldoffBuff = Buffs.AddNewBuff("ShieldOffBuff", alphashieldoffBuffIcon, Color.black, true, true);
            decayDebuff = Buffs.AddNewBuff("decayDebuff", decayBuffIcon, Color.black, true, true);
            multiplierBuff = Buffs.AddNewBuff("multiplierBuff", multiplierBuffIcon, Color.magenta, false, false);
            pestjumpBuff = Buffs.AddNewBuff("jumpBuff", jumpBuffIcon, Color.green, false, false);
            verminsprintBuff = Buffs.AddNewBuff("sprintBuff", sprintBuffIcon, Color.green, false, false);
            gupspikeBuff = Buffs.AddNewBuff("spikybodyBuff", spikeBuffIcon, Color.red, false, false);
            hermitcrabmortarBuff = Buffs.AddNewBuff("mortarBuff", mortarBuffIcon, Color.magenta, false, false);
            hermitcrabmortararmorBuff = Buffs.AddNewBuff("mortararmorBuff", shieldBuffIcon, Color.blue, true, false);
            larvajumpBuff = Buffs.AddNewBuff("larvajumpBuff", jumpBuffIcon, Color.green, false, false);
            lesserwispBuff = Buffs.AddNewBuff("RangedBuff", boostBuffIcon, Color.red, false, false);
            lunarexploderBuff = Buffs.AddNewBuff("lunarauraBuff", crippleBuffIcon, Color.blue, false, false);
            minimushrumBuff = Buffs.AddNewBuff("healingauraBuff", healBuffIcon, Color.green, false, false);
            roboballminiBuff = Buffs.AddNewBuff("glideBuff", speedBuffIcon, Color.cyan, false, false);
            voidbarnaclemortarBuff = Buffs.AddNewBuff("voidmortarBuff", mortarBuffIcon, Color.magenta, false, false);
            voidbarnaclemortarattackspeedBuff = Buffs.AddNewBuff("voidbarnaclemortarattackspeedBuff", attackspeedBuffIcon, Color.blue, true, false);
            voidjailerBuff = Buffs.AddNewBuff("gravitypullBuff", gravityBuffIcon, Color.magenta, false, false);
            impbossBuff = Buffs.AddNewBuff("bleedBuff", bleedBuffIcon, Color.black, false, false);
            stonetitanBuff = Buffs.AddNewBuff("stoneskinBuff", skinBuffIcon, Color.magenta, false, false);
            vagrantBuff = Buffs.AddNewBuff("vagrant'sorbBuff", orbreadyBuffIcon, Color.magenta, false, false);
            vagrantdisableBuff = Buffs.AddNewBuff("vagrantdisableBuff ", orbdisableBuffIcon, Color.black, true, false); ;
            vagrantDebuff = Buffs.AddNewBuff("vagrantDebuff", orbdisableBuffIcon, Color.black, true, true);
            magmawormBuff = Buffs.AddNewBuff("blazingauraBuff", blazingBuffIcon, Color.magenta, false, false);
            overloadingwormBuff = Buffs.AddNewBuff("lightningauraBuff", lightningBuffIcon, Color.magenta, false, false);


            //Sprite TransformBuff = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/LunarSkillReplacements/bdLunarSecondaryRoot.asset").WaitForCompletion().iconSprite;
            //transformBuff = AddNewBuff("TransformTimer", TransformBuff, Color.yellow, false, false);

        }

        // simple helper method
        internal static BuffDef AddNewBuff(string buffName, Sprite buffIcon, Color buffColor, bool canStack, bool isDebuff)
        {
            BuffDef buffDef = ScriptableObject.CreateInstance<BuffDef>();
            buffDef.name = buffName;
            buffDef.buffColor = buffColor;
            buffDef.canStack = canStack;
            buffDef.isDebuff = isDebuff;
            buffDef.eliteDef = null;
            buffDef.iconSprite = buffIcon;

            Buffs.buffDefs.Add(buffDef);

            return buffDef;
        }

    }
}
