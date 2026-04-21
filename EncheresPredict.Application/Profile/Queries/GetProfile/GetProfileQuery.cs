using MediatR;

namespace EncheresPredict.Application.Profile.Queries.GetProfile;

public sealed record GetProfileQuery : IRequest<ProfileDto?>;

public sealed record ProfileDto(Guid Id, string Profile, List<string> Regions, decimal BudgetMin, decimal BudgetMax, List<string> Types);
