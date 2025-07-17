using UnityEngine;

namespace Game.UI.ScreensComponents.Gameplay
{
    public class GeneralScreen : ScreenBase
    {
        [SerializeField] private GameplayMenuScreen _gameplayMenu;
        [SerializeField] private GameplayOpenMenuButton _openMenuButton;

        public override void Show()
        {
            base.Show();
            _openMenuButton.Show();

            _openMenuButton.OpenMenuPressed += ShowMainMenu;
        }

        public override void Hide()
        {
            base.Hide();
            _openMenuButton.Hide();
            
            _openMenuButton.OpenMenuPressed -= ShowMainMenu;
        }

        private void ShowMainMenu() =>
            _gameplayMenu.Show();
    }
}