namespace EncheresPredictApi.Models;

public class DocumentSummary
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string AuctionId { get; set; } = null!;
    public string SummaryJson { get; set; } = null!;
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    public string? ModelVersion { get; set; }
    public string? PdfUrl { get; set; }
}
