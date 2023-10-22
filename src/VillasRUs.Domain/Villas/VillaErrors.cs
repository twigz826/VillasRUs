using VillasRUs.Domain.Abstractions;

namespace VillasRUs.Domain.Villas
{
    public static class VillaErrors
    {
        public static Error NotFound = new(
            "Property.Found",
            "The property with the specified id was not found");
    }
}
