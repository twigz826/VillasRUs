using VillasRUs.Domain.Abstractions;
using VillasRUs.Domain.Bookings.Events;
using VillasRUs.Domain.Shared;
using VillasRUs.Domain.Villas;

namespace VillasRUs.Domain.Bookings;

public sealed class Booking : Entity
{
    private Booking(Guid id, Guid villaId, Guid userId, DateRange duration, Money priceForPeriod, Money cleaningFee,
        Money amenitiesFee, Money totalPrice, BookingStatus status, DateTime createdOnUtc) : base(id)
    {
        VillaId = villaId;
        UserId = userId;
        Duration = duration;
        PriceForPeriod = priceForPeriod;
        CleaningFee = cleaningFee;
        AmenitiesFee = amenitiesFee;
        TotalPrice = totalPrice;
        Status = status;
        CreatedOnUtc = createdOnUtc;
    }

    public Guid VillaId { get; private set; }

    public Guid UserId { get; private set; }

    public DateRange Duration { get; private set; }

    public Money PriceForPeriod { get; private set; }

    public Money CleaningFee { get; private set; }

    public Money AmenitiesFee { get; private set; }

    public Money TotalPrice { get; private set; }

    public BookingStatus Status { get; private set; } = new();

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? ConfirmedOnUtc { get; private set; }

    public DateTime? RejectedOnUtc { get; private set; }

    public DateTime? CompletedOnUtc { get; private set; }

    public DateTime? CancelledOnUtc { get; private set; }

    public static Booking Reserve(Villa villa, Guid userId, DateRange duration, DateTime utcNow, PricingService pricingService)
    {
        var pricingDetails = pricingService.CalculatePrice(villa, duration);

        var booking = new Booking(Guid.NewGuid(), villa.Id, userId, duration, pricingDetails.PriceForPeriod,
            pricingDetails.CleaningFee, pricingDetails.AmenitiesFee, pricingDetails.TotalPrice, BookingStatus.Reserved, utcNow);

        booking.RaiseDomainEvent(new BookingReservedDomainEvent(booking.Id));

        villa.LastBookedOnUtc = utcNow;

        return booking;
    }

    public Result Confirm(DateTime utcNow)
    {
        if (Status != BookingStatus.Reserved)
        {
            return Result.Failure(BookingErrors.NotReserved);
        }

        Status = BookingStatus.Confirmed;
        ConfirmedOnUtc = utcNow;

        RaiseDomainEvent(new BookingConfirmedDomainEvent(Id));

        return Result.Success();
    }

    public Result Reject(DateTime utcNow)
    {
        if (Status != BookingStatus.Reserved)
        {
            return Result.Failure(BookingErrors.NotReserved);
        }

        Status = BookingStatus.Rejected;
        RejectedOnUtc = utcNow;

        RaiseDomainEvent(new BookingRejectedDomainEvent(Id));

        return Result.Success();
    }

    public Result Complete(DateTime utcNow)
    {
        if (Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.NotConfirmed);
        }

        Status = BookingStatus.Completed;
        CompletedOnUtc = utcNow;

        RaiseDomainEvent(new BookingCompletedDomainEvent(Id));

        return Result.Success();
    }

    public Result Cancel(DateTime utcNow)
    {
        if (Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.NotConfirmed);
        }

        var currentDate = DateOnly.FromDateTime(utcNow);

        if (currentDate > Duration.Start)
        {
            return Result.Failure(BookingErrors.AlreadyStarted);
        }

        Status = BookingStatus.Cancelled;
        CancelledOnUtc = utcNow;

        RaiseDomainEvent(new BookingCancelledDomainEvent(Id));

        return Result.Success();
    }
}
