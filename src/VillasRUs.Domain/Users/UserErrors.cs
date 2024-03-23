using VillasRUs.Domain.Abstractions;

namespace VillasRUs.Domain.Users;

public static class UserErrors
{
    public static Error NotFound { get; } = new(
        "User.NotFound",
        "The user with the specified id was not found");

    public static Error InvalidCredentials { get; } = new(
        "User.InvalidCredentials",
        "The provided credentials were invalid");
}
