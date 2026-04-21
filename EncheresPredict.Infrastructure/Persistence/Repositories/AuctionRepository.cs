using EncheresPredict.Domain.Entities;
using EncheresPredict.Domain.Enums;
using EncheresPredict.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EncheresPredict.Infrastructure.Persistence.Repositories;

public class AuctionRepository(AppDbContext db) : IAuctionRepository
{
    public async Task<Auction?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await db.Auctions
            .Include(a => a.AiAnalysis)
            .Include(a => a.Documents)
            .FirstOrDefaultAsync(a => a.Id == id, ct);

    public async Task<(IEnumerable<Auction> Items, int Total)> GetFilteredAsync(
        string? city, string? type, BadgeType? badge, string? region,
        decimal? budgetMin, decimal? budgetMax,
        string sort, int page, int pageSize, CancellationToken ct = default)
    {
        var q = db.Auctions.AsQueryable();

        if (!string.IsNullOrEmpty(city))   q = q.Where(a => a.City == city);
        if (!string.IsNullOrEmpty(type))   q = q.Where(a => a.Type == type);
        if (!string.IsNullOrEmpty(region)) q = q.Where(a => a.Region == region);
        if (badge.HasValue)                q = q.Where(a => a.Badge == badge.Value);
        if (budgetMin.HasValue)            q = q.Where(a => a.StartPriceAmount >= budgetMin.Value);
        if (budgetMax.HasValue)            q = q.Where(a => a.StartPriceAmount <= budgetMax.Value);

        q = sort switch
        {
            "price" => q.OrderBy(a => a.StartPriceAmount),
            "date"  => q.OrderBy(a => a.AuctionDate),
            "conf"  => q.OrderByDescending(a => a.Confidence),
            _       => q.OrderByDescending(a => a.RoiValue)
        };

        var total = await q.CountAsync(ct);
        var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return (items, total);
    }

    public async Task<IEnumerable<Auction>> GetTopByRoiAsync(int count, CancellationToken ct = default) =>
        await db.Auctions.OrderByDescending(a => a.RoiValue).Take(count).ToListAsync(ct);

    public async Task AddAsync(Auction auction, CancellationToken ct = default)
    {
        await db.Auctions.AddAsync(auction, ct);
        await db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Auction auction, CancellationToken ct = default)
    {
        db.Auctions.Update(auction);
        await db.SaveChangesAsync(ct);
    }

    public async Task<int> CountAsync(CancellationToken ct = default) =>
        await db.Auctions.CountAsync(ct);
}
