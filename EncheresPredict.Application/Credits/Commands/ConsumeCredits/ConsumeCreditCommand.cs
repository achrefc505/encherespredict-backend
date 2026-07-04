using MediatR;

namespace EncheresPredict.Application.Credits.Commands.ConsumeCredit;

public sealed record ConsumeCreditCommand(
    string Reason
) : IRequest;