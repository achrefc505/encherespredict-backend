using EncheresPredict.Domain.Repositories;
using MediatR;

namespace EncheresPredict.Application.Profile.Queries.GetProfile;

public sealed class GetProfileQueryHandler(IUserProfileRepository repo)
    : IRequestHandler<GetProfileQuery, ProfileDto?>
{
    public async Task<ProfileDto?> Handle(GetProfileQuery q, CancellationToken ct)
    {
        var p = await repo.GetCurrentAsync(ct);
        if (p is null) return null;
        return new ProfileDto(p.Id, p.Profile, p.GetRegions(), p.BudgetMin, p.BudgetMax, p.GetTypes());
    }
}
