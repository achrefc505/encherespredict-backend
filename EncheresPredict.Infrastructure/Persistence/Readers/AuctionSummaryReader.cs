using EncheresPredict.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EncheresPredict.Infrastructure.Persistence.Readers;

public class AuctionSummaryReader(AppDbContext db) : IAuctionSummaryReader
{
    public async Task<AuctionSummaryReadDto?> GetLatestAsync(Guid auctionId, CancellationToken ct = default)
    {
        return await db.DocumentSummaries
            .AsNoTracking()
            .Where(s => s.AuctionId == auctionId)
            .OrderByDescending(s => s.GeneratedAt)
            .Select(s => new AuctionSummaryReadDto(
                s.AuctionId,
                s.GeneratedAt,
                s.ModelVersion,
                s.SummaryJson))
            .FirstOrDefaultAsync(ct);
    }
}
