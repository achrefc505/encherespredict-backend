using EncheresPredict.Domain.Enums;

namespace EncheresPredict.Domain.Entities;

public class DocumentSummary : BaseEntity
{
    public Guid AuctionId { get; private set; }

    public string SummaryJson { get; private set; } = "{}";

    public DateTime GeneratedAt { get; private set; }

    public string ModelVersion { get; private set; } = string.Empty;

    public string? PdfUrl { get; private set; }

    public DocumentSummaryStatus Status { get; private set; }

    public string? FailureReason { get; private set; }

    private DocumentSummary()
    {
    }

    public static DocumentSummary Create(
        Guid auctionId,
        string pdfUrl,
        string modelVersion)
    {
        return new DocumentSummary
        {
            AuctionId = auctionId,
            PdfUrl = pdfUrl,
            ModelVersion = modelVersion,
            GeneratedAt = DateTime.UtcNow,
            SummaryJson = "{}",
            Status = DocumentSummaryStatus.Pending
        };
    }

    public void MarkProcessing()
    {
        Status = DocumentSummaryStatus.Processing;
    }

    public void MarkReady(string summaryJson)
    {
        SummaryJson = summaryJson;
        Status = DocumentSummaryStatus.Ready;
        FailureReason = null;
    }

    public void MarkFailed(string reason)
    {
        Status = DocumentSummaryStatus.Failed;
        FailureReason = reason;
    }
}