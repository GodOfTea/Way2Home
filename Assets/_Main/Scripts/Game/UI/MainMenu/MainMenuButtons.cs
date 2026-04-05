using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.MainMenu
{
    [Serializable]
    public class MainMenuButtons
    {
        [SerializeField] private MainMenuButtonView _play;
        [SerializeField] private MainMenuButtonView _continue;
        [SerializeField] private MainMenuButtonView _settings;
        [SerializeField] private MainMenuButtonView _feedback;
        [SerializeField] private MainMenuButtonView _quit;

        private List<MainMenuButtonView> _buttons;
        
        public event Action PlayClicked;
        public event Action ContinueClicked;
        public event Action SettingsClicked;
        public event Action FeedbackClicked;
        public event Action QuitClicked;

        public void Init(IMainMenuButtonVisualData visualData)
        {
            _buttons = new List<MainMenuButtonView> { _play, _continue, _settings, _feedback, _quit };
            
            foreach (var button in _buttons)
                button.SetVisualData(visualData);

            _play.Clicked += OnPlayClicked;
            _continue.Clicked += OnContinueClicked;
            _settings.Clicked += OnSettingsClicked;
            _feedback.Clicked += OnFeedbackClicked;
            _quit.Clicked += OnQuitClicked;
        }

        public void Disable()
        {
            _play.Clicked -= OnPlayClicked;
            _continue.Clicked -= OnContinueClicked;
            _settings.Clicked -= OnSettingsClicked;
            _feedback.Clicked -= OnFeedbackClicked;
            _quit.Clicked -= OnQuitClicked;
        }

        private void OnPlayClicked() => PlayClicked?.Invoke();
        private void OnContinueClicked() => ContinueClicked?.Invoke();
        private void OnSettingsClicked() => SettingsClicked?.Invoke();
        private void OnFeedbackClicked() => FeedbackClicked?.Invoke();
        private void OnQuitClicked() => QuitClicked?.Invoke();
    }
}