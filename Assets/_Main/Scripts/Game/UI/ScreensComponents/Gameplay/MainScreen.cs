using System;
using UI.Event;
using UnityEngine;

namespace Game.UI.ScreensComponents.Gameplay
{
    public class MainScreen : ScreenBase
    {
        [SerializeField] private IndicatorsView _indicatorsView;
        [SerializeField] private MoralView _moralView;
        [SerializeField] private EventView _eventView;
        [SerializeField] private SpriteRenderer _imagePoint;

        public IndicatorsView IndicatorsView => _indicatorsView;
        public MoralView MoralView => _moralView;

        public event Action MainScreenShowed;

        public override void Show()
        {
            MainScreenShowed?.Invoke();
            base.Show();
        }
    }
}
