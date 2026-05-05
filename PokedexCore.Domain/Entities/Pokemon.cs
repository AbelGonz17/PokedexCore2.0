using PokedexCore.Domain.Enums;
using PokedexCore.Domain.Events;

namespace PokedexCore.Domain.Entities
{
    public class Pokemon : EntityBase
    {
        public string Name { get; private set; }
        public string MainType { get; private set; }
        public string Region { get; private set; }
        public int TrainerId { get; private set; }
        public DateTime CaptureDate { get; private set; }
        public bool IsShiny { get; private set; }
        public bool IsFainted { get; private set; }
        public int Level { get; private set; }
        public string SpriteUrl { get; private set; }
        public PokemonStatus Status { get; private set; }
        public Trainer Trainer { get; private set; }

        private Pokemon() { }

        private Pokemon(
            string name,
            string mainType,
            string region,
            int trainerId,
            DateTime captureDate,
            bool isShiny,
            string spriteUrl,
            int level,
            PokemonStatus status)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            MainType = mainType ?? throw new ArgumentNullException(nameof(mainType));
            Region = region ?? throw new ArgumentNullException(nameof(region));
            TrainerId = trainerId;
            CaptureDate = captureDate;
            IsShiny = isShiny;
            SpriteUrl = spriteUrl;
            Status = status;
            Level = level;
            IsFainted = false;
        }

        public static Pokemon CreatePokemon(
            string name,
            string mainType,
            string region,
            Trainer trainer,
            DateTime captureDate,
            bool isShiny,
            string spriteUrl,
            int level)
        {
            return new Pokemon(
                name,
                mainType,
                region,
                trainer.Id,
                captureDate,
                isShiny,
                spriteUrl,
                level,
                PokemonStatus.Active);
        }

        public void LevelUp()
        {
            if (Level >= 100)
                throw new InvalidOperationException("Pokemon has already reached max level.");

            Level++;
            AddDomainEvent(new PokemonLevelUpEvent(Id, Level));
        }

        public void Evolve(string newName, string newType)
        {
            if (Level < 16)
                throw new InvalidOperationException("Pokemon must be at least level 16 to evolve.");

            Name = newName;
            MainType = newType;

            AddDomainEvent(new PokemonEvolvedEvent(newName, newType));
        }

        public void EnsureCanBattle()
        {
            if (Level < 5)
                throw new InvalidOperationException("Pokemon must be at least level 5 to fight");

            if (IsFainted)
                throw new InvalidOperationException("Pokemon is weak, can not fight");

            if (TrainerId <= 0)
                throw new InvalidOperationException("Pokemon must be has a Trainer");

            AddDomainEvent(new PokemonCanBattle(Id, Name, Level));
        }
    }
}