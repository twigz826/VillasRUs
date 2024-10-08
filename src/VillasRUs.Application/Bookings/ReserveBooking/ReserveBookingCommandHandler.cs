using VillasRUs.Application.Abstractions.Clock;
using VillasRUs.Application.Abstractions.Messaging;
using VillasRUs.Application.Exceptions;
using VillasRUs.Domain.Abstractions;
using VillasRUs.Domain.Bookings;
using VillasRUs.Domain.Users;
using VillasRUs.Domain.Villas;

namespace VillasRUs.Application.Bookings.ReserveBooking;

internal sealed class ReserveBookingCommandHandler : ICommandHandler<ReserveBookingCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IVillaRepository _villaRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly PricingService _pricingService;
    private readonly IDatetimeProvider _dateTimeProvider;

    public ReserveBookingCommandHandler(IUserRepository userRepository, IVillaRepository villaRepository, IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork, PricingService pricingService, IDatetimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _villaRepository = villaRepository;
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
        _pricingService = pricingService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var villa = await _villaRepository.GetByIdAsync(request.VillaId, cancellationToken);
        if (villa is null)
        {
            return Result.Failure<Guid>(VillaErrors.NotFound);
        }

        var duration = DateRange.Create(request.StartDate, request.EndDate);

        if (await _bookingRepository.IsOverlappingAsync(villa, duration, cancellationToken))
        {
            return Result.Failure<Guid>(BookingErrors.Overlap);
        }

        try
        {
            var booking = Booking.Reserve(villa, user.Id, duration, _dateTimeProvider.UtcNow, _pricingService);
            _bookingRepository.Add(booking);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return booking.Id;
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(BookingErrors.Overlap);
        }
    }
}