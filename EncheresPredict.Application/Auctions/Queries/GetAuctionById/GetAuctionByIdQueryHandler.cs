using EncheresPredict.Domain.Exceptions;
using EncheresPredict.Domain.Repositories;
using MediatR;

namespace EncheresPredict.Application.Auctions.Queries.GetAuctionById;

public sealed class GetAuctionByIdQueryHandler(IAuctionRepository repo)
    : IRequestHandler<GetAuctionByIdQuery, AuctionDetailDto>
{
    public async Task<AuctionDetailDto> Handle(GetAuctionByIdQuery q, CancellationToken ct)
    {
        var a = await repo.GetByIdAsync(q.Id, ct)
            ?? throw new AuctionNotFoundException(q.Id);

        var aiDto = a.AiAnalysis is null ? null : new AiAnalysisDto(
            a.AiAnalysis.PricePerSqm, a.AiAnalysis.MarketTrend, a.AiAnalysis.RenovationCost,
            a.AiAnalysis.NetYield, a.AiAnalysis.GrossYield, a.AiAnalysis.PotentialResalePrice,
            a.AiAnalysis.GetRiskFactors(), a.AiAnalysis.GetStrengths(),
            a.AiAnalysis.ModelVersion, a.AiAnalysis.AnalyzedAt);

        var docs = a.Documents.Select(d => new DocumentDto(d.Id, d.Name, d.Type, d.Size, d.Available));

        return new AuctionDetailDto(
            a.Id, a.Title, a.Tribunal, a.City, a.Region, a.Address,
            a.Surface, a.Rooms, a.Type, a.Description,
            a.StartPriceAmount, a.AiEstimateAmount,
            a.Confidence, a.RoiValue, a.Badge.ToString(), a.Status.ToString(), a.AuctionDate,
            aiDto, docs);
    }
}
