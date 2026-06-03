namespace EncheresPredict.Application.Common.Interfaces;

public interface IAuctionSummaryReader
{
    Task<AuctionSummaryReadDto?> GetLatestAsync(Guid auctionId, CancellationToken ct = default);
}

public sealed record AuctionSummaryReadDto(
    Guid AuctionId,
    DateTime GeneratedAt,
    string ModelVersion,
    string SummaryJson);
