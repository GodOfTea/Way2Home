using Infrastructure.Services.Audio;
using UnityEngine;

namespace Game.UI.ScreensComponents.Gameplay
{
    public class GameplayMenuScreen : ScreenBase
    {
        [SerializeField] private AudioTrack _openSound;
        [SerializeField] private GameplayMenuButton[] _menuButtons;
        
        public override void Show()
        {
            base.Show();
            
            _openSound.PlayOneShot();
            foreach (var menuButton in _menuButtons)
                menuButton.Show(this);
        }

        public override void Hide()
        {
            base.Hide();
            
            foreach (var menuButton in _menuButtons)
                menuButton.Hide();
        }
    }
}