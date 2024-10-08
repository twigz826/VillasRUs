using VillasRUs.Application.Abstractions.Email;

namespace VillasRUs.Infrastructure.Email;

internal class EmailService : IEmailService
{
    public Task SendAsync(Domain.Users.Email recipient, string subject, string body)
    {
        return Task.CompletedTask;
    }
}
