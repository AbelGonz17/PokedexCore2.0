namespace PokedexCore.Domain.Events
{
    public class PokemonLevelUpEvent
    {
        public int PokemonId { get; }
        public int NewLevel { get; }

        public DateTime OccurredOn { get; }

        public PokemonLevelUpEvent(int pokemonId, int newLevel)
        {
            PokemonId = pokemonId;
            NewLevel = newLevel;
            OccurredOn = DateTime.UtcNow;
        }
    }
}