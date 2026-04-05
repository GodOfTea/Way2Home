using System;
using Data;
using Editor.GoogleDataImporter;
using Infrastructure.Services;
using Infrastructure.Services.Audio;
using Infrastructure.Services.Localization;
using Infrastructure.Services.PersistentProgreses;
using Infrastructure.Services.State;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.ScreensComponents.Gameplay
{
    public class EndGameScreen : ScreenBase, ILocalizable
    {
        [Header("Visual")]
        [SerializeField] private GameObject _winResult;
        [SerializeField] private GameObject _loseResult;
        [Space]
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _imagePlace;

        [Header("Buttons")]
        [SerializeField] private Button _exitButton;

        private ResultSheet.Reference _resultData;
        private ILocalizationService _localizationService;

        public event Action ExitButtonPressed;
        
        private void Awake()
        {
            _localizationService = AllServices.Container.Single<ILocalizationService>();
            _localizationService.AddLocalizable(this);
        }
        
        public void SetVisual(ResultSheet.Reference resultData, bool isWin)
        {
            Debug.Log("1. Result data: " + _resultData);
            _resultData = resultData;
            
            Debug.Log("_winResult is null? " + (_winResult == null));
            
            _winResult.SetActive(isWin);
            _loseResult.SetActive(!isWin);

            AudioHandler.Instance.PlayEndGameMusic(isWin);
        }
        
        public override void Show()
        {
            Debug.Log("2. Result data: " + _resultData);
            SetText();
            _imagePlace.sprite = Resources.Load<Sprite>(Paths.EVENT_IMAGES + _resultData.Ref.ImagePath); 
            
            base.Show();
            _exitButton.onClick.AddListener(ReturnToMainMenu);
        }

        public override void Hide()
        {
            base.Hide();
            _exitButton.onClick.RemoveListener(ReturnToMainMenu);
        }

        private void ReturnToMainMenu()
        {
            var progressesService = AllServices.Container.Single<IPersistentProgressesService>();
            AllServices.Container.Single<IStateSwitcher>().Enter<LoadMainMenuState, string>(
                progressesService.GameProgress.MainMenuScene);
        }

        private void SetText() =>
            _description.text = _localizationService.GetLocalizationEventText(_resultData.Id);

        public void UpdateLocalization()
        {
            if (_resultData != null)
                SetText();
        }
    }
}