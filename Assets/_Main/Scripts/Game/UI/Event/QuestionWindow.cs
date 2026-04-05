using System;
using Infrastructure.Services;
using Infrastructure.Services.Audio;
using Infrastructure.Services.Localization;
using TMPro;
using UnityEngine;

namespace UI.Event
{
    public class QuestionWindow : EventWindow, ILocalizable
    {
        [SerializeField] private AudioTrack _clickOptionSound;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private OptionView[] _optionViews;

        private EventComponents.Event _event;
        private ILocalizationService _localizationService;
        
        public event Action<string> QuestionResolved;

        private void Awake()
        {
            _localizationService = AllServices.Container.Single<ILocalizationService>();
            _localizationService.AddLocalizable(this);
        }

        private void OnEnable()
        {
            foreach (var optionView in _optionViews)
                optionView.OptionChoose += OnOptionChoose;
        }

        private void OnDisable()
        {
            foreach (var optionView in _optionViews)
                optionView.OptionChoose -= OnOptionChoose;
        }

        public void UpdateView(EventComponents.Event eventData)
        {
            /* Берем из локализации по ключу */
            _event = eventData;
            SetText();

            var options = eventData.GetAnswers();
            options.Shuffle();

            for (var i = 0; i < _optionViews.Length; i++)
            {
                var optionView = _optionViews[i];
                optionView.SetAnswer(options[i]);
            }
        }

        private void OnOptionChoose(string answerId)
        {
            _clickOptionSound.PlayOneShot();
            QuestionResolved?.Invoke(answerId);
        }

        private void SetText() =>
            _descriptionText.text = _localizationService.GetLocalizationEventText(_event.ID);

        public void UpdateLocalization()
        {
            if (_event != null)
                SetText();
        }
    }
}