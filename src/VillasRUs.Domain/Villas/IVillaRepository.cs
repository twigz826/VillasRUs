namespace VillasRUs.Domain.Villas
{
    public interface IVillaRepository
    {
        Task<Villa> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
