using System;
using System.Collections.Generic;
using Enumeration;

namespace Game.Economy
{
    [Serializable]
    public class IndicatorsKeeper /* Нет необходимости знать про другие значения индикаторов */
    {
        private Dictionary<IndicatorType, int> _indicatorsMap;
        
        public Dictionary<IndicatorType, int> IndicatorsMap => _indicatorsMap;

        public IndicatorsKeeper(int people, int supplies, int risk, int days)
        {
            _indicatorsMap = new Dictionary<IndicatorType, int>
            {
                { IndicatorType.People, people },
                { IndicatorType.Supplies, supplies },
                { IndicatorType.Risk, risk },
                { IndicatorType.Days, days }
            };
        }

        public int GetIndicatorValue(IndicatorType type) => _indicatorsMap[type];
    }
}