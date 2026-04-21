using EncheresPredict.Domain.Repositories;
using MediatR;

namespace EncheresPredict.Application.Alerts.Commands.MarkAlertAsRead;

public sealed class MarkAlertAsReadCommandHandler(IAlertRepository repo)
    : IRequestHandler<MarkAlertAsReadCommand>
{
    public async Task Handle(MarkAlertAsReadCommand c, CancellationToken ct)
    {
        var alert = await repo.GetByIdAsync(c.Id, ct)
            ?? throw new KeyNotFoundException($"Alerte '{c.Id}' introuvable.");
        alert.MarkAsRead();
        await repo.UpdateAsync(alert, ct);
    }
}
