using UnityEngine;

namespace Game.LocalizationComponents
{
    public class LocalizationText : LocalizationObject
    {
        [SerializeField] private string _string;
        
        public string Text => _string;
    }
}