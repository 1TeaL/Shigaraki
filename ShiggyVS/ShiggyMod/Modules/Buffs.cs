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
        internal static BuffDef OFAFOBuff;
        internal static BuffDef machineFormBuff;
        internal static BuffDef gargoyleProtectionBuff;
        internal static BuffDef finalReleaseBuff;
        internal static BuffDef weatherReportBuff;
        internal static BuffDef decayAwakenedBuff;
        internal static BuffDef wildcardSpeedBuff;
        internal static BuffDef wildcardSlowBuff;
        internal static BuffDef wildcardDamageBuff;
        internal static BuffDef wildcardNoProjectileBuff;
        internal static BuffDef lightFormBuff;
        internal static BuffDef darknessFormBuff;
        internal static BuffDef lightAndDarknessFormBuff;

        //ultimate debuffs
        internal static BuffDef theWorldDebuff;
        internal static BuffDef extremeSpeedHitsDebuff;
        internal static BuffDef deathAuraDebuff;
        internal static BuffDef blastingZoneBurnDebuff;
        internal static BuffDef lightFormDebuff;
        internal static BuffDef darknessFormDebuff;
        internal static BuffDef lightAndDarknessFormDebuff;

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
            //passive buffs
            beetleBuff = Buffs.AddNewBuff($"Strength Buff- <style=cIsDamage>Gain {StaticValues.beetleFlatDamage} base damage</style>. ", Assets.boostBuffIcon, new Color(0.67f, 0.87f, 0.93f), false, false);
            alphashieldonBuff = Buffs.AddNewBuff($"Alpha Construct Shield On- Gain a barrier that blocks the next hit. Recharges after {StaticValues.alphaconstructCooldown} seconds. ", Assets.alphashieldonBuffIcon, Color.magenta, false, false);
            alphashieldoffBuff = Buffs.AddNewBuff($"Alpha Construct Shield Off", Assets.alphashieldoffBuffIcon, Color.black, true, false);
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
            acridBuff = Buffs.AddNewBuff($"Poison Buff- <style=cIsDamage>Poison.</style> Attacks apply <style=cIsHealth>Poison</style>.", Assets.bleedBuffIcon, Color.green, false, false);
            captainBuff = Buffs.AddNewBuff($"Defensive Microbots Buff- Passively gain Microbots that shoot down nearby enemy projectiles. Drones are also given Microbots.", Assets.shieldBuffIcon, Color.red, false, false);
            loaderBuff = Buffs.AddNewBuff($"Scrap Barrier Buff- Gain <style=cIsUtility> Gain {100f * StaticValues.loaderBarrierGainCoefficient} of your max health as barrier</style> on all attacks.", Assets.shieldBuffIcon, Color.yellow, false, false);
            bigbangBuff = Buffs.AddNewBuff($"Big Bang Buff -Each hit on an enemy builds up an explosive charge. On the {StaticValues.bigbangBuffThreshold}th hit, an explosion occurs, dealing <style=cIsDamage>{100f * StaticValues.bigbangBuffCoefficient}% of the hit's damage</style>.", Assets.orbreadyBuffIcon, new Color(0.67f, 0.87f, 0.93f), false, false);
            wisperBuff = Buffs.AddNewBuff($"Wisper Buff- Every attack that has a proc coefficient shoots a homing wisp towards the target for <style=cIsDamage>{100f * StaticValues.wisperBuffDamageCoefficient}% damage</style> with no proc coefficient.", Assets.multiplierBuffIcon, new Color(0.52f, 0.48f, 0.39f), false, false);
            omniboostBuff = Buffs.AddNewBuff($"Omniboost Buff- Damage and attackspeed is boosted by <style=cIsDamage>{StaticValues.omniboostBuffCoefficient + 1}x</style>. Every {StaticValues.omniboostNumberOfHits}rd hit on the same enemy further boosts this buff by <style=cIsDamage>{StaticValues.omniboostBuffStackCoefficient * 100f}% per stack</style>.", Assets.boostBuffIcon, Color.white, false, false);
            omniboostBuffStacks = Buffs.AddNewBuff($"Omniboost Buff stacks", Assets.boostBuffIcon, Color.gray, true, false);
            omniboostDebuffStacks = Buffs.AddNewBuff($"Omniboost Debuff stacks", Assets.boostBuffIcon, Color.black, true, true);
            gachaBuff = Buffs.AddNewBuff($"Gacha Buff- Get a random item every <style=cIsUtility>{StaticValues.gachaBuffThreshold} seconds</style>.", Assets.attackspeedBuffIcon, Color.yellow, true, false);
            stoneFormBuff = Buffs.AddNewBuff($"StoneForm Buff- While still for {StaticValues.stoneFormWaitDuration} seconds, enter stone form. Gain <style=cIsUtility>{StaticValues.stoneFormBlockChance}% block chance and take no knockback</style>.", Assets.shieldBuffIcon, Color.grey, false, false);
            stoneFormStillBuff = Buffs.AddNewBuff($"Stone Form Buff", Assets.skinBuffIcon, Color.grey, false, false);
            auraOfBlightBuff = Buffs.AddNewBuff($"Aura Of Blight Buff- Apply blight to enemies around you every second, dealing <style=cIsDamage>{StaticValues.auraOfBlightBuffDotDamage * 100f}% over {StaticValues.auraOfBlightBuffDotDuration} seconds</style>.", Assets.bleedBuffIcon, Color.yellow, false, false);
            barbedSpikesBuff = Buffs.AddNewBuff($"Barbed Spikes Buff- Deal <style=cIsDamage>{StaticValues.barbedSpikesDamageCoefficient * 100f}% damage</style> to nearby enemies every {StaticValues.barbedSpikesBuffThreshold} seconds. " +
                $"Damage increases the area of effect. ", Assets.spikyDebuffIcon, Color.grey, false, false);
            ingrainBuff = Buffs.AddNewBuff($"Ingrain Buff- Gain health regen equivalent to <style=cIsHealing>{StaticValues.ingrainBuffHealthRegen * 100f}% of your max health</style>.", Assets.medkitBuffIcon, Color.green, false, false);
            elementalFusionFireBuff = Buffs.AddNewBuff($"Elemental Fusion Fire Buff- Gain elemental power, allowing you to burn, freeze or shock enemies. Every {StaticValues.elementalFusionThreshold} hits cycle between <style=cIsDamage>burning, freezing or shocking</style>.", Assets.multiplierBuffIcon, Color.red, false, false);
            elementalFusionFreezeBuff = Buffs.AddNewBuff($"Elemental Fusion Freeze Buff- Gain elemental power, allowing you to burn, freeze or shock enemies. Every {StaticValues.elementalFusionThreshold} hits cycle between <style=cIsDamage>burning, freezing or shocking</style>.", Assets.multiplierBuffIcon, Color.cyan, false, false);
            elementalFusionShockBuff = Buffs.AddNewBuff($"Elemental Fusion Shock Buff- Gain elemental power, allowing you to burn, freeze or shock enemies. Every {StaticValues.elementalFusionThreshold} hits cycle between <style=cIsDamage>burning, freezing or shocking</style>.", Assets.multiplierBuffIcon, Color.yellow, false, false);
            elementalFusionBuffStacks = Buffs.AddNewBuff($"Elemental Fusion Buff Stacks", Assets.multiplierBuffIcon, Color.white, true, false);
            doubleTimeBuff = Buffs.AddNewBuff($"Double Time Buff- Perceive time at a heightened speed. Nearby enemies' <style=cIsUtility>movespeed and attackspeed are slowed down by {StaticValues.doubleTimeSlowCoefficient * 100f}%</style>. Killing enemies grant <style=cIsDamage>{StaticValues.doubleTimeCoefficient * 100f}% damage, attackspeed and movespeed</style> additively. " +
                $"Stacks are halved every {StaticValues.doubleTimeThreshold} seconds. ", Assets.warcryBuffIcon, Color.white, false, false);
            doubleTimeBuffStacks = Buffs.AddNewBuff($"Double Time Buff Stacks", Assets.warcryBuffIcon, Color.yellow, true, false);
            blindSensesBuff = Buffs.AddNewBuff($"Blind Senses Buff- Gain the heightened senses of blindness. Gain <style=cIsUtility>{StaticValues.blindSensesBlockChance * 100f}% dodge chance</style>. <style=cIsUtility>Blocking an attack causes you to counterattack</style>, stunning and dealing <style=cIsDamage>{StaticValues.blindSensesDamageCoefficient * 100f}% of the damage</style> you would have taken to the attacker. Getting Tougher Times increases the chances of this counterattack independent of dodging as well.", Assets.cloakBuffIcon, Color.green, false, false);
            supernovaBuff = Buffs.AddNewBuff($"Supernova Buff Stacks- Absorb the damage you take, charging a supernova within you. When total damage is greater than <style=cIsUtility>{StaticValues.supernovaHealthThreshold * 100f}% of your MAX health</style>, unleash a colossal explosion, dealing <style=cIsDamage>{StaticValues.supernovaDamageCoefficient * 100f}% damage</style> in a large area around you. ", Assets.singularityBuffIcon, Color.cyan, true, false);
            deathAuraBuff = Buffs.AddNewBuff($"Death aura Buff", Assets.deathMarkDebuffIcon, Color.magenta, true, false);
            reversalBuff = Buffs.AddNewBuff($"Reversal Buff- Sprint to build up reversal stacks. When hit, <style=cIsUtility>damage is removed, and you dash away from the enemy.</style> Freeze enemies around them, dealing <style=cIsDamage>{StaticValues.reversalDamageCoefficient * 100f}% damage</style>.", Assets.sprintBuffIcon, Color.blue, false, false);
            reversalBuffStacks = Buffs.AddNewBuff($"Reversal Buff stacks", Assets.sprintBuffIcon, Color.cyan, true, false);
            machineFormBuff = Buffs.AddNewBuff($"Machine Form Buff- Materialize machinery around yourself, passively shooting missiles and bullets to nearby enemies, dealing <style=cIsDamage>{StaticValues.machineFormDamageCoefficient * 100f}% damage</style>. ", Assets.resonanceBuffIcon, Color.blue, false, false);
            gargoyleProtectionBuff = Buffs.AddNewBuff($"Gargoyle Protection Buff- Gain the protection of a gargoyle, <style=cIsUtility>reducing damage taken by {StaticValues.gargoyleProtectionDamageReductionCoefficient * 100f}% and reflecting it back to the attacker</style>. ", Assets.resonanceBuffIcon, Color.blue, false, false);
            weatherReportBuff = Buffs.AddNewBuff($"Weather Report Buff- Gain the ability to manipulate the weather. Every {StaticValues.weatherReportThreshold} seconds, cause nearby enemies to randomly be <style=cIsDamage>struck by lightning, be frozen, hit with a fire tornado, sent flying up or sent down, dealing {StaticValues.weatherReportDamageCoefficient * 100f}%</style>. ", Assets.spikeBuffIcon, Color.white, false, false);
            decayAwakenedBuff = Buffs.AddNewBuff($"Weather Report Buff- Gain the ability to manipulate the weather. Every {StaticValues.weatherReportThreshold} seconds, cause nearby enemies to randomly be <style=cIsDamage>struck by lightning, be frozen, hit with a fire tornado, sent flying up or sent down, dealing {StaticValues.weatherReportDamageCoefficient * 100f}%</style>. ", Assets.decayBuffIcon, Color.white, false, false);



            //shiggy buffs
            airwalkBuff = Buffs.AddNewBuff($"Air walk acceleration Buff", Assets.jumpBuffIcon, Color.magenta, false, false);
            multiplierBuff = Buffs.AddNewBuff($"Multiplier Buff", Assets.multiplierBuffIcon, Color.magenta, false, false);
            multBuff = Buffs.AddNewBuff($"mult Buff", Assets.predatorBuffIcon, Color.yellow, false, false);
            OFABuff = Buffs.AddNewBuff($"One For All Buff", Assets.lightningBuffIcon, Color.magenta, false, false);

            //active buffs
            windShieldBuff = Buffs.AddNewBuff($"WindShield Buff", Assets.shieldBuffIcon, Color.white, false, false);
            mechStanceBuff = Buffs.AddNewBuff($"Mech Stance Buff", Assets.ruinDebuffIcon, Color.yellow, false, false);
            limitBreakBuff = Buffs.AddNewBuff($"Limit Break Buff", Assets.lightningBuffIcon, Color.black, false, false);
            voidFormBuff = Buffs.AddNewBuff($"Void Form Buff", Assets.voidFogDebuffIcon, Color.magenta, false, false);
            theWorldBuff = Buffs.AddNewBuff($"The World Buff", Assets.resonanceBuffIcon, Color.yellow, false, false);
            OFAFOBuff = Buffs.AddNewBuff($"One For All For One Buff", Assets.lightningBuffIcon, Color.black, false, false);
            finalReleaseBuff = Buffs.AddNewBuff($"Final Release Buff", Assets.mercExposeIcon, Color.white, true, false);
            lightFormBuff = Buffs.AddNewBuff($"Light Form Buff", Assets.lunarRootIcon, Color.white, false, false);
            darknessFormBuff = Buffs.AddNewBuff($"Darkness Form Buff", Assets.lunarRootIcon, Color.black, false, false);
            lightAndDarknessFormBuff = Buffs.AddNewBuff($"Light And Darkness Form Buff", Assets.lunarRootIcon, Color.magenta, false, false);

            //wild card buffs
            wildcardSpeedBuff = Buffs.AddNewBuff($"Wildcard Speed Buff- Move {StaticValues.wildcardSpeedCoefficient}x faster. ", Assets.speedBuffIcon, Color.white, false, false);
            wildcardSlowBuff = Buffs.AddNewBuff($"Wildcard Slow Buff- Move {StaticValues.wildcardSpeedCoefficient}x slower. ", Assets.tarBuffIcon, Color.white, false, false);
            wildcardDamageBuff = Buffs.AddNewBuff($"Wildcard Damage Buff- Deal {StaticValues.wildcardDamageCoefficient}x damage. ", Assets.boostBuffIcon, Color.white, false, false);
            wildcardNoProjectileBuff = Buffs.AddNewBuff($"Wildcard No Projectile Buff- Destroy all projectiles. ", Assets.cloakBuffIcon, Color.white, false, false);

            //debuffs
            grovetenderChainDebuff = Buffs.AddNewBuff($"chainDebuff", Assets.tarBuffIcon, Color.blue, false, true);
            bigbangDebuff = Buffs.AddNewBuff($"bigbangDebuff", Assets.orbdisableBuffIcon, Color.green, true, true);
            doubleTimeDebuff = Buffs.AddNewBuff($"doubleTime Debuff", Assets.warcryBuffIcon, Color.black, false, true);
            theWorldDebuff = Buffs.AddNewBuff($"The World Debuff", Assets.resonanceBuffIcon, Color.black, false, true);
            extremeSpeedHitsDebuff = Buffs.AddNewBuff($"extremeSpeedHits Debuff", Assets.crippleBuffIcon, Color.magenta, true, true);
            deathAuraDebuff = Buffs.AddNewBuff($"Death aura Debuff", Assets.deathMarkDebuffIcon, Color.black, true, true);
            decayDebuff = Buffs.AddNewBuff($"decay Debuff", Assets.decayBuffIcon, Color.magenta, true, true);
            eliteDebuff = Buffs.AddNewBuff($"eliteDebuff", Assets.critBuffIcon, Color.black, false, true);
            blastingZoneBurnDebuff = Buffs.AddNewBuff($"Blaze debuff", Assets.strongerBurnIcon, Color.red, true, true);
            lightFormDebuff = Buffs.AddNewBuff($"Light Form Debuff", Assets.crippleBuffIcon, Color.white, true, true);
            darknessFormDebuff = Buffs.AddNewBuff($"Darkness Form Debuff", Assets.crippleBuffIcon, Color.black, true, true);
            lightAndDarknessFormDebuff = Buffs.AddNewBuff($"Light And Darkness Form Debuff", Assets.crippleBuffIcon, Color.magenta, true, true);


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
