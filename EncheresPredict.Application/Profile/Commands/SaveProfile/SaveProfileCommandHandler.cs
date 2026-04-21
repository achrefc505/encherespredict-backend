using EncheresPredict.Domain.Entities;
using EncheresPredict.Domain.Repositories;
using MediatR;

namespace EncheresPredict.Application.Profile.Commands.SaveProfile;

public sealed class SaveProfileCommandHandler(IUserProfileRepository repo)
    : IRequestHandler<SaveProfileCommand, Guid>
{
    public async Task<Guid> Handle(SaveProfileCommand c, CancellationToken ct)
    {
        var profile = UserProfile.Create(c.Profile, c.Regions, c.BudgetMin, c.BudgetMax, c.Types);
        await repo.SaveAsync(profile, ct);
        return profile.Id;
    }
}
