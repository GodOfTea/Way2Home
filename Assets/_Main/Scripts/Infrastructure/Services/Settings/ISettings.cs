using Infrastructure.Services.Localization;
using UnityEngine.Audio;

namespace Infrastructure.Services.Settings
{
    public interface ISettings : IService
    {
        LanguageType CurrentLanguage { get; }
        AudioMixerGroup MusicMixerGroup { get; }
        AudioMixerGroup SoundsMixerGroup { get; }
        
        void SetLanguage(LanguageType language);
        void UpdateLanguage();
        void SetMusicVolume(int value);
        void SetSoundVolume(int value);
    }
}