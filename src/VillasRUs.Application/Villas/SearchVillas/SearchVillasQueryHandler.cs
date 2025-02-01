using Dapper;
using VillasRUs.Application.Abstractions.Data;
using VillasRUs.Application.Abstractions.Messaging;
using VillasRUs.Domain.Abstractions;
using VillasRUs.Domain.Bookings;

namespace VillasRUs.Application.Villas.SearchVillas;

internal sealed class SearchVillasQueryHandler : IQueryHandler<SearchVillasQuery, IReadOnlyList<VillaResponse>>
{
    private static readonly int[] ActiveBookingStatuses =
    [
        (int)BookingStatus.Reserved,
        (int)BookingStatus.Confirmed,
        (int)BookingStatus.Completed,
    ];

    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchVillasQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<VillaResponse>>> Handle(SearchVillasQuery request, CancellationToken cancellationToken)
    {
        if (request.StartDate > request.EndDate)
        {
            return new List<VillaResponse>();
        }

        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
        SELECT
            a.id AS Id,
            a.name AS Name,
            a.description AS Description,
            a.price_amount AS Price,
            a.price_currency AS Currency,
            a.address_country AS Country,
            a.address_county AS County,
            a.address_postcode AS Postcode,
            a.address_city AS City,
            a.address_street AS Street
        FROM villas AS a
        WHERE NOT EXISTS
        (
            SELECT 1
            FROM bookings AS b
            WHERE
                b.villa_id = a.id AND
                b.duration_start <= @EndDate AND
                b.duration_end >= @StartDate AND
                b.status = ANY(@ActiveBookingStatuses)
        )
        """;

        var villas = await connection.QueryAsync<VillaResponse, AddressResponse, VillaResponse>(
            sql,
            (villa, address) =>
            {
                villa.Address = address;
                return villa;
            },
            new { request.StartDate, request.EndDate, ActiveBookingStatuses },
            splitOn: "Country"
            );

        return villas.ToList();
    }
}