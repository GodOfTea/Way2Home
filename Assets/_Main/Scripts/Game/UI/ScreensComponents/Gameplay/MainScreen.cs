using System;
using UnityEngine;

namespace Game.UI.ScreensComponents.Gameplay
{
    public class MainScreen : ScreenBase
    {
        [SerializeField] private IndicatorsView _indicatorsView;
        [SerializeField] private EventView _eventView;
        [SerializeField] private SpriteRenderer _imagePoint;

        public IndicatorsView IndicatorsView => _indicatorsView;

        public event Action MainScreenShowed;

        public override void Show()
        {
            MainScreenShowed?.Invoke();
            base.Show();
        }
    }
}
