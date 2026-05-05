using PokedexCore.Domain.Interfaces;

namespace PokedexCore.Domain.Events
{
    public class PokemonCanBattle : IDomainEvents
    {
        public int Id { get; }
        public string Name { get; }
        public int Level { get; }

        public DateTime OccurredOn { get; }

        public PokemonCanBattle(int id, string name, int level, DateTime occurredOn)
        {
            Id = id;
            Name = name;
            Level = level;
            OccurredOn = occurredOn;
        }
    }
}