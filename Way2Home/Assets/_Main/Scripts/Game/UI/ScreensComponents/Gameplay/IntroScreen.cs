using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.ScreensComponents.Gameplay
{
    public class IntroScreen : ScreenBase
    {
        [SerializeField] private Tutorial _tutorial;
        [SerializeField] private NextButton _nextButton;
        [SerializeField] private NextButton _startButton;

        public event Action IntroCompleted;

        public override void Show()
        {
            _startButton.Disable();
            _nextButton.Enable();
            _tutorial.Enable();
            
            _nextButton.Pressed += ShowTutorial;
         
            base.Show();
        }

        private void ShowTutorial()
        {
            _nextButton.Pressed -= ShowTutorial;
            _startButton.Pressed += StartGameplay;
            
            _tutorial.SwitchToTutorial();
            
            _nextButton.Disable();
            _startButton.Enable();
        }

        private void StartGameplay()
        {
            _startButton.Pressed -= StartGameplay;
            
            IntroCompleted?.Invoke();
            _nextButton.Disable();
            Hide();
        }

        [Serializable]
        private class Tutorial
        {
            [SerializeField] private CanvasGroup _intro;
            [SerializeField] private CanvasGroup _tutorial;

            public void Enable()
            {
                _intro.alpha = 1f;
                _tutorial.alpha = 0f;
            }

            public void SwitchToTutorial()
            {
                DOVirtual.Float(1f, 0f, 0.3f, (value) => _intro.alpha = value);
                DOVirtual.Float(0f, 1f, 0.3f, (value) => _tutorial.alpha = value);
            }
        }
        
        [Serializable]
        private class NextButton
        {
            [SerializeField] private Button _button;

            public event Action Pressed;

            public void Enable()
            {
                _button.gameObject.SetActive(true);
                _button.onClick.AddListener(SendEvent);                
            }

            public void Disable()
            {
                _button.gameObject.SetActive(false);
                _button.onClick.RemoveListener(SendEvent);
            }

            private void SendEvent() => Pressed?.Invoke();
        }
    }
}