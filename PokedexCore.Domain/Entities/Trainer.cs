using PokedexCore.Domain.Enums;
using PokedexCore.Domain.Events;

namespace PokedexCore.Domain.Entities
{
    public class Trainer : EntityBase
    {
        public string UserId { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public int PokemonCount { get; private set; }
        public TrainerRank Rank { get; private set; }
        private readonly List<Pokemon> _pokemons = new();
        public IReadOnlyList<Pokemon> Pokemons => _pokemons.AsReadOnly();

        private Trainer() { }

        public Trainer(string userName, string email, string userId)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Email = email;
            UserId = userId;
            RegistrationDate = DateTime.UtcNow;
            Rank = TrainerRank.Rookie;
            PokemonCount = 0;
        }

        public static Trainer CreateTrainer(string userName, string email, string userId)
        {
            var trainer = new Trainer(userName, email, userId);

            trainer.AddDomainEvent(new TrainerRegisteredEvent(trainer.Id, userName, email));

            return trainer;
        }

        public void CatchPokemon(Pokemon newPokemon)
        {
            ArgumentNullException.ThrowIfNull(newPokemon);

            _pokemons.Add(newPokemon);
            PokemonCount++; 

            AddDomainEvent(new TrainerCaughtPokemonEvent(Id, newPokemon.Id, newPokemon.Name));

            CheckForRankUpdate();
        }

        public void ReleasePokemon(int pokemonId)
        {
            var pokemon = _pokemons.FirstOrDefault(p => p.Id == pokemonId);

            if(pokemon == null)
                throw new InvalidOperationException("Pokemon not found in trainer's collection.");

            _pokemons.Remove(pokemon);
            PokemonCount--;

            AddDomainEvent(new PokemonReleasedEvent(Id, pokemon.Id));
        }

        private void CheckForRankUpdate()
        {
            var newRank = CalculateRank();

            if(newRank != Rank)
            {
                var oldRank = Rank;
                Rank = newRank;
                AddDomainEvent(new TrainerRankUpdateEvent(Id, oldRank, newRank));
            }
        }

        private TrainerRank CalculateRank()
        {
            return PokemonCount switch
            {
                >= 50 => TrainerRank.Master,
                >= 25 => TrainerRank.Expert,
                >= 10 => TrainerRank.Advanced,
                >= 3 => TrainerRank.Intermediate,
                _ => TrainerRank.Rookie
            };   
        }
    }
}