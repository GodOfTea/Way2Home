using System.Collections.Generic;
using Game.Cnofigs;
using Enumeration;
using System;
using Game.Economy.MoralComponents;
using UnityEngine;

namespace Game.Economy
{
    public class IndicatorsBank /* Отвечает только за value (?) */
    {
        private Dictionary<IndicatorType, IndicatorProperty> _indicators;
        
        public event Action<IReadOnlyDictionary<IndicatorType, IndicatorProperty>> IndicatorValueUpdated;

        private IndicatorsConfig _indicatorsConfig;
        
        public Moral Moral { get; private set; }
        
        public IndicatorsBank(IndicatorsConfig indicatorsConfig, MoralConfig moralConfig)
        {
            _indicatorsConfig = indicatorsConfig;
            Moral = new Moral(moralConfig);
            
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

        public void UpdateIndicatorsValues(IndicatorValue[] indicators, out IndicatorValue[] newIndicators)
        {
            newIndicators = new IndicatorValue[indicators.Length];
            int i = 0;
            foreach (var indicator in indicators)
            {
                int value = indicator.Value;
                if (Math.Abs(indicator.Value) > SpecialIndicatorValues.CHECK_CODE)
                {
                    value = UseSpecialOperation(indicator.Value, _indicators[indicator.Type].Value);
                    _indicators[indicator.Type].ChangeValue(value);
                }
                else
                {
                    _indicators[indicator.Type].AddValue(value);
                }
                newIndicators[i] = new IndicatorValue { Type = indicator.Type, Value = value };
                Debug.Log($"Indicator {indicator.Type} updated by {value}, current value: {_indicators[indicator.Type].Value}");
                ++i;
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