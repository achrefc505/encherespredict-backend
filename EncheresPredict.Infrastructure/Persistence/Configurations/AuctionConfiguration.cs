using EncheresPredict.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EncheresPredict.Infrastructure.Persistence.Configurations;

public class AuctionConfiguration : IEntityTypeConfiguration<Auction>
{
    public void Configure(EntityTypeBuilder<Auction> b)
    {
        b.HasKey(a => a.Id);
        b.Property(a => a.Title).HasMaxLength(200).IsRequired();
        b.Property(a => a.Tribunal).HasMaxLength(100).IsRequired();
        b.Property(a => a.City).HasMaxLength(100).IsRequired();
        b.Property(a => a.Region).HasMaxLength(100).IsRequired();
        b.Property(a => a.Address).HasMaxLength(300);
        b.Property(a => a.Type).HasMaxLength(50).IsRequired();
        b.Property(a => a.Description).HasMaxLength(2000);
        b.Property(a => a.StartPriceAmount).HasColumnType("decimal(18,2)");
        b.Property(a => a.AiEstimateAmount).HasColumnType("decimal(18,2)");
        b.Property(a => a.RoiValue).HasColumnType("decimal(10,2)");
        b.Property(a => a.Badge).HasConversion<string>();
        b.Property(a => a.Status).HasConversion<string>();

        b.HasOne(a => a.AiAnalysis)
         .WithOne()
         .HasForeignKey<AiAnalysis>(ai => ai.AuctionId)
         .OnDelete(DeleteBehavior.Cascade);

        b.HasMany(a => a.Documents)
         .WithOne()
         .HasForeignKey(d => d.AuctionId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}
