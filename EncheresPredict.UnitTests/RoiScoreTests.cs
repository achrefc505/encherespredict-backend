using FluentAssertions;
using EncheresPredict.Domain.ValueObjects;

namespace EncheresPredict.UnitTests;

public class RoiScoreTests
{
    [Fact]
    public void Calculate_Should_Return_50_Percent()
    {
        // Arrange
        var startPrice = new Money(100000);
        var aiEstimate = new Money(150000);

        // Act
        var result =
            RoiScore.Calculate(startPrice, aiEstimate);

        // Assert
        result.Value.Should().Be(50);
    }

    [Fact]
    public void Calculate_Should_Return_Zero_When_StartPrice_Is_Zero()
    {
        // Arrange
        var startPrice = new Money(0);
        var aiEstimate = new Money(150000);

        // Act
        var result =
            RoiScore.Calculate(startPrice, aiEstimate);

        // Assert
        result.Value.Should().Be(0);
    }
}