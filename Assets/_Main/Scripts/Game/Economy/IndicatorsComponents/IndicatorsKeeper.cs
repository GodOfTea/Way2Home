using System;
using System.Linq;
using Enumeration;
using UnityEngine;

namespace Game.Economy
{
    [Serializable]
    public class IndicatorsKeeper /* Нет необходимости знать про другие значения индикаторов */
    {
        [SerializeField] private IndicatorValue[] _indicators;

        public IndicatorValue[] Indicators => _indicators;

        public IndicatorsKeeper(int people, int supplies, int risk, int days)
        {
            _indicators = new[]
            {
                new IndicatorValue { Type = IndicatorType.People, Value = people },
                new IndicatorValue { Type = IndicatorType.Supplies, Value = supplies },
                new IndicatorValue { Type = IndicatorType.Risk, Value = risk },
                new IndicatorValue { Type = IndicatorType.Days, Value = days }
            };
        }

        public int GetIndicatorValue(IndicatorType type)
        {
            return _indicators.FirstOrDefault(o => o.Type == type)?.Value ?? 
                   throw new NullReferenceException();
        }
    }

    [Serializable]
    public class IndicatorValue
    {
        public IndicatorType Type;
        public int Value;
    }
}