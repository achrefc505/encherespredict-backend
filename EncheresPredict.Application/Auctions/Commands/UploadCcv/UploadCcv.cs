using MediatR;

namespace EncheresPredict.Application.Auctions.Commands.UploadCcv;

public sealed record UploadCcvCommand(
    Guid AuctionId,
    byte[] PdfContent,
    string FileName) : IRequest;