using Microsoft.EntityFrameworkCore;
using PokedexCore.Domain.Entities;
using PokedexCore.Domain.Interfaces;
using PokedexCore.Infrastructure.Context;

namespace PokedexCore.Infrastructure.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly PokedexCoreDbContext _context;

        public TrainerRepository(PokedexCoreDbContext context)
        {
            _context = context;
        }   

        public async Task<Trainer?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Trainers.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<IReadOnlyList<Trainer>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Trainers
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Trainer?> GetByIdWithPokemonAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Trainers
                .Include(t => t.Pokemons)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<bool> EmailExistAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Trainers
                .AnyAsync(t => t.Email == email, cancellationToken);
        }

        public async Task AddAsync(Trainer trainer, CancellationToken cancellationToken = default)
        {
            await _context.Trainers.AddAsync(trainer,cancellationToken);
        }

        public void Update(Trainer trainer)
        {
            _context.Trainers.Update(trainer);
        }

        public void Delete(Trainer trainer)
        {
            _context.Trainers.Remove(trainer);
        }
    }
}