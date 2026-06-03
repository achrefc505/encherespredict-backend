using EncheresPredict.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EncheresPredict.Infrastructure.Persistence.Configurations;

public class DocumentSummaryConfiguration : IEntityTypeConfiguration<DocumentSummary>
{
    public void Configure(EntityTypeBuilder<DocumentSummary> b)
    {
        b.HasKey(s => s.Id);
        b.Property(s => s.SummaryJson).IsRequired();
        b.Property(s => s.ModelVersion).HasMaxLength(80).IsRequired();
        b.Property(s => s.PdfUrl).HasMaxLength(500);
        b.HasIndex(s => s.AuctionId);
        b.HasIndex(s => s.GeneratedAt);
    }
}
