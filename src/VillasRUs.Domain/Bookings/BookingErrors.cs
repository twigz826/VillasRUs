using VillasRUs.Domain.Abstractions;

namespace VillasRUs.Domain.Bookings;

public static class BookingErrors
{
    public static Error NotFound { get; } = new("Booking.Found", "The booking with the specified id was not found");

    public static Error Overlap { get; } = new("Booking.Overlap", "The current booking overlaps with an existing booking");

    public static Error NotReserved { get; } = new("Booking.NotReserved", "The booking is still pending");

    public static Error NotConfirmed { get; } = new("Booking.NotConfirmed", "The booking is not yet confirmed");

    public static Error AlreadyStarted { get; } = new("Booking.AlreadyStarted", "The booking period has already started");
}
