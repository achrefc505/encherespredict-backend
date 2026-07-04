using EncheresPredict.Domain.Enums;
using EncheresPredict.Domain.Exceptions;

namespace EncheresPredict.Domain.Entities;

public class CreditAccount : BaseEntity
{
    public const int WelcomeCredits = 3;
    public const string WelcomeReason = "welcome_beta";

    private const int CreditCost = 1;

    public string UserId { get; private set; } = string.Empty;

    public int Balance { get; private set; }

    private readonly List<CreditTransaction> _transactions = [];

    public IReadOnlyCollection<CreditTransaction> Transactions
        => _transactions.AsReadOnly();

    private CreditAccount()
    {
    }

    public static CreditAccount Create(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException(
                "UserId is required.",
                nameof(userId));
        }
        var account = new CreditAccount
        {
            UserId = userId
        };

        account.Grant(
    WelcomeCredits,
    WelcomeReason);

        return account;
    }

    public void Grant(
      int amount,
      string reason)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Amount must be greater than zero.");
        }

        ValidateReason(reason);

        Balance += amount;

        AddTransaction(
            amount,
            CreditTransactionType.Grant,
            reason);
    }

    public void Consume(string reason)
    {
        if (Balance < CreditCost)
        {
            throw new InsufficientCreditsException();
        }
        ValidateReason(reason);

        Balance -= CreditCost;

        AddTransaction(
            -CreditCost,
            CreditTransactionType.Consume,
            reason);
    }

    private void ValidateReason(string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
        {
            throw new ArgumentException(
                "Reason is required.",
                nameof(reason));
        }
    }

    private void AddTransaction(
        int amount,
        CreditTransactionType type,
        string reason)
    {
        var transaction = CreditTransaction.Create(
            this,
            amount,
            type,
            reason);

        _transactions.Add(transaction);
    }
}