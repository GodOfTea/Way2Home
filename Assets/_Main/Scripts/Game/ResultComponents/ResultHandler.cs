using System;
using System.Collections.Generic;
using Game.Economy;
using Data;
using Enumeration;

namespace Game.ResultComponents
{
    [Serializable]
    public class ResultHandler
    {
        private ResultsDatabase _resultsDatabase;
        private readonly IndicatorsBank _indicatorsBank;

        private bool _isResultSet = false;

        public bool IsResultSet => _isResultSet;
        
        public event Action<ResultVisualData> ResultSet;

        public ResultHandler(ResultsDatabase resultsDatabase, IndicatorsBank indicatorsBank)
        {
            _resultsDatabase = resultsDatabase;
            _indicatorsBank = indicatorsBank;
            _indicatorsBank.IndicatorValueUpdated += CheckResult;
        }
        
        // ~ResultHandler()
        // {
        //     IndicatorsBank.IndicatorValueUpdated -= CheckResult;
        // }

        public void SetResultByLastEvent() =>
            SetResult(IndicatorType.Days);

        /* TODO: Подумать над проверкой результата, критикал велью, где его взять */
        private void CheckResult(IReadOnlyDictionary<IndicatorType, IndicatorProperty> indicators)
        {
            foreach (var indicator in indicators)
            {
                if (indicator.Value.IsCritical() == false)
                    continue;
                SetResult(indicator.Key);
            }
        }

        private void SetResult(IndicatorType type) /* Ошибка со скринами */
        {
            _indicatorsBank.IndicatorValueUpdated -= CheckResult;
            
            var resultVisualData = new ResultVisualData(_resultsDatabase.GetResult(type), 
                isWin: type == IndicatorType.Days);
            
            _isResultSet = true;
            ResultSet?.Invoke(resultVisualData);
        }
    }
}