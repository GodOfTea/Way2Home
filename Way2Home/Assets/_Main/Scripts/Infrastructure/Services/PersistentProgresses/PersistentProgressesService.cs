using Data.Progress;

namespace Infrastructure.Services.PersistentProgreses
{
    public class PersistentProgressesService : IPersistentProgressesService
    {
        public GameProgress GameProgress { get; set; }
    }
}