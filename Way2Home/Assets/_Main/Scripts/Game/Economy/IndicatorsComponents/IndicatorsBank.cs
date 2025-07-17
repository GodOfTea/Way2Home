using System.Collections.Generic;
using Game.Cnofigs;
using Enumeration;
using System;

namespace Game.Economy
{
    public class IndicatorsBank /* Временно статик */ /* Отвечает только за value (?) */
    {
        private Dictionary<IndicatorType, IndicatorProperty> _indicators;

        public event Action<IReadOnlyDictionary<IndicatorType, IndicatorProperty>> IndicatorValueUpdated;

        private IndicatorsConfig _indicatorsConfig;

        public IndicatorsBank(IndicatorsConfig indicatorsConfig)
        {
            _indicatorsConfig = indicatorsConfig;
            
            _indicators = new Dictionary<IndicatorType, IndicatorProperty>()
            {
                { IndicatorType.People,   CreateProperty(IndicatorType.People) },
                { IndicatorType.Supplies, CreateProperty(IndicatorType.Supplies) },
                { IndicatorType.Risk,     CreateProperty(IndicatorType.Risk) },
                { IndicatorType.Days,     CreateProperty(IndicatorType.Days) }
            };
        }

        public void SetIndicators()
        {
            IndicatorValueUpdated?.Invoke(_indicators);
        }

        public void UpdateIndicatorsValues(IReadOnlyDictionary<IndicatorType, int> indicators)
        {
            foreach (var indicator in indicators)
            {
                if (Math.Abs(indicator.Value) > SpecialIndicatorValues.CHECK_CODE)
                    _indicators[indicator.Key].ChangeValue(
                        UseSpecialOperation(indicator.Value, _indicators[indicator.Key].Value)); 
                else
                    _indicators[indicator.Key].AddValue(indicator.Value);
            }
            
            IndicatorValueUpdated?.Invoke(_indicators);
        }
        
        public void UpdateIndicatorValue(IndicatorType type, int value)
        {
            _indicators[type].AddValue(value);
            IndicatorValueUpdated?.Invoke(_indicators);
        }
        
        public int GetCurrentIndicatorValue(IndicatorType type) => _indicators[type].Value;
        public void SetIndicatorValue(IndicatorType type, int value) => _indicators[type].ChangeValue(value);

        private IndicatorProperty CreateProperty(IndicatorType type)
        {
            Indicator intdicator = null;

            if (_indicatorsConfig.TryGetIndicator(type, out intdicator) == false)
                throw new NullReferenceException();
            
            IndicatorProperty property = new IndicatorProperty(
                intdicator.Value, intdicator.CriticalValue, intdicator.MinMaxValue);
            
            return property;
        }

        private int UseSpecialOperation(int value, int current)
        {
            if (value == SpecialIndicatorValues.MULTIPLY_CODE)
                return current * 2;
            if (value == SpecialIndicatorValues.DIVIDE_CODE)
                return current / 2;

            return current;
        }
    }
}