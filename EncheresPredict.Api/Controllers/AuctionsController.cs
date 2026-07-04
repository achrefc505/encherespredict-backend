using EncheresPredict.Api.Contracts.Auctions;
using EncheresPredict.Application.Auctions.Commands.CreateAuction;
using EncheresPredict.Application.Auctions.Commands.UploadCcv;
using EncheresPredict.Application.Auctions.Queries.GetAuctionById;
using EncheresPredict.Application.Auctions.Queries.GetAuctionSummary;
using EncheresPredict.Application.Auctions.Queries.GetAuctions;
using EncheresPredict.Application.Auctions.Queries.GetDashboardStats;
using EncheresPredict.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EncheresPredict.Application.Auctions.Commands.CompleteDocumentSummary;

namespace EncheresPredict.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AuctionsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetAuctionsResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? city,
        [FromQuery] string? type,
        [FromQuery] BadgeType? badge,
        [FromQuery] string? region,
        [FromQuery] decimal? budgetMin,
        [FromQuery] decimal? budgetMax,
        [FromQuery] string sort = "roi",
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(
            new GetAuctionsQuery(
                city,
                type,
                badge,
                region,
                budgetMin,
                budgetMax,
                sort,
                page,
                pageSize),
            ct);

        return Ok(result);
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats(CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetDashboardStatsQuery(), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetAuctionByIdQuery(id), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}/summary")]
    public async Task<IActionResult> GetSummary(Guid id, CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetAuctionSummaryQuery(id), ct);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateAuctionCommand command,
        CancellationToken ct = default)
    {
        var id = await mediator.Send(command, ct);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPost("{id:guid}/ccv")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadCcv(
        [FromRoute] Guid id,
        [FromForm] UploadCcvRequest request,
        CancellationToken ct)
    {
        using var memory = new MemoryStream();

        await request.Pdf.CopyToAsync(memory, ct);

        await mediator.Send(
            new UploadCcvCommand(
                id,
                memory.ToArray(),
                request.Pdf.FileName),
            ct);

        return Accepted();
    }
    [HttpPost("callback")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Callback(
    [FromBody] CompleteDocumentSummaryRequest request,
    CancellationToken ct)
    {
        await mediator.Send(
            new CompleteDocumentSummaryCommand(
                request.SummaryId,
                request.SummaryJson),
            ct);

        return Ok(new
        {
            success = true
        });
    }
}