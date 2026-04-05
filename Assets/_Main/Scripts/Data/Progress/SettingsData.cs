using System;
using Infrastructure.Services.Localization;

namespace Data.Progress
{
    [Serializable]
    public class SettingsData
    {
        public LanguageType Language;
        public int SoundVolume;
        public int MusicVolume;

        public SettingsData()
        {
            Language = LanguageType.English;
            SoundVolume = 5;
            MusicVolume = 5;
        }
    }
}