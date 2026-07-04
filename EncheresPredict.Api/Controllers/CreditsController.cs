using EncheresPredict.Application.Credits.Commands.ConsumeCredit;
using EncheresPredict.Application.Credits.Commands.GrantCredits;
using EncheresPredict.Application.Credits.Queries.GetCreditBalance;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EncheresPredict.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreditsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(CreditBalanceDto), 200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetBalance(CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetCreditBalanceQuery(), ct);
        return Ok(result);
    }

    [HttpPost("grant")]
    [ProducesResponseType(204)]
    [ProducesResponseType(422)]
    public async Task<IActionResult> Grant(
        [FromBody] GrantCreditsCommand command,
        CancellationToken ct = default)
    {
        await mediator.Send(command, ct);
        return NoContent();
    }

    [HttpPost("consume")]
    [ProducesResponseType(204)]
    [ProducesResponseType(409)]
    [ProducesResponseType(422)]
    public async Task<IActionResult> Consume(
        [FromBody] ConsumeCreditCommand command,
        CancellationToken ct = default)
    {
        await mediator.Send(command, ct);
        return NoContent();
    }
}
