using VillasRUs.Domain.Abstractions;

namespace VillasRUs.Domain.Bookings.Events
{
    public sealed record BookingCompletedDomainEvent(Guid BookingId) : IDomainEvent;
}
