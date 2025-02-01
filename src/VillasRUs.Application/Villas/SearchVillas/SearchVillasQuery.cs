using VillasRUs.Application.Abstractions.Messaging;

namespace VillasRUs.Application.Villas.SearchVillas;

public sealed record SearchVillasQuery(DateTime StartDate, DateTime EndDate) : IQuery<IReadOnlyList<VillaResponse>>;