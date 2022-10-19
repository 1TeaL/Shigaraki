using BepInEx.Configuration;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using RiskOfOptions;

namespace ShiggyMod.Modules
{
    public static class Config
    {
        public static ConfigEntry<bool> retainLoadout;
        public static ConfigEntry<bool> holdButtonAFO;
        public static ConfigEntry<KeyboardShortcut> AFOHotkey { get; set; }
        public static ConfigEntry<KeyboardShortcut> RemoveHotkey { get; set; }

        public static void ReadConfig()
        {
            retainLoadout = ShiggyPlugin.instance.Config.Bind("General", "Retain loadout across stages", true, "Should you retain your stolen quirks across stages and respawns.");
            holdButtonAFO = ShiggyPlugin.instance.Config.Bind("General", "Steal and Remove quirks instantly", true, "Set to false to hold the button for 1 second to steal and/or remove.");

            AFOHotkey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "AFO Key", new KeyboardShortcut(UnityEngine.KeyCode.K), "Keybinding for AFO");
            RemoveHotkey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "Remove Quirk Key", new KeyboardShortcut(UnityEngine.KeyCode.V), "Keybinding for Remove Quirk");
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
        }
    }
}
