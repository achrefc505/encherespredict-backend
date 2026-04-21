using EncheresPredict.Domain.Enums;
using EncheresPredict.Domain.ValueObjects;

namespace EncheresPredict.Domain.Entities;

public class Auction : BaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public string Tribunal { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Region { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public int Surface { get; private set; }
    public int Rooms { get; private set; }
    public string Type { get; private set; } = string.Empty;
    public decimal StartPriceAmount { get; private set; }
    public decimal AiEstimateAmount { get; private set; }
    public int Confidence { get; private set; }
    public decimal RoiValue { get; private set; }
    public BadgeType Badge { get; private set; }
    public AuctionStatus Status { get; private set; }
    public DateTime AuctionDate { get; private set; }
    public string Description { get; private set; } = string.Empty;

    public AiAnalysis? AiAnalysis { get; private set; }

    private readonly List<Document> _documents = [];
    public IReadOnlyCollection<Document> Documents => _documents.AsReadOnly();

    private Auction() { }

    public static Auction Create(
        string title, string tribunal, string city, string region, string address,
        int surface, int rooms, string type,
        decimal startPrice, decimal aiEstimate, int confidence,
        AuctionStatus status, DateTime auctionDate, string description)
    {
        var startMoney = new Money(startPrice);
        var estimateMoney = new Money(aiEstimate);
        var roi = RoiScore.Calculate(startMoney, estimateMoney);

        return new Auction
        {
            Title = title, Tribunal = tribunal, City = city, Region = region,
            Address = address, Surface = surface, Rooms = rooms, Type = type,
            StartPriceAmount = startPrice, AiEstimateAmount = aiEstimate,
            Confidence = confidence, RoiValue = roi.Value, Badge = roi.ToBadge(),
            Status = status, AuctionDate = auctionDate, Description = description
        };
    }

    public Money StartPrice => new(StartPriceAmount);
    public Money AiEstimate => new(AiEstimateAmount);
    public RoiScore Roi => RoiScore.Calculate(StartPrice, AiEstimate);

    public void SetAiAnalysis(AiAnalysis analysis) => AiAnalysis = analysis;
    public void AddDocument(Document doc) => _documents.Add(doc);
}
