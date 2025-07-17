using Data.Progress;
using Infrastructure.Services;
using Infrastructure.Services.Localization;
using Infrastructure.Services.PersistentProgreses;
using Infrastructure.Services.Settings;
using UnityEngine;

namespace SettingsComponents
{
    public class SettingsManager : MonoBehaviour, ISavedProgress
    {
        private ISettings _settings;

        private LanguageType _currentLanguage;
        private int _currentSoundVolume;
        private int _currentMusicVolume;

        public int CurrentSoundVolume => _currentSoundVolume;
        public int CurrentMusicVolume => _currentMusicVolume;

        public LanguageType CurrentLanguage => _currentLanguage;

        private void Awake()
        {
            _settings = AllServices.Container.Single<ISettings>();
            DontDestroyOnLoad(this);
        }

        public void ChangeMusicVolume(int volume)
        {
            _currentMusicVolume = volume;
            _settings.SetMusicVolume(_currentMusicVolume);
        }
        
        public void ChangeSoundVolume(int volume)
        {
            _currentSoundVolume = volume;
            _settings.SetSoundVolume(_currentSoundVolume);
        }
        
        public void ChangeLanguage(int languageIndex)
        {
            _currentLanguage = (LanguageType)languageIndex;
            _settings.SetLanguage(_currentLanguage);
        }
        
        private void ChangeLanguage(LanguageType languageType)
        {
            _currentLanguage = languageType;
            _settings.SetLanguage(_currentLanguage);
        }

        public void UpdateProgress(GameProgress progress)
        {
            progress.SettingsData.SoundVolume = _currentSoundVolume;
            progress.SettingsData.MusicVolume = _currentMusicVolume;
            progress.SettingsData.Language = _currentLanguage;
        }

        public void LoadProgress(GameProgress progress)
        {
            _currentSoundVolume = progress.SettingsData.SoundVolume;
            _currentMusicVolume = progress.SettingsData.MusicVolume;
            _currentLanguage = progress.SettingsData.Language;
            
            SetSettings();
        }
        
        private void SetSettings()
        {
            ChangeLanguage(_currentLanguage);
            ChangeSoundVolume(_currentSoundVolume);
            ChangeMusicVolume(_currentMusicVolume);
        }
    }
}