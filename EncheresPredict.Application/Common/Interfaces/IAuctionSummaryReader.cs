namespace EncheresPredict.Application.Common.Interfaces;
using EncheresPredict.Domain.Enums;

public interface IAuctionSummaryReader
{
    Task<AuctionSummaryReadDto?> GetLatestAsync(Guid auctionId, CancellationToken ct = default);
}

public sealed record AuctionSummaryReadDto(
    Guid AuctionId,
    DateTime GeneratedAt,
    string ModelVersion,
    DocumentSummaryStatus Status,
    string SummaryJson);