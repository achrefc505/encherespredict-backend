using EncheresPredict.Domain.Entities;
using EncheresPredict.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EncheresPredict.Infrastructure.Persistence.Repositories;

public class AlertRepository(AppDbContext db) : IAlertRepository
{
    public async Task<IEnumerable<Alert>> GetAllAsync(CancellationToken ct = default) =>
        await db.Alerts.OrderByDescending(a => a.CreatedAt).ToListAsync(ct);

    public async Task<Alert?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await db.Alerts.FindAsync([id], ct);

    public async Task AddAsync(Alert alert, CancellationToken ct = default)
    {
        await db.Alerts.AddAsync(alert, ct);
        await db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Alert alert, CancellationToken ct = default)
    {
        db.Alerts.Update(alert);
        await db.SaveChangesAsync(ct);
    }
}
