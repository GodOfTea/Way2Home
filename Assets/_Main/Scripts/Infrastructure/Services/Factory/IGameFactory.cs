using System.Collections.Generic;
using Infrastructure.Services.PersistentProgreses;
using SettingsComponents;

namespace Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressesWriters { get; }
        SettingsManager SettingsManager { get; }
        void Cleanup();
        void CreateSettingsManager();
        void CreateGameplay();
        void DestroyGameplay();
        void DestroySettingsManager();
    }
}