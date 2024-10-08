using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VillasRUs.Domain.Shared;
using VillasRUs.Domain.Villas;

namespace VillasRUs.Infrastructure.Configurations;

public sealed class VillaConfiguration : IEntityTypeConfiguration<Villa>
{
    public void Configure(EntityTypeBuilder<Villa> builder)
    {
        builder.ToTable("villas");

        builder.HasKey(villa => villa.Id);

        builder.OwnsOne(villa => villa.Address);

        builder.Property(villa => villa.Name)
            .HasMaxLength(128)
            .HasConversion(name => name.Value, value => new Name(value));

        builder.Property(villa => villa.Description)
            .HasMaxLength(1500)
            .HasConversion(description => description.Value, value => new Description(value));

        builder.OwnsOne(villa => villa.Price, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.OwnsOne(villa => villa.CleaningFee, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.Property<uint>("Version").IsRowVersion();
    }
}
