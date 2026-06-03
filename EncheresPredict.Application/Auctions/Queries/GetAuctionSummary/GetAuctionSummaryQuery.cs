using System.Text.Json;
using MediatR;

namespace EncheresPredict.Application.Auctions.Queries.GetAuctionSummary;

public sealed record GetAuctionSummaryQuery(Guid AuctionId) : IRequest<AuctionSummaryDto>;

public sealed record AuctionSummaryDto(
    Guid AuctionId,
    DateTime GeneratedAt,
    string ModelVersion,
    JsonElement Summary);
