using EncheresPredict.Domain.Enums;

namespace EncheresPredict.Domain.ValueObjects;

public sealed record RoiScore
{
    public decimal Value { get; }

    private RoiScore(decimal value) => Value = value;

    public static RoiScore Calculate(Money startPrice, Money aiEstimate)
    {
        if (startPrice.Amount == 0) return new RoiScore(0);
        var roi = ((aiEstimate.Amount - startPrice.Amount) / startPrice.Amount) * 100;
        return new RoiScore(Math.Round(roi, 1));
    }

    public BadgeType ToBadge() => Value switch
    {
        >= 30 => BadgeType.TresBonneAffaire,
        >= 15 => BadgeType.BonneAffaire,
        >= 0  => BadgeType.Neutre,
        _     => BadgeType.Risque
    };

    public string Formatted => $"{(Value > 0 ? "+" : "")}{Value:F1}%";
}
