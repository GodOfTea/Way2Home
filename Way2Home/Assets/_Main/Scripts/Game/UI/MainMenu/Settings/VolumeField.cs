using System;
using TMPro;
using UnityEngine;

namespace Game.UI.MainMenu.Settings
{
    public class VolumeField : SettingsField
    {
        [SerializeField] private TMP_Text _valueView;

        private readonly int _maxValue = 10;
        private readonly int _minValue = 0;
        
        private int _value;

        public event Action<int> ValueUpdated;

        public void SetValue(int value)
        {
            _value = value;
            _valueView.text = $"{_value}";
        }

        protected override void NextOnPressed()
        {
            base.NextOnPressed();
            _value += 1;

            if (_value > _maxValue)
                _value = _maxValue;

            _valueView.text = $"{_value}";
            ValueUpdated?.Invoke(_value);
        }

        protected override void PreviousOnPressed()
        { 
            base.PreviousOnPressed();
            _value -= 1;

            if (_value < _minValue)
                _value = _minValue;
            
            _valueView.text = $"{_value}";
            ValueUpdated?.Invoke(_value);
        }
    }
}