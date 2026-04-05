using System;
using Game.UI.MainMenu.Settings;
using Infrastructure.Services;
using Infrastructure.Services.Factory;
using SettingsComponents;
using UnityEngine;

namespace Game.UI.ScreensComponents.MainMenu
{
    public class SettingsScreen : ScreenBase
    {
        [SerializeField] private LanguageField _languageField;
        [SerializeField] private VolumeField _musicVolume;
        [SerializeField] private VolumeField _soundVolume;

        [Header("Static")]
        [SerializeField] private SettingsManager _settingsManager;

        private void Start()
        {
            RegisterSettingsManager();
        }

        public override void Show()
        {
            SetFields();       
            base.Show();
        }

        public override void ShowImmediate()
        {
            SetFields();            
            base.ShowImmediate();
        }
        
        private void SetFields()
        {
            RegisterSettingsManager();
            
            _languageField.SetLanguage(_settingsManager.CurrentLanguage);
            _musicVolume.SetValue(_settingsManager.CurrentMusicVolume);
            _soundVolume.SetValue(_settingsManager.CurrentSoundVolume);
        }

        private void OnEnable()
        {
            _languageField.ValueUpdated += OnLanguageChanged;
            
            _musicVolume.ValueUpdated += ChangeMusicVolume;
            _soundVolume.ValueUpdated += ChangeSoundVolume;
        }

        private void OnDestroy()
        {
            _languageField.ValueUpdated -= OnLanguageChanged;
            _musicVolume.ValueUpdated -= ChangeMusicVolume;
            _soundVolume.ValueUpdated -= ChangeSoundVolume;
        }

        private void OnLanguageChanged(int languageIndex)
        {
            _settingsManager.ChangeLanguage(languageIndex);
        }

        private void ChangeMusicVolume(int value)
        {
            _settingsManager.ChangeMusicVolume(value);
        }

        private void ChangeSoundVolume(int value)
        {
            _settingsManager.ChangeSoundVolume(value);
        }

        private void RegisterSettingsManager()
        {
            _settingsManager = FindObjectOfType<SettingsManager>();
            
            if (_settingsManager == null)
                _settingsManager = AllServices.Container.Single<IGameFactory>().SettingsManager;
        }
    }
}