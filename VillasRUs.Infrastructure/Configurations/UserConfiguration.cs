using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VillasRUs.Domain.Users;

namespace VillasRUs.Infrastructure.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.FirstName)
            .HasMaxLength(128)
            .HasConversion(fName => fName.Value, value => new FirstName(value));

        builder.Property(user => user.LastName)
            .HasMaxLength(128)
            .HasConversion(lName => lName.Value, value => new LastName(value));

        builder.Property(user => user.Email)
            .HasMaxLength(350)
            .HasConversion(email => email.Value, value => new Domain.Users.Email(value));

        builder.HasIndex(user => user.Email).IsUnique();
    }
}
