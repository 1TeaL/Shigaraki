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
        public static ConfigEntry<bool> allowRangedAFO;
        public static ConfigEntry<float> maxAFORange;   // determine max afo range, up to 70 which is the range of the indicator
        public static ConfigEntry<float> holdButtonAFO;
        public static ConfigEntry<KeyboardShortcut> AFOHotkey;
        //public static ConfigEntry<KeyboardShortcut> RemoveHotkey;
        public static ConfigEntry<KeyboardShortcut> AFOGiveHotkey;
        public static ConfigEntry<KeyboardShortcut> OpenQuirkMenuHotkey;
        public static ConfigEntry<KeyboardShortcut> CloseQuirkMenuHotkey;
        public static ConfigEntry<bool> ShowQuirkNameOverlay;
        public static ConfigEntry<bool> ShowOwnedCheckOverlay;
        public static ConfigEntry<bool> StartWithAllQuirks;
        //public static ConfigEntry<float> glideSpeed;
        //public static ConfigEntry<float> glideAcceleration;
        public static ConfigEntry<KeyboardShortcut> AirWalkDescentKey;

        public static ConfigEntry<int> ApexStacksPerSecondReset;       // how many debuff stacks per 1s of cooldown reset
        public static ConfigEntry<int> ApexAdaptPerSecondReset;        // how many adaptation stacks per 1s reset
        public static ConfigEntry<float> ApexHPDrainPerStackPerSecond;   // flat negative regen HP drained
        public static ConfigEntry<float> ApexDamagePenaltyPerStack;      // flat damage multiplier penalty per stack (0.01 = -1% per stack)
        public static ConfigEntry<int> ApexOverdriveBaseCap;           // base stack cap before overdrive
        public static ConfigEntry<int> ApexOverdriveCapPerAdapt;    // how much the cap increases per adaptation
        public static ConfigEntry<int> ApexAdaptThreshold;        // how many stacks for each adaptation threshold
        public static ConfigEntry<float> ApexAdaptReward;             // how much flat stat upgrade do you gain for each threshold hit
        public static ConfigEntry<float> ApexOverdriveChunk;             // % MaxHP chunk on overdrive (0.4 = 40%)
        public static ConfigEntry<int> ApexHealBlockDuration;          // seconds heal-block lasts
        public static ConfigEntry<bool> ApexScaleByRemainingOnly;       // if true, use remaining cooldown; else use full final cooldown
        public static ConfigEntry<bool> ApexShowAdaptationOverlay;       // show the number of adaptation stacks and how many thresholds have happened


        public static void ReadConfig()
        {
            retainLoadout = ShiggyPlugin.instance.Config.Bind("General", "Retain loadout across stages", true, "Should you retain your stolen quirks across stages and respawns.");
            holdButtonAFO = ShiggyPlugin.instance.Config.Bind("General", "Steal, Give and Remove quirk timer", 0f, "Set how long you want to hold the button.");
            allowVoice = ShiggyPlugin.instance.Config.Bind("General", "Allow voice", true, "Allow voice lines of Shigaraki.");
            StartWithAllQuirks = ShiggyPlugin.instance.Config.Bind("General", "StartWithAllQuirks", false,
                "Begin runs with all quirks unlocked.");



            AFOHotkey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "AFO Key", new KeyboardShortcut(UnityEngine.KeyCode.F), "Keybinding for AFO");
            //RemoveHotkey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "Remove Quirk Key", new KeyboardShortcut(UnityEngine.KeyCode.V), "Keybinding for Remove Quirk");
            AFOGiveHotkey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "Give Quirk Key", new KeyboardShortcut(UnityEngine.KeyCode.C), "Keybinding for Give Quirk");
            OpenQuirkMenuHotkey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "Quirk UI Key", new KeyboardShortcut(UnityEngine.KeyCode.V), "Keybinding for Opening the Quirk UI");
            CloseQuirkMenuHotkey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "Quirk UI Close Key", new KeyboardShortcut(UnityEngine.KeyCode.Tilde), "Keybinding for Closing the Quirk UI");
            AirWalkDescentKey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "Air Walk Descent Key", new KeyboardShortcut(UnityEngine.KeyCode.X), "Keybinding for descending while in Air Walk");

            allowAllSkills = ShiggyPlugin.instance.Config.Bind("AFO", "Allow all skils to be picked", false, "Should you be allowed to pick all skills in the loadout menu. AFO functionality is not disabled. Will require a Restart.");
            allowRangedAFO = ShiggyPlugin.instance.Config.Bind("AFO", "Allow Ranged AFO", false, "Should you be allowed to use AFO from max distance.");
            maxAFORange = ShiggyPlugin.instance.Config.Bind("AFO", "Max Range for AFO", 5f, "Maximum range for AFO, up to 70.");
            ShowQuirkNameOverlay = ShiggyPlugin.instance.Config.Bind("UI/Indicators", "Show Quirk Name Overlay", true, "Enable a label with the quirk's name if the target has one.");
            ShowOwnedCheckOverlay = ShiggyPlugin.instance.Config.Bind("UI/Indicators", "Show Owned Check Overlay", true, "Show a ✓ when you’ve already stolen that quirk.");
            ApexShowAdaptationOverlay = ShiggyPlugin.instance.Config.Bind("UI/Indicators", "Show Adaptation Stacks Overlay", true, "Show how many stacks of adaptation and how many thresholds have been passed.");
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

            ApexStacksPerSecondReset = ShiggyPlugin.instance.Config.Bind("Apex", "Stacks Per Second Reset", 1, "Debuff stacks added per second of cooldown reset.");
            ApexAdaptPerSecondReset = ShiggyPlugin.instance.Config.Bind("Apex", "Adapt Per Second Reset", 1, "Adaptation stacks gained per second of cooldown reset.");
            ApexHPDrainPerStackPerSecond = ShiggyPlugin.instance.Config.Bind("Apex", "HP Drain Per Stack Per Second", 0.5f, "Negative HP Regen per Apex Surgery Debuff stack.");
            ApexDamagePenaltyPerStack = ShiggyPlugin.instance.Config.Bind("Apex", "Damage Penalty Per Stack", 0.01f, "Outgoing damage penalty per stack (0.01 = 1%).");
            ApexOverdriveBaseCap = ShiggyPlugin.instance.Config.Bind("Apex", "Overdrive Base Cap", 20, "Base debuff stack cap before overdrive triggers.");
            ApexOverdriveCapPerAdapt = ShiggyPlugin.instance.Config.Bind("Apex", "Over drive Cap Per 100 Adapt", 10, "Cap increases by this many stacks per Adaptation threshold.");
            ApexAdaptThreshold = ShiggyPlugin.instance.Config.Bind("Apex", "Adaptation threshold stacks", 100, "How many stacks of Adaptation needed to hit the next threshold.");
            ApexAdaptReward = ShiggyPlugin.instance.Config.Bind("Apex", "Adaptation Reward Value", 0.1f, "Percent of stat boosts gained per threshold met.");
            ApexOverdriveChunk = ShiggyPlugin.instance.Config.Bind("Apex", "OverdriveChunk", 0.40f, "Percent Max HP damage when overdrive triggers.");
            ApexHealBlockDuration = ShiggyPlugin.instance.Config.Bind("Apex", "HealBlockDuration", 5, "Seconds of heal-block during overdrive.");
            ApexScaleByRemainingOnly = ShiggyPlugin.instance.Config.Bind("Apex", "ScaleByRemainingOnly", true, "When true, stacks scale with remaining cooldown rather than full cooldown.");

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
                AFOGiveHotkey));
            ModSettingsManager.AddOption(new KeyBindOption(
                OpenQuirkMenuHotkey));
            ModSettingsManager.AddOption(new KeyBindOption(
                CloseQuirkMenuHotkey));
            ModSettingsManager.AddOption(new KeyBindOption(
                AirWalkDescentKey));
            ModSettingsManager.AddOption(new CheckBoxOption(
                ShowQuirkNameOverlay));
            ModSettingsManager.AddOption(new CheckBoxOption(
                ShowOwnedCheckOverlay));
            ModSettingsManager.AddOption(new CheckBoxOption(
                StartWithAllQuirks));
            ModSettingsManager.AddOption(new CheckBoxOption(
                retainLoadout));
            ModSettingsManager.AddOption(new CheckBoxOption(
                allowAllSkills));
            ModSettingsManager.AddOption(new CheckBoxOption(
                allowVoice));
            ModSettingsManager.AddOption(new StepSliderOption(
                holdButtonAFO, new StepSliderConfig() { min = 0, max = 10, increment = 1f }));


            ModSettingsManager.AddOption(new CheckBoxOption(
                allowRangedAFO));
            ModSettingsManager.AddOption(new StepSliderOption(
                maxAFORange, new StepSliderConfig() { min = 1f, max = 70f, increment = 1f }));

            ModSettingsManager.AddOption(new IntSliderOption(
                ApexStacksPerSecondReset, new IntSliderConfig() { min = 1, max = 10 }));
            ModSettingsManager.AddOption(new IntSliderOption(
                ApexAdaptPerSecondReset, new IntSliderConfig() { min = 1, max = 100 }));
            ModSettingsManager.AddOption(new StepSliderOption(
                ApexHPDrainPerStackPerSecond, new StepSliderConfig() { min = 0, max = 1, increment = 0.001f }));
            ModSettingsManager.AddOption(new StepSliderOption(
                ApexDamagePenaltyPerStack, new StepSliderConfig() { min = 0, max = 1, increment = 0.01f }));
            ModSettingsManager.AddOption(new IntSliderOption(
                ApexOverdriveBaseCap, new IntSliderConfig() { min = 1, max = 1000 }));
            ModSettingsManager.AddOption(new IntSliderOption(
                ApexOverdriveCapPerAdapt, new IntSliderConfig() { min = 1, max = 1000 }));
            ModSettingsManager.AddOption(new IntSliderOption(
                ApexAdaptThreshold, new IntSliderConfig() { min = 1, max = 1000 }));
            ModSettingsManager.AddOption(new StepSliderOption(
                ApexOverdriveChunk, new StepSliderConfig() { min = 0, max = 1, increment = 0.01f }));
            ModSettingsManager.AddOption(new IntSliderOption(
                ApexHealBlockDuration, new IntSliderConfig() { min = 1, max = 100 }));
            ModSettingsManager.AddOption(new CheckBoxOption(
                ApexScaleByRemainingOnly));
            ModSettingsManager.AddOption(new CheckBoxOption(
                ApexShowAdaptationOverlay));

            ModSettingsManager.SetModDescription("Shigaraki Mod");
            Sprite icon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("Shiggy");
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
