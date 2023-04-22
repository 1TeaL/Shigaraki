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

            string desc = $"Shiggy is a multi-option survivor that can steal quirks from monster and base survivors to create his own playstyle.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            desc = desc + $"< ! > Steal quirk with {Config.AFOHotkey.Value}. Remove quirks with {Config.RemoveHotkey.Value}. Give quirks with. All rebindable in the configs." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Grabbing a quirk when owning a specific quirk already will create a combination, these combinations can further combine." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > The Plus Chaos Meter in the middle increases naturally and by killing enemies, it is used for All For One and certain skills." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Some quirks are passive buffs, while others are active skills." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Try out all the quirks and craft your ultimate build!" + Environment.NewLine + Environment.NewLine;



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
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION", $"Steal quirks by looking at a target and pressing {Config.AFOHotkey.Value}. Remove them with {Config.RemoveHotkey.Value}. " + Environment.NewLine +
                Helpers.Passive("[Plus Chaos Meter] [Decay] [Air Walk]") + Environment.NewLine +
                "<style=cSub>[RightHanded]</style> and <style=cSub>[LeftHanded]</style> skills can be used together. " +
                "<style=cIsUtility>He has a double jump. He can sprint in any direction.</style>");
            #endregion

            #region Base Skills
            LanguageAPI.Add(prefix + "DECAY_NAME", "Decay");
            LanguageAPI.Add(prefix + "DECAY_DESCRIPTION", $"<style=cIsDamage>Agile.</style> " +
                $"Slam and <style=cWorldEvent>[Decay]</style> the ground/air around you, dealing <style=cIsDamage>{100f * StaticValues.decayattackDamageCoefficient}% damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[RightHanded]</style> <style=cWorldEvent>[Decay]</style>");
            LanguageAPI.Add(prefix + "BULLETLASER_NAME", "Bullet Laser");
            LanguageAPI.Add(prefix + "BULLETLASER_DESCRIPTION", $"<style=cIsDamage>Agile." +
                $"</style> Shoot piercing lasers for <style=cIsDamage>5x{100f * StaticValues.bulletlaserDamageCoefficient}% damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "AIRCANNON_NAME", "Air Cannon");
            LanguageAPI.Add(prefix + "AIRCANNON_DESCRIPTION", $"<style=cIsDamage>Agile.</style> " +
                $"Blasts an air shockwave behind you, dealing <style=cIsDamage>{100f * StaticValues.aircannonDamageCoefficient}% damage</style> and propelling you forward." + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[LeftHanded]</style>");
            LanguageAPI.Add(prefix + "MULTIPLIER_NAME", "Multiplier");
            LanguageAPI.Add(prefix + "MULTIPLIER_DESCRIPTION", $"<style=cIsDamage>Agile." +
                $"</style> Boosts your next attack to deal <style=cIsDamage>{StaticValues.multiplierCoefficient}x damage</style>. " +
                $"</style> Triples the number of projectiles, shots and decay stacks as well." + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged]  [RightHanded]</style>");
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
            LanguageAPI.Add(prefix + "BEETLE_DESCRIPTION", $"<style=cIsDamage>Gain {StaticValues.beetleFlatDamage} base damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Lesser Wisp]</style>");
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
                $"<style=cSub>[Jump]</style> <style=cWorldEvent>[Decay]</style>");
            LanguageAPI.Add(prefix + "LESSERWISP_NAME", "Haste");
            LanguageAPI.Add(prefix + "LESSERWISP_DESCRIPTION", $"<style=cIsDamage>Gain {StaticValues.lesserwispFlatAttackSpeed} flat attackspeed</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Beetle]</style>");
            LanguageAPI.Add(prefix + "LUNAREXPLODER_NAME", "Lunar Barrier");
            LanguageAPI.Add(prefix + "LUNAREXPLODER_DESCRIPTION", $"Gain a <style=cIsUtility>Shield equal to {StaticValues.lunarexploderShieldCoefficient}% of your max health</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Scavenger]</style>");
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
            LanguageAPI.Add(prefix + "VOIDJAILER_DESCRIPTION", $"<style=cIsDamage>Slowing.</style><style=cIsUtility> While moving</style>, Pull nearby enemies and deal <style=cIsDamage>{100 * StaticValues.voidjailerDamageCoefficient}% damage</style> ." +
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
            LanguageAPI.Add(prefix + "LOADER_DESCRIPTION", $"Gain <style=cIsUtility> Gain {100f *StaticValues.loaderBarrierGainCoefficient} of your max health as barrier</style> on all attacks. ");
            #endregion

            #region Active 
            LanguageAPI.Add(prefix + "VULTURE_NAME", "Wind Blast");
            LanguageAPI.Add(prefix + "VULTURE_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Create a gust of wind, pushing and stunning enemies in front of you for <style=cIsDamage>{100f*StaticValues.vultureDamageCoefficient}% damage</style>. "
                + Environment.NewLine + Environment.NewLine + 
                $" <style=cSub>[Engineer]</style>");
            LanguageAPI.Add(prefix + "BEETLEGUARD_NAME", "Fast Drop");
            LanguageAPI.Add(prefix + "BEETLEGUARD_DESCRIPTION", $"<style=cIsDamage>Stunning. Agile.</style> Drop and slam down, stunning and dealing <style=cIsDamage>{100f * StaticValues.beetleguardSlamDamageCoefficient}% damage</style> and gaining <style=cIsHealing>{Modules.StaticValues.beetleguardSlamBarrierCoefficient * 100f}% of your max health as Barrier</style> on hit. " +
                $"Damage and radius and barrier gain scales with drop time and movespeed. " + Environment.NewLine + Environment.NewLine +
                $" <style=cWorldEvent>[Decay]</style>");
            LanguageAPI.Add(prefix + "BISON_NAME", "Charging");
            LanguageAPI.Add(prefix + "BISON_DESCRIPTION", $"<style=cIsDamage>Stunning. Agile.</style> Charge forward at super speed, and if you slam into a solid object, generates a shockwave that stuns enemies for <style=cIsDamage>{100f * StaticValues.bisonchargeDamageCoefficient}% damage</style> in a radius. Hold the button to keep charging. " +
                $"Damage and radius scales with charge duration and movespeed. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[LeftHanded] [Movespeed]</style> <style=cWorldEvent>[Decay]</style>");
            LanguageAPI.Add(prefix + "BRONZONG_NAME", "Spiked Ball Control");
            LanguageAPI.Add(prefix + "BRONZONG_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Summon 3 spiked balls, then release them, dealing <style=cIsDamage>{100f * StaticValues.bronzongballDamageCoefficient}% damage</style> per ball. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "APOTHECARY_NAME", "Clay AirStrike");
            LanguageAPI.Add(prefix + "APOTHECARY_DESCRIPTION", $"<style=cIsDamage>Tar. Agile.</style> Release a tar shockwave, dealing <style=cIsDamage>{100f * StaticValues.clayapothecarymortarDamageCoefficient}% damage</style> and send a mortar into the sky, which rains down on enemies around you for <style=cIsDamage>{100f * StaticValues.clayapothecarymortarDamageCoefficient}% damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [LeftHanded] [Decay]</style>");
            LanguageAPI.Add(prefix + "TEMPLAR_NAME", "Clay Minigun");
            LanguageAPI.Add(prefix + "TEMPLAR_DESCRIPTION", $"<style=cIsDamage>Tar. Agile.</style> Shoot a rapid hail of tar bullets, tarring and dealing <style=cIsDamage>{100f * StaticValues.claytemplarminigunDamageCoefficient}% damage per bullet</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "ELDERLEMURIAN_NAME", "Fire Blast");
            LanguageAPI.Add(prefix + "ELDERLEMURIAN_DESCRIPTION", $"<style=cIsDamage>Burning. Agile.</style> Hold the button down to charge a fire blast which, when released, deals <style=cIsDamage>{100f * StaticValues.elderlemurianfireblastDamageCoefficient}% damage</style> per hit. " +
                $"Number of hits and radius scales with charge duration." + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[RightHanded]</style>");
            LanguageAPI.Add(prefix + "GREATERWISP_NAME", "Spirit Boost");
            LanguageAPI.Add(prefix + "GREATERWISP_DESCRIPTION", $"<style=cIsDamage>Agile.</style> For {StaticValues.greaterwispballbuffDuration} seconds, your attacks explode, dealing <style=cIsDamage>{100f * StaticValues.greaterwispballDamageCoefficient}% damage</style> of the attack. " +
                $"Additional uses adds to the current duration. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[RightHanded]</style>");
            LanguageAPI.Add(prefix + "IMP_NAME", "Blink");
            LanguageAPI.Add(prefix + "IMP_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Blink a short distance away, scaling with movespeed. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[LeftHanded]</style>");
            LanguageAPI.Add(prefix + "JELLYFISH_NAME", "Regenerate");
            LanguageAPI.Add(prefix + "JELLYFISH_DESCRIPTION", $"<style=cIsDamage> Agile.</style> Store half the damage you take, decaying by <style=cIsUtility>{100f * StaticValues.JellyfishHealTickRate}% of your max HP every second</style>. " +
                $"Activate this skill to <style=cIsHealing>Heal based on the number of stacks</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Alpha Construct] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "LEMURIAN_NAME", "Fireball");
            LanguageAPI.Add(prefix + "LEMURIAN_DESCRIPTION", $"<style=cIsDamage>Burning. Agile.</style> Shoot a fireball, burning and dealing <style=cIsDamage>{100f * StaticValues.lemurianfireballDamageCoefficient}% damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "LUNARGOLEM_NAME", "Slide Reset");
            LanguageAPI.Add(prefix + "LUNARGOLEM_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Slide, Reducing all cooldowns by {StaticValues.lunarGolemSlideCooldown}s. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[LeftHanded]</style>");
            LanguageAPI.Add(prefix + "LUNARWISP_NAME", "Lunar Minigun");
            LanguageAPI.Add(prefix + "LUNARWISP_DESCRIPTION", $"<style=cIsDamage>Cripple. Agile.</style> Shoot a rapid hail of lunar bullets, crippling and dealing <style=cIsDamage>{100f * StaticValues.lunarwispminigunDamageCoefficient}% damage per bullet</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "PARENT_NAME", "Teleport");
            LanguageAPI.Add(prefix + "PARENT_DESCRIPTION", $"<style=cIsDamage>Stunning. Agile.</style> Teleport to the target you're looking at and generate a shockwave on arrival that stuns enemies for <style=cIsDamage>{100f * StaticValues.parentDamageCoefficient}% damage</style> in a radius. " +
                $"Damage and radius scales with charge duration. " + Environment.NewLine + Environment.NewLine +
                $" <style=cWorldEvent>[Decay]</style>");
            LanguageAPI.Add(prefix + "STONEGOLEM_NAME", "Laser");
            LanguageAPI.Add(prefix + "STONEGOLEM_DESCRIPTION", $"<style=cIsDamage>Stunning. Agile.</style> Hold the button down to charge a laser which, when released, deals <style=cIsDamage>{100f * StaticValues.stonegolemDamageCoefficient}% damage</style>. " +
                $"Damage and radius scales with charge duration." + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "VOIDREAVER_NAME", "Nullifier Artillery");
            LanguageAPI.Add(prefix + "VOIDREAVER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hold the button down to constantly summon nullifier bombs on the target, dealing <style=cIsDamage>{100f * StaticValues.voidreaverDamageCoefficient}% damage per bomb</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "BEETLEQUEEN_NAME", "Summon Ally");
            LanguageAPI.Add(prefix + "BEETLEQUEEN_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Summon a Survivor that <style=cIsUtility>inherits all your items</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[?which skill yet] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "GRANDPARENT_NAME", "Solar Flare");
            LanguageAPI.Add(prefix + "GRANDPARENT_DESCRIPTION", $"Hold the button to summon a miniature sun. Sprinting or letting go of the button cancels the skill. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[RightHanded]</style>");
            LanguageAPI.Add(prefix + "GROVETENDER_NAME", "Chain");
            LanguageAPI.Add(prefix + "GROVETENDER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> <style=cIsUtility>Chain</style> nearby enemies for <style=cIsUtility>{StaticValues.grovetenderDuration} seconds, immobilising them</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Greater Wisp] [LeftHanded]</style>");
            LanguageAPI.Add(prefix + "CLAYDUNESTRIDER_NAME", "Tar boost");
            LanguageAPI.Add(prefix + "CLAYDUNESTRIDER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> For the next {StaticValues.claydunestriderbuffDuration} seconds, your attacks <style=cIsDamage>Tar</style>, gain <style=cIsHealing>{100f * StaticValues.claydunestriderHealCoefficient} Lifesteal</style> and <style=cIsUtility>{100f * StaticValues.claydunestriderAttackSpeed}% attackspeed</style>. " +
                $"Additional uses adds to the current duration. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[RightHanded]</style>");
            LanguageAPI.Add(prefix + "SOLUSCONTROLUNIT_NAME", "Anti Gravity");
            LanguageAPI.Add(prefix + "SOLUSCONTROLUNIT_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Summon a large anti-gravity array. After a delay, it explodes, launching enemies and dealing <style=cIsDamage>{100f * StaticValues.soluscontrolunitDamageCoefficient}% damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged]</style>");
            LanguageAPI.Add(prefix + "XICONSTRUCT_NAME", "Beam");
            LanguageAPI.Add(prefix + "XICONSTRUCT_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hold the button to shoot a devastating beam, piercing and dealing <style=cIsDamage>{100f * StaticValues.xiconstructDamageCoefficient}% damage per tick</style>. " +
                $"The beam also explodes on hit, dealing <style=cIsDamage>{100f * StaticValues.xiconstructDamageCoefficient}% damage</style> to nearby enemies. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "VOIDDEVASTATOR_NAME", "Void Missiles");
            LanguageAPI.Add(prefix + "VOIDDEVASTATOR_DESCRIPTION", $"<style=cIsDamage> Agile.</style> Shoot 2x{StaticValues.voiddevastatorTotalMissiles} homing missiles, dealing <style=cIsDamage>{100f * StaticValues.voiddevastatorDamageCoefficient}% damage per missile</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "SCAVENGER_NAME", "Throw Thqwibs");
            LanguageAPI.Add(prefix + "SCAVENGER_DESCRIPTION", $"<style=cIsDamage> Agile.</style> Throw {StaticValues.scavenger} thqwibs that activate <style=cDeath>On-Kill effects</style> and deal <style=cIsDamage>{100f * StaticValues.scavengerDamageCoefficient}% damage</style> each. " + Environment.NewLine + Environment.NewLine +
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
            LanguageAPI.Add(prefix + "MERC_NAME", "Wind Assault");
            LanguageAPI.Add(prefix + "MERC_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Dash forward with a blade of wind, dealing <style=cIsDamage>{100f * StaticValues.mercDamageCoefficient}% damage</style> and expose all enemies hit. " +
                 Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Air cannon]</style>");
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

            #region Synergised Active
            LanguageAPI.Add(prefix + "SWEEPINGBEAM_NAME", "Sweeping Beam");
            LanguageAPI.Add(prefix + "SWEEPINGBEAM_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Fire a sweeping beam, dealing <style=cIsDamage>{100f * StaticValues.sweepingBeamDamageCoefficient}% per hit</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[RapidPierce] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "BLACKHOLEGLAIVE_NAME", "Black Hole Glaive");
            LanguageAPI.Add(prefix + "BLACKHOLEGLAIVE_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Throw a seeking glaive that bounces up to {StaticValues.blackholeGlaiveMaxBounceCount} times for <style=cIsDamage>{100f * StaticValues.blackholeGlaiveDamageCoefficient}% per hit</style>, damaging and pulling nearby enemies as well. Damage increases by <style=cIsDamage>{100f * (StaticValues.blackholeGlaiveDamageCoefficientPerBounce-1f)} per bounce</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Mech Stance] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "GRAVITATIONALDOWNFORCE_NAME", "Gravitational Downforce");
            LanguageAPI.Add(prefix + "GRAVITATIONALDOWNFORCE_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Amplify the force of gravity around you, sending enemies down and dealing <style=cIsDamage>{100f * StaticValues.gravitationalDownforceDamageCoefficient}% damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Elemental Fusion] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "WINDSHIELD_NAME", "Wind Shield");
            LanguageAPI.Add(prefix + "WINDSHIELD_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Generate a barrier of wind around you for {StaticValues.windShieldDuration}, removing nearby projectiles and stunning nearby enemies for <style=cIsDamage>{100f * StaticValues.windShieldDamageCoefficient}% damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Wind Slash] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "GENESIS_NAME", "Genesis");
            LanguageAPI.Add(prefix + "GENESIS_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Rays of light from the sky, attacking enemies all around you forfor <style=cIsDamage>{StaticValues.genesisNumberOfAttacks}x{100f * StaticValues.genesisDamageCoefficient}% damage</style>. " +
                $"Attackspeed increases the number of attacks. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Shadow Claw] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "REFRESH_NAME", "Refresh");
            LanguageAPI.Add(prefix + "REFRESH_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Recharge all skills and recover <style=cIsUtility>{100f * StaticValues.refreshEnergyCoefficient}% of your maximum plus chaos</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Gacha] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "EXPUNGE_NAME", "Expunge");
            LanguageAPI.Add(prefix + "EXPUNGE_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Expunge enemies in an area, dealing <style=cIsDamage>{100f * StaticValues.expungeDamageCoefficient}% damage, with each debuff increasing damage by {100f* StaticValues.expungeDamageMultiplier}%</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Orbital Spikes] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "SHADOWCLAW_NAME", "Shadow Claw");
            LanguageAPI.Add(prefix + "SHADOWCLAW_DESCRIPTION", $"<style=cIsDamage>Slayer. Agile.</style> <style=cIsUtility>Cloak yourself</style> while holding the button. " +
                $"Release to claw nearby enemies for <style=cIsDamage>{StaticValues.shadowClawHits}x{100f * StaticValues.shadowClawDamageCoefficient}% damage</style>. " +
                $"Kills reset all your cooldowns. Staying cloaked gradually slows your movespeed." +
                $"Attackspeed increases the number of attacks. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Genesis] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "ORBITALSTRIKE_NAME", "Orbital Strike");
            LanguageAPI.Add(prefix + "ORBITALSTRIKE_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hold to aim and release to call an orbital strike to a location, dealing <style=cIsDamage>{100f * StaticValues.orbitalStrikeDamageCoefficient}% damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Blast Burn] [RightHanded]</style>");
            LanguageAPI.Add(prefix + "THUNDERCLAP_NAME", "Thunderclap");
            LanguageAPI.Add(prefix + "THUNDERCLAP_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Dash forward while covered in electricity, dealing <style=cIsDamage>{100f * StaticValues.mercDamageCoefficient}% damage</style> and shock all enemies hit. " +
                 Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Mach Punch]</style>");
            LanguageAPI.Add(prefix + "BLASTBURN_NAME", "Blast Burn");
            LanguageAPI.Add(prefix + "BLASTBURN_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hold to radiate heat from your body, dealing <style=cIsDamage>{100f * StaticValues.blastBurnDamageCoefficient}% damage</style> in pulses, burning all enemies hit. The size of the blast increases after each pulse. " +
                $"Attackspeed decreases the gap between pulses." +
                 Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Orbital Strike]</style>");
            LanguageAPI.Add(prefix + "BARRIERJELLY_NAME", "Barrier Jelly");
            LanguageAPI.Add(prefix + "BARRIERJELLY_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Become <style=cIsUtility>invincible for {StaticValues.barrierJellyDuration} seconds</style>. "  +
                 Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Blind Senses]</style>");
            LanguageAPI.Add(prefix + "MECHSTANCE_NAME", "Mech Stance");
            LanguageAPI.Add(prefix + "MECHSTANCE_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Become <style=cIsUtility>immune to fall damage, walking causes quakes that deal {100f* StaticValues.mechStanceDamageCoefficient}% damage</style>. " +
                $"However, jumping prevents you from gaining height. " +
                $"Movespeed increases the size and damage of the quakes. " +
                 Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Blackhole Glaive]</style>");
            #endregion

            #region Synergised Passive
            LanguageAPI.Add(prefix + "BIGBANG_NAME", "Big Bang");
            LanguageAPI.Add(prefix + "BIGBANG_DESCRIPTION", $"Each hit on an enemy builds up an explosive charge. On the {StaticValues.bigbangBuffThreshold}th hit, an explosion occurs, dealing <style=cIsDamage>{100f * StaticValues.bigbangBuffHealthCoefficient}% of the enemy's max health</style>. " + 
                Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Wisper]</style>");
            LanguageAPI.Add(prefix + "WISPER_NAME", "Wisper");
            LanguageAPI.Add(prefix + "WISPER_DESCRIPTION", $"Every attack that has a proc coefficient shoots a homing wisp towards the target for <style=cIsDamage>{100f * StaticValues.wisperBuffDamageCoefficient}% damage</style> with no proc coefficient. " + 
                Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Big Bang]</style>");
            LanguageAPI.Add(prefix + "OMNIBOOST_NAME", "Omniboost");
            LanguageAPI.Add(prefix + "OMNIBOOST_DESCRIPTION", $"Damage and attackspeed is boosted by <style=cIsDamage>{StaticValues.omniboostBuffCoefficient+1}x</style>. Killing an enemy further boosts this buff by <style=cIsDamage>{StaticValues.omniboostBuffStackCoefficient * 100f}% per kill</style>. " + 
                Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Double Time]</style>");
            LanguageAPI.Add(prefix + "GACHA_NAME", "Gacha");
            LanguageAPI.Add(prefix + "GACHA_DESCRIPTION", $"Get a random item every <style=cIsUtility>{StaticValues.gachaBuffThreshold} seconds</style>. " + 
                Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Refresh]</style>");
            LanguageAPI.Add(prefix + "STONEFORM_NAME", "Stone Form");
            LanguageAPI.Add(prefix + "STONEFORM_DESCRIPTION", $"While still for {StaticValues.stoneFormWaitDuration} seconds, enter stone form. Gain <style=cIsUtility>{StaticValues.stoneFormBlockChance}% block chance and take no knockback</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ingrain]</style>");
            LanguageAPI.Add(prefix + "AURAOFBLIGHT_NAME", "Aura Of Blight");
            LanguageAPI.Add(prefix + "AURAOFBLIGHT_DESCRIPTION", $"Apply blight to enemies around you every second, dealing <style=cIsDamage>{StaticValues.auraOfBlightBuffDotDamage * 100f}% over {StaticValues.auraOfBlightBuffDotDuration} seconds</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Decay Plus Ultra]</style>");
            LanguageAPI.Add(prefix + "BARBEDSPIKES_NAME", "Barbed Spikes");
            LanguageAPI.Add(prefix + "BARBEDSPIKES_DESCRIPTION", $"Deal <style=cIsDamage>{StaticValues.barbedSpikesDamageCoefficient* 100f}% damage</style> to nearby enemies every {StaticValues.barbedSpikesBuffThreshold} seconds. " +
                $"Damage increases the area of effect. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Expunge]</style>");
            LanguageAPI.Add(prefix + "INGRAIN_NAME", "Ingrain");
            LanguageAPI.Add(prefix + "INGRAIN_DESCRIPTION", $"Gain health regen equivalent to <style=cIsHealing>{StaticValues.ingrainBuffHealthRegen* 100f}% of your max health</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Stone Form]</style>");
            #endregion
            #region Achievements
            LanguageAPI.Add("ACHIEVEMENT_" + prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_NAME", "Shiggy: Mastery");
            LanguageAPI.Add("ACHIEVEMENT_" + prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_DESCRIPTION", "As Shiggy, beat the game or obliterate on Monsoon.");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_UNLOCKABLE_NAME", "Shiggy: Mastery");
            #endregion


            #region Keywords
            LanguageAPI.Add(prefix + "KEYWORD_DECAY", $"<style=cKeywordName>Decay</style> Deal <style=cIsDamage>{100f *StaticValues.decayDamageCoefficient} damage</style> per second for {StaticValues.decayDamageTimer} seconds. This spreads to nearby targets every {StaticValues.decayadditionalTimer} seconds." +
                $"Each <style=cStack>stack reduces movespeed and attackspeed by 4%</style>. " +
                $"<style=cDeath>Instakills</style> at {StaticValues.decayInstaKillThreshold} stacks.");
            LanguageAPI.Add(prefix + "KEYWORD_PASSIVE", $"<style=cKeywordName>Plus Chaos Meter</style>"
                + "Shigaraki has a" + Helpers.Passive(" meter that regenerates over time and through killing enemies. Stealing quirks, giving quirks, and specific skills cost plus chaos") + "."
                + Environment.NewLine
                + Environment.NewLine
            + $"<style=cKeywordName>Decay</style>"
                + $"Melee skills apply Decay. Decay deals <style=cIsDamage>{100f *StaticValues.decayDamageCoefficient} damage</style> per second for {StaticValues.decayDamageTimer} seconds. This spreads to nearby targets every {StaticValues.decayadditionalTimer} seconds."
                + Environment.NewLine
                + Environment.NewLine
            + $"<style=cKeywordName>Air Walk</style>"
                + "<style=cIsUtility>Holding jump in the air after 0.5 seconds let's him Float, flying up or slowing his descent while using a skill</style>."
                + Helpers.Passive(" Drains plus ultra"));
            #endregion
            #endregion


        }
    }
}
