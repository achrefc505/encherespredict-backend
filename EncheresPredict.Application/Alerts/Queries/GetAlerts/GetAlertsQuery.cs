using MediatR;

namespace EncheresPredict.Application.Alerts.Queries.GetAlerts;

public sealed record GetAlertsQuery : IRequest<IEnumerable<AlertDto>>;

public sealed record AlertDto(Guid Id, Guid? AuctionId, string Type, string Title, string Message, bool IsRead, DateTime CreatedAt);
