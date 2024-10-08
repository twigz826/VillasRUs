namespace VillasRUs.Application.Villas.SearchVillas;

public sealed class AddressResponse
{
    public string Country { get; init; } = string.Empty;

    public string County { get; init; } = string.Empty;

    public string Postcode { get; init; } = string.Empty;

    public string City { get; init; } = string.Empty;

    public string Street { get; init; } = string.Empty;
}