namespace EncheresPredictApi.Models.Api;

public record AuctionsResult(List<AuctionListItem> Items, int Total, int Page, int PageSize);

public record AuctionListItem(
    string Id, string Title, string Tribunal, string City, string Region,
    double Surface, int Rooms, string Type, decimal StartPrice, decimal AiEstimate,
    double Confidence, double Roi, string Badge, string Status, string AuctionDate
);

public record BadgeCounts(
    int TresBonneAffaire = 0, int BonneAffaire = 0, int Neutre = 0, int Risque = 0
);

public record ChartPoint(string Title, decimal StartPrice, decimal AiEstimate, double Roi);

public record DashboardStats(
    int TotalAuctions, double AvgRoi, double AvgConfidence, int TotalOpportunities,
    BadgeCounts ByBadge, List<ChartPoint> ChartData
);

public record AlertDto(
    string Id, string? AuctionId, string Type, string Title,
    string Message, bool IsRead, string CreatedAt
);
