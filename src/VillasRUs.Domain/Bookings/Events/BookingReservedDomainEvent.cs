using VillasRUs.Domain.Abstractions;

namespace VillasRUs.Domain.Bookings.Events
{
    public sealed record BookingReservedDomainEvent(Guid BookingId) : IDomainEvent;
}
