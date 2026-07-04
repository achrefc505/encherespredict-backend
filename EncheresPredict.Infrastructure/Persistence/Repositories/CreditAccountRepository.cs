using EncheresPredict.Domain.Entities;
using EncheresPredict.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EncheresPredict.Infrastructure.Persistence.Repositories;

public sealed class CreditAccountRepository(AppDbContext db)
    : ICreditAccountRepository
{
    public async Task<CreditAccount?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        return await db.CreditAccounts
            .Include(a => a.Transactions)
            .FirstOrDefaultAsync(a => a.Id == id, ct);
    }

    public async Task<CreditAccount?> GetByUserIdAsync(
        string userId,
        CancellationToken ct = default)
    {
        return await db.CreditAccounts
            .Include(a => a.Transactions)
            .FirstOrDefaultAsync(a => a.UserId == userId, ct);
    }
    public async Task AddAsync(
        CreditAccount account,
        CancellationToken ct = default)
    {
        await db.CreditAccounts.AddAsync(account, ct);
    }
    public Task UpdateAsync(
        CreditAccount account,
        CancellationToken ct = default)
    {
        if (db.Entry(account).State == EntityState.Detached)
        {
            db.CreditAccounts.Update(account);
        }

        return Task.CompletedTask;
    }
}