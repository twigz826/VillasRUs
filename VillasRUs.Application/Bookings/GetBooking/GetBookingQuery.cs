using VillasRUs.Application.Abstractions.Messaging;

namespace VillasRUs.Application.Bookings.GetBooking;

public sealed record GetBookingQuery(Guid BookingId) : IQuery<BookingResponse>;