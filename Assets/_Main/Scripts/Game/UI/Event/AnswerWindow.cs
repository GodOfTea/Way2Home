using System.Collections.Generic;
using Enumeration;
using EventComponents;
using Game;
using Game.Economy;
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

        private EventOptionResult _result;
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

        public void UpdateView(EventOptionResult result, IndicatorValue[] indicators)
        {
            _result = result;
            SetText(); 
            _eventPicture.ChangeAnswerPicture(result.ResultImage);
            _indicatorsResultView.UpdateValues(indicators);
        }

        private void SetText() => //TODO: Тут не работает локализация
            _resultText.text = _localizationService.GetLocalizationEventText(_result.ID);

        public void UpdateLocalization()
        {
            if (_result != null)
                SetText();
        }
    }
}