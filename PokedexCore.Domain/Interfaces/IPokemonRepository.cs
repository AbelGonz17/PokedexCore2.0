using PokedexCore.Domain.Entities;

namespace PokedexCore.Domain.Interfaces
{
    public interface IPokemonRepository
    {
            Task<Pokemon?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
            Task<IReadOnlyList<Pokemon>> GetAllAsync(CancellationToken cancellationToken = default);
            Task<Pokemon?> GetbyIdWithTrainerAsync(int id, CancellationToken cancellationToken = default);
            Task<IReadOnlyList<Pokemon>> GetPokemonByTrainerIdAsync(int trainerId, CancellationToken cancellationToken = default);
            Task AddAsync(Pokemon pokemon, CancellationToken cancellationToken = default);
            void Update(Pokemon pokemon);
            void Delete(Pokemon pokemon);
    }
}