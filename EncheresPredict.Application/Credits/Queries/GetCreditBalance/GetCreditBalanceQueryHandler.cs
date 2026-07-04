using EncheresPredict.Application.Common.Interfaces;
using EncheresPredict.Domain.Repositories;
using MediatR;

namespace EncheresPredict.Application.Credits.Queries.GetCreditBalance;

public sealed class GetCreditBalanceQueryHandler(
    ICreditAccountRepository repo,
    ICurrentUserService currentUser)
    : IRequestHandler<GetCreditBalanceQuery, CreditBalanceDto>
{
    public async Task<CreditBalanceDto> Handle(
        GetCreditBalanceQuery query,
        CancellationToken ct)
    {
        var userId = currentUser.UserId
            ?? throw new UnauthorizedAccessException("User is not authenticated.");

        var account = await repo.GetByUserIdAsync(userId, ct);

        if (account is null)
        {
            throw new KeyNotFoundException(
                $"Credit account not found for user '{userId}'.");
        }

        var recentTransactions = account.Transactions
            .OrderByDescending(t => t.CreatedAt)
            .Take(10)
            .Select(t => new CreditTransactionDto(
                t.Type.ToString(),
                t.Amount,
                t.Reason,
                t.CreatedAt))
            .ToList();

        return new CreditBalanceDto(
            account.Balance,
            recentTransactions);
    }
}
