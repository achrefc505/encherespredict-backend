using EncheresPredict.Application.Common.Interfaces;
using EncheresPredict.Application.Credits.Commands.ConsumeCredit;
using EncheresPredict.Application.Credits.Commands.GrantCredits;
using EncheresPredict.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using EncheresPredict.Application.Common.Configuration;

namespace EncheresPredict.Application.Auctions.Commands.UploadCcv;

public sealed class UploadCcvCommandHandler(
    IMediator mediator,
    ICurrentUserService currentUser,
    IFileStorage fileStorage,
    IApplicationDbContext context,
    IOptions<AppOptions> appOptions,
    IN8nClient n8nClient)
    : IRequestHandler<UploadCcvCommand>
{
    public async Task Handle(
        UploadCcvCommand command,
        CancellationToken ct)
    {
        if (currentUser.UserId is null)
        {
            throw new UnauthorizedAccessException(
                "User is not authenticated.");
        }

        await mediator.Send(
            new ConsumeCreditCommand("Analyse CCV"),
            ct);

        var pdfPath = await fileStorage.SaveAsync(
     command.PdfContent,
     command.FileName,
     ct);

        var fileName = Path.GetFileName(pdfPath);

        var pdfUrl =
            $"{appOptions.Value.PublicBaseUrl}/storage/{fileName}";

        var summary = DocumentSummary.Create(
            command.AuctionId,
            pdfUrl,
            "claude-sonnet-4-6");

        context.DocumentSummaries.Add(summary);

        await context.SaveChangesAsync(ct);

        try
        {
            await n8nClient.SendDocumentForAnalysisAsync(
                summary.Id,
                pdfUrl,
                ct);

            summary.MarkProcessing();

            await context.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            summary.MarkFailed(ex.Message);

            await context.SaveChangesAsync(ct);

            await mediator.Send(
                new GrantCreditsCommand(
                    currentUser.UserId!,
                    1,
                    "Remboursement analyse CCV"),
                ct);

            throw;
        }

        // TODO
    }
}