using MediatR;
using VillasRUs.Application.Abstractions.Email;
using VillasRUs.Domain.Bookings;
using VillasRUs.Domain.Bookings.Events;
using VillasRUs.Domain.Users;

namespace VillasRUs.Application.Bookings.ReserveBooking;

internal sealed class BookingReservedDomainEventHandler : INotificationHandler<BookingReservedDomainEvent>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public BookingReservedDomainEventHandler(IEmailService emailService, IUserRepository userRepository, IBookingRepository bookingRepository)
    {
        _emailService = emailService;
        _userRepository = userRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(notification.BookingId, cancellationToken);
        if (booking is null)
        {
            return;
        }

        var user = await _userRepository.GetByIdAsync(booking.UserId, cancellationToken);
        if (user is null)
        {
            return;
        }

        await _emailService.SendAsync(user.Email, "Booking reserved!", "You have 10 minutes to confirm this booking");
    }
}