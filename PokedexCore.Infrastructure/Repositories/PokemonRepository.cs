using Microsoft.EntityFrameworkCore;
using PokedexCore.Domain.Entities;
using PokedexCore.Domain.Interfaces;
using PokedexCore.Infrastructure.Context;

namespace PokedexCore.Infrastructure.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokedexCoreDbContext _context;

        public PokemonRepository(PokedexCoreDbContext context)
        {
            _context = context;
        }

        public async Task<Pokemon?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Pokemons.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<IReadOnlyList<Pokemon>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Pokemons
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Pokemon?> GetbyIdWithTrainerAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Pokemons
                .Include(p => p.Trainer)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Pokemon>> GetPokemonByTrainerIdAsync(int trainerId, CancellationToken cancellationToken = default)
        {
            return await _context.Pokemons
                .AsNoTracking()
                .Where(p => p.TrainerId == trainerId)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Pokemon pokemon, CancellationToken cancellationToken = default)
        {
            await _context.Pokemons.AddAsync(pokemon, cancellationToken);
        }

        public void Update(Pokemon pokemon)
        {
            _context.Pokemons.Update(pokemon);
        }

        public void Delete(Pokemon pokemon)
        {
            _context.Pokemons.Remove(pokemon);
        }
    }
}