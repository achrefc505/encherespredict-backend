using EncheresPredict.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EncheresPredict.Application.Auctions.Commands.CompleteDocumentSummary;

public sealed class CompleteDocumentSummaryCommandHandler(
    IApplicationDbContext context)
    : IRequestHandler<CompleteDocumentSummaryCommand>
{
    public async Task Handle(
        CompleteDocumentSummaryCommand command,
        CancellationToken ct)
    {
        var summary = await context.DocumentSummaries
            .FirstOrDefaultAsync(
                x => x.Id == command.SummaryId,
                ct);

        if (summary is null)
        {
            throw new KeyNotFoundException(
                $"DocumentSummary '{command.SummaryId}' not found.");
        }

        summary.MarkReady(command.SummaryJson);

        await context.SaveChangesAsync(ct);
    }
}