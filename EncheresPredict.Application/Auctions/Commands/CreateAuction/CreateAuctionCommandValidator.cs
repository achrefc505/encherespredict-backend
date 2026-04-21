using FluentValidation;

namespace EncheresPredict.Application.Auctions.Commands.CreateAuction;

public sealed class CreateAuctionCommandValidator : AbstractValidator<CreateAuctionCommand>
{
    public CreateAuctionCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Tribunal).NotEmpty().MaximumLength(100);
        RuleFor(x => x.City).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Region).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Surface).GreaterThan(0);
        RuleFor(x => x.Rooms).GreaterThanOrEqualTo(0);
        RuleFor(x => x.StartPrice).GreaterThan(0);
        RuleFor(x => x.AiEstimate).GreaterThan(0);
        RuleFor(x => x.Confidence).InclusiveBetween(0, 100);
        RuleFor(x => x.AuctionDate).GreaterThan(DateTime.UtcNow.AddDays(-1));
    }
}
