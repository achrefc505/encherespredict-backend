using MediatR;

namespace EncheresPredict.Application.Auctions.Queries.GetAuctionById;

public sealed record GetAuctionByIdQuery(Guid Id) : IRequest<AuctionDetailDto>;

public sealed record AuctionDetailDto(
    Guid Id, string Title, string Tribunal, string City, string Region, string Address,
    int Surface, int Rooms, string Type, string Description,
    decimal StartPrice, decimal AiEstimate,
    int Confidence, decimal Roi, string Badge, string Status, DateTime AuctionDate,
    AiAnalysisDto? AiAnalysis,
    IEnumerable<DocumentDto> Documents);

public sealed record AiAnalysisDto(
    decimal PricePerSqm, string MarketTrend, decimal RenovationCost,
    decimal NetYield, decimal GrossYield, decimal PotentialResalePrice,
    List<string> RiskFactors, List<string> Strengths, string ModelVersion, DateTime AnalyzedAt);

public sealed record DocumentDto(Guid Id, string Name, string Type, string Size, bool Available);
