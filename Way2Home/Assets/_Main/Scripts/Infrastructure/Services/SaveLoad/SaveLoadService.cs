using Data.Progress;
using Data.Scripts.Extension;
using Infrastructure.Services.Factory;
using Infrastructure.Services.PersistentProgreses;
using UnityEngine;

namespace Infrastructure.Services.SaveLoad
{
    class SaveLoadService : ISaveLoadService
    {
        private const string GameProgressKey = "GameProgress";
        
        private readonly IPersistentProgressesService _progressesService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressesService progressesService, IGameFactory gameFactory)
        {
            _progressesService = progressesService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressesWriter in _gameFactory.ProgressesWriters)
                progressesWriter.UpdateProgress(_progressesService.GameProgress);

            var json = _progressesService.GameProgress.ToJson();
            PlayerPrefs.SetString(GameProgressKey, json);
            
            Debug.Log("Saved");
        }

        public GameProgress LoadProgress()
        {
            return PlayerPrefs.GetString(GameProgressKey)?.ToDeserialized<GameProgress>();
        }
    }
}