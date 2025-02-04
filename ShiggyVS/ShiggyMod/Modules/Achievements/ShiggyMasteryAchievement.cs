﻿using RoR2;
using System;
using UnityEngine;

namespace ShiggyMod.Modules.Achievements
{
    [RegisterAchievement(ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_MASTERYUNLOCKABLE_ACHIEVEMENT",
        ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_MASTERYUNLOCKABLE_REWARD_ID", null, 10)]
    internal class MasteryAchievement : ModdedUnlockable
    {
        public override string AchievementIdentifier { get; } = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_MASTERYUNLOCKABLE_ACHIEVEMENT_ID";
        public override string UnlockableIdentifier { get; } = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_MASTERYUNLOCKABLE_REWARD_ID";
        public override string AchievementNameToken { get; } = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_MASTERYUNLOCKABLE_ACHIEVEMENT_NAME";
        public override string PrerequisiteUnlockableIdentifier { get; } = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_UNLOCKABLE_REWARD_ID";
        public override string UnlockableNameToken { get; } = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_MASTERYUNLOCKABLE_UNLOCKABLE_NAME";
        public override string AchievementDescToken { get; } = ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_MASTERYUNLOCKABLE_ACHIEVEMENT_DESC";
        public override Sprite Sprite { get; } = Modules.ShiggyAsset.mainAssetBundle.LoadAsset<Sprite>("allforone");

        public override Func<string> GetHowToUnlock { get; } = (() => Language.GetStringFormatted("UNLOCK_VIA_ACHIEVEMENT_FORMAT", new object[]
                            {
                                Language.GetString(ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_MASTERYUNLOCKABLE_ACHIEVEMENT_NAME"),
                                Language.GetString(ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_MASTERYUNLOCKABLE_ACHIEVEMENT_DESC")
                            }));
        public override Func<string> GetUnlocked { get; } = (() => Language.GetStringFormatted("UNLOCKED_FORMAT", new object[]
                            {
                                Language.GetString(ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_MASTERYUNLOCKABLE_ACHIEVEMENT_NAME"),
                                Language.GetString(ShiggyPlugin.developerPrefix + "_SHIGGY_BODY_MASTERYUNLOCKABLE_ACHIEVEMENT_DESC")
                            }));

        public override BodyIndex LookUpRequiredBodyIndex()
        {
            return BodyCatalog.FindBodyIndex(Modules.Survivors.Shiggy.instance.fullBodyName);
        }

        public void ClearCheck(Run run, RunReport runReport)
        {
            if (run is null) return;
            if (runReport is null) return;

            if (!runReport.gameEnding) return;

            if (runReport.gameEnding.isWin)
            {
                DifficultyDef difficultyDef = DifficultyCatalog.GetDifficultyDef(runReport.ruleBook.FindDifficulty());

                if (difficultyDef != null && difficultyDef.countsAsHardMode)
                {
                    if (base.meetsBodyRequirement)
                    {
                        base.Grant();
                    }
                }
            }
        }

        public override void OnInstall()
        {
            base.OnInstall();

            Run.onClientGameOverGlobal += this.ClearCheck;
        }

        public override void OnUninstall()
        {
            base.OnUninstall();

            Run.onClientGameOverGlobal -= this.ClearCheck;
        }
    }
}