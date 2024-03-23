using VillasRUs.Domain.Abstractions;

namespace VillasRUs.Domain.Bookings.Events;

public sealed record BookingConfirmedDomainEvent(Guid BookingId) : IDomainEvent;
