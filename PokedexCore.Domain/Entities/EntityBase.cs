using PokedexCore.Domain.Interfaces;

namespace PokedexCore.Domain.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; protected set; }

        private readonly List<IDomainEvents> _domainEvents = new();

        public IReadOnlyList<IDomainEvents> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvents eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}