using FluentValidation;

namespace VillasRUs.Application.Bookings.ReserveBooking;

public class ReserveBookingCommandValidator : AbstractValidator<ReserveBookingCommand>
{
    public ReserveBookingCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.VillaId).NotEmpty();
        RuleFor(c => c.StartDate).LessThan(c => c.EndDate);
    }
}