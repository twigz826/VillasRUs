using VillasRUs.Domain.Shared;

namespace VillasRUs.Domain.Bookings
{
    public record PricingDetails(Money PriceForPeriod, Money CleaningFee, Money AmenitiesFee, Money TotalPrice);
}
