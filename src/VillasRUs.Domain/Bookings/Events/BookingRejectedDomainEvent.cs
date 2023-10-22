using VillasRUs.Domain.Abstractions;

namespace VillasRUs.Domain.Bookings.Events
{
    public sealed record BookingRejectedDomainEvent(Guid BookingId) : IDomainEvent;
}
