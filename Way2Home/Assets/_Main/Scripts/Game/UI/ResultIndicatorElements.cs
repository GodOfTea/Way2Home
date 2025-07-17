using System;
using UnityEngine;

namespace Game.UI
{
    [Serializable]
    public class ResultIndicatorElements : IndicatorElements
    {
        public GameObject IconContainer;

        public override void UpdateText(int value)
        {
            char modi = value > 0 ? '+' : '-';
            string valueText = $"{modi}{Math.Abs(value)}";
            TextField.text = valueText;
        }

        public void Enable() => IconContainer.SetActive(true);
        public void Disable() => IconContainer.SetActive(false);
    }
}