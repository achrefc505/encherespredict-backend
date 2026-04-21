using EncheresPredict.Domain.Repositories;
using MediatR;

namespace EncheresPredict.Application.Auctions.Queries.GetAuctions;

public sealed class GetAuctionsQueryHandler(IAuctionRepository repo)
    : IRequestHandler<GetAuctionsQuery, GetAuctionsResult>
{
    public async Task<GetAuctionsResult> Handle(GetAuctionsQuery q, CancellationToken ct)
    {
        var (items, total) = await repo.GetFilteredAsync(
            q.City, q.Type, q.Badge, q.Region,
            q.BudgetMin, q.BudgetMax, q.Sort, q.Page, q.PageSize, ct);

        var dtos = items.Select(a => new AuctionListItemDto(
            a.Id, a.Title, a.Tribunal, a.City, a.Region,
            a.Surface, a.Rooms, a.Type,
            a.StartPriceAmount, a.AiEstimateAmount,
            a.Confidence, a.RoiValue,
            a.Badge.ToString(), a.Status.ToString(),
            a.AuctionDate));

        return new GetAuctionsResult(dtos, total, q.Page, q.PageSize);
    }
}
