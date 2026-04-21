using EncheresPredict.Domain.Enums;
using MediatR;

namespace EncheresPredict.Application.Auctions.Queries.GetAuctions;

public sealed record GetAuctionsQuery(
    string? City = null,
    string? Type = null,
    BadgeType? Badge = null,
    string? Region = null,
    decimal? BudgetMin = null,
    decimal? BudgetMax = null,
    string Sort = "roi",
    int Page = 1,
    int PageSize = 20
) : IRequest<GetAuctionsResult>;

public sealed record GetAuctionsResult(IEnumerable<AuctionListItemDto> Items, int Total, int Page, int PageSize);

public sealed record AuctionListItemDto(
    Guid Id, string Title, string Tribunal, string City, string Region,
    int Surface, int Rooms, string Type,
    decimal StartPrice, decimal AiEstimate,
    int Confidence, decimal Roi, string Badge, string Status,
    DateTime AuctionDate);
