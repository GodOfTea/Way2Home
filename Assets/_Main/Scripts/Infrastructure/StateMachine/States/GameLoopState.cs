using Infrastructure.Services.Factory;
using Infrastructure.Services.PersistentProgreses;
using Infrastructure.Services.Settings;

namespace Infrastructure.Services.State
{
    public class GameLoopState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressesService _progressesService;
        private readonly GameStateMachine _stateMachine;
        private ISettings _settings;

        public GameLoopState(GameStateMachine stateMachine, IGameFactory gameFactory, 
            IPersistentProgressesService progressesService, ISettings settings)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _progressesService = progressesService;
            _settings = settings;
        }

        public void Enter()
        {
            _settings.UpdateLanguage();
        }

        public void Exit()
        {
            AudioHandler.Instance.StopSounds();
            foreach (ISavedProgress writer in _gameFactory.ProgressesWriters)
                writer.UpdateProgress(_progressesService.GameProgress);
            _gameFactory.DestroyGameplay();
        }
    }
}