using EncheresPredict.Domain.Entities;
using EncheresPredict.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EncheresPredict.Infrastructure.Persistence.Repositories;

public class UserProfileRepository(AppDbContext db) : IUserProfileRepository
{
    public async Task<UserProfile?> GetCurrentAsync(CancellationToken ct = default) =>
        await db.UserProfiles.OrderByDescending(p => p.CreatedAt).FirstOrDefaultAsync(ct);

    public async Task SaveAsync(UserProfile profile, CancellationToken ct = default)
    {
        await db.UserProfiles.AddAsync(profile, ct);
        await db.SaveChangesAsync(ct);
    }
}
