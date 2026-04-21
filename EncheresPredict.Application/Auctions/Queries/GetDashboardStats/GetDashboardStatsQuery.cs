using MediatR;

namespace EncheresPredict.Application.Auctions.Queries.GetDashboardStats;

public sealed record GetDashboardStatsQuery : IRequest<DashboardStatsDto>;

public sealed record DashboardStatsDto(
    int TotalAuctions,
    decimal AvgRoi,
    double AvgConfidence,
    int TotalOpportunities,
    BadgeCountsDto ByBadge,
    IEnumerable<ChartPointDto> ChartData);

public sealed record BadgeCountsDto(int TresBonneAffaire, int BonneAffaire, int Neutre, int Risque);
public sealed record ChartPointDto(string Title, decimal StartPrice, decimal AiEstimate, decimal Roi);
