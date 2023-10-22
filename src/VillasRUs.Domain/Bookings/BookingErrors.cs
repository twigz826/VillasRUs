using VillasRUs.Domain.Abstractions;

namespace VillasRUs.Domain.Bookings
{
    public static class BookingErrors
    {
        public static Error NotFound = new("Booking.Found", "The booking with the specified id was not found");

        public static Error Overlap = new("Booking.Overlap", "The current booking overlaps with an existing booking");

        public static Error NotReserved = new("Booking.NotReserved", "The booking is still pending");

        public static Error NotConfirmed = new("Booking.NotConfirmed", "The booking is not yet confirmed");

        public static Error AlreadyStarted = new("Booking.AlreadyStarted", "The booking period has already started");
    }
}
