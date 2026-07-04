namespace EncheresPredict.Api.Contracts.Auctions;

public sealed class CompleteDocumentSummaryRequest
{
    public Guid SummaryId { get; set; }

    public string SummaryJson { get; set; } = string.Empty;
}