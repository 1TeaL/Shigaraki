using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using UnityEngine;

namespace ShiggyMod.Modules
{
    public static class Config
    {
        public static ConfigEntry<bool> retainLoadout;
        public static ConfigEntry<bool> allowAllSkills;
        public static ConfigEntry<bool> allowVoice;
        public static ConfigEntry<bool> allowRangedAFO;
        public static ConfigEntry<bool> allowEyeGlow;
        public static ConfigEntry<float> maxAFORange;   // determine max afo range, up to 70 which is the range of the indicator
        public static ConfigEntry<float> holdButtonAFO;
        public static ConfigEntry<KeyboardShortcut> AFOHotkey;
        //public static ConfigEntry<KeyboardShortcut> RemoveHotkey;
        public static ConfigEntry<KeyboardShortcut> AFOGiveHotkey;
        public static ConfigEntry<KeyboardShortcut> OpenQuirkMenuHotkey;
        public static ConfigEntry<KeyboardShortcut> ConfirmQuirkMenuHotkey;
        public static ConfigEntry<KeyboardShortcut> CloseQuirkMenuHotkey;
        public static ConfigEntry<bool> ShowQuirkNameOverlay;
        public static ConfigEntry<bool> ShowOwnedCheckOverlay;
        public static ConfigEntry<bool> StartWithAllQuirks;
        //public static ConfigEntry<float> AirWalkSpeedMultiplier;
        //public static ConfigEntry<float> glideAcceleration
        public static ConfigEntry<KeyboardShortcut> AirWalkDescentKey;
        public static ConfigEntry<KeyboardShortcut> LayDownKey;

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
        public static ConfigEntry<bool> ApexShowAdaptationOverlay;       // show the number of adaptation stacks and how many thresholds have happened
        public static ConfigEntry<float> ApexHoldSecondsToReset;             // seconds to hold to reset cooldowns

        public static ConfigEntry<float> HyperRegenerationInterval;
        public static ConfigEntry<float> HyperRegenerationHealPercent;

        public static ConfigEntry<float> DecayInterval;
        public static ConfigEntry<float> DecayDuration;
        public static ConfigEntry<int> DecayStacks;
        public static ConfigEntry<float> DecayBaseDamage;
        public static ConfigEntry<float> DecayDamagePercentage;
        public static ConfigEntry<float> DecaySpreadRadius;

        public static ConfigEntry<float> GargoyleProtectionDamageReductionPercent;

        public static ConfigEntry<float> DoubleTimeSlowMultiplier;
        public static ConfigEntry<float> DoubleTimeStatMultiplier;
        public static ConfigEntry<float> DoubleTimeDuration;
        public static ConfigEntry<float> DoubleTimeRadius;

        public static ConfigEntry<float> AirCannonDamage;
        public static ConfigEntry<float> AirCannonSpeed;
        public static ConfigEntry<float> AirCannonRange;
        public static ConfigEntry<float> AirCannonCooldown;

        public static ConfigEntry<float> BulletLaserDamage;
        public static ConfigEntry<float> BulletLaserRange;
        public static ConfigEntry<float> BulletLaserCooldown;
        public static ConfigEntry<float> BulletLaserSize;

        public static ConfigEntry<float> StolenInheritanceHPCoefficient;

        public static ConfigEntry<float> CryoChargedRailgunDamage;

        public static ConfigEntry<float> StoneSkinArmorGain;

        public static ConfigEntry<float> DoubleTapProc;
        public static ConfigEntry<float> DoubleTapDamage;

        public static ConfigEntry<float> ScrapBarrierBarrierGain;

        public static ConfigEntry<float> LimitBreakDamageMultiplier;
        public static ConfigEntry<float> LimitBreakHealthCostPercentage;

        public static ConfigEntry<float> OFAMultiplierAdditive;
        public static ConfigEntry<float> OFAHealthCostPercentage;

        public static ConfigEntry<float> StrengthBoostBaseDamage;

        public static ConfigEntry<float> HasteAttackSpeedAdditive;

        public static ConfigEntry<float> LifeForceHealthMultiplier;

        public static ConfigEntry<float> OverclockAscensionMovespeedMultiplier;
        public static ConfigEntry<float> OverclockAscensionSprintspeedMultiplier;
        public static ConfigEntry<float> OverclockAscensionAttackspeedMultiplier;
        public static ConfigEntry<float> OverclockAscensionDodgeChance;
        public static ConfigEntry<float> OverclockAscensionHealAmount;
        public static ConfigEntry<float> OverclockAscensionHealthRegenCost;
        public static ConfigEntry<float> OverclockAscensionHealthRegenCostIncrease;
        public static ConfigEntry<float> OverclockAscensionSlowMultiplier;
        public static ConfigEntry<float> OverclockAscensionRadius;

        public static ConfigEntry<float> DecayPlusChaosDamage;
        public static ConfigEntry<float> DecayPlusChaosRange;
        public static ConfigEntry<float> DecayPlusChaosHealthCost;


        public static void ReadConfig()
        {
            //General configs
            retainLoadout = ShiggyPlugin.instance.Config.Bind("General", "Retain loadout across stages", true, "Should you retain your stolen quirks across stages and respawns.");
            holdButtonAFO = ShiggyPlugin.instance.Config.Bind("General", "Steal, Give and Remove quirk timer", 0f, "Set how long you want to hold the button.");
            allowVoice = ShiggyPlugin.instance.Config.Bind("General", "Allow voice", true, "Allow voice lines of Shigaraki.");
            StartWithAllQuirks = ShiggyPlugin.instance.Config.Bind("General", "Start With All Quirks", false,
                "Begin runs with all quirks unlocked.");
            allowAllSkills = ShiggyPlugin.instance.Config.Bind("General", "Allow all skils to be picked in the loadout", false, "Should you be allowed to pick all skills in the loadout menu. AFO functionality is not disabled. Will require a Restart.");
            allowEyeGlow = ShiggyPlugin.instance.Config.Bind("General", "Allow eye glow effect", true, "Should you have the eye glow effects based on how much plus chaos you own.");
            

            //Input configs
            AFOHotkey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "AFO Key", new KeyboardShortcut(UnityEngine.KeyCode.F), "Keybinding for AFO");
            //RemoveHotkey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "Remove Quirk Key", new KeyboardShortcut(UnityEngine.KeyCode.V), "Keybinding for Remove Quirk");
            AFOGiveHotkey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "Give Quirk Key", new KeyboardShortcut(UnityEngine.KeyCode.C), "Keybinding for Give Quirk");
            OpenQuirkMenuHotkey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "Quirk UI Key", new KeyboardShortcut(UnityEngine.KeyCode.V), "Keybinding for Opening the Quirk UI");
            ConfirmQuirkMenuHotkey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "Quirk UI Confirm Key", new KeyboardShortcut(UnityEngine.KeyCode.Return), "Keybinding for Confirming the Quirk UI");
            CloseQuirkMenuHotkey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "Quirk UI Close Key", new KeyboardShortcut(UnityEngine.KeyCode.Tilde), "Keybinding for Closing the Quirk UI");
            AirWalkDescentKey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "Air Walk Descent Key", new KeyboardShortcut(UnityEngine.KeyCode.X), "Keybinding for descending while in Air Walk");
            LayDownKey = ShiggyPlugin.instance.Config.Bind<KeyboardShortcut>("Input", "Lay Down Key", new KeyboardShortcut(UnityEngine.KeyCode.K), "Keybinding for Lay Down Animation Emote");

            //AFO configs
            allowRangedAFO = ShiggyPlugin.instance.Config.Bind("AFO", "Allow Ranged AFO", false, "Should you be allowed to use AFO from max distance.");
            maxAFORange = ShiggyPlugin.instance.Config.Bind("AFO", "Max Range for AFO", 10f, "Maximum range for AFO, up to 70.");

            //UI configs
            ShowQuirkNameOverlay = ShiggyPlugin.instance.Config.Bind("UI/Indicators", "Show Quirk Name Overlay", true, "Enable a label with the quirk's name if the target has one.");
            ShowOwnedCheckOverlay = ShiggyPlugin.instance.Config.Bind("UI/Indicators", "Show Owned Check Overlay", true, "Show a ✓ when you’ve already stolen that quirk.");
            ApexShowAdaptationOverlay = ShiggyPlugin.instance.Config.Bind("UI/Indicators", "Show Adaptation Stacks Overlay", true, "Show how many stacks of adaptation and how many thresholds have been passed.");

            //Apex
            ApexStacksPerSecondReset = ShiggyPlugin.instance.Config.Bind("Apex", "Stacks Per Second Reset", 1, "Debuff stacks added per second of cooldown reset.");
            ApexAdaptPerSecondReset = ShiggyPlugin.instance.Config.Bind("Apex", "Adapt Per Second Reset", 1, "Adaptation stacks gained per second of cooldown reset.");
            ApexHPDrainPerStackPerSecond = ShiggyPlugin.instance.Config.Bind("Apex", "HP Drain Per Stack Per Second", 0.2f, "Negative HP Regen per Apex Surgery Debuff stack.");
            ApexDamagePenaltyPerStack = ShiggyPlugin.instance.Config.Bind("Apex", "Damage Penalty Per Stack", 0.01f, "Outgoing damage penalty per stack (0.01 = 1%).");
            ApexOverdriveBaseCap = ShiggyPlugin.instance.Config.Bind("Apex", "Overdrive Base Cap", 20, "Base debuff stack cap before overdrive triggers.");
            ApexOverdriveCapPerAdapt = ShiggyPlugin.instance.Config.Bind("Apex", "Over drive Cap Per 100 Adapt", 10, "Cap increases by this many stacks per Adaptation threshold.");
            ApexAdaptThreshold = ShiggyPlugin.instance.Config.Bind("Apex", "Adaptation threshold stacks", 100, "How many stacks of Adaptation needed to hit the next threshold.");
            ApexAdaptReward = ShiggyPlugin.instance.Config.Bind("Apex", "Adaptation Reward Value", 0.1f, "Percent of stat boosts gained per threshold met.");
            ApexOverdriveChunk = ShiggyPlugin.instance.Config.Bind("Apex", "Overdrive Damage", 0.40f, "Percent Max HP damage when overdrive triggers.");
            ApexHealBlockDuration = ShiggyPlugin.instance.Config.Bind("Apex", "Heal Block Duration", StaticValues.quirkOverdriveDuration, "Seconds of heal-block during overdrive.");
            ApexHoldSecondsToReset = ShiggyPlugin.instance.Config.Bind("Apex", "Hold Seconds To Reset", 0.30f, "Seconds to hold to reset cooldowns.");

            //Quirks Configs
            HyperRegenerationInterval = ShiggyPlugin.instance.Config.Bind("Quirks", "Hyper Regeneration Interval", StaticValues.hyperRegenInterval, "Seconds between each heal tick.");
            HyperRegenerationHealPercent = ShiggyPlugin.instance.Config.Bind("Quirks", "Hyper Regeneration Heal Percent", StaticValues.hyperRegenHealthCoefficient, "Percentage healing (out of 1 = 100%) of your Max HP that you are healed.");
            DecayInterval = ShiggyPlugin.instance.Config.Bind("Quirks", "Decay Spread Interval", StaticValues.decayadditionalTimer, "Seconds between each decay spread.");
            DecayDuration = ShiggyPlugin.instance.Config.Bind("Quirks", "Decay Duration", StaticValues.decayDamageTimer, "How many seconds decay lasts.");
            DecayBaseDamage = ShiggyPlugin.instance.Config.Bind("Quirks", "Decay Attacker Damage", StaticValues.decayDamageStack, "How much damage decay does based on player damage.");
            DecayDamagePercentage = ShiggyPlugin.instance.Config.Bind("Quirks", "Decay HP Damage Percentage", StaticValues.decayDamagePercentage, "How much damage decay does based on enemy max HP.");
            DecaySpreadRadius = ShiggyPlugin.instance.Config.Bind("Quirks", "Decay Spread Radius", StaticValues.decayspreadRadius, "How far decay can spread.");
            DecayStacks = ShiggyPlugin.instance.Config.Bind("Quirks", "Decay Instakill Threshold", StaticValues.decayInstaKillThreshold, "How many stacks to instakill an enemy.");

            GargoyleProtectionDamageReductionPercent = ShiggyPlugin.instance.Config.Bind("Quirks", "Gargoyle Protection Damage Reduction Percent", StaticValues.gargoyleProtectionDamageReductionCoefficient, "Damage Reduction for Gargoyle Protection.");

            DoubleTimeDuration = ShiggyPlugin.instance.Config.Bind("Quirks", "Double Time Duration", StaticValues.doubleTimeThreshold, "How many seconds till Double Time buff stacks are halved.");
            DoubleTimeRadius = ShiggyPlugin.instance.Config.Bind("Quirks", "Double Time Radius", StaticValues.doubleTimeRadius, "Radius of effect of Double Time.");
            DoubleTimeSlowMultiplier = ShiggyPlugin.instance.Config.Bind("Quirks", "Double Time Slow Multiplier", StaticValues.doubleTimeSlowCoefficient, "How slowed enemies are.");
            DoubleTimeStatMultiplier = ShiggyPlugin.instance.Config.Bind("Quirks", "Double Time Stat Multiplier", StaticValues.doubleTimeCoefficient, "How much damage, attackspeed and movespeed are gained on kill.");

            BulletLaserDamage = ShiggyPlugin.instance.Config.Bind("Quirks", "Bullet Laser Damage", StaticValues.bulletlaserDamageCoefficient, "How much damage each hit of Bullet Laser does.");
            BulletLaserCooldown = ShiggyPlugin.instance.Config.Bind("Quirks", "Bullet Laser Cooldown", StaticValues.bulletlaserCooldown, "What cooldown Bullet Laser has.");
            BulletLaserRange = ShiggyPlugin.instance.Config.Bind("Quirks", "Bullet Laser Range", StaticValues.bulletlaserRange, "How much range Bullet Laser has.");
            BulletLaserSize = ShiggyPlugin.instance.Config.Bind("Quirks", "Bullet Laser Size", StaticValues.bulletlaserSize, "How much size each laser Bullet Laser has.");

            AirCannonDamage = ShiggyPlugin.instance.Config.Bind("Quirks", "Air Cannon Damage", StaticValues.aircannonDamageCoefficient, "How much damage Air Cannon does.");
            AirCannonCooldown = ShiggyPlugin.instance.Config.Bind("Quirks", "Air Cannon Cooldown", StaticValues.aircannonCoodldown, "What cooldown Air Cannon has.");
            AirCannonRange = ShiggyPlugin.instance.Config.Bind("Quirks", "Air Cannon Range", StaticValues.aircannonRadius, "How much range the Air Cannon blast has.");
            AirCannonSpeed = ShiggyPlugin.instance.Config.Bind("Quirks", "Air Cannon Speed", StaticValues.aircannonSpeedCoefficient, "How much speed Air Cannon has.");

            StolenInheritanceHPCoefficient = ShiggyPlugin.instance.Config.Bind("Quirks", "Stolen Inheritance HP ratio", StaticValues.falseSonHPCoefficient, "How much percentage of max HP is converted to base damage.");

            CryoChargedRailgunDamage = ShiggyPlugin.instance.Config.Bind("Quirks", "CryoCharged Railgun Damage", StaticValues.railgunnerDamageCoefficient, "How much damage Cryo Charged Railgun does.");

            StoneSkinArmorGain = ShiggyPlugin.instance.Config.Bind("Quirks", "Stone Skin Armor Gain", StaticValues.stonetitanarmorGain, "How much armor gain Stone Skin provides.");

            DoubleTapDamage = ShiggyPlugin.instance.Config.Bind("Quirks", "Double Tap Damage", StaticValues.commandoDamageMultiplier, "How much damage the 2nd hit with Double Tap does compared to the initial hit.");
            DoubleTapProc = ShiggyPlugin.instance.Config.Bind("Quirks", "Double Tap Proc Chance", StaticValues.commandoProcCoefficient, "How much proc the 2nd hit with Double Tap does.");

            ScrapBarrierBarrierGain = ShiggyPlugin.instance.Config.Bind("Quirks", "Scrap Barrier Barrier Gain", StaticValues.loaderBarrierGainCoefficient, "How much percentage of max HP is gained as barrier on attacks.");

            LimitBreakDamageMultiplier = ShiggyPlugin.instance.Config.Bind("Quirks", "Limit Break Damage Multiplier", StaticValues.limitBreakCoefficient, "How much damage is multiplied with Limit Break.");
            LimitBreakHealthCostPercentage = ShiggyPlugin.instance.Config.Bind("Quirks", "Limit Break Health Cost Percentage", StaticValues.limitBreakHealthCostCoefficient, "How much percentage of max HP is spent per attack with Limit Break.");

            OFAMultiplierAdditive = ShiggyPlugin.instance.Config.Bind("Quirks", "One For All Multiplier", StaticValues.OFACoefficient, "How much percentage of stats are gained additively.");
            OFAHealthCostPercentage = ShiggyPlugin.instance.Config.Bind("Quirks", "One For All Health Cost Percentage", StaticValues.OFAHealthCostCoefficient, "How much percentage of current HP is spent every second.");

            StrengthBoostBaseDamage = ShiggyPlugin.instance.Config.Bind("Quirks", "Strength Boost Base Damage", StaticValues.beetleFlatDamage, "How much base damage gain Strength Boost provides.");

            HasteAttackSpeedAdditive = ShiggyPlugin.instance.Config.Bind("Quirks", "Haste Attack Speed", StaticValues.lesserwispFlatAttackSpeed, "How much attack speed gain Haste provides.");

            LifeForceHealthMultiplier = ShiggyPlugin.instance.Config.Bind("Quirks", "Life Force Health Multiplier", StaticValues.lifeForceMultiplier, "How much health is multiplied by with Life Force.");

            OverclockAscensionAttackspeedMultiplier = ShiggyPlugin.instance.Config.Bind("Quirks", "Overclock Ascension Attackspeed Multiplier", StaticValues.overclockAscensionAttackspeed, "How much attackspeed is increased additively with Overclock Ascension.");
            OverclockAscensionMovespeedMultiplier = ShiggyPlugin.instance.Config.Bind("Quirks", "Overclock Ascension Movespeed Multiplier", StaticValues.overclockAscensionMovespeed, "How much movespeed is increased additively with Overclock Ascension.");
            OverclockAscensionSprintspeedMultiplier = ShiggyPlugin.instance.Config.Bind("Quirks", "Overclock Ascension Sprint speed Multiplier", StaticValues.overclockAscensionSprintspeed, "How much sprint speed is increased additively with Overclock Ascension.");
            OverclockAscensionDodgeChance = ShiggyPlugin.instance.Config.Bind("Quirks", "Overclock Ascension Dodge Chance", StaticValues.overclockAscensionDodgeChance, "How much dodge chance with Overclock Ascension.");
            OverclockAscensionHealAmount = ShiggyPlugin.instance.Config.Bind("Quirks", "Overclock Ascension Heal Amount", StaticValues.overclockAscensionHeal, "How much percentage of max HP is healed every second with Overclock Ascension.");
            OverclockAscensionHealthRegenCost = ShiggyPlugin.instance.Config.Bind("Quirks", "Overclock Ascension Health Regen Cost", StaticValues.overclockAscensionHealthRegenCost, "How much negative health regen is given with Overclock Ascension.");
            OverclockAscensionHealthRegenCostIncrease = ShiggyPlugin.instance.Config.Bind("Quirks", "Overclock Ascension Health Regen Cost Increase", StaticValues.overclockAscensionHealthRegenCost, "How much negative health regen increases every second with Overclock Ascension.");
            OverclockAscensionSlowMultiplier = ShiggyPlugin.instance.Config.Bind("Quirks", "Overclock Ascension Slow Multiplier", StaticValues.overclockAscensionSlowMultiplier, "How much enemy movespeed and attackspeed are slowed by with Overclock Ascension.");
            OverclockAscensionRadius = ShiggyPlugin.instance.Config.Bind("Quirks", "Overclock Ascension Radius", StaticValues.overclockAscensionRadius, "The radius with Overclock Ascension.");

            DecayPlusChaosDamage = ShiggyPlugin.instance.Config.Bind("Quirks", "Decay Plus Chaos Damage", StaticValues.decayPlusChaosDamageCoefficient, "How much damage Decay Plus Chaos does.");
            DecayPlusChaosHealthCost = ShiggyPlugin.instance.Config.Bind("Quirks", "Decay Plus Chaos Health Cost", StaticValues.decayPlusChaosHealthCostCoefficient, "How much percentage of max HP is spent when Decay Plus Chaos is used.");
            DecayPlusChaosRange = ShiggyPlugin.instance.Config.Bind("Quirks", "Decay Plus Chaos Range", StaticValues.decayPlusChaosRadius, "How much range Decay Plus Chaos has.");

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
                ConfirmQuirkMenuHotkey));
            ModSettingsManager.AddOption(new KeyBindOption(
                CloseQuirkMenuHotkey));
            ModSettingsManager.AddOption(new KeyBindOption(
                AirWalkDescentKey));
            ModSettingsManager.AddOption(new KeyBindOption(
                LayDownKey));
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
            ModSettingsManager.AddOption(new CheckBoxOption(
                allowEyeGlow));
            ModSettingsManager.AddOption(new StepSliderOption(
                holdButtonAFO, new StepSliderConfig() { min = 0, max = 10, increment = 1f }));


            ModSettingsManager.AddOption(new CheckBoxOption(
                allowRangedAFO));
            ModSettingsManager.AddOption(new StepSliderOption(
                maxAFORange, new StepSliderConfig() { min = 1f, max = StaticValues.maxTrackingDistance, increment = 1f }));

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
                ApexShowAdaptationOverlay));
            ModSettingsManager.AddOption(new StepSliderOption(
                ApexHoldSecondsToReset, new StepSliderConfig() { min = 0.01f, max = 10f, increment = 0.01f }));
            ModSettingsManager.AddOption(new StepSliderOption(
                ApexAdaptReward, new StepSliderConfig() { min = 0.1f, max = 10f, increment = 0.1f }));

            ModSettingsManager.AddOption(new StepSliderOption(
                HyperRegenerationInterval, new StepSliderConfig() { min = 0.1f, max = 5f, increment = 0.1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
                HyperRegenerationHealPercent, new StepSliderConfig() { min = 0.01f, max = 1f, increment = 0.01f }));

            ModSettingsManager.AddOption(new StepSliderOption(
                DecayInterval, new StepSliderConfig() { min = 0.1f, max = 100f, increment = 0.1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
                DecayDuration, new StepSliderConfig() { min = 1f, max = 100f, increment = 1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
                DecayBaseDamage, new StepSliderConfig() { min = 0.1f, max = 100f, increment = 0.1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
                DecaySpreadRadius, new StepSliderConfig() { min = 1f, max = 100f, increment = 1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
                DecayDamagePercentage, new StepSliderConfig() { min = 0.01f, max = 1f, increment = 0.01f }));
            ModSettingsManager.AddOption(new IntSliderOption(
                DecayStacks, new IntSliderConfig() { min = 1, max = 1000 }));

            ModSettingsManager.AddOption(new StepSliderOption(
               GargoyleProtectionDamageReductionPercent, new StepSliderConfig() { min = 0.01f, max = 1f, increment = 0.01f }));

            ModSettingsManager.AddOption(new StepSliderOption(
               DoubleTimeRadius, new StepSliderConfig() { min = 1f, max = 100f, increment = 1f}));
            ModSettingsManager.AddOption(new StepSliderOption(
               DoubleTimeDuration, new StepSliderConfig() { min = 1f, max = 100, increment = 1f}));
            ModSettingsManager.AddOption(new StepSliderOption(
               DoubleTimeSlowMultiplier, new StepSliderConfig() { min = 0.01f, max = 1f, increment = 0.01f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               DoubleTimeStatMultiplier, new StepSliderConfig() { min = 0.1f, max = 10f, increment = 0.1f }));

            ModSettingsManager.AddOption(new StepSliderOption(
               BulletLaserDamage, new StepSliderConfig() { min = 0.1f, max = 100f, increment = 0.1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               BulletLaserCooldown, new StepSliderConfig() { min = 1f, max = 100f, increment = 1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               BulletLaserRange, new StepSliderConfig() { min = 10f, max = 500f, increment = 1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               BulletLaserSize, new StepSliderConfig() { min = 0.1f, max = 10f, increment = 0.1f }));

            ModSettingsManager.AddOption(new StepSliderOption(
               StolenInheritanceHPCoefficient, new StepSliderConfig() { min = 0.01f, max = 1f, increment = 0.01f }));

            ModSettingsManager.AddOption(new StepSliderOption(
               CryoChargedRailgunDamage, new StepSliderConfig() { min = 0.1f, max = 1000f, increment = 0.1f }));

            ModSettingsManager.AddOption(new StepSliderOption(
               StoneSkinArmorGain, new StepSliderConfig() { min = 1f, max = 1000f, increment = 1f }));

            ModSettingsManager.AddOption(new StepSliderOption(
               DoubleTapDamage, new StepSliderConfig() { min = 0.01f, max = 1f, increment = 0.01f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               DoubleTapProc, new StepSliderConfig() { min = 0.1f, max = 10f, increment = 0.1f }));

            ModSettingsManager.AddOption(new StepSliderOption(
               ScrapBarrierBarrierGain, new StepSliderConfig() { min = 0.01f, max = 1f, increment = 0.01f }));

            ModSettingsManager.AddOption(new StepSliderOption(
               LimitBreakDamageMultiplier, new StepSliderConfig() { min = 1f, max = 100f, increment = 0.1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               LimitBreakHealthCostPercentage, new StepSliderConfig() { min = 0.01f, max = 1f, increment = 0.01f }));

            ModSettingsManager.AddOption(new StepSliderOption(
               OFAMultiplierAdditive, new StepSliderConfig() { min = 0.1f, max = 100f, increment = 0.1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               OFAHealthCostPercentage, new StepSliderConfig() { min = 0.01f, max = 1f, increment = 0.01f }));

            ModSettingsManager.AddOption(new StepSliderOption(
               StrengthBoostBaseDamage, new StepSliderConfig() { min = 1f, max = 100f, increment = 0.1f }));

            ModSettingsManager.AddOption(new StepSliderOption(
               HasteAttackSpeedAdditive, new StepSliderConfig() { min = 0.1f, max = 10f, increment = 0.1f }));

            ModSettingsManager.AddOption(new StepSliderOption(
               LifeForceHealthMultiplier, new StepSliderConfig() { min = 0f, max = 10000f, increment = 1f }));

            ModSettingsManager.AddOption(new StepSliderOption(
               OverclockAscensionAttackspeedMultiplier, new StepSliderConfig() { min = 0f, max = 10f, increment = 0.1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               OverclockAscensionMovespeedMultiplier, new StepSliderConfig() { min = 0f, max = 10f, increment = 0.1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               OverclockAscensionSprintspeedMultiplier, new StepSliderConfig() { min = 0f, max = 10f, increment = 0.1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               OverclockAscensionHealAmount, new StepSliderConfig() { min = 0f, max = 10f, increment = 0.1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               OverclockAscensionHealthRegenCost, new StepSliderConfig() { min = 0f, max = 1000f, increment = 1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               OverclockAscensionHealthRegenCostIncrease, new StepSliderConfig() { min = 0f, max = 100f, increment = 1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               OverclockAscensionDodgeChance, new StepSliderConfig() { min = 0f, max = 1f, increment = 0.1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               OverclockAscensionSlowMultiplier, new StepSliderConfig() { min = 0f, max = 1f, increment = 0.1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               OverclockAscensionRadius, new StepSliderConfig() { min = 0f, max = 300f, increment = 1f }));
                        
            ModSettingsManager.AddOption(new StepSliderOption(
               AirCannonRange, new StepSliderConfig() { min = 1f, max = 100f, increment = 1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               AirCannonSpeed, new StepSliderConfig() { min = 1f, max = 500f, increment = 1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               AirCannonCooldown, new StepSliderConfig() { min = 1f, max = 100f, increment = 1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               AirCannonDamage, new StepSliderConfig() { min = 0.1f, max = 100f, increment = 0.1f }));

            ModSettingsManager.AddOption(new StepSliderOption(
               DecayPlusChaosDamage, new StepSliderConfig() { min = 0.1f, max = 1000f, increment = 0.1f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               DecayPlusChaosHealthCost, new StepSliderConfig() { min = 0f, max = 1f, increment = 0.01f }));
            ModSettingsManager.AddOption(new StepSliderOption(
               DecayPlusChaosRange, new StepSliderConfig() { min = 1f, max = 300f, increment = 1f }));


            ModSettingsManager.SetModDescription("Shigaraki Mod");
            Sprite icon = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("texShiggyIcon");
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
