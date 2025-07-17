using System;
using EventComponents;
using Game;
using Game.UI.Animations;
using Infrastructure.Services;
using Infrastructure.Services.Audio;
using Infrastructure.Services.Localization;
using TMPro;
using UnityEngine;

namespace UI.Event
{
    public class AnswerWindow : EventWindow, ILocalizable
    {
        [SerializeField] private AudioTrack _buttonClickSound;
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private IndicatorsResultView _indicatorsResultView;
        [SerializeField] private ContinueButton _continueButton;

        private IAnswerResult _result;
        private EventPicture _eventPicture;
        private ILocalizationService _localizationService;
        private IEventSwitcher _eventSwitcher;

        private void Awake()
        {
            _localizationService = AllServices.Container.Single<ILocalizationService>();
            _localizationService.AddLocalizable(this);
        }

        public void Init(IEventSwitcher eventSwitcher, EventPicture imagePoint)
        {
            _eventSwitcher = eventSwitcher;
            _eventPicture = imagePoint;
        }
        
        private void OnEnable()
        {
            _continueButton.Clicked += SetNextEvent;
        }

        private void OnDisable()
        {
            _continueButton.Clicked -= SetNextEvent;
        }

        private void SetNextEvent()
        {
            _buttonClickSound.PlayOneShot();
            _eventSwitcher.ShowNextRandomEvent();
        }

        public void UpdateView(IAnswerResult result)
        {
            _result = result;
            SetText(); 
            _eventPicture.ChangeAnswerPicture(result.AnswerImage);
            _indicatorsResultView.UpdateValues(result.IndicatorsResultMap);
        }

        private void SetText() =>
            _resultText.text = _localizationService.GetLocalizationEventText(_result.ResultAnswerId);

        public void UpdateLocalization()
        {
            if (_result != null)
                SetText();
        }
    }
}