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

    public void UpdateValues(IndicatorValue[] indicators)
    {
        foreach (var indicator in indicators)
        {
            var resultElement = _resultIndicatorMap[indicator.Type];

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
