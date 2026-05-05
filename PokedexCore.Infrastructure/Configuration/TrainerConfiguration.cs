using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokedexCore.Domain.Entities;

namespace PokedexCore.Infrastructure.Configuration
{
    public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.ToTable("Trainers");

            builder.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(t => t.Pokemons)
                .WithOne(p => p.Trainer)
                .HasForeignKey(p => p.TrainerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}