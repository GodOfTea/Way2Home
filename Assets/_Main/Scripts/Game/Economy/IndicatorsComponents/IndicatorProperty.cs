using UnityEngine;

namespace Game.Economy
{
    public class IndicatorProperty
    {
        private int _count;
        private int _criticalValue;
        private int _min;
        private int _max;

        public int Value => _count;

        public IndicatorProperty(int value, int criticalValue, Vector2Int minMax)
        {
            _count = value;
            _criticalValue = criticalValue;
            _min = minMax.x;
            _max = minMax.y;
        }

        public void ChangeValue(int value)
        {
            _count = value;
            FixBorders();
        }

        public void AddValue(int value)
        {
            _count += value;
            FixBorders();
        }

        public bool IsCritical() => _count == _criticalValue;

        private void FixBorders()
        {
            if (_count < _min)
                _count = _min;

            if (_count > _max)
                _count = _max;
        }
    }
}