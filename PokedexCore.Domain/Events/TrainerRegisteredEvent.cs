using PokedexCore.Domain.Interfaces;

namespace PokedexCore.Domain.Events
{
    public class TrainerRegisteredEvent : IDomainEvents
    {
        public int Id { get; }
        public string UserName { get; }
        public string Email { get; }

        public DateTime OccurredOn { get; }

        public TrainerRegisteredEvent(int id, string userName, string email)
        {
            Id = id;
            UserName = userName;
            Email = email;
            OccurredOn = DateTime.UtcNow;
        }
    }
}