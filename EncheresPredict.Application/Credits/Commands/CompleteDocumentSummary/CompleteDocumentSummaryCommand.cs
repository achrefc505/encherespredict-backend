using MediatR;

namespace EncheresPredict.Application.Auctions.Commands.CompleteDocumentSummary;

public sealed record CompleteDocumentSummaryCommand(
    Guid SummaryId,
    string SummaryJson
) : IRequest;