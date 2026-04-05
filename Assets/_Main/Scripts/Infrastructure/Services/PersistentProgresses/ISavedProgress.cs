using Data.Progress;

namespace Infrastructure.Services.PersistentProgreses
{
    public interface ISavedProgressReader
    {
        void LoadProgress(GameProgress progress);
    }

    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(GameProgress progress);
    }
}