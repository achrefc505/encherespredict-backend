using EncheresPredict.Domain.Entities;

namespace EncheresPredict.Domain.Repositories;

public interface IUserProfileRepository
{
    Task<UserProfile?> GetCurrentAsync(CancellationToken ct = default);
    Task SaveAsync(UserProfile profile, CancellationToken ct = default);
}
