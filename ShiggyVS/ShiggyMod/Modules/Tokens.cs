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
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION", "Shigaraki can grab quirks from anyone he's looking at. Actives replace the respective skill below and passives take up the slot but both can still be replaced. All melee attacks deal Decay.<style=cIsUtility> He can sprint in any direction.</style>");
            #endregion

            #region Base Skills
            LanguageAPI.Add(prefix + "DECAY_NAME", "Struggle");
            LanguageAPI.Add(prefix + "DECAY_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");

            LanguageAPI.Add(prefix + "BULLETLASER_NAME", "Struggle");
            LanguageAPI.Add(prefix + "BULLETLASER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");

            LanguageAPI.Add(prefix + "AIRCANNON_NAME", "Struggle");
            LanguageAPI.Add(prefix + "AIRCANNON_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");

            LanguageAPI.Add(prefix + "MULTIPLIER_NAME", "Struggle");
            LanguageAPI.Add(prefix + "MULTIPLIER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");

            LanguageAPI.Add(prefix + "AFO_NAME", "Struggle");
            LanguageAPI.Add(prefix + "AFO_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");

            #endregion



            #region Passive
            LanguageAPI.Add(prefix + "ALPHACONSTRUCT_NAME", "Struggle");
            LanguageAPI.Add(prefix + "ALPHACONSTRUCT_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "BEETLE_NAME", "Struggle");
            LanguageAPI.Add(prefix + "BEETLE_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "GUP_NAME", "Struggle");
            LanguageAPI.Add(prefix + "GUP_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "LARVA_NAME", "Struggle");
            LanguageAPI.Add(prefix + "LARVA_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "LESSERWISP_NAME", "Struggle");
            LanguageAPI.Add(prefix + "LESSERWISP_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "LUNAREXPLODER_NAME", "Struggle");
            LanguageAPI.Add(prefix + "LUNAREXPLODER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "HERMITCRAB_NAME", "Struggle");
            LanguageAPI.Add(prefix + "HERMITCRAB_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "PEST_NAME", "Struggle");
            LanguageAPI.Add(prefix + "PEST_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "VERMIN_NAME", "Struggle");
            LanguageAPI.Add(prefix + "VERMIN_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "MINIMUSHRUM_NAME", "Struggle");
            LanguageAPI.Add(prefix + "MINIMUSHRUM_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "ROBOBALLMINI_NAME", "Struggle");
            LanguageAPI.Add(prefix + "ROBOBALLMINI_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "VOIDBARNACLE_NAME", "Struggle");
            LanguageAPI.Add(prefix + "VOIDBARNACLE_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "VOIDJAILER_NAME", "Struggle");
            LanguageAPI.Add(prefix + "VOIDJAILER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "STONETITAN_NAME", "Struggle");
            LanguageAPI.Add(prefix + "STONETITAN_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "MAGMAWORM_NAME", "Struggle");
            LanguageAPI.Add(prefix + "MAGMAWORM_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "OVERLOADINGWORM_NAME", "Struggle");
            LanguageAPI.Add(prefix + "OVERLOADINGWORM_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "VAGRANT_NAME", "Struggle");
            LanguageAPI.Add(prefix + "VAGRANT_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            #endregion

            #region Active 
            LanguageAPI.Add(prefix + "VULTURE_NAME", "Struggle");
            LanguageAPI.Add(prefix + "VULTURE_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "BEETLEGUARD_NAME", "Struggle");
            LanguageAPI.Add(prefix + "BEETLEGUARD_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "BISON_NAME", "Struggle");
            LanguageAPI.Add(prefix + "BISON_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "BRONZONG_NAME", "Struggle");
            LanguageAPI.Add(prefix + "BRONZONG_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "APOTHECARY_NAME", "Struggle");
            LanguageAPI.Add(prefix + "APOTHECARY_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "TEMPLAR_NAME", "Struggle");
            LanguageAPI.Add(prefix + "TEMPLAR_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "GREATERWISP_NAME", "Struggle");
            LanguageAPI.Add(prefix + "GREATERWISP_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "IMP_NAME", "Struggle");
            LanguageAPI.Add(prefix + "IMP_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "JELLYFISH_NAME", "Struggle");
            LanguageAPI.Add(prefix + "JELLYFISH_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "LEMURIAN_NAME", "Struggle");
            LanguageAPI.Add(prefix + "LEMURIAN_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "LUNARGOLEM_NAME", "Struggle");
            LanguageAPI.Add(prefix + "LUNARGOLEM_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "LUNARWISP_NAME", "Struggle");
            LanguageAPI.Add(prefix + "LUNARWISP_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "PARENT_NAME", "Struggle");
            LanguageAPI.Add(prefix + "PARENT_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "STONEGOLEM_NAME", "Struggle");
            LanguageAPI.Add(prefix + "STONEGOLEM_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "VOIDJAILER_NAME", "Struggle");
            LanguageAPI.Add(prefix + "VOIDJAILER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "BEETLEQUEEN_NAME", "Struggle");
            LanguageAPI.Add(prefix + "BEETLEQUEEN_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "GROVETENDER_NAME", "Struggle");
            LanguageAPI.Add(prefix + "GROVETENDER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "CLAYDUNESTRIDER_NAME", "Struggle");
            LanguageAPI.Add(prefix + "CLAYDUNESTRIDER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "SOLUSCONTROLUNIT_NAME", "Struggle");
            LanguageAPI.Add(prefix + "SOLUSCONTROLUNIT_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "XICONSTRUCT_NAME", "Struggle");
            LanguageAPI.Add(prefix + "XICONSTRUCT_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "VOIDDEVASTATOR_NAME", "Struggle");
            LanguageAPI.Add(prefix + "VOIDDEVASTATOR_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            LanguageAPI.Add(prefix + "SCAVENGER_NAME", "Struggle");
            LanguageAPI.Add(prefix + "SCAVENGER_DESCRIPTION", $"<style=cIsDamage>Agile.</style> Hit enemies in front of you for <style=cIsDamage>{100f * StaticValues.decayattackDamageCoeffecient}% damage</style>.");
            #endregion

            #region Achievements
            LanguageAPI.Add("ACHIEVEMENT_" + prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_NAME", "Shiggy: Mastery");
            LanguageAPI.Add("ACHIEVEMENT_" + prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_DESCRIPTION", "As Shiggy, beat the game or obliterate on Monsoon.");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_UNLOCKABLE_NAME", "Shiggy: Mastery");
            #endregion


            #region Achievements
            LanguageAPI.Add(prefix + "KEYWORD_DECAY", $"<style=cArtifact>[ Decay ]</style> deal <style=cIsDamage>100% of your base damage</style> per second per stack. Each <style=cStack>stack reduces movespeed and attackspeed by 3%</style>. <style=cDeath>Instakills</style> at 100 stacks.");
            #endregion
            #endregion


        }
    }
}
