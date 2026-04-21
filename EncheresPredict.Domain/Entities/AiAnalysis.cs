namespace EncheresPredict.Domain.Entities;

public class AiAnalysis : BaseEntity
{
    public Guid AuctionId { get; private set; }
    public decimal PricePerSqm { get; private set; }
    public string MarketTrend { get; private set; } = string.Empty;
    public decimal RenovationCost { get; private set; }
    public decimal NetYield { get; private set; }
    public decimal GrossYield { get; private set; }
    public decimal PotentialResalePrice { get; private set; }
    public string RiskFactorsJson { get; private set; } = "[]";
    public string StrengthsJson { get; private set; } = "[]";
    public string ModelVersion { get; private set; } = "v2.1";
    public DateTime AnalyzedAt { get; private set; } = DateTime.UtcNow;

    private AiAnalysis() { }

    public static AiAnalysis Create(
        Guid auctionId,
        decimal pricePerSqm,
        string marketTrend,
        decimal renovationCost,
        decimal netYield,
        decimal grossYield,
        decimal potentialResalePrice,
        List<string> riskFactors,
        List<string> strengths,
        string modelVersion = "v2.1")
    {
        return new AiAnalysis
        {
            AuctionId = auctionId,
            PricePerSqm = pricePerSqm,
            MarketTrend = marketTrend,
            RenovationCost = renovationCost,
            NetYield = netYield,
            GrossYield = grossYield,
            PotentialResalePrice = potentialResalePrice,
            RiskFactorsJson = System.Text.Json.JsonSerializer.Serialize(riskFactors),
            StrengthsJson = System.Text.Json.JsonSerializer.Serialize(strengths),
            ModelVersion = modelVersion
        };
    }

    public List<string> GetRiskFactors() =>
        System.Text.Json.JsonSerializer.Deserialize<List<string>>(RiskFactorsJson) ?? [];

    public List<string> GetStrengths() =>
        System.Text.Json.JsonSerializer.Deserialize<List<string>>(StrengthsJson) ?? [];
}
