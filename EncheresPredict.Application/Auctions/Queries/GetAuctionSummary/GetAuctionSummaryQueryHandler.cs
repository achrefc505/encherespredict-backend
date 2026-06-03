using System.Text.Json;
using EncheresPredict.Application.Common.Interfaces;
using MediatR;

namespace EncheresPredict.Application.Auctions.Queries.GetAuctionSummary;

public sealed class GetAuctionSummaryQueryHandler(IAuctionSummaryReader reader)
    : IRequestHandler<GetAuctionSummaryQuery, AuctionSummaryDto>
{
    public async Task<AuctionSummaryDto> Handle(GetAuctionSummaryQuery query, CancellationToken ct)
    {
        var record = await reader.GetLatestAsync(query.AuctionId, ct)
            ?? throw new KeyNotFoundException("Aucun resume CCV disponible pour ce bien.");

        try
        {
            var summary = JsonSerializer.Deserialize<JsonElement>(record.SummaryJson);
            return new AuctionSummaryDto(record.AuctionId, record.GeneratedAt, record.ModelVersion, summary);
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Resume CCV corrompu en base.", ex);
        }
    }
}
