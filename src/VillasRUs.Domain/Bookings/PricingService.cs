using VillasRUs.Domain.Shared;
using VillasRUs.Domain.Villas;

namespace VillasRUs.Domain.Bookings;

public class PricingService
{
    public PricingDetails CalculatePrice(Villa villa, DateRange period)
    {
        var currency = villa.Price.Currency;

        var priceForPeriod = new Money(villa.Price.Amount * period.LengthInDays, currency);

        decimal percentageFee = 0;
        foreach (var amenity in villa.Amenities)
        {
            percentageFee += amenity switch
            {
                Amenity.Garden or Amenity.Gym or Amenity.MountainView => 0.04m,
                Amenity.SwimmingPool or Amenity.Heating => 0.02m,
                Amenity.Terrace or Amenity.Parking => 0.01m,
                _ => 0
            };
        }

        var amenitiesFee = Money.Zero(currency);

        if (percentageFee > 0)
        {
            amenitiesFee = new Money(priceForPeriod.Amount * percentageFee, currency);
        }

        var totalPrice = Money.Zero();

        totalPrice += priceForPeriod;
        totalPrice += amenitiesFee;

        if (!villa.CleaningFee.IsZero())
        {
            totalPrice += villa.CleaningFee;
        }

        return new PricingDetails(priceForPeriod, villa.CleaningFee, amenitiesFee, totalPrice);
    }

}
