using EncheresPredict.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EncheresPredict.Infrastructure.Persistence.Configurations;

public class AlertConfiguration : IEntityTypeConfiguration<Alert>
{
    public void Configure(EntityTypeBuilder<Alert> b)
    {
        b.HasKey(a => a.Id);
        b.Property(a => a.Title).HasMaxLength(200).IsRequired();
        b.Property(a => a.Message).HasMaxLength(1000).IsRequired();
        b.Property(a => a.Type).HasConversion<string>();
    }
}
