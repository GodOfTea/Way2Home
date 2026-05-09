using System;
using System.Collections.Generic;
using Data.Progress;
using Enumeration;
using EventComponents;
using Game.Cnofigs;
using Game.Economy;
using Game.ResultComponents;
using Game.UI;
using Game.UI.ScreensComponents;
using Game.UI.ScreensComponents.Gameplay;
using Infrastructure.Services.PersistentProgreses;
using Main.Editor;
using UnityEngine;

namespace Game
{
    public interface IEventSwitcher
    {
        public void ShowNextRandomEvent();
    }

    public class Gameplay : MonoBehaviour, IEventSwitcher, ISavedProgress
    {
        [Header("Data")]
        [SerializeField] private EventsConfig _eventsConfig;
        [SerializeField] private Screens _screens;
        [Space]
        [SerializeField] private CheatMenu _cheatMenu;
        
        private EventView _view;
        private EventDispenser _eventDispenser;
        private ResultHandler _resultHandler;
        private IndicatorsBank _indicatorsBank;
        private UIIndicatorsController _uiIndicatorsController;
        
        private List<string> _cachedPreviousEvents;
        private string _cachedCurrentEvent;
        private bool _isNewGame;

        private void Start()
        {
            CollectData();
            InitializeEvents();
            
            _screens.Get<GeneralScreen>().Show();
            
            /* Если нет сохраненных данных */
            if (_cachedPreviousEvents is not { Count: > 0 })
                ShowIntro();
            else
                ShowMainScreen();
            
            ShowQuestion();
            
#if UNITY_EDITOR
            _cheatMenu.SetIndicatorsBank(_indicatorsBank);
#endif
        }

        private void CollectData()
        {
            var sceneData = FindAnyObjectByType<SceneData>();
            
            _view = sceneData.EventView;
            _screens = sceneData. Screens;

            if (_indicatorsBank == null)
            {
                GameConfigs gameConfigs = new GameConfigs();
                _indicatorsBank = new IndicatorsBank(gameConfigs.IndicatorsConfig, gameConfigs.MoralConfig);
            }

            _uiIndicatorsController = new UIIndicatorsController(_screens, _indicatorsBank);
        }

        private void InitializeEvents()
        {
            var databases = _eventsConfig.GetDatabases();
            _eventDispenser = null;
            _resultHandler = null;
            
            _eventDispenser = new EventDispenser(_eventsConfig, databases.Item1, _indicatorsBank, _cachedPreviousEvents, _cachedCurrentEvent);
            _resultHandler = new ResultHandler(databases.Item2, _indicatorsBank);
            _resultHandler.ResultSet += ShowResultVisual;
        }

        private void ShowIntro()
        {
            _screens.Get<IntroScreen>().Show();
            _screens.Get<IntroScreen>().IntroCompleted += ShowMainScreen;
            AudioHandler.Instance.TutorialStart.PlayMusic(0.5f);
        }

        private void ShowMainScreen()
        {
            _screens.Get<IntroScreen>().IntroCompleted -= ShowMainScreen;
            _screens.Get<MainScreen>().Show();
            AudioHandler.Instance.TutorialStart.PauseMusic(0.5f);
            AudioHandler.Instance.PlayMusic();
        }

        private void ShowQuestion()
        {
            _view.Init(this, _indicatorsBank);
            _view.ShowQuestion(_eventDispenser.CurrentEvent, true);
        }

        public void ShowNextRandomEvent()
        {
            if (_eventDispenser.HasNextEvent())
                _view.ShowQuestion(_eventDispenser.GetNextRandomEvent());
            else
                _resultHandler.SetResultByLastEvent();
        }

        private void ShowResultVisual(ResultVisualData resultVisualData)
        {
            _screens.Get<EndGameScreen>().SetVisual(resultVisualData.ResultData, resultVisualData.IsWin);
            _screens.Get<EndGameScreen>().Show();
            _screens.Get<MainScreen>().Hide();
        }

        public void UpdateProgress(GameProgress progress)
        {
            if (_resultHandler.IsResultSet)
            {
                progress.GameplayData = new GameplayData();
                progress.GameplayData.EndWithResult = true;
                return;
            }
            
            progress.GameplayData.PreviousEvents = _eventDispenser.PreviousEvents;
            progress.GameplayData.CurrentEvent = _eventDispenser.CurrentEvent.ID;

            if (_indicatorsBank == null)
                throw new NullReferenceException();
            
            progress.GameplayData.People   = _indicatorsBank.GetCurrentIndicatorValue(IndicatorType.People);
            progress.GameplayData.Supplies = _indicatorsBank.GetCurrentIndicatorValue(IndicatorType.Supplies);
            progress.GameplayData.Risk     = _indicatorsBank.GetCurrentIndicatorValue(IndicatorType.Risk);
            progress.GameplayData.Days     = _indicatorsBank.GetCurrentIndicatorValue(IndicatorType.Days);
        }

        public void LoadProgress(GameProgress progress)
        {
            _cachedPreviousEvents = progress.GameplayData.PreviousEvents;
            _cachedCurrentEvent = progress.GameplayData.CurrentEvent;
            
            _isNewGame = progress.GameplayData.EndWithResult || 
                    (_cachedPreviousEvents is not { Count: > 0 } && string.IsNullOrEmpty(_cachedCurrentEvent));
            
            _indicatorsBank = new IndicatorsBank(progress.GameplayData.GameConfigs.IndicatorsConfig, 
                progress.GameplayData.GameConfigs.MoralConfig);
            _indicatorsBank.SetIndicatorValue(IndicatorType.People, progress.GameplayData.People);
            _indicatorsBank.SetIndicatorValue(IndicatorType.Supplies, progress.GameplayData.Supplies);
            _indicatorsBank.SetIndicatorValue(IndicatorType.Risk, progress.GameplayData.Risk);
            _indicatorsBank.SetIndicatorValue(IndicatorType.Days, progress.GameplayData.Days);
            _indicatorsBank.Moral.SetNewMoralValue(progress.GameplayData.Moral);
        }
    }
}
