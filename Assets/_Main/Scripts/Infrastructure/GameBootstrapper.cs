using Infrastructure.Services;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.State;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game(this);
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }

        private void OnApplicationQuit()
        {
            AllServices.Container.Single<ISaveLoadService>().SaveProgress();   
            AudioHandler.Instance.StopSounds();
        }
    }
}