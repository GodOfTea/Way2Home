using System.Linq;
using Enumeration;
using Game.Economy;
using UnityEngine;

namespace Game.Cnofigs
{
    [CreateAssetMenu(fileName = "IndicatorsConfig", menuName = "Configs/IndicatorsConfig")]
    public class IndicatorsConfig : ScriptableObject
    {
        [SerializeField] public Indicator[] _indicators;
        
        public bool TryGetIndicator(IndicatorType indicatorType, out Indicator indicator)
        {
            indicator = _indicators.FirstOrDefault(o => o.Type == indicatorType);
            return indicator != null;
        }
        
        public int GetIndicatorStartValue(IndicatorType indicatorType)
        {
            return _indicators.FirstOrDefault(o => o.Type == indicatorType)!.Value;
        }
    }
}