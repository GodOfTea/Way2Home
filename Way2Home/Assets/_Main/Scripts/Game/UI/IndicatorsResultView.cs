using System.Collections.Generic;
using System.Collections.ObjectModel;
using Enumeration;
using Game.Economy;
using Game.UI;
using UnityEngine;

public class IndicatorsResultView : MonoBehaviour
{
    [SerializeField] private ResultIndicatorElements[] _resultIndicatorElements;
    private Dictionary<IndicatorType, ResultIndicatorElements> _resultIndicatorMap;

    private void Awake()
    {
        _resultIndicatorMap = new Dictionary<IndicatorType, ResultIndicatorElements>();
        foreach (var indicatorElement in _resultIndicatorElements)
        {
            _resultIndicatorMap.Add(indicatorElement.IndicatorType, indicatorElement);
        }
    }

    private void Start()
    {
        foreach (var indicatorElement in _resultIndicatorElements)
        {
            indicatorElement.Disable();
        }
    }

    public void UpdateValues(IReadOnlyDictionary<IndicatorType, int> indicators)
    {
        foreach (var indicator in indicators)
        {
            var resultElement = _resultIndicatorMap[indicator.Key];

            if (indicator.Value != 0)
            {
                resultElement.Enable();
                resultElement.UpdateText(indicator.Value);
            }
            else
            {
                resultElement.Disable();
            }
        }
    }
}
