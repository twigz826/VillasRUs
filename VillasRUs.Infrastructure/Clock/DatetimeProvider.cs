using VillasRUs.Application.Abstractions.Clock;

namespace VillasRUs.Infrastructure.Clock;

internal class DatetimeProvider : IDatetimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
