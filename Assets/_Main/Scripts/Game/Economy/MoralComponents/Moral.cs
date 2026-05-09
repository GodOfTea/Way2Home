using UnityEngine;

namespace Game.Economy.MoralComponents
{
    public class Moral
    {
        private readonly Vector2 _moralRange;
        private int _moralValue;

        /* TODO: Временно, для тестов */
        public int MoralValue => _moralValue;

        public Moral(MoralConfig moralConfig)
        {
            _moralRange = new Vector2(moralConfig.NegativeMoralValue, moralConfig.PositiveMoralValue);
            _moralValue = moralConfig.StartMoralValue;
        }

        public void Change(int value)
        {
            _moralValue += value;
        }

        public int GetMoralStatus()
        {
            return _moralValue <= _moralRange.x ? -1 :
                _moralValue >= _moralRange.y ? 1 : 0;
        }
        
        public void SetNewMoralValue(int value)
        {
            _moralValue = value;
        }
    }
}