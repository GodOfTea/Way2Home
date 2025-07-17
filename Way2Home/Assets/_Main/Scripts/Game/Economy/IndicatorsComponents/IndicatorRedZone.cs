using System;
using Enumeration;
using UnityEngine;

namespace Game.Economy
{
    [Serializable]
    public class IndicatorRedZone : IRedZoneValue
    {
        [SerializeField] private IndicatorType _indicatorType;
        [SerializeField] private int _redZoneValue;
        [SerializeField] private bool _isLess;

        public IndicatorType IndicatorType => _indicatorType;
        public int RedZoneValue => _redZoneValue;
        public bool IsLess => _isLess;
    }
}