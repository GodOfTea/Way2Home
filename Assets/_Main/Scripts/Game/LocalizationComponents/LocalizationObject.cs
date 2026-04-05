using Infrastructure.Services;
using Infrastructure.Services.Localization;
using UnityEngine;

namespace Game.LocalizationComponents
{
    public abstract class LocalizationObject : MonoBehaviour
    {
        ILocalizationService _localizationService;
        
        private void Awake()
        {
            _localizationService = AllServices.Container.Single<ILocalizationService>();
        }

        protected virtual void OnLanguageChanged(LanguageType obj) { }
    }
}