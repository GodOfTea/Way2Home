using Game.Economy.MoralComponents;
using UnityEngine;

namespace Game.Cnofigs
{
    public class GameConfigs
    {
        private IndicatorsConfig _indicatorsConfig;
        private MoralConfig _moralConfig;

        public IndicatorsConfig IndicatorsConfig => _indicatorsConfig;
        public MoralConfig MoralConfig => _moralConfig;

        public GameConfigs()
        {
            _indicatorsConfig = Resources.Load<IndicatorsConfig>("Configs/IndicatorsConfig");
            _moralConfig = Resources.Load<MoralConfig>("Configs/MoralConfig");
        }
    }
}