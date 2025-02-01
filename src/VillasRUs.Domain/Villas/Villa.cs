using VillasRUs.Domain.Abstractions;
using VillasRUs.Domain.Shared;

namespace VillasRUs.Domain.Villas;

public sealed class Villa : Entity
{
    public Villa(Guid id, Name name, Description description, Address address, Money price, Money cleaningFee, List<Amenity> amenities) : base(id)
    {
        Name = name;
        Description = description;
        Address = address;
        Price = price;
        CleaningFee = cleaningFee;
        Amenities = amenities;
    }

    private Villa()
    {
    }

    public Name Name { get; private set; }

    public Description Description { get; private set; }

    public Address Address { get; private set; }

    public Money Price { get; private set; }

    public Money CleaningFee { get; private set; }

    public DateTime? LastBookedOnUtc { get; internal set; }
    //This has an internal setter so that we can update the value from within the domain project

    public List<Amenity> Amenities { get; private set; } = [];
}
