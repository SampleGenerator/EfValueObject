using EfValueObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfValueObject.Data;

public sealed class PersonConfig : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder
            .OwnsOne(p => p.Address, cfg =>
            {
                cfg.Property(a => a.Country).HasColumnName(nameof(Address.Country));
                cfg.Property(a => a.City).HasColumnName(nameof(Address.City));
                cfg.Property(a => a.PostalCode).HasColumnName(nameof(Address.PostalCode));
            });

        builder
            .OwnsMany(p => p.PhoneNumbers)
            .Property(p => p.Value)
            .HasColumnName(nameof(PhoneNumber));

        builder
            .Property(p => p.GiftCards)
            .HasConversion(
                s => string.Join(", ", s), 
                d => d.Split(", ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(g => Enum.Parse<GiftCard>(g))
                    .ToList()
            );
    }
}
