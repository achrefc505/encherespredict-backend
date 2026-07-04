using EncheresPredict.Domain.Enums;

namespace EncheresPredict.Domain.Entities;

public class CreditTransaction : BaseEntity

{
    public Guid CreditAccountId { get; private set; }

    public CreditAccount CreditAccount { get; private set; } = null!;
    public int Amount { get; private set; }

    public CreditTransactionType Type { get; private set; }

    public string Reason { get; private set; } = string.Empty;

    private CreditTransaction()
    {
    }

    public static CreditTransaction Create(
        CreditAccount account,
        int amount,
        CreditTransactionType type,
        string reason)
    {
        return new CreditTransaction
        {
            CreditAccountId = account.Id,
            CreditAccount = account,
            Amount = amount,
            Type = type,
            Reason = reason
        };
    }
}