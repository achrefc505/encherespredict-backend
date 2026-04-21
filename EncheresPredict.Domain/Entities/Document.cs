namespace EncheresPredict.Domain.Entities;

public class Document : BaseEntity
{
    public Guid AuctionId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Type { get; private set; } = string.Empty;
    public string Size { get; private set; } = string.Empty;
    public bool Available { get; private set; }

    private Document() { }

    public static Document Create(Guid auctionId, string name, string type, string size, bool available = true) =>
        new() { AuctionId = auctionId, Name = name, Type = type, Size = size, Available = available };
}
