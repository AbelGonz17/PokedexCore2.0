namespace PokedexCore.Domain.Events
{
    public class TrainerCaughtPokemonEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}