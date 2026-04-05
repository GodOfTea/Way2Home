using Data.Progress;
using Infrastructure.Services.PersistentProgreses;
using Infrastructure.Services.SaveLoad;

namespace Infrastructure.Services.State
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentProgressesService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressesService progressService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadMainMenuState, string>(_progressService.GameProgress.MainMenuScene);
        }

        public void Exit()
        {
            
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.GameProgress = _saveLoadService.LoadProgress() ?? CreateNewProgress();
        }

        private GameProgress CreateNewProgress()
        {
            return new GameProgress(mainMenuScene: "MainMenu");
        }
    }
}