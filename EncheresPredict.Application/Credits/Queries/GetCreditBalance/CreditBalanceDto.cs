namespace EncheresPredict.Application.Credits.Queries.GetCreditBalance;

public sealed record CreditBalanceDto(
    int Balance,
    IReadOnlyList<CreditTransactionDto> RecentTransactions);
