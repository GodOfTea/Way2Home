using System;
using System.Collections.Generic;
using DG.Tweening;
using Enumeration;
using Game.Economy;
using UnityEngine;

namespace Game.UI
{
    public class IndicatorsView : MonoBehaviour
    {
        [SerializeField] private IndicatorAnimationSettings _animationSettings;
        [SerializeField] private IndicatorElements[] _indicatorElements;
        private IndicatorsBank _indicatorsBank;

        private void Start()
        {
            foreach (var indicator in _indicatorElements)
                indicator.SetAnimationSettings(_animationSettings);
        }

        public void UpdateIndicator(IReadOnlyDictionary<IndicatorType, IndicatorProperty> indicators)
        {
            foreach (var indicator in _indicatorElements)
                indicator.UpdateText(indicators[indicator.IndicatorType].Value);
        }
    }
    
    [Serializable]
    public class IndicatorAnimationSettings
    {
        public float AnimationDuration = 0.5f;
        public float AnimationYMove = 1.5f;
        public Color AnimationDecreaseColor = Color.black;
        public Color AnimationIncreaseColor = Color.black;
        public Ease AnimationEase = Ease.OutBack;
    }
}
