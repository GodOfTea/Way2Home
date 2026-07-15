using System;
using DG.Tweening;
using Enumeration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [Serializable]
    public class IndicatorElements
    {
        public string Name;
        public IndicatorType IndicatorType;
        public RectTransform TextFieldRect;
        public TMP_Text TextField;

        private int _currentValue = -1;
        private IndicatorAnimationSettings _animationSettings;

        public void SetAnimationSettings(IndicatorAnimationSettings animationSettings)
        {
            _animationSettings = animationSettings;
        }
        
        public virtual void UpdateText(int value)
        {
            if (_currentValue == -1)
            {
                _currentValue = value;
            }
            else if (value > _currentValue)
            {
                IncreaseTextAnimation();
            }
            else if (value < _currentValue)
            {
                DecreaseTextAnimation();
            }

            TextField.text = value.ToString();
            _currentValue = value;
        }
        
        private void DecreaseTextAnimation(bool isForce = false)
        {
            if (isForce == false)
            {
                if (IndicatorType is IndicatorType.Risk or IndicatorType.Days)
                {
                    IncreaseTextAnimation(true);
                    return;
                }
            }

            var sequence = DOTween.Sequence();
            sequence.Append(TextField.
                DOColor(_animationSettings.AnimationDecreaseColor, _animationSettings.AnimationDuration));
            sequence.Join(TextFieldRect.
                DOLocalMoveY(-_animationSettings.AnimationYMove, _animationSettings.AnimationDuration).
                SetEase(_animationSettings.AnimationEase));
            sequence.SetLoops(2, LoopType.Yoyo);
        }
        
        private void IncreaseTextAnimation(bool isForce = false)
        {
            if (isForce == false)
            {
                if (IndicatorType is IndicatorType.Risk or IndicatorType.Days)
                {
                    DecreaseTextAnimation(true);
                    return;
                }
            }

            var sequence = DOTween.Sequence();
            sequence.Append(TextField.
                DOColor(_animationSettings.AnimationIncreaseColor, _animationSettings.AnimationDuration));
            sequence.Join(TextFieldRect.
                DOLocalMoveY(_animationSettings.AnimationYMove, _animationSettings.AnimationDuration).
                SetEase(_animationSettings.AnimationEase));
            sequence.SetLoops(2, LoopType.Yoyo);
        }
    }
}