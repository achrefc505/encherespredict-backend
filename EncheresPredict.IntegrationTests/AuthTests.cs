using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace EncheresPredict.IntegrationTests;

public class AuthTests
{
    [Fact]
    public async Task Should_Return_401_When_No_Token_Is_Provided()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>();

        var client = factory.CreateClient();

        // Act
        var response =
            await client.GetAsync("/api/Auctions");

        // Assert
        Assert.Equal(
            HttpStatusCode.Unauthorized,
            response.StatusCode);
    }
}