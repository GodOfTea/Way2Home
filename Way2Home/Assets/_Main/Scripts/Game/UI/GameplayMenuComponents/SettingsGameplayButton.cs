using UnityEngine;

namespace Game
{
    public class SettingsGameplayButton : GameplayMenuButton
    {
        [SerializeField] private ScreenBase _settingsScreen;
        
        protected override void OnClicked()
        {
            base.OnClicked();
            _menuScreen.Hide();
            _settingsScreen.Show();
        }
    }
}