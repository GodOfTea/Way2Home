
using Infrastructure.Services.Factory;
using Infrastructure.Services.PersistentProgreses;

namespace Infrastructure.Services.State
{
    public class LoadGameState : IPayloadedState<string, bool>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly SceneLoader _sceneLoader;
        private readonly IPersistentProgressesService _progressesService;

        private bool _startNewGame;
        
        public LoadGameState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory, IPersistentProgressesService progressesService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _progressesService = progressesService;
        }

        public void Enter(string sceneName, bool isNewGame)
        {
            _startNewGame = isNewGame;
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            _gameFactory.CreateGameplay();
            
            if (_startNewGame == false)
                InformProgressReaders();
            
            _stateMachine.Enter<GameLoopState>();
        }
        
        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressesService.GameProgress);
        }
    }
}