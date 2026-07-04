namespace EncheresPredict.Application.Credits.Queries.GetCreditBalance;

public sealed record CreditTransactionDto(
    string Type,
    int Amount,
    string Reason,
    DateTime Date);
