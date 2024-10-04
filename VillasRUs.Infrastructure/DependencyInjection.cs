using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VillasRUs.Application.Abstractions.Clock;
using VillasRUs.Application.Abstractions.Data;
using VillasRUs.Application.Abstractions.Email;
using VillasRUs.Domain.Abstractions;
using VillasRUs.Domain.Bookings;
using VillasRUs.Domain.Users;
using VillasRUs.Domain.Villas;
using VillasRUs.Infrastructure.Clock;
using VillasRUs.Infrastructure.Data;
using VillasRUs.Infrastructure.Email;
using VillasRUs.Infrastructure.Repositories;

namespace VillasRUs.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDatetimeProvider, DatetimeProvider>();
        services.AddTransient<IEmailService, EmailService>();

        var connectionString = configuration.GetConnectionString("Database") ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVillaRepository, VillaRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        return services;
    }
}
