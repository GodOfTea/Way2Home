using System.Collections.Generic;
using Enumeration;
using Game.Economy;
using UnityEngine;

namespace Game.UI
{
    public class IndicatorsView : MonoBehaviour
    {
        [SerializeField] private IndicatorElements[] _indicatorElements;
        private IndicatorsBank _indicatorsBank;

        public void UpdateIndicator(IReadOnlyDictionary<IndicatorType, IndicatorProperty> indicators)
        {
            foreach (var indicator in _indicatorElements)
                indicator.UpdateText(indicators[indicator.IndicatorType].Value);
        }
    }
}
