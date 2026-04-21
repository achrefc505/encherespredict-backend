using EncheresPredict.Domain.Entities;

namespace EncheresPredict.Domain.Repositories;

public interface IAlertRepository
{
    Task<IEnumerable<Alert>> GetAllAsync(CancellationToken ct = default);
    Task<Alert?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Alert alert, CancellationToken ct = default);
    Task UpdateAsync(Alert alert, CancellationToken ct = default);
}
