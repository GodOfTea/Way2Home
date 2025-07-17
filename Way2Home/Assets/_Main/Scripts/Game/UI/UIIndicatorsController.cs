using System.Collections.Generic;
using Enumeration;
using Game.Economy;
using Game.UI.ScreensComponents;
using Game.UI.ScreensComponents.Gameplay;

namespace Game.UI
{
    public class UIIndicatorsController
    {
        private readonly Screens _screens;
        private readonly IndicatorsBank _indicatorsBank;

        private IndicatorsView _indicatorsView;
        
        public UIIndicatorsController(Screens screens, IndicatorsBank indicatorsBank)
        {
            _screens = screens;
            _indicatorsBank = indicatorsBank;
            _indicatorsView = _screens.Get<MainScreen>().IndicatorsView;
            _screens.Get<MainScreen>().MainScreenShowed += ShowIndicators;
            
            _indicatorsBank.IndicatorValueUpdated += UpdateIndicatorsValue;
        }
        
        ~UIIndicatorsController()
        {
            _screens.Get<MainScreen>().MainScreenShowed -= ShowIndicators;
            _indicatorsBank.IndicatorValueUpdated -= UpdateIndicatorsValue;
            _indicatorsView = null;
        }
        
        private void ShowIndicators() =>
            _indicatorsBank.SetIndicators();

        private void UpdateIndicatorsValue(IReadOnlyDictionary<IndicatorType, IndicatorProperty> indicators)
        {
            _indicatorsView.UpdateIndicator(indicators);
        }
    }
}