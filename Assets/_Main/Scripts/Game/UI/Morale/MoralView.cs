using System;
using System.Linq;
using Game.Economy.MoralComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Event
{
    public class MoralView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moralValueText;
        [SerializeField] private Image _mainIcon;
        [SerializeField] private MoraleBar[] _moraleBars;
        [SerializeField] private MoraleViewSettings _viewSettings;
        [SerializeField] private MoraleVisual[] _moraleVisuals;

        private MoraleType _currentMoraleType = MoraleType.Nothing;
        private MoralConfig _moralConfig;
        private int _currentMoralValue = 0;
        
        public void Setup(MoralConfig moralConfig)
        {
            _moralConfig = moralConfig;
        }
        
        public void UpdateMoralValue(int moralValue)
        {
            var newMoraleType = CalculateMoralType(moralValue);
            bool isMaxNew = IsMaxMoralValue(moralValue);
            bool isMaxCurrent = IsMaxMoralValue(_currentMoralValue);

            if (_currentMoraleType != newMoraleType || isMaxNew != isMaxCurrent)
            {
                UpdateMoralVisual(newMoraleType, isMaxNew);
            }
            
            _moralValueText.text = $"{moralValue}";
            float moralModifier = (float)moralValue / (_currentMoraleType == MoraleType.Positive ? 
                _moralConfig.PositiveMoralValue : _moralConfig.NegativeMoralValue);
            foreach (MoraleBar bar in _moraleBars)
            {
                bar.UpdateValue(moralModifier, _viewSettings.MoralBarChangeSpeed);
            }

            _currentMoralValue = moralValue;
        }

        private void UpdateMoralVisual(MoraleType newMoraleType, bool isMaxNew)
        {
            _currentMoraleType = newMoraleType;
            
            var visual = _moraleVisuals.FirstOrDefault(v => v.Type == newMoraleType);
            if (visual == null)
            {
                throw new NullReferenceException("MoralVisual");
            }
            foreach (MoraleBar bar in _moraleBars)
            {
                bar.ChangeBar(visual.BarSprite);
            }
            _mainIcon.sprite = visual.MainSprite;
            _mainIcon.enabled = isMaxNew;
        }

        private bool IsMaxMoralValue(int moralValue)
        {
            if (_moralConfig == null)
            {
                throw new NullReferenceException("MoraleConfig");
            }
            var moralType = CalculateMoralType(moralValue);
            int maxLimit = moralType == MoraleType.Positive ? _moralConfig.PositiveMoralValue : _moralConfig.NegativeMoralValue;
            
            return Math.Abs(moralValue) >= Math.Abs(maxLimit); 
        }

        private MoraleType CalculateMoralType(int moralValue)
        {
            return moralValue >= _moralConfig.StartMoralValue ? 
                MoraleType.Positive : MoraleType.Negative;
        }

        [Serializable]
        private class MoraleViewSettings
        {
            public float MoralBarChangeSpeed;
        }

        [Serializable]
		private class MoraleVisual
		{
            public MoraleType Type;
            public Sprite MainSprite;
            public Sprite BarSprite;
		}
    }
}
