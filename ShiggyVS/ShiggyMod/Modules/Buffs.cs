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

        //monster buffs
        internal static BuffDef airwalkBuff;
        internal static BuffDef beetleBuff;
        internal static BuffDef alphashieldonBuff;
        internal static BuffDef alphashieldoffBuff;
        internal static BuffDef pestjumpBuff;
        internal static BuffDef verminsprintBuff;
        internal static BuffDef gupspikeBuff;
        internal static BuffDef greaterwispBuff;
        internal static BuffDef hermitcrabmortarBuff;
        internal static BuffDef hermitcrabmortararmorBuff;
        internal static BuffDef jellyfishHealStacksBuff;
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

        //monsterdebuffs
        internal static BuffDef grovetenderChainDebuff;

        //synergy buffs
        internal static BuffDef bigbangBuff;
        internal static BuffDef wisperBuff;
        internal static BuffDef omniboostBuff;
        internal static BuffDef omniboostBuffStacks;
        internal static BuffDef gachaBuff;
        internal static BuffDef windShieldBuff;
        internal static BuffDef stoneFormBuff;
        internal static BuffDef stoneFormStillBuff;
        internal static BuffDef mechStanceBuff;
        internal static BuffDef auraOfBlightBuff;
        internal static BuffDef barbedSpikesBuff;
        internal static BuffDef ingrainBuff;
        internal static BuffDef limitBreakBuff;
        internal static BuffDef voidFormBuff;
        internal static BuffDef elementalFusionFireBuff;
        internal static BuffDef elementalFusionFreezeBuff;
        internal static BuffDef elementalFusionShockBuff;
        internal static BuffDef elementalFusionBuffStacks;
        internal static BuffDef doubleTimeBuff;
        internal static BuffDef doubleTimeBuffStacks;

        //synergy debuffs
        internal static BuffDef bigbangDebuff;
        internal static BuffDef omniboostDebuffStacks;
        internal static BuffDef doubleTimeDebuff;

        //survivor buffs
        internal static BuffDef acridBuff;
        internal static BuffDef commandoBuff;
        internal static BuffDef captainBuff;
        internal static BuffDef loaderBuff;
        internal static BuffDef multBuff;
        internal static BuffDef OFABuff;

        //shiggy skill decay and multiplier
        internal static BuffDef decayDebuff;
        internal static BuffDef multiplierBuff;

        //elite 60 second debuff timer
        internal static BuffDef eliteDebuff;


        internal static void RegisterBuffs()
        {

            airwalkBuff = Buffs.AddNewBuff("Fly Buff", Assets.jumpBuffIcon, Color.magenta, false, false);
            beetleBuff = Buffs.AddNewBuff("Strength Buff", Assets.boostBuffIcon, new Color(0.67f, 0.87f, 0.93f), false, false);
            alphashieldonBuff = Buffs.AddNewBuff("Alpha Construct Shield On", Assets.alphashieldonBuffIcon, Color.magenta, false, false);
            alphashieldoffBuff = Buffs.AddNewBuff("Alpha Construct Shield Off", Assets.alphashieldoffBuffIcon, Color.black, true, false);
            decayDebuff = Buffs.AddNewBuff("decay Debuff", Assets.decayBuffIcon, Color.magenta, true, true);
            multiplierBuff = Buffs.AddNewBuff("Multiplier Buff", Assets.multiplierBuffIcon, Color.magenta, false, false);
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
            jellyfishHealStacksBuff = Buffs.AddNewBuff("Jellyfish Heal Stacks", Assets.healBuffIcon, Color.grey, false, false);
            commandoBuff = Buffs.AddNewBuff("doubletapBuff", Assets.critBuffIcon, Color.magenta, false, false);
            acridBuff = Buffs.AddNewBuff("poisonBuff", Assets.bleedBuffIcon, Color.green, false, false);
            captainBuff = Buffs.AddNewBuff("microbotBuff", Assets.shieldBuffIcon, Color.red, false, false);
            loaderBuff = Buffs.AddNewBuff("loaderBuff", Assets.shieldBuffIcon, Color.yellow, false, false);
            multBuff = Buffs.AddNewBuff("multBuff", Assets.predatorBuffIcon, Color.yellow, false, false);
            OFABuff = Buffs.AddNewBuff("One For All Buff", Assets.lightningBuffIcon, Color.magenta, false, false);

            eliteDebuff = Buffs.AddNewBuff("eliteDebuff", Assets.critBuffIcon, Color.black, false, true);

            bigbangBuff = Buffs.AddNewBuff("bigbangBuff", Assets.orbreadyBuffIcon, new Color(0.67f, 0.87f, 0.93f), false, false);
            wisperBuff = Buffs.AddNewBuff("wisperBuff", Assets.multiplierBuffIcon, new Color(0.52f, 0.48f, 0.39f), false, false);
            omniboostBuff = Buffs.AddNewBuff("omniboost Buff", Assets.boostBuffIcon, Color.white, false, false);
            omniboostBuffStacks = Buffs.AddNewBuff("omniboost Buff stacks", Assets.boostBuffIcon, Color.gray, true, false);
            omniboostDebuffStacks = Buffs.AddNewBuff("omniboost Debuff stacks", Assets.boostBuffIcon, Color.black, true, true);
            gachaBuff = Buffs.AddNewBuff("gacha Buff", Assets.attackspeedBuffIcon, Color.yellow, true, false);
            windShieldBuff = Buffs.AddNewBuff("windShield Buff", Assets.shieldBuffIcon, Color.white, false, false);
            stoneFormBuff = Buffs.AddNewBuff("stoneForm Buff", Assets.shieldBuffIcon, Color.grey, false, false);
            stoneFormStillBuff = Buffs.AddNewBuff("stoneFormStill Buff", Assets.skinBuffIcon, Color.grey, false, false);
            mechStanceBuff = Buffs.AddNewBuff("mechStance Buff", Assets.ruinDebuffIcon, Color.yellow, false, false);
            auraOfBlightBuff = Buffs.AddNewBuff("AuraOfPoison Buff", Assets.bleedBuffIcon, Color.yellow, false, false);
            barbedSpikesBuff = Buffs.AddNewBuff("barbedSpikes Buff", Assets.spikyDebuffIcon, Color.grey, false, false);
            ingrainBuff = Buffs.AddNewBuff("ingrain Buff", Assets.medkitBuffIcon, Color.green, false, false);
            limitBreakBuff = Buffs.AddNewBuff("limit Break Buff", Assets.lightningBuffIcon, Color.black, false, false);
            voidFormBuff = Buffs.AddNewBuff("void form Buff", Assets.voidFogDebuffIcon, Color.magenta, false, false);
            elementalFusionFireBuff = Buffs.AddNewBuff("void form Buff", Assets.multiplierBuffIcon, Color.red, false, false);
            elementalFusionFreezeBuff = Buffs.AddNewBuff("void form Buff", Assets.multiplierBuffIcon, Color.cyan, false, false);
            elementalFusionShockBuff = Buffs.AddNewBuff("void form Buff", Assets.multiplierBuffIcon, Color.yellow, false, false);
            elementalFusionBuffStacks = Buffs.AddNewBuff("void form Buff", Assets.multiplierBuffIcon, Color.white, true, false);
            doubleTimeBuff = Buffs.AddNewBuff("doubleTime Buff", Assets.warcryBuffIcon, Color.white, false, false);
            doubleTimeBuffStacks = Buffs.AddNewBuff("double Time Buff Stacks", Assets.warcryBuffIcon, Color.yellow, true, false);
            //debuff
            grovetenderChainDebuff = Buffs.AddNewBuff("chainDebuff", Assets.tarBuffIcon, Color.blue, false, true);
            bigbangDebuff = Buffs.AddNewBuff("bigbangDebuff", Assets.orbdisableBuffIcon, Color.green, true, true);
            doubleTimeDebuff = Buffs.AddNewBuff("doubleTime Debuff", Assets.warcryBuffIcon, Color.black, false, true);


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
