namespace EncheresPredict.Infrastructure.Persistence.ReadModels;

public class DocumentSummary
{
    public Guid Id { get; private set; }
    public Guid AuctionId { get; private set; }
    public string SummaryJson { get; private set; } = "{}";
    public DateTime GeneratedAt { get; private set; } = DateTime.UtcNow;
    public string ModelVersion { get; private set; } = "claude-sonnet-4-6";
    public string? PdfUrl { get; private set; }
}
