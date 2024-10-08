using VillasRUs.Application.Abstractions.Messaging;

namespace VillasRUs.Application.Villas.SearchVillas;

public sealed record SearchVillasQuery(DateOnly StartDate, DateOnly EndDate) : IQuery<IReadOnlyList<VillaResponse>>;