using System;
using Enumeration;
using UnityEngine;

namespace Game.Economy
{
    [Serializable]
    public class Indicator : IIndicatorValue
    {
        [SerializeField] private string _name;
        
        [Space]
        [SerializeField] private IndicatorType _type;
        [SerializeField] private int _value;
        [SerializeField] private Vector2Int _minMaxValue;
        [SerializeField] private int _criticalValue;

        public int Value => _value;
        public int CriticalValue => _criticalValue;

        public IndicatorType Type => _type;
        public Vector2Int MinMaxValue => _minMaxValue;
    }
}