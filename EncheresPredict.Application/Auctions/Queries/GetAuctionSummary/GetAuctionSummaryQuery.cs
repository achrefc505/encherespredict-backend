using System.Text.Json;
using EncheresPredict.Domain.Enums;
using MediatR;

namespace EncheresPredict.Application.Auctions.Queries.GetAuctionSummary;

public sealed record GetAuctionSummaryQuery(Guid AuctionId)
    : IRequest<AuctionSummaryDto>;

public sealed record AuctionSummaryDto(
    Guid AuctionId,
    DateTime GeneratedAt,
    string ModelVersion,
    DocumentSummaryStatus Status,
    JsonElement Summary);