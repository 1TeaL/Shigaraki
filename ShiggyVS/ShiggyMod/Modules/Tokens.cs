using R2API;
using System;

namespace ShiggyMod.Modules
{
    internal static class Tokens
    {
        internal static void AddTokens()
        {
            #region Shiggy
            string prefix = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_";

            string desc = "Shiggy can transform into any character/monster<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Shiggy copies every stat besides regen, armor and movespeed. The transformation will have 10% of its Max HP + Shiggy's Max HP" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Use Shiggy's equipment to transform back to Shiggy. It can also drop naturally" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Shiggy's secondary and utility skills are items that when activated give buffs that are carried over when transformed as well" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Aim to increase your HP as he has low base HP" + Environment.NewLine + Environment.NewLine;



            string outro = "..and so he left, continuing to destroy everything";
            string outroFailure = "They cheated!";

            LanguageAPI.Add(prefix + "NAME", "Shigaraki Tomura");
            LanguageAPI.Add(prefix + "DESCRIPTION", desc);
            LanguageAPI.Add(prefix + "SUBTITLE", "2nd All For One User");
            LanguageAPI.Add(prefix + "LORE", "Let's clear this level and go home.");
            LanguageAPI.Add(prefix + "OUTRO_FLAVOR", outro);
            LanguageAPI.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            LanguageAPI.Add(prefix + "DEFAULT_SKIN_NAME", "Default");
            LanguageAPI.Add(prefix + "MASTERY_SKIN_NAME", "Shiny");
            LanguageAPI.Add(prefix + "HANDLESS_SKIN_NAME", "Handless");
            #endregion

            #region Passive
            LanguageAPI.Add(prefix + "PASSIVE_NAME", "All For One");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION", $"Press the [{Config.AFOHotkey.Value}] key to use <style=cIsUtility>All For One and steal quirks</style>."
                + $" Press the [{Config.RemoveHotkey.Value}] key to <style=cIsUtility>remove quirks</style>." + Environment.NewLine +
                "<style=cSub>[Melee]</style> skills deal <style=cWorldEvent>[Decay]</style>. " + Environment.NewLine +
                "<style=cSub>[RightHanded]</style> and <style=cSub>[LeftHanded]</style> skills can be used together. " +
                "<style=cIsUtility>He can sprint in any direction and has a double jump.</style>");
            #endregion

            #region Base Skills
            LanguageAPI.Add(prefix + "DECAY_NAME", "Decay");
            LanguageAPI.Add(prefix + "DECAY_DESCRIPTION", $"<style=cIsDamage>Agile.</style> " +
                $"Slam and <style=cWorldEvent>[Decay]</style> the ground/air around you, dealing <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Melee] [RightHanded]</style> <style=cWorldEvent>[Decay]</style>");
            LanguageAPI.Add(prefix + "BULLETLASER_NAME", "Bullet Laser");
            LanguageAPI.Add(prefix + "BULLETLASER_DESCRIPTION", $"<style=cIsDamage>Agile." +
                $"</style> Shoot piercing lasers for <style=cIsDamage>5x{100f * StaticValues.bulletlaserDamageCoeffecient}% damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "AIRCANNON_NAME", "Air Cannon");
            LanguageAPI.Add(prefix + "AIRCANNON_DESCRIPTION", $"<style=cIsDamage>Agile.</style> " +
                $"Blasts an air shockwave behind you, dealing <style=cIsDamage>{100f * StaticValues.aircannonDamageCoeffecient}% damage</style> and propelling you forward." + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Melee] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "MULTIPLIER_NAME", "Multiplier");
            LanguageAPI.Add(prefix + "MULTIPLIER_DESCRIPTION", $"<style=cIsDamage>Agile." +
                $"</style> Boosts your next attack to deal <style=cIsDamage>{StaticValues.multiplierCoefficient}x damage</style>. " +
                $"</style> Triples the number of projectiles, shots and decay stacks as well." + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [Melee] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "AFO_NAME", "All For One");
            LanguageAPI.Add(prefix + "AFO_DESCRIPTION", $"Press the [{Config.AFOHotkey.Value}] key to use <style=cIsUtility>All For One and steal quirks</style>." 
                + $" Press the [{Config.RemoveHotkey.Value}] key to <style=cIsUtility>remove quirks</style>." +
                " Actives<style=cWorldEvent>[Circle]</style> and Passives<style=cWorldEvent>[Triangle]</style> have different indicators." );
            LanguageAPI.Add(prefix + "CHOOSESKILL_NAME", "Choose Skill Slot");
            LanguageAPI.Add(prefix + "CHOOSESKILL_DESCRIPTION", $"Press this to <style=cIsUtility>slot in the stolen quirk</style>.");
            LanguageAPI.Add(prefix + "REMOVESKILL_NAME", "Remove Skill Slot");
            LanguageAPI.Add(prefix + "REMOVESKILL_DESCRIPTION", $"Press this to <style=cIsUtility>remove the quirk from this slot</style>.");

            #endregion



            #region Passive
            LanguageAPI.Add(prefix + "ALPHACONSTRUCT_NAME", "Barrier");
            LanguageAPI.Add(prefix + "ALPHACONSTRUCT_DESCRIPTION", $" Gain a barrier that blocks the next hit. Recharges after {StaticValues.alphaconstructCooldown} seconds. ");
            LanguageAPI.Add(prefix + "BEETLE_NAME", "Strength Boost");
            LanguageAPI.Add(prefix + "BEETLE_DESCRIPTION", $"<style=cIsDamage>Gain {StaticValues.beetledamageMultiplier}x damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Melee]</style>");
            LanguageAPI.Add(prefix + "PEST_NAME", "Jump Boost");
            LanguageAPI.Add(prefix + "PEST_DESCRIPTION", $"<style=cIsUtility>Gain 4 extra jumps and jump power</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Jump]</style>");
            LanguageAPI.Add(prefix + "VERMIN_NAME", "Super Speed");
            LanguageAPI.Add(prefix + "VERMIN_DESCRIPTION", $"<style=cIsUtility>Gain {StaticValues.verminmovespeedMultiplier}x movespeed and change sprint speed to {StaticValues.verminsprintMultiplier}x</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Movespeed]</style>");
            LanguageAPI.Add(prefix + "GUP_NAME", "Spiky Body");
            LanguageAPI.Add(prefix + "GUP_DESCRIPTION", $"<style=cIsDamage>Gain spikes that deal <style=cIsDamage>{StaticValues.spikedamageCoefficient * 100}% damage</style> to those around you when you're hit</style>. ");
            LanguageAPI.Add(prefix + "LARVA_NAME", "Acid Jump");
            LanguageAPI.Add(prefix + "LARVA_DESCRIPTION", $"<style=cIsDamage>Release an Acidic blast</style> when <style=cIsUtility>jumping and landing</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Melee] [Jump]</style> <style=cWorldEvent>[Decay]</style>");
            LanguageAPI.Add(prefix + "LESSERWISP_NAME", "Ranged Boost");
            LanguageAPI.Add(prefix + "LESSERWISP_DESCRIPTION", $"<style=cIsDamage>Gain {StaticValues.lesserwispdamageMultiplier}x damage</style>. All <style=cSub>[Ranged]</style> attacks deal <style=cIsDamage>{StaticValues.lesserwisprangedMultiplier}x damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged]</style>");
            LanguageAPI.Add(prefix + "LUNAREXPLODER_NAME", "Lunar Aura");
            LanguageAPI.Add(prefix + "LUNAREXPLODER_DESCRIPTION", $"<style=cIsHealth>At <50% health</style>, periodically release a <style=cIsDamage>Lunar blaze that deals <style=cIsDamage>{StaticValues.lunarexploderDamageCoefficient}% damage</style> per tick to enemies on the ground</style>. ");
            LanguageAPI.Add(prefix + "HERMITCRAB_NAME", "Mortar");
            LanguageAPI.Add(prefix + "HERMITCRAB_DESCRIPTION", $"While standing still, attack nearby enemies for <style=cIsDamage>{100 * StaticValues.mortarDamageCoefficient}% damage</style> and " +
                $"gain <style=cIsUtility>{StaticValues.mortararmorGain} armor </style> every ({StaticValues.mortarbaseDuration}/Attackspeed) second(s). " +
                $"Radius and Damage scales with armor and attackspeed. " +
                $"" + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Mortar]</style>");
            LanguageAPI.Add(prefix + "MINIMUSHRUM_NAME", "Healing Aura");
            LanguageAPI.Add(prefix + "MINIMUSHRUM_DESCRIPTION", $"<style=cIsHealing>Heal yourself and nearby allies {100f * StaticValues.minimushrumhealFraction}% health every second</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Healing]</style>");
            LanguageAPI.Add(prefix + "ROBOBALLMINI_NAME", "Solus Boost");
            LanguageAPI.Add(prefix + "ROBOBALLMINI_DESCRIPTION", $"<style=cIsUtility> While holding any skill button, gain {100f * StaticValues.roboballattackspeedMultiplier}% attack speed </style>every second. ");
            //LanguageAPI.Add(prefix + "ROBOBALLMINI_NAME", "Glide");
            //LanguageAPI.Add(prefix + "ROBOBALLMINI_DESCRIPTION", $"<style=cIsUtility> While holding the jump button, glide across the sky without losing height</style>. " + Environment.NewLine + Environment.NewLine +
            //    );
            LanguageAPI.Add(prefix + "VOIDBARNACLE_NAME", "Void Mortar");
            LanguageAPI.Add(prefix + "VOIDBARNACLE_DESCRIPTION", $"While standing still, attack nearby enemies for <style=cIsDamage>{100 * StaticValues.mortarDamageCoefficient}% damage</style>, " +
                $"gain <style=cIsUtility>{StaticValues.voidmortarattackspeedGain} attackspeed </style> every ({StaticValues.voidmortarbaseDuration}/(CurrentArmor/BaseArmor)) second(s). " +
                $"Radius and damage scales with armor and attack speed. " +
                $"" + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Mortar]</style>");
            LanguageAPI.Add(prefix + "VOIDJAILER_NAME", "Gravity");
            LanguageAPI.Add(prefix + "VOIDJAILER_DESCRIPTION", $"<style=cIsDamage>Slowing.</style><style=cIsUtility> While moving</style>, Pull nearby enemies and deal <style=cIsDamage>{100 * StaticValues.voidjailerDamageCoeffecient}% damage</style> ." +
                $"The gap between attacks scales with movespeed. " +
                $" " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [Movespeed]</style>");
            LanguageAPI.Add(prefix + "IMPBOSS_NAME", "Bleed");
            LanguageAPI.Add(prefix + "IMPBOSS_DESCRIPTION", $"<style=cIsDamage>Bleeding.</style> Attacks apply <style=cIsHealth>Bleed</style>. " +
                $"" + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Bleeding]</style>");
            LanguageAPI.Add(prefix + "STONETITAN_NAME", "Stone Skin");
            LanguageAPI.Add(prefix + "STONETITAN_DESCRIPTION", $"<style=cIsUtility>Gain {StaticValues.stonetitanarmorGain} armor and flat damage reduction equal to your armor</style>. " +
                $"When you're below 50% health. damage can be reduced <style=cIsHealing>below zero and heal you</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Healing]</style>");
            LanguageAPI.Add(prefix + "MAGMAWORM_NAME", "Blazing Aura");
            LanguageAPI.Add(prefix + "MAGMAWORM_DESCRIPTION", $"<style=cIsDamage>Burning.</style> Burn nearby enemies for <style=cIsDamage>{100 * StaticValues.magmawormCoefficient}% damage over {StaticValues.magmawormDuration} seconds</style>. " +
                $"" + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Movespeed]</style>");
            LanguageAPI.Add(prefix + "OVERLOADINGWORM_NAME", "Lightning Aura");
            LanguageAPI.Add(prefix + "OVERLOADINGWORM_DESCRIPTION", $"Summon lightning bolts on nearby enemies for <style=cIsDamage>{100 * StaticValues.overloadingCoefficient}% damage</style> every ({StaticValues.overloadingInterval}/Attackspeed) seconds. ");
            LanguageAPI.Add(prefix + "VAGRANT_NAME", "Vagrant's Orb");
            LanguageAPI.Add(prefix + "VAGRANT_DESCRIPTION", $"When striking an enemy for <style=cIsDamage>{100 * StaticValues.vagrantdamageThreshold}% or more damage</style>, Create a nova Explosion that stuns and deals <style=cIsDamage>{100 * StaticValues.vagrantDamageCoefficient/3}% damage</style>. " +
                $"This bonus attack has a cooldown of {StaticValues.vagrantCooldown} seconds.");
            LanguageAPI.Add(prefix + "ACRID_NAME", "Poison");
            LanguageAPI.Add(prefix + "ACRID_DESCRIPTION", $"<style=cIsDamage>Poison.</style> Attacks apply <style=cIsHealth>Poison</style>. " +
                $"" + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Poison]</style>");
            LanguageAPI.Add(prefix + "COMMANDO_NAME", "Double Tap");
            LanguageAPI.Add(prefix + "COMMANDO_DESCRIPTION", $"All attacks hit twice, dealing <style=cIsDamage>{100 * StaticValues.commandoDamageMultiplier}% damage</style> of the attack, with a proc coefficient of {StaticValues.commandoProcCoefficient}. ");
            LanguageAPI.Add(prefix + "CAPTAIN_NAME", "Defensive Microbots");
            LanguageAPI.Add(prefix + "CAPTAIN_DESCRIPTION", $"Passively gain Microbots that shoot down nearby enemy projectiles. Drones are also given Microbots. " );
            LanguageAPI.Add(prefix + "LOADER_NAME", "Scrap Barrier");
            LanguageAPI.Add(prefix + "LOADER_DESCRIPTION", $"Gain <style=cIsUtility> Gain 5% of your max health as barrier</style> and <style=cIsDamage>{100 * StaticValues.loaderDamageMultiplier}% damage</style> on all <style=cSub>[Melee]</style> attacks. " +
                $"" + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Melee]</style>");
            #endregion

            #region Active 
            LanguageAPI.Add(prefix + "VULTURE_NAME", "Flight");
            LanguageAPI.Add(prefix + "VULTURE_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Jump and float in the air, disabling gravity for {StaticValues.alloyvultureflyduration} seconds.");
            LanguageAPI.Add(prefix + "BEETLEGUARD_NAME", "Fast Drop");
            LanguageAPI.Add(prefix + "BEETLEGUARD_DESCRIPTION", $"<style=cIsDamage>Stunning. Agile.</style> Drop and slam down, stunning and dealing <style=cIsDamage>{100f * StaticValues.beetleguardslamDamageCoeffecient}% damage</style> and gaining <style=cIsHealing>5% of your max health as Barrier</style> on hit. " +
                $"Damage, radius and barrier gain scales with drop time and movespeed. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Melee]</style> <style=cWorldEvent>[Decay]</style>");
            LanguageAPI.Add(prefix + "BISON_NAME", "Charging");
            LanguageAPI.Add(prefix + "BISON_DESCRIPTION", $"<style=cIsDamage>Stunning. Agile.</style> Charge forward at super speed, and if you slam into a solid object, generates a shockwave that stuns enemies for <style=cIsDamage>{100f * StaticValues.bisonchargeDamageCoeffecient}% damage</style> in a radius. Hold the button to keep charging. " +
                $"Damage and radius scales with charge duration and movespeed. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Melee] [LeftHanded] [Movespeed]</style> <style=cWorldEvent>[Decay]</style>");
            LanguageAPI.Add(prefix + "BRONZONG_NAME", "Spiked Ball Control");
            LanguageAPI.Add(prefix + "BRONZONG_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Summon 3 spiked balls, then release them, dealing <style=cIsDamage>{100f * StaticValues.bronzongballDamageCoeffecient}% damage</style> per ball. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "APOTHECARY_NAME", "Clay AirStrike");
            LanguageAPI.Add(prefix + "APOTHECARY_DESCRIPTION", $"<style=cIsDamage>Tar. Agile.</style> Release a tar shockwave, dealing <style=cIsDamage>{100f * StaticValues.clayapothecarymortarDamageCoeffecient}% damage</style> and send a mortar into the sky, which rains down on enemies around you for <style=cIsDamage>{100f * StaticValues.clayapothecarymortarDamageCoeffecient}% damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Melee] [Ranged] [LeftHanded] [Decay]</style>");
            LanguageAPI.Add(prefix + "TEMPLAR_NAME", "Clay Minigun");
            LanguageAPI.Add(prefix + "TEMPLAR_DESCRIPTION", $"<style=cIsDamage>Tar. Agile.</style> Shoot a rapid hail of tar bullets, tarring and dealing <style=cIsDamage>{100f * StaticValues.claytemplarminigunDamageCoeffecient}% damage per bullet</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "ELDERLEMURIAN_NAME", "Fire Blast");
            LanguageAPI.Add(prefix + "ELDERLEMURIAN_DESCRIPTION", $"<style=cIsDamage>Burning. Agile.</style> Hold the button down to charge a fire blast which, when released, deals <style=cIsDamage>{100f * StaticValues.elderlemurianfireblastDamageCoefficient}% damage</style> per hit. " +
                $"Number of hits and radius scales with charge duration." + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Melee] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "GREATERWISP_NAME", "Spirit Boost");
            LanguageAPI.Add(prefix + "GREATERWISP_DESCRIPTION", $"<style=cIsDamage>Agile.</style> For {StaticValues.greaterwispballbuffDuration} seconds, your attacks explode, dealing <style=cIsDamage>{100f * StaticValues.greaterwispballDamageCoeffecient}% damage</style> of the attack. " +
                $"Additional uses adds to the current duration. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[RightHanded]</style>");
            LanguageAPI.Add(prefix + "IMP_NAME", "Blink");
            LanguageAPI.Add(prefix + "IMP_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Blink a short distance away, scaling with movespeed. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[LeftHanded]</style>");
            LanguageAPI.Add(prefix + "JELLYFISH_NAME", "Nova Explosion");
            LanguageAPI.Add(prefix + "JELLYFISH_DESCRIPTION", $"<style=cIsDamage>Stunning. Agile.</style> Detonate an explosion on the target, stunning and dealing <style=cIsDamage>{100f * StaticValues.jellyfishnovaDamageCoeffecient}% damage</style>. " +
                $"This explosion can hurt the user as well. " +
                $"Radius scales with attackspeed. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "LEMURIAN_NAME", "Fireball");
            LanguageAPI.Add(prefix + "LEMURIAN_DESCRIPTION", $"<style=cIsDamage>Burning. Agile.</style> Shoot a fireball, burning and dealing <style=cIsDamage>{100f * StaticValues.lemurianfireballDamageCoeffecient}% damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "LUNARGOLEM_NAME", "Slide Reset");
            LanguageAPI.Add(prefix + "LUNARGOLEM_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Slide, resetting coolodwns for all other skills. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[LeftHanded]</style>");
            LanguageAPI.Add(prefix + "LUNARWISP_NAME", "Lunar Minigun");
            LanguageAPI.Add(prefix + "LUNARWISP_DESCRIPTION", $"<style=cIsDamage>Cripple. Agile.</style> Shoot a rapid hail of lunar bullets, crippling and dealing <style=cIsDamage>{100f * StaticValues.lunarwispminigunDamageCoeffecient}% damage per bullet</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "PARENT_NAME", "Teleport");
            LanguageAPI.Add(prefix + "PARENT_DESCRIPTION", $"<style=cIsDamage>Stunning. Agile.</style> Teleport to the target you're looking at and generate a shockwave on arrival that stuns enemies for <style=cIsDamage>{100f * StaticValues.parentDamageCoeffecient}% damage</style> in a radius. " +
                $"Damage and radius scales with charge duration. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Melee]</style> <style=cWorldEvent>[Decay]</style>");
            LanguageAPI.Add(prefix + "STONEGOLEM_NAME", "Laser");
            LanguageAPI.Add(prefix + "STONEGOLEM_DESCRIPTION", $"<style=cIsDamage>Stunning. Agile.</style> Hold the button down to charge a laser which, when released, deals <style=cIsDamage>{100f * StaticValues.stonegolemDamageCoeffecient}% damage</style>. " +
                $"Damage and radius scales with charge duration." + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "VOIDREAVER_NAME", "Nullifier Artillery");
            LanguageAPI.Add(prefix + "VOIDREAVER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hold the button down to constantly summon nullifier bombs on the target, dealing <style=cIsDamage>{100f * StaticValues.voidreaverDamageCoeffecient}% damage per bomb</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "BEETLEQUEEN_NAME", "Acid Shotgun");
            LanguageAPI.Add(prefix + "BEETLEQUEEN_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Shoot an acid shotgun infront of you for <style=cIsDamage>5x{100f * StaticValues.beetlequeenDamageCoeffecient}%</style>, leaving an acid puddle on the ground. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Melee] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "GRANDPARENT_NAME", "Solar Flare");
            LanguageAPI.Add(prefix + "GRANDPARENT_DESCRIPTION", $"Hold the button to summon a miniature sun. Sprinting or letting go of the button cancels the skill. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[RightHanded]</style>");
            LanguageAPI.Add(prefix + "GROVETENDER_NAME", "Hook Shotgun");
            LanguageAPI.Add(prefix + "GROVETENDER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Shoot 5 hooks sequentially, pulling enemies and dealing <style=cIsDamage>{100f * StaticValues.grovetenderDamageCoeffecient}% damage per hook</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "CLAYDUNESTRIDER_NAME", "Tar boost");
            LanguageAPI.Add(prefix + "CLAYDUNESTRIDER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> For the next {StaticValues.claydunestriderbuffDuration} seconds, your attacks <style=cIsDamage>Tar</style>, gain <style=cIsHealing>{100f * StaticValues.claydunestriderHealCoefficient} Lifesteal</style> and <style=cIsUtility>{100f * StaticValues.claydunestriderAttackSpeed}% attackspeed</style>. " +
                $"Additional uses adds to the current duration. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[RightHanded]</style>");
            LanguageAPI.Add(prefix + "SOLUSCONTROLUNIT_NAME", "Anti Gravity");
            LanguageAPI.Add(prefix + "SOLUSCONTROLUNIT_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Summon a large anti-gravity array. After a delay, it explodes, launching enemies and dealing <style=cIsDamage>{100f * StaticValues.soluscontrolunitDamageCoeffecient}% damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged]</style>");
            LanguageAPI.Add(prefix + "XICONSTRUCT_NAME", "Beam");
            LanguageAPI.Add(prefix + "XICONSTRUCT_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hold the button to shoot a devastating beam, piercing and dealing <style=cIsDamage>{100f * StaticValues.xiconstructDamageCoeffecient}% damage per tick</style>. " +
                $"The beam also explodes on hit, dealing <style=cIsDamage>{100f * StaticValues.xiconstructDamageCoeffecient}% damage</style> to nearby enemies. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "VOIDDEVASTATOR_NAME", "Void Missiles");
            LanguageAPI.Add(prefix + "VOIDDEVASTATOR_DESCRIPTION", $"<style=cIsDamage> Agile.</style> Shoot 2x{StaticValues.voiddevastatorTotalMissiles} homing missiles, dealing <style=cIsDamage>{100f * StaticValues.voiddevastatorDamageCoeffecient}% damage per missile</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "SCAVENGER_NAME", "Throw Thqwibs");
            LanguageAPI.Add(prefix + "SCAVENGER_DESCRIPTION", $"<style=cIsDamage> Agile.</style> Throw {StaticValues.scavengerProjectileCount} thqwibs that activate <style=cDeath>On-Kill effects</style> and deal <style=cIsDamage>{100f * StaticValues.scavengerDamageCoeffecient}% damage</style> each. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "ARTIFICERFLAMETHROWER_NAME", "Elementality: Fire");
            LanguageAPI.Add(prefix + "ARTIFICERFLAMETHROWER_DESCRIPTION", $"<style=cIsDamage>Burning. Agile.</style> Burn all enemies in front of you for <style=cIsDamage>{100f * StaticValues.artificerflamethrowerDamageCoefficient}% damage</style>. " + Environment.NewLine +
                $"Cycle to Elementality: Ice." + Environment.NewLine +
                $"<style=cSub>[Ranged] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "ARTIFICERICEWALL_NAME", "Elementality: Ice");
            LanguageAPI.Add(prefix + "ARTIFICERICEWALL_DESCRIPTION", $"<style=cIsDamage>Freezing. Agile.</style> Create a barrier that freezes enemies for <style=cIsDamage>{100f * StaticValues.artificericewallDamageCoefficient}% damage</style>. " + Environment.NewLine +
                $"Cycle to Elementality: Lightning." + Environment.NewLine +
                $"<style=cSub>[Ranged] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "ARTIFICERLIGHTNINGORB_NAME", "Elementality: Lightning");
            LanguageAPI.Add(prefix + "ARTIFICERLIGHTNINGORB_DESCRIPTION", $"<style=cIsDamage>Stunning. Agile.</style> Charge up an exploding nano-bomb that deals <style=cIsDamage>{100f * StaticValues.artificerlightningorbMinDamageCoefficient}%-{100f * StaticValues.artificerlightningorbMaxDamageCoefficient}% damage</style>. " + Environment.NewLine +
                $"Cycle to Elementality: Fire." + Environment.NewLine +
                $"<style=cSub>[Ranged] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "BANDIT_NAME", "Lights Out");
            LanguageAPI.Add(prefix + "BANDIT_DESCRIPTION", $"<style=cIsDamage>Slayer. Agile.</style> <style=cIsUtility>Cloak yourself for {StaticValues.banditcloakDuration} seconds</style> and ready a shot while holding the button. " +
                $"Release to fire the shot for <style=cIsDamage>{100f * StaticValues.banditDamageCoefficient}% damage</style>. " +
                $"Kills reset all your cooldowns." + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "ENGI_NAME", "Turret");
            LanguageAPI.Add(prefix + "ENGI_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Place a turret that inherits all your items. Fires a cannon for <style=cIsDamage>100% damage</style>. Can place up to 2. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[RightHanded]</style>");
            LanguageAPI.Add(prefix + "HUNTRESS_NAME", "Flurry");
            LanguageAPI.Add(prefix + "HUNTRESS_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Fire {StaticValues.huntressmaxArrowCount/2} seeking arrows at the target for <style=cIsDamage>3x{100f * StaticValues.huntressDamageCoefficient}% damage</style>. " +
                $"<style=cIsUtility>Critical Strikes fire {StaticValues.huntressmaxArrowCount} arrows</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "MERC_NAME", "Eviscerate");
            LanguageAPI.Add(prefix + "MERC_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Target the nearest enemy, attacking them for <style=cIsDamage>{100f * StaticValues.mercDamageCoefficient}% damage</style> repeatedly. " +
                $"<style=cIsUtility>You cannot be hit for the duration</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Melee] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "MULTBUFF_NAME", "Power Stance");
            LanguageAPI.Add(prefix + "MULTBUFF_DESCRIPTION", $"<style=cIsDamage>Agile.</style>Adopt a stance and gain <style=cIsUtility>{StaticValues.multArmor} armor, {StaticValues.multAttackspeed}x attackspeed but have {StaticValues.multMovespeed}x </style>. " +
                $"Reactivate to cancel the stance." + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[RightHanded]</style>");
            LanguageAPI.Add(prefix + "MULTBUFFCANCEL_NAME", "Power Stance: Cancel");
            LanguageAPI.Add(prefix + "MULTBUFFCANCEL_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Cancel the stance. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[RightHanded]</style>");
            LanguageAPI.Add(prefix + "RAILGUNNNER_NAME", "Cryocharged Railgun");
            LanguageAPI.Add(prefix + "RAILGUNNNER_DESCRIPTION", $"<style=cIsDamage>Freezing. Agile.</style> Hold to ready a freezing, piercing round. " +
                $"Release to fire the round for <style=cIsDamage>{100f * StaticValues.railgunnerDamageCoefficient}% damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "REX_NAME", "Seed Barrage");
            LanguageAPI.Add(prefix + "REX_DESCRIPTION", $"<style=cIsDamage>Agile.</style> <style=cIsHealth>Costs {100f * StaticValues.rexHealthCost}% health</style>. Launch a mortar into the sky for <style=cIsDamage>{100f * StaticValues.rexDamageCoefficient}% damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "VOIDFIEND_NAME", "Cleanse");
            LanguageAPI.Add(prefix + "VOIDFIEND_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Disappear into the Void, <style=cIsUtility>cleansing all debuffs</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[LeftHanded]</style>");
            #endregion

            #region Achievements
            LanguageAPI.Add("ACHIEVEMENT_" + prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_NAME", "Shiggy: Mastery");
            LanguageAPI.Add("ACHIEVEMENT_" + prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_DESCRIPTION", "As Shiggy, beat the game or obliterate on Monsoon.");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_UNLOCKABLE_NAME", "Shiggy: Mastery");
            #endregion


            #region Keywords
            LanguageAPI.Add(prefix + "KEYWORD_DECAY", $"<style=cKeywordName>Decay</style> Deal <style=cIsDamage>100% of your base damage</style> per second for 10 seconds. " +
                $"Each <style=cStack>stack reduces movespeed and attackspeed by 4%</style>. " +
                $"<style=cDeath>Instakills</style> at 50 stacks.");
            #endregion
            #endregion


        }
    }
}
