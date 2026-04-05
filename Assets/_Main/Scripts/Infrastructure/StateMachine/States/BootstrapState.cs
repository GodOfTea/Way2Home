using Infrastructure.Services.Audio;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Localization;
using Infrastructure.Services.Settings;
using Infrastructure.Services.PersistentProgreses;
using Infrastructure.Services.SaveLoad;

namespace Infrastructure.Services.State
{
    public class BootstrapState : IState
    {
        private const string BootstrapSceneName = "BootstrapScene";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(BootstrapSceneName, onLoaded: LoadProgress);
        }

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IStateSwitcher>(_stateMachine);
            _services.RegisterSingle<ISettings>(new SettingsService());
            _services.RegisterSingle<ILocalizationService>(new LocalizationService(_services.Single<ISettings>()));
            _services.RegisterSingle<IGameFactory>(new GameFactory());
            _services.RegisterSingle<IPersistentProgressesService>(new PersistentProgressesService());
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                _services.Single<IPersistentProgressesService>(), _services.Single<IGameFactory>()));
            _services.RegisterSingle<IAudioService>(new AudioService(_services.Single<ISettings>()));
        }

        private void LoadProgress()
        {
            _stateMachine.Enter<LoadProgressState>();
        }
    }
}