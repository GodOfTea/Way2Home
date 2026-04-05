using System.Collections.Generic;
using Enumeration;

namespace Game.Economy
{
    public interface IIndicatorsKeeper
    {
        Dictionary<IndicatorType, int> IndicatorsMap { get; }
    }
}