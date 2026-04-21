using EncheresPredict.Domain.Enums;

namespace EncheresPredict.Domain.Entities;

public class Alert : BaseEntity
{
    public Guid? AuctionId { get; private set; }
    public AlertType Type { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Message { get; private set; } = string.Empty;
    public bool IsRead { get; private set; }

    private Alert() { }

    public static Alert Create(AlertType type, string title, string message, Guid? auctionId = null) =>
        new() { Type = type, Title = title, Message = message, AuctionId = auctionId };

    public void MarkAsRead() => IsRead = true;
}
