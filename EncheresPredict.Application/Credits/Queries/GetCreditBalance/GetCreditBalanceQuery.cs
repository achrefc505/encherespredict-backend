using MediatR;

namespace EncheresPredict.Application.Credits.Queries.GetCreditBalance;

public sealed record GetCreditBalanceQuery : IRequest<CreditBalanceDto>;
