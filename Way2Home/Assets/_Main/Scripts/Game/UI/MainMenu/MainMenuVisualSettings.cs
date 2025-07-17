using System;
using UnityEngine;

namespace Game.UI.MainMenu
{
    [Serializable]
    public class MainMenuVisualSettings : IMainMenuButtonVisualData
    {
        [Header("Buttons")]
        [SerializeField] private float _selectedTextSize;
        [SerializeField] private float _defaultTextSize;
        [Space]
        [SerializeField] private Color _selectedTextColor;
        [SerializeField] private Color _defaultTextColor;
        [Space]
        [SerializeField] private float _transitionTime;

        #region Fields

        public float SelectedTextSize => _selectedTextSize;
        public float DefaultTextSize => _defaultTextSize;

        public Color SelectedTextColor => _selectedTextColor;
        public Color DefaultTextColor => _defaultTextColor;

        public float TransitionTime => _transitionTime;

        #endregion
    }

    public interface IMainMenuButtonVisualData
    {
        float SelectedTextSize { get; }
        float DefaultTextSize { get; }
        
        Color SelectedTextColor { get; }
        Color DefaultTextColor { get; }
        
        float TransitionTime { get; }
    }
}