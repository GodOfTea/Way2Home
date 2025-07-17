using System.Collections.Generic;
using Enumeration;

namespace Game.Economy
{
    public interface IIndicatorsKeeper
    {
        IReadOnlyDictionary<IndicatorType, int> IndicatorsMap { get; }
    }
}