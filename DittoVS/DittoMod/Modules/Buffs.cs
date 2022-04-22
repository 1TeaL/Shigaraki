using RoR2;
using System.Collections.Generic;
using UnityEngine;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using UnityEngine.AddressableAssets;

namespace DittoMod.Modules
{
    public static class Buffs
    {
        internal static List<BuffDef> buffDefs = new List<BuffDef>();

        internal static BuffDef transformBuff;
        internal static BuffDef assaultvestBuff;
        internal static BuffDef choicescarfBuff;
        internal static BuffDef choicebandBuff;
        internal static BuffDef choicespecsBuff;
        internal static BuffDef leftoversBuff;
        internal static BuffDef lifeorbBuff;
        internal static BuffDef luckyeggBuff;
        internal static BuffDef rockyhelmetBuff;
        internal static BuffDef scopelensBuff;
        internal static BuffDef shellbellBuff;

        internal static void RegisterBuffs()
        {
            transformBuff = Buffs.AddNewBuff("transformBuff", Assets.mainAssetBundle.LoadAsset<Sprite>("Transform"), Color.white, true, false);
            assaultvestBuff = Buffs.AddNewBuff("assaultvestBuff", Assets.mainAssetBundle.LoadAsset<Sprite>("AssaultVest"), Color.white, true, false);
            choicescarfBuff = Buffs.AddNewBuff("choicescarfBuff", Assets.mainAssetBundle.LoadAsset<Sprite>("ChoiceScarf"), Color.white, true, false);
            choicebandBuff = Buffs.AddNewBuff("choicebandBuff", Assets.mainAssetBundle.LoadAsset<Sprite>("ChoiceBand"), Color.white, true, false);
            choicespecsBuff = Buffs.AddNewBuff("choicespecsBuff", Assets.mainAssetBundle.LoadAsset<Sprite>("ChoiceSpecs"), Color.white, true, false);
            leftoversBuff = Buffs.AddNewBuff("leftoversBuff", Assets.mainAssetBundle.LoadAsset<Sprite>("Leftovers"), Color.white, true, false);
            lifeorbBuff = Buffs.AddNewBuff("lifeorbBuff", Assets.mainAssetBundle.LoadAsset<Sprite>("LifeOrb"), Color.white, true, false);
            luckyeggBuff = Buffs.AddNewBuff("luckyeggBuff", Assets.mainAssetBundle.LoadAsset<Sprite>("LuckyEgg"), Color.white, true, false);
            rockyhelmetBuff = Buffs.AddNewBuff("rockyhelmetBuff", Assets.mainAssetBundle.LoadAsset<Sprite>("RockyHelmet"), Color.white, true, false);
            scopelensBuff = Buffs.AddNewBuff("scopelensBuff", Assets.mainAssetBundle.LoadAsset<Sprite>("ScopeLens"), Color.white, true, false);
            shellbellBuff = Buffs.AddNewBuff("shellbellBuff", Assets.mainAssetBundle.LoadAsset<Sprite>("ShellBell"), Color.white, true, false);

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
