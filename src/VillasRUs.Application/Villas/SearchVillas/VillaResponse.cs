namespace VillasRUs.Application.Villas.SearchVillas;

public sealed class VillaResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public decimal Price { get; init; }

    public string Currency { get; init; } = string.Empty;

    public AddressResponse? Address { get; set; }
}