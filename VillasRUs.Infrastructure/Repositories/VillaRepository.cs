using VillasRUs.Domain.Villas;

namespace VillasRUs.Infrastructure.Repositories;

internal sealed class VillaRepository : Repository<Villa>, IVillaRepository
{
    public VillaRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
