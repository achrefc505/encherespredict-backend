using EncheresPredict.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EncheresPredict.Infrastructure.Persistence.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> b)
    {
        b.HasKey(p => p.Id);
        b.Property(p => p.Profile).HasMaxLength(50).IsRequired();
        b.Property(p => p.RegionsJson).HasColumnName("Regions");
        b.Property(p => p.TypesJson).HasColumnName("Types");
        b.Property(p => p.BudgetMin).HasColumnType("decimal(18,2)");
        b.Property(p => p.BudgetMax).HasColumnType("decimal(18,2)");
    }
}
