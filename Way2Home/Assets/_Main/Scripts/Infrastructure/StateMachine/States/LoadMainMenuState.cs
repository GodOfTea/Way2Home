using Infrastructure.Services.Factory;
using Infrastructure.Services.PersistentProgreses;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.State
{
    public class LoadMainMenuState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressesService _progressesService;

        public LoadMainMenuState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory, IPersistentProgressesService progressesService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _progressesService = progressesService;
        }

        public void Enter(string payload)
        {
            Debug.Log($"Load {payload}");
            _sceneLoader.Load(payload, OnLoaded);
        }

        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            _gameFactory.CreateSettingsManager();
            Debug.Log(SceneManager.GetActiveScene().name);

            InformProgressReaders();
            
            _stateMachine.Enter<MainMenuState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressesService.GameProgress);
        }
    }
}