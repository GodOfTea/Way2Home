using System.Collections.Generic;
using Data;
using Game;
using Infrastructure.Services.PersistentProgreses;
using Infrastructure.Services.Settings;
using SettingsComponents;
using UnityEngine;

namespace Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressesWriters { get; } = new List<ISavedProgress>();

        private SettingsManager _settingsManager;
        private Gameplay _gameplay;

        public SettingsManager SettingsManager => _settingsManager;

        public GameFactory() { }

        public void CreateSettingsManager()
        {
            if (_settingsManager != null)
                DestroySettingsManager();
            
            _settingsManager = GameObject.Instantiate(Resources.Load<SettingsManager>(Paths.SETTINGS_MANAGER));
            RegisterProgressWatcher(_settingsManager.gameObject);
        }

        public void CreateGameplay()
        {
            if (_gameplay != null)
                DestroyGameplay();
            
            _gameplay = GameObject.Instantiate(Resources.Load<Gameplay>(Paths.GAMEPLAY));
            RegisterProgressWatcher(_gameplay.gameObject);
        }
        
        public void DestroyGameplay()
        {
            GameObject.Destroy(_gameplay.gameObject);
            _gameplay = null;
        }
        
        public void DestroySettingsManager()
        {
            GameObject.Destroy(_settingsManager.gameObject);
            _settingsManager = null;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressesWriters.Clear();
        }

        private void RegisterProgressWatcher(GameObject gameObject)
        {
            foreach (var progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressesWriters.Add(progressWriter);
            
            ProgressReaders.Add(progressReader);            
        }
    }
}