using EncheresPredict.Domain.Enums;
using MediatR;

namespace EncheresPredict.Application.Auctions.Commands.CreateAuction;

public sealed record CreateAuctionCommand(
    string Title, string Tribunal, string City, string Region, string Address,
    int Surface, int Rooms, string Type,
    decimal StartPrice, decimal AiEstimate, int Confidence,
    AuctionStatus Status, DateTime AuctionDate, string Description
) : IRequest<Guid>;
