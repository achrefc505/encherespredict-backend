using EncheresPredict.Domain.ValueObjects;
using FluentAssertions;

namespace EncheresPredict.UnitTests;

public class MoneyTests
{
    [Fact]
    public void Should_Create_Money_With_Valid_Amount()
    {
        // Arrange & Act
        var money = new Money(100000);

        // Assert
        money.Amount.Should().Be(100000);
        money.Currency.Should().Be("EUR");
    }

    [Fact]
    public void Should_Create_Money_With_Custom_Currency()
    {
        // Arrange & Act
        var money = new Money(1000, "USD");

        // Assert
        money.Amount.Should().Be(1000);
        money.Currency.Should().Be("USD");
    }

    [Fact]
    public void Should_Throw_When_Amount_Is_Negative()
    {
        // Arrange
        Action act = () => new Money(-100);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Should_Return_Zero_Money()
    {
        // Act
        var money = Money.Zero;

        // Assert
        money.Amount.Should().Be(0);
        money.Currency.Should().Be("EUR");
    }

    [Fact]
    public void Should_Add_Two_Money_Values()
    {
        // Arrange
        var m1 = new Money(100);
        var m2 = new Money(50);

        // Act
        var result = m1 + m2;

        // Assert
        result.Amount.Should().Be(150);
    }

    [Fact]
    public void Should_Add_Zero_Correctly()
    {
        // Arrange
        var m1 = new Money(100);
        var m2 = Money.Zero;

        // Act
        var result = m1 + m2;

        // Assert
        result.Amount.Should().Be(100);
    }

    [Fact]
    public void Should_Subtract_Two_Money_Values()
    {
        // Arrange
        var m1 = new Money(100);
        var m2 = new Money(50);

        // Act
        var result = m1 - m2;

        // Assert
        result.Amount.Should().Be(50);
    }

    [Fact]
    public void Should_Return_True_When_First_Amount_Is_Greater()
    {
        // Arrange
        var m1 = new Money(200);
        var m2 = new Money(100);

        // Act & Assert
        (m1 > m2).Should().BeTrue();
    }

    [Fact]
    public void Should_Return_False_When_First_Amount_Is_Not_Greater()
    {
        // Arrange
        var m1 = new Money(100);
        var m2 = new Money(200);

        // Act & Assert
        (m1 > m2).Should().BeFalse();
    }

    [Fact]
    public void Should_Return_True_When_First_Amount_Is_Smaller()
    {
        // Arrange
        var m1 = new Money(100);
        var m2 = new Money(200);

        // Act & Assert
        (m1 < m2).Should().BeTrue();
    }

    [Fact]
    public void Should_Return_False_When_First_Amount_Is_Not_Smaller()
    {
        // Arrange
        var m1 = new Money(200);
        var m2 = new Money(100);

        // Act & Assert
        (m1 < m2).Should().BeFalse();
    }

    [Fact]
    public void Should_Format_Money_Correctly()
    {
        // Arrange
        var money = new Money(100000);

        // Act
        var formatted = money.Formatted;

        // Assert
        formatted.Should().Contain("100");
        formatted.Should().Contain("€");
    }
}