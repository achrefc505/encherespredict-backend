using EncheresPredict.Domain.Entities;
using EncheresPredict.Domain.Repositories;
using MediatR;

namespace EncheresPredict.Application.Auctions.Commands.CreateAuction;

public sealed class CreateAuctionCommandHandler(IAuctionRepository repo)
    : IRequestHandler<CreateAuctionCommand, Guid>
{
    public async Task<Guid> Handle(CreateAuctionCommand c, CancellationToken ct)
    {
        var auction = Auction.Create(
            c.Title, c.Tribunal, c.City, c.Region, c.Address,
            c.Surface, c.Rooms, c.Type,
            c.StartPrice, c.AiEstimate, c.Confidence,
            c.Status, c.AuctionDate, c.Description);

        await repo.AddAsync(auction, ct);
        return auction.Id;
    }
}
