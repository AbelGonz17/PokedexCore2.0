using PokedexCore.Domain.Entities;
using PokedexCore.Domain.Enums;

namespace PokedexCore.Domain.Events
{
    public class TrainerRankUpdateEvent
    {
        public int Id { get; }
        public TrainerRank OldRank { get; }
        public TrainerRank NewRank { get; }

        public DateTime OccurredOn { get; }

        public TrainerRankUpdateEvent(int id, TrainerRank oldRank, TrainerRank newRank)
        {
            Id = id;
            OldRank = oldRank;
            NewRank = newRank;
            OccurredOn = DateTime.UtcNow;
        }
    }
}