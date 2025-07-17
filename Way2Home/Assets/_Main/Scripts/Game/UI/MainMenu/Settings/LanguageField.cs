using System;
using Infrastructure.Services;
using Infrastructure.Services.Localization;
using TMPro;
using UnityEngine;

namespace Game.UI.MainMenu.Settings
{
    public class LanguageField : SettingsField
    {
        [SerializeField] private TMP_Text _languageTitle;
        
        private int _currentLanguageIndex;
        private int _localizationsCount;
        
        private ILocalizationService _localizationService;

        public event Action<int> ValueUpdated;
        
        private void Start()
        {
            _localizationsCount = Enum.GetValues(typeof(LanguageType)).Length;
            _localizationService = AllServices.Container.Single<ILocalizationService>();
        }

        public void SetLanguage(LanguageType languageType)
        {
            _currentLanguageIndex = (int)languageType;
            ChangeTitle();
        }

        protected override void NextOnPressed()
        {
            base.NextOnPressed();
            _currentLanguageIndex += 1;

            if (_currentLanguageIndex >= _localizationsCount)
                _currentLanguageIndex = 0;

            ValueUpdated?.Invoke(_currentLanguageIndex);
            ChangeTitle();
        }

        protected override void PreviousOnPressed()
        {
            base.PreviousOnPressed();
            _currentLanguageIndex -= 1;

            if (_currentLanguageIndex < 0)
                _currentLanguageIndex = _localizationsCount - 1;
            
            ValueUpdated?.Invoke(_currentLanguageIndex);
            _languageTitle.text = _localizationService.GetLocalizationName();
        }

        private void ChangeTitle()
        {
            _languageTitle.text = _localizationService.GetLocalizationName();
        }
    }
}