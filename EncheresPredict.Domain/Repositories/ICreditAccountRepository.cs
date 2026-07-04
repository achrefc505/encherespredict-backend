using EncheresPredict.Domain.Entities;

namespace EncheresPredict.Domain.Repositories;

public interface ICreditAccountRepository
{
    Task<CreditAccount?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default);

    Task<CreditAccount?> GetByUserIdAsync(
        string userId,
        CancellationToken ct = default);

    Task AddAsync(
        CreditAccount account,
        CancellationToken ct = default);

    Task UpdateAsync(
        CreditAccount account,
        CancellationToken ct = default);
}