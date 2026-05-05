namespace PokedexCore.Domain.Events
{
    public class PokemonReleasedEvent
    {
        public int Id { get; }
        public int PokemonId { get; }

        public DateTime OccurredOn { get; }

        public PokemonReleasedEvent(int pokemonId, int trainerId)
        {
            Id = trainerId;
            PokemonId = pokemonId;
            OccurredOn = DateTime.UtcNow;
        }
    }
}