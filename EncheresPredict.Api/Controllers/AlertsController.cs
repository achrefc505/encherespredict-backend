using EncheresPredict.Application.Alerts.Commands.MarkAlertAsRead;
using EncheresPredict.Application.Alerts.Queries.GetAlerts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EncheresPredict.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AlertDto>), 200)]
    public async Task<IActionResult> GetAll(CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetAlertsQuery(), ct);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/read")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> MarkAsRead(Guid id, CancellationToken ct = default)
    {
        await mediator.Send(new MarkAlertAsReadCommand(id), ct);
        return NoContent();
    }
}
