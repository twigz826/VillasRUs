using VillasRUs.Domain.Abstractions;

namespace VillasRUs.Domain.Users.Events
{
    public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
}
