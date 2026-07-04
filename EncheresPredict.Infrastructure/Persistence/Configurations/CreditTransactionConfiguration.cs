using EncheresPredict.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EncheresPredict.Infrastructure.Persistence.Configurations;

public class CreditTransactionConfiguration
    : IEntityTypeConfiguration<CreditTransaction>
{
    public void Configure(EntityTypeBuilder<CreditTransaction> b)
    {
        b.HasKey(t => t.Id);

        b.Property(t => t.Amount)
            .IsRequired();

        b.Property(t => t.Type)
            .HasConversion<string>()
            .IsRequired();

        b.Property(t => t.Reason)
            .HasMaxLength(200)
            .IsRequired();

        b.Property(t => t.CreatedAt)
            .IsRequired();
    }
}