using MediatR;

namespace EncheresPredict.Application.Profile.Commands.SaveProfile;

public sealed record SaveProfileCommand(
    string Profile,
    List<string> Regions,
    decimal BudgetMin,
    decimal BudgetMax,
    List<string> Types
) : IRequest<Guid>;
