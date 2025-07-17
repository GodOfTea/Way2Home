using System;
using System.Collections.Generic;
using Enumeration;
using Game.Cnofigs;

namespace Data.Progress
{
    [Serializable]
    public class GameplayData
    {
        /* Events */
        public List<string> PreviousEvents;
        public string CurrentEvent;
        public bool EndWithResult;
        
        /* Indicators */
        public int People;
        public int Supplies;
        public int Risk;
        public int Days;
        
        public GameConfigs GameConfigs;

        public GameplayData()
        {
            PreviousEvents = new List<string>();
            CurrentEvent = string.Empty;
            EndWithResult = false;

            GameConfigs = new GameConfigs();
            IndicatorsConfig indicatorsConfig = GameConfigs.IndicatorsConfig;

            People = indicatorsConfig.GetIndicatorStartValue(IndicatorType.People);
            Supplies = indicatorsConfig.GetIndicatorStartValue(IndicatorType.Supplies);
            Risk = indicatorsConfig.GetIndicatorStartValue(IndicatorType.Risk);
            Days = indicatorsConfig.GetIndicatorStartValue(IndicatorType.Days);
        }
    }
}