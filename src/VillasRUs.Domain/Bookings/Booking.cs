using VillasRUs.Domain.Abstractions;
using VillasRUs.Domain.Bookings.Events;
using VillasRUs.Domain.Shared;

namespace VillasRUs.Domain.Bookings
{
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

        public static Booking Create(Guid villaId, Guid userId, DateRange duration, DateTime utcNow, PricingDetails pricingDetails)
        {
            var booking = new Booking(Guid.NewGuid(), villaId, userId, duration, pricingDetails.PriceForPeriod,
                pricingDetails.CleaningFee, pricingDetails.AmenitiesFee, pricingDetails.TotalPrice, BookingStatus.Reserved, utcNow);

            booking.RaiseDomainEvent(new BookingCreatedDomainEvent(booking.Id));

            return booking;
        }
    }
}
