using EncheresPredict.Domain.Enums;
using EncheresPredict.Domain.Repositories;
using MediatR;

namespace EncheresPredict.Application.Auctions.Queries.GetDashboardStats;

public sealed class GetDashboardStatsQueryHandler(IAuctionRepository repo)
    : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
{
    public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery q, CancellationToken ct)
    {
        var (all, total) = await repo.GetFilteredAsync(null, null, null, null, null, null, "roi", 1, 1000, ct);
        var list = all.ToList();

        var top10 = await repo.GetTopByRoiAsync(10, ct);

        var chart = top10.Select(a => new ChartPointDto(
            a.Title.Length > 25 ? a.Title[..25] + "…" : a.Title,
            a.StartPriceAmount, a.AiEstimateAmount, a.RoiValue));

        return new DashboardStatsDto(
            TotalAuctions: total,
            AvgRoi: list.Count > 0 ? Math.Round(list.Average(a => a.RoiValue), 1) : 0,
            AvgConfidence: list.Count > 0 ? Math.Round(list.Average(a => a.Confidence), 1) : 0,
            TotalOpportunities: list.Count(a => a.Badge == BadgeType.TresBonneAffaire || a.Badge == BadgeType.BonneAffaire),
            ByBadge: new BadgeCountsDto(
                list.Count(a => a.Badge == BadgeType.TresBonneAffaire),
                list.Count(a => a.Badge == BadgeType.BonneAffaire),
                list.Count(a => a.Badge == BadgeType.Neutre),
                list.Count(a => a.Badge == BadgeType.Risque)),
            ChartData: chart);
    }
}
