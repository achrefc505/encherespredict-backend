using EncheresPredict.Application.Auctions.Queries.GetAuctions;
using EncheresPredict.Domain.Entities;
using EncheresPredict.Domain.Enums;
using EncheresPredict.Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace EncheresPredict.UnitTests;

public class GetAuctionsQueryHandlerTests
{
    [Fact]
    public async Task Should_Return_Auctions_From_Repository()
    {
        // Arrange

        var repo = Substitute.For<IAuctionRepository>();

        var auctions = new List<Auction>
        {
            Auction.Create(
                "Appartement T3",
                "TJ Paris",
                "Paris",
                "Ile-de-France",
                "1 rue test",
                70,
                3,
                "Appartement",
                100000,
                130000,
                90,
                AuctionStatus.Active,
                DateTime.UtcNow,
                "Description")
        };

        repo.GetFilteredAsync(
            Arg.Any<string?>(),
            Arg.Any<string?>(),
            Arg.Any<BadgeType?>(),
            Arg.Any<string?>(),
            Arg.Any<decimal?>(),
            Arg.Any<decimal?>(),
            Arg.Any<string>(),
            Arg.Any<int>(),
            Arg.Any<int>(),
            Arg.Any<CancellationToken>())
        .Returns((auctions, 1));

        var handler = new GetAuctionsQueryHandler(repo);

        var query = new GetAuctionsQuery();

        // Act

        var result =
            await handler.Handle(
                query,
                CancellationToken.None);

        // Assert

        result.Should().NotBeNull();

        result.Total.Should().Be(1);

        result.Items.Should().HaveCount(1);

        result.Items.First().Title.Should().Be("Appartement T3");
    }
}