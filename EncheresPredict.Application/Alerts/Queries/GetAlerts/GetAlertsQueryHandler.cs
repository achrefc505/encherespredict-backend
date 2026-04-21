using EncheresPredict.Domain.Repositories;
using MediatR;

namespace EncheresPredict.Application.Alerts.Queries.GetAlerts;

public sealed class GetAlertsQueryHandler(IAlertRepository repo)
    : IRequestHandler<GetAlertsQuery, IEnumerable<AlertDto>>
{
    public async Task<IEnumerable<AlertDto>> Handle(GetAlertsQuery q, CancellationToken ct)
    {
        var alerts = await repo.GetAllAsync(ct);
        return alerts.Select(a => new AlertDto(a.Id, a.AuctionId, a.Type.ToString(), a.Title, a.Message, a.IsRead, a.CreatedAt));
    }
}
