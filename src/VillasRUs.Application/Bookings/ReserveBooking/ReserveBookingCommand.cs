using VillasRUs.Application.Abstractions.Messaging;

namespace VillasRUs.Application.Bookings.ReserveBooking;

public record ReserveBookingCommand(Guid VillaId, Guid UserId, DateOnly StartDate, DateOnly EndDate) : ICommand<Guid>;