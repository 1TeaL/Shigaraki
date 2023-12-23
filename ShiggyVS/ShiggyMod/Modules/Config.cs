using BepInEx.Configuration;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using RiskOfOptions;
using UnityEngine;
using System;

namespace ShiggyMod.Modules
{
    public static class Config
    {
        public static ConfigEntry<bool> retainLoadout;
        public static ConfigEntry<bool> allowAllSkills;
        public static ConfigEntry<bool> allowVoice;
        public static ConfigEntry<float> holdButtonAFO;
        public static ConfigEntry<KeyboardShortcut> AFOHotkey { get; set; }
        public static ConfigEntry<KeyboardShortcut> RemoveHotkey { get; set; }
        public static ConfigEntry<KeyboardShortcut> AFOGiveHotkey { get; set; }

        //public static ConfigEntry<float> glideSpeed;
        //public static ConfigEntry<float> glideAcceleration;

        public static void ReadConfig()
        {
            retainLoadout = ShiggyPlugin.instance.Config.Bind("General", "Retain loadout across stages", true, "Should you retain your stolen quirks across stages and respawns.");
            holdButtonAFO = ShiggyPlugin.instance.Config.Bind("General", "Steal, Give and Remove quirk timer", 0f, "Set how long you want to hold the button.");
            allowVoice = ShiggyPlugin.instance.Config.Bind("General", "Allow voice", true, "Allow voice lines of Shigaraki.");

            AFOHotkey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "AFO Key", new KeyboardShortcut(UnityEngine.KeyCode.F), "Keybinding for AFO");
            RemoveHotkey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "Remove Quirk Key", new KeyboardShortcut(UnityEngine.KeyCode.V), "Keybinding for Remove Quirk");
            AFOGiveHotkey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "Give Quirk Key", new KeyboardShortcut(UnityEngine.KeyCode.C), "Keybinding for Give Quirk");

            allowAllSkills = ShiggyPlugin.instance.Config.Bind("General", "Allow all skils to be picked", false, "Should you be allowed to pick all skills in the loadout menu. AFO functionality is not disabled. Will require a Restart.");

            //Gliding
            //glideSpeed = ShiggyPlugin.instance.Config.Bind<float>
            //(
            //    new ConfigDefinition("Gliding", "Falling Speed when gliding"),
            //    60f,
            //    new ConfigDescription("Determines the base speed of descent when gliding.",
            //        null,
            //        Array.Empty<object>()
            //    )
            //);
            //glideAcceleration = ShiggyPlugin.instance.Config.Bind<float>
            //(
            //    new ConfigDefinition("Gliding", "Falling acceleration when gliding"),
            //    29.6f,
            //    new ConfigDescription("Determines the falling acceleration when gliding.",
            //        null,
            //        Array.Empty<object>()
            //    )
            //);
        }

        // this helper automatically makes config entries for disabling survivors
        internal static ConfigEntry<bool> CharacterEnableConfig(string characterName)
        {
            return ShiggyPlugin.instance.Config.Bind<bool>(new ConfigDefinition(characterName, "Enabled"), true, new ConfigDescription("Set to false to disable this character"));
        }

        internal static ConfigEntry<bool> EnemyEnableConfig(string characterName)
        {
            return ShiggyPlugin.instance.Config.Bind<bool>(new ConfigDefinition(characterName, "Enabled"), true, new ConfigDescription("Set to false to disable this enemy"));
        }

        public static void SetupRiskOfOptions()
        {
            //Risk of Options intialization
            ModSettingsManager.AddOption(new KeyBindOption(
                AFOHotkey));
            ModSettingsManager.AddOption(new KeyBindOption(
                RemoveHotkey));
            ModSettingsManager.AddOption(new KeyBindOption(
                AFOGiveHotkey));
            ModSettingsManager.AddOption(new CheckBoxOption(
                retainLoadout));
            ModSettingsManager.AddOption(new CheckBoxOption(
                allowAllSkills));
            ModSettingsManager.AddOption(new CheckBoxOption(
                allowVoice));
            ModSettingsManager.AddOption(new StepSliderOption(
                holdButtonAFO, new StepSliderConfig() { min = 0, max = 10, increment = 1f }));
            ModSettingsManager.SetModDescription("Shigaraki Mod");
            Sprite icon = Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("Shiggy");
            ModSettingsManager.SetModIcon(icon);

            //ModSettingsManager.AddOption(
            //    new StepSliderOption(
            //        glideSpeed,
            //        new StepSliderConfig
            //        {
            //            min = 0,
            //            max = 100f,
            //            increment = 0.05f
            //        }
            //    ));

            //ModSettingsManager.AddOption(
            //    new StepSliderOption(
            //        glideAcceleration,
            //        new StepSliderConfig
            //        {
            //            min = 0f,
            //            max = 100f,
            //            increment = 0.05f
            //        }
            //    ));

        }
    }
}
