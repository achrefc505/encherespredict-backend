using EncheresPredict.Application.Auctions.Commands.CreateAuction;
using EncheresPredict.Application.Auctions.Queries.GetAuctionById;
using EncheresPredict.Application.Auctions.Queries.GetAuctions;
using EncheresPredict.Application.Auctions.Queries.GetDashboardStats;
using EncheresPredict.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EncheresPredict.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuctionsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetAuctionsResult), 200)]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? city, [FromQuery] string? type,
        [FromQuery] BadgeType? badge, [FromQuery] string? region,
        [FromQuery] decimal? budgetMin, [FromQuery] decimal? budgetMax,
        [FromQuery] string sort = "roi", [FromQuery] int page = 1, [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(
            new GetAuctionsQuery(city, type, badge, region, budgetMin, budgetMax, sort, page, pageSize), ct);
        return Ok(result);
    }

    [HttpGet("stats")]
    [ProducesResponseType(typeof(DashboardStatsDto), 200)]
    public async Task<IActionResult> GetStats(CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetDashboardStatsQuery(), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AuctionDetailDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetAuctionByIdQuery(id), ct);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), 201)]
    [ProducesResponseType(422)]
    public async Task<IActionResult> Create([FromBody] CreateAuctionCommand command, CancellationToken ct = default)
    {
        var id = await mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }
}
