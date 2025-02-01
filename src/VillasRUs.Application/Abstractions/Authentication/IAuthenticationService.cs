using VillasRUs.Domain.Users;

namespace VillasRUs.Application.Abstractions.Authentication;
public interface IAuthenticationService
{
    Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken = default);
}
