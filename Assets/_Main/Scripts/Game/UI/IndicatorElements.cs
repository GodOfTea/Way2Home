using System;
using Enumeration;
using TMPro;

namespace Game.UI
{
    [Serializable]
    public class IndicatorElements
    {
        public string Name;
        public IndicatorType IndicatorType;
        public TMP_Text TextField;

        public virtual void UpdateText(int value)
        {
            TextField.text = value.ToString();
        }
    }
}