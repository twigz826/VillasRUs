using VillasRUs.Domain.Villas;

namespace VillasRUs.Domain.Bookings
{
    public interface IBookingRepository
    {
        Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<bool> IsOverlappingAsync(
            Villa villa,
            DateRange duration,
            CancellationToken cancellationToken = default);

        void Add(Booking booking);
    }
}
