using UnityEngine;

namespace Game.Cnofigs
{
    public class GameConfigs
    {
        private IndicatorsConfig _indicatorsConfig;

        public IndicatorsConfig IndicatorsConfig => _indicatorsConfig;

        public GameConfigs()
        {
            _indicatorsConfig = Resources.Load<IndicatorsConfig>("Configs/IndicatorsConfig");
        }
    }
}