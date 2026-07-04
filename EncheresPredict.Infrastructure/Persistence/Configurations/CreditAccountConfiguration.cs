using EncheresPredict.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EncheresPredict.Infrastructure.Persistence.Configurations;

public class CreditAccountConfiguration
    : IEntityTypeConfiguration<CreditAccount>
{
    public void Configure(EntityTypeBuilder<CreditAccount> b)
    {
        b.HasKey(a => a.Id);

        b.Property(a => a.UserId)
            .HasMaxLength(450)
            .IsRequired();

        b.HasIndex(a => a.UserId)
            .IsUnique();

        b.Property(a => a.Balance)
            .IsRequired();

        b.HasMany(a => a.Transactions)
            .WithOne(t => t.CreditAccount)
            .HasForeignKey(t => t.CreditAccountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}