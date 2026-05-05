using PokedexCore.Domain.Interfaces;

namespace PokedexCore.Domain.Events
{
    public class PokemonEvolvedEvent : IDomainEvents
    {
       public string Name { get; }
       public string MainType { get; }

       public DateTime OccurredOn { get; }

         public PokemonEvolvedEvent(string name, string mainType)
         {
              Name = name;
              MainType = mainType;
              OccurredOn = DateTime.UtcNow;
        }
    }
}