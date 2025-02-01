using VillasRUs.Application.Abstractions.Messaging;

namespace VillasRUs.Application.Users.LogInUser;
public record LogInUserCommand(string Email, string Password) : ICommand<AccessTokenResponse>;
