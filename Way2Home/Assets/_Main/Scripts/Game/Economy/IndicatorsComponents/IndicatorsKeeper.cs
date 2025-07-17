using System.Collections.Generic;
using Enumeration;

namespace Game.Economy
{
    public class IndicatorsKeeper : IIndicatorsKeeper /* Нет необходимости знать про другие значения индикаторов */
    {
        private Dictionary<IndicatorType, int> _indicatorsMap;

        public IReadOnlyDictionary<IndicatorType, int> IndicatorsMap => _indicatorsMap;

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
    }
}