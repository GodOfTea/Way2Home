using Data;
using Infrastructure.Services.Localization;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;

namespace Infrastructure.Services.Settings
{
    public class SettingsService : ISettings
    {
        private const string MASTER_MIXER = "Master";

        private readonly string _musicMixerGroupName = "Music";
        private readonly string _soundsMixerGroupName = "Sounds";
        
        private readonly string _soundParamName = "SoundsVolume";
        private readonly string _musicParamName = "MusicVolume";

        private readonly float _step = 8f;
        private readonly float _minVolume = -80f;

        private LanguageType _currentLanguage;
        private int _currentSoundVolume;
        private int _currentMusicVolume;

        private readonly AudioMixer _masterMixer;
        private readonly AudioMixerGroup _musicMixerGroup;
        private readonly AudioMixerGroup _soundsMixerGroup;

        public LanguageType CurrentLanguage => _currentLanguage;
        public AudioMixerGroup MusicMixerGroup => _musicMixerGroup;
        public AudioMixerGroup SoundsMixerGroup => _soundsMixerGroup;

        public SettingsService()
        {
            _masterMixer = Resources.Load<AudioMixer>(Paths.AUDIO_MIXERS + MASTER_MIXER);
            _musicMixerGroup = GetAudioMixerGroup(_musicMixerGroupName);
            _soundsMixerGroup = GetAudioMixerGroup(_soundsMixerGroupName);
        }

        public void SetLanguage(LanguageType language)
        {
            _currentLanguage = language;
            UpdateLanguage();
        }

        public void UpdateLanguage()
        {
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll(_currentLanguage.ToString());

            foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
            {
                if (_currentLanguage.ToString() == locale.LocaleName)
                {
                    LocalizationSettings.SelectedLocale = locale;
                    break;
                }
            }
        }
        
        private AudioMixerGroup GetAudioMixerGroup(string groupName)
        {
            AudioMixerGroup[] groups = _masterMixer.FindMatchingGroups(groupName);
            return groups.Length > 0 ? groups[0] : null;
        }

        public void SetMusicVolume(int value)
        {
            _masterMixer.SetFloat(_musicParamName, _minVolume + _step * value);
        }

        public void SetSoundVolume(int value)
        {
            _masterMixer.SetFloat(_soundParamName, _minVolume + _step * value);

        }
    }
}
