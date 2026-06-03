using EncheresPredict.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EncheresPredict.Infrastructure.Persistence.Configurations;

public class AiAnalysisConfiguration : IEntityTypeConfiguration<AiAnalysis>
{
    public void Configure(EntityTypeBuilder<AiAnalysis> b)
    {
        b.HasKey(a => a.Id);
        b.Property(a => a.PricePerSqm).HasColumnType("decimal(18,2)");
        b.Property(a => a.RenovationCost).HasColumnType("decimal(18,2)");
        b.Property(a => a.NetYield).HasColumnType("decimal(10,2)");
        b.Property(a => a.GrossYield).HasColumnType("decimal(10,2)");
        b.Property(a => a.PotentialResalePrice).HasColumnType("decimal(18,2)");
        b.Property(a => a.MarketTrend).HasMaxLength(30).IsRequired();
        b.Property(a => a.ModelVersion).HasMaxLength(30).IsRequired();
        b.Property(a => a.RiskFactorsJson).HasColumnName("RiskFactors").IsRequired();
        b.Property(a => a.StrengthsJson).HasColumnName("Strengths").IsRequired();
    }
}
