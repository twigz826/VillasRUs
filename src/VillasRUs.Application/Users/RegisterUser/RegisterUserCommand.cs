using VillasRUs.Application.Abstractions.Messaging;

namespace VillasRUs.Application.Users.RegisterUser;
public record RegisterUserCommand(string Email, string FirstName, string LastName, string Password) : ICommand<Guid>;
