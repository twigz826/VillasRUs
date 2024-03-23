using VillasRUs.Domain.Abstractions;

namespace VillasRUs.Domain.Reviews.Events;

public record ReviewCreatedDomainEvent(Guid ReviewId) : IDomainEvent;
