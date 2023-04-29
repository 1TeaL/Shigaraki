using RoR2;
using System.Collections.Generic;
using UnityEngine;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using UnityEngine.AddressableAssets;
using System;

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
        internal static BuffDef blindSensesBuff;

        //synergy debuffs
        internal static BuffDef bigbangDebuff;
        internal static BuffDef omniboostDebuffStacks;
        internal static BuffDef doubleTimeDebuff;

        //ultimate buffs
        internal static BuffDef theWorldBuff;
        internal static BuffDef supernovaBuff;
        internal static BuffDef deathAuraBuff;
        internal static BuffDef reversalBuff;
        internal static BuffDef reversalBuffStacks;

        //ultimate debuffs
        internal static BuffDef theWorldDebuff;
        internal static BuffDef extremeSpeedHitsDebuff;
        internal static BuffDef deathAuraDebuff;

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

            airwalkBuff = Buffs.AddNewBuff($"Fly Buff", Assets.jumpBuffIcon, Color.magenta, false, false);
            beetleBuff = Buffs.AddNewBuff($"Strength Buff- <style=cIsDamage>Gain {StaticValues.beetleFlatDamage} base damage</style>. ", Assets.boostBuffIcon, new Color(0.67f, 0.87f, 0.93f), false, false);
            alphashieldonBuff = Buffs.AddNewBuff($"Alpha Construct Shield On- Gain a barrier that blocks the next hit. Recharges after {StaticValues.alphaconstructCooldown} seconds. ", Assets.alphashieldonBuffIcon, Color.magenta, false, false);
            alphashieldoffBuff = Buffs.AddNewBuff($"Alpha Construct Shield Off", Assets.alphashieldoffBuffIcon, Color.black, true, false);
            decayDebuff = Buffs.AddNewBuff($"decay Debuff", Assets.decayBuffIcon, Color.magenta, true, true);
            multiplierBuff = Buffs.AddNewBuff($"Multiplier Buff", Assets.multiplierBuffIcon, Color.magenta, false, false);
            pestjumpBuff = Buffs.AddNewBuff($"Jump Boost Buff- <style=cIsUtility>Gain 4 extra jumps and jump power</style>. ", Assets.jumpBuffIcon, Color.cyan, false, false);
            verminsprintBuff = Buffs.AddNewBuff($"Super speed Buff- <style=cIsUtility>Gain {StaticValues.verminmovespeedMultiplier}x movespeed and change sprint speed to {StaticValues.verminsprintMultiplier}x</style>. ", Assets.sprintBuffIcon, Color.green, false, false);
            gupspikeBuff = Buffs.AddNewBuff($"Spiky Body Buff- <style=cIsDamage>Gain spikes that deal <style=cIsDamage>{StaticValues.spikedamageCoefficient * 100}% damage</style> to those around you when you're hit</style>. ", Assets.spikeBuffIcon, Color.red, false, false);
            greaterwispBuff = Buffs.AddNewBuff($"greaterwispBuff", Assets.resonanceBuffIcon, Color.magenta, true, false);
            hermitcrabmortarBuff = Buffs.AddNewBuff($"Mortar Buff- While standing still, attack nearby enemies for <style=cIsDamage>{100 * StaticValues.mortarDamageCoefficient}% damage</style> and " +
                $"gain <style=cIsUtility>{StaticValues.mortararmorGain} armor </style> every ({StaticValues.mortarbaseDuration}/Attackspeed) second(s). " +
                $"Radius and Damage scales with armor and attackspeed. ", Assets.mortarBuffIcon, Color.magenta, false, false);
            hermitcrabmortararmorBuff = Buffs.AddNewBuff($"mortararmorBuff", Assets.shieldBuffIcon, Color.blue, true, false);
            larvajumpBuff = Buffs.AddNewBuff($"Acid Jump Buff- <style=cIsDamage>Release an Acidic blast</style> when <style=cIsUtility>jumping and landing</style>.", Assets.jumpBuffIcon, Color.green, false, false);
            lesserwispBuff = Buffs.AddNewBuff($"Haste Buff- <style=cIsDamage>Gain {StaticValues.lesserwispFlatAttackSpeed} flat attackspeed</style>.", Assets.boostBuffIcon, Color.red, false, false); 
            lunarexploderBuff = Buffs.AddNewBuff($"Lunar Barrier Buff- Gain a <style=cIsUtility>Shield equal to {StaticValues.lunarexploderShieldCoefficient}% of your max health</style>.", Assets.crippleBuffIcon, Color.blue, false, false);
            minimushrumBuff = Buffs.AddNewBuff($"Healing Aura Buff- <style=cIsHealing>Heal yourself and nearby allies {100f * StaticValues.minimushrumhealFraction}% health every second</style>. ", Assets.healBuffIcon, Color.green, false, false);
            roboballminiBuff = Buffs.AddNewBuff($"Solus Boost Buff- <style=cIsUtility> While holding any skill button, gain {100f * StaticValues.roboballattackspeedMultiplier}% attack speed </style>every second.", Assets.jumpBuffIcon, Color.grey, false, false);
            roboballminiattackspeedBuff = Buffs.AddNewBuff($"Solus Boost attackspeedBuff", Assets.predatorBuffIcon, Color.grey, true, false);
            voidbarnaclemortarBuff = Buffs.AddNewBuff($"Void Mortar Buff- While standing still, attack nearby enemies for <style=cIsDamage>{100 * StaticValues.mortarDamageCoefficient}% damage</style>, " +
                $"gain <style=cIsUtility>{StaticValues.voidmortarattackspeedGain} attackspeed </style> every ({StaticValues.voidmortarbaseDuration}/(CurrentArmor/BaseArmor)) second(s). " +
                $"Radius and damage scales with armor and attack speed. ", Assets.mortarBuffIcon, Color.magenta, false, false);
            voidbarnaclemortarattackspeedBuff = Buffs.AddNewBuff($"voidbarnaclemortarattackspeedBuff", Assets.attackspeedBuffIcon, Color.blue, true, false);
            voidjailerBuff = Buffs.AddNewBuff($"Gravity Buff- <style=cIsDamage>Slowing.</style><style=cIsUtility> While moving</style>, Pull nearby enemies and deal <style=cIsDamage>{100 * StaticValues.voidjailerDamageCoefficient}% damage</style> ." +
                $"The gap between attacks scales with movespeed. ", Assets.gravityBuffIcon, Color.magenta, false, false);
            claydunestriderBuff = Buffs.AddNewBuff($"claydunestriderBuff", Assets.claygooBuffIcon, Color.magenta, true, false);
            impbossBuff = Buffs.AddNewBuff($"Bleed Buff- <style=cIsDamage>Bleeding.</style> Attacks apply <style=cIsHealth>Bleed</style>.", Assets.bleedBuffIcon, Color.red, false, false);
            stonetitanBuff = Buffs.AddNewBuff($"Stone Skin Buff- <style=cIsUtility>Gain {StaticValues.stonetitanarmorGain} armor and flat damage reduction equal to your armor</style>. " +
                $"When you're below 50% health. damage can be reduced <style=cIsHealing>below zero and heal you</style>. ", Assets.skinBuffIcon, Color.magenta, false, false);
            vagrantBuff = Buffs.AddNewBuff($"Vagrant's Orb Buff- When striking an enemy for <style=cIsDamage>{100 * StaticValues.vagrantdamageThreshold}% or more damage</style>, Create a nova Explosion that stuns and deals <style=cIsDamage>{100 * StaticValues.vagrantDamageCoefficient / 3}% damage</style>. " +
                $"This bonus attack has a cooldown of {StaticValues.vagrantCooldown} seconds.", Assets.orbreadyBuffIcon, Color.magenta, false, false);
            vagrantdisableBuff = Buffs.AddNewBuff($"vagrant disable Buff ", Assets.orbdisableBuffIcon, Color.black, true, false);
            vagrantDebuff = Buffs.AddNewBuff($"vagrant Debuff", Assets.orbdisableBuffIcon, Color.white, true, false);
            magmawormBuff = Buffs.AddNewBuff($"Blazing Aura Buff- <style=cIsDamage>Burning.</style> Burn nearby enemies for <style=cIsDamage>{100 * StaticValues.magmawormCoefficient}% damage over {StaticValues.magmawormDuration} seconds</style>.", Assets.blazingBuffIcon, Color.magenta, false, false);
            overloadingwormBuff = Buffs.AddNewBuff($"Lighting Aura Buff- Summon lightning bolts on nearby enemies for <style=cIsDamage>{100 * StaticValues.overloadingCoefficient}% damage</style> every ({StaticValues.overloadingInterval}/Attackspeed) seconds. ", Assets.lightningBuffIcon, Color.magenta, false, false);
            jellyfishHealStacksBuff = Buffs.AddNewBuff($"Jellyfish Heal Stacks", Assets.healBuffIcon, Color.grey, true, false);
            commandoBuff = Buffs.AddNewBuff($"Double Tap Buff- All attacks hit twice, dealing <style=cIsDamage>{100 * StaticValues.commandoDamageMultiplier}% damage</style> of the attack, with a proc coefficient of {StaticValues.commandoProcCoefficient}.", Assets.critBuffIcon, Color.magenta, false, false);
            acridBuff = Buffs.AddNewBuff($"poison Buff", Assets.bleedBuffIcon, Color.green, false, false);
            captainBuff = Buffs.AddNewBuff($"Defensive Microbots Buff- Passively gain Microbots that shoot down nearby enemy projectiles. Drones are also given Microbots.", Assets.shieldBuffIcon, Color.red, false, false);
            loaderBuff = Buffs.AddNewBuff($"Scrap Barrier Buff- Gain <style=cIsUtility> Gain {100f * StaticValues.loaderBarrierGainCoefficient} of your max health as barrier</style> on all attacks.", Assets.shieldBuffIcon, Color.yellow, false, false);
            multBuff = Buffs.AddNewBuff($"mult Buff", Assets.predatorBuffIcon, Color.yellow, false, false);
            OFABuff = Buffs.AddNewBuff($"One For All Buff", Assets.lightningBuffIcon, Color.magenta, false, false);

            eliteDebuff = Buffs.AddNewBuff($"eliteDebuff", Assets.critBuffIcon, Color.black, false, true);

            bigbangBuff = Buffs.AddNewBuff($"bigbangBuff", Assets.orbreadyBuffIcon, new Color(0.67f, 0.87f, 0.93f), false, false);
            wisperBuff = Buffs.AddNewBuff($"wisperBuff", Assets.multiplierBuffIcon, new Color(0.52f, 0.48f, 0.39f), false, false);
            omniboostBuff = Buffs.AddNewBuff($"omniboost Buff", Assets.boostBuffIcon, Color.white, false, false);
            omniboostBuffStacks = Buffs.AddNewBuff($"omniboost Buff stacks", Assets.boostBuffIcon, Color.gray, true, false);
            omniboostDebuffStacks = Buffs.AddNewBuff($"omniboost Debuff stacks", Assets.boostBuffIcon, Color.black, true, true);
            gachaBuff = Buffs.AddNewBuff($"gacha Buff", Assets.attackspeedBuffIcon, Color.yellow, true, false);
            windShieldBuff = Buffs.AddNewBuff($"windShield Buff", Assets.shieldBuffIcon, Color.white, false, false);
            stoneFormBuff = Buffs.AddNewBuff($"stoneForm Buff", Assets.shieldBuffIcon, Color.grey, false, false);
            stoneFormStillBuff = Buffs.AddNewBuff($"stone Form Still Buff", Assets.skinBuffIcon, Color.grey, false, false);
            mechStanceBuff = Buffs.AddNewBuff($"mech Stance Buff", Assets.ruinDebuffIcon, Color.yellow, false, false);
            auraOfBlightBuff = Buffs.AddNewBuff($"Aura Of Poison Buff", Assets.bleedBuffIcon, Color.yellow, false, false);
            barbedSpikesBuff = Buffs.AddNewBuff($"barbed Spikes Buff", Assets.spikyDebuffIcon, Color.grey, false, false);
            ingrainBuff = Buffs.AddNewBuff($"ingrain Buff", Assets.medkitBuffIcon, Color.green, false, false);
            limitBreakBuff = Buffs.AddNewBuff($"limit Break Buff", Assets.lightningBuffIcon, Color.black, false, false);
            voidFormBuff = Buffs.AddNewBuff($"void form Buff", Assets.voidFogDebuffIcon, Color.magenta, false, false);
            elementalFusionFireBuff = Buffs.AddNewBuff($"elemental Fusion Fire Buff", Assets.multiplierBuffIcon, Color.red, false, false);
            elementalFusionFreezeBuff = Buffs.AddNewBuff($"elemental Fusion Freeze Buff", Assets.multiplierBuffIcon, Color.cyan, false, false);
            elementalFusionShockBuff = Buffs.AddNewBuff($"elemental Fusion Shock Buff", Assets.multiplierBuffIcon, Color.yellow, false, false);
            elementalFusionBuffStacks = Buffs.AddNewBuff($"elemental Fusion Buff Stacks", Assets.multiplierBuffIcon, Color.white, true, false);
            doubleTimeBuff = Buffs.AddNewBuff($"double Time Buff", Assets.warcryBuffIcon, Color.white, false, false);
            doubleTimeBuffStacks = Buffs.AddNewBuff($"double Time Buff Stacks", Assets.warcryBuffIcon, Color.yellow, true, false);
            blindSensesBuff = Buffs.AddNewBuff($"blindSenses Buff", Assets.cloakBuffIcon, Color.green, false, false);
            theWorldBuff = Buffs.AddNewBuff($"The World Buff", Assets.resonanceBuffIcon, Color.yellow, false, false);
            supernovaBuff = Buffs.AddNewBuff($"Supernova Buff Stacks- Absorb the damage you take, charging a supernova within you. When total damage is greater than <style=cIsUtility>{StaticValues.supernovaHealthThreshold* 100f}% of your MAX health</style>, unleash a colossal explosion, dealing <style=cIsDamage>{StaticValues.supernovaDamageCoefficient* 100f}% damage</style> in a large area around you. ", Assets.singularityBuffIcon, Color.cyan, true, false);
            deathAuraBuff = Buffs.AddNewBuff($"Death aura Buff", Assets.deathMarkDebuffIcon, Color.magenta, true, false);
            reversalBuff = Buffs.AddNewBuff($"Reversal Buff- Sprint to build up reversal stacks. When hit, <style=cIsUtility>damage is removed, and you teleport to the enemy.</style> Freeze enemies around them, dealing <style=cIsDamage>{StaticValues.reversalDamageCoefficient* 100f}% damage</style>", Assets.sprintBuffIcon, Color.blue, false, false);
            reversalBuffStacks = Buffs.AddNewBuff($"Reversal Buff stacks. ", Assets.sprintBuffIcon, Color.cyan, true, false);


            //debuff
            grovetenderChainDebuff = Buffs.AddNewBuff($"chainDebuff", Assets.tarBuffIcon, Color.blue, false, true);
            bigbangDebuff = Buffs.AddNewBuff($"bigbangDebuff", Assets.orbdisableBuffIcon, Color.green, true, true);
            doubleTimeDebuff = Buffs.AddNewBuff($"doubleTime Debuff", Assets.warcryBuffIcon, Color.black, false, true);
            theWorldDebuff = Buffs.AddNewBuff($"The World Debuff", Assets.resonanceBuffIcon, Color.black, false, true);
            extremeSpeedHitsDebuff = Buffs.AddNewBuff($"extremeSpeedHits Debuff", Assets.crippleBuffIcon, Color.magenta, true, true);
            deathAuraDebuff = Buffs.AddNewBuff($"Death aura Debuff", Assets.deathMarkDebuffIcon, Color.black, true, true);


            //Sprite TransformBuff = Addressables.LoadAssetAsync<BuffDef>($"RoR2/Base/LunarSkillReplacements/bdLunarSecondaryRoot.asset").WaitForCompletion().iconSprite;
            //transformBuff = AddNewBuff($"TransformTimer", TransformBuff, Color.yellow, false, false);

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
