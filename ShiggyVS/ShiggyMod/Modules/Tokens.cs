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
            #endregion

            #region Passive
            LanguageAPI.Add(prefix + "PASSIVE_NAME", "All For One");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION", "Shigaraki can grab quirks from anyone he's looking at. " +
                "Actives<style=cWorldEvent>(Circle)</style> replace main skills and Passives<style=cWorldEvent>(Triangle)</style> replace extra skills. <style=cSub>[Melee]</style> skills deal <style=cWorldEvent>[Decay].</style>" + Environment.NewLine +
                "<style=cWorldEvent>RightHanded</style> and <style=cWorldEvent>LeftHanded</style> skills can be used together. " +
                "<style=cIsUtility>He can sprint in any direction.</style>");
            #endregion

            #region Base Skills
            LanguageAPI.Add(prefix + "DECAY_NAME", "Decay");
            LanguageAPI.Add(prefix + "DECAY_DESCRIPTION", $"<style=cIsDamage>Agile.</style> " +
                $"Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Melee]</style>");
            LanguageAPI.Add(prefix + "BULLETLASER_NAME", "Bullet Laser");
            LanguageAPI.Add(prefix + "BULLETLASER_DESCRIPTION", $"<style=cIsDamage>Agile." +
                $"</style> Shoot 5 Lasers for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>. " + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged]</style>");
            LanguageAPI.Add(prefix + "AIRCANNON_NAME", "Air Cannon");
            LanguageAPI.Add(prefix + "AIRCANNON_DESCRIPTION", $"<style=cIsDamage>Agile.</style> " +
                $"Blasts an air shockwave behind you, dealing <style=cIsDamage>{100f * StaticValues.aircannonDamageCoeffecient}% damage</style> and propelling you forward." + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Melee]</style>");
            LanguageAPI.Add(prefix + "MULTIPLIER_NAME", "Multiplier");
            LanguageAPI.Add(prefix + "MULTIPLIER_DESCRIPTION", $"<style=cIsDamage>Agile." +
                $"</style> Boosts your next attack to deal <style=cIsDamage>{100f * StaticValues.multiplierCoefficient}% damage</style>. " +
                $"</style> Triples the number of projectiles, shots and decay stacks as well." + Environment.NewLine + Environment.NewLine +
                $"<style=cSub>[Ranged] [Melee]</style>");
            LanguageAPI.Add(prefix + "AFO_NAME", "All For One");
            LanguageAPI.Add(prefix + "AFO_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");

            #endregion



            #region Passive
            LanguageAPI.Add(prefix + "ALPHACONSTRUCT_NAME", "Barrier");
            LanguageAPI.Add(prefix + "ALPHACONSTRUCT_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "BEETLE_NAME", "Strength Boost");
            LanguageAPI.Add(prefix + "BEETLE_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "PEST_NAME", "Jump Boost");
            LanguageAPI.Add(prefix + "PEST_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "VERMIN_NAME", "Super Speed");
            LanguageAPI.Add(prefix + "VERMIN_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "GUP_NAME", "Spiky Body");
            LanguageAPI.Add(prefix + "GUP_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "LARVA_NAME", "Acid Jump");
            LanguageAPI.Add(prefix + "LARVA_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "LESSERWISP_NAME", "Ranged Boost");
            LanguageAPI.Add(prefix + "LESSERWISP_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "LUNAREXPLODER_NAME", "Lunar Aura");
            LanguageAPI.Add(prefix + "LUNAREXPLODER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "HERMITCRAB_NAME", "Mortar");
            LanguageAPI.Add(prefix + "HERMITCRAB_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "MINIMUSHRUM_NAME", "Healing Aura");
            LanguageAPI.Add(prefix + "MINIMUSHRUM_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "ROBOBALLMINI_NAME", "Glide");
            LanguageAPI.Add(prefix + "ROBOBALLMINI_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "VOIDBARNACLE_NAME", "Void Mortar");
            LanguageAPI.Add(prefix + "VOIDBARNACLE_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "VOIDJAILER_NAME", "Gravity");
            LanguageAPI.Add(prefix + "VOIDJAILER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "STONETITAN_NAME", "Stone Skin");
            LanguageAPI.Add(prefix + "STONETITAN_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "MAGMAWORM_NAME", "Blazing Aura");
            LanguageAPI.Add(prefix + "MAGMAWORM_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "OVERLOADINGWORM_NAME", "Lightning Aura");
            LanguageAPI.Add(prefix + "OVERLOADINGWORM_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "VAGRANT_NAME", "Vagrant's Orb");
            LanguageAPI.Add(prefix + "VAGRANT_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            #endregion

            #region Active 
            LanguageAPI.Add(prefix + "VULTURE_NAME", "Flight");
            LanguageAPI.Add(prefix + "VULTURE_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "BEETLEGUARD_NAME", "Fast Drop");
            LanguageAPI.Add(prefix + "BEETLEGUARD_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "BISON_NAME", "Charging");
            LanguageAPI.Add(prefix + "BISON_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "BRONZONG_NAME", "Spiked Ball Control");
            LanguageAPI.Add(prefix + "BRONZONG_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "APOTHECARY_NAME", "Clay AirStrike");
            LanguageAPI.Add(prefix + "APOTHECARY_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "TEMPLAR_NAME", "Clay Minigun");
            LanguageAPI.Add(prefix + "TEMPLAR_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "GREATERWISP_NAME", "Spirit Ball Control");
            LanguageAPI.Add(prefix + "GREATERWISP_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "IMP_NAME", "Blink");
            LanguageAPI.Add(prefix + "IMP_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "JELLYFISH_NAME", "Nova Explosion");
            LanguageAPI.Add(prefix + "JELLYFISH_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "LEMURIAN_NAME", "Fireball");
            LanguageAPI.Add(prefix + "LEMURIAN_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "LUNARGOLEM_NAME", "Slide Reset");
            LanguageAPI.Add(prefix + "LUNARGOLEM_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "LUNARWISP_NAME", "Lunar Minigun");
            LanguageAPI.Add(prefix + "LUNARWISP_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "PARENT_NAME", "Teleport");
            LanguageAPI.Add(prefix + "PARENT_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "STONEGOLEM_NAME", "Laser");
            LanguageAPI.Add(prefix + "STONEGOLEM_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "VOIDREAVER_NAME", "Nullifier Artillery");
            LanguageAPI.Add(prefix + "VOIDREAVER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "BEETLEQUEEN_NAME", "Acid Shotgun");
            LanguageAPI.Add(prefix + "BEETLEQUEEN_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "GROVETENDER_NAME", "Hook Shotgun");
            LanguageAPI.Add(prefix + "GROVETENDER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "CLAYDUNESTRIDER_NAME", "Rolling Clay");
            LanguageAPI.Add(prefix + "CLAYDUNESTRIDER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "SOLUSCONTROLUNIT_NAME", "Anti Gravity");
            LanguageAPI.Add(prefix + "SOLUSCONTROLUNIT_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "XICONSTRUCT_NAME", "Beam");
            LanguageAPI.Add(prefix + "XICONSTRUCT_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "VOIDDEVASTATOR_NAME", "Void Missiles");
            LanguageAPI.Add(prefix + "VOIDDEVASTATOR_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "SCAVENGER_NAME", "Throw Thqwibs");
            LanguageAPI.Add(prefix + "SCAVENGER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            #endregion

            #region Achievements
            LanguageAPI.Add("ACHIEVEMENT_" + prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_NAME", "Shiggy: Mastery");
            LanguageAPI.Add("ACHIEVEMENT_" + prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_DESCRIPTION", "As Shiggy, beat the game or obliterate on Monsoon.");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_UNLOCKABLE_NAME", "Shiggy: Mastery");
            #endregion


            #region Achievements
            LanguageAPI.Add(prefix + "KEYWORD_DECAY", $"<style=cKeywordName>Decay</style> Deal <style=cIsDamage>100% of your base damage</style> per second for 10 seconds. " +
                $"Each <style=cStack>stack reduces movespeed and attackspeed by 4%</style>. " +
                $"<style=cDeath>Instakills</style> at 50 stacks.");
            #endregion
            #endregion


        }
    }
}
