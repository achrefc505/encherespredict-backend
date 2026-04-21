namespace EncheresPredict.Domain.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency = "EUR")
    {
        if (amount < 0) throw new ArgumentException("Le montant ne peut pas être négatif.", nameof(amount));
        Amount = amount;
        Currency = currency;
    }

    public string Formatted => $"{Amount:N0} €";

    public static Money operator +(Money a, Money b) => new(a.Amount + b.Amount);
    public static Money operator -(Money a, Money b) => new(a.Amount - b.Amount);
    public static bool operator >(Money a, Money b) => a.Amount > b.Amount;
    public static bool operator <(Money a, Money b) => a.Amount < b.Amount;

    public static Money Zero => new(0);
}
