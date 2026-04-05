using System;
using System.Collections.Generic;
using Infrastructure.Services.Settings;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Infrastructure.Services.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private readonly ISettings _settingService;
        private LocalizedStringDatabase _localizedStringDatabase;
        private List<ILocalizable> _localizableObjects;
        private Dictionary<LanguageType, string> _localizationNames;
        
        public event Action OnLanguageChanged;
        
        public LocalizationService(ISettings settingsService)
        {
            _localizableObjects = new List<ILocalizable>();
            _localizationNames = new Dictionary<LanguageType, string>()
            {
                { LanguageType.English, "english" },
                { LanguageType.Russian, "Русский" }
            };


            _settingService = settingsService;
            _localizedStringDatabase = LocalizationSettings.StringDatabase;
            LocalizationSettings.SelectedLocaleChanged += UpdateLocalization;
        }

        ~LocalizationService()
        {
            LocalizationSettings.SelectedLocaleChanged -= UpdateLocalization;
        }

        private void UpdateLocalization(Locale locale)
        {
            foreach (var localizable in _localizableObjects)
                localizable.UpdateLocalization();
            
            OnLanguageChanged?.Invoke();
        }

        /* TODO: Поменять */
        public string GetLocalizationName() => 
            _localizationNames[_settingService.CurrentLanguage];
        
        public string GetLocalizationEventText(string key) => 
            _localizedStringDatabase.GetLocalizedString(LocalizationTables.EventsTable, key);

        public void AddLocalizable(ILocalizable localizable) =>
            _localizableObjects.Add(localizable);
    }
}