using EncheresPredict.Api.Contracts.Auctions;
using EncheresPredict.Api.Contracts.InternalSummaries;
using EncheresPredict.Application.Auctions.Commands.CompleteDocumentSummary;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EncheresPredict.Api.Filters;


namespace EncheresPredict.Api.Controllers;

[ApiController]
[Route("api/internal/summaries")]
public class InternalSummariesController(IMediator mediator) : ControllerBase
{
    [HttpPost("{summaryId:guid}")]
    [AllowAnonymous]
    [ServiceFilter(typeof(ApiKeyFilter))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Complete(
        Guid summaryId,
        [FromBody] CompleteDocumentSummaryRequest request,
        CancellationToken ct)
    {
        await mediator.Send(
            new CompleteDocumentSummaryCommand(
                summaryId,
                request.SummaryJson),
            ct);

        return Ok(new
        {
            success = true
        });
    }
}