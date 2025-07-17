using Game.UI.ScreensComponents;
using Game.UI.ScreensComponents.MainMenu;
using Infrastructure.Services;
using Infrastructure.Services.Audio;
using Infrastructure.Services.State;
using UnityEngine;

namespace Game.UI.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Screens _screens;

        [Space] 
        [SerializeField] private AudioTrack _backgroundMusic;
        [SerializeField] private AudioTrack _clickSound;
        [SerializeField] private AudioTrack _playClickSound;
        
        [Space]
        [SerializeField] private MainMenuVisualSettings _visualSettings;
        [SerializeField] private MainMenuButtons _buttons;

        private void Awake() =>
            _buttons.Init(_visualSettings);

        private void Start()
        {
            _screens.Get<MainScreen>().ShowImmediate();
            _backgroundMusic.PlayMusic(0.5f);
        }

        private void OnDestroy() =>
            _buttons.Disable();

        private void OnEnable()
        {
            _buttons.PlayClicked += StartNewGame;
            _buttons.ContinueClicked += LoadGame;
            _buttons.SettingsClicked += EnableSettings;
            _buttons.QuitClicked += QuitGame;
        }

        private void OnDisable()
        {
            _buttons.PlayClicked -= StartNewGame;
            _buttons.ContinueClicked -= LoadGame;
            _buttons.SettingsClicked -= EnableSettings;
            _buttons.QuitClicked -= QuitGame;
        }

        private void StartNewGame()
        {
            _playClickSound.PlayOneShot();
            AllServices.Container.Single<IStateSwitcher>().
                Enter<LoadGameState, string, bool>("Main", true);
            _backgroundMusic.PauseMusic();
        }

        private void LoadGame()
        {
            _playClickSound.PlayOneShot();
            AllServices.Container.Single<IStateSwitcher>().
                Enter<LoadGameState, string, bool>("Main", false);
            _backgroundMusic.PauseMusic();
        }

        private void EnableSettings()
        {
            _clickSound.PlayOneShot();
            _screens.Get<MainScreen>().HideImmediate();
            _screens.Get<SettingsScreen>().Show();   
        }

        private void QuitGame()
        {            
            _clickSound.PlayOneShot();
            _backgroundMusic.PauseMusic();
            AllServices.Container.Single<IStateSwitcher>().Enter<GameExitState>();
        }
    }
}