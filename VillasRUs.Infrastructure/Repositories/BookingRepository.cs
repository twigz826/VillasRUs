using Microsoft.EntityFrameworkCore;
using VillasRUs.Domain.Bookings;
using VillasRUs.Domain.Villas;

namespace VillasRUs.Infrastructure.Repositories;

internal sealed class BookingRepository : Repository<Booking>, IBookingRepository
{
    private static readonly BookingStatus[] ActiveBookingStatuses =
    {
        BookingStatus.Reserved,
        BookingStatus.Confirmed,
        BookingStatus.Completed
    };

    public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsOverlappingAsync(Villa villa, DateRange duration, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Booking>()
            .AnyAsync(booking =>
                booking.VillaId == villa.Id &&
                booking.Duration.Start <= duration.End &&
                booking.Duration.End >= duration.Start &&
                ActiveBookingStatuses.Contains(booking.Status),
                cancellationToken);
    }
}
