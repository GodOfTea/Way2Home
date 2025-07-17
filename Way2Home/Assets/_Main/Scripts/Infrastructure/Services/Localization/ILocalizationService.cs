using System;

namespace Infrastructure.Services.Localization
{
    public interface ILocalizationService : IService
    {
        public event Action OnLanguageChanged;
        
        string GetLocalizationName();
        string GetLocalizationEventText(string key);
        void AddLocalizable(ILocalizable localizable);
    }
}