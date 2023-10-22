using VillasRUs.Domain.Abstractions;

namespace VillasRUs.Domain.Users
{
    public static class UserErrors
    {
        public static Error NotFound = new(
            "User.Found",
            "The user with the specified id was not found");

        public static Error InvalidCredentials = new(
            "User.InvalidCredentials",
            "The provided credentials were invalid");
    }
}
