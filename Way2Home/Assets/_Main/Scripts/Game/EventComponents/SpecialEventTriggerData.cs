using System;
using Enumeration;

namespace EventComponents
{
    [Serializable]
    public class SpecialEventTriggerData
    {
        public IndicatorType IndicatorType;
        public int ValueToTrigger;
        public bool IsLess;
    }
}