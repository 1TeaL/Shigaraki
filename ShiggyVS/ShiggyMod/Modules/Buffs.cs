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
        internal static BuffDef greaterwispBuff;
        internal static BuffDef hermitcrabmortarBuff;
        internal static BuffDef hermitcrabmortararmorBuff;
        internal static BuffDef larvajumpBuff;
        internal static BuffDef lesserwispBuff;
        internal static BuffDef lunarexploderBuff;
        internal static BuffDef minimushrumBuff;
        internal static BuffDef roboballminiBuff;
        internal static BuffDef roboballminiattackspeedBuff;
        internal static BuffDef voidbarnaclemortarBuff;
        internal static BuffDef voidbarnaclemortarattackspeedBuff;
        internal static BuffDef voidjailerBuff;
        internal static BuffDef impbossBuff;
        internal static BuffDef claydunestriderBuff;
        internal static BuffDef stonetitanBuff;
        internal static BuffDef vagrantBuff;
        internal static BuffDef vagrantDebuff;
        internal static BuffDef vagrantdisableBuff;
        internal static BuffDef magmawormBuff;
        internal static BuffDef overloadingwormBuff;


        internal static BuffDef acridBuff;
        internal static BuffDef commandoBuff;
        internal static BuffDef captainBuff;
        internal static BuffDef loaderBuff;
        internal static BuffDef multBuff;

        internal static void RegisterBuffs()
        {

            flyBuff = Buffs.AddNewBuff("FlyBuff", Assets.jumpBuffIcon, Color.magenta, false, false);
            beetleBuff = Buffs.AddNewBuff("StrengthBuff", Assets.boostBuffIcon, Color.grey, false, false);
            alphashieldonBuff = Buffs.AddNewBuff("ShieldOnBuff", Assets.alphashieldonBuffIcon, Color.magenta, false, false);
            alphashieldoffBuff = Buffs.AddNewBuff("ShieldOffBuff", Assets.alphashieldoffBuffIcon, Color.black, true, false);
            decayDebuff = Buffs.AddNewBuff("decayDebuff", Assets.decayBuffIcon, Color.magenta, true, true);
            multiplierBuff = Buffs.AddNewBuff("multiplierBuff", Assets.multiplierBuffIcon, Color.magenta, false, false);
            pestjumpBuff = Buffs.AddNewBuff("jumpBuff", Assets.jumpBuffIcon, Color.cyan, false, false);
            verminsprintBuff = Buffs.AddNewBuff("sprintBuff", Assets.sprintBuffIcon, Color.green, false, false);
            gupspikeBuff = Buffs.AddNewBuff("spikybodyBuff", Assets.spikeBuffIcon, Color.red, false, false);
            greaterwispBuff = Buffs.AddNewBuff("greaterwispBuff", Assets.resonanceBuffIcon, Color.magenta, true, false);
            hermitcrabmortarBuff = Buffs.AddNewBuff("mortarBuff", Assets.mortarBuffIcon, Color.magenta, false, false);
            hermitcrabmortararmorBuff = Buffs.AddNewBuff("mortararmorBuff", Assets.shieldBuffIcon, Color.blue, true, false);
            larvajumpBuff = Buffs.AddNewBuff("larvajumpBuff", Assets.jumpBuffIcon, Color.green, false, false);
            lesserwispBuff = Buffs.AddNewBuff("RangedBuff", Assets.boostBuffIcon, Color.red, false, false);
            lunarexploderBuff = Buffs.AddNewBuff("lunarauraBuff", Assets.crippleBuffIcon, Color.blue, false, false);
            minimushrumBuff = Buffs.AddNewBuff("healingauraBuff", Assets.healBuffIcon, Color.green, false, false);
            roboballminiBuff = Buffs.AddNewBuff("solusprobeBuff", Assets.jumpBuffIcon, Color.grey, false, false);
            roboballminiattackspeedBuff = Buffs.AddNewBuff("solusprobeattackspeedBuff", Assets.predatorBuffIcon, Color.grey, true, false);
            voidbarnaclemortarBuff = Buffs.AddNewBuff("voidmortarBuff", Assets.mortarBuffIcon, Color.magenta, false, false);
            voidbarnaclemortarattackspeedBuff = Buffs.AddNewBuff("voidbarnaclemortarattackspeedBuff", Assets.attackspeedBuffIcon, Color.blue, true, false);
            voidjailerBuff = Buffs.AddNewBuff("gravitypullBuff", Assets.gravityBuffIcon, Color.magenta, false, false);
            claydunestriderBuff = Buffs.AddNewBuff("claydunestriderBuff", Assets.claygooBuffIcon, Color.magenta, true, false);
            impbossBuff = Buffs.AddNewBuff("bleedBuff", Assets.bleedBuffIcon, Color.red, false, false);
            stonetitanBuff = Buffs.AddNewBuff("stoneskinBuff", Assets.skinBuffIcon, Color.magenta, false, false);
            vagrantBuff = Buffs.AddNewBuff("vagrant'sorbBuff", Assets.orbreadyBuffIcon, Color.magenta, false, false);
            vagrantdisableBuff = Buffs.AddNewBuff("vagrantdisableBuff ", Assets.orbdisableBuffIcon, Color.black, true, false);
            vagrantDebuff = Buffs.AddNewBuff("vagrantDebuff", Assets.orbdisableBuffIcon, Color.white, true, false);
            magmawormBuff = Buffs.AddNewBuff("blazingauraBuff", Assets.blazingBuffIcon, Color.magenta, false, false);
            overloadingwormBuff = Buffs.AddNewBuff("lightningauraBuff", Assets.lightningBuffIcon, Color.magenta, false, false);

            commandoBuff = Buffs.AddNewBuff("doubletapBuff", Assets.critBuffIcon, Color.magenta, false, false);
            acridBuff = Buffs.AddNewBuff("poisonBuff", Assets.bleedBuffIcon, Color.green, false, false);
            captainBuff = Buffs.AddNewBuff("microbotBuff", Assets.shieldBuffIcon, Color.red, false, false);
            loaderBuff = Buffs.AddNewBuff("loaderBuff", Assets.shieldBuffIcon, Color.yellow, false, false);
            multBuff = Buffs.AddNewBuff("multBuff", Assets.predatorBuffIcon, Color.yellow, false, false);



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
