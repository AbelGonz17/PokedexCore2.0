using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PokedexCore.Domain.Entities;
using PokedexCore.Domain.Interfaces;
using System.Reflection;

namespace PokedexCore.Infrastructure.Context
{
    public class PokedexCoreDbContext : IdentityDbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public PokedexCoreDbContext(DbContextOptions options, IMediator mediator ) : base(options)
        {
           _mediator = mediator;
        }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Trainer> Trainers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEntities = ChangeTracker
                .Entries<EntityBase>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

          var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}