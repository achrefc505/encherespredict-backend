using MediatR;

namespace EncheresPredict.Application.Credits.Commands.GrantCredits;

public sealed record GrantCreditsCommand(
    string UserId,
    int Amount,
    string Reason) : IRequest;