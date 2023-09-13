using VillasRUs.Domain.Abstractions;

namespace VillasRUs.Domain.Bookings.Events
{
    public sealed record BookingCreatedDomainEvent(Guid BookingId) : IDomainEvent;
}
