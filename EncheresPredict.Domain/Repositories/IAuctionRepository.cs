using EncheresPredict.Domain.Entities;
using EncheresPredict.Domain.Enums;

namespace EncheresPredict.Domain.Repositories;

public interface IAuctionRepository
{
    Task<Auction?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<(IEnumerable<Auction> Items, int Total)> GetFilteredAsync(
        string? city, string? type, BadgeType? badge, string? region,
        decimal? budgetMin, decimal? budgetMax,
        string sort, int page, int pageSize, CancellationToken ct = default);
    Task<IEnumerable<Auction>> GetTopByRoiAsync(int count, CancellationToken ct = default);
    Task AddAsync(Auction auction, CancellationToken ct = default);
    Task UpdateAsync(Auction auction, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
