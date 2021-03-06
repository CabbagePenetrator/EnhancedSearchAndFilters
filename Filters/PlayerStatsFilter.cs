﻿using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using EnhancedSearchAndFilters.SongData;
using EnhancedSearchAndFilters.Utilities;

namespace EnhancedSearchAndFilters.Filters
{
    internal class PlayerStatsFilter : FilterBase
    {
        public override string Name => "Player Stats";
        public override bool IsFilterApplied => _hasCompletedAppliedValue != SongCompletedFilterOption.Off || _hasFullComboAppliedValue != SongFullComboFilterOption.Off;
        public override bool HasChanges => _hasCompletedStagingValue != _hasCompletedAppliedValue ||
            _hasFullComboStagingValue != _hasFullComboAppliedValue ||
            _easyStagingValue != _easyAppliedValue ||
            _normalStagingValue != _normalAppliedValue ||
            _hardStagingValue != _hardAppliedValue ||
            _expertStagingValue != _expertAppliedValue ||
            _expertPlusStagingValue != _expertPlusAppliedValue;
        public override bool IsStagingDefaultValues => _hasCompletedStagingValue == SongCompletedFilterOption.Off &&
            _hasFullComboStagingValue == SongFullComboFilterOption.Off &&
            _easyStagingValue == false &&
            _normalStagingValue == false &&
            _hardStagingValue == false &&
            _expertStagingValue == false &&
            _expertPlusStagingValue == false;

        protected override string ViewResource => "EnhancedSearchAndFilters.UI.Views.Filters.PlayerStatsFilterView.bsml";
        protected override string ContainerGameObjectName => "PlayerStatsFilterViewContainer";

        private SongCompletedFilterOption _hasCompletedStagingValue = SongCompletedFilterOption.Off;
        [UIValue("completed-value")]
        public SongCompletedFilterOption HasCompletedStagingValue
        {
            get => _hasCompletedStagingValue;
            set
            {
                _hasCompletedStagingValue = value;
                InvokeSettingChanged();
            }
        }
        private SongFullComboFilterOption _hasFullComboStagingValue = SongFullComboFilterOption.Off;
        [UIValue("full-combo-value")]
        public SongFullComboFilterOption HasFullComboStagingValue
        {
            get => _hasFullComboStagingValue;
            set
            {
                _hasFullComboStagingValue = value;
                InvokeSettingChanged();
            }
        }
        private bool _easyStagingValue = false;
        [UIValue("easy-value")]
        public bool EasyStagingValue
        {
            get => _easyStagingValue;
            set
            {
                _easyStagingValue = value;
                InvokeSettingChanged();
            }
        }
        private bool _normalStagingValue = false;
        [UIValue("normal-value")]
        public bool NormalStagingValue
        {
            get => _normalStagingValue;
            set
            {
                _normalStagingValue = value;
                InvokeSettingChanged();
            }
        }
        private bool _hardStagingValue = false;
        [UIValue("hard-value")]
        public bool HardStagingValue
        {
            get => _hardStagingValue;
            set
            {
                _hardStagingValue = value;
                InvokeSettingChanged();
            }
        }
        private bool _expertStagingValue = false;
        [UIValue("expert-value")]
        public bool ExpertStagingValue
        {
            get => _expertStagingValue;
            set
            {
                _expertStagingValue = value;
                InvokeSettingChanged();
            }
        }
        private bool _expertPlusStagingValue = false;
        [UIValue("expert-plus-value")]
        public bool ExpertPlusStagingValue
        {
            get => _expertPlusStagingValue;
            set
            {
                _expertPlusStagingValue = value;
                InvokeSettingChanged();
            }
        }

        private SongCompletedFilterOption _hasCompletedAppliedValue = SongCompletedFilterOption.Off;
        private SongFullComboFilterOption _hasFullComboAppliedValue = SongFullComboFilterOption.Off;
        private bool _easyAppliedValue = false;
        private bool _normalAppliedValue = false;
        private bool _hardAppliedValue = false;
        private bool _expertAppliedValue = false;
        private bool _expertPlusAppliedValue = false;

        [UIValue("completed-options")]
        private static readonly List<object> SongCompletedOptions = Enum.GetValues(typeof(SongCompletedFilterOption)).Cast<SongCompletedFilterOption>().Select(x => (object)x).ToList();
        [UIValue("full-combo-options")]
        private static readonly List<object> FullComboOptions = Enum.GetValues(typeof(SongFullComboFilterOption)).Cast<SongFullComboFilterOption>().Select(x => (object)x).ToList();

        public override void SetDefaultValuesToStaging()
        {
            _hasCompletedStagingValue = SongCompletedFilterOption.Off;
            _hasFullComboStagingValue = SongFullComboFilterOption.Off;
            _easyStagingValue = false;
            _normalStagingValue = false;
            _hardStagingValue = false;
            _expertStagingValue = false;
            _expertPlusStagingValue = false;

            RefreshValues();
        }

        public override void SetAppliedValuesToStaging()
        {
            _hasCompletedStagingValue = _hasCompletedAppliedValue;
            _hasFullComboStagingValue = _hasFullComboAppliedValue;
            _easyStagingValue = _easyAppliedValue;
            _normalStagingValue = _normalAppliedValue;
            _hardStagingValue = _hardAppliedValue;
            _expertStagingValue = _expertAppliedValue;
            _expertPlusStagingValue = _expertPlusAppliedValue;

            RefreshValues();
        }

        public override void ApplyStagingValues()
        {
            _hasCompletedAppliedValue = _hasCompletedStagingValue;
            _hasFullComboAppliedValue = _hasFullComboStagingValue;
            _easyAppliedValue = _easyStagingValue;
            _normalAppliedValue = _normalStagingValue;
            _hardAppliedValue = _hardStagingValue;
            _expertAppliedValue = _expertStagingValue;
            _expertPlusAppliedValue = _expertPlusStagingValue;
        }

        public override void ApplyDefaultValues()
        {
            _hasCompletedAppliedValue = SongCompletedFilterOption.Off;
            _hasFullComboAppliedValue = SongFullComboFilterOption.Off;
            _easyAppliedValue = false;
            _normalAppliedValue = false;
            _hardAppliedValue = false;
            _expertAppliedValue = false;
            _expertPlusAppliedValue = false;
        }

        public override void FilterSongList(ref List<BeatmapDetails> detailsList)
        {
            if (!IsFilterApplied)
                return;

            List<BeatmapDifficulty> diffList = new List<BeatmapDifficulty>(5);
            if (_easyAppliedValue)
                diffList.Add(BeatmapDifficulty.Easy);
            if (_normalAppliedValue)
                diffList.Add(BeatmapDifficulty.Normal);
            if (_hardAppliedValue)
                diffList.Add(BeatmapDifficulty.Hard);
            if (_expertAppliedValue)
                diffList.Add(BeatmapDifficulty.Expert);
            if (_expertPlusAppliedValue)
                diffList.Add(BeatmapDifficulty.ExpertPlus);

            // touch the helper singletons to make sure they're instantiated before the parallel query
            // also, might as well do a check to see if they were able to be instantiated
            if (PlayerDataHelper.Instance == null && LocalLeaderboardDataHelper.Instance == null)
            {
                Logger.log.Warn("Both PlayerDataHelper and LocalLeaderboardDataHelper objects could not be instantiated (unable to apply player stats filter)");
                return;
            }

            var levelsToRemove = detailsList.AsParallel().Where(delegate (BeatmapDetails details)
            {
                bool remove = false;

                // NOTE: if any difficulties are selected, this filter also has the same behaviour as the difficulty filter
                if (diffList.Count > 0)
                {
                    bool hasDifficultiesToCheck = details.DifficultyBeatmapSets.Any(set => set.DifficultyBeatmaps.Any(diff => diffList.Contains(diff.Difficulty)));
                    remove |= !hasDifficultiesToCheck;
                }
                if (_hasCompletedAppliedValue != SongCompletedFilterOption.Off && !remove)
                {
                    // if PlayerData and LocalLeaderboardModel objects cannot be found, assume level has not been completed
                    bool hasBeenCompleted = PlayerDataHelper.Instance?.HasCompletedLevel(details.LevelID, null, diffList) ?? false;
                    hasBeenCompleted |= LocalLeaderboardDataHelper.Instance?.HasCompletedLevel(details.LevelID, diffList) ?? false;

                    remove |= hasBeenCompleted != (_hasCompletedAppliedValue == SongCompletedFilterOption.HasCompleted);
                }
                if (_hasFullComboAppliedValue != SongFullComboFilterOption.Off && !remove)
                {
                    // if PlayerData and LocalLeaderboardModel objects cannot be found, assume level has not been full combo'd
                    bool hasFullCombo = PlayerDataHelper.Instance?.HasFullComboForLevel(details.LevelID, null, diffList) ?? false;
                    hasFullCombo |= LocalLeaderboardDataHelper.Instance?.HasFullComboForLevel(details.LevelID, diffList) ?? false;

                    remove |= hasFullCombo != (_hasFullComboAppliedValue == SongFullComboFilterOption.HasFullCombo);
                }

                return remove;
            }).ToList();

            foreach (var level in levelsToRemove)
                detailsList.Remove(level);
        }

        public override List<FilterSettingsKeyValuePair> GetAppliedValuesAsPairs()
        {
            return FilterSettingsKeyValuePair.CreateFilterSettingsList(
                "completed", _hasCompletedAppliedValue,
                "fullCombo", _hasFullComboAppliedValue,
                "easy", _easyAppliedValue,
                "normal", _normalAppliedValue,
                "hard", _hardAppliedValue,
                "expert", _expertAppliedValue,
                "expertPlus", _expertPlusAppliedValue);
        }

        public override void SetStagingValuesFromPairs(List<FilterSettingsKeyValuePair> settingsList)
        {
            SetDefaultValuesToStaging();

            foreach (var pair in settingsList)
            {
                if (bool.TryParse(pair.Value, out bool boolValue))
                {
                    switch (pair.Key)
                    {
                        case "easy":
                            _easyStagingValue = boolValue;
                            break;
                        case "normal":
                            _normalStagingValue = boolValue;
                            break;
                        case "hard":
                            _hardStagingValue = boolValue;
                            break;
                        case "expert":
                            _expertStagingValue = boolValue;
                            break;
                        case "expertPlus":
                            _expertPlusStagingValue = boolValue;
                            break;
                    }
                }
                else if (pair.Key == "completed" && Enum.TryParse(pair.Value, out SongCompletedFilterOption completedValue))
                {
                    _hasCompletedStagingValue = completedValue;
                }
                else if (pair.Key == "fullCombo" && Enum.TryParse(pair.Value, out SongFullComboFilterOption fcValue))
                {
                    _hasFullComboStagingValue = fcValue;
                }
            }

            RefreshValues();
        }

        [UIAction("completed-formatter")]
        private string SongCompletedFormatter(object value)
        {
            switch ((SongCompletedFilterOption)value)
            {
                case SongCompletedFilterOption.Off:
                    return "Off";
                case SongCompletedFilterOption.HasCompleted:
                    return "<size=62%>Keep Completed</size>";
                case SongCompletedFilterOption.HasNeverCompleted:
                    return "<size=62%>Keep Never Completed</size>";
                default:
                    return "ERROR!";
            }
        }

        [UIAction("full-combo-formatter")]
        private string SongFullComboFormatter(object value)
        {
            switch ((SongFullComboFilterOption)value)
            {
                case SongFullComboFilterOption.Off:
                    return "Off";
                case SongFullComboFilterOption.HasFullCombo:
                    return "<size=75%>Keep Songs With FC</size>";
                case SongFullComboFilterOption.HasNoFullCombo:
                    return "<size=75%>Keep Songs Without FC</size>";
                default:
                    return "ERROR!";
            }
        }
    }

    internal enum SongCompletedFilterOption
    {
        Off = 0,
        HasNeverCompleted = 1,
        HasCompleted = 2
    }

    internal enum SongFullComboFilterOption
    {
        Off = 0,
        HasNoFullCombo = 1,
        HasFullCombo = 2
    }
}
