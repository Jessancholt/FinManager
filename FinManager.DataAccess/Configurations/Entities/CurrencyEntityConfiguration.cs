using FinManager.DataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinManager.DataAccess.Configurations.Entities;

public class CurrencyEntityConfiguration : BaseEntityConfiguration<Currency>
{
    public override void Configure(EntityTypeBuilder<Currency> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Code)
            .HasMaxLength(3)
            .IsRequired();
    }
}
