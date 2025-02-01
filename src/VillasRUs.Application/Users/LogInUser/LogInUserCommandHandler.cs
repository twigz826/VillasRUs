using VillasRUs.Application.Abstractions.Authentication;
using VillasRUs.Application.Abstractions.Messaging;
using VillasRUs.Domain.Abstractions;
using VillasRUs.Domain.Users;

namespace VillasRUs.Application.Users.LogInUser;
public class LogInUserCommandHandler : ICommandHandler<LogInUserCommand, AccessTokenResponse>
{
    private readonly IJwtService _jwtService;

    public LogInUserCommandHandler(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    public async Task<Result<AccessTokenResponse>> Handle(LogInUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _jwtService.GetAccessTokenAsync(request.Email, request.Password, cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
        }

        return new AccessTokenResponse(result.Value);
    }
}
