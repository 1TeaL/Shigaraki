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
        internal static BuffDef spikeBuff;
        internal static BuffDef hermitcrabmortarBuff;
        internal static BuffDef hermitcrabmortararmorBuff;
        internal static BuffDef larvajumpBuff;
        internal static BuffDef lesserwispBuff;
        internal static BuffDef lunarexploderBuff;
        internal static BuffDef minimushrumBuff;
        internal static BuffDef roboballminiBuff;
        internal static BuffDef voidbarnaclemortarBuff;
        internal static BuffDef voidbarnaclemortarattackspeedBuff;

        internal static void RegisterBuffs()
        {
            Sprite warcryBuffIcon = Addressables.LoadAssetAsync<BuffDef>("RoR2/Base/WarCryOnMultiKill/bdWarCryBuff.asset").WaitForCompletion().iconSprite;

            flyBuff = Buffs.AddNewBuff("FlyBuff", warcryBuffIcon, Color.magenta, false, false);
            beetleBuff = Buffs.AddNewBuff("StrengthBuff", warcryBuffIcon, Color.blue, false, false);
            alphashieldonBuff = Buffs.AddNewBuff("ShieldOnBuff", warcryBuffIcon, Color.white, false, false);
            alphashieldoffBuff = Buffs.AddNewBuff("ShieldOffBuff", warcryBuffIcon, Color.green, true, true);
            decayDebuff = Buffs.AddNewBuff("decayDebuff", warcryBuffIcon, Color.cyan, true, true);
            multiplierBuff = Buffs.AddNewBuff("multiplierBuff", warcryBuffIcon, Color.white, false, false);
            pestjumpBuff = Buffs.AddNewBuff("jumpBuff", warcryBuffIcon, Color.white, false, false);
            verminsprintBuff = Buffs.AddNewBuff("sprintBuff", warcryBuffIcon, Color.white, false, false);
            spikeBuff = Buffs.AddNewBuff("spikeBuff", warcryBuffIcon, Color.white, false, false);
            hermitcrabmortarBuff = Buffs.AddNewBuff("mortarBuff", warcryBuffIcon, Color.white, false, false);
            hermitcrabmortararmorBuff = Buffs.AddNewBuff("mortararmorBuff", warcryBuffIcon, Color.green, true, false);
            larvajumpBuff = Buffs.AddNewBuff("larvajumpBuff", warcryBuffIcon, Color.green, false, false);
            lesserwispBuff = Buffs.AddNewBuff("RangedBuff", warcryBuffIcon, Color.green, false, false);
            lunarexploderBuff = Buffs.AddNewBuff("lunarexploderBuff", warcryBuffIcon, Color.green, false, false);
            minimushrumBuff = Buffs.AddNewBuff("minimushrumBuff", warcryBuffIcon, Color.green, false, false);
            roboballminiBuff = Buffs.AddNewBuff("roboballminiBuff", warcryBuffIcon, Color.green, false, false);
            voidbarnaclemortarBuff = Buffs.AddNewBuff("voidbarnaclemortarBuff", warcryBuffIcon, Color.white, false, false);
            voidbarnaclemortarattackspeedBuff = Buffs.AddNewBuff("voidbarnaclemortarattackspeedBuff", warcryBuffIcon, Color.green, true, false);


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
