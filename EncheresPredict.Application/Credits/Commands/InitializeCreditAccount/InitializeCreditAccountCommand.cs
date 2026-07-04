using MediatR;

namespace EncheresPredict.Application.Credits.Commands.InitializeCreditAccount;

public sealed record InitializeCreditAccountCommand(string UserId) : IRequest;