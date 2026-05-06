using PokedexCore.Domain.Entities;

namespace PokedexCore.Domain.Interfaces
{
    public interface ITrainerRepository
    {
        Task<Trainer?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Trainer>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Trainer?> GetByIdWithPokemonAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> EmailExistAsync(string email, CancellationToken cancellationToken = default);
        Task AddAsync(Trainer trainer, CancellationToken cancellationToken = default);
        void Update(Trainer trainer);
        void Delete(Trainer trainer);
    }
}