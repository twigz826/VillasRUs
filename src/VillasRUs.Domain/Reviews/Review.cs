using VillasRUs.Domain.Abstractions;
using VillasRUs.Domain.Bookings;
using VillasRUs.Domain.Reviews.Events;

namespace VillasRUs.Domain.Reviews;

public sealed class Review : Entity
{
    private Review(Guid id, Guid villaId, Guid bookingId, Guid userId, Rating rating, Comment comment, DateTime createdOnUtc)
        : base(id)
    {
        VillaId = villaId;
        BookingId = bookingId;
        UserId = userId;
        Rating = rating;
        Comment = comment;
        CreatedOnUtc = createdOnUtc;
    }

    public Guid VillaId { get; private set; }

    public Guid BookingId { get; private set; }

    public Guid UserId { get; private set; }

    public Rating Rating { get; private set; }

    public Comment Comment { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public static Result<Review> Create(Booking booking, Rating rating, Comment comment, DateTime createdOnUtc)
    {
        if (booking.Status != BookingStatus.Completed)
        {
            return Result.Failure<Review>(ReviewErrors.NotEligible);
        }

        var review = new Review(Guid.NewGuid(), booking.VillaId, booking.Id, booking.UserId, rating, comment, createdOnUtc);

        review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));

        return review;
    }
}
