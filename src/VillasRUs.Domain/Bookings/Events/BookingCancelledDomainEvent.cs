using VillasRUs.Domain.Abstractions;

namespace VillasRUs.Domain.Bookings.Events;

public sealed record BookingCancelledDomainEvent(Guid BookingId) : IDomainEvent;
