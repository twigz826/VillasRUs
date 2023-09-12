﻿namespace VillasRUs.Domain.Abstractions
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        protected Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; init; }

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents;
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
