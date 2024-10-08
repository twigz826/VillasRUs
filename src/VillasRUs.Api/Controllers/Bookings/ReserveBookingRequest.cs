namespace VillasRUs.Api.Controllers.Bookings;

public sealed record ReserveBookingRequest(Guid VillaId, Guid UserId, DateOnly StartDate, DateOnly EndDate);
