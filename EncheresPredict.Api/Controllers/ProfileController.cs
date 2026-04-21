using EncheresPredict.Application.Profile.Commands.SaveProfile;
using EncheresPredict.Application.Profile.Queries.GetProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EncheresPredict.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ProfileDto), 200)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> Get(CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetProfileQuery(), ct);
        return result is null ? NoContent() : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    [ProducesResponseType(422)]
    public async Task<IActionResult> Save([FromBody] SaveProfileCommand command, CancellationToken ct = default)
    {
        var id = await mediator.Send(command, ct);
        return CreatedAtAction(nameof(Get), new { id });
    }
}
