using Data.Progress;

namespace Infrastructure.Services.PersistentProgreses
{
    public interface IPersistentProgressesService : IService
    {
        GameProgress GameProgress { get; set; }
    }
}